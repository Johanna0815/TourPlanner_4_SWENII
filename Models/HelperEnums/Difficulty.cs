using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_4_SWENII.Models.HelperEnums
{
    [Flags]
    public enum Difficulty
    {
        None = 0,
        Easy,
        Medium,
        Hard,
        Expert
        /*
        None = 0, // none means no difficulty, so more than easy, or none inserted? TODO 
        Easy = 1,
        CanDoKidsToo = 2,
        ABitConditionRequired = 4,
        BetterTrainHardBefore = 8
        */
       


    }
}
