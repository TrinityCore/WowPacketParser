using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSuspendToken
    {
        public ClientSuspendReason Reason;
        public uint Sequence;
    }
}
