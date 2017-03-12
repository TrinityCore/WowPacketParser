using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPartyMemberState
    {
        public uint ChangeMask;
        public ulong MemberGuid;
        public bool ForEnemy;
        public bool FullUpdate;
        public Data Changes;
    }
}
