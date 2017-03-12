using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGMInvis
    {
        public int Action;
        public string PlayerName;
    }
}
