using AutoMapper;
using Diary.Main.Domain.Entities;
using Diary.Main.Domain.Models;

namespace Diary.Main.Infrastructure.ObjectMapping.Profiles
{
	public class EntityMappingProfile : Profile
    {
	    public EntityMappingProfile()
	    {
		    this.CreateMap<EntityBaseModel, EntityBase>()
			    .ForMember(d => d.Id, mo => mo.Ignore());
	    }
    }
}
