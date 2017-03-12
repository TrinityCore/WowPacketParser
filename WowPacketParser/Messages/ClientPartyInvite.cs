using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPartyInvite
    {
        public bool AllowMultipleRoles;
        public uint InviterCfgRealmID;
        public bool MightCRZYou;
        public bool CanAccept;
        public ulong InviterGuid;
        public bool MustBeBNetFriend;
        public uint LfgCompletedMask;
        public uint ProposedRoles;
        public List<uint> LfgSlots;
        public bool IsXRealm;
        public string InviterRealmName;
        public string InviterName;
        public ulong InviterBNetAccountID;
    }
}
