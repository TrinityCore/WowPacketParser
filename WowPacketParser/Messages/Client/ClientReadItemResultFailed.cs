using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientReadItemResultFailed
    {
        public ulong Item;
        public ReadItemFailure Subcode;
        public int Delay;
    }
}
