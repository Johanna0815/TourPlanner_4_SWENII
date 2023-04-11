using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.DAL
{
    public class DataHandlerMemory : IDataHandler
    {
        private IEnumerable<Tour> tours = new List<Tour>();
        private DataHandlerEF dataHandlerEF = new();

        public DataHandlerMemory()
        {
            //tours.Add(new Tour() { name = "demo", ...});
        }

        public void AddTour(Tour newTour)
        {
            //todo: save in memory
            dataHandlerEF.AddTour(newTour);
        }

        public void DeleteTour(Tour tour)
        {
            //todo: save in memory
            dataHandlerEF.DeleteTour(tour);
        }

        public IEnumerable<Tour> GetTours()
        {
            tours = dataHandlerEF.GetTours();
            return tours;
        }
    }
}
