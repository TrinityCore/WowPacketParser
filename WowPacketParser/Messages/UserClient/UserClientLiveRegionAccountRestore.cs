namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientLiveRegionAccountRestore
    {
        public uint Token;
        public string DevRealmOverride;
        public byte? RegionID; // Optional
        public string DevCharOverride;
    }
}
