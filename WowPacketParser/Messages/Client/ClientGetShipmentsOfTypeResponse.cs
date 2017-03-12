using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGetShipmentsOfTypeResponse
    {
        public int ContainerID;
        public List<CliCharacterShipment> Shipments;
    }
}
