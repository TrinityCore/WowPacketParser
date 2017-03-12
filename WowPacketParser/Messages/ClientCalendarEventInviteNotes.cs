using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventInviteNotes
    {
        public ulong InviteGUID;
        public bool ClearPending;
        public string Notes;
        public ulong EventID;
    }
}
