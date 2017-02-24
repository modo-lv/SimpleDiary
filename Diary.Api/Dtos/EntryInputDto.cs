using System;
using System.Collections.Generic;

namespace Diary.Api.Dtos
{
	public class EntryInputDto
	{
		public String Title { get; set; }

		public IList<DateTime> Timestamps { get; set; }
	}
}