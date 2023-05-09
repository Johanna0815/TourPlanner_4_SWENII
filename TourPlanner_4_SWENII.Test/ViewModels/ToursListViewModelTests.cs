using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.Models;
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
        public void Tours_ShouldContain()
        {
            //Arrange
            ToursListViewModel tlvm;
            Tour tour = new() { Name="Tour1" };

            //Act
            tlvm = new ToursListViewModel();

            //Assert
            Assert.IsNotNull(tlvm);
            Assert.IsNotNull(tlvm.Tours);
            Assert.That(tlvm.Tours, Has.Exactly(1).Items);
            //Assert.Contains("");

        }

        [Test]
        public void Test2()
        {
            Assert.True(true);
        }
    }
}
