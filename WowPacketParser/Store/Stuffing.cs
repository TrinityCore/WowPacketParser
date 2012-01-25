using System;
using System.Collections.Concurrent;
using WowPacketParser.Enums;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Store
{
    public class Stuffing // TODO: Rename
    {
        // Stores opcodes read, npc/GOs/spell/item/etc IDs found in sniffs
        // and other miscellaneous stuff
        public readonly ConcurrentBag<SniffData> SniffData =
            new ConcurrentBag<SniffData>();


        /* Key: Guid */

        // Units, Game.Objects, Players, Items
        public readonly ConcurrentDictionary<Guid, WoWObject> Objects =
            new ConcurrentDictionary<Guid, WoWObject>();


        /* Key: Entry */

        // Templates
        public readonly ConcurrentDictionary<uint, GameObjectTemplate> GameObjectTemplates =
            new ConcurrentDictionary<uint, GameObjectTemplate>();
        public readonly ConcurrentDictionary<uint, ItemTemplate> ItemTemplates =
            new ConcurrentDictionary<uint, ItemTemplate>();
        public readonly ConcurrentDictionary<uint, QuestTemplate> QuestTemplates =
            new ConcurrentDictionary<uint, QuestTemplate>();
        public readonly ConcurrentDictionary<uint, UnitTemplate> UnitTemplates =
            new ConcurrentDictionary<uint, UnitTemplate>();

        // Vendor & trainer
        public readonly ConcurrentDictionary<uint, NpcTrainer> NpcTrainers =
            new ConcurrentDictionary<uint, NpcTrainer>();
        public readonly ConcurrentDictionary<uint, NpcVendor> NpcVendors =
            new ConcurrentDictionary<uint, NpcVendor>();

        // Page & npc text
        public readonly ConcurrentDictionary<uint, PageText> PageTexts =
            new ConcurrentDictionary<uint, PageText>();
        public readonly ConcurrentDictionary<uint, NpcText> NpcTexts =
            new ConcurrentDictionary<uint, NpcText>();


        /* Key: Misc */

        // Start info (Race, Class)
        public readonly ConcurrentDictionary<Tuple<Race, Class>, StartAction> StartActions =
            new ConcurrentDictionary<Tuple<Race, Class>, StartAction>();

        public readonly ConcurrentDictionary<Tuple<Race, Class>, StartSpell> StartSpells =
            new ConcurrentDictionary<Tuple<Race, Class>, StartSpell>();

        public readonly ConcurrentDictionary<Tuple<Race, Class>, StartPosition> StartPositions =
            new ConcurrentDictionary<Tuple<Race, Class>, StartPosition>();


        // Gossips (MenuId, TextId)
        public readonly ConcurrentDictionary<Tuple<uint, uint>, Gossip> Gossips =
            new ConcurrentDictionary<Tuple<uint, uint>, Gossip>();

        // Loot (ItemId, LootType)
        public readonly ConcurrentDictionary<Tuple<uint, ObjectType>, Loot> Loots =
            new ConcurrentDictionary<Tuple<uint, ObjectType>, Loot>();

        // Quest POI (QuestId, Id)
        public readonly ConcurrentDictionary<Tuple<uint, uint>, QuestPOI> QuestPOIs =
            new ConcurrentDictionary<Tuple<uint, uint>, QuestPOI>();

        // Names
        public readonly ConcurrentDictionary<uint, ObjectName> ObjectNames =
            new ConcurrentDictionary<uint, ObjectName>();
    }
}
