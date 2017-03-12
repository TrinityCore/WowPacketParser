using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSwapInvItem
    {
        public InvUpdate Inv;
        public byte Slot2;
        public byte Slot1;
    }
}
