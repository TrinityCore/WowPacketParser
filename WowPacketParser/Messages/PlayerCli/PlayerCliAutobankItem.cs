using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliAutobankItem
    {
        public byte PackSlot;
        public InvUpdate Inv;
        public byte Slot;
    }
}
