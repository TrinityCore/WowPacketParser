using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarRaidLockoutRemoved
    {
        public ulong InstanceID;
        public int MapID;
        public uint DifficultyID;
    }
}
