using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCreateShipmentResponse
    {
        public ulong ShipmentID;
        public int Result;
        public int ShipmentRecID;
    }
}
