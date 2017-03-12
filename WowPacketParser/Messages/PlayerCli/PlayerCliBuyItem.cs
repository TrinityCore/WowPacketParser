using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
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
