using System;

namespace TourPlanner_4_SWENII.BL
{
    public static class TourItemManagerFactory
    {
        private static ITourItemManager instance;

        public static ITourItemManager GetInstance()
        {
            if (instance == null)
            {
                instance = new TourItemManagerImpl();

            }

            return instance;
        }

    }
}
