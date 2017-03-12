using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBFMgrEntryInvite
    {
        public int AreaID;
        public UnixTime ExpireTime;
        public ulong QueueID;
    }
}
