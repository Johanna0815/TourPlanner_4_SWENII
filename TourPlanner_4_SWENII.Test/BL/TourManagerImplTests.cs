
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;

using System.Configuration;

using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.DAL;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Models.HelperEnums;
using TourPlanner_4_SWENII.Utils.FileAndFolderHandling;
using TourPlanner_4_SWENII.Views;

namespace TourPlanner_4_SWENII.Test.BL
{
    public class TourManagerImplTests
    {
        Mock<IDataHandler> dal;
        ITourManager tourManager;
        List<Tour> tours;

        Mock<IMapQuest> mapquest;

        [SetUp]
        public void SetUp()
        {
            dal = new Mock<IDataHandler>();
            mapquest = new Mock<IMapQuest>();

            tourManager = new TourManagerImpl(dal.Object);
            tours = new List<Tour>() {
                new Tour(){
                    Id=1,
                    Name="TestTour1",
                    Description="this is a test",
                    From="Wien",
                    To="Graz",
                    TransportType=Models.HelperEnums.TransportType.Bicycle,
                    Distance=200m,
                    EstimatedTime=TimeSpan.FromHours(2), //new TimeSpan(0, 30, 0),
                    TourLogs=new List<TourLog>()
                    {
                        new TourLog
                        {
                            Id = 1,

                        }
                    }
                },
                new Tour(){ Id=2, Name="tour" },
                new Tour(){ Id=3, Name="Tour 3"}
            };
        }

        [Test]
        public void GetTours_ShouldGetToursFromDAL()
        {
            // Arrange
            dal.Setup(x => x.GetTours()).Returns(tours);

            // Act
            IEnumerable<Tour> receivedTours = tourManager.GetTours();

            // Assert
            Assert.That(receivedTours, Is.EquivalentTo(tours));
        }

        [Test]
        public void AddTour_ShouldReturnNewTourFromDAL()
        {
            // Arrange
            dal.Setup(x => x.AddTour(It.IsAny<Tour>())).Returns(tours[0]);

            // Act
            Tour receivedTour = tourManager.AddTour(tours[0].Name, tours[0].Description, tours[0].From, tours[0].To, tours[0].TransportType);

            // Assert
            Assert.That(receivedTour, Is.EqualTo(tours[0]));

            // TODO: seperate mapQuest from addTour?
            //      validate dal.addTour parameters
        }

        [Test]
        public void UpdateTour_ShouldCallDALUpdateTour()
        {
            // Arrange
            dal.Setup(x => x.UpdateTour(It.IsAny<Tour>()));//.Returns((Tour mytour) => { return mytour; });
            // Act
            tourManager.UpdateTour(tours[0]);
            // Assert
            dal.Verify(x => x.UpdateTour(It.IsAny<Tour>()), Times.Once);
        }

        [Test]
        public void DeleteTour_TourShouldCallDALDeleteTour()
        {
            //Arrange 
            dal.Setup(x => x.DeleteTour(It.IsAny<Tour>()));

            //Act
            tourManager.DeleteTour(tours.First());

            //Assert
            dal.Verify(x => x.DeleteTour(It.IsAny<Tour>()), Times.Once);

        }
        [Test]
        public void GetTours_ToursShouldCallGetTours()

        {
            //Arrange 
            dal.Setup(x => x.GetTours());

            //Act
            tourManager.GetTours();

            //Assert
            dal.Verify(x => x.GetTours(), Times.Once);

        }

        [Test]
        public void Search_ToursShouldBeSearched()

        {
            //Arrange
            dal.Setup(x => x.GetTours()).Returns(tours);
            //tourManager.SearchProperty(tours[0], "tour", false);

            //Act 
            IEnumerable<Tour> receivedTours = tourManager.Search("Tour", true);


            //Assert
            Assert.That(receivedTours.Count(), Is.EqualTo(tours.Count - 1));


        }

        [Test]
        public void  SearchProperty_ShouldReturnTrueIfCasesensitive()
        
        {
            //Act

           var result =  tourManager.SearchTourProperties(tours[0], "Tour", true);

            Assert.That(result, Is.True);
        }

        [Test]

        public void SearchProperty_ShouldReturnFalseIfIsCasesensitive()

        {
            //Act

            var result = tourManager.SearchTourProperties(tours[1], "Tour", true);

            Assert.That(result, Is.False);
        }

        [Test]

