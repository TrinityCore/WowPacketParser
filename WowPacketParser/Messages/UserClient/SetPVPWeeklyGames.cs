namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetPVPWeeklyGames
    {
        public uint NumPlayed;
        public int Season;
        public uint NumWon;
        public byte Bracket;
    }
}
