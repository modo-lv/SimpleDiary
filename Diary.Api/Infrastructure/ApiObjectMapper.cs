﻿using AutoMapper;
using AutoMapper.Configuration;
using Diary.Api.Infrastructure.ObjectMapping.Profiles;

namespace Diary.Api.Infrastructure
{
    public class ApiObjectMapper
    {
	    public static IMapperConfigurationExpression Configure(IMapperConfigurationExpression cfg)
	    {
				if (cfg == null)
					cfg = new MapperConfigurationExpression();

				cfg.AddProfile<EntryDtoMappingProfile>();

		    return cfg;
	    }
    }
};
