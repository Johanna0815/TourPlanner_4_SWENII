using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_4_SWENII.Utils.FileAndFolderHandling;

namespace TourPlanner_4_SWENII
{
    public class WindowServiceImpl : IWindowService
    {
        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public string ShowSelectFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            FolderCreator folderCreator = new FolderCreator();
            folderCreator.ToCreateDirectory(ConfigurationManager.AppSettings["ExportImportSubdir"]);
            openFileDialog.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), ConfigurationManager.AppSettings["ExportImportSubdir"]);
            Debug.WriteLine("currentDirectory: " + openFileDialog.InitialDirectory);
            openFileDialog.Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;
            else
                return "";
        }
    }
}
