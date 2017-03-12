using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct SpellMissStatus
    {
        public byte Reason;
        public byte ReflectStatus;
    }
}
