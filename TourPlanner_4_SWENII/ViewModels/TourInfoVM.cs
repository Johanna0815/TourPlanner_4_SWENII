using iText.Layout.Element;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.Models;
using System.Windows.Controls;
using TourPlanner_4_SWENII.Views;
using System.Windows.Media;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class TourInfoVM : ViewModelBase
    {

        private ITourManager tourManager;
        public ObservableCollection<Tour> Tour { get; set; } = new();

        public System.Windows.Controls.Image testImage { get; set; } = new();
        string ImgSourceText = "C:\\Users\\Miriam\\Pictures\\biology.PNG";

        private Tour _selectedTour;
        private int _tourId;

        public TourInfoVM()
        {
            tourManager = TourManagerFactory.GetInstance();

            LoadImage(ImgSourceText);
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

          var tours  = tourManager.GetTours();

           Tour.Add(tours.Where(t => t.Id == tour_id).First());
           
           

        }

        public void LoadImage(string imagePath)
        {
            Debug.WriteLine($"current dirctory: {System.AppDomain.CurrentDomain.BaseDirectory}");

            // UriKind uriKind = UriKind.Absolute;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            //bitmap.UriSource = new Uri(ImgSourceText, uriKind);
            //bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            testImage.Stretch = Stretch.Fill;
            testImage.Source = bitmap;
        }

    }
}
