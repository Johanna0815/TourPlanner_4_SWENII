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


        [SetUp]
        public void SetUp()
        {

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
            var tourManager  = new Mock<ITourManager>();
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
            tourManager.Setup(x=>x.GetTours()).Returns(tours);
            tlvm.FillListBox();

            //Act
            var tourToDelete = tours.First();  
            tlvm.DeleteTour(tourToDelete);
            //tlvm.FillListBox();


            //Assert

            Assert.AreEqual(2, tlvm.Tours.Count);


        }





    }
}
