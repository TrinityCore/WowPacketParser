using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliAutoEquipItemSlot
    {
        public ulong Item;
        public byte ItemDstSlot;
        public InvUpdate Inv;
    }
}
