using Xunit;

namespace Diary.Api.Tests.Integration
{
	public class IntegrationTestBase : IClassFixture<TestFixture> {
		protected readonly TestFixture _fixture;

		public IntegrationTestBase(TestFixture fixture) {
			this._fixture = fixture;
		}
	}
}
