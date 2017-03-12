using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPendingRaidLock
    {
        public uint CompletedMask;
        public bool WarningOnly;
        public int TimeUntilLock;
        public bool Extending;
    }
}
