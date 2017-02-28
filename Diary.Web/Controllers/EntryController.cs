using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Api.Controllers;
using Diary.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Diary.Web.Controllers
{
	public class EntryController : Controller
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly IMapper _mapper;

		public EntryController(IServiceProvider serviceProvider, IMapper mapper)
		{
			this._serviceProvider = serviceProvider;
			this._mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> New()
		{
			return this.View("Edit", new EntryDto());
		}

		[HttpPost]
		public async Task<IActionResult> New(EntryDto model)
		{
			var ctrl = this._serviceProvider.GetService<EntryApiController>();

			model = await ctrl.PostAsync(this._mapper.Map<EntryDto>(model));

			return this.View("Edit", model);
		}
	}
}