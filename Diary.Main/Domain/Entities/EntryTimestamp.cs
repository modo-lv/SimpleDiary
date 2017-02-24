using System;

namespace Diary.Main.Domain.Entities
{
	public class EntryTimestamp : EntityBase
	{
		public virtual Entry Entry { get; set; }

		public virtual UInt64 Timestamp { get; set; }
	}
}