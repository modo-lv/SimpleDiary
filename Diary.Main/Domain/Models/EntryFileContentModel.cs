using System;
using Diary.Main.Domain.Entities;

namespace Diary.Main.Domain.Models
{
    public class EntryFileContentModel : EntryFileContent
    {
			/// <summary>
			/// URL to use in HTML to link to this file.
			/// </summary>
			public String FileUrl { get; set; }
    }
}
