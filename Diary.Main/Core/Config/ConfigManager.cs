using Newtonsoft.Json;
using Simpler.Net.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Diary.Main.Core.Config {
	/// <summary>
	/// Read/write configuration files
	/// </summary>
	public class ConfigManager {
		/// <summary>
		/// Read configuration from file for a given config type.
		/// </summary>
		/// <param name="configDir">Directory where to look for the configuration file.</param>
		/// <typeparam name="TConfig">Type of configuration.</typeparam>
		/// <returns>Configuration object.</returns>
		public virtual TConfig ReadConfig<TConfig>(String configDir = ".") {
			String fileName = this.GetConfigFileName<TConfig>();
			String filePath = SimplerPath.Combine(configDir, fileName);

			String json = File.ReadAllText(filePath);

			var config = JsonConvert.DeserializeObject<TConfig>(json);

			return config;
		}


		public virtual void WriteConfig<TConfig>() {
			
		}


		/// <summary>
		/// Use configuration class name to determine configuration file name.
		/// </summary>
		/// <typeparam name="TConfig">Configuration class</typeparam>
		/// <returns>Name of the configuration file, including extension.</returns>
		protected virtual String GetConfigFileName<TConfig>() {
			String className = typeof(TConfig).Name;
			String fileName = Regex.Replace(className.ToLowerInvariant(), @"Config$", "", RegexOptions.IgnoreCase);
			if (fileName == className) {
				throw new Exception($"Configuration classes must be named .\"..Config\", but \"{className}\" is not.");
			}

			return $"{fileName}.json";
		}
	}
}