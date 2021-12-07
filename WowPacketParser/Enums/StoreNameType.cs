using System;

namespace WowPacketParser.Enums
{
    public enum StoreNameType
    {
        None                = 1,
        Spell               = 2,
        Map                 = 3,
        LFGDungeon          = 4,
        Battleground        = 5,
        Unit                = 6,
        GameObject          = 7,
        CreatureDifficulty  = 8,
        Item                = 9,
        Quest               = 10,
        Opcode              = 11, // Packet
        PageText            = 12,
        NpcText             = 13,
        BroadcastText       = 14,
        Gossip              = 15,
        Zone                = 16,
        Area                = 17,
        AreaTrigger         = 18,
        Phase               = 19,
        Player              = 20,
        Achievement         = 21,
        CreatureFamily      = 22,
        Criteria            = 23,
        Currency            = 24,
        Difficulty          = 25,
        Faction             = 26,
        QuestGreeting       = 27,
        QuestObjective      = 28,
        Sound               = 29,
        PhaseId             = 30,
    }

    public static class StoreName
    {
        public static StoreNameType ToEnum<T>() where T : IId
        {
            if (typeof(T) == typeof(AchievementId))
                return StoreNameType.Achievement;
            if (typeof(T) == typeof(AreaId))
                return StoreNameType.Area;
            if (typeof(T) == typeof(BgId))
                return StoreNameType.Battleground;
            if (typeof(T) == typeof(CreatureFamilyId))
                return StoreNameType.CreatureFamily;
            if (typeof(T) == typeof(CriteriaId))
                return StoreNameType.Criteria;
            if (typeof(T) == typeof(CurrencyId))
                return StoreNameType.Currency;
            if (typeof(T) == typeof(DifficultyId))
                return StoreNameType.Difficulty;
            if (typeof(T) == typeof(FactionId))
                return StoreNameType.Faction;
            if (typeof(T) == typeof(GOId))
                return StoreNameType.GameObject;
            if (typeof(T) == typeof(ItemId))
                return StoreNameType.Item;
            if (typeof(T) == typeof(LFGDungeonId))
                return StoreNameType.LFGDungeon;
            if (typeof(T) == typeof(MapId))
                return StoreNameType.Map;
            if (typeof(T) == typeof(QuestId))
                return StoreNameType.Quest;
            if (typeof(T) == typeof(SoundId))
                return StoreNameType.Sound;
            if (typeof(T) == typeof(SpellId))
                return StoreNameType.Spell;
            if (typeof(T) == typeof(UnitId))
                return StoreNameType.Unit;
            if (typeof(T) == typeof(ZoneId))
                return StoreNameType.Zone;
            if (typeof(T) == typeof(PhaseId))
                return StoreNameType.PhaseId;

            throw new ArgumentOutOfRangeException(typeof(T).ToString());
        }
    }

    public interface IId { }

    public struct AchievementId : IId { }
    public struct AreaId : IId { }
    public struct BgId : IId { }
    public struct CreatureFamilyId : IId { }
    public struct CriteriaId : IId { }
    public struct CurrencyId : IId { }
    public struct DifficultyId : IId { }
    public struct FactionId : IId { }
    public struct GOId : IId { }
    public struct ItemId : IId { }
    public struct LFGDungeonId : IId { }
    public struct MapId : IId { }
    public struct QuestId : IId { }
    public struct SoundId : IId { }
    public struct SpellId :  IId { }
    public struct UnitId : IId { }
    public struct ZoneId : IId { }
    public struct PhaseId : IId { }
}
