using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQueryPageText
    {
        public ulong ItemGUID;
        public uint PageTextID;
    }
}
