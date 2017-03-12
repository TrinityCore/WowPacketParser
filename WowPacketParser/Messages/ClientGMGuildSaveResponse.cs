using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMGuildSaveResponse
    {
        public bool Success;
        public Data ProfileData;
    }
}
