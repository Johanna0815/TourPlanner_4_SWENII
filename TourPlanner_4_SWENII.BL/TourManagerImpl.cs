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
using System.Windows.Input;
using log4net;

namespace TourPlanner_4_SWENII.BL
{
    // Execution of the Methodes in IMediaItemFactory

    public class TourManagerImpl : ITourManager
    {

        // get the logger from a factory so that the concrete implementation is hidden behind some interface
        //private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        private IDataHandler dal;
        private MapQuest mapquest = new();

        public TourManagerImpl(IDataHandler dal)
        {
            //dal = new DataHandlerEF();//remove instantiation
            this.dal = dal;
        }

        public Tour AddTour(string tourName, string description, string from, string to, TransportType transportType, decimal distance)
        {
            if (tourName == null || tourName == "")
            {
                tourName = "New Tour";
            }
            Tour newTour = dal.AddTour(new Tour() { Name = tourName, Description = description, From = from, To = to, TransportType = transportType, Distance = distance });
            GetMap(newTour);
            return newTour;

        }

        public TourLog AddTourLog(int TourId)
        {
            return dal.AddTourLog(new TourLog() { TourId = TourId });
            //delete !!
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

            if (tour != null)
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


        //  string tourName
        public void UpdateTourLog(TourLog tourLog)
        {
            // Find the tour in the list
            //var tour = tours.FirstOrDefault(t => t.Name == tourName);
            //if (tour != null)
            //{
            //    // Find the tour log in the tour
            //    var existingLog = tour.Logs.FirstOrDefault(l => l.DateTime == log.DateTime);
            //    if (existingLog != null)
            //    {
            //        // Update the tour log properties
            //        existingLog.Comment = log.Comment;
            //        existingLog.Difficulty = log.Difficulty;
            //        existingLog.TotalTime = log.TotalTime;
            //        existingLog.Rating = log.Rating;



            throw new NotImplementedException();


        }


        // Method to update an existing tour
        //public void UpdateTour(Tour tour)
        //{
        //    // Find the tour in the list
        //    var existingTour = tours.FirstOrDefault(t => t.Name == tour.Name);
        //    if (existingTour != null)
        //    {
        //        PopulateTourData(tour);

        //        // Update the tour properties
        //        existingTour.Description = tour.Description;
        //        existingTour.From = tour.From;
        //        existingTour.To = tour.To;
        //        existingTour.TransportType = tour.TransportType;
        //        existingTour.Distance = tour.Distance;
        //        existingTour.EstimatedTime = tour.EstimatedTime;
        //        existingTour.RouteInformation = tour.RouteInformation;
        //    }
        //}







        public void GetMap(Tour tour)
        {
            mapquest.GetMapQuest(tour);

        }
    }
}
            
