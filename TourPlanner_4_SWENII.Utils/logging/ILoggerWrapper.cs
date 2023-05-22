namespace TourPlanner_4_SWENII.logging
{
    public interface ILoggerWrapper
    {
        void Debug(string message);
        void Error(string message);
        void Fatal(string message);
        void Warn(string message);
    }
}

//to use with: private static ILoggerWrapper logger = LoggerFactory.GetLogger();