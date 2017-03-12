using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientOpenShipmentNPCResult
    {
        public int CharShipmentContainerID;
        public bool Success;
    }
}