        public void GetTourLogs_ShouldCallDALGetTourLogs()
        {


            //Arrange 
            dal.Setup(x => x.GetTourLogs(It.IsAny<int>())).Returns(tours[0].TourLogs);

            //Act
            tourManager.GetTourLogs(1);

            //Assert
            dal.Verify(x => x.GetTourLogs(It.IsAny<int>()), Times.Once);

        }

        [Test]

        public void GenerateReport_ForSelectedToursReportCouldBeGenerated()

        {
            //Arrange
            var filename = "TestReport.pdf";

            string currentDirectory = Directory.GetCurrentDirectory();
            string parentDirectory = Directory.GetParent(currentDirectory)?.Parent?.FullName;
            string reportsDirectoryPath = Path.Combine(parentDirectory, "..", "..", "Reports");
            string filePath = Path.Combine(reportsDirectoryPath, filename);


            //Act

            tourManager.GenerateReport(tours[0], filename);

            //assert

            Assert.IsTrue(File.Exists(filePath));

            // To Delete generated Report
            File.Delete(filename);

        }

        [Test]

        public void AddTourLogs_ShouldReturnNewTourFromDAL()
        {
            //Arrange
            dal.Setup(x => x.AddTourLog(It.IsAny<TourLog>())).Returns(tours[0].TourLogs.First());

            //Act
            TourLog receivedTourlogs = tourManager.AddTourLog(tours[0].Id);


            //Assert
            Assert.That(receivedTourlogs, Is.EqualTo(tours[0].TourLogs.First()));
            Assert.That(receivedTourlogs.Id, Is.EqualTo(tours[0].TourLogs.First().Id));

        }

        [Test]

        public void DeleteTourLog_TourShouldCallDALDeleteTourLog()
        {
            //Arrange 
            dal.Setup(x => x.DeleteTourLog(It.IsAny<TourLog>()));

            //Act
            tourManager.DeleteTourLog(tours[0].TourLogs.First());

            //Assert
            dal.Verify(x => x.DeleteTourLog(It.IsAny<TourLog>()), Times.Once);
            var result = tourManager.GetTourLogs(tours[0].Id);
            Assert.That(result.Contains(tours[0].TourLogs.First()), Is.False);

        }

        [Test]
        public void UpdateTourlog_ShouldCallDALUpdateTourLog()
        {
            // Arrange
            var existingTourLog = new TourLog { Id = 1, Comment = "Old Comment" };
            var updatedTourLog = new TourLog { Id = 1, Comment = "New Comment" };
            dal.Setup(x => x.UpdateTourLog(It.IsAny<TourLog>())).Returns(updatedTourLog);

            // Act
            var result = tourManager.UpdateTourLog(existingTourLog);

            // Assert
            dal.Verify(x => x.UpdateTourLog(existingTourLog), Times.Once);
            Assert.AreEqual(updatedTourLog.Comment, result.Comment);
        }

        [Test]
        public void ExportTour_SelectedToursCouldBeExported()
        {
            //Arrange
            Tour tour = new Tour()
            {
                Id = 1,
                Name = "Test",


            };

            string dateString = $"{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}";

            ConfigurationManager.AppSettings["ExportImportSubdir"] = "Exports";


            string fileName = $"{tour.Name}_{dateString}.json";
            string filePath = Path.Combine(ConfigurationManager.AppSettings["ExportImportSubdir"], fileName);


            //Act

            tourManager.ExportTour(tour);

            //Assert

            Assert.That(File.Exists(filePath));

            File.Delete(filePath);

        }
        [Test]
        public void ImportTour_ToursCouldBeImported()
        {
            //Arrange
            Tour tour = new Tour()
            {
                Id = 1,
                Name = "Test",
            };

            string dateString = $"{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}";

            ConfigurationManager.AppSettings["ExportImportSubdir"] = "Exports";


            string fileName = $"{tour.Name}_{dateString}.json";
            string filePath = Path.Combine(ConfigurationManager.AppSettings["ExportImportSubdir"], fileName);



            dal.Setup(x => x.AddTour(tour)).Returns(tour);
            tourManager.ExportTour(tour);

            //Act
            Tour importedTour = ExportImportManager.ImportTourFromFile(filePath);


            //Assert

            Assert.IsTrue(File.Exists(filePath));
            Assert.That(tour.Name, Is.EqualTo(importedTour.Name));
            File.Delete(filePath);



        }

        [Test]

        public void ChildFriendly_CouldBeBeReachedByLessDistance()

