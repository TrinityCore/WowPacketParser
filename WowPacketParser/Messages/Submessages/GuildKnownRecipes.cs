namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct GuildKnownRecipes
    {
        public int SkillLineID;
        public fixed byte SkillLineBitArray[300];
    }
}
