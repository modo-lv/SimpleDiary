using System;

namespace Diary.Main.Domain.Entities
{
	public abstract class EntityBase
	{
		/// <summary>
		/// Entity ID.
		/// </summary>
		public virtual UInt32 Id { get; set; }
	}
}