namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliBnetLicense
    {
        public int LicenseID;
        public UnixTime Expiration;
    }
}
