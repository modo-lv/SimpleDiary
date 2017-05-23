using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Main.Core.Config;
using Diary.Main.Core.Persistence;
using Diary.Main.Domain.Entities;
using Diary.Main.Domain.Models;
using Diary.Main.Infrastructure.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Simpler.Net.Io;
using Simpler.Net.Io.Abstractions;

namespace Diary.Main.Services
{
	public class DiaryEntryService : IDiaryEntryService
	{
		private readonly DiaryDbContext _dbContext;
		private readonly IFileSystem _fileSystem;
		private readonly MainConfig _config;
		private readonly IStreamFactory _streams;
		private IMapper _mapper;

		public DiaryEntryService(
			DiaryDbContext dbContext,
			IFileSystem fileSystem,
			MainConfig config,
			IStreamFactory streams,
			IMapper mapper)
		{
			this._dbContext = dbContext;
			this._fileSystem = fileSystem;
			this._config = config;
			this._streams = streams;
			this._mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Entry> GetEntryAsync(UInt32 id)
			=>
				await this._dbContext.Entries
					.Include(e => e.TextContent)
					.Include(e => e.FileContent)
					.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);


		/// <inheritdoc />
		public async Task<IList<Entry>> GetEntriesAsync()
			=>
				await this._dbContext.Entries
					.Include(e => e.TextContent)
					.Include(e => e.FileContent)
					.Where(e => !e.IsDeleted)
					.OrderByDescending(e => e.Timestamp)
					.ToListAsync();


		/// <inheritdoc />
		public async Task<Entry> SaveEntryAsync(
			EntryModel input,
			UInt32 id = 0,
			Stream newFileContents = null)
		{
			Entry entry;
			if (id > 0)
			{
				entry = await this.GetEntryAsync(id);
				this._mapper.Map(input, entry);
				this._dbContext.Update(entry);
			}
			else
			{
				entry = this._mapper.Map<Entry>(input);
				this._dbContext.Add(entry);
			}
			await this._dbContext.SaveChangesAsync();

			if (input.Type != EntryType.File)
				return entry;

			var newFileName = $"{input.Id:D8}_{input.FileContent.FileName}";
			var oldFileName = entry.FileContent.FileName;
			var hasOldFile = !String.IsNullOrEmpty(oldFileName);

			// Rename only
			if (hasOldFile && newFileContents == null && newFileName != oldFileName)
			{
				this._fileSystem.File.Move(oldFileName, newFileName);
				try {
					entry.FileContent.FileName = newFileName;
					this._dbContext.Update(entry);
					await this._dbContext.SaveChangesAsync();
				}
				catch (Exception)
				{
					// In case of errors updating DB, move the file back
					this._fileSystem.File.Move(newFileName, oldFileName);
					throw;
				}
			}
			// Update file
			else if (newFileContents != null)
			{
				var newFileTempPath = 
					this._fileSystem.Path.GetTempPath() + Guid.NewGuid() + Path.GetExtension(newFileName);
				var oldFileTempPath = 
					this._fileSystem.Path.GetTempPath() + Guid.NewGuid() + Path.GetExtension(oldFileName);
				var newFilePath = SimplerPath.Combine(this._config.FileStorageDir, newFileName);
				var oldFilePath = SimplerPath.Combine(this._config.FileStorageDir, oldFileName);

				try
				{
					// Copy new and current files to temp
					using (Stream tempFile = this._streams.GetFileStream(newFileTempPath, FileMode.Create))
					{
						await newFileContents.CopyToAsync(tempFile);
					}

					if (hasOldFile)
						File.Copy(oldFilePath, oldFileTempPath);

					using (IDbContextTransaction trans = await this._dbContext.Database.BeginTransactionAsync())
					{
						try
						{
							// Move new file over
							this._fileSystem.CreateDirectoryForFile(newFilePath);
							if (hasOldFile && newFileName == oldFileName)
								this._fileSystem.File.Delete(oldFilePath);
							this._fileSystem.File.Move(newFileTempPath, newFilePath);

							// Update entry
							if (!hasOldFile)
								entry.FileContent = new EntryFileContent();
							input.FileContent.FileName = newFileName;
							this._dbContext.Entries.Update(entry);
							await this._dbContext.SaveChangesAsync();
							trans.Commit();

							// Delete old file
							if (hasOldFile && newFileName != oldFileName)
								this._fileSystem.File.Delete(oldFilePath);
						}
						catch (Exception)
						{
							// If anything fails, move the old file back and cancel DB update
							if (hasOldFile)
								this._fileSystem.File.Move(oldFileTempPath, oldFilePath);
							trans.Rollback();
							throw;
						}
					}
				} finally
				{
					// Delete leftovers
					this._fileSystem.File.Delete(newFileTempPath);
					this._fileSystem.File.Delete(oldFileTempPath);
				}
			}

			return entry;
		}
	}
}