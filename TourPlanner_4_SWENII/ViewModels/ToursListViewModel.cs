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

        private ITourItemManager tourManager;
        public ObservableCollection<TourItem> Items { get; set; }

        public ToursListViewModel()
        {
            this.tourManager = TourItemManagerFactory.GetInstance();
            InitListBox();
        }

        private TourItem _selecteditem;
        public TourItem SelectedItem
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

            Items = new ObservableCollection<TourItem>();
            // foreach (COLLECTION collection in COLLECTION)
            FillListBox();
            SelectedItem = Items.First();   
        }

        public void FillListBox()
        {
            foreach (TourItem item in this.tourManager.GetItems())
            {
                Items.Add(item);
            }
        }

        public void SearchFor(string query)
        {
            IEnumerable foundItems = tourManager.Search(query);
            Items.Clear();
            foreach (TourItem item in foundItems)
            {
                if (item == null)
                {

                    throw new ArgumentNullException(nameof(item));

                }
                Items.Add(item);

            }
        }
    }
}
