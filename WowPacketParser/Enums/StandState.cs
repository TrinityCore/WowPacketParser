using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParser.Enums
{
    enum StandState : uint
    {
        Stand          = 0,
        Sit            = 1,
        SitChair       = 2,
        Sleep          = 3,
        SitLowChair    = 4,
        SitMediumChair = 5,
        SitHighChair   = 6,
        Dead           = 7,
        Kneel          = 8,
        Submerged      = 9
    }
}
