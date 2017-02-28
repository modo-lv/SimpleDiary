using System.Threading.Tasks;
using AutoMapper;
using Diary.Api.Dtos;
using Diary.Main.Core.Persistence;
using Diary.Main.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Diary.Api.Controllers
{
	[Route("api/entry")]
	public class EntryApiController : Controller
	{
		private readonly DiaryDbContext _dbContext;
		private readonly IMapper _mapper;

		public EntryApiController(DiaryDbContext dbContext, IMapper mapper)
		{
			this._dbContext = dbContext;
			this._mapper = mapper;
		}

		[HttpPost]
		public async Task<EntryDto> PostAsync([FromBody] EntryDto input) {
			var entry = this._mapper.Map<Entry>(input);

			this._dbContext.Add(entry);

			await this._dbContext.SaveChangesAsync();

			var output = this._mapper.Map<EntryDto>(entry);

			return output;
		}
	}
}