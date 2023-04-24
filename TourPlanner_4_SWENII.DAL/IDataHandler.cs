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
        public IEnumerable<Tour> GetTours();    //only for inMemory
        //public IEnumerable<Tour> LoadTours();   //for loading from db
        public Tour AddTour(Tour newTour);
        //public void EditTour(Tour tour);
        public void DeleteTour(Tour tour);

        public IEnumerable<TourLog> GetTourLogs(int tourId);
        public TourLog AddTourLog(TourLog newTourLog);
    }
}
