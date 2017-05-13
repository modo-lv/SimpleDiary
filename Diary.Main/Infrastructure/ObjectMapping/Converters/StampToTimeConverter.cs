using System;
using AutoMapper;
using Simpler.Net.Time;

namespace Diary.Main.Infrastructure.ObjectMapping.Converters
{
	public class StampToTimeConverter : ITypeConverter<Int64, DateTime>
	{
		public DateTime Convert(Int64 source, DateTime destination, ResolutionContext context)
			=> SimplerTime.UnixEpochStart.AddSeconds(source);
	}
}