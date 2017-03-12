namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellChanceProcLog
    {
        public ulong Guid;
        public ulong TargetGUID;
        public int ProcType;
        public float Roll;
        public float Needed;
        public int SpellID;
        public int ProcSubType;
    }
}
