using System;
using System.Collections.Concurrent;
using WowPacketParser.Enums;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Store
{
    public static class Storage
    {
        // Stores opcodes read, npc/GOs/spell/item/etc IDs found in sniffs
        // and other miscellaneous stuff
        public static readonly ConcurrentBag<SniffData> SniffData =
            new ConcurrentBag<SniffData>();


        /* Key: Guid */

        // Units, GameObjects, Players, Items
        public static readonly ConcurrentDictionary<Guid, WoWObject> Objects =
            new ConcurrentDictionary<Guid, WoWObject>();


        /* Key: Entry */

        // Templates
        public static readonly ConcurrentDictionary<uint, GameObjectTemplate> GameObjectTemplates =
            new ConcurrentDictionary<uint, GameObjectTemplate>();
        public static readonly ConcurrentDictionary<uint, ItemTemplate> ItemTemplates =
            new ConcurrentDictionary<uint, ItemTemplate>();
        public static readonly ConcurrentDictionary<uint, QuestTemplate> QuestTemplates =
            new ConcurrentDictionary<uint, QuestTemplate>();
        public static readonly ConcurrentDictionary<uint, UnitTemplate> UnitTemplates =
            new ConcurrentDictionary<uint, UnitTemplate>();

        // Vendor & trainer
        public static readonly ConcurrentDictionary<uint, NpcTrainer> NpcTrainers =
            new ConcurrentDictionary<uint, NpcTrainer>();
        public static readonly ConcurrentDictionary<uint, NpcVendor> NpcVendors =
            new ConcurrentDictionary<uint, NpcVendor>();

        // Page & npc text
        public static readonly ConcurrentDictionary<uint, PageText> PageTexts =
            new ConcurrentDictionary<uint, PageText>();
        public static readonly ConcurrentDictionary<uint, NpcText> NpcTexts =
            new ConcurrentDictionary<uint, NpcText>();

        // Misc
        public static readonly ConcurrentDictionary<uint, SpellsX> SpellsX = // creature_template.spellX
            new ConcurrentDictionary<uint, SpellsX>();

        /* Key: Misc */

        // Start info (Race, Class)
        public static readonly ConcurrentDictionary<Tuple<Race, Class>, StartAction> StartActions =
            new ConcurrentDictionary<Tuple<Race, Class>, StartAction>();

        public static readonly ConcurrentDictionary<Tuple<Race, Class>, StartSpell> StartSpells =
            new ConcurrentDictionary<Tuple<Race, Class>, StartSpell>();

        public static readonly ConcurrentDictionary<Tuple<Race, Class>, StartPosition> StartPositions =
            new ConcurrentDictionary<Tuple<Race, Class>, StartPosition>();


        // Gossips (MenuId, TextId)
        public static readonly ConcurrentDictionary<Tuple<uint, uint>, Gossip> Gossips =
            new ConcurrentDictionary<Tuple<uint, uint>, Gossip>();

        // Loot (ItemId, LootType)
        public static readonly ConcurrentDictionary<Tuple<uint, ObjectType>, Loot> Loots =
            new ConcurrentDictionary<Tuple<uint, ObjectType>, Loot>();

        // Quest POI (QuestId, Id)
        public static readonly ConcurrentDictionary<Tuple<uint, uint>, QuestPOI> QuestPOIs =
            new ConcurrentDictionary<Tuple<uint, uint>, QuestPOI>();

        // Names
        public static readonly ConcurrentDictionary<uint, ObjectName> ObjectNames =
            new ConcurrentDictionary<uint, ObjectName>();
    }

    // Utilities extension methods to aid dealing with the above dictionaries
    public static class StorageExtensions
    {
        // Adds to or update an entry in Objects dictionary for SMSG_CHAR_ENUM player data
        public static void AddOrUpdate(this ConcurrentDictionary<Guid, WoWObject> dict, Guid guid, Player playerInfo)
        {
            dict.AddOrUpdate(guid, playerInfo, (guid1, wowObject) => Player.UpdatePlayerInfo((Player)wowObject, playerInfo));
        }
        
        // Maybe we should rename the above extension so this workaround isn't needed
        public static void AddOrUpdate<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict.AddOrUpdate(key, value, (key1, value1) => (value));
        }
    }
}
