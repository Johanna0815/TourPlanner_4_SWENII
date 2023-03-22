using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.BL;
using System.Collections.ObjectModel;
/*
namespace TourPlanner_4_SWENII.ViewModels
{
    public class SearchViewModel : ViewModelBase
    
    {
        private IMediaItemFactory mediaItemFactory;
        public ObservableCollection<MediaItem> Items { get; set; }

        private MediaItem currentItem;

        private string searchName;
        private RelayCommand searchCommand;
        private RelayCommand clearCommand;

        public SearchViewModel(IMediaItemFactory mediaItemFactory)
        {


            InitListBox();
            this.mediaItemFactory = mediaItemFactory;



        }



        public ICommand SearchCommand => searchCommand ??= new RelayCommand(Search);

        public ICommand ClearCommand => clearCommand ??= new RelayCommand(Clear);



        public MediaItem CurrentItem
        {
            get => currentItem;
            set
            {
                if ((currentItem != value) && (value != null))
                {
                    currentItem = value;
                    RaisePropertyChangedEvent(nameof(CurrentItem));
                }

            }
        }

        public string SearchName
        {
            get => searchName;
            set
            {
                if (searchName != value)
                {
                    searchName = value;
                    RaisePropertyChangedEvent(nameof(SearchName));
                }
            }
        }


        private void Search(object commandParameter)
        {
            IEnumerable foundItems = mediaItemFactory.Search(SearchName);
            Items.Clear();
            foreach (MediaItem item in foundItems)
            {
                if(item == null)
                {

                    throw  new ArgumentNullException(nameof(item)); 

                }
                Items.Add(item);

            }
        }

        private void FillListBox()
        {
            foreach (MediaItem item in this.mediaItemFactory.GetItems())
            {
                Items.Add(item);
            }
        }

        private void InitListBox()
        {
            Items = new ObservableCollection<MediaItem>();
            // foreach (COLLECTION collection in COLLECTION)
            FillListBox();
        }





        private void Clear(object commandParameter)
        {

            Items.Clear();
            SearchName = "";

            FillListBox();


        }

    }
}
*/