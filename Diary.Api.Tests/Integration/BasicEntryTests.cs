using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Diary.Api.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Simpler.Net.Http;
using Xunit;

namespace Diary.Api.Tests.Integration
{
	public class BasicEntryTests : IntegrationTestBase {

		public BasicEntryTests(TestFixture fixture) : base(fixture) {}

		[Fact]
		public async Task CreateEntry() {
			// ARRANGE
			var input = new EntryInputDto
			{
				Title = "Test",
				Timestamps = new []
				{
					DateTime.UtcNow
				}
			};

			// ACT
			HttpResponseMessage response = await this._client.PostAsync(
				"/api/entry",
				new StringContent(
					JsonConvert.SerializeObject(input),
					Encoding.UTF8,
					CommonMimeTypes.Json)
			);
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var output = JsonConvert.DeserializeObject<EntryOutputDto>(responseString);

			// ASSERT
			output.Id.Should().BeGreaterThan(0);
			output.Title.ShouldBeEquivalentTo(input.Title);
			output.ShouldBeEquivalentTo(input.Timestamps);
		}
	}
}
