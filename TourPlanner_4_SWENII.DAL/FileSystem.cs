using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.DAL
{
    internal class FileSystem : IDataAccess
    {

        private string filePath;

        public FileSystem()
        {

            // GET filePath from config file.
            filePath = "...";
        }

        public List<TourItem> GetItems()
        {

            // get media Items from file system
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
