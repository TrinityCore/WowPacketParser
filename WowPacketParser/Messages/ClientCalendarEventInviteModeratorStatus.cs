using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventInviteModeratorStatus
    {
        public byte Status;
        public ulong InviteGUID;
        public ulong EventID;
        public bool ClearPending;
    }
}
