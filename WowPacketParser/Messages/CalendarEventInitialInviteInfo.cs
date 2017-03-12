using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CalendarEventInitialInviteInfo
    {
        public ulong InviteGUID;
        public byte Level;
    }
}
