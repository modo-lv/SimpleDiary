using System;
using System.IO;
using Diary.Main.Core.Config;
using Diary.Main.Core.Persistence;
using Diary.Main.Infrastructure.Dependencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Simpler.Net.Io.Abstractions;

namespace Diary.Main
{
	public class DiaryCore
	{
		public static void Init(IServiceCollection services)
		{
			services.AddLogging();

			CoreDependencies.AddTo(services);
			ServiceDependencies.AddTo(services);
		}

		/// <summary>
		/// Fundamental startup config that applies to all types of instances (web, API, desktop, ...)
		/// </summary>
		/// <param name="serviceProvider">Configured service provider.</param>
		public static void Startup(IServiceProvider serviceProvider)
		{
			var config = serviceProvider.GetRequiredService<MainConfig>();

			// Files and folders
			var fs = serviceProvider.GetRequiredService<IFileSystem>();
			fs.Directory.CreateDirectory(config.FileStorageDir);

			// Database
			var context = serviceProvider.GetRequiredService<DiaryDbContext>();
			context.Database.Migrate();
		}
	}
}