using Diary.Main.Core.Config;
using Diary.Main.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diary.Main.Core.Persistence {
	public class DiaryDbContext : DbContext {
		private readonly MainConfig _config;

		public DbSet<Entry> Entries { get; set; }


		#if DEBUG
		/// <summary>
		/// For use in DB migration generation
		/// </summary>
		public DiaryDbContext()
		{
			this._config = DiaryConfig.ReadConfig<MainConfig>();
		}
		#endif

		public DiaryDbContext(MainConfig config) {
			this._config = config;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlite($"Data Source={this._config.DbFilePath}");
		}
	}
}