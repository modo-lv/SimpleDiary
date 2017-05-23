using AutoMapper;
using AutoMapper.Configuration;
using Diary.Main.Infrastructure.ObjectMapping.Profiles;

namespace Diary.Main.Infrastructure.ObjectMapping
{
	public class MainObjectMapper
	{
		public static IMapperConfigurationExpression Configure(IMapperConfigurationExpression cfg)
		{
			if (cfg == null)
				cfg = new MapperConfigurationExpression();

			cfg.AddProfile<CoreMappingProfile>();
			cfg.AddProfile<EntityMappingProfile>();
			cfg.AddProfile<EntryMappingProfile>();

			return cfg;
		}
	}
}