﻿using System;
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

        /// <summary>
        /// Waypoints, key: npc entry, point id
        /// Waypoints are currently written with npc Entry instead of GUID * 10, since our GUIDs differ from retails GUIDs
        /// </summary>
        public static ConcurrentDictionary<Tuple<uint,uint>, Waypoint> Waypoints =
            new ConcurrentDictionary<Tuple<uint, uint>, Waypoint>();

        // Gossips
        /* Key: npc entry, menuid */
        public static ConcurrentDictionary<Tuple<uint,uint>, GossipMenu> Gossips =
            new ConcurrentDictionary<Tuple<uint, uint>, GossipMenu>();

        /* Key: Misc */

        // Race-Class start information
        public static readonly ConcurrentDictionary<Tuple<Race, Class>, StartInfo> StartInformation =
            new ConcurrentDictionary<Tuple<Race, Class>, StartInfo>();

        // Loot
        public static readonly ConcurrentDictionary<Tuple<uint, LootType>, Loot> Loots =
            new ConcurrentDictionary<Tuple<uint, LootType>, Loot>();
    }
}
