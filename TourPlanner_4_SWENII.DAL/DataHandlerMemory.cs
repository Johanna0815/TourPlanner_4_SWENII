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
        // private DataHandlerEF dataHandlerEF = new();

        public DataHandlerMemory()
        {
            // Debug.WriteLine($"data handler memory ctor called");
            // tours.Add(new Tour() { name = "demo", ...});
            // IEnumerable<TourLog> tourLogs =
            // dataHandlerEF.GetTourLogs(0);
            // tours = dataHandlerEF.GetTours();   //(List<Tour>)

            // not neccessary :)
            /*foreach(TourLog log in tourLogs)
            {
                //tours.Where(t => t.Id == log.TourId);
            }*/
        }

        public Tour AddTour(Tour newTour)
        {
            // newTour = 
            // dataHandlerEF.AddTour(newTour);   // add to db and return because of id
            tours.Append(newTour);                      // add to memory
            return newTour;                             // pass to bl->view
        }

        public Tour  UpdateTour(Tour tour)
        {
            tours = tours.Where(t => t.Id == tour.Id).ToList();

            return tour;
        }

        public void DeleteTour(Tour tour)
        {
            tours = tours.Where(t => t.Id != tour.Id).ToList();
            // dataHandlerEF.DeleteTour(tour);
        }

        public IEnumerable<Tour> GetTours()
        {
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

        public TourLog AddTourLog(TourLog newTourLog)
        {
            try
            {
                //newTourLog = 
                //dataHandlerEF.AddTourLog(newTourLog);          //deprecated

                tours.Where(t => t.Id == newTourLog.TourId).First().TourLogs.Add(newTourLog);

                return newTourLog;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Add TourLog failed because: ");
                Debug.WriteLine(ex.InnerException);
            }
            return new TourLog();
        }

        public TourLog UpdateTourLog(TourLog tourlog)
        {
            throw new NotImplementedException();
        }

        public void DeleteTourLog(TourLog tourLog)
        {
            throw new NotImplementedException();
        }
    }

    // Method to update an existing tour log for a tour
    //public void UpdateTourLog(string tourName, TourLog log)
    //{
    //    // Find the tour in the list
    //    var tour = tours.FirstOrDefault(t => t.Name == tourName);
    //    if (tour != null)
    //    {
    //        // Find the tour log in the tour
    //        var existingLog = tour.Logs.FirstOrDefault(l => l.DateTime == log.DateTime);
    //        if (existingLog != null)
    //        {
    //            // Update the tour log properties
    //            existingLog.Comment = log.Comment;
    //            existingLog.Difficulty = log.Difficulty;
    //            existingLog.TotalTime = log.TotalTime;
    //            existingLog.Rating = log.Rating;
    //        }
    //    }
}
