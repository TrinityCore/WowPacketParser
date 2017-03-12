using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventInviteRemovedAlert
    {
        public ulong EventID;
        public uint Date;
        public uint Flags;
        public byte Status;
    }
}
