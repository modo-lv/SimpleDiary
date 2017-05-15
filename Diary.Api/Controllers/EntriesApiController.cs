using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Api.Dtos;
using Diary.Main.Core.Persistence;
using Diary.Main.Domain.Entities;
using Diary.Main.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Diary.Api.Controllers
{
	[Route("api/entries")]
	public class EntriesApiController : Controller
	{
		private readonly DiaryDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IDiaryEntryService _diaryEntryService;

		public EntriesApiController(DiaryDbContext dbContext, IMapper mapper, IDiaryEntryService diaryEntryService)
		{
			this._dbContext = dbContext;
			this._mapper = mapper;
			this._diaryEntryService = diaryEntryService;
		}

		/// <summary>
		/// Retrieve a list of all entries.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IList<EntryDto>> GetAllAsync()
		{
			var entries = await this._diaryEntryService.GetEntriesAsync();

			return this._mapper.Map<List<EntryDto>>(entries);
		}

		/// <summary>
		/// Create a new diary entry.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<EntryDto> PostAsync([FromBody] EntryDto input) {
			var entry = this._mapper.Map<Entry>(input);

			this._dbContext.Add(entry);

			await this._dbContext.SaveChangesAsync();

			var output = this._mapper.Map<EntryDto>(entry);

			return output;
		}

		/// <summary>
		/// Update an existing entry.
		/// </summary>
		/// <param name="id">Entry ID.</param>
		/// <param name="input">Updated entry data.</param>
		/// <returns>Updated entry data after saving.</returns>
		[HttpPut("{id}")]
		public async Task<EntryDto> PutAsync([FromRoute] UInt32 id, [FromBody] EntryDto input)
		{
			Entry entry = await this._diaryEntryService.GetEntryAsync(id);
			this._mapper.Map(input, entry);
			this._dbContext.Update(entry);
			await this._dbContext.SaveChangesAsync();
			var output = this._mapper.Map<EntryDto>(entry);
			return output;
		}

		/// <summary>
		/// Retrieve a single diary entry.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<EntryDto> GetAsync([FromRoute] UInt32 id)
		{
			Entry entry = await this._diaryEntryService.GetEntryAsync(id);
			var dto = this._mapper.Map<EntryDto>(entry);
			return dto;
		}

		/// <summary>
		/// Delete a given entry.
		/// </summary>
		/// <param name="id">Entry ID.</param>
		[HttpDelete("{id}")]
		public async Task DeleteAsync([FromRoute] UInt32 id)
		{
			Entry entry = await this._dbContext.Entries.SingleAsync(e => e.Id == id);
			this._dbContext.Entries.Remove(entry);
			await this._dbContext.SaveChangesAsync();
		}
	}
}