using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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

        private string newTourName;
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

        public RelayCommand AddTourCommand { get; set; }
        public RelayCommand DeleteTourCommand { get; set; }

        //public event EventHandler<string> TourAdded;

        public ToursListViewModel() //ITourManager tourmanager
        {
            //this.tourmanager = tourmanager
            tourManager = TourManagerFactory.GetInstance(); //create and pass in app-startup instead
            InitListBox();

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

            var newTour = tourManager.AddTour(NewTourName);
            //Tours.Add(newTour);
            FillListBox();

            NewTourName = "";
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

        private void InitListBox()
        {
            //Tours = new ObservableCollection<Tour>();

            // foreach (COLLECTION collection in COLLECTION)
            FillListBox();
            // SelectedItem = Tours.First();   
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
