using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSwapItem
    {
        public InvUpdate Inv;
        public byte SlotA;
        public byte ContainerSlotB;
        public byte SlotB;
        public byte ContainerSlotA;
    }
}
