using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Diary.Main.Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Diary.Main.Core.Persistence {
	public class DiaryContext : DbContext {
		public DbSet<Entry> Entries { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlite(
				@"Data Source=c:\home\martin\dev\misc\SimpleDiary\Diary.Data\diary.db");
		}
	}
}