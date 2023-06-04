using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
//using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using log4net.Repository.Hierarchy;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.logging;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Models.HelperEnums;
using TourPlanner_4_SWENII.ViewModels;
using TourPlanner_4_SWENII.Views;
using Xceed.Wpf.Toolkit.Primitives;
//using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class ToursListViewModel : ViewModelBase
    {

        private ITourManager tourManager;
        private IMapQuest mapquest;
        private IWindowService windowService;

        private bool currentlyEditing = false;

        public event EventHandler<Tour> OnGetMap;
        public ObservableCollection<Tour> Tours { get; set; } = new();
        public Dictionary<TransportType, string> TransportTypeWithCaptions { get; } =
        new Dictionary<TransportType, string>()
        {
            {TransportType.Pedestrian, "walking"},
            {TransportType.Bicycle, "cycling" },
            {TransportType.Fastest, "by car (fastest)"},
            {TransportType.Shortest, "by car (shortest)"},
        };

        private string newTourName = string.Empty;
        public string NewTourName
        {
            get => newTourName;

            set
            {
                if (newTourName != value)
                {
                    newTourName = value;
                    this.RaisePropertyChangedEvent();
                    this.AddTourCommand.RaiseCanExecuteChanged();
                    this.UpdateTourCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;

            set
            {
                if (_description != value)
                {
                    _description = value;
                    this.RaisePropertyChangedEvent();
                    //this.AddTourCommand.RaiseCanExecuteChanged();
                    //this.UpdateTourCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string from = string.Empty;
        public string From
        {
            get => from;

            set
            {
                if (from != value)
                {
                    from = value;
                    this.RaisePropertyChangedEvent();
                    this.AddTourCommand.RaiseCanExecuteChanged();
                    this.UpdateTourCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string to = string.Empty;
        public string To
        {
            get => to;

            set
            {
                if (to != value)
                {
                    to = value;
                    this.RaisePropertyChangedEvent();
                    this.AddTourCommand.RaiseCanExecuteChanged();
                    this.UpdateTourCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private Models.HelperEnums.TransportType _transportType;
        public Models.HelperEnums.TransportType TransportType
        {
            get => _transportType;

            set
            {
                if (_transportType != value)
                {
                    _transportType = value;
                    this.RaisePropertyChangedEvent();
                    this.AddTourCommand.RaiseCanExecuteChanged();
                    this.UpdateTourCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private Tour _selecteditem;
        public Tour SelectedItem
        {
            get => _selecteditem;

            set
            {
                if (value != _selecteditem)
                {
                    _selecteditem = value;

                    RaisePropertyChangedEvent();
                    UpdateTourCommand.RaiseCanExecuteChanged();
                    DeleteTourCommand.RaiseCanExecuteChanged();
                    FillFormCommand.RaiseCanExecuteChanged();
                    if (currentlyEditing)
                    {
                        FillForm();
                    }
                }
            }
        }
        /*
        private decimal _distance;
        public decimal Distance
        {
            get => _distance;

            set
            {
                if (_distance != value)
                {
                    _distance = value;
                    this.RaisePropertyChangedEvent();
                    this.AddTourCommand.RaiseCanExecuteChanged();
                    this.UpdateTourCommand.RaiseCanExecuteChanged();
                }
            }
        }*/

        public RelayCommand AddTourCommand { get; set; }
        public RelayCommand DeleteTourCommand { get; set; }
        public RelayCommand UpdateTourCommand { get; set; }
        public RelayCommand FillFormCommand { get; set; }
        public RelayCommand EmptyFormCommand { get; set; }
        //public event EventHandler<string> TourAdded;

        public ToursListViewModel(ITourManager tourManager, IMapQuest mapquest, IWindowService windowService) //
        {
            this.tourManager = tourManager;
            this.mapquest = mapquest;
            this.windowService = windowService;
            //tourManager = TourManagerFactory.GetInstance(); //create and pass in app-startup instead
            FillListBox();

            AddTourCommand = new RelayCommand(
                (O) => (!String.IsNullOrEmpty(NewTourName)) 
                    && (!String.IsNullOrEmpty(From)) 
                    && (!String.IsNullOrEmpty(To)),
                (O) => { AddTour(); }
            );

            DeleteTourCommand = new RelayCommand(
                (O) => SelectedItem != null, //&& !String.IsNullOrEmpty(SelectedItem.Name)
                (O) => { DeleteTour(SelectedItem); }
            );

            UpdateTourCommand = new RelayCommand(
               (O) => SelectedItem != null, // && !String.IsNullOrEmpty(SelectedItem.Name)
               (O) => { UpdateTour(); }
            );

            FillFormCommand = new RelayCommand(
                (O) => SelectedItem != null, //&& !String.IsNullOrEmpty(SelectedItem.Name)
                (O) => { FillForm(); currentlyEditing = true; }
            );

            EmptyFormCommand = new RelayCommand(
               (O) => { return true; }, // && !String.IsNullOrEmpty(SelectedItem.Name)
               (O) => { SetFormEmpty(); currentlyEditing = false; }
            );


            NewTourName = "";
        }


        public void AddTour()
        {
            //Debug.Print($"Adding tour {NewTourName}");
            try
            {

                var newTour = tourManager.AddTour(NewTourName, Description, From, To,
                    (Models.HelperEnums.TransportType)TransportType);
                //Tours.Add(newTour);
                FillListBox();
                SetFormEmpty();
                OnGetMap?.Invoke(this, newTour);
                //CallGetRouteAndGetImage(newTour);
            }
            catch (ArgumentException ex)
            {
                ILoggerWrapper logger = LoggerFactory.GetLogger();

                logger.Warn(" Could not AddTour, because of invalid user inputs!!!!");

                // TODO in ein INterface - im gleiches Layer legen. bei views. ImplInterface. DI regel zu machen. 
                //MessageBox.Show("Info", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                windowService.ShowMessageBox("Invalid User Input: Did you fill all fields correctly?");

            }
            //TourAdded?.Invoke(this, NewTourName);
        }

        public void UpdateTour()
        {/*
            currentlyEditing= false;
            SelectedItem.Name = newTourName;
            SelectedItem.Description = _description;
            SelectedItem.From = from;
            SelectedItem.To = to;
            SelectedItem.TransportType = (Models.HelperEnums.TransportType)_transportType;
            //SelectedItem.Distance = _distance;
            // ----- COmment //   CallGetRouteAndGetImage();
            tourManager.UpdateTour(SelectedItem);

            // laufen ab bevor getourte shcon fertig ist. 
            FillListBox();
            //  mapquest.GetImage()
            SetFormEmpty();*/

            try
            {
                currentlyEditing = false;
                SelectedItem.Name = newTourName;
                SelectedItem.Description = _description;
                SelectedItem.From = from;
                SelectedItem.To = to;
                SelectedItem.TransportType = (Models.HelperEnums.TransportType)_transportType;
                //SelectedItem.Distance = _distance;
                // ----- COmment //   CallGetRouteAndGetImage();
                tourManager.UpdateTour(SelectedItem);
                OnGetMap?.Invoke(this, SelectedItem);

                FillListBox();
                //SetFormEmpty();
                
                //CallGetRouteAndGetImage(newTour);
            }
            catch (ArgumentException ex)
            {
                ILoggerWrapper logger = LoggerFactory.GetLogger();

                logger.Warn(" Could not EditTour, because of invalid user inputs!!!!");
                windowService.ShowMessageBox("Invalid User Input: Did you fill all fields correctly?");
            }
        }

        /*
        private async Task CallGetRouteAndGetImage(Tour tour)
        {
            Route route = await mapquest.GetRoute(tour);

            // var route = task.Result;
            tour.Distance = route.distance; // ObjectRefernce not setted to an inst of an obkj
            tour.EstimatedTime = route.estimatedTime;
            tourManager.UpdateTour(tour);

            //
            //  RaisePropertyChangedEvent(nameof(SelectedItem));

            Stream awaitStream = await mapquest.GetImage(route);

            await using var filestream = new FileStream($"{tour.Name}{tour.Id}.png", FileMode.Create, FileAccess.Write);
            awaitStream.CopyTo(filestream);

        }*/




        public void DeleteTour(Tour tour)
        {
            //Debug.Print($"Deleting tour {item.Name}");

            tourManager.DeleteTour(tour);
            Tours.Remove(tour);
            // FillListBox();
        }

        

        public void FillListBox()
        {
            //todo?: remove clear
            //this is here right now to allow reading the whole list from the db after every change
            Tours.Clear();
            foreach (Tour tour in tourManager.GetTours())
            {
                Tours.Add(tour);
            }
        }

        public void SearchFor(string query)
        {
            IEnumerable foundItems = tourManager.Search(query);

            Tours.Clear();

            foreach (Tour tour in foundItems)
            {
                /*
                if (tour == null)
                {

                    throw new ArgumentNullException(nameof(tour));

                }*/
                Tours.Add(tour);
            }
        }

        private void FillForm()
        {
            NewTourName = SelectedItem.Name;
            Description= SelectedItem.Description;
            From = SelectedItem.From;
            To = SelectedItem.To;
            TransportType = SelectedItem.TransportType;
        }

        private void SetFormEmpty()
        {
            NewTourName = "";
            Description = "";
            From = "";
            To = "";
            TransportType = TransportType.Pedestrian;
            //Distance = 0;


        }
    }
}
