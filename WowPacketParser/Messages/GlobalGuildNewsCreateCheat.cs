using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildNewsCreateCheat
    {
        public ulong Guild;
        public uint DateOffset;
        public uint Data;
        public uint NewsType;
    }
}
