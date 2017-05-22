using System;
using Diary.Main.Domain.Entities;

namespace Diary.Api.Dtos {
	public abstract class EntityDtoBase
	{
		/// <inheritdoc cref="EntityBase.Id"/>
		public UInt32 Id { get; set; }
	}
}