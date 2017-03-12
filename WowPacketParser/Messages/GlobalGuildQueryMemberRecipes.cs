using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildQueryMemberRecipes
    {
        public ulong GuildMember;
        public ulong GuildGUID;
        public int SkillLineID;
    }
}
