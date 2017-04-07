using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Api.Controllers;
using Diary.Api.Dtos;
using Diary.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Diary.Web.Controllers
{
	public class EntriesController : Controller
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly IMapper _mapper;

		private readonly EntryApiController _api;

		public EntriesController(IServiceProvider serviceProvider, IMapper mapper)
		{
			this._serviceProvider = serviceProvider;
			this._mapper = mapper;

			this._api = this._serviceProvider.GetService<EntryApiController>();
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var model = new EntriesIndexViewModel
			{
				Entries = await this._api.GetAsync()
			};

			return this.View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(UInt32 id)
		{
			await this._api.DeleteAsync(id);
			return this.RedirectToAction(nameof(this.Index));
		}

		[HttpGet]
		public async Task<IActionResult> New()
		{
			return this.View(new EntryDto());
		}

		[HttpPost]
		public async Task<IActionResult> New(EntryDto model)
		{
			model = await this._api.PostAsync(this._mapper.Map<EntryDto>(model));

			return this.View(model);
		}
	}
}