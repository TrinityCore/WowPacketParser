using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct ProgressiveEventCheat
    {
        public Submessages.ProgressiveEventCheat Type;
        public int ItemID;
        public int Count;
        public int EventID;
    }
}
