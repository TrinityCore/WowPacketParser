using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryTimeResponse
    {
        public UnixTime CurrentTime;
        public int TimeOutRequest;
    }
}
