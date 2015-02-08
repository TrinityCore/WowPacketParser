namespace WowPacketParser.Enums
{
    public enum QuestRequirementType : byte
    {
        CreatureKill            = 0,
        Item                    = 1,
        GameObject              = 2,
        CreatureInteract        = 3,
        Currency                = 4,
        Spell                   = 5,
        FactionRepHigher        = 6,
        FactionRepLower         = 7,
        Money                   = 8,
        PlayerKills             = 9,
        Dummy                   = 10,
        PetBattleDefeatCreature = 11,
        PetBattleDefeatSpecies  = 12,
        PetBattleDefeatPlayer   = 13
    }
}
