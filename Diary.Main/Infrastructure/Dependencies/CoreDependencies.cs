using Diary.Main.Core.Config;
using Diary.Main.Core.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Diary.Main.Infrastructure.Dependencies
{
	public class CoreDependencies
	{
		public static IServiceCollection AddTo(IServiceCollection services)
		{
			// Config
			services.AddScoped(sp => DiaryConfig.ReadConfig<MainConfig>(saveDefaultConfig: true));

			// DB
			services.AddTransient<DiaryDbContext>();

			return services;
		}
	}
}
