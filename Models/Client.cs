//using System;
//using System.CodeDom;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Media.Animation;

//namespace TourPlanner_4_SWENII.Models
//{
//    public class Client
//    {
//        private static Client clientID;

//        public string Client_ID { get; set; }



//        public Client()
//        {

//        }


//        /// <summary>
//        /// the user can create new tours (no user management, login, registration... everybody sees all 
//        ///tours)
//        /// </summary>
//        /// <param name="name"></param>
//        /// <param name="clientId"></param>
//        /// <returns>        // should just return a special Tour; regarding to an ID. </returns>
//        public static Client ShowTour(Tour name, string clientId)
//        {
//            Client name_clientId;

//            if (name != null && clientId != null) 
//            {
//                name_clientId = new Client { Client_ID = "1" }; // db connection necessary here! 
//            }
//            else
//            {
//                Console.WriteLine("You not permitted to create a tour.; nothing return. ");
//                return null;
//            }
           
//            //return !(!name || !clientId);
//            return name_clientId;
//        }




//        public Tour CreateTourWhenSeeIt(Tour name)
//        {

//            // should return the created TOur
//            return new Tour();
//        }


//        //public static string EditTour(string deleter)
//        //{
            
//        //}


//    }
//}
