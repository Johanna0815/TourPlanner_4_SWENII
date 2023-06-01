using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_4_SWENII.Models
{
    public class Route
    {
        public string sessionID { get; set; }
        


        // string boundingBox
        public string ul_Longitude { get; set; }
        public string lr_Longitude { get; set; }
        public string ul_Latitude { get; set; }
        public string lr_Latitude { get; set; }



        public decimal distance { get; set; }
        public TimeSpan estimatedTime { get; set; }


    }
}
