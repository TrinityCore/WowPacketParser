using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientDFGetSystemInfo
    {
        public byte PartyIndex;
        public bool Player;
    }
}
