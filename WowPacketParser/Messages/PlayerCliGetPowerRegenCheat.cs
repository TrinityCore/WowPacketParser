using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGetPowerRegenCheat
    {
        public ulong Target;
        public int PowerType;
    }
}
