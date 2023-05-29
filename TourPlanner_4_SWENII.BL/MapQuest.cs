﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using TourPlanner_4_SWENII.logging;
using TourPlanner_4_SWENII.Models;

namespace TourPlanner_4_SWENII.BL
{
    public class MapQuest
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();


        public async Task<Tour> GetMapQuest(Tour tour)
        {
            var key = "vp9wvjCQjHGcsdhQt6LZ1vqkgyZkOT5W";
            var from = tour.From;
            var to = tour.To;


            var url = $"https://www.mapquestapi.com/directions/v2/route?key={key}&from={from}&to={to}&unit=%20k";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            //Console.WriteLine(content);


            var rootNode = JsonNode.Parse(content);
            //  var rootNode = JsonDocument.Parse(content).RootElement;
            Console.WriteLine(rootNode?.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));

            if (rootNode["info"]["statuscode"].ToString() != "0")
            {
                Debug.WriteLine($"Error {rootNode["info"]["statuscode"]}: getting map failed");
                return tour;
            }
            var sessionId = rootNode["route"]["sessionId"].ToString();
            var boundingBox = rootNode["route"]["boundingBox"];
            // tour.Distance = rootNode["route"]["distance"].GetDecimal();

            //  tour.Distance = rootNode["route"]["distance"].GetValue<decimal>();

            try
            {

                var routeNode = rootNode["route"];
                var distanceNode = routeNode?["distance"];

                if (routeNode != null && distanceNode != null)
                {
                    tour.Distance = distanceNode.GetValue<decimal>();
                }
                else
                {

                    Debug.Print($"The Distance is {tour.Distance} long.");
                    tour.Distance = 0; // Setting a default value for Distance
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($" In {tour.Distance} occured the following exception: {ex}");
            }










            //  tour.Distance = rootNode["route"]["distance"].Deserialize<Decimal>();



            tour.EstimatedTime = rootNode["route"]["formattedTime"].Deserialize<TimeSpan>(); // datemnTyp DateTime ?


            logger.Debug($"{tour.EstimatedTime}");

            Debug.Print($"{tour.EstimatedTime}");
            //   Debug.Print($"hier ist die distance.{Distance}");



            // tour.EstimatedTime = .logging{""} // 
            //var ul_lng = boundingBox["ul"]["lng"].ToString();


            var ul_lat = boundingBox["ul"]["lat"].ToString();
            var ul_lng = boundingBox["ul"]["lng"].ToString();
            var lr_lat = boundingBox["lr"]["lat"].ToString();
            var lr_lng = boundingBox["lr"]["lng"].ToString();


            url = $"http://www.mapquestapi.com/staticmap/v5/map?key={key}&session={sessionId}&boundingBox={ul_lat},{ul_lng},{lr_lat},{lr_lng}&size=800,600";
            var stream = await client.GetStreamAsync(url);
            await using var filestream = new FileStream($"{tour.Name}{tour.Id}.png", FileMode.Create, FileAccess.Write);
            stream.CopyTo(filestream);



            return tour;
        }

        //public async void Image()
        //{



        //    await 


        //}




    }
}


