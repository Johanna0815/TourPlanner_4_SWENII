namespace TourPlanner_4_SWENII.logging
{
    public interface ILoggerWrapper
    {
        void Debug(string message);
        void Error(string message);
        void Fatal(string message);
        void Warn(string message);

        /// <summary>
        /// in case a person needs to subscribe/get an Info about the Tour X AGAIN
        /// </summary>
        /// <param name="message"></param>
        void Info_Notice(string message);
    }
}

//to use with: private static ILoggerWrapper logger = LoggerFactory.GetLogger();