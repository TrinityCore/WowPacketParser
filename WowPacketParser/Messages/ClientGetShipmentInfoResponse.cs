using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGetShipmentInfoResponse
    {
        public List<CliCharacterShipment> Shipments;
        public int PlotInstanceID;
        public int ShipmentID;
        public bool Success;
        public int MaxShipments;
    }
}
