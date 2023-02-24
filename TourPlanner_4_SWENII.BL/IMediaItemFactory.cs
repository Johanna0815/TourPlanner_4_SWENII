using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.BL
{
    public interface IMediaItemFactory
    {

        IEnumerable<MediaItem> GetItems();
        IEnumerable<MediaItem> Search(string itemName, bool caseSensitive = false);
    }
}
