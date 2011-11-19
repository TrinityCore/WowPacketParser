using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
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

        /// <summary>
        /// Waypoints, key: npc entry, point id
        /// Waypoints are currently written with npc Entry instead of GUID * 10, since our GUIDs differ from retails GUIDs
        /// </summary>
        public static ConcurrentDictionary<Tuple<uint,uint>, Waypoint> Waypoints =
            new ConcurrentDictionary<Tuple<uint, uint>, Waypoint>();

        /* Key: npc entry, menuid */
        public static ConcurrentDictionary<Tuple<uint,uint>, GossipMenu> Gossips =
            new ConcurrentDictionary<Tuple<uint, uint>, GossipMenu>();

        /* Key: Misc */

        // Race, Class
        public static ConcurrentDictionary<Tuple<Race, Class>, StartInfo> StartInformation =
            new ConcurrentDictionary<Tuple<Race, Class>, StartInfo>();


        public static string CreateNpcTrainerTestSQL()
        {
            var sqlQuery = new StringBuilder(string.Empty);

            const string tableName = "npc_trainer";
            const string primaryKey = "entry";
            string[] tableStructure = {"entry", "spell", "spellcost", "reqskill", "reqskillvalue", "reqlevel"};

            // Delete
            sqlQuery.Append("DELETE FROM " + SQLUtilities.AddBackQuotes(tableName) + " WHERE ");

            var iter = 0;
            foreach (var npcTrainer in NpcTrainers.Keys)
            {
                iter++;
                sqlQuery.Append(SQLUtilities.AddBackQuotes(primaryKey) + "=" + npcTrainer);
                if (NpcTrainers.Count != iter)
                    sqlQuery.Append(" OR ");
            }

            sqlQuery.Append(";" + Environment.NewLine);

            // Insert
            sqlQuery.Append("INSERT INTO " + SQLUtilities.AddBackQuotes(tableName) + " (");
            iter = 0;
            foreach (var collumn in tableStructure)
            {
                iter++;
                sqlQuery.Append(SQLUtilities.AddBackQuotes(collumn));
                if (tableStructure.Length != iter)
                    sqlQuery.Append(",");
            }
            sqlQuery.Append(") VALUES" + Environment.NewLine);

            // Insert rows
            foreach (var npcTrainer in NpcTrainers)
            {
                sqlQuery.Append("-- " + StoreGetters.GetName(StoreNameType.Unit, (int) npcTrainer.Key) +
                                Environment.NewLine);
                foreach (var trainerSpell in npcTrainer.Value.TrainerSpells)
                {
                    sqlQuery.Append("(" +
                                    npcTrainer.Key + ", " +
                                    trainerSpell.Spell + ", " +
                                    trainerSpell.Cost + ", " +
                                    trainerSpell.RequiredSkill + ", " +
                                    trainerSpell.RequiredSkillLevel + ", " +
                                    trainerSpell.RequiredLevel + ")," + " -- " +
                                    StoreGetters.GetName(StoreNameType.Spell, (int) trainerSpell.Spell, false) +
                                    Environment.NewLine);
                }
            }

            for (int i = sqlQuery.Length - 1; i > 0; i--)
                if (sqlQuery[i] == ',')
                {
                    sqlQuery[i] = ';';
                    break;
                }

            return sqlQuery.ToString();
        }

        public static string CreateNpcVendorTestSQL()
        {
            var sqlQuery = new StringBuilder(string.Empty);

            const string tableName = "npc_vendor";
            const string primaryKey = "entry";
            string[] tableStructure = {"entry", "slot", "item", "maxcount", "ExtendedCost"};

            // Delete
            sqlQuery.Append("DELETE FROM " + SQLUtilities.AddBackQuotes(tableName) + " WHERE ");

            var iter = 0;
            foreach (var npcVendor in NpcVendors.Keys)
            {
                iter++;
                sqlQuery.Append(SQLUtilities.AddBackQuotes(primaryKey) + "=" + npcVendor);
                if (NpcVendors.Count != iter)
                    sqlQuery.Append(" OR ");
            }
            sqlQuery.Append(";" + Environment.NewLine);

            // Insert
            sqlQuery.Append("INSERT INTO " + SQLUtilities.AddBackQuotes(tableName) + " (");
            iter = 0;
            foreach (var collumn in tableStructure)
            {
                iter++;
                sqlQuery.Append(SQLUtilities.AddBackQuotes(collumn));
                if (tableStructure.Length != iter)
                    sqlQuery.Append(",");
            }
            sqlQuery.Append(") VALUES" + Environment.NewLine);

            // Insert rows
            foreach (var npcVendor in NpcVendors)
            {
                sqlQuery.Append("-- " + StoreGetters.GetName(StoreNameType.Unit, (int)npcVendor.Key) +
                                Environment.NewLine);
                foreach (var vendorItem in npcVendor.Value.VendorItems)
                {
                    sqlQuery.Append("(" +
                                    npcVendor.Key + ", " +
                                    vendorItem.Slot + ", " +
                                    vendorItem.ItemId + ", " +
                                    vendorItem.MaxCount + ", " +
                                    vendorItem.ExtendedCostId + ")," + " -- " +
                                    StoreGetters.GetName(StoreNameType.Item, (int)vendorItem.ItemId, false) +
                                    Environment.NewLine);
                }
            }

            return sqlQuery.ReplaceLast(',', ';').ToString();
        }
    }
}
