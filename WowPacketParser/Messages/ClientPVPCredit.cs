using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPVPCredit
    {
        public ulong Target;
        public int Honor;
        public int Rank;
    }
}
