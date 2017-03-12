using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
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
