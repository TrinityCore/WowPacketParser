namespace WowPacketParser.Enums
{
    public enum TradeStatus
    {
        Busy          = 0,
        BeginTrade    = 1,
        OpenWindow    = 2,
        Canceled      = 3,
        Accept        = 4,
        Busy2         = 5,
        NoTarget      = 6,
        BackToTrade   = 7,
        Tradecomplete = 8,
        Unk9          = 9,
        TargetToFar   = 10,
        WrongFaction  = 11,
        CloseWindow   = 12,
        Unk13         = 13,
        IgnoringYou   = 14,
        YouStunned    = 15,
        TargetStunned = 16,
        YouDead       = 17,
        TargetDead    = 18,
        YouLogout     = 19,
        TargetLogout  = 20,
        TrialAccount  = 21,
        OnlyConjured  = 22,
        NotEligible   = 23
    }

    public enum TradeStatus434
    {
        OpenWindow          = 0,
        NotEligible         = 2,
        YouLogout           = 3,
        IgnoringYou         = 4,
        TargetDead          = 5,
        Accept              = 6,
        TargetLogout        = 7,
        Tradecomplete       = 9,
        TrialAccount        = 10,
        BeginTrade          = 12,
        YouDead             = 13,
        TargetToFar         = 16,
        NoTarget            = 17,
        Busy2               = 18,
        CurrencyNotTradable = 19,
        WrongFaction        = 20,
        Busy                = 21,
        Unk9                = 22,
        Canceled            = 23,
        TradeCurrency       = 24,
        BackToTrade         = 25,
        OnlyConjured        = 26,
        YouStunned          = 27,
        TargetStunned       = 29,
        CloseWindow         = 31
    }
}
