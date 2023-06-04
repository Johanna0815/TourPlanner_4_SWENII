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
        /*
        [ForeignKey("OrderId")]
        //virtual
        public long TourLogId { get; set; } = 0;*/

        public Tour() { }

        // constructor for the ToOrderTour
        public Tour(string tourOnDisplayOne, int id)
        {
            this.Name = tourOnDisplayOne;
            this.Id = id;
        }

        public int Id { get; set; } = 0;
        public string Name { get; set; } //  = string.Empty;

        public string Description { get; set; } = string.Empty;

        //rename?
        public string From { get; set; } = string.Empty; //cityname/etc or coordinates?
        //rename?
        public string To { get; set; } = string.Empty;

        public TransportType TransportType { get; set; } = TransportType.Pedestrian;

        public decimal Distance { get; set; }  = 0;

        // vorher war DateTime
        public TimeSpan EstimatedTime { get; set; } //= DateTime.UtcNow;

        // NotMapped Attribute, because we do not want to create a column in the db for that.
        [NotMapped]
        public double Childfriendlyness { get; set; } 


        //public AllowedMemberLocations AllowedMemberLocations { get; set; } = new AllowedMemberLocations();
        //abstract Lookup<Tour, TransportType> RestoreTransportType();


        /// <summary>
        /// route information (an image with the tour map)
        /// </summary>
        //public List<MediaTypeNames.Image> RouteInformation { get; set; }




        public void ProcessTour()
        {
            Console.WriteLine($"Tour {Name} processed."); // oder debug.print ?
        }

    }


}
