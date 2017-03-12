using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellChannelUpdate
    {
        public ulong CasterGUID;
        public int TimeRemaining;
    }
}
