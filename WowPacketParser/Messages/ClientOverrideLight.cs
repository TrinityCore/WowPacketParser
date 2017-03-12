using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientOverrideLight
    {
        public int TransitionMilliseconds;
        public int AreaLightID;
        public int OverrideLightID;
    }
}
