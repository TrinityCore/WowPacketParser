using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientTaxiEnable
    {
        public string Target;
        public uint NodeID;
    }
}
