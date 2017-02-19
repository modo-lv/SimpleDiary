using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.Main.Domain.Entities
{
    public abstract class EntityBase
    {
			public virtual UInt32 Id { get; set; }
    }
}
