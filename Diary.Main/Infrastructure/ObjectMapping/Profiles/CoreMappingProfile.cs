using System;
using AutoMapper;
using Diary.Main.Domain.Entities;
using Diary.Main.Infrastructure.ObjectMapping.Converters;

namespace Diary.Main.Infrastructure.ObjectMapping.Profiles
{
	public class CoreMappingProfile : Profile
	{
		public CoreMappingProfile()
		{
			this.CreateMap<Int64, DateTime>().ConvertUsing<StampToTimeConverter>();
			this.CreateMap<DateTime, Int64>().ConvertUsing<TimeToStampConverter>();

			this.CreateMap<DateTime, EntryTimestamp>()
				.ForMember(d => d.Timestamp, mo => mo.MapFrom(s => s));

			this.CreateMap<EntryTimestamp, DateTime>()
				.ConstructUsing((et, ctx) => ctx.Mapper.Map<DateTime>(et.Timestamp).ToUniversalTime());
		}
	}
}