using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientPartyInvite
    {
        public ulong TargetGuid;
        public uint ProposedRoles;
        public string TargetName;
        public byte PartyIndex;
        public string TargetRealm;
        public uint TargetCfgRealmID;
    }
}
