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
            Debug.WriteLine($"data handler memory ctor called");
            //tours.Add(new Tour() { name = "demo", ...});
            IEnumerable<TourLog> tourLogs = dataHandlerEF.GetTourLogs(0);
            tours = dataHandlerEF.GetTours();   //(List<Tour>)

            //not neccessary :)
            /*foreach(TourLog log in tourLogs)
            {
                //tours.Where(t => t.Id == log.TourId);
            }*/
        }

        public Tour AddTour(Tour newTour)
        {
            newTour = dataHandlerEF.AddTour(newTour);   // add to db and return because of id
            tours.Append(newTour);                      // add to memory
            return newTour;                             // pass to bl->view
        }

        public void EditTour(Tour tour)
        {
            //tours = tours.Where(t => t.Id == tour.Id).
        }

        public void DeleteTour(Tour tour)
        {
            tours = tours.Where(t => t.Id != tour.Id).ToList();
            dataHandlerEF.DeleteTour(tour);
        }

        public IEnumerable<Tour> GetTours()
        {
            return tours;
        }

        public IEnumerable<TourLog> GetTourLogs(int tourId)
        {
            if(tours.Where(t => t.Id == tourId).Count() > 0)
            {
                Debug.WriteLine($"GetTourLogs:");
                Debug.WriteLine($"Tours with right Id found: {tours.Where(t => t.Id == tourId).Count()} (should be 1)");
                Debug.WriteLine($"TourLogs in that Tour found: {tours.Where(t => t.Id == tourId).First().TourLogs.Count()}");

                return tours.Where(t => t.Id == tourId).First().TourLogs;
            }
            else
            {
                Debug.WriteLine($"No Tour with id {tourId} found");
                return new List<TourLog>();
            }
        }

        public TourLog AddTourLog(TourLog newTourLog)
        {
            try
            {
                /*
                Debug.WriteLine($"AddTourLog:");
                Debug.WriteLine($"adding Log for Tours with Id: {newTourLog.TourId}");
                Debug.WriteLine($"Tours with right Id found: {tours.Where(t => t.Id == newTourLog.TourId).Count()} (should be 1)");
                Debug.WriteLine($"TourLogs inMemory counted: {tours.Where(t => t.Id == newTourLog.TourId).First().TourLogs.Count()}");
                Debug.WriteLine($"");
                Debug.WriteLine($"newTourLog Id before adding in EF: {newTourLog.Id}");*/

                newTourLog = dataHandlerEF.AddTourLog(newTourLog);          //get from EF because of Id
                //auto saves tourlog in memory
                //  -> but WHY though ???!!?!
                
                /*Debug.WriteLine($"newTourLog Id after adding in EF: {newTourLog.Id}");
                Debug.WriteLine($"Tours with right Id found: {tours.Where(t => t.Id == newTourLog.TourId).Count()} (should be 1)");
                Debug.WriteLine($"TourLogs inMemory counted: {tours.Where(t => t.Id == newTourLog.TourId).First().TourLogs.Count()}");
                Debug.WriteLine($"");
                Debug.WriteLine($"adding in mem ->");*/

                //not neccessary - but why !??!?!?
                //tours.Where(t => t.Id == newTourLog.TourId).First().TourLogs.Add(newTourLog); //is it this?

                /*Debug.WriteLine($"newTourLog Id after adding in Memory: {newTourLog.Id}");
                Debug.WriteLine($"Tours with right Id found: {tours.Where(t => t.Id == newTourLog.TourId).Count()} (should be 1)");
                Debug.WriteLine($"TourLogs inMemory counted: {tours.Where(t => t.Id == newTourLog.TourId).First().TourLogs.Count()} (should be one more than before)");
                */
                return newTourLog;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Add TourLog failed because: ");
                Debug.WriteLine(ex.InnerException);
            }
            return new TourLog();
        }

        /*
         //Add for getting from db, getTours only from memory
        public IEnumerable<Tour> LoadTours()
        {
            
            return tours;
        }*/
    }
}
