using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.ViewModels;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class MediaFolderVM : ViewModelBase
    {

        private MediaItem currentItem;


        private RelayCommand searchCommand;
        private RelayCommand clearCommand;
        public ICommand SearchCommand => searchCommand ??= new RelayCommand(Search);

        public ICommand ClearCommand => clearCommand ??= new RelayCommand(Clear);
        public ObservableCollection<MediaItem> Items { get; set; }

        public MediaItem CurrentItem
        {
            get
            {
                return currentItem;
            }
            set
            {
                if ((currentItem != value) && (value != null))
                {
                    currentItem = value;
                    RaisePropertyChangedEvent(nameof(CurrentItem));
                }

            }
        }

        public MediaFolderVM()
        {
                
        }

        private void Search(object commandParameter)
        {
        }

       
        private void Clear(object commandParameter)
        {
        }
    }
}
