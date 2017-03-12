using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAutoStoreBagItem
    {
        public byte ContainerSlotB;
        public InvUpdate Inv;
        public byte ContainerSlotA;
        public byte SlotA;
    }
}
