using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Diary.Main.Domain.Entities;

namespace Diary.Main.Services
{
    public interface IDiaryEntryService {
	    Task<Entry> GetEntryAsync(UInt32 id);
	    Task<IList<Entry>> GetEntriesAsync();
    }
}
