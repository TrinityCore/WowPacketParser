using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerProgressiveEventCheat
    {
        public ProgressiveEventCheat Type;
        public int ItemID;
        public int Count;
        public int EventID;
    }
}
