using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.DAL
{
    public class DataHandlerEF : IDataHandler
    {
        private TourPlannerDBContext context = new(); //_dbContext

        public DataHandlerEF()
        {
            context.Database.EnsureCreated();

            //for testing only:
            /*
            context.Tours.AddRange(
                new Tour() { Name = "TourDeFrance", TourLogs = new List<TourLog>() 
                    { 
                        new TourLog() { Comment = "thsi is a test" },
                        new TourLog() { Comment = "this is also a test uwu" }
                    } 
                },
                //new Tour() { Name = "TourDeSwiss" },
                //new Tour() { Name = "TourDeAustria" },
                //new Tour() { Name = "DonauRadler" },
                new Tour() { Name = "HeimOderWoandersHin" });
            context.SaveChanges();*/
        }

        public Tour AddTour(Tour newTour)
        {
            Debug.WriteLine($"getting newTour with Id {newTour.Id}");
            context.Tours.Add(newTour);
            context.SaveChanges();
            Debug.WriteLine($"returning newTour with Id {newTour.Id}");
            return newTour;
        }

        public TourLog AddTourLog(TourLog newTourLog)
        {
            context.Tours.Where(t => t.Id == newTourLog.TourId).First().TourLogs.Add(newTourLog);
            context.SaveChanges();
            return newTourLog;
        }

        public void DeleteTour(Tour tour)
        {
            context.Tours.Remove(tour);
            context.SaveChanges();
        }

        public IEnumerable<TourLog> GetTourLogs(int tourId)
        {
            context.TourLogs.Load();
            return context.TourLogs;
        }

       public IEnumerable<Tour> GetTours()
        {
            context.Tours.Load();
            return context.Tours;
        }
    }
}
