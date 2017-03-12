using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientReferAFriendFailure
    {
        public string Str;
        public int Reason;
    }
}
