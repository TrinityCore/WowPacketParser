namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuraCastLog
    {
        public ulong Victim;
        public ulong Attacker;
        public float ApplyRoll;
        public int SpellID;
        public float ApplyRollNeeded;
    }
}
