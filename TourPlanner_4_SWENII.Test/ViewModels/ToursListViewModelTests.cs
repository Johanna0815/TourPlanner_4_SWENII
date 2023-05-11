using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Models.HelperEnums;
using TourPlanner_4_SWENII.ViewModels;

namespace TourPlanner_4_SWENII.Test.ViewModels
{
    public class ToursListViewModelTest
    {
       // nsjkdf wher T : Tour();
        IEnumerable<Tour> Tours;
        Tour tour;
        //ITourManager TourManager;

        [SetUp]
        public void SetUp()
        {
            tour = new() { Name = "Tour1" };
            Tours = new List<Tour>() { tour };
            //TourManager = new MoqTourManager(Tours);
        }
        
        [Test]
        public void Tours_ShouldContain()
        {
            var mockTourManager = new Mock<ITourManager>();
            mockTourManager.Setup(m => m.GetTours()).Returns(Tours); //m.GetTours(It.IsAny<Product>())

            //Arrange
            ToursListViewModel tlvm;

            //Act
            tlvm = new ToursListViewModel(mockTourManager.Object);

            //Assert
            Assert.IsNotNull(tlvm);
            Assert.IsNotNull(tlvm.Tours);
            Assert.That(tlvm.Tours, Has.Exactly(1).Items);
            Assert.That(tlvm.Tours[0].Name, Is.EqualTo(tour.Name));
            //Assert.Contains("");

        }

        [Test]
        public void Test2()
        {
            Assert.True(true);
        }
    }
    /*
    public class MoqTourManager : ITourManager
    {
        IEnumerable<Tour> Tours;

        public MoqTourManager(IEnumerable<Tour> Tours)
        {
            this.Tours = Tours;
        }

        public Tour AddTour(string tourName, string description, string from, string to, TransportType transportType, decimal distance)
        {
            throw new NotImplementedException();
        }

        public TourLog AddTourLog(int TourId)
        {
            throw new NotImplementedException();
        }

        public void DeleteTour(Tour item)
        {
            throw new NotImplementedException();
        }

        public void DeleteTourLog(TourLog tourLog)
        {
            throw new NotImplementedException();
        }

        public void GenerateReport(Tour tour, string filename)
        {
            throw new NotImplementedException();
        }

        public void GetMap(Tour tour)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TourLog> GetTourLogs(int tourId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tour> GetTours()
        {
            return Tours;
            
        }

        public IEnumerable<Tour> Search(string itemName, bool caseSensitive = false)
        {
            throw new NotImplementedException();
        }

        public void UpdateTourLog(TourLog tourLog)
        {
            throw new NotImplementedException();
        }
    }*/
}
