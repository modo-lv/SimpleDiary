using Diary.Main.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Diary.Api.Dtos
{
	public class EntryFileContentDto : EntryFileContentModel
	{
		public IFormFile FileData { get; set; }
	}
}