using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMRequestPlayerInfo
    {
        public bool Success;
        public string Name;
    }
}
