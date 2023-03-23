using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.DAL;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.BL


{
    // Execution of the Methodes in IMediaItemFactory

    internal class TourItemManagerImpl : ITourItemManager
    {

        private MediaItemDAO mediaItemDao = new MediaItemDAO();

        public IEnumerable<TourItem> GetItems()
        {
           return mediaItemDao.GetItems();
            //return statement
        }

        public IEnumerable<TourItem> Search(string itemName, bool caseSensitive = false)
        {
            IEnumerable<TourItem> items = GetItems();
            // throw new NotImplementedException();
            if (caseSensitive)
            {

                return items.Where(x => x.Name.Contains(itemName));

            }

            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }


    }
}
