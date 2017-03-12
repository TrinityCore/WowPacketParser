using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQueryNPCText
    {
        public ulong Guid;
        public uint TextID;
    }
}
