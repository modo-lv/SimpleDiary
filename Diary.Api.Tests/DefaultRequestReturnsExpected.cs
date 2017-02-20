using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Diary.Api.Tests
{
	public class DefaultRequestReturnsExpected {
		private readonly TestServer _server;
		private readonly HttpClient _client;
		public DefaultRequestReturnsExpected() {
			// Arrange
			this._server = new TestServer(new WebHostBuilder()
					.UseStartup<Startup>());
			this._client = this._server.CreateClient();
		}

		[Fact]
		public async Task ReturnHelloWorld() {
			// Act
			HttpResponseMessage response = await this._client.GetAsync("/api/test");
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.StartsWith(@"D:\home\martin\dev\misc\SimpleDiary\Diary.Api", responseString);
		}
	}
}
