using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMSpawnTrackerDump
    {
        public ulong Target;
        public SpawnTrackerData SpawnTrackerData;
    }
}
