﻿using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class NavBarVM : ViewModelBase
    {
        public event EventHandler GenerateReport;
        public event EventHandler GenerateTourLogsReport;
        public event EventHandler GetMap;

        public event EventHandler OnExportTour;
        public event EventHandler OnImportTour;

        public NavBarVM()
        {

             this.GenerateReportCommand = new RelayCommand(
                (O) => { return true; },
                (O) => { GenerateReport?.Invoke(this, new EventArgs()); }
             );

             this.GenerateTourLogsReportCommand = new RelayCommand(
                 (O) => { return true; },
                 (O) => { GenerateTourLogsReport?.Invoke(this, new EventArgs()); }
             );

             this.ExportTourCommand = new RelayCommand(
                (O) => { return true; },
                (O) => { OnExportTour?.Invoke(this, new EventArgs()); }
            );

             this.ImportTourCommand = new RelayCommand(
                (O) => { return true; },
                (O) => { OnImportTour?.Invoke(this, new EventArgs()); }
            );

            GetMapCommand = new RelayCommand(
                (O) => { return true; },
                (O) => { GetMap?.Invoke(this, new EventArgs()); }
            );
        }

        public RelayCommand GenerateReportCommand { get; set; }
        public RelayCommand GenerateTourLogsReportCommand { get; set; }
        public RelayCommand GetMapCommand { get; set; }

        public RelayCommand ExportTourCommand { get; set; }
        public RelayCommand ImportTourCommand { get; set; }

    }
}
