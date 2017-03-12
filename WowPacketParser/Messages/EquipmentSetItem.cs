using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct EquipmentSetItem
    {
        public ulong Item;
        public byte ContainerSlot;
        public byte Slot;
    }
}
