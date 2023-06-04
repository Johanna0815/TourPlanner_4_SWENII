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
        //Tours
        IEnumerable<Tour> GetTours();
        Tour AddTour(string tourName, string description, string from, string to, TransportType transportType);
        void DeleteTour(Tour item);
        void UpdateTour(Tour tour);

        //TourLogs
        IEnumerable<TourLog> GetTourLogs(int tourId);
        TourLog AddTourLog(int TourId);
        TourLog UpdateTourLog(TourLog tourLog);
        void DeleteTourLog(TourLog tourLog);


        //Import/Export
        Tour ImportTourFrom(string filePath);
        void ExportTour(Tour tour);

        void GenerateReport(Tour tour, string filename);
        void Summarize_TourLogs(string filename);

        // MapQuest
        Task CallGetRouteAndGetImage(Tour tour);

        //Search
        IEnumerable<Tour> Search(SearchParameters searchParameters);
        bool SearchTourProperties(Tour tour, string searchItem, bool caseSensitive);

        // calculated properties
        TimeSpan AverageTime(ICollection<TourLog> tourlogs);
        double AverageRating(ICollection<TourLog> tourlogs);
        double CaculateChildFriendlyness(Tour tour);
    }
}
