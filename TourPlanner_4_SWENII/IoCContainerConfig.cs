using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_4_SWENII.BL;
using TourPlanner_4_SWENII.DAL;
using TourPlanner_4_SWENII.ViewModels;

namespace TourPlanner_4_SWENII
{
    public class IoCContainerConfig
    {
        private readonly IServiceProvider serviceProvider;

        public IoCContainerConfig()
        {
            var services = new ServiceCollection();

            // create all layers
            services.AddSingleton<IDataHandler, DataHandlerEF>();
            services.AddSingleton<ITourManager, TourManagerImpl>();

            // create all viewmodels
            services.AddSingleton<NavBarVM>();
            services.AddSingleton<SearchBarVM>();
            services.AddSingleton<TourInfoVM>();
            services.AddSingleton<TourLogsVM>();
            services.AddSingleton<ToursListViewModel>();
            services.AddSingleton<MainViewModel>();

            serviceProvider = services.BuildServiceProvider();
        }

        public MainViewModel MainViewModel
            => serviceProvider.GetRequiredService<MainViewModel>();

        public NavBarVM NavBarVM
            => serviceProvider.GetRequiredService<NavBarVM>();

        public SearchBarVM SearchBarVM
            => serviceProvider.GetRequiredService<SearchBarVM>();

        public TourInfoVM TourInfoVM
            => serviceProvider.GetRequiredService<TourInfoVM>();

        public TourLogsVM TourLogsVM
            => serviceProvider.GetRequiredService<TourLogsVM>();

        public ToursListViewModel ToursListViewModel
            => serviceProvider.GetRequiredService<ToursListViewModel>();
    }
}
