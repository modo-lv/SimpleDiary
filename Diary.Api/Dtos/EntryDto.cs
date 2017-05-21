using System;

namespace Diary.Api.Dtos
{
	/// <summary>
	/// Entry data common to both input and output.
	/// </summary>
	public abstract class EntryDto
	{
		public DateTime Timestamp { get; set; }

		public String Content { get; set; }
	}
}