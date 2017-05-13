using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Diary.Api.Dtos;
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
			this.CreateMap<EntryDto, Entry>()
				.ForMember(d => d.Timestamps, mo => mo.ResolveUsing(dto => new List<DateTime> {dto.Timestamp}));

			this.CreateMap<Entry, EntryDto>()
				.ForMember(d => d.Timestamp, mo => mo.ResolveUsing(entry => entry.Timestamps?.FirstOrDefault()));
		}
	}
}