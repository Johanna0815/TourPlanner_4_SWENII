using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.DAL
{
    public class DataHandlerMemory : IDataHandler
    {
        private IEnumerable<Tour> tours = new List<Tour>();
        private DataHandlerEF dataHandlerEF = new();

        public DataHandlerMemory()
        {
            //tours.Add(new Tour() { name = "demo", ...});
        }
        public IEnumerable<Tour> GetTours()
        {
            tours = dataHandlerEF.GetTours();
            return tours;
        }
    }
}
