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

    internal class TourManagerImpl : ITourManager
    {
        private IDataHandler dal = new DataHandlerMemory();//remove instantiation

        public TourManagerImpl() { //IDataHandler dal
            //this.dal = dal;
        }   

        public Tour AddTour(string tourName)
        {
            return dal.AddTour(new Tour() { Name=tourName });
        }

        public TourLog AddTourLog(int TourId)
        {
            return dal.AddTourLog(new TourLog() { TourId=TourId });
        }

        public void DeleteTour(Tour tour)
        {
            dal.DeleteTour(tour);
        }

        public void DeleteTourLog(TourLog tourLog)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TourLog> GetTourLogs(int tourId)
        {
            if (tourId == 0)
            {
                return new List<TourLog>();
            }
            return dal.GetTourLogs(tourId);
        }

        //private MediaItemDAO mediaItemDao = new MediaItemDAO();

        public IEnumerable<Tour> GetTours()
        {
            return dal.GetTours();
            //return statement
        }

        public IEnumerable<Tour> Search(string itemName, bool caseSensitive = false)
        {
            IEnumerable<Tour> items = GetTours();
            // throw new NotImplementedException();
            if (caseSensitive)
            {

                return items.Where(x => x.Name.Contains(itemName));

            }

            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }

        public void UpdateTourLog(TourLog tourLog)
        {
            throw new NotImplementedException();
        }
    }
}
