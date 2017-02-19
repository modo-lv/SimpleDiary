using System;
using System.Collections.Generic;
using System.Text;
using Diary.Main.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Diary.Main.Infrastructure {
	public class MainStartup {
		public static void Initialize()
		{
			var context = new DiaryContext();
			context.Database.Migrate();
		}
	}
}