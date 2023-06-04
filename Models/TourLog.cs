using System;
using System.ComponentModel.DataAnnotations.Schema;
using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.Models
{
    public class TourLog
    {
        public int Id { get; set; } = 0;

        public DateTime TimeNow { get; set; } = DateTime.UtcNow;

        public string Comment { get; set; } = string.Empty;
        public Difficulty Difficulty { get; set; } = Difficulty.None;
        public TimeSpan TotalTime { get; set; } = TimeSpan.Zero;

        public Rating Rating { get; set; } = Rating.None;

        public int TourId { get; set; }

        public TourLog() { }

      
    }
}