using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildBankTextQueryResult
    {
        public int Tab;
        public string Text;
    }
}
