using System;
using System.IO;
using Diary.Main.Core.Config;
using Diary.Main.Core.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Diary.Api.Tests.TestEnvironment;

namespace Diary.Api.Tests
{
	public class TestEnvironment : Api.Startup
	{
		/// <summary>
		/// 
		/// </summary>
		public static IServiceProvider Services;

		public override void ConfigureServices(IServiceCollection services) {
			// Replace DB path
			DiaryConfig.BaseDir = Directory.GetCurrentDirectory();

			base.ConfigureServices(services);
		}

		public override void Configure(
			IApplicationBuilder app,
			IHostingEnvironment env,
			ILoggerFactory loggerFactory)
		{
			Services = app.ApplicationServices;

			// Move on to real configure
			base.Configure(app, env, loggerFactory);
		}
	}
}