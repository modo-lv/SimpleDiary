﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Diary.Main.Domain.Entities;
using Diary.Main.Domain.Models;

namespace Diary.Main.Services
{
    public interface IDiaryEntryService {

	    Task<Entry> GetEntryAsync(UInt32 id);

	    Task<IList<Entry>> GetEntriesAsync();

	    /// <summary>
	    /// Create/update a diary entry.
	    /// </summary>
	    /// <param name="entry">Entry data to save.</param>
	    /// <param name="id">ID of the entry to update. If 0, a new entry will be created.</param>
	    /// <param name="newFileContents"></param>
	    /// <returns>New/updated entry.</returns>
	    Task<Entry> SaveEntryAsync(EntryModel entry, UInt32 id = 0, Stream newFileContents = null);
    }
}
