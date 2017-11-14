namespace WowPacketParser.Messages.Player
{
    public unsafe struct GuidLookupData
    {
        public bool IsDeleted;
        public uint AccountID;
        public ulong BnetAccountID;
        public ulong GuidActual;
        public string Name;
        public uint VirtualRealmAddress;
        public byte Race;
        public byte Sex;
        public byte ClassID;
        public byte Level;
        public string[/*5*/] DeclinedNames;
    }
}
