using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarEventRSVP
    {
        public ulong InviteID;
        public ulong EventID;
        public byte Status;
    }
}
