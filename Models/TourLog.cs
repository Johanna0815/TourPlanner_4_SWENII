using System;
using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.Models
{
    public class TourLog
    {
        public int Id { get; set; }
        // date/ time 2 different or ? 
        //public DateTime due_date { get; set; }
        public DateTime timeNow { get; set; }

        public string Comment { get; set; }
        public Difficulty Difficulty { get; set; }
        public decimal TotalTime { get; set; }

        // or should rating be decimal ? // or calc with the difficulty ? difficulty 5 is hard to do. 
        public int Rating { get; set; }
        public TourLog() { }


    }
}