using System;
using System.IO;

namespace Diary.Main.Services
{
	public class HiService : IHiService
	{
		public String SayHi()
			=> Directory.GetCurrentDirectory();
	}
}