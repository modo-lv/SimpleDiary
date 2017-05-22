using System;

namespace Diary.Main.Domain.Entities {
	/// <summary>
	/// Main entity for a diary entry.
	/// </summary>
	public class Entry : EntityBase {
		/// <summary>
		/// UNIX timestamp for the date and time that this
		/// entry relates to (usually entry time).
		/// </summary>
		public virtual Int64 Timestamp { get; set; }

		/// <summary>
		/// Main type of the entry.
		/// </summary>
		public virtual EntryType Type { get; set; }

		/// <summary>
		/// Entry title.
		/// </summary>
		public virtual String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual String Description { get; set; }

		/// <summary>
		/// Has the entry been deleted?
		/// </summary>
		public virtual Boolean IsDeleted { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual EntryTextContent TextContent { get; set; }

		/// <summary>
		/// If 
		/// </summary>
		public virtual EntryFileContent FileContent { get; set; }
	}

	/// <summary>
	/// Entry type.
	/// </summary>
	public enum EntryType
	{
		/// <summary>
		/// Regular text entry.
		/// </summary>
		Text,

		/// <summary>
		/// This entry is a file upload.
		/// </summary>
		File
	}
}