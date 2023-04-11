using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Schema;
using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.Models
{
    public class Tour
    {
        public Tour() { }  

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        //rename?
        public string From { get; set; }
        //rename?
        public string To { get; set; }

        public TransportType TransportType { get; set; }

        public decimal Distance { get; set; }

        public DateTime EstimatedTime { get; set; }

        //public AllowedMemberLocations AllowedMemberLocations { get; set; } = new AllowedMemberLocations();
        //abstract Lookup<Tour, TransportType> RestoreTransportType();

        
        /// <summary>
        /// route information (an image with the tour map)
        /// </summary>
        //public List<MediaTypeNames.Image> RouteInformation { get; set; }


    }

  
}
