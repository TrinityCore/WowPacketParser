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
        Busy          = 21,
        BeginTrade    = 12,
        OpenWindow    = 0,
        Canceled      = 23,
        Accept        = 6,
        Busy2         = 18,
        NoTarget      = 17,
        BackToTrade   = 25,
        Tradecomplete = 9,
        Unk9          = 22,
        TargetToFar   = 16,
        WrongFaction  = 20,
        CloseWindow   = 31,
        //Unk13         = ?,
        IgnoringYou   = 4,
        YouStunned    = 27,
        TargetStunned = 29,
        YouDead       = 13,
        TargetDead    = 5,
        YouLogout     = 3,
        TargetLogout  = 7,
        TrialAccount  = 10,
        OnlyConjured  = 26,
        NotEligible   = 2,

        TradeCurrency = 24,
        CurrencyNotTradable = 19,
    }
}
