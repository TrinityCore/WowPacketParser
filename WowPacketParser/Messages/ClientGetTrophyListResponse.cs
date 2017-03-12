using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGetTrophyListResponse
    {
        public List<int> TrophyID;
        public bool Success;
    }
}
