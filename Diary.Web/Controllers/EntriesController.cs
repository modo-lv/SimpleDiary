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

		private readonly EntriesApiController _api;

		public EntriesController(IServiceProvider serviceProvider, IMapper mapper)
		{
			this._serviceProvider = serviceProvider;
			this._mapper = mapper;

			this._api = this._serviceProvider.GetService<EntriesApiController>();
		}

		public async Task<IActionResult> Index(UInt32? id)
		{
			return await (id.HasValue ? this.Display(id.Value) : this.List());
		}


		/// <summary>
		/// List all entries.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> List()
		{
			var model = new EntriesIndexViewModel
			{
				Entries = await this._api.GetAllAsync()
			};

			return this.View("List", model);
		}

		/// <summary>
		/// View a single entry.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Display(UInt32 id)
		{
			EntryDto model = await this._api.GetAsync(id);
			return this.View("Display", model);
		}


		/// <summary>
		/// Delete entry
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Delete(UInt32 id)
		{
			await this._api.DeleteAsync(id);
			return this.RedirectToAction(nameof(this.Display));
		}

		/// <summary>
		/// Open editor for a new entry.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult New()
		{
			var model = new EntryDto {Timestamp = DateTime.Now};
			return this.View("Edit", model);
		}

		/// <summary>
		/// Create a new entry.
		/// </summary>
		/// <param name="model">New entry data.</param>
		/// <param name="saveAndClose">Non-<c>null</c> if user wants to close editor after saving.</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> New(EntryDto model, String saveAndClose)
		{
			model = await this._api.PostAsync(this._mapper.Map<EntryDto>(model));

			return saveAndClose == null
				? (IActionResult) this.View("Edit", model)
				: this.RedirectToAction(nameof(this.Display));
		}

		/// <summary>
		/// Open existing entry for editing.
		/// </summary>
		/// <param name="id">Entry ID.</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Edit(UInt32 id)
		{
			EntryDto entry = await this._api.GetAsync(id);

			return this.View("Edit", entry);
		}

		/// <summary>
		/// Update an existing entry.
		/// </summary>
		/// <param name="id">Entry ID.</param>
		/// <param name="entry">New entry data.</param>
		/// <param name="saveAndClose">Non-<c>null</c> if user wants to close editor.</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Edit(UInt32 id, EntryDto entry, String saveAndClose)
		{
			var model = await this._api.PutAsync(id, this._mapper.Map<EntryDto>(entry));

			return saveAndClose == null
				? (IActionResult)this.View("Edit", model)
				: this.RedirectToAction(nameof(this.Display));
		}
	}
}