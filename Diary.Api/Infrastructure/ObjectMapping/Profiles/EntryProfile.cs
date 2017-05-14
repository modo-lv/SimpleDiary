using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Diary.Api.Dtos;
using Diary.Main.Domain.Entities;
using Simpler.Net;
using Simpler.Net.Time;

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
				.ForMember(
					d => d.Timestamps,
					mo => mo.ResolveUsing(
						(dto, entry) =>
						{
							if (dto.Timestamp == null)
								return entry.Timestamps;

							entry.Timestamps.Clear();
							entry.Timestamps.Add(
								new EntryTimestamp {Timestamp = dto.Timestamp.Value.ToUnixTimeStamp<Int64>()});
							return entry.Timestamps;
						}));

			this.CreateMap<Entry, EntryDto>()
				.ForMember(
					d => d.Timestamp,
					mo => mo.ResolveUsing(
						entry => entry.Timestamps?.FirstOrDefault()
							.IfNotNull<EntryTimestamp, DateTime?>(t => SimplerTime.UnixEpochStart.AddSeconds(t.Timestamp))));
		}
	}
}