namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGameTimeSet
    {
        public uint ServerTime;
        public int GameTimeHolidayOffset;
        public int ServerTimeHolidayOffset;
        public uint GameTime;
    }
}
