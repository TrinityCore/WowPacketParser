namespace WowPacketParser.Enums
{
    public enum ArenaCommandResult
    {
        TeamCreated               = 0,
        InternalError             = 1,
        YouAlreadyInTeam          = 2,
        AlreadyInTeam             = 3,
        YouHaveAlreadyBeenInvited = 4,
        AlreadyBeenInvited        = 5,
        TeamNameInvalid           = 6,
        TeamNameAlreadyExists     = 7,
        InsufficientPermission    = 8,
        YouAreNotInTeamOfThatSize = 9,
        PlayerNotInTeam           = 10,
        PlayerNotFound            = 11,
        EnemiInvited              = 12,
        PlayerIgnoringYou         = 19,
        PlayerLevelTooLow         = 21,
        PlayerLevelTooHigh        = 22,
        TeamFull                  = 23,
        ArenaTeamNotOnline        = 27,
        CommandsLocked            = 30,
        TooManyCreateAttempts     = 33
    }
}
