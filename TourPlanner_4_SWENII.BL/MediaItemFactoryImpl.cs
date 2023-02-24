using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.BL
{
    internal class MediaItemFactoryImpl : IMediaItemFactory
    {
        public IEnumerable<MediaItem> GetItems()
        {
            return new List<MediaItem>()
            {
                new MediaItem() { Name = "Item1" },
                new MediaItem() { Name = "Item2" },
                new MediaItem() { Name = "Item3" },
                new MediaItem() { Name = "Item4" },
                new MediaItem() { Name = "Item5" }
                // throw new NotImplementedException();
            };
        }

        public IEnumerable<MediaItem> Search(string itemName, bool caseSensitive = false)
        {
            throw new NotImplementedException();
        }
    }
}
