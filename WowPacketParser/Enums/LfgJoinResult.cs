namespace WowPacketParser.Enums
{
    public enum LfgJoinResult
    {
        Ok                     = 0,
        Failed                 = 1,
        GroupFull              = 2,
        InternalError          = 4,
        NotMeetReqs            = 5,
        PartyNotMeetReqs       = 6,
        MixedRaidDungeon       = 7,
        NotFromMultipleRealms  = 8,
        SomeoneIsDisconnected  = 9,
        PartyInfoFailed        = 10,
        DungeonInvalid         = 11,
        HasDeserterFlag        = 12,
        PartyHasDeserterFlag   = 13,
        HasRandomCooldown      = 14,
        PartyHasRandomCooldown = 15,
        TooMuchMembers         = 16,
        NotWhileUsingBGSystem  = 17
    }
}
