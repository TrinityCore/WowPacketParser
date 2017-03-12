using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCompleteShipmentResponse
    {
        public ulong ShipmentID;
        public int Result;
    }
}
