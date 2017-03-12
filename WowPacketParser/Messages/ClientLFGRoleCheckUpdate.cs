using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGRoleCheckUpdate
    {
        public bool IsBeginning;
        public List<uint> JoinSlots;
        public ulong BgQueueID;
        public byte PartyIndex;
        public byte RoleCheckStatus;
        public List<ClientLFGRoleCheckUpdateMember> Members;
    }
}
