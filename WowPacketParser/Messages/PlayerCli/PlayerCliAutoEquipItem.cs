using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliAutoEquipItem
    {
        public byte Slot;
        public InvUpdate Inv;
        public byte PackSlot;
    }
}
