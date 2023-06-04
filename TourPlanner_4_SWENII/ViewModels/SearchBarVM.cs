using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class SearchBarVM : ViewModelBase
    {
        public SearchBarVM()
        {
            this.ClearCommand = new RelayCommand(
                (O) => { return true; }, //1.Paramater canexecute //!String.IsNullOrEmpty(SearchText)
                (O) => { Clear(); }//2.p execute
            );

            this.SearchCommand = new RelayCommand(
                (O) => !String.IsNullOrEmpty(SearchText),
                (O) => { Search(); }
            );

            SearchText = "First";
        }

        public RelayCommand ClearCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }

        public event EventHandler<string> SearchCleared;
        public event EventHandler<SearchParameters> SearchForText;

        private string searchtext;
        public string SearchText
        {
            get => searchtext;

            set
            {
                if (searchtext != value)
                {
                    searchtext = value;
                    this.RaisePropertyChangedEvent();
                    this.ClearCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private bool _caseSensitive = false;
        public bool CaseSensitive
        {
            get => _caseSensitive;
            set
            {
                if (_caseSensitive != value)
                {
                    _caseSensitive = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private bool _searchInTourLogs = false;
        public bool SearchInTourLogs
        {
            get => _searchInTourLogs;
            set
            {
                if (_searchInTourLogs != value)
                {
                    _searchInTourLogs = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private void Search()
        {
            Debug.Print($"Searching for text {SearchText}");
            SearchParameters searchParams = new SearchParameters();
            searchParams.searchText = SearchText;
            searchParams.caseSensitive = CaseSensitive;
            searchParams.searchInTourLogs = SearchInTourLogs;
            SearchForText?.Invoke(this, searchParams);
        }

        private void Clear()    //object commandParameter
        {
            Debug.Print("Text Cleared");
            SearchText = "";

            SearchCleared?.Invoke(this, SearchText);
        }
    }
}
