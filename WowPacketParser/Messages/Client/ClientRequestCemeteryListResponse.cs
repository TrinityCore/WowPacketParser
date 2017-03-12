using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientRequestCemeteryListResponse
    {
        public bool IsGossipTriggered;
        public List<uint> CemeteryID;
    }
}
