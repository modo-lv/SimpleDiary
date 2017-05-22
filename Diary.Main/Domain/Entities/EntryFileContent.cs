using System;

namespace Diary.Main.Domain.Entities
{
	public class EntryFileContent : EntityBase
	{
		/// <summary>
		/// Name of the file as it is stored.
		/// </summary>
		public virtual String FileName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual FileEntryType FileType { get; set; }
	}

	public enum FileEntryType
	{
		Generic,

		Image
	}
}