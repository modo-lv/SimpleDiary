using System;

namespace Diary.Main.Domain.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class EntryTextContent : EntityBase
	{
		public virtual String Content { get; set; }

		public virtual TextContentFormat Format { get; set; }
	}

	public enum TextContentFormat
	{
		Plain,

		Markdown,

		Html
	}
}