using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using Simpler.Net.Io;
using Simpler.Net.Io.Abstractions;
using Simpler.Net.Io.Abstractions.Implementations;

namespace Diary.Main.Core.Config
{
	/// <summary>
	/// Read/write configuration files
	/// </summary>
	public class DiaryConfig
	{
		/// <summary>
		/// Base directory for all application data: DB, config, etc.
		/// </summary>
		public static String BaseDir;

		private static readonly IFileSystem Io;

		/// <summary>
		/// Static constructor.
		/// </summary>
		static DiaryConfig() {
			Io = new DotNetFileSystem();

			BaseDir = Io.Directory.GetCurrentDirectory();
#if DEBUG
			BaseDir = @"c:\home\martin\dev\misc\SimpleDiary\Diary.Data";
#endif
		}


		/// <summary>
		/// Read configuration from file for a given config type.
		/// </summary>
		/// <param name="saveDefaultConfig">Write a new config file with default values if 
		/// it doesn't exist?</param>
		/// <typeparam name="TConfig">Type of configuration.</typeparam>
		/// <returns>Configuration object.</returns>
		public static TConfig ReadConfig<TConfig>(Boolean saveDefaultConfig = false)
		{
			String fileName = GetConfigFileName<TConfig>();
			String filePath = SimplerPath.Combine(BaseDir, fileName);

			TConfig config;

			if (Io.File.Exists(filePath))
			{
				String json = Io.File.ReadAllText(filePath);
				config = JsonConvert.DeserializeObject<TConfig>(json);
			}
			else
			{
				config = Activator.CreateInstance<TConfig>();
				if (saveDefaultConfig)
				{
					Io.CreateDirectoryForFile(filePath);
					Io.File.WriteAllText(filePath, JsonConvert.SerializeObject(config));
				}
			}

			return config;
		}

		/// <summary>
		/// Write configuration to its file.
		/// </summary>
		/// <typeparam name="TConfig">Configuration type.</typeparam>
		/// <param name="config">Configuration instance.</param>
		public static void WriteConfig<TConfig>(TConfig config)
		{
			String fileName = GetConfigFileName<TConfig>();
			String filePath = SimplerPath.Combine(BaseDir, fileName);
			String json = JsonConvert.SerializeObject(config);

			Io.File.WriteAllText(filePath, json);
		}

		/// <summary>
		/// Use configuration class name to determine configuration file name.
		/// </summary>
		/// <typeparam name="TConfig">Configuration class</typeparam>
		/// <returns>Name of the configuration file, including extension.</returns>
		protected static String GetConfigFileName<TConfig>()
		{
			// All other config
			String className = typeof(TConfig).Name;
			String fileName = Regex.Replace(className.ToLowerInvariant(), @"Config$", "", RegexOptions.IgnoreCase);
			if (fileName == className)
			{
				throw new Exception(
					$"Configuration classes must be named .\"..Config\", but \"{className}\" is not.");
			}

			return $"diary.config/{fileName}.json";
		}

	}
}