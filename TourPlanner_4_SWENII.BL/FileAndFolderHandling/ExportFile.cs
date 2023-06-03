using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using TourPlanner_4_SWENII.Models;

using System.Text.Json;
using System.Text.Json.Serialization;
using TourPlanner_4_SWENII.Models;


namespace TourPlanner_4_SWENII.Utils.FileAndFolderHandling

{
    public class ExportFile
    {
        public static void JsonToFile(object Object, string path, string name)
        {

            FolderCreator createDirectory = new FolderCreator();
           
            
            
            createDirectory.ToCreateDirectory(path);
            

            

            JsonSerializerOptions options = new JsonSerializerOptions();
            // for pretty printing. 
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(Object, typeof(Object), options);
            File.WriteAllText($"./{path}/{name}" , json);

        }



        // generates json file in this project under the name tourTest.json
        // ____________________    JsonToFile(tourTest, "config.json"); _______________________________________


        //--------------------------- TO Deserialize again.
        //Tour configFromFile = ConfigReader("tourTest.json");
        //Console.WriteLine(configFromFile.Name);




        //--------------------------- TO Deserialize again. // for the Import to the DB Button.
        public static Tour ImportTourFromFile(string path)
        {
            // Auch ein interface wie für die error... 
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Tour>(json);

        }



    }
}
