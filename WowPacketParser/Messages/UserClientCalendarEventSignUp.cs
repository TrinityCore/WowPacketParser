using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarEventSignUp
    {
        public bool Tentative;
        public ulong EventID;
    }
}
