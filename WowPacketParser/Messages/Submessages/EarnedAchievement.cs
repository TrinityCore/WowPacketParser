namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct EarnedAchievement
    {
        public int Id;
        public Data Date;
        public ulong Owner;
        public uint VirtualRealmAddress;
        public uint NativeRealmAddress;
    }
}
