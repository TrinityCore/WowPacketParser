using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGhost
    {
        public bool Enable;
        public string PlayerName;
    }
}
