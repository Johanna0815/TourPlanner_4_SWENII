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

        public MainViewModel(NavBarVM nbVM, SearchBarVM sbVM, TourInfoVM tiVM, TourLogsVM tlogVM, ToursListViewModel tlistvm) //SearchViewModel svm
        {
            navBarVM= nbVM;
            searchBarVM= sbVM;
            tourInfoVM= tiVM;
            tourLogsVM= tlogVM;
            toursListViewModel = tlistvm;

            searchBarVM.SearchForText += (_, searchText) =>
            { 
                //toursListViewModel.Items.Clear(); 
                toursListViewModel.SearchFor(searchText); 
            };

            searchBarVM.SearchCleared += (_, searchText) =>
            { 
                toursListViewModel.Items.Clear(); 
                toursListViewModel.FillListBox(); 
            };
        }
    }
}
