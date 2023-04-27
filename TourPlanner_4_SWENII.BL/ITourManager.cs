using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.BL
{
    public interface ITourManager
    {
        IEnumerable<Tour> GetTours();
        IEnumerable<Tour> Search(string itemName, bool caseSensitive = false);

        Tour AddTour(string tourName);
        void DeleteTour(Tour item);

        IEnumerable<TourLog> GetTourLogs(int tourId);
        TourLog AddTourLog(int TourId);
        void UpdateTourLog(TourLog tourLog);
        void DeleteTourLog(TourLog tourLog);

        void GenerateReport(Tour tour, string filename);
    }
}
