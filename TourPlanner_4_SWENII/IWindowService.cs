using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_4_SWENII
{
    public interface IWindowService
    {
        public string ShowSelectFileDialog();
        public void ShowMessageBox(string message);
    }
}
