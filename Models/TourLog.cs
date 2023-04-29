using System;
using System.ComponentModel.DataAnnotations.Schema;
using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.Models
{
    public class TourLog
    {
        public int Id { get; private set; } = 0;

        //[ForeignKey("TourId")]
        // public int TourId { get; set; }
        // date/ time 2 different or ? 
        //public DateTime due_date { get; set; }
        public DateTime TimeNow { get; set; } = DateTime.UtcNow;

        public string Comment { get; set; } = string.Empty;
        public Difficulty Difficulty { get; set; } = Difficulty.None;
        public double TotalTime { get; set; } = 0;

        // or should rating be decimal ? // or calc with the difficulty ? difficulty 5 is hard to do. 
        public int Rating { get; set; } = 0;

        public int TourId { get; set; }

        public TourLog() { }

      
    }
}