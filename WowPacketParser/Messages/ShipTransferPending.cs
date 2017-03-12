using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ShipTransferPending
    {
        public uint ID;
        public int OriginMapID;
    }
}
