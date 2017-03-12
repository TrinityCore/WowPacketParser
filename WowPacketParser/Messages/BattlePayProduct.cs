using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct BattlePayProduct
    {
        public uint ProductID;
        public ulong NormalPriceFixedPoint;
        public ulong CurrentPriceFixedPoint;
        public List<BattlepayProductItem> Items;
        public byte Type;
        public byte ChoiceType;
        public uint Flags;
        public BattlepayDisplayInfo? DisplayInfo; // Optional
    }
}
