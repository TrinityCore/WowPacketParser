namespace WowPacketParser.Enums
{
    public enum PowerType
    {
        Health              = -2,
        Mana                = 0,
        Rage                = 1,
        Focus               = 2,
        Energy              = 3,
        ComboPoints         = 4, // added 5.x or 6.x, was named Happiness in <4.x
        Runes               = 5,
        RunicPower          = 6,
        SoulShards          = 7,
        LunarPower          = 8, // was named Eclipse, Balance pre 7.x
        HolyPower           = 9,
        Alternate           = 10,
        Maelstrom           = 11, // added 7.x, named ElusiveBrew in 5.x
        Chi                 = 12, // added 5.x
        Insanity            = 13, // added 5.x, renamed from ShadowOrbs in 7.x
        BurningEmbers       = 14, // added 5.x, removed in 7.x/named Obsolete
        DemonicFury         = 15, // added 5.x, removed in 7.x/named Obsolete2
        ArcaneCharges       = 16, // added 5.x
        Fury                = 17, // added 7.x
        Pain                = 18, // added 7.x
        Essence             = 19, // added 10.x
        RuneBlood           = 20, // added 10.x
        RuneFrost           = 21, // added 10.x
        RuneUnholy          = 22, // added 10.x
        AlternateQuest      = 23, // added 10.x
        AlternateEncounter  = 24, // added 10.x
        AlternateMount      = 25, // added 10.x
    }
}
