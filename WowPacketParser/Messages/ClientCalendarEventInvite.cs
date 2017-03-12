using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventInvite
    {
        public ulong InviteID;
        public uint ResponseTime;
        public byte Level;
        public ulong InviteGUID;
        public ulong EventID;
        public byte Type;
        public bool ClearPending;
        public byte Status;
    }
}
