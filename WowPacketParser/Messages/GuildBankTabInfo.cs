using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GuildBankTabInfo
    {
        public int TabIndex;
        public string Name;
        public string Icon;
    }
}
