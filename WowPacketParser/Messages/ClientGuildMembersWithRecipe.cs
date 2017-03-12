using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildMembersWithRecipe
    {
        public int SpellID;
        public List<ulong> Members;
        public int SkillLineID;
    }
}
