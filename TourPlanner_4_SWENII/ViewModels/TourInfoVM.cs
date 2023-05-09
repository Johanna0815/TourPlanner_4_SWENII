using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Views;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class TourInfoVM : ViewModelBase
    {

        private ITourManager tourManager;
        public ObservableCollection<Tour> Tour { get; set; } = new ();

         

        private Tour _selectedTour;
        //private int _tourId;

        public TourInfoVM()
        {
            tourManager = TourManagerFactory.GetInstance();
            
        }

        public Tour SelectedTour
        {
            get => _selectedTour;

            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;

                   

                    RaisePropertyChangedEvent();

                }
            }
        }

        // Get the selected Tour 
       public void GetTour(int tour_id)
        {
            Tour.Clear();

            var tours = tourManager.GetTours();

            SelectedTour = tours.First(t => t.Id == tour_id);

        }
       
    }
}
