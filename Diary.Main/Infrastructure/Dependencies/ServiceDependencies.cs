using Diary.Main.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Diary.Main.Infrastructure.Dependencies {
	public class ServiceDependencies {
		public static IServiceCollection AddTo(IServiceCollection services)
		{
			services.AddScoped<IDiaryEntryService, DiaryEntryService>();

			return services;
		}
	}
}