using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildQueryMembersForRecipe
    {
        public ulong GuildGUID;
        public int UniqueBit;
        public int SkillLineID;
        public int SpellID;
    }
}
