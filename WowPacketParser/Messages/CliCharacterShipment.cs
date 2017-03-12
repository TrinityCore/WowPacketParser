using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliCharacterShipment
    {
        public int ShipmentRecID;
        public ulong ShipmentID;
        public UnixTime CreationTime;
        public UnixTime ShipmentDuration;
    }
}
