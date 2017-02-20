using System;
using Simpler.Net.FileSystem;

namespace Diary.Main.Core.Config
{
	/// <summary>
	/// Contains configuration for core functionality, mainly DB location.
	/// </summary>
	public class MainConfig
	{
		/// <summary>
		/// Full path (can be relative to the app folder) to where the
		/// </summary>
		public String DbFilePath { get; set; }

		/// <summary>
		/// Directory path where files (images, videos, etc.) are kept.
		/// </summary>
		public String FileStorageDir { get; set; }


		/// <summary>
		/// Constructor
		/// </summary>
		public MainConfig()
		{
			this.DbFilePath = SimplerPath.Combine(DiaryConfig.BaseDir, "diary.db");

			this.FileStorageDir = SimplerPath.Combine(DiaryConfig.BaseDir, "diary.files");
		}
	}
}