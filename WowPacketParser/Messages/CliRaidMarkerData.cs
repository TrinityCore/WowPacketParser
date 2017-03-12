using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliRaidMarkerData
    {
        public ulong TransportGUID;
        public int MapID;
        public Vector3 Position;
    }
}
