using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Main.Core.Persistence;
using Diary.Main.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diary.Main.Services
{
	public class DiaryEntryService : IDiaryEntryService
	{
		private readonly DiaryDbContext _dbContext;

		public DiaryEntryService(DiaryDbContext dbContext) { this._dbContext = dbContext; }

		public Task<Entry> GetEntryAsync(UInt32 id) =>
			this._dbContext.Entries.FirstOrDefaultAsync();

		public async Task<IList<Entry>> GetEntriesAsync() =>
			await this._dbContext.Entries
				.ToListAsync();
	}
}