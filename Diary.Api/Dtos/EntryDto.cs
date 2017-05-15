using System;

namespace Diary.Api.Dtos
{
	public class EntryDto
	{
		public UInt32 Id { get; set; }

		public String Content { get; set; }

		public DateTime Timestamp { get; set; }
	}
}