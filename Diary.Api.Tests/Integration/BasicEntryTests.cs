using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Diary.Api.Dtos;
using FluentAssertions;
using Newtonsoft.Json;
using Simpler.Net.Http;
using Simpler.Net.Time;
using Xunit;

namespace Diary.Api.Tests.Integration
{
	[Trait("Category", "Integration")]
	public class BasicEntryTests : IntegrationTestBase
	{
		public BasicEntryTests(TestFixture fixture) : base(fixture) { }

		[Fact(DisplayName = nameof(BasicEntryTests) + TestCore.TestNameSeparator + nameof(CreateEntry))]
		public async Task CreateEntry()
		{
			// ARRANGE
			var input = new EntryDto
			{
				Title = "Test",
				Timestamps = new[]
				{
					DateTime.UtcNow.DropMilliseconds()
				}
			};

			// ACT
			HttpResponseMessage response = await this._client.PostAsync(
				"/api/entries",
				new StringContent(
					JsonConvert.SerializeObject(input),
					Encoding.UTF8,
					CommonMimeTypes.Json)
			);
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var output = JsonConvert.DeserializeObject<EntryDto>(responseString);

			// ASSERT
			output.Id.Should().BeGreaterThan(0);
			output.Title.ShouldBeEquivalentTo(input.Title);
			output.Timestamps.ShouldBeEquivalentTo(input.Timestamps);
		}
	}
}