using System;
using Newtonsoft.Json;
using Simpler.Net.FileSystem;

namespace Diary.Main.Core.Config
{
	/// <summary>
	/// Contains configuration for core functionality, mainly DB location.
	/// </summary>
	public class MainConfig
	{
		/// <summary>
		/// Filename used for DB storage.
		/// </summary>
		public String DbFile { get; set; }

		/// <summary>
		/// Full path to the DB file.
		/// </summary>
		[JsonIgnore]
		public String DbFilePath => SimplerPath.Combine(DiaryConfig.BaseDir, this.DbFile);

		/// <summary>
		/// Folder where diary files (images, videos, etc.) are kept.
		/// </summary>
		public String FileStorageFolder { get; set; }

		/// <summary>
		/// Full path to the file storage folder.
		/// </summary>
		[JsonIgnore]
		public String FileStorageDir => SimplerPath.Combine(DiaryConfig.BaseDir, this.FileStorageFolder);


		/// <summary>
		/// Constructor
		/// </summary>
		public MainConfig()
		{
			this.DbFile = "diary.db";

			this.FileStorageFolder = "diary.files";
		}
	}
}