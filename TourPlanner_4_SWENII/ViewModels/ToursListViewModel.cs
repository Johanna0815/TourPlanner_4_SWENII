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

        private IMediaItemFactory mediaItemFactory;
        public ObservableCollection<MediaItem> Items { get; set; }

        public ToursListViewModel()
        {
            this.mediaItemFactory = MediaItemFactory.GetInstance();
            InitListBox();
        }


        private void InitListBox()
        {
            Items = new ObservableCollection<MediaItem>();
            // foreach (COLLECTION collection in COLLECTION)
            FillListBox();
        }

        public void FillListBox()
        {
            foreach (MediaItem item in this.mediaItemFactory.GetItems())
            {
                Items.Add(item);
            }
        }

        public void SearchFor(string query)
        {
            IEnumerable foundItems = mediaItemFactory.Search(query);
            Items.Clear();
            foreach (MediaItem item in foundItems)
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
