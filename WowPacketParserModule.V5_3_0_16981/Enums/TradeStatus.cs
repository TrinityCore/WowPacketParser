namespace WowPacketParserModule.V5_3_0_16981.Enums
{
    public enum TradeStatus
    {
        //Busy = 21,
        BeginTrade = 12, // checked
        OpenWindow = 21, // checked
        Canceled = 20, // checked
        Accept = 1, // checked
        Busy2 = 18,
        NoTarget = 17,
        BackToTrade = 16, // checked
        //Tradecomplete = 9,
        Unk9 = 24,// checked
        //TargetToFar = 16,
        //WrongFaction = 20,
        CloseWindow = 15, // checked
        //Unk13         = ?,
        IgnoringYou = 4,
        YouStunned = 27,
        TargetStunned = 29,
        YouDead = 13,
        //TargetDead = 5,
        //YouLogout = 3,
        TargetLogout = 7,
        TrialAccount = 9, // checked
        OnlyConjured = 30, // checked
        NotEligible = 5, // checked

        TradeCurrency = 3, // checked
        CurrencyNotTradable = 22 // checked
    }
}
