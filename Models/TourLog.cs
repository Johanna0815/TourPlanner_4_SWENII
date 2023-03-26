using System;
using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.Models
{
    public class TourLog
    {

        public TourLog created_TourLogs { get; set; }

        public TourLog modified_TourLogs { get; set; }
        public TourLog deleted_TourLogs { get; set; }

        public TourLog updated_TourLogs { get; private set; }

        // date/ time 2 different or ? 
        public DateTime due_date { get; set; }
        public DateTime timeNOw { get; set; }


        public string Comment { get; set; }
        public  Difficulty Difficulty { get; set; }
        public decimal TotalTime { get; set; }

        // or should rating be decimal ? // or calc with the difficulty ? difficulty 5 is hard to do. 
        public int Rating { get; set; }




        public TourLog()
        {
            return created_TourLogs;
        }

      
    }
}