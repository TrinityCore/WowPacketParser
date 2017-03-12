namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct BannedAddonInfo
    {
        public int Id;
        public UnixTime LastModified;
        public int Flags;
        public fixed uint NameMD5[4];
        public fixed uint VersionMD5[4];
    }
}
