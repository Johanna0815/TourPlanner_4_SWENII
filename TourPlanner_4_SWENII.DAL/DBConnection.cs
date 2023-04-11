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
        public List<Tour> GetItems()
        {
            Console.WriteLine($"returned items");

            // select SQL query
            return new List<Tour>()
            {
                new Tour() { Name = "TourDeFrance" },
                new Tour() { Name = "TourDeSwiss" },
                new Tour() { Name = "TourDeAustria" },
                new Tour() { Name = "DonauRadler" },
                new Tour() { Name = "HeimOderWoandersHin" }
                // throw new NotImplementedException();
            };
           // throw new NotImplementedException();
        }
    }
}
