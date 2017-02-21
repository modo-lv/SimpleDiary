using System;

namespace Diary.Api.Dtos
{
	public class EntryOutputDto : EntryInputDto
	{
		public UInt32 Id { get; set; }
	}
}