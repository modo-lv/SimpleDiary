using System;
using AutoMapper;
using Simpler.Net.Time;

namespace Diary.Main.Infrastructure.ObjectMapping.Converters
{
	public class TimeToStampConverter : ITypeConverter<DateTime, UInt64>
	{
		public UInt64 Convert(DateTime source, UInt64 destination, ResolutionContext context)
		{
			return source.ToUniversalTime().ToUnixTimeStamp<UInt64>();
		}
	}
}