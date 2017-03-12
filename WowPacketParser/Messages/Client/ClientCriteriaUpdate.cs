using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCriteriaUpdate
    {
        public Data CurrentTime;
        public int Flags;
        public ulong Quantity;
        public ulong PlayerGUID;
        public int CriteriaID;
        public UnixTime ElapsedTime;
        public UnixTime CreationTime;
    }
}
