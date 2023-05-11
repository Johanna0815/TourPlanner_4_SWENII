using Moq;
using Moq;
using NUnit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.DAL;
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
        [Test]

        public void Tours_ShouldBeAdded()
        {
            //Arrange
            var tourManager = new Mock<ITourManager>();
            var tlvm = new ToursListViewModel(tourManager.Object);

            ObservableCollection<Tour> tour = new()
            {
                new Tour { Name = "Tour1", Description = "Description1", From = "From1", To = "To1", Distance = 100, TransportType = 0 }

            };

            tourManager.Setup(x => x.GetTours()).Returns(tour);

            //Act
            tlvm.AddTour();

            //Assert

            Assert.AreEqual(tour.First().Name, tlvm.Tours.First().Name);







        }



        [Test]
        public void ToursForm_ShouldBeEmptyAfterAdding()
        {
            // Arrange 
            var tourManager = new Mock<ITourManager>();
            var viewModel = new ToursListViewModel(tourManager.Object);



            // Act 
            viewModel.NewTourName = "MyTour";
            viewModel.AddTour();




            //Assert



            Assert.AreEqual("", viewModel.NewTourName);
        }

        [Test]

        public void Tours_ShouldBeAddedToViewList()


        {
            // Arrange
            ObservableCollection<Tour> tours = new()
            {
                    new Tour { Name = "Tour1", Description = "Description1", From = "From1", To = "To1", Distance = 100, TransportType = 0 },
                    new Tour { Name = "Tour2", Description = "Description2", From = "From2", To = "To2", Distance = 200, TransportType = 0 },
                    new Tour { Name = "Tour3", Description = "Description3", From = "From3", To = "To3", Distance = 300, TransportType = 0 }
            };

            var tourManager = new Mock<ITourManager>();

            tourManager.Setup(x => x.GetTours()).Returns(tours);
            var tlvm = new ToursListViewModel(tourManager.Object);

            // Act

            tlvm.FillListBox();



            // Assert
            Assert.That(tlvm.Tours.Count, Is.EqualTo(3));

        }

        [Test]

        public void Tours_CouldBeSearched()

        {
            //Arrange

            var tourManager = new Mock<ITourManager>();
            var tlvm = new ToursListViewModel(tourManager.Object);

            ObservableCollection<Tour> expectedtours = new()
            {
                    new Tour { Name = "Tour1", Description = "Description1", From = "From1", To = "To1", Distance = 100, TransportType = 0 },
                    new Tour { Name = "Tour2", Description = "Description2", From = "From2", To = "To2", Distance = 200, TransportType = 0 },
                    new Tour { Name = "Tour3", Description = "Description3", From = "From3", To = "To3", Distance = 300, TransportType = 0 }
            };

            var matchingTours2 = new List<Tour>
            {
                new Tour { Name = "Paris ", Description = " Paris1" },
                new Tour { Name = "Paris Tour", Description = "Paris2" }
            };




            var searchingText = "tour";
            tourManager.Setup(x => x.Search(searchingText, false)).Returns(expectedtours);
            tourManager.Setup(x => x.Search("Paris", false)).Returns(matchingTours2);





            //Act 

            tlvm.SearchFor(searchingText);
            Assert.AreEqual(3, tlvm.Tours.Count());

            //Assert

            tlvm.SearchFor("Paris");
            Assert.AreEqual(2, tlvm.Tours.Count());




        }


        [Test]


        public void Tour_ShouldBeDeleted()

        {
            //Arrange
            var tourManager = new Mock<ITourManager>();
            var tlvm = new ToursListViewModel(tourManager.Object);

            ObservableCollection<Tour> tours = new()
            {
                new Tour {Name="Tour1"},
                new Tour {Name="Tour2"},
                new Tour {Name="Tour3"}

            };

            ObservableCollection<Tour> expectedTours = new()
            {
                new Tour {Name="Tour2"},
                new Tour {Name="Tour3"}

            };



            // tourManager.Setup(x => x.DeleteTour(tours.First()));
            tourManager.Setup(x => x.GetTours()).Returns(tours);
            tlvm.FillListBox();

            //Act
            var tourToDelete = tours.First();
            tlvm.DeleteTour(tourToDelete);
            //tlvm.FillListBox();


            //Assert

            Assert.AreEqual(2, tlvm.Tours.Count);


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
