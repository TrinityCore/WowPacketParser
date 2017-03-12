using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientResumeToken
    {
        public uint Sequence;
        public ClientSuspendReason Reason;
    }
}
