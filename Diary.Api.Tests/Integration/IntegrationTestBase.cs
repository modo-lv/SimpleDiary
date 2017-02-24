using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Diary.Api.Tests.Integration
{
	public class IntegrationTestBase : IClassFixture<TestFixture> {
		protected readonly TestServer _server;
		protected readonly HttpClient _client;
		protected readonly TestFixture _fixture;

		public IntegrationTestBase(TestFixture fixture) {
			this._fixture = fixture;

			// Arrange
			this._server = new TestServer(new WebHostBuilder()
					.UseStartup<TestEnvironment>());
			this._client = this._server.CreateClient();
		}
	}
}
