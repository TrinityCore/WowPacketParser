namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSetTimeZoneInformation
    {
        public string ServerTimeTZ;
        public string GameTimeTZ;
    }
}
