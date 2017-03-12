using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarSendCalendarRaidLockoutInfo
    {
        public ulong InstanceID;
        public int MapID;
        public uint DifficultyID;
        public int ExpireTime;
    }
}
