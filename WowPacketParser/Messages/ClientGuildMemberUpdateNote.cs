using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildMemberUpdateNote
    {
        public string Note;
        public ulong Member;
        public bool IsPublic;
    }
}
