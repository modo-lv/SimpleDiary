using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Diary.Api.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
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
		public async Task TestBasicRestCall() {
			// Act
			HttpResponseMessage response = await this._client.PostAsync(
				"/api/entry",
				new StringContent(
					JsonConvert.SerializeObject(
						new EntryInputDto
						{
							Title = "Test",
							DateAndTime = DateTime.UtcNow
						}), Encoding.UTF8, "application/json"));
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.StartsWith(@"D:\home\martin\dev\misc\SimpleDiary\Diary.Api", responseString);
		}
	}
}
