﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Main.Core.Config;
using Diary.Main.Core.Persistence;
using Diary.Main.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Simpler.Net.Io;
using Simpler.Net.Io.Abstractions;

namespace Diary.Main.Services
{
	public class DiaryEntryService : IDiaryEntryService
	{
		private readonly DiaryDbContext _dbContext;
		private IMapper _mapper;
		private readonly IFileSystem _fileSystem;
		private readonly MainConfig _config;
		private IServiceProvider _services;
		private readonly IStreamFactory _streams;

		public DiaryEntryService(
			DiaryDbContext dbContext,
			IMapper mapper,
			IFileSystem fileSystem,
			MainConfig config,
			IServiceProvider services,
			IStreamFactory streams)
		{
			this._dbContext = dbContext;
			this._mapper = mapper;
			this._fileSystem = fileSystem;
			this._config = config;
			this._services = services;
			this._streams = streams;
		}

		public Task<Entry> GetEntryAsync(UInt32 id) =>
			this._dbContext.Entries.FirstOrDefaultAsync(e => !e.IsDeleted);

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
				entry = await this.GetEntryAsync(id);
			}
			else
			{
				this._dbContext.Add(entry);
				await this._dbContext.SaveChangesAsync();
			}

			if (newFileContents == null)
				return entry;

			// Sort out all the names and paths
			var oldFileName = entry.FilePath;
			var newFileTempPath = this._fileSystem.Path.GetTempFileName();
			var oldFileTempPath = this._fileSystem.Path.GetTempFileName();
			var newFilePath = SimplerPath.Combine(this._config.FileStorageDir, newFileName);
			var oldFilePath = SimplerPath.Combine(this._config.FileStorageDir, oldFileName);

			try
			{
				// Copy new and current files to temp
				using (Stream tempFile = this._streams.GetFileStream(newFileTempPath, FileMode.Create))
				{
					await newFileContents.CopyToAsync(tempFile);
				}
				File.Copy(oldFilePath, oldFileTempPath);

				using (IDbContextTransaction trans = await this._dbContext.Database.BeginTransactionAsync())
				{
					try
					{
						// Move new file over the old and update DB
						this._fileSystem.File.Move(newFileTempPath, newFilePath);
						entry.FilePath = newFileName;
						this._dbContext.Entries.Update(entry);
						await this._dbContext.SaveChangesAsync();
						trans.Commit();

						if (newFileName != oldFileName)
							this._fileSystem.File.Delete(oldFilePath);
					}
					catch (Exception)
					{
						// If anything fails, move the old file back and cancel DB update
						trans.Rollback();
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