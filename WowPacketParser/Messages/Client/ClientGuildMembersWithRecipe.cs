using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildMembersWithRecipe
    {
        public int SpellID;
        public List<ulong> Members;
        public int SkillLineID;
    }
}
