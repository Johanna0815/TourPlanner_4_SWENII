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

        public List<MediaItem> GetItems()
        {

            // get media Items from file system
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
