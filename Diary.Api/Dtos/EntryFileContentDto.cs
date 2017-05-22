using System;
using Diary.Main.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Diary.Api.Dtos
{
    public class EntryFileContentDto : EntryFileContent
    {
	    public IFormFile FileData { get; set; }

			/// <summary>
			/// URL to use in HTML to link to this file.
			/// </summary>
			public String FileUrl { get; set; }
    }
}
