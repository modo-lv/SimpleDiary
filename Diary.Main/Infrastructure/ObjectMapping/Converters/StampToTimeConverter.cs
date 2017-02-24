using System;
using AutoMapper;

namespace Diary.Main.Infrastructure.ObjectMapping.Converters
{
	public class StampToTimeConverter : ITypeConverter<UInt64, DateTime>
	{
		public DateTime Convert(UInt64 source, DateTime destination, ResolutionContext context)
			=> DateTime.UtcNow.AddSeconds(source);
	}
}