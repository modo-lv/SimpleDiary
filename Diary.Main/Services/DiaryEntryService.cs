using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Diary.Main.Core.Config;
using Diary.Main.Core.Persistence;
using Diary.Main.Domain.Entities;
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

		public DiaryEntryService(
			DiaryDbContext dbContext,
			IFileSystem fileSystem,
			MainConfig config,
			IStreamFactory streams)
		{
			this._dbContext = dbContext;
			this._fileSystem = fileSystem;
			this._config = config;
			this._streams = streams;
		}

		public Task<Entry> GetEntryAsync(UInt32 id) =>
			this._dbContext.Entries.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

		public async Task<IList<Entry>> GetEntriesAsync() =>
			await this._dbContext.Entries
				.Where(e => !e.IsDeleted)
				.OrderByDescending(e => e.Timestamp)
				.ToListAsync();

		/// <inheritdoc />
		public async Task<Entry> SaveEntryAsync(
			Entry entry,
			UInt32 id = 0,
			String newFileName = null,
			Stream newFileContents = null)
		{
			if (id > 0)
			{
				Entry existing = await this.GetEntryAsync(id);
				existing.Timestamp = entry.Timestamp;
				existing.Content = entry.Content;
				this._dbContext.Update(existing);
				entry = existing;
			}
			else
			{
				this._dbContext.Add(entry);
			}
			await this._dbContext.SaveChangesAsync();

			if (newFileContents == null)
				return entry;

			// Sort out all the names and paths
			newFileName = $"{entry.Id:D8}_{newFileName}";
			var oldFileName = entry.FileName;
			var hasOldFile = !String.IsNullOrEmpty(entry.FileName);
			var newFileTempPath = this._fileSystem.Path.GetTempPath() + Guid.NewGuid() + Path.GetExtension(newFileName);
			var oldFileTempPath = this._fileSystem.Path.GetTempPath() + Guid.NewGuid() + Path.GetExtension(oldFileName);
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
						// Move new file over the old and update DB
						this._fileSystem.CreateDirectoryForFile(newFilePath);
						this._fileSystem.File.Move(newFileTempPath, newFilePath);
						entry.FileName = newFileName;
						this._dbContext.Entries.Update(entry);
						await this._dbContext.SaveChangesAsync();
						trans.Commit();

						if (hasOldFile && newFileName != oldFileName)
							this._fileSystem.File.Delete(oldFilePath);
					}
					catch (Exception)
					{
						// If anything fails, move the old file back and cancel DB update
						trans.Rollback();
						if (hasOldFile)
							File.Move(oldFileTempPath, oldFilePath);
						throw;
					}
				}
			} finally
			{
				// Delete leftovers
				this._fileSystem.File.Delete(newFileTempPath);
				this._fileSystem.File.Delete(oldFileTempPath);
			}

			return entry;
		}
	}
}