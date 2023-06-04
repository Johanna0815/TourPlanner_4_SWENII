using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_4_SWENII.Models
{
    public class SearchParameters
    {
        public string searchText { get; set; } = string.Empty;
        public bool caseSensitive { get; set; } = false;
        public bool searchInTourLogs { get; set; } = false;
    }
}
