using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Models.HelperEnums;
using TourPlanner_4_SWENII.Views;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class TourLogsVM : ViewModelBase
    {
        private ITourManager tourManager;
        public event EventHandler<string> TourLogAdded;
        public ObservableCollection<TourLog> TourLogs { get; set; } = new();

        public Dictionary<Difficulty, string> DifficultyWithCaptions { get; } =
        new Dictionary<Difficulty, string>()
        {
            {Difficulty.Easy, "Easy"},
            {Difficulty.Medium, "Medium" },
            {Difficulty.Hard, "Hard"},
            {Difficulty.Expert, "Expert"},
            {Difficulty.None, "keine Angabe"},
        };

        public Dictionary<Rating, string> RatingWithCaptions { get; } =
        new Dictionary<Rating, string>()
        {
            {Rating.VeryGood, "Incredible"},
            {Rating.Good, "Enjoyable"},
            {Rating.Okay, "Okay"},
            {Rating.Bad, "Not that good"},
            {Rating.VeryBad, "Awful"},
            {Rating.None, "keine Angabe"},
        };

        private int selectedTourId = 0;
        public int SelectedTourId
        {
            get => selectedTourId;
            set
            {
                if (selectedTourId != value)
                {
                    selectedTourId= value;
                    RaisePropertyChangedEvent();
                    AddTourLogCommand.RaiseCanExecuteChanged();
                }
            }
        }

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
                    FillFieldsCommand.RaiseCanExecuteChanged();
                    EditTourLogCommand.RaiseCanExecuteChanged();
                    DeleteTourLogCommand.RaiseCanExecuteChanged();
                }
            }
        }

        //used to fill values for a new Tour
        private TourLog _newTourLog;
        public TourLog NewTourLog
        {
            get => _newTourLog;
            set
            {
                if (value != _newTourLog)
                    _newTourLog = value;
                RaisePropertyChangedEvent();
            }
        }

        private Difficulty _difficultyToEdit = Difficulty.None;
        public Difficulty DifficultyToEdit
        {
            get => _difficultyToEdit;
            set
            {
                if(_difficultyToEdit != value)
                {
                    _difficultyToEdit = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private string _commentToEdit = "";
        public string CommentToEdit
        {
            get => _commentToEdit;
            set
            {
                if (_commentToEdit != value)
                {
                    _commentToEdit = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private Rating _ratingToEdit = Rating.None;
        public Rating RatingToEdit
        {
            get => _ratingToEdit;
            set
            {
                if (_ratingToEdit != value)
                {
                    _ratingToEdit = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private TimeSpan _totalTimeToEdit = TimeSpan.Zero;
        public TimeSpan TotalTimeToEdit
        {
            get => _totalTimeToEdit;
            set
            {
                if (_totalTimeToEdit != value)
                {
                    _totalTimeToEdit = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private DateTime _dateExecutedToEdit = DateTime.Now;
        public DateTime DateExecutedToEdit
        {
            get => _dateExecutedToEdit;
            set
            {
                if (_dateExecutedToEdit != value)
                {
                    _dateExecutedToEdit = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        //public TourLog TourLogToEdit { get; set; } = new() { Comment = "new log" };



        /*
        private ExampleEnum example;
        public ExampleEnum ExampleProperty
        {
            get { return example; }
            set { // set and notify; }
        }*/


        public TourLogsVM(ITourManager tourManager)
        {
            this.tourManager = tourManager; //create and pass in app-startup
            // InitTourLogList();

            /*TourLogs = new ObservableCollection<TourLog>() { 
                new TourLog(){timeNow=new DateTime(), Comment="Hello my good sir", Rating=4, TotalTime=1.5, Difficulty=Difficulty.Easy},
                new TourLog(){timeNow=new DateTime(), Comment="Hello", Rating=4, TotalTime=1.5, Difficulty=Difficulty.CanDoKidsToo},
                new TourLog(){timeNow=new DateTime(), Comment=":)", Rating=4, TotalTime=1.2, Difficulty=Difficulty.BetterTrainHardBefore}
            };*/

            AddTourLogCommand = new RelayCommand(
                (O) => selectedTourId != 0,
                (O) => { AddTourLog(); });

            FillFieldsCommand = new RelayCommand(
                (O) => SelectedItem != null,
                (O) => { CopyTourLogPropertiesFromTo(SelectedItem, NewTourLog); });

            EmptyFieldsCommand = new RelayCommand(
                (O) => { return true; },
                (O) => { EmptyFields(); });

            EditTourLogCommand = new RelayCommand(
                (O) => SelectedItem != null,
                (O) => { EditTourLog(SelectedItem); });

            DeleteTourLogCommand = new RelayCommand(
                (O) => SelectedItem != null,
                (O) => { DeleteTourLog(SelectedItem); });

            //NewTourName = "";
            selectedTourId = 0;
            NewTourLog = new() { Comment = "new log" };
            GetTourLogs(selectedTourId);
        }



        public RelayCommand AddTourLogCommand { get; set; }
        public RelayCommand FillFieldsCommand { get; set; }
        public RelayCommand EmptyFieldsCommand { get; set; }
        public RelayCommand EditTourLogCommand { get; set; }
        public RelayCommand DeleteTourLogCommand { get; set; }



        public void GetTourLogs(int tourId)
        {
            TourLogs.Clear();
            if (tourId > 0)
            {
                // get tour logs from bl
                var result = tourManager.GetTourLogs(tourId);
                foreach (var item in result)
                {
                    Debug.WriteLine($"adding tour log {item}");
                    TourLogs.Add(item);
                    //RaisePropertyChangedEvent("TourLogs");
                }
            }
            //todo
            SelectedTourId = tourId;
        }
        private void AddTourLog()
        {
            EmptyFields();
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
            Debug.WriteLine($"3 {NewTourLog.Comment}");
            Debug.Print($"Editing tour log {tourLog.Id}");
            tourLog.Comment = NewTourLog.Comment; //NewTourLog.Comment;//$"edited on {DateTime.UtcNow}"
            tourManager.UpdateTourLog(tourLog);
            //CopyTourLogPropertiesFromTo(tourLog, TourLogs.Where(t => t.Id == tourLog.Id).First());
            //TourLogs.Where(t => t.Id== tourLog.Id).First().

            //Update View
            GetTourLogs(selectedTourId);
            //CopyTourLogPropertiesFromTo()
            //SelectedItem = NewTourLog; //to hopefully update the view

        }
        private void DeleteTourLog(TourLog tourLog)
        {
            Debug.Print($"Deleting tour log {tourLog.Id}");
            tourManager.DeleteTourLog(tourLog);
            TourLogs.Remove(tourLog);
            //tourManager.GetTourLogs(selectedTourId);
        }
        private void FillFields()
        {
            Debug.WriteLine("fill fields was called");
            NewTourLog.Rating = SelectedItem.Rating;
            NewTourLog.TotalTime = SelectedItem.TotalTime;
            NewTourLog.TimeNow = SelectedItem.TimeNow;
            NewTourLog.Comment = SelectedItem.Comment;
            NewTourLog.Difficulty = SelectedItem.Difficulty;
        }
        private void EmptyFields()
        {
            Debug.WriteLine("empty fields was called");
            NewTourLog = new TourLog() {
                Rating = 0,
                TotalTime = TimeSpan.Zero,
                TimeNow = DateTime.Now,
                Comment = "",
                Difficulty = Difficulty.None
            };
            
        }

        private void CopyTourLogPropertiesFromTo(TourLog refTourLog, TourLog destTourLog)
        {

            Debug.WriteLine($"1 {destTourLog.Comment}");
            NewTourLog = new TourLog()
            {
                Rating = refTourLog.Rating,
                Comment = refTourLog.Comment,
                Difficulty = refTourLog.Difficulty,
                TimeNow = refTourLog.TimeNow,
                TotalTime = refTourLog.TotalTime,
            };
            /*
            destTourLog.Rating = refTourLog.Rating;
            destTourLog.TotalTime = refTourLog.TotalTime;
            destTourLog.TimeNow = refTourLog.TimeNow;
            destTourLog.Comment = refTourLog.Comment;
            destTourLog.Difficulty = refTourLog.Difficulty;
            */
            Debug.WriteLine($"2 {destTourLog.Comment}");
        }

    }
}
