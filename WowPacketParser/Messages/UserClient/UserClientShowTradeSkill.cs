namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientShowTradeSkill
    {
        public ulong PlayerGUID;
        public int SkillLineID;
        public int SpellID;
    }
}
