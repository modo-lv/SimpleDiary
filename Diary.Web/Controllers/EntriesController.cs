﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Api.Controllers;
using Diary.Api.Dtos;
using Diary.Main.Infrastructure.ObjectMapping;
using Diary.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Web.Controllers
{
	public class EntriesController : Controller
	{
		private readonly IMapper _mapper;

		private readonly EntriesApiController _api;

		public EntriesController(IMapper mapper, EntriesApiController api)
		{
			this._mapper = mapper;
			this._api = api;
		}

		public IActionResult Index() => this.RedirectToAction(nameof(this.List));


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
		/// Delete entry
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Delete(UInt32 id)
		{
			await this._api.DeleteAsync(id);
			return this.RedirectToAction(nameof(this.List));
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
			EntryDto result = (await this._api.SaveEntry(model)).MapTo<EntryDto>(this._mapper);

			return saveAndClose == null
				? (IActionResult) this.View("Edit", result)
				: this.RedirectToAction(nameof(this.List));
		}

		/// <summary>
		/// Open existing entry for editing.
		/// </summary>
		/// <param name="id">Entry ID.</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Edit(UInt32 id)
		{
			var entry = (await this._api.GetAsync(id)).MapTo<EntryDto>(this._mapper);

			this.ViewBag.Id = id;

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
		public async Task<IActionResult> Edit([FromRoute] UInt32 id, EntryDto entry, String saveAndClose)
		{
			EntryDto model = (await this._api.SaveEntry(entry, id)).MapTo<EntryDto>(this._mapper);

			return saveAndClose == null
				? (IActionResult)this.View("Edit", model)
				: this.RedirectToAction(nameof(this.List));
		}
	}
}