using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.ViewModels;


namespace TourPlanner_4_SWENII.ViewModels
{
    public class ToursListViewModel : ViewModelBase
    {

        private ITourManager tourManager;
        public ObservableCollection<Tour> Tours { get; set; }

        public ToursListViewModel()
        {
            this.tourManager = TourManagerFactory.GetInstance();
            InitListBox();
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

            Tours = new ObservableCollection<Tour>();
            // foreach (COLLECTION collection in COLLECTION)
            FillListBox();
            SelectedItem = Tours.First();   
        }

        public void FillListBox()
        {
            foreach (Tour tour in this.tourManager.GetTours())
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
                if (tour == null)
                {

                    throw new ArgumentNullException(nameof(tour));

                }
                Tours.Add(tour);

            }
        }
    }
}
