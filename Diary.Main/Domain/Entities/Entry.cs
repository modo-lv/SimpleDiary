using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.Main.Domain.Entities {
	public class Entry : EntityBase {
		public virtual String Title { get; set; }

		public virtual IList<EntryTimestamp> Timestamps { get; set; }
	}
}