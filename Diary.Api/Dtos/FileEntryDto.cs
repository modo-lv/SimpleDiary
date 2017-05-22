using System;
using Diary.Main.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Diary.Api.Dtos
{
    public class FileEntryDto : EntryDto
    {
	    public IFormFile FileData { get; set; }

	    /// <inheritdoc cref="Entry.FileName"/>
	    public String FileName { get; set; }

	    public String FileUrl { get; set; }
    }
}
