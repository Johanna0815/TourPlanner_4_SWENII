using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.DAL;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.BL
{
    internal class MediaItemFactoryImpl : IMediaItemFactory
    {

        private MediaItemDAO mediaItemDao = new MediaItemDAO();

        public IEnumerable<MediaItem> GetItems()
        {
           return mediaItemDao.GetItems();
            //return statement
        }

        public IEnumerable<MediaItem> Search(string itemName, bool caseSensitive = false)
        {
            IEnumerable<MediaItem> items = GetItems();
            // throw new NotImplementedException();
            if (caseSensitive)
            {

                return items.Where(x => x.Name.Contains(itemName));

            }

            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }


    }
}
