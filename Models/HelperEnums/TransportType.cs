using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_4_SWENII.Models.HelperEnums
{

    /// <summary>
    ///
    /// NOTE: default value (0) must be valid
    /// for instance: if ((flag & TransportType.Bike) != 0)
    ///
    /// var usedTransportType = MixedTransportType.Hike ^ MixedTransportType.Running; // ==  Hike XOR Running.  
    /// </summary>

    [Flags]
    public enum TransportType //  : uint 
    {
        Pedestrian,
        Bicycle,
        Fastest,
        Shortest
    }
}
