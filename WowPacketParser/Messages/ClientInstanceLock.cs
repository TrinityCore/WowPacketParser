using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInstanceLock
    {
        public uint MapID;
        public uint DifficultyID;
        public ulong InstanceID;
        public bool Locked;
        public bool Extended;
        public uint TimeRemaining;
        public uint Completed_mask;
    }
}
