﻿using System;
using System.Diagnostics;
using System.IO;

namespace TourPlanner_4_SWENII.Utils.FileAndFolderHandling
{
    public class FolderCreator
    {

        public void ToCreateDirectory(string folderPath)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            //Debug.WriteLine("Current Directory: " + currentDirectory);

            // Create a new directory
            string newDirectory = Path.Combine(currentDirectory, folderPath);
            Debug.WriteLine($"newDirectory: {newDirectory}");
            try
            {
                if (!Directory.Exists(newDirectory))
                {

                    Directory.CreateDirectory(newDirectory);
                    Debug.WriteLine("New directory created successfully.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error creating directory: " + ex.Message);
            }

        }

    }

}
