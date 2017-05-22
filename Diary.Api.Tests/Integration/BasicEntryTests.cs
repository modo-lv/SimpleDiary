using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Diary.Api.Dtos;
using Diary.Main.Domain.Entities;
using Diary.Main.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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

		/// <summary>
		/// Test creating an entry.
		/// </summary>
		/// <returns></returns>
		[Fact(DisplayName = nameof(BasicEntryTests) + TestCore.TestNameSeparator + nameof(CreateEntry))]
		public async Task CreateEntry()
		{
			// ARRANGE
			var input = new FileEntryDto
			{
				Text = "Test",
				Timestamp = DateTime.Now.DropMilliseconds()
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
			output.Id.Should().BeGreaterThan(0);
			output.Timestamp.ShouldBeEquivalentTo(input.Timestamp);
		}

		/// <summary>
		/// Test updating an entry.
		/// </summary>
		/// <returns></returns>
		[Fact(DisplayName = nameof(BasicEntryTests) + TestCore.TestNameSeparator + nameof(UpdateEntry))]
		public async Task UpdateEntry()
		{
			// ARRANGE
			var service = this._fixture.Services.GetRequiredService<IDiaryEntryService>();
			Entry entry = await service.SaveEntryAsync(new Entry
			{
				Description = "Old value",
				Timestamp = 0
			});

			var newData = new EntryDto {
				Text = "New value",
				Timestamp = DateTime.Now.DropMilliseconds()
			};

			// ACT
			HttpResponseMessage response = await this._fixture.Client.PutAsync(
				$"/api/entries/{entry.Id}",
				new StringContent(
					JsonConvert.SerializeObject(newData),
					Encoding.UTF8,
					CommonMimeTypes.Json)
			);
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var output = JsonConvert.DeserializeObject<EntryDto>(responseString);

			// ASSERT
			output.Timestamp.ShouldBeEquivalentTo(newData.Timestamp);
			output.Text.ShouldBeEquivalentTo(newData.Text);
		}


		/// <summary>
		/// Test retrieving a single entry.
		/// </summary>
		/// <returns></returns>
		[Fact(DisplayName = nameof(BasicEntryTests) + TestCore.TestNameSeparator + nameof(GetEntry))]
		public async Task GetEntry() {
			// ARRANGE
			var input = this._fixture.Mapper.Map<Entry>(
				new EntryDto
				{
					Text = "Test",
					Timestamp = DateTime.Now.DropMilliseconds()
				});

			this._fixture.DbContext.Entries.Add(input);
			await this._fixture.DbContext.SaveChangesAsync();

			// ACT
			HttpResponseMessage response = await this._fixture.Client.GetAsync($"/api/entries/{input.Id}");
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var output = JsonConvert.DeserializeObject<EntryDto>(responseString);

			// ASSERT
			output.Id.ShouldBeEquivalentTo(input.Id);
			output.Timestamp.ShouldBeEquivalentTo(SimplerTime.UnixEpochStart.AddSeconds(input.Timestamp).ToLocalTime());
		}
	}
}