using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Diary.Main.Domain.Entities;

namespace Diary.Main.Infrastructure.ObjectMapping.Profiles
{
	public class EntityMappingProfile : Profile
	{
		public EntityMappingProfile()
		{
			this.CreateMap<EntityBase, EntityBase>()
				.ForMember(d => d.Id, mo => mo.Ignore());

			this.CreateMap<Entry, Entry>()
				.IncludeBase<EntityBase, EntityBase>();

			this.CreateMap<EntryTextContent, EntryTextContent>()
				.IncludeBase<EntityBase, EntityBase>();

			this.CreateMap<EntryFileContent, EntryFileContent>()
				.IncludeBase<EntityBase, EntityBase>();
		}
	}
}