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
        //IEnumerable<Tour> Tours;
        //Tour tour;

        ObservableCollection<Tour> tours;
        Mock<ITourManager> tourManager;
        Mock<IMapQuest> mapquest;
        ToursListViewModel tlvm;

        //ITourManager TourManager;

        [SetUp]
        public void SetUp()
        {
            

            tourManager = new Mock<ITourManager>();
            mapquest = new Mock<IMapQuest>();
            tlvm = new ToursListViewModel(tourManager.Object, mapquest.Object);

            tours = new()
            {
                new Tour { Name = "Tour1", Description = "Description1", From = "From1", To = "To1", Distance = 100, TransportType = 0 }

            };

            //TourManager = new MoqTourManager(Tours);
        }

       /* [Test]
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

        }*/


        [Test]

        public void Tours_ShouldBeAdded()
        {
            //Arrange

            tourManager.Setup(x => x.GetTours()).Returns(tours);

            //Act
            tlvm.AddTour();

            //Assert

            Assert.That(tlvm.Tours.First().Name, Is.EqualTo(tours.First().Name));

        }



        [Test]
        public void ToursForm_ShouldBeEmptyAfterAdding()
        {
            // Arrange // by Setup()
          
            // Act 
            tlvm.NewTourName = "MyTour";
            tlvm.AddTour();

            //Assert

            Assert.That(tlvm.NewTourName, Is.EqualTo(""));
        }

        [Test]

        public void Tours_ShouldBeAddedToViewList()

        {
            // Arrange
            ObservableCollection<Tour> expectedTours = new()
            {
                    new Tour { Name = "Tour1", Description = "Description1", From = "From1", To = "To1", Distance = 100, TransportType = 0 },
                    new Tour { Name = "Tour2", Description = "Description2", From = "From2", To = "To2", Distance = 200, TransportType = 0 },
                    new Tour { Name = "Tour3", Description = "Description3", From = "From3", To = "To3", Distance = 300, TransportType = 0 }
            };

            tourManager.Setup(x => x.GetTours()).Returns(expectedTours);
          
            // Act

            tlvm.FillListBox();

            // Assert
            Assert.That(tlvm.Tours, Has.Count.EqualTo(3));

        }

        [Test]

        public void Tours_CouldBeSearched()

        {
            //Arrange

         

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

            
            var searchingText = "tour1";
            tourManager.Setup(x => x.Search(searchingText, false)).Returns(expectedtours);
            tourManager.Setup(x => x.Search("Paris", false)).Returns(matchingTours2);


            //Act 

            tlvm.SearchFor(searchingText);
            Assert.That(tlvm.Tours.Count(), Is.EqualTo(3));

            //Assert

            tlvm.SearchFor("Paris");
            Assert.That(tlvm.Tours.Count(), Is.EqualTo(2));

        }


        [Test]


        public void Tour_ShouldBeDeleted()

        {
            //Arrange

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

            Assert.That(tlvm.Tours.Count, Is.EqualTo(2)); // expectedTours != tlvm.Tours ???


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
