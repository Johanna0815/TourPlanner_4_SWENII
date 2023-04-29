using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.BL;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private NavBarVM navBarVM;
        private SearchBarVM searchBarVM;
        private TourInfoVM tourInfoVM;
        private TourLogsVM tourLogsVM;
        private ToursListViewModel toursListViewModel;
        private ITourManager tourManager;

        public MainViewModel(ITourManager tourManager, NavBarVM nbVM, SearchBarVM sbVM, TourInfoVM tiVM, TourLogsVM tlogVM, ToursListViewModel tlistvm) //SearchViewModel svm
        {
            

            navBarVM= nbVM;
            searchBarVM= sbVM;
            tourInfoVM= tiVM;
            tourLogsVM= tlogVM;
            toursListViewModel = tlistvm;
            this.tourManager = tourManager;

            searchBarVM.SearchForText += (_, searchText) =>
            { 
                //toursListViewModel.Items.Clear(); 
                toursListViewModel.SearchFor(searchText); 
            };

            searchBarVM.SearchCleared += (_, searchText) =>
            { 
                toursListViewModel.Tours.Clear(); 
                toursListViewModel.FillListBox(); 
            };



            toursListViewModel.PropertyChanged += (_, SelectedItem) =>
            {
                Debug.WriteLine($"property selectedItem {SelectedItem} was changed");
                if(toursListViewModel.SelectedItem != null)
                {
                    tourLogsVM.GetTourLogs(toursListViewModel.SelectedItem.Id);
                    tourInfoVM.GetTour(toursListViewModel.SelectedItem.Id);
                }
                else
                {
                    tourLogsVM.GetTourLogs(0);
                }
            };

            navBarVM.GenerateReport += (_, _) =>
            {
                var tour = toursListViewModel.SelectedItem;

                tourManager.GenerateReport(tour, "report.pdf");

                    
            };
        }
    }
}
