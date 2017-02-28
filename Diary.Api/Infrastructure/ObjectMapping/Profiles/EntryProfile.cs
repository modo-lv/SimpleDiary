using AutoMapper;
using Diary.Api.Dtos;
using Diary.Main.Domain.Entities;

namespace Diary.Api.Infrastructure.ObjectMapping.Profiles
{
	/// <summary>
	/// AutoMapper profile for configuring mapping between <see cref="Entry"/>-related
	/// objects.
	/// </summary>
	public class EntryProfile : Profile
	{
		public EntryProfile()
		{
			this.CreateMap<EntryDto, Entry>();

			this.CreateMap<Entry, EntryDto>();
		}
	}
}