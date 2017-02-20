using Newtonsoft.Json;
using Simpler.Net.FileSystem;
using System;
using System.IO;
using System.Text.RegularExpressions;

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
		public static readonly String BaseDir;

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

			if (File.Exists(filePath))
			{
				String json = File.ReadAllText(filePath);
				config = JsonConvert.DeserializeObject<TConfig>(json);
			}
			else
			{
				config = Activator.CreateInstance<TConfig>();
				if (saveDefaultConfig)
				{
					File.WriteAllText(filePath, JsonConvert.SerializeObject(config));
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

			File.WriteAllText(filePath, json);
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

			return $"{fileName}.json";
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static DiaryConfig() {
			BaseDir = Directory.GetCurrentDirectory();
#if DEBUG
			BaseDir = SimplerPath.Combine(Directory.GetParent(BaseDir).FullName, "Diary.Data");
#endif
		}
	}
}