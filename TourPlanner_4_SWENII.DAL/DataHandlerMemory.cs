using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            //tours.Append(newTour);
            dataHandlerEF.AddTour(newTour);
        }

        public void EditTour(Tour tour)
        {
            //tours = tours.Where(t => t.Id == tour.Id).
        }

        public void DeleteTour(Tour tour)
        {
            //tours = tours.Where(t => t.Id != tour.Id).ToList();
            dataHandlerEF.DeleteTour(tour);
        }

        public IEnumerable<Tour> GetTours()
        {
            tours = dataHandlerEF.GetTours();   //(List<Tour>)
            return tours;
        }

        /*
         //Add for getting from db, getTours only from memory
        public IEnumerable<Tour> LoadTours()
        {
            
            return tours;
        }*/
    }
}
