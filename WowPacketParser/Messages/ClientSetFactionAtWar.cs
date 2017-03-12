using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetFactionAtWar
    {
        public byte FactionIndex;
        public byte Flags;
    }
}
