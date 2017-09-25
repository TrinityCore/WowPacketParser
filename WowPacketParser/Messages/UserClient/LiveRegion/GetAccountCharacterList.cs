namespace WowPacketParser.Messages.UserClient.LiveRegion
{
    public unsafe struct GetAccountCharacterList
    {
        public string DevRealmOverride;
        public uint Token;
        public string DevCharOverride;
        public byte? RegionID; // Optional
    }
}
