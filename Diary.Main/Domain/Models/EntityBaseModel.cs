using System;
using Diary.Main.Domain.Entities;

namespace Diary.Main.Domain.Models {
	public abstract class EntityBaseModel
	{
		/// <inheritdoc cref="EntityBase.Id"/>
		public UInt32 Id { get; set; }
	}
}