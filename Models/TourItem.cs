using System;
using System.Collections.Generic;

namespace TourPlanner_4_SWENII.Models
{
    public class TourItem

    {

        /// <summary>
        /// might need for db / recration the url Creationtime ? 
        /// </summary>

        //public string Name { get; set; }
        //public string Url { get; set; }
        //public DateTime CreationTime { get; set; }


        public IList<Tour> ToursListItem { get; set; }

        public TourLog created_TourLogs { get; set; } 

        public TourLog deleted_TourLogs { get; set; }
    }
}
