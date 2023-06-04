using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.DAL
{
    public interface IDataHandler
    {
        // Tours
        public IEnumerable<Tour> GetTours();
        public Tour AddTour(Tour newTour);
        public Tour UpdateTour(Tour newTour);
        public void DeleteTour(Tour tour);

        // TourLogs
        public IEnumerable<TourLog> GetTourLogs(int tourId);
        public TourLog AddTourLog(TourLog newTourLog);
        public TourLog UpdateTourLog(TourLog tourlog);
        public void DeleteTourLog(TourLog tourLog);
    }
}
