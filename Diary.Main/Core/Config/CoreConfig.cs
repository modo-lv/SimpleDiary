using System;
using System.IO;
using Simpler.Net.FileSystem;

namespace Diary.Main.Core.Config
{
	/// <summary>
	/// Contains configuration for core functionality, mainly DB location.
	/// </summary>
	public class CoreConfig
	{
		/// <summary>
		/// Full path (can be relative to the app folder) to where the
		/// </summary>
		public String DbFilePath { get; set; }

		public CoreConfig()
		{
			this.DbFilePath = SimplerPath.Combine(MainConfig.BaseDir, "diary.db");
		}
	}
}