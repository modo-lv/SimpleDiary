using System;
using System.IO;
using Diary.Main.Core.Config;
using Diary.Main.Core.Persistence;
using Microsoft.Extensions.DependencyInjection;
using static Diary.Api.Tests.TestEnvironment;

namespace Diary.Api.Tests
{
	public class TestEnvironment : Api.Startup
	{
		public override void ConfigureServices(IServiceCollection services) {
			// Replace DB path
			DiaryConfig.BaseDir = AppContext.BaseDirectory;

			base.ConfigureServices(services);

			TestDependencies.AddTo(services);
		}
	}
}