using Diary.Main.Core.Config;
using Diary.Main.Core.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Simpler.Net.FileSystem.Abstractions;
using Simpler.Net.FileSystem.Abstractions.Implementations;

namespace Diary.Main.Infrastructure.Dependencies
{
	public class CoreDependencies
	{
		public static IServiceCollection AddTo(IServiceCollection services)
		{
			// Filesystem
			services.AddSingleton<IFileSystem, DotNetFileSystem>();

			// Config
			services.AddScoped(sp => DiaryConfig.ReadConfig<MainConfig>(saveDefaultConfig: true));

			// DB
			services.AddTransient<DiaryDbContext>();

			return services;
		}
	}
}
