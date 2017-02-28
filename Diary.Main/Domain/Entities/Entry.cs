using System;
using System.Collections.Generic;

namespace Diary.Main.Domain.Entities {
	/// <summary>
	/// Main entity for a diary entry.
	/// </summary>
	public class Entry : EntityBase {
		/// <summary>
		/// Title of the entry.
		/// </summary>
		public virtual String Title { get; set; }

		/// <summary>
		/// Content of the entry.
		/// </summary>
		public virtual String Content { get; set; }

		/// <summary>
		/// Timestamps that this entity describes / relates to.
		/// </summary>
		public virtual IList<EntryTimestamp> Timestamps { get; set; }
	}
}