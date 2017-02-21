using AutoMapper;
using Diary.Api.Dtos;
using Diary.Main.Core.Persistence;
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
		public EntryOutputDto Post([FromBody] EntryInputDto input)
		{
			var output = this._mapper.Map<EntryOutputDto>(input);

			return output;
		}
	}
}