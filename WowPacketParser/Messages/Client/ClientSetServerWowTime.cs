namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSetServerWowTime
    {
        public uint EncodedTime;
        public int HolidayOffset;
    }
}
