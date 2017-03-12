using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAreaSpiritHealerTime
    {
        public ulong HealerGuid;
        public int TimeLeft;
    }
}
