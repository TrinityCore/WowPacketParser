using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGroupNewLeader
    {
        public byte PartyIndex;
        public string Name;
    }
}
