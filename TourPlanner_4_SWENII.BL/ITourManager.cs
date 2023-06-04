using System;
using System.Collections.Generic;
using System.IO;
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
        IEnumerable<Tour> Search(SearchParameters searchParameters);
        bool SearchTourProperties(Tour tour, string searchItem, bool caseSensitive);


        Tour AddTour(string tourName, string description, string from, string to, TransportType transportType);
        void DeleteTour(Tour item);

        IEnumerable<TourLog> GetTourLogs(int tourId);
        TourLog AddTourLog(int TourId);
        TourLog UpdateTourLog(TourLog tourLog);

        Tour ImportTourFrom(string filePath);
        void ExportTour(Tour tour);

        void DeleteTourLog(TourLog tourLog);

        void GenerateReport(Tour tour, string filename);

        void Summarize_TourLogs(string filename);

        TimeSpan AverageTime(ICollection<TourLog> tourlogs);

        double AverageRating(ICollection<TourLog> tourlogs);

      //  void GetMap(Tour tour);

        void UpdateTour(Tour tour);
        double CaculateChildFriendlyness(Tour tour);
        Task CallGetRouteAndGetImage(Tour tour);
    }
}
