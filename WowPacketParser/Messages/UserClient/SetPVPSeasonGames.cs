namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetPVPSeasonGames
    {
        public uint NumWon;
        public int Season;
        public uint NumPlayed;
        public byte Bracket;
    }
}
