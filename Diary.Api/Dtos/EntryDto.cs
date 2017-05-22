using System;
using Diary.Main.Domain.Entities;

namespace Diary.Api.Dtos
{
	/// <inheritdoc cref="Entry"/>
	public class EntryDto : EntityDtoBase
	{
		/// <inheritdoc cref="Entry.Timestamp"/>
		public DateTime Timestamp { get; set; }

		/// <inheritdoc cref="Entry.Type"/>
		public virtual EntryType Type { get; set; }

		/// <inheritdoc cref="Entry.Name"/>
		public virtual String Name { get; set; }

		/// <inheritdoc cref="Entry.Description"/>
		public virtual String Description { get; set; }

		/// <inheritdoc cref="Entry.FileContent"/>
		public EntryFileContentDto FileContent { get; set; }

		/// <inheritdoc cref="Entry.TextContent"/>
		public EntryTextContentDto TextContent { get; set; }

		public EntryDto()
		{
			this.FileContent = new EntryFileContentDto();
			this.TextContent = new EntryTextContentDto();
		}
	}
}