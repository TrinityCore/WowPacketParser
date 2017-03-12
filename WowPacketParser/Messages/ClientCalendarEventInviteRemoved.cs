using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventInviteRemoved
    {
        public bool ClearPending;
        public uint Flags;
        public ulong InviteGUID;
        public ulong EventID;
    }
}
