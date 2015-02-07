namespace WowPacketParser.Enums
{
    public enum PowerType
    {
        Health        = -2,
        Mana          = 0,
        Rage          = 1,
        Focus         = 2,
        Energy        = 3,
        Happiness     = 4, // removed in >4.x
        Rune          = 5,
        RunicPower    = 6,
        SoulShards    = 7,
        Eclipse       = 8,
        HolyPower     = 9,
        Alternate     = 10,
        ElusiveBrew   = 11, // added 5.x
        Chi           = 12, // added 5.x
        ShadowOrbs    = 13, // added 5.x
        BurningEmbers = 14, // added 5.x
        DemonicFury   = 15, // added 5.x
        ArcaneCharge  = 16  // added 5.x
    }
}
