﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_4_SWENII.logging
{
   
        public static class LoggerFactory
        {
            public static ILoggerWrapper GetLogger()
            {
                return Log4NetWrapper.CreateLogger("./log4net.config");
            }
        }
    

}
