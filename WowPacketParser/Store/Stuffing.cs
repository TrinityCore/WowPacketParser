using System;
using System.Collections.Concurrent;
using WowPacketParser.Enums;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Store
{
    public static class Stuffing // TODO: Rename
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

        
        /* Key: Misc */

        // Start info (Race, Class)
        public static readonly ConcurrentDictionary<Tuple<Race, Class>, StartInfo> StartInformation =
            new ConcurrentDictionary<Tuple<Race, Class>, StartInfo>();

        // Gossips (MenuId, TextId)
        public static readonly ConcurrentDictionary<Tuple<uint, uint>, Gossip> Gossips =
            new ConcurrentDictionary<Tuple<uint, uint>, Gossip>();

        // Loot (ItemId, LootType)
        public static readonly ConcurrentDictionary<Tuple<uint, ObjectType>, Loot> Loots =
            new ConcurrentDictionary<Tuple<uint, ObjectType>, Loot>();

        // Quest POI (QuestId, Id)
        public static readonly ConcurrentDictionary<Tuple<uint, uint>, QuestPOI> QuestPOIs =
            new ConcurrentDictionary<Tuple<uint, uint>, QuestPOI>();
    }
}
