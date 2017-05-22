using System;
using Diary.Main.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Diary.Api.Dtos
{
	/// <summary>
	/// Entry data common to both input and output.
	/// </summary>
	public class EntryDto : EntityDtoBase
	{
		/// <inheritdoc cref="Entry.Timestamp"/>
		public DateTime Timestamp { get; set; }

		/// <inheritdoc cref="Entry.Description"/>
		public String Text { get; set; }
	}
}