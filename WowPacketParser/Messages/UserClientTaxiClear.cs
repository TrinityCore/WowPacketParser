using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientTaxiClear
    {
        public uint NodeID;
        public string Target;
    }
}
