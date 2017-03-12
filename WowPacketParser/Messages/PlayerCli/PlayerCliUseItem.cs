using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliUseItem
    {
        public byte PackSlot;
        public SpellCastRequest Cast;
        public byte Slot;
        public ulong CastItem;
    }
}
