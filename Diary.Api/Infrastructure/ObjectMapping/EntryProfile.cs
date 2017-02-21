using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diary.Api.Dtos;

namespace Diary.Api.Infrastructure.ObjectMapping
{
	public class EntryProfile : Profile
	{
		public EntryProfile()
		{
			this.CreateMap<EntryInputDto, EntryOutputDto>();
		}
	}
}