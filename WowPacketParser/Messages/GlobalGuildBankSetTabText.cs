using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildBankSetTabText
    {
        public int Tab;
        public string TabText;
    }
}
