using Diary.Main.Core.Config;
using Diary.Main.Core.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Simpler.Net.Io.Abstractions;
using Simpler.Net.Io.Abstractions.Implementations;

namespace Diary.Main.Infrastructure.Dependencies
{
	public class CoreDependencies
	{
		public static IServiceCollection AddTo(IServiceCollection services)
		{
			// Filesystem
			services.AddSingleton<IFileSystem, DotNetFileSystem>();
			services.AddSingleton<IStreamFactory, DotNetStreamFactory>();

			// Config
			services.AddScoped(sp => DiaryConfig.ReadConfig<MainConfig>(saveDefaultConfig: true));

			// DB
			services.AddTransient<DiaryDbContext>();

			return services;
		}
	}
}
