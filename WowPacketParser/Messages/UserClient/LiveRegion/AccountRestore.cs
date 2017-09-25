namespace WowPacketParser.Messages.UserClient.LiveRegion
{
    public unsafe struct AccountRestore
    {
        public uint Token;
        public string DevRealmOverride;
        public byte? RegionID; // Optional
        public string DevCharOverride;
    }
}
