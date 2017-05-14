using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Api.Infrastructure;
using Diary.Main;
using Diary.Main.Infrastructure.ObjectMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Diary.Web
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			var mapperConfig = new MapperConfiguration(
				cfg =>
				{
					MainObjectMapper.Configure(cfg);
					ApiObjectMapper.Configure(cfg);
				});
			IMapper mapper = mapperConfig.CreateMapper();

			services
				.AddSingleton(mapper)
				.AddMvc()
				.AddViewLocalization()
				.AddApplicationPart(typeof(Api.Startup).GetTypeInfo().Assembly)
				.AddControllersAsServices();

			DiaryCore.Init(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseStaticFiles();

			app.UseRequestLocalization(
				new RequestLocalizationOptions
				{
					DefaultRequestCulture = new RequestCulture("lv"),
					SupportedCultures = new[]
					{
						new CultureInfo("lv"),
						new CultureInfo("en-GB"), 
						new CultureInfo("en-US"),
					}
				});

			loggerFactory.AddConsole();

			DiaryCore.Startup(app.ApplicationServices);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvcWithDefaultRoute();
		}
	}
}
