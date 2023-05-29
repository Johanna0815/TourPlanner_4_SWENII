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
                    TransportType=Models.HelperEnums.TransportType.Bike, 
                    Distance=200m,
                    EstimatedTime=TimeSpan.FromHours(2), //new TimeSpan(0, 30, 0),
                    TourLogs=new List<TourLog>()},
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
    }
}
