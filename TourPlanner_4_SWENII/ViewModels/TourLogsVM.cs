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
        public ObservableCollection<TourLog> TourLogs { get; set; }

        /*
        private string newTourName;
        public string NewTourName
        {
            get => newTourName;

            set
            {
                if (newTourName != value)
                {
                    newTourName = value;
                    this.RaisePropertyChangedEvent();
                    this.AddTourCommand.RaiseCanExecuteChanged();
                    Debug.Print($" changed newTourName to {value}");
                }
            }
        }*/
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

        public TourLogsVM() {
            tourManager = TourManagerFactory.GetInstance(); //create and pass in app-startup instead
            // InitTourLogList();

            TourLogs = new ObservableCollection<TourLog>() { 
                new TourLog(){timeNow=new DateTime(), Comment="Hello my good sir", Rating=4, TotalTime=1.5, Difficulty=Difficulty.Easy},
                new TourLog(){timeNow=new DateTime(), Comment="Hello", Rating=4, TotalTime=1.5, Difficulty=Difficulty.CanDoKidsToo},
                new TourLog(){timeNow=new DateTime(), Comment=":)", Rating=4, TotalTime=1.2, Difficulty=Difficulty.BetterTrainHardBefore}
            };

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
            Debug.WriteLine($"getting new tour logs");
            // tourLogs = 
            var result = tourManager.GetTourLogs(tourId);
            selectedTourId = tourId;
            Debug.WriteLine($"tourid: {tourId}");
            TourLogs.Clear();
            foreach ( var item in result )
            {
                Debug.WriteLine($"adding tour log {item}");
                TourLogs.Add( item );
            }
            Debug.WriteLine($"added new tour logs");
        }
        private void AddTourLog()
        {
            Debug.Print($"Adding new tour log");

            //notify mainVM to get tour id
            tourManager.AddTourLog(selectedTourId);
            //FillListBox();

            //NewTourName = "";
            //TourAdded?.Invoke(this, NewTourName);
        }

        private void EditTourLog(TourLog tourLog)
        {
            Debug.Print($"Editing tour log {tourLog.Id}");
            tourManager.UpdateTourLog(tourLog);
        }

        private void DeleteTourLog(TourLog tourLog)
        {
            Debug.Print($"Deleting tour log {tourLog.Id}");
            tourManager.DeleteTourLog(tourLog);
        }
    }
}