        {
            //Arrange

            List<Tour> tours = new()
            {
              new Tour() { Id = 1, Name = "Test", Distance = 15 , EstimatedTime = TimeSpan.FromMinutes(25) },
              new Tour() { Id = 2, Name = "Test", Distance = 45 , EstimatedTime = TimeSpan.FromMinutes(80) },



            };


            //Act

            double result1 = tourManager.CaculateChildFriendlyness(tours[0]);
            double result2 = tourManager.CaculateChildFriendlyness(tours[1]);



            //Assert

            Assert.Greater(result2, result1);


        }

        [Test]
        public void ChildFriendly_CouldBeReachedByLowTime()

        {
            //Arrange

            List<Tour> tours = new List<Tour>()
            {


              new Tour() { Id = 1, Name = "Test", Distance = 60 , EstimatedTime = TimeSpan.FromMinutes(50) },
              new Tour() { Id = 2, Name = "Test", Distance = 60 , EstimatedTime = TimeSpan.FromMinutes(90)}

            };


            //Act

            double result1 = tourManager.CaculateChildFriendlyness(tours[0]);
            double result2 = tourManager.CaculateChildFriendlyness(tours[1]);



            //Assert

            Assert.Less(result1, result2);


        }

        /* [Test]

         public  async Task CallGetRouteAndGetImage_ToursCouldBeAttachedWithMap()

         {
             //Arrange 
             Tour tour = new Tour()
             {  Name = "TestTour", Id = 1, From = "Wien", To = "Graz", Description ="Description1 " };



             Route route = new Route();
             route.distance = 1000;
             route.sessionID = "AKUA5wcAAIwAAAAAAAAAEwAAALQAAAB42mPYwsDGyMTAwMCekVqUapWcm1lynBXIZZASftnIJXVhWvd7lplJ8UBakHVmkggDJoBpPH-OB6zx86kbdQwXWzZ2rw_zT7oNpFcDaWwaQYCP8VdUPSOQcZ_BIYWBqaGhwaGJgYGFkaGFgUGFQUmAgYNBQIFDtFGBIYDDiWWhokeToILRBhcRAATBKV3lXspR:bicycle";
             route.estimatedTime = TimeSpan.FromHours(60);
               route.ul_Latitude = ul_lat;
               route.ul_Longitude = ul_lng;
               route.lr_Latitude = lr_lat;
               route.lr_Longitude = lr_lng;


             var imageStream = new MemoryStream();

             mapquest.Setup(x => x.GetRoute(tour)).ReturnsAsync(route);
             mapquest.Setup(x => x.GetImage(route)).ReturnsAsync(imageStream);
             //Route route =  await mapquest.GetRoute(tour);

             //Act

             await tourManager.CallGetRouteAndGetImage(tour);

             //Assert

             mapquest.Verify(x =>  x.GetRoute(tour), Times.Once());

             var filePath = $"{tour.Name}{tour.Id}.png";

             //Assert.IsTrue(File.Exists(filePath));




         }*/

        [Test]
        public void AverageRating_ShouldReturnRatingAverage()
        
        {
            //Arrange

            List<TourLog> tourlogs = new List<TourLog>
            {

           
                new TourLog() { Rating = Rating.VeryGood},
                new TourLog() { Rating = Rating.Good},
                new TourLog() { Rating = Rating.Okay},
                new TourLog() { Rating = Rating.Bad},
                new TourLog() { Rating = Rating.VeryBad}


            };


            //Act

            var AverageResult = tourManager.AverageRating(tourlogs);

            //Assert

            Assert.That(AverageResult, Is.EqualTo(3));






        }


        [Test]
        public void AverageTime_ShouldReturnTotalTimeAverage()

        {


            //Arrange

            List<TourLog> tourlogs = new List<TourLog>
            {


                new TourLog() { TotalTime = TimeSpan.FromMinutes(0) },
                new TourLog() { TotalTime = TimeSpan.FromMinutes(10) },
                new TourLog() { TotalTime = TimeSpan.FromMinutes(30) },
                new TourLog() { TotalTime = TimeSpan.FromMinutes(50) },
                new TourLog() { TotalTime = TimeSpan.FromMinutes(50) }


            };


            //Act

            var AverageResult = tourManager.AverageTime(tourlogs);

            //Assert

            Assert.That(AverageResult, Is.EqualTo(TimeSpan.FromSeconds(2100)));



        }



    }
}
