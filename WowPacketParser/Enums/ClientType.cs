namespace WowPacketParser.Enums
{
    public enum ClientType
    {
        Current            = -1, // this is used by creature_template HealthScalingExpansion field, client forces this to current expansion and also increases levels by "current max"
        WorldOfWarcraft    = 0,
        TheBurningCrusade  = 1,
        WrathOfTheLichKing = 2,
        Cataclysm          = 3,
        MistsOfPandaria    = 4,
        WarlordsOfDraenor  = 5,
        Legion             = 6,
        BattleForAzeroth   = 7,
        Classic            = 7,
        Shadowlands        = 8,
        ClassicSoM         = 8,
        ClassicHardcore    = 8,
        ClassicSoD         = 8,
        BurningCrusadeClassic = 8,
        WotLKClassic       = 8,
        Dragonflight       = 9,
        CataClassic        = 9,
        TheWarWithin       = 10,
    }
}
