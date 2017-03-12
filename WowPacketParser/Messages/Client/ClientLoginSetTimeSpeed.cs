namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLoginSetTimeSpeed
    {
        public float NewSpeed;
        public int ServerTimeHolidayOffset;
        public uint GameTime;
        public uint ServerTime;
        public int GameTimeHolidayOffset;
    }
}
