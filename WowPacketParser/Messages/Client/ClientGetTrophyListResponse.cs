using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGetTrophyListResponse
    {
        public List<int> TrophyID;
        public bool Success;
    }
}
