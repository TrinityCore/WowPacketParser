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

        // chose higher value to have some room for future
        Classic             = 20,
        WotlkClassic        = 21
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
        AnyRetail                         = TheBurningCrusade | WrathOfTheLichKing | Cataclysm | WarlordsOfDraenor | Legion | BattleForAzeroth | Shadowlands | Dragonflight,

        // Classic
        Classic                           = 1 << TargetedDatabase.Classic,
        WotlkClassic                      = 1 << TargetedDatabase.WotlkClassic,
        AnyClassic                        = Classic | WotlkClassic,

        Any                               = AnyRetail | AnyClassic,

        // predefines
        TillWrathOfTheLichKing            = TheBurningCrusade | WrathOfTheLichKing,
        TillCataclysm                     = TheBurningCrusade | WrathOfTheLichKing | Cataclysm,
        TillWarlordsOfDraenor             = TheBurningCrusade | WrathOfTheLichKing | Cataclysm | WarlordsOfDraenor,
        TillLegion                        = TheBurningCrusade | WrathOfTheLichKing | Cataclysm | WarlordsOfDraenor | Legion,
        TillBattleForAzeroth              = TheBurningCrusade | WrathOfTheLichKing | Cataclysm | WarlordsOfDraenor | Legion | BattleForAzeroth,
        TillShadowlands                   = TheBurningCrusade | WrathOfTheLichKing | Cataclysm | WarlordsOfDraenor | Legion | BattleForAzeroth | Shadowlands,

        FromCataclysmTillBattleForAzeroth = Cataclysm | WarlordsOfDraenor | Legion | BattleForAzeroth,

        // update us when new expansion arrives
        SinceCataclysm                    = Cataclysm | WarlordsOfDraenor | Legion | BattleForAzeroth | Shadowlands | Dragonflight,
        SinceWarlordsOfDraenor            = WarlordsOfDraenor | Legion | BattleForAzeroth | Shadowlands | Dragonflight,
        SinceLegion                       = Legion | BattleForAzeroth | Shadowlands | Dragonflight,
        SinceBattleForAzeroth             = BattleForAzeroth | Shadowlands | Dragonflight,
        SinceShadowlands                  = Shadowlands | Dragonflight,

        // Classic
    }
}
