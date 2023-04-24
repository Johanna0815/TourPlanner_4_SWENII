using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            IEnumerable<TourLog> tourLogs = dataHandlerEF.GetTourLogs(0);
            
            //not neccessary :)
            /*foreach(TourLog log in tourLogs)
            {
                //tours.Where(t => t.Id == log.TourId);
            }*/
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

        public IEnumerable<TourLog> GetTourLogs(int tourId)
        {
            if(tours.Where(t => t.Id == tourId).Count() > 0)
            {
                return tours.Where(t => t.Id == tourId).First().TourLogs;
            }
            else
            {
                Debug.WriteLine($"No Tour with id {tourId} found");
                return new List<TourLog>();
            }
        }

        /*
         //Add for getting from db, getTours only from memory
        public IEnumerable<Tour> LoadTours()
        {
            
            return tours;
        }*/
    }
}
