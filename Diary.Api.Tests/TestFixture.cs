using System;
using System.Net.Http;
using AutoMapper;
using Diary.Main.Core.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Diary.Api.Tests
{
	public class TestFixture : IDisposable
	{
		public DiaryDbContext DbContext;
		public readonly TestServer Server;
		public readonly HttpClient Client;
		public readonly IServiceProvider Services;
		public readonly IMapper Mapper;

		public TestFixture()
		{
			this.Server = new TestServer(new WebHostBuilder()
				.UseStartup<TestEnvironment>());
			this.Client = this.Server.CreateClient();
			this.Services = this.Server.Host.Services;
			this.Mapper = this.Services.GetService<IMapper>();
			this.DbContext = this.Services.GetService<DiaryDbContext>();

			if (this.DbContext == null)
				throw new NullReferenceException($"Service provider returned a null {nameof(DiaryDbContext)}.");
		}

		public void Dispose()
		{
			// New DB for each test class.
			this.DbContext.Database.EnsureDeleted();
		}
	}
}