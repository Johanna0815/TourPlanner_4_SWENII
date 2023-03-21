using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ToursListViewModel toursListViewModel;
       // private SearchViewModel searchViewModel;
       private ClearCommandVM clearCommandVM;

        public MainViewModel(ToursListViewModel tlvm ,ClearCommandVM ccvm) //SearchViewModel svm
        {
          //  this.searchViewModel = svm;
            this.toursListViewModel = tlvm;

            clearCommandVM = ccvm;
        
        }


    }
}
