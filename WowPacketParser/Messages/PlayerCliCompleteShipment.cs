using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCompleteShipment
    {
        public ulong ShipmentID;
        public ulong NpcGUID;
    }
}
