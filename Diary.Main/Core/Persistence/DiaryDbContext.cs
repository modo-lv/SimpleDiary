﻿using System;
using System.IO;
using Diary.Main.Core.Config;
using Diary.Main.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diary.Main.Core.Persistence {
	public class DiaryDbContext : DbContext {
		private readonly CoreConfig _config;

		public DbSet<Entry> Entries { get; set; }

		public DiaryDbContext(CoreConfig config) {
			this._config = config;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			String path = Path.GetFullPath(this._config.DbFilePath);

			optionsBuilder.UseSqlite($"Data Source={path}");
		}
	}
}