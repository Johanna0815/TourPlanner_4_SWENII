using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private NavBarVM navBarVM;
        private SearchBarVM searchBarVM;
        private TourInfoVM tourInfoVM;
        private TourLogsVM tourLogsVM;
        private ToursListViewModel toursListViewModel;
        //private ClearCommandVM clearCommandVM;



        public MainViewModel(NavBarVM nbVM, SearchBarVM sbVM, TourInfoVM tiVM, TourLogsVM tlogVM, ToursListViewModel tlistvm) //SearchViewModel svm
        {
            navBarVM= nbVM;
            searchBarVM= sbVM;
            tourInfoVM= tiVM;
            tourLogsVM= tlogVM;
            toursListViewModel = tlistvm;

            /*
            addGreetingBarViewModel.GreetingNameChanged += (_, greetingName) =>
            greetingsViewModel.Greetings.Add(new Greeting() { Text = greetingName, Created = DateTime.Now, Status = true });
            */

        }


    }
}
