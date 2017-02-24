using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Diary.Main.Domain.Entities;

namespace Diary.Main.Infrastructure.ObjectMapping.Converters
{
	public class EntryTimestampToDateTimeConverter : ITypeConverter<EntryTimestamp, DateTime>
	{
		public DateTime Convert(EntryTimestamp source, DateTime destination, ResolutionContext context)
			=> context.Mapper.Map<DateTime>(source.Timestamp);
	}
}