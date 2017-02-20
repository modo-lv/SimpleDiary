using System;
using System.Collections.Generic;
using System.Text;
using Diary.Main.Core.Config;
using Diary.Main.Core.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Diary.Main.Infrastructure.Dependencies
{
	public class ConfigDependencies
	{
		public static IServiceCollection AddTo(IServiceCollection services)
		{
			return services.AddScoped(sp => MainConfig.ReadConfig<CoreConfig>());
		}
	}
}