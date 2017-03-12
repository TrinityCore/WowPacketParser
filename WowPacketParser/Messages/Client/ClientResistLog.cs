namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientResistLog
    {
        public ulong Attacker;
        public ulong Victim;
        public float ResistRoll;
        public int CastLevel;
        public int SpellID;
        public float ResistRollNeeded;
        public byte ResistLogFlags;
    }
}
