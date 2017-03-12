using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventInviteStatusAlert
    {
        public ulong EventID;
        public uint Flags;
        public uint Date;
        public byte Status;
    }
}
