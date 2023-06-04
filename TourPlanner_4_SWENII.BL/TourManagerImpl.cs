using iText.Kernel.Pdf;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
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
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Image;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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
            //string directorypath = "Reports/";
            //Directory.CreateDirectory(directorypath);
            //string  filePath = Path.Combine(directorypath, filename);

            string currentDirectory = Directory.GetCurrentDirectory();
            string parentDirectory = Directory.GetParent(currentDirectory)?.Parent?.FullName;
            string reportsDirectoryPath = Path.Combine(parentDirectory, "..", "..", "Reports");
            string filePath = Path.Combine(reportsDirectoryPath, filename);

            // Create the Reports directory if it doesn't exist
            Directory.CreateDirectory(reportsDirectoryPath);

            var writer = new PdfWriter(filePath);
            PdfDocument pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf);

            var titleFont = PdfFontFactory.CreateFont("Helvetica-Bold");
            var headingFont = PdfFontFactory.CreateFont("Helvetica");
            var textFont = PdfFontFactory.CreateFont("Helvetica");

            // Set styles
            var titleStyle = new Style().SetFont(titleFont).SetFontColor(ColorConstants.LIGHT_GRAY).SetFontSize(24);
            var headingStyle = new Style().SetFont(headingFont).SetFontColor(ColorConstants.BLACK).SetFontSize(16).SetBold();
            var infoStyle = new Style().SetFont(textFont).SetFontColor(ColorConstants.BLUE).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER);

            if (tour != null)
            {


                document.SetMargins(50, 50, 50, 50);



                // Add tour information and map image
                document.Add(new Paragraph("Tour Report").AddStyle(titleStyle).SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20)).SetTopMargin(20);
                document.Add(new Paragraph().Add(new Text("Tour Name: ").AddStyle(headingStyle)).Add(new Text(tour.Name).AddStyle(infoStyle)));
                document.Add(new Paragraph().Add(new Text("From: ").AddStyle(headingStyle)).Add(new Text(tour.From).AddStyle(infoStyle)));
                document.Add(new Paragraph().Add(new Text("To: ").AddStyle(headingStyle)).Add(new Text(tour.To).AddStyle(infoStyle)));
                document.Add(new Paragraph().Add(new Text("TransportType: ").AddStyle(headingStyle)).Add(new Text(tour.TransportType.ToString()).AddStyle(infoStyle)));
                document.Add(new Paragraph().Add(new Text("Distance: ").AddStyle(headingStyle)).Add(new Text(tour.Distance + " km").AddStyle(infoStyle)));
                document.Add(new Paragraph().Add(new Text("Estimated Time: ").AddStyle(headingStyle)).Add(new Text(tour.EstimatedTime.Hours + " hours").AddStyle(infoStyle)));



            }
            else
            {
                Debug.WriteLine("no tour was selected when creating a report");
                document.Add(new Paragraph($"No tour was selected"));
            }

            string imagePath = $"{tour.Name}{tour.Id}.png";

            float maxWidth = 200;
            float maxHeight = 200;

            if (File.Exists(imagePath))

            {
                Image tourImage = new Image(ImageDataFactory.Create(imagePath));
                tourImage.SetMaxWidth(maxWidth);
                tourImage.SetMaxHeight(maxHeight);
                tourImage.SetAutoScaleWidth(true);
                tourImage.SetAutoScaleHeight(true);

                document.Add(new Paragraph().Add(tourImage));
            }

            else
            {
                Debug.WriteLine("Image doesn't Exist");
            }


            if (tour.TourLogs != null && tour.TourLogs.Count > 0 )
            {
                document.Add(new Paragraph("Tour Logs").AddStyle(headingStyle));

                foreach(var  tourlog  in  tour.TourLogs)
                {

                    document.Add(new Paragraph().Add(new Text("TourLog_ID:").AddStyle(infoStyle)).Add(tourlog.Id.ToString()));
                    document.Add(new Paragraph().Add(new Text("Comment:").AddStyle(infoStyle)).Add(tourlog.Comment));
                    document.Add(new Paragraph().Add(new Text("Created DateTime:").AddStyle(infoStyle)).Add(tourlog.TimeNow.ToString()));
                    document.Add(new Paragraph().Add(new Text("Difficulty:").AddStyle(infoStyle)).Add(tourlog.Difficulty.ToString()));
                    document.Add(new Paragraph().Add(new Text("Rating:").AddStyle(infoStyle)).Add(tourlog.Rating.ToString()));


                    document.Add(new Paragraph("---------------------------------------------------------"));

                }


            }

            document.Add(new Paragraph("Tour Description").AddStyle(titleStyle).SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20));

            document.Add(new Paragraph().Add(new Text(tour.Description)).AddStyle(infoStyle));

            document.Close();
        }


        public void Summarize_TourLogs(string filename)

        {

            var tours = GetTours();



            string currentDirectory = Directory.GetCurrentDirectory();
            string parentDirectory = Directory.GetParent(currentDirectory)?.Parent?.FullName;
            string reportsDirectoryPath = Path.Combine(parentDirectory, "..", "..", "Reports");
            string filePath = Path.Combine(reportsDirectoryPath, filename);

            // Create the Reports directory if it doesn't exist
            Directory.CreateDirectory(reportsDirectoryPath);

            var writer = new PdfWriter(filePath);
            PdfDocument pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf);


            var titleFont = PdfFontFactory.CreateFont("Helvetica-Bold");
            var headingFont = PdfFontFactory.CreateFont("Helvetica");
            var textFont = PdfFontFactory.CreateFont("Helvetica");

            // Set styles
            var titleStyle = new Style().SetFont(titleFont).SetFontColor(ColorConstants.LIGHT_GRAY).SetFontSize(24);
            var headingStyle = new Style().SetFont(headingFont).SetFontColor(ColorConstants.BLACK).SetFontSize(16).SetBold();
            var infoStyle = new Style().SetFont(textFont).SetFontColor(ColorConstants.BLUE).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER);
                document.SetMargins(50, 50, 50, 50);

            foreach (var tour in tours)
            {

                if(tour != null)
                {
                        document.Add(new Paragraph().Add(new Text("Tour: ").AddStyle(headingStyle)).Add(new Text(tour.Name).AddStyle(infoStyle)));


                        TimeSpan AT = AverageTime(tour.TourLogs);

                        document.Add(new Paragraph().Add(new Text("Average Time: ").AddStyle(headingStyle)).Add(new Text(AT.Hours.ToString() + " hours").AddStyle(infoStyle)));
                        document.Add(new Paragraph().Add(new Text("Average Rating: ").AddStyle(headingStyle)).Add(new Text(AverageRating(tour.TourLogs).ToString()).AddStyle(infoStyle)));

                       // document.Add(new Paragraph().Add(new Text("Tour: ").AddStyle(headingStyle)).Add(new Text(tour.TourLogs.).AddStyle(infoStyle)));



               


                }

                else
                {
                    Debug.WriteLine("no tour was selected when creating a TourLogs report");
                    document.Add(new Paragraph($"No tour was selected"));
                }



                string imagePath = $"{tour.Name}{tour.Id}.png";

                float maxWidth = 200;
                float maxHeight = 200;

                if (File.Exists(imagePath))

                {
                    Image tourImage = new Image(ImageDataFactory.Create(imagePath));
                    tourImage.SetMaxWidth(maxWidth);
                    tourImage.SetMaxHeight(maxHeight);
                    tourImage.SetAutoScaleWidth(true);
                    tourImage.SetAutoScaleHeight(true);

                    document.Add(new Paragraph().Add(tourImage));

                    document.Add(new Paragraph("---------------------------------------------------------"));
                }

                else
                {
                    document.Add(new Paragraph("---------------------------------------------------------"));
                    Debug.WriteLine("Image doesn't Exist");
                }

            }



          



            /*if (tour != null)
            {




                // Add tour information and map image
                document.Add(new Paragraph("Tour Report").AddStyle(titleStyle).SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20)).SetTopMargin(20);
                document.Add(new Paragraph().Add(new Text("Tour Name: ").AddStyle(headingStyle)).Add(new Text(tour.Name).AddStyle(infoStyle)));
                document.Add(new Paragraph().Add(new Text("Average Time: ").AddStyle(headingStyle)).Add(new Text(AverageTime(tour.TourLogs).Hours.ToString() + "hours" ).AddStyle(infoStyle)));
                document.Add(new Paragraph().Add(new Text("Average Rating: ").AddStyle(headingStyle)).Add(new Text(AverageRating(tour.TourLogs).ToString()).AddStyle(infoStyle)));



            }*/

          






            document.Close();
        }


        public TimeSpan AverageTime(ICollection<TourLog> tourlogs)
        {

            TimeSpan averageTime = TimeSpan.Zero;

            int counter = 0;

            foreach (var tourlog in tourlogs)
            {
                if(tourlog.TotalTime != TimeSpan.Zero)
                {
                    counter++;

                    averageTime += tourlog.TotalTime;



                }
             

            }

            if (counter != 0)
            {
            averageTime = averageTime / counter;

            }

            return averageTime;
        }


        public double AverageRating(ICollection<TourLog> tourlogs)
        {
            double averageRating = 0;
            int counter = 0;

            foreach (var tourlog in tourlogs)
            {

                if(tourlog.Rating != Rating.None)
                {
                    counter++;

                    averageRating += ((double)tourlog.Rating);

                  
                }
                    

            }

            if (counter != 0)
            {
                
            averageRating = averageRating / counter;

            }


            return averageRating;
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







        /* public IEnumerable<Tour> Search(string itemName, bool caseSensitive = false)
         {
             IEnumerable<Tour> items = GetTours();
             // throw new NotImplementedException();
             if (caseSensitive)
             {

                 return items.Where(x => x.Name.Contains(itemName));

             }

             return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
         }*/

        /*public IEnumerable<Tour> Search(string searchItem, bool caseSensitive = false)
        {
            IEnumerable<Tour> items = GetTours();
            if (caseSensitive)
            {
                return items.Where(x => SearchProperty(x, searchItem, caseSensitive));
            }
            return items.Where(x => SearchProperty(x, searchItem.ToLower(), caseSensitive));
        }*/
        public IEnumerable<Tour> Search(SearchParameters searchParams)
        {
            IEnumerable<Tour> tours = GetTours();
            //(IEnumerable<Tour>)
            if (searchParams.searchInTourLogs)
            {
                List<Tour> foundTours = new List<Tour>();
                bool tourLogFound = false; 

                foreach (Tour tour in tours)
                {
                    //IEnumerable<TourLog> tourLogs = tour.TourLogs;
                    tourLogFound = false;

                    foreach (TourLog tourLog in tour.TourLogs)
                    {
                        if (!tourLogFound)
                        {
                            if (SearchTourLogProperties(tourLog, searchParams.searchText, searchParams.caseSensitive))
                            {
                                foundTours.Add(tour);
                                tourLogFound = true;
                            }
                        }
                    }
                }
                return foundTours;
            }
            else //only search on tours
            {
                IEnumerable<Tour> foundTours = new List<Tour>();
                foundTours = tours.Where(x => SearchTourProperties(x, searchParams.searchText, searchParams.caseSensitive));
                return foundTours;
            }
        }

        public bool SearchTourLogProperties(TourLog tourLog, string searchItem, bool caseSensitive)
        {
            if (caseSensitive)
            {
                if (tourLog.Comment.Contains(searchItem)
                        || (tourLog.Difficulty.ToString().Contains(searchItem))
                        || (tourLog.Rating.ToString().Contains(searchItem))
                        || (tourLog.TotalTime.ToString().Contains(searchItem))
                        || (tourLog.TimeNow.ToString().Contains(searchItem)))
                {
                    return true;
                }
            }
            else // if not casesensitive;
            {
                searchItem = searchItem.ToLower();
                if (tourLog.Comment.ToLower().Contains(searchItem)
                        || (tourLog.Difficulty.ToString().ToLower().Contains(searchItem))
                        || (tourLog.Rating.ToString().ToLower().Contains(searchItem))
                        || (tourLog.TotalTime.ToString().ToLower().Contains(searchItem))
                        || (tourLog.TimeNow.ToString().ToLower().Contains(searchItem)))
                {
                    return true;
                }
            }

            return false;
        }

        public bool SearchTourProperties(Tour tour, string searchItem, bool caseSensitive)
        {
            if (caseSensitive)
            {
                if (tour.Name.Contains(searchItem)
                        || (tour.From.Contains(searchItem))
                        || (tour.To.Contains(searchItem))
                        || (tour.Distance.ToString().Contains(searchItem))
                        || (tour.EstimatedTime.Hours.ToString().Contains(searchItem)))
                {
                    return true;
                }
            }

            else // if not casesensitive;
            {
                searchItem = searchItem.ToLower();
                if (tour.Name.ToLower().Contains(searchItem)
                       || (tour.From.ToLower().Contains(searchItem))
                       || (tour.To.ToLower().Contains(searchItem))
                       || (tour.Distance.ToString().Contains(searchItem))
                       || (tour.EstimatedTime.Hours.ToString().Contains(searchItem)))
                {
                    return true;
                }
            }

            return false;

        }

        /*public bool SearchProperty(Tour tour, string searchItem, bool caseSensitive)
        {
            if (caseSensitive)
            {
                if (tour.Name.Contains(searchItem)
                        || (tour.From.Contains(searchItem))
                        || (tour.To.Contains(searchItem))
                        || (tour.Distance.ToString().Contains(searchItem))
                        || (tour.EstimatedTime.Hours.ToString().Contains(searchItem)))
                {
                    return true;
                }



            }

            else // if not casesensitive;
            {
                searchItem = searchItem.ToLower();
                if (tour.Name.Contains(searchItem)
                       || (tour.From.ToLower().Contains(searchItem))
                       || (tour.To.ToLower().Contains(searchItem))
                       || (tour.Distance.ToString().Contains(searchItem))
                       || (tour.EstimatedTime.Hours.ToString().Contains(searchItem)))

                {

                    return true;
                }


            }

            return false;

        }*/


        //return tour.Name.Contains(searchItem)

        //    || (!caseSensitive && tour.From.ToLower().Contains(searchItem))
        //    || (!caseSensitive && tour.To.ToLower().Contains(searchItem))
        //    || (!caseSensitive && tour.Distance.ToString().Contains(searchItem))
        //    || (!caseSensitive && tour.EstimatedTime.Hours.ToString().Contains(searchItem));





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

