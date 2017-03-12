namespace WowPacketParser.Messages.Global
{
    public unsafe struct GlobalGuildQueryMembersForRecipe
    {
        public ulong GuildGUID;
        public int UniqueBit;
        public int SkillLineID;
        public int SpellID;
    }
}
