using System;
using AutoMapper;
using Diary.Api.Dtos;
using Diary.Main.Core.Config;
using Diary.Main.Domain.Entities;

namespace Diary.Api.Infrastructure.ObjectMapping.Profiles
{
	/// <summary>
	/// AutoMapper profile for configuring mapping between <see cref="Entry"/>-related
	/// objects.
	/// </summary>
	public class EntryProfile : Profile
	{
		public EntryProfile()
		{
			this.CreateMap<Entry, EntryDto>()
				.ReverseMap()
				.AfterMap(
					(dto, entry) =>
					{
						if (entry.Type != EntryType.Text)
							entry.TextContent = null;
						if (entry.Type != EntryType.File)
							entry.FileContent = null;
					});

			this.CreateMap<EntryFileContent, EntryFileContentDto>()
				.ForMember(d => d.FileUrl, mo => mo.ResolveUsing(
					fileEntry =>
					{
						if (String.IsNullOrEmpty(fileEntry?.FileName))
							return null;

						var config = DiaryConfig.ReadConfig<MainConfig>();
						return $"~/{config.FileStorageFolder}/{fileEntry.FileName}";
					}));

			this.CreateMap<EntryTextContent, EntryTextContentDto>()
				.ReverseMap();
		}
	}
}