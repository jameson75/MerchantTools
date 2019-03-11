using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CipherPark.TriggerOrange.Web.Models
{
    public class SearchHotStartersViewModel 
    {
        public const int MaxPageSize = 100;
        public string SelectedRootCategory { get; set; }
        public string SelectedChildCategory { get; set; }        
        public List<SelectListItem> Sites { get; set; }  
        public string SelectedSite { get; set; }
        public string Keywords { get; set; }
        public List<SelectListItem> SortKeys { get; set; }
        public string SelectedSortKey { get; set; }
        public long UserActiveListId { get; set; }
    }    
}
