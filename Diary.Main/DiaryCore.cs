using System;
using Diary.Main.Core.Persistence;
using Diary.Main.Infrastructure.Dependencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

		public static void Startup(IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetService<DiaryDbContext>();
			if (context == null)
				throw new NullReferenceException($"Service provider returned a null {nameof(DiaryDbContext)}.");
			context.Database.Migrate();
		}
	}
}