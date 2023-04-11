using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.DAL
{
    public interface IDataHandler
    {
        public IEnumerable<Tour> GetTours();
        public void AddTour(Tour newTour);
        public void DeleteTour(Tour tour);
    }
}
