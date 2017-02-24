using System;
using Diary.Main.Core.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Diary.Api.Tests
{
	public class TestFixture : IDisposable
	{
		public DiaryDbContext DbContext;

		public TestFixture()
		{
		}

		public void Dispose()
		{
			this.DbContext = TestEnvironment.Services.GetService<DiaryDbContext>();
			if (this.DbContext == null)
				throw new NullReferenceException($"Service provider returned a null {nameof(DiaryDbContext)}.");

			// New DB for each test class.
			this.DbContext.Database.EnsureDeleted();
		}
	}
}