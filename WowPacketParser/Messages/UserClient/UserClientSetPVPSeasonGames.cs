namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSetPVPSeasonGames
    {
        public uint NumWon;
        public int Season;
        public uint NumPlayed;
        public byte Bracket;
    }
}
