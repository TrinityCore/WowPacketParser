using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
