using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.BL
{
    public  interface IMapQuest
    {

        public Task<Route> GetRoute(Tour tour);

        public Task<Stream> GetImage(Route route);



    }
}
