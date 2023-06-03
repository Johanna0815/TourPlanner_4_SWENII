using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.DAL;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.Test.BL
{
    public class TourManagerImplTests
    {
        Mock<IDataHandler> dal;
        ITourManager tourManager;
        List<Tour> tours;

        [SetUp] 
        public void SetUp() 
        { 
            dal = new Mock<IDataHandler>();


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
                new Tour(){ Id=2, Name="hello woowold" },
                new Tour(){ Id=3, Name="TOUR 3"}
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

            //Act 
            IEnumerable<Tour> receivedTours = tourManager.Search("tour",false);


            //Assert
            Assert.That(receivedTours.Count(), Is.EqualTo(tours.Count - 1));


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

            //Act

            tourManager.GenerateReport(tours[0], filename);

            //assert

            Assert.IsTrue(File.Exists(filename));

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
            Assert.False(result.Contains(tours[0].TourLogs.First()));

        }

        [Test]
        public void UpdateTourlog_ShouldCallDALUpdateTourLog()
        {
            // Arrange
            var existingTourLog = new TourLog { Id = 1, Comment = "Old Comment" };
            var updatedTourLog = new TourLog { Id = 1, Comment = "New Comment" };
            dal.Setup(x => x.UpdateTourLog(It.IsAny<TourLog>())).Returns(updatedTourLog);

            // Act
            var result =  tourManager.UpdateTourLog(existingTourLog);

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

            string folderPath = "Exports";
            string fileName = $"{tour.Name}_{dateString}.json";
            string filePath = Path.Combine(folderPath, fileName);


            //Act

            tourManager.ExportTour(tour);

            //Assert

            Assert.IsTrue(File.Exists(filePath));

            File.Delete(filePath);

        }





    }
}
