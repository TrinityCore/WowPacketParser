using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
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
