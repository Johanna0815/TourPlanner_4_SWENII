using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.DAL
{
    public class MediaItemDAO
    {
        private IDataAccess dataAccess;

        public MediaItemDAO()
        {
                // check which datasoource to use

                dataAccess = new DBConnection();

            // in case it should grab filesystem
               //  dataAccess = new FileSystem()
        }


        public List<Tour> GetItems()
        {
            return dataAccess.GetItems();
        }

    }
}
