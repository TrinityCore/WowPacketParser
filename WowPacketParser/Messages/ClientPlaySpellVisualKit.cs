using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPlaySpellVisualKit
    {
        public ulong Unit;
        public int KitType;
        public uint Duration;
        public int KitRecID;
    }
}
