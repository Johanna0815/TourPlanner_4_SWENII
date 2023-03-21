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


        ToursListViewModel tourListViewModel = new ToursListViewModel();
        //SearchViewModel searchViewModel = new SearchViewModel(mediaItemFactory);
        ClearCommandVM clearCommandVM = new ClearCommandVM();



        var wnd = new MainWindow()
        {

            DataContext = new MainViewModel(tourListViewModel,clearCommandVM),
            SearchBar = { DataContext = clearCommandVM },
            ToursListView = { DataContext = tourListViewModel },
            TourInfo = {DataContext = tourListViewModel},
            //TourLogs = {DataContext = tourListViewModel },
            //NavBarView = { DataContext = tourListViewModel },

        };
        wnd.Show();
      
}
    }
}
