using System;
using System.Collections.Generic;

namespace Diary.Api.Dtos
{
	public class EntryDto
	{
		public UInt32 Id { get; set; }

		public String Title { get; set; }

		public String Content { get; set; }

		public IList<DateTime> Timestamps { get; set; }
	}
}