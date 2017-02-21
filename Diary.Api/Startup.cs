using System;
using AutoMapper;
using Diary.Api.Infrastructure;
using Diary.Main;
using Diary.Main.Infrastructure;
using Diary.Main.Infrastructure.Dependencies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Diary.Api {
	public class Startup {
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			var mapperConfig = new MapperConfiguration(cfg => {ApiObjectMapper.Configure(cfg);});
			IMapper mapper = mapperConfig.CreateMapper();

			services
				.AddSingleton(mapper)

				.AddMvc();

			DiaryCore.Init(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();

			DiaryCore.Startup(app.ApplicationServices);

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}
}