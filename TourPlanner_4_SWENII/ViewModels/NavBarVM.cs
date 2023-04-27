using iText.Kernel.Pdf;
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

        public NavBarVM()
        {

            this.GenerateReportCommand = new RelayCommand(
                (O) => { return true; },
                (O) => { GenerateReport?.Invoke(this, new EventArgs()); }
            );
        }

        public RelayCommand GenerateReportCommand { get; set; }

        /*
        private void GenerateReport()
        {
            

            var writer = new PdfWriter("report.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.Add(new Paragraph("Tour Report"));
            document.Close();
        }*/
    }
}
