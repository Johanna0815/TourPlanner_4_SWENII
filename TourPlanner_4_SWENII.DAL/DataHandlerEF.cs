using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.DAL
{
    public class DataHandlerEF : IDataHandler
    {
        private TourPlannerDBContext context = new();

        public DataHandlerEF()
        {
            context.Database.EnsureCreated();

            //for testing only:
            context.Tours.AddRange(
                new Tour() { Name = "TourDeFrance" },
                new Tour() { Name = "TourDeSwiss" },
                new Tour() { Name = "TourDeAustria" },
                new Tour() { Name = "DonauRadler" },
                new Tour() { Name = "HeimOderWoandersHin" });
            context.SaveChanges();
        }

		public IEnumerable<Tour> GetTours()
        {
            context.Tours.Load();
            return context.Tours;
        }

        /*
        public void AddTour(Tour newTour)
        {
            context.Tours.Add(newTour);
            context.SaveChanges();
        }*/
    }
}
