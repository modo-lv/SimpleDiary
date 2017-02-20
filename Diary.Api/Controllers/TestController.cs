using System;
using Diary.Main.Core.Persistence;
using Diary.Main.Domain.Entities;
using Diary.Main.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Diary.Api.Controllers
{
	[Route("api/[controller]")]
	public class TestController : Controller
	{
		private readonly IHiService _hiService;
		private readonly DiaryDbContext _dbContext;

		public TestController(IHiService hiService, DiaryDbContext dbContext)
		{
			this._hiService = hiService;
			this._dbContext = dbContext;
		}

		[HttpGet]
		public String Get()
		{
			var blog = new Entry {Title = "Test entry"};
			this._dbContext.Entries.Add(blog);
			this._dbContext.SaveChanges();

			var text = this._hiService.SayHi();
			return text;
		}
	}
}