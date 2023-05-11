using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.ViewModels;
using TourPlanner_4_SWENII.Views;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class ToursListViewModel : ViewModelBase
    {
     
        private ITourManager tourManager;
        public ObservableCollection<Tour> Tours { get; set; } = new();

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
                    this.AddTourCommand.RaiseCanExecuteChanged();
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
                }
            }
        }

        private TransportType _transportType;
        public TransportType TransportType
        {
            get => _transportType;

            set
            {
                if (_transportType != value)
                {
                    _transportType = value;
                    this.RaisePropertyChangedEvent();
                    this.AddTourCommand.RaiseCanExecuteChanged();
                }
            }
        }

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
                }
            }
        }

        public RelayCommand AddTourCommand { get; set; }
        public RelayCommand DeleteTourCommand { get; set; }

        //public event EventHandler<string> TourAdded;

        public ToursListViewModel(ITourManager tourManager) //
        {
            this.tourManager = tourManager;
            //tourManager = TourManagerFactory.GetInstance(); //create and pass in app-startup instead
            FillListBox();

            AddTourCommand = new RelayCommand(
                (O) => !String.IsNullOrEmpty(NewTourName),
                (O) => { AddTour(); }
            );

            DeleteTourCommand = new RelayCommand(
                (O) => SelectedItem != null && !String.IsNullOrEmpty(SelectedItem.Name),
                (O) => { DeleteTour(SelectedItem); }
            );

            NewTourName = "";
        }


        private void AddTour()
        {
            //Debug.Print($"Adding tour {NewTourName}");

            var newTour = tourManager.AddTour(NewTourName,Description,From,To, (Models.HelperEnums.TransportType)TransportType,Distance);
            //Tours.Add(newTour);
            FillListBox();

            NewTourName = "";
            Description = "";
            From = "";
            To = "";
            TransportType = 0;
            Distance = 0;
            //TourAdded?.Invoke(this, NewTourName);
        }

        private void DeleteTour(Tour item)
        {
            //Debug.Print($"Deleting tour {item.Name}");

            //Tours.Remove(item);
            tourManager.DeleteTour(item);
            FillListBox();
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
                }
            }
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
    }
}
