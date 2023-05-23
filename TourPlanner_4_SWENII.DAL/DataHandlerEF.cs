using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            context.Tours.Add(newTour);
            context.SaveChanges();
            return newTour;
        }

        public TourLog AddTourLog(TourLog newTourLog)
        {
            //change to use tourlogs directly??
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
            if (context.TourLogs.Where(t => t.TourId == tourId).Count() > 0)
            {
                return context.TourLogs.Where(t => t.TourId == tourId);
            }
            else
            {
                Debug.WriteLine($"No Tour with id {tourId} found");
                return new List<TourLog>();
            }
        }

       public IEnumerable<Tour> GetTours()
        {
            return context.Tours;
        }

        public Tour UpdateTour(Tour newTour)
        
        {
            context.Tours.Update(newTour);
            context.SaveChanges();
            return newTour;
        }




        // might using this as unique Feature ?
        public void ToOrderTour()
        {

            Queue<Tour> orderTours = new Queue<Tour>();

            foreach (Tour o in RecieveOrdersFromBranch1()) // will return an order Array. 
            {
                // add each Tour to the queue.
                orderTours.Enqueue(o);
            }

            foreach (Tour o in RecieveOrdersFromBranch2())
            {
                orderTours.Enqueue(o);

            }

            while (orderTours.Count > 0)
            {

                // remove the order At the fornt odf the queuee
                // and store it in a var called currentTour.
                Tour currentTour = orderTours.Dequeue();
                // 
                currentTour.ProcessTour();
            }



        }




        static Tour[] RecieveOrdersFromBranch1()
        {
            Tour[] orderedTour = new Tour[]
            {
                new Tour("firstTour",1),
                new Tour("fifthTour",3),
                new Tour("thirdTour",2)
            };
            return orderedTour;


        }


        static Tour[] RecieveOrdersFromBranch2()
        {
            Tour[] orderedTour = new Tour[]
            {
                new Tour("fourthTour",4),
                new Tour("secondTour",5),
                new Tour("sixthTour",7)
            };
            return orderedTour;


        }





    }
}
