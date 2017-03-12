using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ResyncRune
    {
        public byte RuneType;
        public byte Cooldown;
    }
}
