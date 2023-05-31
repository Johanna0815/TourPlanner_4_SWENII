using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class TourLogsVM : ViewModelBase
    {
        private ITourManager tourManager;

        private int selectedTourId = 0;
        public ObservableCollection<TourLog> TourLogs { get; set; } = new();

        private TourLog _selecteditem;
        public TourLog SelectedItem
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

        public TourLogsVM(ITourManager tourManager) {
            this.tourManager = tourManager; //create and pass in app-startup
            // InitTourLogList();

            /*TourLogs = new ObservableCollection<TourLog>() { 
                new TourLog(){timeNow=new DateTime(), Comment="Hello my good sir", Rating=4, TotalTime=1.5, Difficulty=Difficulty.Easy},
                new TourLog(){timeNow=new DateTime(), Comment="Hello", Rating=4, TotalTime=1.5, Difficulty=Difficulty.CanDoKidsToo},
                new TourLog(){timeNow=new DateTime(), Comment=":)", Rating=4, TotalTime=1.2, Difficulty=Difficulty.BetterTrainHardBefore}
            };*/

            AddTourLogCommand = new RelayCommand(
                (O) => { return true; },
                (O) => { AddTourLog(); }
            );

            EditTourLogCommand = new RelayCommand(
                (O) => SelectedItem != null,
                (O) => { EditTourLog(SelectedItem); }
            );

            DeleteTourLogCommand = new RelayCommand(
                (O) => SelectedItem != null,
                (O) => { DeleteTourLog(SelectedItem); }
            );

            //NewTourName = "";
            selectedTourId = 0;
            GetTourLogs(selectedTourId);
        }

        public event EventHandler<string> TourLogAdded;

        public RelayCommand AddTourLogCommand { get; set; }
        public RelayCommand EditTourLogCommand { get; set; }
        public RelayCommand DeleteTourLogCommand { get; set; }

        public void GetTourLogs(int tourId)
        {
            //Debug.WriteLine($"getting new tour logs");
            // tourLogs = 

            // get tour logs from bl
            TourLogs.Clear();
            var result = tourManager.GetTourLogs(tourId);
            
            //Debug.WriteLine($"tourid: {tourId}");
            
            foreach ( var item in result ) //debug: already doubled here
            {
                Debug.WriteLine($"adding tour log {item}");
                TourLogs.Add( item );
            }
            //Debug.WriteLine($"added new tour logs");

            selectedTourId = tourId;
        }
        private void AddTourLog()
        {
            //Debug.Print($"Adding new tour log");
            //TourLogs.Add(new TourLog() { TourId = selectedTourId});

            //notify mainVM to get tour id
            TourLogs.Add(tourManager.AddTourLog(selectedTourId));
            //FillListBox();

            //NewTourName = "";
            //TourAdded?.Invoke(this, NewTourName);
        }

        private void EditTourLog(TourLog tourLog)
        {
            Debug.Print($"Editing tour log {tourLog.Id}");
            tourLog.Comment = $"edited on {DateTime.UtcNow}";
            tourManager.UpdateTourLog(tourLog);
        }

        private void DeleteTourLog(TourLog tourLog)
        {
            Debug.Print($"Deleting tour log {tourLog.Id}");
            tourManager.DeleteTourLog(tourLog);
        }
    }
}
