using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliBuyItem
    {
        public ulong VendorGUID;
        public ItemInstance Item;
        public uint Muid;
        public uint Slot;
        public byte ItemType;
        public int Quantity;
        public ulong ContainerGUID;
    }
}
