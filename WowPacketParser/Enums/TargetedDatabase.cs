using System;

namespace WowPacketParser.Enums
{
    public enum TargetedDatabase : int
    {
        Zero                = 0,
        TheBurningCrusade   = 1,
        WrathOfTheLichKing  = 2,
        Cataclysm           = 3,
        WarlordsOfDraenor   = 4,
        Legion              = 5,
        BattleForAzeroth    = 6,
        Shadowlands         = 7,
        Dragonflight        = 8,
        TheWarWithin        = 9,

        // chose higher value to have some room for future
        Classic             = 20,
        WotlkClassic        = 21,
        CataClassic         = 22,
        MoPClassic          = 23
    }

    [Flags]
    public enum TargetedDatabaseFlag : uint
    {
        // Retail
        Zero                              = 1 << TargetedDatabase.Zero,
        TheBurningCrusade                 = 1 << TargetedDatabase.TheBurningCrusade,
        WrathOfTheLichKing                = 1 << TargetedDatabase.WrathOfTheLichKing,
        Cataclysm                         = 1 << TargetedDatabase.Cataclysm,
        WarlordsOfDraenor                 = 1 << TargetedDatabase.WarlordsOfDraenor,
        Legion                            = 1 << TargetedDatabase.Legion,
        BattleForAzeroth                  = 1 << TargetedDatabase.BattleForAzeroth,
        Shadowlands                       = 1 << TargetedDatabase.Shadowlands,
        Dragonflight                      = 1 << TargetedDatabase.Dragonflight,
        TheWarWithin                      = 1 << TargetedDatabase.TheWarWithin,
        AnyRetail                         = TheBurningCrusade | WrathOfTheLichKing | Cataclysm | WarlordsOfDraenor | Legion | BattleForAzeroth | Shadowlands | Dragonflight | TheWarWithin,

        // Classic
        Classic                           = 1 << TargetedDatabase.Classic,
        WotlkClassic                      = 1 << TargetedDatabase.WotlkClassic,
        CataClassic                       = 1 << TargetedDatabase.CataClassic,
        MoPClassic                        = 1 << TargetedDatabase.MoPClassic,
        AnyClassic                        = Classic | WotlkClassic | CataClassic | MoPClassic,

        Any                               = AnyRetail | AnyClassic,

        // predefines
        TillWrathOfTheLichKing            = WrathOfTheLichKing | TheBurningCrusade,
        TillCataclysm                     = Cataclysm | TillWrathOfTheLichKing,
        TillWarlordsOfDraenor             = WarlordsOfDraenor | TillCataclysm,
        TillLegion                        = Legion | TillWarlordsOfDraenor,
        TillBattleForAzeroth              = BattleForAzeroth | TillLegion,
        TillShadowlands                   = Shadowlands | TillBattleForAzeroth,
        TillDragonflight                  = Dragonflight | TillShadowlands,
        TillTheWarWithin                  = TheWarWithin | TillDragonflight,

        FromCataclysmTillBattleForAzeroth = Cataclysm | WarlordsOfDraenor | Legion | BattleForAzeroth,

        // update us when new expansion arrives
        SinceTheWarWithin                 = TheWarWithin,
        SinceDragonflight                 = Dragonflight | SinceTheWarWithin,
        SinceShadowlands                  = Shadowlands | SinceDragonflight,
        SinceBattleForAzeroth             = BattleForAzeroth | SinceShadowlands,
        SinceLegion                       = Legion | SinceBattleForAzeroth,
        SinceWarlordsOfDraenor            = WarlordsOfDraenor | SinceLegion,
        SinceCataclysm                    = Cataclysm | SinceWarlordsOfDraenor,

        SinceWarlordsOfDraenorTillShadowLands = WarlordsOfDraenor | Legion | BattleForAzeroth | Shadowlands,
        SinceBattleForAzerothTillDragonflight = BattleForAzeroth | Shadowlands | Dragonflight,

        SinceCataClassic                  = CataClassic | MoPClassic
    }
}
