using System;
using Diary.Main.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Diary.Api.Controllers {
	[Route("api/[controller]")]
	public class TestController : Controller {
		private readonly IHiService _hiService;

		public TestController(IHiService hiService) { this._hiService = hiService; }

		// GET: api/values
		[HttpGet]
		public String Get()
		{
			var text = this._hiService.SayHi();
			return text;
		}
	}
}