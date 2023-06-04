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
using System.Windows;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class TourInfoVM : ViewModelBase
    {

        private ITourManager tourManager;
        //public ObservableCollection<Tour> Tour { get; set; } = new ();

        public System.Windows.Controls.Image testImage { get; set; } = new();
        //string ImgSourceText = string.Empty;

        
        //private int _tourId;

        public TourInfoVM(ITourManager tourManager)
        {
            this.tourManager = tourManager;    
        }

        private Tour _selectedTour;
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
            SelectedTour = new();
            if(tour_id > 0)
            {
                var tours = tourManager.GetTours();

                SelectedTour = (tours.Where(t => t.Id == tour_id).First());
                //SelectedTour = tours.First(t => t.Id == tour_id);

                LoadImage($"{SelectedTour.Name}{SelectedTour.Id}.png");
            }
        }
        /*
        public void GetTour(int tour_id)
        {
            Tour.Clear();
            if (tour_id > 0)
            {
                var tours = tourManager.GetTours();

                Tour.Add(tours.Where(t => t.Id == tour_id).First());
                SelectedTour = tours.First(t => t.Id == tour_id);

                LoadImage($"{Tour[0].Name}{Tour[0].Id}.png");
            }
        }
        */
        public void LoadImage(string imagePath)
        {
            try
            {
                testImage.Source = null;

                //imagePath = "C:\\Users\\Miriam\\Pictures\\random stuff to sort\\Capture (2).PNG";
                //imagePath = "/TourPlanner_4_SWENII;component/Capture (2).PNG";

                Debug.WriteLine($"current dirctory: {System.AppDomain.CurrentDomain.BaseDirectory}");


                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();

                //UriKind uriKind = UriKind.Absolute;
                UriKind uriKind = UriKind.Relative;

                bitmap.UriSource = new Uri(imagePath, uriKind);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;

                bitmap.EndInit();

                testImage.Stretch = Stretch.Fill;
                testImage.Source = bitmap;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Debug.WriteLine("Picture does not exist error: ");
                Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unknown error:");
                Debug.WriteLine(ex.Message);
            }
        }

    }
}
