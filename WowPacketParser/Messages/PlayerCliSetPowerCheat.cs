using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetPowerCheat
    {
        public int Power;
        public byte PowerType;
    }
}
