using System;
using System.Collections.Concurrent;
using WowPacketParser.Enums;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Store
{
    public static class Stuffing // TODO: Rename
    {
        /* Key: Guid */

        // Units, GameObjects, Players, Items
        public static ConcurrentDictionary<Guid, WoWObject> Objects =
            new ConcurrentDictionary<Guid, WoWObject>();


        /* Key: Entry */

        // Templates
        public static ConcurrentDictionary<uint, GameObjectTemplate> GameObjectTemplates =
            new ConcurrentDictionary<uint, GameObjectTemplate>();
        public static ConcurrentDictionary<uint, ItemTemplate> ItemTemplates =
            new ConcurrentDictionary<uint, ItemTemplate>();
        public static ConcurrentDictionary<uint, QuestTemplate> QuestTemplates =
            new ConcurrentDictionary<uint, QuestTemplate>();
        public static ConcurrentDictionary<uint, UnitTemplate> UnitTemplates =
            new ConcurrentDictionary<uint, UnitTemplate>();

        // Vendor & trainer
        public static ConcurrentDictionary<uint, TrainerSpell> TrainerSpells =
            new ConcurrentDictionary<uint, TrainerSpell>();
        public static ConcurrentDictionary<uint, VendorItem> VendorItems =
            new ConcurrentDictionary<uint, VendorItem>();

        // Page & npc text
        public static ConcurrentDictionary<uint, PageText> PageTexts =
            new ConcurrentDictionary<uint, PageText>();
        public static ConcurrentDictionary<uint, NpcText> NpcTexts =
            new ConcurrentDictionary<uint, NpcText>();

        // Loot
        public static ConcurrentDictionary<uint, Loot> Loots =
            new ConcurrentDictionary<uint, Loot>();

        // Gossips
        public static ConcurrentDictionary<uint, Gossip> Gossips =
            new ConcurrentDictionary<uint, Gossip>();


        /* Key: Misc */

        // Race, Class
        public static ConcurrentDictionary<Tuple<Race, Class>, StartInfo> StartInformation =
            new ConcurrentDictionary<Tuple<Race, Class>, StartInfo>();
    }
}
