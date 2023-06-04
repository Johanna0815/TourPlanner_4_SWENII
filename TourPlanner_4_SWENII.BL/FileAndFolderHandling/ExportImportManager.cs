using System;
using System.IO;
using System.Text.Json;
using TourPlanner_4_SWENII.Models;


namespace TourPlanner_4_SWENII.Utils.FileAndFolderHandling

{
    public class ExportImportManager
    {
        public static void JsonToFile(object Object, string subPath, string name)
        {
            FolderCreator createDirectory = new FolderCreator();

            createDirectory.ToCreateDirectory(subPath);

            JsonSerializerOptions options = new JsonSerializerOptions();

            // for pretty printing. 
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(Object, typeof(Object), options);
            File.WriteAllText($"./{subPath}/{name}" , json);
        }

        //--------------------------- TO Deserialize again. // for the Import to the DB Button.
        public static Tour ImportTourFromFile(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Tour>(json);

        }

    }
}
