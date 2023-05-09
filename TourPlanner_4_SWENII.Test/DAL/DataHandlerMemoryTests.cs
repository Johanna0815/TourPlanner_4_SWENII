using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.DAL;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.Test.DAL
{
    public class DataHandlerMemoryTests
    {
        public IDataHandler dal;


        [SetUp]
        public void Setup()
        {
            dal = new DataHandlerMemory();


        }

        [Test]
        public void ToTestDatabaseValues()
        {
            //Arrange

            // erstelle neue Tour zum Einfügen in die DummyDB.
            //  Tour tours = new Tour();
            IEnumerable<Tour> tours = new List<Tour>();




            //Act
            // die Funktion AddTour wird korrekt ausgeführt. 

            Tour tourTest = new Tour() { Description = "Test", Distance = 6.5m, Name = "testtoureins" };
            dal.AddTour(tourTest);

            //tourTest = dal.GetTours().First();

            //Tour expectedTour = new Tour() { Description = "Test", Distance = 6.5m, Name = "testtoureins" };
            //dal.AddTour(expectedTour);

            //Assert
            // 
            //Assert.That(expectedTour, Is.EqualTo(tourTest));
            tours = dal.GetTours();
            Assert.IsEmpty(tours);
            //Assert.That(tours.Count(), Is.EqualTo(1));

        }
    }
}
