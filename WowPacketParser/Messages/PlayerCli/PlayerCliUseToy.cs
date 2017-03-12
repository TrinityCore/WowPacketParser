using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliUseToy
    {
        public SpellCastRequest Cast;
        public int ItemID;
    }
}
