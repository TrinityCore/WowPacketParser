using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRequestCemeteryListResponse
    {
        public bool IsGossipTriggered;
        public List<uint> CemeteryID;
    }
}
