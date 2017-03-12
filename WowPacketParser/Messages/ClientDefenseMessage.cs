using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDefenseMessage
    {
        public int ZoneID;
        public string MessageText;
    }
}
