/*using System;
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

        public DataHandlerMemory()
        {

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
}*/
