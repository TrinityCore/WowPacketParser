using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct BattlePayShopEntry
    {
        public uint EntryID;
        public uint GroupID;
        public uint ProductID;
        public int Ordering;
        public uint Flags;
        public byte BannerType;
        public BattlepayDisplayInfo? DisplayInfo; // Optional
    }
}
