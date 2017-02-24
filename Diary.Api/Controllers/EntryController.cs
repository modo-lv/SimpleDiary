using System.Threading.Tasks;
using AutoMapper;
using Diary.Api.Dtos;
using Diary.Main.Core.Persistence;
using Diary.Main.Domain.Entities;
using Diary.Main.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Diary.Api.Controllers
{
	[Route("api/[controller]")]
	public class EntryController : Controller
	{
		private readonly DiaryDbContext _dbContext;
		private readonly IMapper _mapper;

		public EntryController(IHiService hiService, DiaryDbContext dbContext, IMapper mapper)
		{
			this._dbContext = dbContext;
			this._mapper = mapper;
		}

		[HttpPost]
		public async Task<EntryOutputDto> PostAsync([FromBody] EntryInputDto input) {
			var entry = this._mapper.Map<Entry>(input);

			this._dbContext.Add(entry);

			await this._dbContext.SaveChangesAsync();

			var output = this._mapper.Map<EntryOutputDto>(entry);

			return output;
		}
	}
}