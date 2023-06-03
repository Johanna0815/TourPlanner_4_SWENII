using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
//using log4net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.logging;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Utils.FileAndFolderHandling;

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
        private IWindowService windowService;
        private IMapQuest mapQuest;
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        public MainViewModel(ITourManager tourManager, NavBarVM nbVM, SearchBarVM sbVM, TourInfoVM tiVM, TourLogsVM tlogVM, ToursListViewModel tlistvm, IWindowService windowService, IMapQuest mapquest) //SearchViewModel svm
        {
            navBarVM= nbVM;
            searchBarVM= sbVM;
            tourInfoVM= tiVM;
            tourLogsVM= tlogVM;
            toursListViewModel = tlistvm;
            this.tourManager = tourManager;
            this.windowService = windowService;
            this.mapQuest = mapquest;


            searchBarVM.SearchForText += (_, searchText) =>
            {
                //toursListViewModel.Items.Clear(); 
                logger.Debug($"Searching for text {searchText}");

                toursListViewModel.SearchFor(searchText); 
            };

            searchBarVM.SearchCleared += (_, searchText) =>
            { 
                toursListViewModel.Tours.Clear(); 
                toursListViewModel.FillListBox(); 
            };

            navBarVM.OnExportTour += (_, _) =>
            {
                //toursListViewModel.Items.Clear(); 
                logger.Debug($"Exporting Tour {toursListViewModel.SelectedItem.Name }");

                tourManager.ExportTour(toursListViewModel.SelectedItem);
            };

            navBarVM.OnImportTour += (_, _) =>
            {
                string filePath = windowService.ShowSelectFileDialog();
                if (filePath != null && filePath != string.Empty)
                {
                    logger.Info_Notice($"Importing Tour from path {filePath}");
                }
                //Tour importedTour = ExportFile.ImportTourFromFile();
                Tour tour = tourManager.ImportTourFrom(filePath);
                CallGetRouteAndGetImage(tour);
                toursListViewModel.FillListBox();
                //tourManager.ExportTour(toursListViewModel.SelectedItem);
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

                tourManager.GenerateReport(tour,tour.Name + "_Report.pdf");



            };

            navBarVM.GetMap += (_, tour) =>
            {
                //CallGetRouteAndGetImage(tour);
            };

            toursListViewModel.OnGetMap += (_, tour) =>
            {
                CallGetRouteAndGetImage(tour);
            };
        }

        private async Task CallGetRouteAndGetImage(Tour tour)
        {
            Route route = await mapQuest.GetRoute(tour);

            // var route = task.Result;
            tour.Distance = route.distance; // ObjectRefernce not setted to an inst of an obkj
            tour.EstimatedTime = route.estimatedTime;
            tourManager.UpdateTour(tour);

            //
            //  RaisePropertyChangedEvent(nameof(SelectedItem));

            Stream awaitStream = await mapQuest.GetImage(route);

            await using var filestream = new FileStream($"{tour.Name}{tour.Id}.png", FileMode.Create, FileAccess.Write);
            awaitStream.CopyTo(filestream);

        }
    }
}
