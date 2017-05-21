using System;
using Diary.Main.Domain.Entities;

namespace Diary.Api.Dtos
{
	/// <summary>
	/// Entry data for displaying to client.
	/// </summary>
	public class EntryOutputDto : EntryDto
	{
		/// <inheritdoc cref="EntityBase.Id"/>
		public UInt32 Id { get; set; }

		/// <inheritdoc cref="Entry.FilePath"/>
		public String FileName { get; set; }
	}
}