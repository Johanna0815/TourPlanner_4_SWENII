using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xaml.Schema;
using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.Models
{
    public class Tour
    {
        //[NotMapped]
        //virtual
        public ICollection<TourLog> TourLogs { get; set; } = new List<TourLog>();

        public Tour() { }

        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string From { get; set; } = string.Empty;

        public string To { get; set; } = string.Empty;

        public TransportType TransportType { get; set; } = TransportType.Pedestrian;

        public decimal Distance { get; set; }  = 0;

        public TimeSpan EstimatedTime { get; set; }

        // NotMapped Attribute, because we do not want to create a column in the db for that.
        [NotMapped]
        public double Childfriendlyness { get; set; } 

    }
}
