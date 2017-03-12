using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliAutostoreBankItem
    {
        public InvUpdate Inv;
        public byte Slot;
        public byte PackSlot;
    }
}
