namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellDispellData
    {
        public int SpellID;
        public bool Harmful;
        public int? Rolled; // Optional
        public int? Needed; // Optional
    }
}
