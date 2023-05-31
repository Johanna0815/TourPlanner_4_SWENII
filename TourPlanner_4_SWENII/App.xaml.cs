using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.ViewModels;
using TourPlanner_4_SWENII.Views;

namespace TourPlanner_4_SWENII
{
    public partial class App : Application
    {
        public ITourManager? mediaItemFactory;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var ioCConfig = (IoCContainerConfig)Application.Current.Resources["IoCConfig"];

            var wnd = new MainWindow
            {
                DataContext = ioCConfig.MainViewModel,
                NavBarView = { DataContext = ioCConfig.NavBarVM },
                SearchBar = { DataContext = ioCConfig.SearchBarVM },
                TourInfo = { DataContext = ioCConfig.TourInfoVM },
                TourLogs = { DataContext = ioCConfig.TourLogsVM },
                ToursListView = { DataContext = ioCConfig.ToursListViewModel }
            };
            wnd.Show();
        }
    }
}


/* All of this is now managed through services
            
            // Create all Layers

            //SearchViewModel searchViewModel = new SearchViewModel(mediaItemFactory); // Erstellen Sie eine IMediaItemFactory-Instanz


            // create all VMs and inject them later
            ITourManager tourManager = TourManagerFactory.GetInstance();
            NavBarVM navBarVM = new NavBarVM();
            SearchBarVM searchBarVM = new SearchBarVM();
            TourInfoVM tourInfoVM = new TourInfoVM();
            TourLogsVM tourLogsVM = new TourLogsVM();
            ToursListViewModel tourListViewModel = new ToursListViewModel(tourManager);
            //ClearCommandVM clearCommandVM = new ClearCommandVM();

            var wnd = new MainWindow()
            {

                DataContext = new MainViewModel(tourManager, navBarVM, searchBarVM, tourInfoVM, tourLogsVM, tourListViewModel),

                NavBarView = { DataContext = navBarVM },
                SearchBar = { DataContext = searchBarVM },
                TourInfo = { DataContext = tourInfoVM },
                TourLogs = { DataContext = tourLogsVM },
                ToursListView = { DataContext = tourListViewModel }
            };
            wnd.Show();*/