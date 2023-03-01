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
        public List<MediaItem> GetItems()
        {

            // select SQL query
            return new List<MediaItem>()
            {
                new MediaItem() { Name = "TourDeFrance" },
                new MediaItem() { Name = "TourDeSwiss" },
                new MediaItem() { Name = "TourDeAustria" },
                new MediaItem() { Name = "DonauRadler" },
                new MediaItem() { Name = "HeimOderWoandersHin" }
                // throw new NotImplementedException();
            };
           // throw new NotImplementedException();
        }
    }
}
