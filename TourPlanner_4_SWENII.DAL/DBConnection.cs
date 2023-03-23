using System;
using System.Collections.Generic;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.DAL
{
    public class DBConnection : IDataAccess
    {

        private string connectionString;

        public DBConnection()
        {

            connectionString = "...";

            // establish connection with DB
        }
        public List<TourItem> GetItems()
        {

            // select SQL query
            return new List<TourItem>()
            {
                new TourItem() { Name = "TourDeFrance" },
                new TourItem() { Name = "TourDeSwiss" },
                new TourItem() { Name = "TourDeAustria" },
                new TourItem() { Name = "DonauRadler" },
                new TourItem() { Name = "HeimOderWoandersHin" }
                // throw new NotImplementedException();
            };
           // throw new NotImplementedException();
        }
    }
}
