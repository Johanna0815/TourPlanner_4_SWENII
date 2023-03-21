/*using System;
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
    public class MediaFolderVM : ViewModelBase
    {


       

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

  


        /// <summary>
        /// uses BL for this SearchMethod.
        /// </summary>
        /// <param name="commandParameter"></param>
        private void Search(object commandParameter)
        {
            IEnumerable foundItems = this.mediaItemFactory.Search(SearchName);
           Items.Clear() ;
           foreach (MediaItem item in foundItems)
           {
               Items.Add(item);
               
           }
        }

       
        private void Clear(object commandParameter)
        {

            Items.Clear();
            SearchName = "";

            FillListBox();
            //IEnumerable foundItems = this.mediaItemFactory.Search(SearchName);
            //Items.Clear();
            //foreach (MediaItem item in foundItems)
            //{
            //    Items.Add(item);

            //}


        }
    }
}
*/