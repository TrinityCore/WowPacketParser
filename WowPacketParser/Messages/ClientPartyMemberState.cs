using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
