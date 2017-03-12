using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientStartTimer
    {
        public UnixTime TimeRemaining;
        public UnixTime TotalTime;
        public int Type;
    }
}
