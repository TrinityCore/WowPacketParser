using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientOpenShipmentNPCFromGossip
    {
        public ulong NpcGUID;
        public int CharShipmentContainerID;
    }
}
