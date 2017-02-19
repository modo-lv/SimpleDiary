using System;
using System.Collections.Generic;
using System.Text;
using Diary.Main.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Diary.Main.Infrastructure {
	public class DiaryDepdenencies {
		public static IServiceCollection AddMainServicesTo(IServiceCollection services)
		{
			services.AddScoped<IHiService, HiService>();

			return services;
		}
	}
}