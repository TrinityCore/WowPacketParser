using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarEventInvite
    {
        public ulong ModeratorID;
        public bool IsSignUp;
        public bool Creating;
        public ulong EventID;
        public string Name;
    }
}
