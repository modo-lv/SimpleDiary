using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Api.Dtos;

namespace Diary.Web.ViewModels
{
    public class EntriesIndexViewModel
    {
	    public IList<EntryOutputDto> Entries { get; set; }

	    public EntriesIndexViewModel()
	    {
		    this.Entries = new List<EntryOutputDto>();
	    }
    }
}
