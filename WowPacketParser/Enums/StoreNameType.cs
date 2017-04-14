using System;

namespace WowPacketParser.Enums
{
    public enum StoreNameType
    {
        None,
        Achievement,
        Area,
        AreaTrigger,
        Battleground,
        CreatureFamily,
        Criteria,
        Currency,
        Difficulty,
        Faction,
        GameObject,
        Gossip,
        Item,
        LFGDungeon,
        Map,
        NpcText,
        Opcode, // Packet
        PageText,
        Phase,
        Player,
        Quest,
        QuestGreeting,
        QuestObjective,
        Sound,
        Spell,
        Unit,
        Zone,
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
}
