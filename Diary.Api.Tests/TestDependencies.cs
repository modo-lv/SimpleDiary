using Microsoft.Extensions.DependencyInjection;
using Simpler.Net.Io.Abstractions;
using Simpler.Net.Io.Abstractions.Implementations;

namespace Diary.Api.Tests
{
	public class TestDependencies {
		public static IServiceCollection AddTo(IServiceCollection services) {
			// Filesystem
			services.AddSingleton<IFileSystem, NullFileSystem>();
			services.AddSingleton<IStreamFactory, NullStreamFactory>();

			return services;
		}
	}

}
