using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientWaitQueueUpdate
    {
        public AuthWaitInfo WaitInfo;
    }
}
