namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTrainerListSpell
    {
        public int SpellID;
        public uint MoneyCost;
        public uint ReqSkillLine;
        public uint ReqSkillRank;
        public fixed int ReqAbility[3];
        public byte Usable;
        public byte ReqLevel;
    }
}
