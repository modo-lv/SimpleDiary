using System;
using AutoMapper;
using Diary.Main.Core.Config;
using Diary.Main.Domain.Entities;
using Diary.Main.Domain.Models;

namespace Diary.Main.Infrastructure.ObjectMapping.Profiles
{
	public class EntryMappingProfile : Profile
	{
		public EntryMappingProfile()
		{
			// Main entry
			this.CreateMap<EntryModel, Entry>()
				.IncludeBase<EntityBaseModel, EntityBase>()
				.ForMember(d => d.TextContent, mo => mo.PreCondition(m => m.Type == EntryType.Text))
				.ForMember(d => d.FileContent, mo => mo.PreCondition(m => m.Type == EntryType.File))

				.ReverseMap();

			// Text content
			this.CreateMap<EntryTextContentModel, EntryTextContent>()
				.IncludeBase<EntityBaseModel, EntityBase>()

				.ReverseMap();

			// File content
			this.CreateMap<EntryFileContentModel, EntryFileContent>()
				.IncludeBase<EntityBaseModel, EntityBase>()
				// Changing a file's name involves more than simple field update,
				// so we avoid overwriting it implicitly and let the relevant services
				// process it explicitly.
				.ForMember(d => d.FileName, mo => mo.Ignore())

				.ReverseMap()
				.ForMember(
					d => d.FileUrl,
					mo => mo.ResolveUsing(
						fileEntry =>
						{
							if (String.IsNullOrEmpty(fileEntry?.FileName))
								return null;

							var config = DiaryConfig.ReadConfig<MainConfig>();
							return $"~/{config.FileStorageFolder}/{fileEntry.FileName}";
						}));
		}
	}
}