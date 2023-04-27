using System;

namespace TourPlanner_4_SWENII.BL
{
    public static class TourManagerFactory
    {
        private static ITourManager instance;

        public static ITourManager GetInstance()
        {
            if (instance == null)
            {
                instance = new TourManagerImpl();

            }

            return instance;
        }

    }
}
