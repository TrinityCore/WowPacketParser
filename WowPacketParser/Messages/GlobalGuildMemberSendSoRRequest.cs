using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildMemberSendSoRRequest
    {
        public ulong Member;
        public string Text;
    }
}
