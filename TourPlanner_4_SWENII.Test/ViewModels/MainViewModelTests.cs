//using TourPlanner_4_SWENII.BL;
//using TourPlanner_4_SWENII.ViewModels;

//namespace TourPlanner_4_SWENII.Test.ViewModels
//{
//    public class Tests
//    {
//        private MainViewModel mVM;
//        private NavBarVM nbVM;
//        private SearchBarVM sbVM;
//        private TourInfoVM tiVM;
//        private TourLogsVM tlogVM;
//        private ToursListViewModel tlistVM;
//        private ITourManager tourManager;

//        [SetUp]
//        public void Setup()
//        {
//            //Arrange
//            nbVM = new NavBarVM();
//            sbVM= new SearchBarVM();
//            tiVM= new TourInfoVM();
//            tlogVM= new TourLogsVM();
//            tlistVM = new ToursListViewModel();
//            tourManager = TourManagerFactory.GetInstance();
//            mVM = new MainViewModel(tourManager, nbVM, sbVM, tiVM, tlogVM, tlistVM);
            
//        }

//        [Test]
//        public void TestItems_ShouldContainInitialTourList()
//        {
//            //Act
//            int actual = tlistVM.Items.Count();
//            int expected = 5;
//            //Assert
//            Assert.That(actual, Is.EqualTo(expected));
//        }

//        [Test]
//        public void TestItems_ShouldSearchForText()
//        {
//            //Act:

//            // enter search text
//            string text = "de";
//            sbVM.SearchText = text;

//            // 2. simulate button click on [Search]
//            sbVM.SearchCommand.Execute(null);

//            //Assert
//            // 3. check if the TourList Items contains the right amount of items

//            int actual = tlistVM.Items.Count();
//            int expected = 4;

//            Assert.That(actual, Is.EqualTo(expected));
//            //Assert.That(tlistVM.Items.Any(Greeting => Greeting.Text == "Hola"));
//        }

//        [Test]

//        public void SearchedText_ShouldBe_Cleared()
        
//        {

//            //Act

            
//            string expected = "";
//            sbVM.ClearCommand.Execute(null);

//            //Assert

//            Assert.That(sbVM.SearchText,Is.EqualTo(expected));



//        }
//    }
//}