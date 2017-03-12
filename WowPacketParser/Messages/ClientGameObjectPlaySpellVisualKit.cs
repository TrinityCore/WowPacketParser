using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGameObjectPlaySpellVisualKit
    {
        public ulong Object;
        public int KitRecID;
        public int KitType;
        public uint Duration;
    }
}
