namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct WhoRequestServerInfo
    {
        public int FactionGroup;
        public int Locale;
        public uint RequesterVirtualRealmAddress;
    }
}
