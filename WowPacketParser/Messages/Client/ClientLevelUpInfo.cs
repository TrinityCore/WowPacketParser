namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLevelUpInfo
    {
        public int Cp;
        public fixed int StatDelta[5];
        public int HealthDelta;
        public fixed int PowerDelta[6];
        public int Level;
    }
}
