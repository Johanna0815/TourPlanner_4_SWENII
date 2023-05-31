﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using iText.StyledXmlParser.Jsoup.Select;
//using TourPlanner_4_SWENII.Models;

using System.Text.Json;
using System.Text.Json.Serialization;




namespace TourPlanner_4_SWENII.Utils.FileAndFolderHandling

{
    public class ExportFile
    {



        //Tour tourTest = new Tour()
        //{
        //    Id = 1,

        //    Name = "DummyDaten 1",
        //    //TimeSpan = DateTime.Now,
        //    // Tags = new List<string>() { "MeetingBoard", "MeetingBoard II", "MeetingBoard anotherOne", "Project Stardust Team Meeting", "Meeting Board III" },
        //    From = "Here",
        //    To = "There"
        //};

        //public
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


        

//--------------------------- TO Deserialize again.
    //static Tour ConfigReader(string path)
    //    {

    //        string json = File.ReadAllText(path);
    //        return JsonSerializer.Deserialize<Tour>(json);

    //    }



    }
}