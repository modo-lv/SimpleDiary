using System;
using Microsoft.AspNetCore.Http;

namespace Diary.Api.Dtos
{
	public class EntryDto
	{
		public UInt32 Id { get; set; }

		public DateTime Timestamp { get; set; }

		public String Content { get; set; }

		public IFormFile FileData { get; set; }
	}
}