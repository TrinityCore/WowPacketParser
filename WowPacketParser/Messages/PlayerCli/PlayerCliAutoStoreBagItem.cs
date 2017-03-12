using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliAutoStoreBagItem
    {
        public byte ContainerSlotB;
        public InvUpdate Inv;
        public byte ContainerSlotA;
        public byte SlotA;
    }
}
