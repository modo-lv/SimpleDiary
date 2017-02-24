using System;
using AutoMapper;
using Simpler.Net.Time;

namespace Diary.Main.Infrastructure.ObjectMapping.Converters
{
	public class StampToTimeConverter : ITypeConverter<UInt64, DateTime>
	{
		public DateTime Convert(UInt64 source, DateTime destination, ResolutionContext context)
			=> SimplerTime.UnixEpochStart.AddSeconds(source);
	}
}