using System;
using System.Collections.Generic;
using System.Text;
using Diary.Main.Core.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Diary.Main.Infrastructure.Dependencies
{
	public class CoreDependencies
	{
		public static IServiceCollection AddTo(IServiceCollection services)
		{
			return services.AddTransient<DiaryDbContext>();
		}
	}
}
