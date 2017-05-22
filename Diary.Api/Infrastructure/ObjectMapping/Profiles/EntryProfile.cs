using System;
using AutoMapper;
using Diary.Api.Dtos;
using Diary.Main.Core.Config;
using Diary.Main.Domain.Entities;
using Simpler.Net.Io;

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
			this.CreateMap<EntryDto, Entry>()
				//.ForMember(d => d.FileName, mo => mo.Ignore())
				.ReverseMap()
				//.ForMember(d => d.FileData, mo => mo.Ignore())
/*				.ForMember(d => d.FileUrl, mo => mo.ResolveUsing(
					e =>
					{
						if (String.IsNullOrEmpty(e.FileName))
							return null;
						var config = DiaryConfig.ReadConfig<MainConfig>();
						return $"~/{config.FileStorageFolder}/{e.FileName}";
					}))*/
					;
		}
	}
}