namespace WowPacketParser.Enums
{
    public enum ArenaError : uint
    {
        NoTeam              = 0, // ERR_ARENA_NO_TEAM_II
        ExpiredCAIS         = 1, // ERR_ARENA_EXPIRED_CAIS
        CantUseBattleground = 2  // ERR_LFG_CANT_USE_BATTLEGROUND
    }
}
