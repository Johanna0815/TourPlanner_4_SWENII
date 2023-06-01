using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
     
        private  ITourManager tourManager;
        private IMapQuest mapquest;
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
                    this.AddTourCommand.RaiseCanExecuteChanged();
                    this.UpdateTourCommand.RaiseCanExecuteChanged();
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
                    this.UpdateTourCommand.RaiseCanExecuteChanged();
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
        //public event EventHandler<string> TourAdded;

        public ToursListViewModel(ITourManager tourManager, IMapQuest mapquest) //
        {
            this.tourManager = tourManager;
            this.mapquest = mapquest;
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

            UpdateTourCommand = new RelayCommand(
               (O) => SelectedItem != null && !String.IsNullOrEmpty(SelectedItem.Name),
               (O) => { UpdateTour(); }
           );







            NewTourName = "";
        }


        public void AddTour()
        {
            //Debug.Print($"Adding tour {NewTourName}");

            var newTour = tourManager.AddTour(NewTourName,Description,From,To, (Models.HelperEnums.TransportType)TransportType);
            //Tours.Add(newTour);
            FillListBox();
            SetFormEmpty();
           CallGetRouteAndGetImage(newTour);
            //TourAdded?.Invoke(this, NewTourName);
        }

        public void UpdateTour()
        {
            SelectedItem.Name = newTourName;
            SelectedItem.Description = _description;
            SelectedItem.From = from;
            SelectedItem.To = to;
            SelectedItem.TransportType = (Models.HelperEnums.TransportType)_transportType;
            //SelectedItem.Distance = _distance;
          // ----- COmment //   CallGetRouteAndGetImage();
            tourManager.UpdateTour(SelectedItem);
          
   // laufen ab bevor getourte shcon fertig ist. 
            //(SelectedItem);
            FillListBox();
       //  mapquest.GetImage()
            SetFormEmpty();

        }

        private async Task CallGetRouteAndGetImage(Tour tour)
        {



            Route route = await mapquest.GetRoute(tour);
            
               // var route = task.Result;
                tour.Distance = route.distance; // ObjectRefernce not setted to an inst of an obkj
                tour.EstimatedTime = route.estimatedTime;
            //
            //  RaisePropertyChangedEvent(nameof(SelectedItem));

            Stream awaitStream = await  mapquest.GetImage(route);

            await using var filestream = new FileStream($"{tour.Name}{tour.Id}.png", FileMode.Create, FileAccess.Write);
            awaitStream.CopyTo(filestream);



        }




        public void DeleteTour(Tour tour)
        {
            //Debug.Print($"Deleting tour {item.Name}");

            tourManager.DeleteTour(tour);
            Tours.Remove(tour);
           // FillListBox();
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

                   
                    this.RaisePropertyChangedEvent();
                    this.UpdateTourCommand.RaiseCanExecuteChanged();

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

        private void SetFormEmpty()
        {
            NewTourName = "";
            Description = "";
            From = "";
            To = "";
            TransportType = 0;
            //Distance = 0;


        }
    }
}
