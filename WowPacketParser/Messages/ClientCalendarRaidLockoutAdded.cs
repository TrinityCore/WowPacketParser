using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarRaidLockoutAdded
    {
        public ulong InstanceID;
        public uint DifficultyID;
        public int TimeRemaining;
        public uint ServerTime;
        public int MapID;
    }
}
