using AutoMapper;
using Diary.Main.Domain.Entities;
using Diary.Main.Domain.Models;
using Diary.Main.Infrastructure.ObjectMapping;
using Xunit;
using FluentAssertions;

namespace Diary.Main.Tests
{
	public class EntryMappingTests
	{
		[Fact]
		public void ModelToEntity_FileContent_IsNotInstantiatedNew()
		{
			// ARRANGE
			var source = new EntryModel
			{
				Type = EntryType.File,
				FileContent = new EntryFileContentModel
				{
					FileType = FileEntryType.Image,
				},
			};

			var entryFileContent = new EntryFileContent
			{
				FileType = FileEntryType.Generic,
			};
			var dest = new Entry
			{
				FileContent = entryFileContent,
			};

			// ACT
			var mapperConfig = new MapperConfiguration(
				cfg => {
					MainObjectMapper.Configure(cfg);
				});
			IMapper mapper = mapperConfig.CreateMapper();

			mapper.Map(source, dest);

			// ASSERT
			dest.FileContent.Should().BeSameAs(entryFileContent);
			dest.FileContent.FileType.ShouldBeEquivalentTo(source.FileContent.FileType);
		}
	}
}