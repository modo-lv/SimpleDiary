using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Diary.Api.Dtos;
using Diary.Main.Domain.Entities;
using Newtonsoft.Json;
using Simpler.Net.Http;
using Simpler.Net.Time;
using Xunit;
using Shouldly;

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
				Timestamp = DateTime.UtcNow.DropMilliseconds()
			};

			// ACT
			HttpResponseMessage response = await this._fixture.Client.PostAsync(
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
			output.Id.ShouldBeGreaterThan<UInt32>(0);
			output.Title.ShouldBe(input.Title);
			output.Timestamp.ShouldBe(input.Timestamp);
		}


		[Fact(DisplayName = nameof(BasicEntryTests) + TestCore.TestNameSeparator + nameof(GetEntry))]
		public async Task GetEntry() {
			// ARRANGE
			var input = this._fixture.Mapper.Map<Entry>(
				new EntryDto
				{
					Title = "Test",
					Timestamp = DateTime.UtcNow.DropMilliseconds()
				});

			this._fixture.DbContext.Entries.Add(input);
			await this._fixture.DbContext.SaveChangesAsync();

			// ACT
			HttpResponseMessage response = await this._fixture.Client.GetAsync($"/api/entries/{input.Id}");
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var output = JsonConvert.DeserializeObject<EntryDto>(responseString);

			// ASSERT
			output.Id.ShouldBe(input.Id);
			output.Title.ShouldBe(input.Title);
			output.Timestamp.ShouldBe(SimplerTime.UnixEpochStart.AddSeconds(input.Timestamp));
		}
	}
}