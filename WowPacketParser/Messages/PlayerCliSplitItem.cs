using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSplitItem
    {
        public byte ToSlot;
        public byte ToPackSlot;
        public byte FromPackSlot;
        public int Quantity;
        public InvUpdate Inv;
        public byte FromSlot;
    }
}
