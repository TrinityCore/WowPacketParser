using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildAddXPCheat
    {
        public ulong Guild;
        public int Xp;
    }
}
