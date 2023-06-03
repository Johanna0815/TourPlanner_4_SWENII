using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.CodeDom;
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
using Microsoft.VisualBasic;
using Org.BouncyCastle.Security;
using TourPlanner_4_SWENII.Utils.FileAndFolderHandling;
using TourPlanner_4_SWENII.Utils.Validating;
using System.Configuration;
using System.IO;

namespace TourPlanner_4_SWENII.BL
{
    // Execution of the Methodes in IMediaItemFactory

    public class TourManagerImpl : ITourManager
    {

        // get the logger from a factory so that the concrete implementation is hidden behind some interface
        //private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        private IDataHandler dal;
        private MapQuestImpl mapquest = new();

        public TourManagerImpl(IDataHandler dal)
        {
            //dal = new DataHandlerEF();//remove instantiation
            this.dal = dal;
        }

        public Tour AddTour(string tourName, string description, string from, string to, TransportType transportType)
        {
            //if (tourName == null || tourName == "")
            //{

            //    tourName = "New Tour";
            //}
            if ((ValidateUserInput.IsInputEmpty(tourName) || ValidateUserInput.IfInputIsTooLong(tourName)) ||
                 (ValidateUserInput.IfInputIsTooLong(description)))
            {

                //  throw new Exception();
                throw new ArgumentException();

            }



            Tour newTour = dal.AddTour(new Tour() { Name = tourName, Description = description, From = from, To = to, TransportType = transportType });


            // GetMap(newTour);

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
            dal.DeleteTourLog(tourLog);
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

            var savedTours = dal.GetTours();

            foreach (var tour in savedTours)
            {

                // Childunfriendly 
                /*var Distance = (double)tour.Distance; // to cast the decimal into the double. 

                var result = ((Distance) * (tour.EstimatedTime.TotalMinutes)) / 0.5;
                */
                tour.Childfriendlyness = CaculateChildFriendlyness(tour);




            }

            return savedTours;
            // return dal.GetTours();
            //return statement



        }


        public double CaculateChildFriendlyness(Tour tour)
        {

            var Distance = (double)tour.Distance; // to cast the decimal into the double. 

            var result = ((Distance) * (tour.EstimatedTime.TotalMinutes)) / 0.5;




            return result;
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
        public TourLog UpdateTourLog(TourLog tourLog)
        {
            return dal.UpdateTourLog(tourLog);
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






        //TODO
        //public  async Task<> GetMap(Tour tour, )
        //{
        //    mapquest.GetRoute(tour);
        //   // erneut speichern.
        //    dal.UpdateTour(tour);
        //    mapquest.GetImage();



        //}

        //public async Task FetchRoute()
        //{

        //    mapquest.GetRoute();


        //}





        public void UpdateTour(Tour selectedTour)

        {
            dal.UpdateTour(selectedTour);

        }

        public void ExportTour(Tour tour)
        {
            string dateString = $"{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}";

            //string folderName = "Exports";

            Debug.WriteLine("wir testen Den Export!!!!!!!!!!!!!!!!!!!!");
            //  ExportFile.JsonToFile(tour, $"{tour.Name}_{DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss")}.json");
            //   ExportFile.JsonToFile(tour, $"/exportFolder/{tour.Name}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.json");

            ExportImportManager.JsonToFile(tour, ConfigurationManager.AppSettings["ExportImportSubdir"], $"{tour.Name}_{dateString}.json"); //$"{folderName}"

            //  {DateTime.UtcNow.ToString("ddMMyyyy")}
        }

        public Tour ImportTourFrom(string filePath)
        {
            Tour tourToImport = ExportImportManager.ImportTourFromFile(filePath);
            //Id is reset to avoid potential clashing in the db with current tours
            tourToImport.Id = 0;
            return dal.AddTour(tourToImport);
        }


        public async Task CallGetRouteAndGetImage(Tour tour)
        {
            Route route = await mapquest.GetRoute(tour);

            // var route = task.Result;
            tour.Distance = route.distance; // ObjectRefernce not setted to an inst of an obkj
            tour.EstimatedTime = route.estimatedTime;
            UpdateTour(tour);

            //
            //  RaisePropertyChangedEvent(nameof(SelectedItem));

            Stream awaitStream = await mapquest.GetImage(route);

            await using var filestream = new FileStream($"{tour.Name}{tour.Id}.png", FileMode.Create, FileAccess.Write);
            awaitStream.CopyTo(filestream);

        }
    }
}

