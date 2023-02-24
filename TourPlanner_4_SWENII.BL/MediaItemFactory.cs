using System;

namespace TourPlanner_4_SWENII.BL
{
    public static class MediaItemFactory
    {
        private static IMediaItemFactory instance;

        public static IMediaItemFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new MediaItemFactoryImpl();

            }

            return instance;
        }

    }
}
