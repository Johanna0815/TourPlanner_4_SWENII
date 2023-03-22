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
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IMediaItemFactory? mediaItemFactory;

        private void Application_Startup(object sender, StartupEventArgs e)
        {

        // Erstellen Sie eine IMediaItemFactory-Instanz

        //SearchViewModel searchViewModel = new SearchViewModel(mediaItemFactory);
        NavBarVM navBarVM = new NavBarVM();
        SearchBarVM searchBarVM = new SearchBarVM();
        TourInfoVM tourInfoVM = new TourInfoVM();
        TourLogsVM tourLogsVM = new TourLogsVM();
        ToursListViewModel tourListViewModel = new ToursListViewModel();
        //ClearCommandVM clearCommandVM = new ClearCommandVM();



        var wnd = new MainWindow()
        {

            DataContext = new MainViewModel(navBarVM, searchBarVM, tourInfoVM, tourLogsVM, tourListViewModel),
            
            NavBarView = { DataContext = navBarVM },
            SearchBar = { DataContext =  searchBarVM}, //clearCommandVM
            TourInfo = {DataContext = tourInfoVM},            //tourListViewModel //change !!
            TourLogs = {DataContext = tourLogsVM },
            ToursListView = { DataContext = tourListViewModel }
        };
        wnd.Show();
      
}
    }
}
