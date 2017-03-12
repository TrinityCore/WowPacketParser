using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarComplain
    {
        public ulong InviteID;
        public ulong EventID;
        public ulong InvitedByGUID;
    }
}
