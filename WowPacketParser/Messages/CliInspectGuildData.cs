using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliInspectGuildData
    {
        public ulong GuildGUID;
        public long GuildXP;
        public int GuildLevel;
        public int NumGuildMembers;
    }
}
