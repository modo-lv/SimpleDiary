using System;

namespace Diary.Api.Dtos {
	public abstract class EntityDtoBase
	{
		/// <inheritdoc cref="EntityBase.Id"/>
		public UInt32 Id { get; set; }
	}
}