using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Diary.Main.Infrastructure.ObjectMapping
{
	public static class ObjectMappingExtensionMethods
	{
		public static TDestination MapTo<TDestination>(this Object source, IMapper mapper)
			=> mapper.Map<TDestination>(source);
	}
}