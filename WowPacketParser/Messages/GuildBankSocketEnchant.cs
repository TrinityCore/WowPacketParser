using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GuildBankSocketEnchant
    {
        public int SocketIndex;
        public int SocketEnchantID;
    }
}
