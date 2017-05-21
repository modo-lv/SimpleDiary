using Microsoft.AspNetCore.Http;

namespace Diary.Api.Dtos
{
	/// <summary>
	/// Client-submitted entry data (for creating and updating).
	/// </summary>
	public class EntryInputDto : EntryDto
	{
		public IFormFile FileData { get; set; }
	}
}