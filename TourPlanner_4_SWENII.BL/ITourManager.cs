using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.BL
{
    public interface ITourManager
    {
        IEnumerable<Tour> GetTours();
        IEnumerable<Tour> Search(string itemName, bool caseSensitive = false);

        Tour AddTour(string tourName, string description, string from, string to, TransportType transportType);
        void DeleteTour(Tour item);

        IEnumerable<TourLog> GetTourLogs(int tourId);
        TourLog AddTourLog(int TourId);
        void UpdateTourLog(TourLog tourLog);



        void DeleteTourLog(TourLog tourLog);

        void GenerateReport(Tour tour, string filename);

        void GetMap(Tour tour);

        void UpdateTour(Tour tour);
    }
}
