using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models.HelperEnums;
//using System.Windows.Documents;
using TourPlanner_4_SWENII.DAL;
using TourPlanner_4_SWENII.Models;
using TransportType = TourPlanner_4_SWENII.Models.HelperEnums.TransportType;

namespace TourPlanner_4_SWENII.BL


{
    // Execution of the Methodes in IMediaItemFactory

    internal class TourManagerImpl : ITourManager
    {
        private IDataHandler dal = new DataHandlerMemory();//remove instantiation

        public TourManagerImpl() { //IDataHandler dal
            //this.dal = dal;
        }   

        public Tour AddTour(string tourName,string description, string from, string to , TransportType transportType,decimal distance  )
        {
            return dal.AddTour(new Tour() { Name=tourName,Description=description,From=from,To=to,TransportType = transportType, Distance = distance });
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

        public void GenerateReport(Tour tour, string filename)
        {
            var writer = new PdfWriter(filename);
            PdfDocument pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf);

            if(tour != null)
            {
                document.Add(new Paragraph($"{tour.Name}"));
            }
            else
            {
                Debug.WriteLine("no tour was selected when creating a report");
                document.Add(new Paragraph($"No tour was selected"));
            }
            document.Close();
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
