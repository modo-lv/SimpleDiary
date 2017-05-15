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
		}
	}
}