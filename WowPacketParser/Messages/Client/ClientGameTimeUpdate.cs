namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGameTimeUpdate
    {
        public int GameTimeHolidayOffset;
        public int ServerTimeHolidayOffset;
        public uint GameTime;
        public uint ServerTime;
    }
}
