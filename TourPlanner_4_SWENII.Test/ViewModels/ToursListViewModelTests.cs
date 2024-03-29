﻿using Moq;

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

        ObservableCollection<Tour> tours;
        Mock<ITourManager> tourManager;
        Mock<IMapQuest> mapquest;
        IWindowService windowService;
        ToursListViewModel tlvm;

        [SetUp]
        public void SetUp()
        {
            

            tourManager = new Mock<ITourManager>();
            mapquest = new Mock<IMapQuest>();
            tlvm = new ToursListViewModel(tourManager.Object, mapquest.Object,windowService);

            tours = new()
            {
                new Tour { Name = "Tour1", Description = "Description1", From = "From1", To = "To1", Distance = 100, TransportType = 0 }

            };

        }


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
            // Arrange // in Setup()
          
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

            SearchParameters sParams1 = new()
            {
                searchText = "Tour",
                caseSensitive = true,
                searchInTourLogs = false,
            };

            SearchParameters sParams2 = new()
            {
                searchText = "Tour",
                caseSensitive = true,
                searchInTourLogs = false,
            };


            IEnumerable<Tour> expectedtours = new List<Tour>()
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
            tourManager.Setup(x => x.Search(sParams1)).Returns(expectedtours);
            tourManager.Setup(x => x.Search(sParams2)).Returns(matchingTours2);


            //Act 

            tlvm.SearchFor(sParams1);
            Assert.That(tlvm.Tours.Count(), Is.EqualTo(3));

            //Assert

            tlvm.SearchFor(sParams2);
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

}
