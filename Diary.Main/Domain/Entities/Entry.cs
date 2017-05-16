using System;

namespace Diary.Main.Domain.Entities {
	/// <summary>
	/// Main entity for a diary entry.
	/// </summary>
	public class Entry : EntityBase {
		/// <summary>
		/// UNIX timestamp denoting when the entry was entered.
		/// </summary>
		public virtual Int64 Timestamp { get; set; }

		/// <summary>
		/// Text content of the entry.
		/// </summary>
		public virtual String Content { get; set; }

		/// <summary>
		/// Has the entry been deleted?
		/// </summary>
		public virtual Boolean IsDeleted { get; set; }

		/// <summary>
		/// Path to this entry's file.
		/// </summary>
		public virtual String FilePath { get; set; }
	}
}