using System;
using System.Collections.Generic;
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
        public static ConcurrentDictionary<uint, NpcTrainer> NpcTrainers =
            new ConcurrentDictionary<uint, NpcTrainer>();
        public static ConcurrentDictionary<uint, NpcVendor> NpcVendors =
            new ConcurrentDictionary<uint, NpcVendor>();

        // Page & npc text
        public static ConcurrentDictionary<uint, PageText> PageTexts =
            new ConcurrentDictionary<uint, PageText>();
        public static ConcurrentDictionary<uint, NpcText> NpcTexts =
            new ConcurrentDictionary<uint, NpcText>();

        // Loot
        public static ConcurrentDictionary<uint, Loot> Loots =
            new ConcurrentDictionary<uint, Loot>();

        // Quests
        public static ConcurrentDictionary<uint, QuestTemplate> Quests =
            new ConcurrentDictionary<uint, QuestTemplate>();

        /* Key: npc entry, menuid */
        public static ConcurrentDictionary<Tuple<uint,uint>, GossipMenu> Gossips =
            new ConcurrentDictionary<Tuple<uint, uint>, GossipMenu>();

        /* Key: Misc */

        // Race, Class
        public static ConcurrentDictionary<Tuple<Race, Class>, StartInfo> StartInformation =
            new ConcurrentDictionary<Tuple<Race, Class>, StartInfo>();
    }
}
