using Diary.Main.Domain.Models;

namespace Diary.Api.Dtos
{
    public class EntryDto : EntryModel
    {
			public new EntryFileContentDto FileContent { get; set; }
    }
}
