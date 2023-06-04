using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media.Imaging;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.Models;
using System.Windows.Media;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class TourInfoVM : ViewModelBase
    {

        private ITourManager tourManager;

        public System.Windows.Controls.Image testImage { get; set; } = new();

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
            //Tour.Clear();
            SelectedTour = new();
            if(tour_id > 0)
            {
                var tours = tourManager.GetTours();

                //Tour.Add(tours.Where(t => t.Id == tour_id).First());
                SelectedTour = (tours.Where(t => t.Id == tour_id).First());

                LoadImage($"{SelectedTour.Name}{SelectedTour.Id}.png");
            }
        }

        public void LoadImage(string imagePath)
        {
            try
            {
                testImage.Source = null;

                Debug.WriteLine($"current dirctory: {System.AppDomain.CurrentDomain.BaseDirectory}");


                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();

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
