using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMSpawnTrackerDump
    {
        public ulong Target;
        public SpawnTrackerData SpawnTrackerData;
    }
}
