using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GuildRankTabPermissions
    {
        public int Flags;
        public int WithdrawItemLimit;
    }
}
