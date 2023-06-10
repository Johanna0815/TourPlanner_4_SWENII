using System;
using System.Diagnostics;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.logging;
using TourPlanner_4_SWENII.Models;

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

        public MainViewModel(ITourManager tManager, NavBarVM nbVM, SearchBarVM sbVM, TourInfoVM tiVM, TourLogsVM tlogVM, ToursListViewModel tlistvm, IWindowService windowService, IMapQuest mapquest) //SearchViewModel svm
        {
            navBarVM= nbVM;
            searchBarVM= sbVM;
            tourInfoVM= tiVM;
            tourLogsVM= tlogVM;
            toursListViewModel = tlistvm;
            this.tourManager = tManager;
            this.windowService = windowService;
            this.mapQuest = mapquest;


            searchBarVM.SearchForText += (_, searchParams) =>
            {
                logger.Debug($"Searching for text {searchParams}");

                toursListViewModel.SearchFor(searchParams); 
            };

            searchBarVM.SearchCleared += (_, searchText) =>
            { 
                toursListViewModel.Tours.Clear(); 
                toursListViewModel.FillListBox(); 
            };

            navBarVM.OnExportTour += (_, _) =>
            {
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

                try
                {
                    Tour tour = tourManager.ImportTourFrom(filePath);
                    tourManager.CallGetRouteAndGetImage(tour);
                    toursListViewModel.FillListBox();
                }
                catch (InvalidOperationException e)
                {
                   logger.Warn($"{e.Message}");
                   
                }

                
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
                    tourInfoVM.GetTour(0);
                }
            };

            navBarVM.GenerateReport += (_, _) =>
            {
                var tour = toursListViewModel.SelectedItem;

                tourManager.GenerateReport(tour,tour.Name + "_Report.pdf");

            };

            navBarVM.GenerateTourLogsReport += (_, _) =>
            {
                var tours = toursListViewModel.Tours; // Get the list of all tours

                tourManager.Summarize_TourLogs ("Summarize_Report.pdf");

            };

            /*
            navBarVM.GetMap += (_, tour) =>
            {
                //CallGetRouteAndGetImage(tour);
            };*/

            toursListViewModel.OnGetMap += (_, tour) =>
            {
                tourManager.CallGetRouteAndGetImage(tour);
            };
        }

       /* private async Task CallGetRouteAndGetImage(Tour tour)
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

        }*/
    }
}
