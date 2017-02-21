using System;

namespace Diary.Api.Dtos
{
	public class EntryInputDto
	{
		public String Title { get; set; }

		public DateTime DateAndTime { get; set; }
	}
}