using System;
using AutoMapper;
using Simpler.Net.Time;

namespace Diary.Main.Infrastructure.ObjectMapping.Converters
{
	public class TimeToStampConverter : ITypeConverter<DateTime, Int64>
	{
		public Int64 Convert(DateTime source, Int64 destination, ResolutionContext context)
		{
			return source.ToUniversalTime().ToUnixTimeStamp<Int64>();
		}
	}
}