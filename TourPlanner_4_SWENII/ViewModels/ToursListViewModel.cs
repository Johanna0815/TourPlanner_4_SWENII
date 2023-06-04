using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.logging;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Models.HelperEnums;

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

        public RelayCommand AddTourCommand { get; set; }
        public RelayCommand DeleteTourCommand { get; set; }
        public RelayCommand UpdateTourCommand { get; set; }
        public RelayCommand FillFormCommand { get; set; }
        public RelayCommand EmptyFormCommand { get; set; }

        public ToursListViewModel(ITourManager tourManager, IMapQuest mapquest, IWindowService windowService) 
        {
            this.tourManager = tourManager;
            this.mapquest = mapquest;
            this.windowService = windowService;
            FillListBox();

            AddTourCommand = new RelayCommand(
                (O) => (!String.IsNullOrEmpty(NewTourName)) 
                    && (!String.IsNullOrEmpty(From)) 
                    && (!String.IsNullOrEmpty(To)),
                (O) => { AddTour(); }
            );

            DeleteTourCommand = new RelayCommand(
                (O) => SelectedItem != null,
                (O) => { DeleteTour(SelectedItem); }
            );

            UpdateTourCommand = new RelayCommand(
               (O) => SelectedItem != null,
               (O) => { UpdateTour(); }
            );

            FillFormCommand = new RelayCommand(
                (O) => SelectedItem != null,
                (O) => { FillForm(); currentlyEditing = true; }
            );

            EmptyFormCommand = new RelayCommand(
               (O) => { return true; },
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
            }
            catch (ArgumentException ex)
            {
                ILoggerWrapper logger = LoggerFactory.GetLogger();

                logger.Warn(" Could not AddTour, because of invalid user inputs!!!!");

                //MessageBox.Show("Info", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                windowService.ShowMessageBox("Invalid User Input: Did you fill all fields correctly?");

            }
        }

        public void UpdateTour()
        {
            try
            {
                currentlyEditing = false;
                SelectedItem.Name = newTourName;
                SelectedItem.Description = _description;
                SelectedItem.From = from;
                SelectedItem.To = to;
                SelectedItem.TransportType = (Models.HelperEnums.TransportType)_transportType;

                tourManager.UpdateTour(SelectedItem);
                OnGetMap?.Invoke(this, SelectedItem);   //CallGetRouteAndGetImage();

                FillListBox();
                //SetFormEmpty();
            }
            catch (ArgumentException ex)
            {
                ILoggerWrapper logger = LoggerFactory.GetLogger();

                logger.Warn(" Could not EditTour, because of invalid user inputs!!!!");
                windowService.ShowMessageBox("Invalid User Input: Did you fill all fields correctly?");
            }
        }

        public void DeleteTour(Tour tour)
        {
            //Debug.Print($"Deleting tour {item.Name}");

            tourManager.DeleteTour(tour);
            Tours.Remove(tour);
        }

        

        public void FillListBox()
        {
            Tours.Clear();  // TODO: change to sth less extreme ?
            foreach (Tour tour in tourManager.GetTours())
            {
                Tours.Add(tour);
            }
        }

        public void SearchFor(SearchParameters searchParams)
        {
            var result = tourManager.Search(searchParams);
            List<Tour> foundItems = new List<Tour>();
            foreach (Tour tour in result)
            {
                foundItems.Add(tour);
            }

            Tours.Clear();

            foreach (Tour tour in foundItems)
            {
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
        }
    }
}
