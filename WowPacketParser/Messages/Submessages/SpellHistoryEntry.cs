namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct SpellHistoryEntry
    {
        public uint SpellID;
        public uint ItemID;
        public uint Category;
        public int RecoveryTime;
        public int CategoryRecoveryTime;
        public bool OnHold;
    }
}
