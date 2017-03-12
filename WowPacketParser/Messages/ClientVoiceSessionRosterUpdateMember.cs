using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientVoiceSessionRosterUpdateMember
    {
        public ulong MemberGUID;
        public byte NetworkId;
        public byte Priority;
        public byte Flags;
    }
}
