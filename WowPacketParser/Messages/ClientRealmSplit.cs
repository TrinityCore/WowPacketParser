using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRealmSplit
    {
        public int Decision;
        public int State;
        public string Date;
    }
}
