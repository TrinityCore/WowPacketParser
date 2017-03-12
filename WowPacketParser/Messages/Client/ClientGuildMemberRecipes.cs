namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildMemberRecipes
    {
        public ulong Member;
        public int SkillRank;
        public int SkillLineID;
        public int SkillStep;
        public fixed byte SkillLineBitArray[300];
    }
}
