namespace WowPacketParser.Enums
{
    public enum CalendarError
    {
        None                  = 0,
        GuildEventsExceeded   = 1,
        EventsExceeded        = 2,
        SelfInvitesExceeded   = 3,
        OtherInvitesExceeded  = 4,
        NoPermissions         = 5,
        EventInvalid          = 6,
        NotInvited            = 7,
        InternalError         = 8,
        PlayerNotInGuild      = 9,
        AlreadyInvitedToEvent = 10,
        PlayerNotFound        = 11,
        NotAllied             = 12,
        PlayerIsIgnoringYou   = 13,
        InvitesExceeded       = 14,
        InvalidDate           = 16,
        InvalidTime           = 17,
        NeedsTitle            = 19,
        EventPassed           = 20,
        EventLocked           = 21,
        DeleteCreatorFailed   = 22,
        SystemDisabled        = 24,
        RestrictedAccount     = 25,
        ArenaEventsExceeded   = 26,
        RestrictedLevel       = 27,
        UserSquelched         = 28,
        NoInvite              = 29,
        WrongServer           = 36,
        InviteWrongServer     = 37,
        NoGuildInvites        = 38,
        InvalidSignup         = 39,
        NoModerator           = 40
    }
}
