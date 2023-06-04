using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
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
    public class MapQuestImpl : IMapQuest
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();


        public async Task<Route> GetRoute(Tour tour)
        {
            string apiKey = ConfigurationManager.AppSettings["MapQuestAPIKey"];
            var from = tour.From;
            var to = tour.To;
            var TransportType = tour.TransportType;

            var url = $"https://www.mapquestapi.com/directions/v2/route?key={apiKey}&from={from}&to={to}&routeType={TransportType}&unit=k";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            //Parse Content
            var rootNode = JsonNode.Parse(content);

            if (rootNode["info"]["statuscode"].ToString() != "0")
            {
                Debug.WriteLine($"Error {rootNode["info"]["statuscode"]}: getting map failed");

                throw new Exception();  // TODO Mapquestexception

            }
            var sessionId = rootNode["route"]["sessionId"].ToString();
            var boundingBox = rootNode["route"]["boundingBox"];

            var routeNode = rootNode["route"];
            var distanceNode = routeNode?["distance"];
            decimal distance = 0;

            if (routeNode != null && distanceNode != null)
            {
                distance = distanceNode.GetValue<decimal>();
            }

            TimeSpan estimatedTime = rootNode["route"]["formattedTime"].Deserialize<TimeSpan>();

            
            var ul_lat = boundingBox["ul"]["lat"].ToString();
            var ul_lng = boundingBox["ul"]["lng"].ToString();
            var lr_lat = boundingBox["lr"]["lat"].ToString();
            var lr_lng = boundingBox["lr"]["lng"].ToString();


            Route route = new Route();
            route.distance = distance;
            route.sessionID = sessionId;
            route.estimatedTime = estimatedTime;
            route.ul_Latitude = ul_lat;
            route.ul_Longitude = ul_lng;
            route.lr_Latitude = lr_lat;
            route.lr_Longitude = lr_lng;

            return route;
        }

        public async Task<Stream> GetImage(Route route)
        {
            var key = ConfigurationManager.AppSettings["MapQuestAPIKey"];

            HttpClient client = new HttpClient();

            string url = $"http://www.mapquestapi.com/staticmap/v5/map?key={key}&session={route.sessionID}&boundingBox={route.ul_Latitude},{route.ul_Longitude},{route.lr_Latitude},{route.lr_Longitude}&size=1920,600";
            return await client.GetStreamAsync(url);

        }

    }
}


