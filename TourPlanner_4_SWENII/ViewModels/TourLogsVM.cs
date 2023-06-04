using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.Models;
using TourPlanner_4_SWENII.Models.HelperEnums;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class TourLogsVM : ViewModelBase
    {
        private ITourManager tourManager;
        //public event EventHandler<string> TourLogAdded;
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

        private DateTime _dateToEdit = DateTime.Now;
        public DateTime DateToEdit
        {
            get => _dateToEdit;
            set
            {
                if (_dateToEdit != value)
                {
                    _dateToEdit = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        public TourLogsVM(ITourManager tourManager)
        {
            this.tourManager = tourManager; //create and pass in app-startup

            AddTourLogCommand = new RelayCommand(
                (O) => selectedTourId != 0,
                (O) => { AddTourLog(); });

            FillFieldsCommand = new RelayCommand(
                (O) => SelectedItem != null,
                (O) => { FillFieldsToEdit(); });

            EmptyFieldsCommand = new RelayCommand(
                (O) => { return true; },
                (O) => { EmptyFields(); });

            EditTourLogCommand = new RelayCommand(
                (O) => SelectedItem != null,
                (O) => { EditTourLog(); });

            DeleteTourLogCommand = new RelayCommand(
                (O) => SelectedItem != null,
                (O) => { DeleteTourLog(SelectedItem); });

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
                }
            }
            SelectedTourId = tourId;
        }
        private void AddTourLog()
        {
            EmptyFields();
            TourLogs.Add(tourManager.AddTourLog(selectedTourId));
        }
        private void EditTourLog()
        {
            Debug.Print($"Editing tour log {SelectedItem.Id}");

            SelectedItem.TimeNow = DateToEdit.ToUniversalTime();
            SelectedItem.TotalTime = TotalTimeToEdit;
            SelectedItem.Comment = CommentToEdit;
            SelectedItem.Rating = RatingToEdit;
            SelectedItem.Difficulty = DifficultyToEdit;
            tourManager.UpdateTourLog(SelectedItem);

            //Update View
            GetTourLogs(selectedTourId);

        }
        private void DeleteTourLog(TourLog tourLog)
        {
            Debug.Print($"Deleting tour log {tourLog.Id}");
            tourManager.DeleteTourLog(tourLog);
            TourLogs.Remove(tourLog);
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

        //used to fill values for a new Tour
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
        private void FillFieldsToEdit()
        {
            DateToEdit = SelectedItem.TimeNow;
            TotalTimeToEdit = SelectedItem.TotalTime;
            CommentToEdit= SelectedItem.Comment;
            DifficultyToEdit = SelectedItem.Difficulty;
            RatingToEdit = SelectedItem.Rating;
        }
    }
}
