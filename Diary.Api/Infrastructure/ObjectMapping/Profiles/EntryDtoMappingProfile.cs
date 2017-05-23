using System;
using AutoMapper;
using Diary.Api.Dtos;
using Diary.Main.Core.Config;
using Diary.Main.Domain.Entities;
using Diary.Main.Domain.Models;

namespace Diary.Api.Infrastructure.ObjectMapping.Profiles
{
	/// <summary>
	/// AutoMapper profile for configuring mapping between <see cref="Entry"/>-related
	/// objects.
	/// </summary>
	public class EntryDtoMappingProfile : Profile
	{
		public EntryDtoMappingProfile()
		{
			this.CreateMap<Main.Domain.Models.EntryModel, EntryDto>()
				.ReverseMap();

			this.CreateMap<EntryFileContentModel, EntryFileContentDto>()
				.ReverseMap();

			this.CreateMap<EntryTextContentModel, EntryTextContentModel>()
				.ReverseMap();
		}
	}
}