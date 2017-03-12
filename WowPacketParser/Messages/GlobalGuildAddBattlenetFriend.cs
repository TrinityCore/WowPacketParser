using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildAddBattlenetFriend
    {
        public uint RoleID;
        public ulong ClientToken;
        public bool VerifyOnly;
        public ulong TargetGUID;
        public string InvitationMsg;
    }
}
