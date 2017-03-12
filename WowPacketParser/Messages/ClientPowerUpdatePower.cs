using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPowerUpdatePower
    {
        public int Power;
        public byte PowerType;
    }
}
