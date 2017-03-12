using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
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
