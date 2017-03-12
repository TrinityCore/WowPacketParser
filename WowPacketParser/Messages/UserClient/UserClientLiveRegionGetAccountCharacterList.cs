namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientLiveRegionGetAccountCharacterList
    {
        public string DevRealmOverride;
        public uint Token;
        public string DevCharOverride;
        public byte? RegionID; // Optional
    }
}
