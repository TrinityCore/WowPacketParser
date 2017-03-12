using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliDebugServerGeo
    {
        public uint ShowPathingGeoDist;
        public bool ShowSurfaceLinks;
        public bool Enabled;
        public bool ShowDoors;
    }
}
