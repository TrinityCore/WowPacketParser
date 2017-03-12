namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientInstanceEncounterStart
    {
        public uint NextCombatResChargeTime;
        public uint CombatResChargeRecovery;
        public int MaxInCombatResCount;
        public int InCombatResCount;
    }
}
