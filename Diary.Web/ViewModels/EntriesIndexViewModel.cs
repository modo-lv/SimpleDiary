using System.Collections.Generic;
using Diary.Main.Domain.Models;

namespace Diary.Web.ViewModels
{
    public class EntriesIndexViewModel
    {
	    public IList<EntryModel> Entries { get; set; }

	    public EntriesIndexViewModel()
	    {
		    this.Entries = new List<EntryModel>();
	    }
    }
}
