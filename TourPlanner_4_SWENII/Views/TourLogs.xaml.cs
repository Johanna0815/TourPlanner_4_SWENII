using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TourPlanner_4_SWENII.Views
{
    /// <summary>
    /// Interaction logic for TourLogs.xaml
    /// </summary>
    public partial class TourLogs : UserControl
    {
        public TourLogs()
        {
            InitializeComponent();
        }

        public void Add(object sender, RoutedEventArgs e)
        {
            if (TourNamePrompt.Visibility == Visibility.Visible)
            {
                TourNamePrompt.Visibility = Visibility.Collapsed;
            }
            else
            {
                TourNamePrompt.Visibility = Visibility.Visible;
            }

            RemoveTourPrompt.Visibility = Visibility.Collapsed;
        }

        public void Remove(object sender, RoutedEventArgs e)
        {
            if (RemoveTourPrompt.Visibility == Visibility.Visible)
            {
                RemoveTourPrompt.Visibility = Visibility.Collapsed;
            }
            else
            {
                RemoveTourPrompt.Visibility = Visibility.Visible;
            }

            TourNamePrompt.Visibility = Visibility.Collapsed;
        }

        public void Edit(object sender, RoutedEventArgs e)
        {
        }
    }
}
