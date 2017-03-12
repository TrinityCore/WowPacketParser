using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientReadyCheckStarted
    {
        public byte PartyIndex;
        public ulong InitiatorGUID;
        public ulong PartyGUID;
        public UnixTime Duration;
    }
}
