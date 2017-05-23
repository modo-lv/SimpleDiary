using System;
using Diary.Main.Domain.Entities;

namespace Diary.Main.Domain.Models
{
	/// <inheritdoc cref="Entry"/>
	public class EntryModel : EntityBaseModel
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
		public EntryFileContentModel FileContent { get; set; }

		/// <inheritdoc cref="Entry.TextContent"/>
		public EntryTextContentModel TextContent { get; set; }

		public EntryModel()
		{
			this.FileContent = new EntryFileContentModel
			{
				FileType = FileEntryType.Image
			};
			this.TextContent = new EntryTextContentModel();
		}
	}
}