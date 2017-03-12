using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct RuneData
    {
        public byte Start;
        public byte Count;
        public List<byte> Cooldowns;
    }
}
