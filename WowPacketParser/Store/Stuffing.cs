using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

        // Gossips
        public static readonly ConcurrentDictionary<uint, Gossip> Gossips =
            new ConcurrentDictionary<uint, Gossip>();


        /* Key: Misc */

        // Race-Class start information
        public static readonly ConcurrentDictionary<Tuple<Race, Class>, StartInfo> StartInformation =
            new ConcurrentDictionary<Tuple<Race, Class>, StartInfo>();

        // Loot
        public static readonly ConcurrentDictionary<Tuple<uint, LootType>, Loot> Loots =
            new ConcurrentDictionary<Tuple<uint, LootType>, Loot>();


        // Consider moving following code to a new class

        private const string cs = ", ";

        /// <summary>
        /// Escapes and quotes a string.
        /// </summary>
        private static string Stringify(string str)
        {
            return SQLUtilities.AddQuotes(SQLUtilities.EscapeString(str));
        }

        /// <summary>
        /// Converts an int to a string hex.
        /// </summary>
        private static string Hexify(int n)
        {
            return "0x" + n.ToString("X");
        }

        /// <summary>
        /// Creates a SQL DELETE query:
        /// "DELETE FROM `tableName` WHERE `primaryKey` IN (entry1, entry2, ..., entryN);"
        /// </summary>
        private static string DeleteQuerySingle(ICollection<uint> entries, string primaryKey, string tableName)
        {
            var result = "DELETE FROM " + SQLUtilities.AddBackQuotes(tableName) + " WHERE " +
                         SQLUtilities.AddBackQuotes(primaryKey) + " IN (";

            var iter = 0;
            foreach (var entry in entries)
            {
                iter++;
                result += entry;
                if (entries.Count != iter)
                    result += cs;
            }

            result += ");" + Environment.NewLine;

            return result;
        }

        /// <summary>
        /// Creates the upper part of a SQL INSERT query:
        /// "INSERT INTO `tableName` (`column1`,`column2`, ..., `columnN`) VALUES"
        /// </summary>
        private static string InsertQueryHeader(ICollection<string> tableStructure, string tableName)
        {
            var result = "INSERT INTO " + SQLUtilities.AddBackQuotes(tableName) + " (";
            var iter = 0;
            foreach (var column in tableStructure)
            {
                iter++;
                result += SQLUtilities.AddBackQuotes(column);
                if (tableStructure.Count != iter)
                    result += cs;
            }
            result += ") VALUES" + Environment.NewLine;

            return result;
        }

        public static string CreateQuestTemplateTestSQL()
        {
            var sqlQuery = new StringBuilder(string.Empty);

            const string tableName = "quest_template";
            const string primaryKey = "entry";
            string[] tableStructure = {
                                          "Id", "Method", "Level", "MinLevel", "Sort", "Type", "SuggestedPlayers",
                                          "RequiredFactionId1", "RequiredFactionId2", "RequiredFactionValue1",
                                          "RequiredFactionValue2", "NextQuestId", "RewardXPId", "RewardOrRequiredMoney",
                                          "RewardMoneyMaxLevel", "RewardSpell", "RewardSpellCast", "RewardHonor",
                                          "RewardHonorMultiplier", "SourceItemId", "Hexify((int)Flags)", "RewardTitleId"
                                          , "RequiredPlayerKills", "RewardTalents", "RewardArenaPoints",
                                          "RewardSkillPoints", "RewardReputationMask", "QuestGiverPortrait",
                                          "QuestTurnInPortrait", "UnknownUInt32", "RewardItemId1", "RewardItemId2",
                                          "RewardItemId3", "RewardItemId4", "RewardItemCount1", "RewardItemCount2",
                                          "RewardItemCount3", "RewardItemCount4", "RewardChoiceItemId1",
                                          "RewardChoiceItemId2", "RewardChoiceItemId3", "RewardChoiceItemId4",
                                          "RewardChoiceItemId5", "RewardChoiceItemId6", "RewardChoiceItemCount1",
                                          "RewardChoiceItemCount2", "RewardChoiceItemCount3", "RewardChoiceItemCount4",
                                          "RewardChoiceItemCount5", "RewardChoiceItemCount6", "RewardFactionId1",
                                          "RewardFactionId2", "RewardFactionId3", "RewardFactionId4", "RewardFactionId5"
                                          , "RewardReputationId1", "RewardReputationId2", "RewardReputationId3",
                                          "RewardReputationId4", "RewardReputationId5", "RewardReputationIdOverride1",
                                          "RewardReputationIdOverride2", "RewardReputationIdOverride3",
                                          "RewardReputationIdOverride4", "RewardReputationIdOverride5", "PointMapId",
                                          "PointX", "PointY", "PointOption", "Title", "Objectives", "Details", "EndText"
                                          , "ReturnText", "RequiredNpcOrGo1", "RequiredNpcOrGo2", "RequiredNpcOrGo3",
                                          "RequiredNpcOrGo4", "RequiredNpcOrGoCount1", "RequiredNpcOrGoCount2",
                                          "RequiredNpcOrGoCount3", "RequiredNpcOrGoCount4", "RequiredSourceItemId1",
                                          "RequiredSourceItemId2", "RequiredSourceItemId3", "RequiredSourceItemId4",
                                          "RequiredSourceItemCount1", "RequiredSourceItemCount2",
                                          "RequiredSourceItemCount3", "RequiredSourceItemCount4", "RequiredItemId1",
                                          "RequiredItemId2", "RequiredItemId3", "RequiredItemId4", "RequiredItemId5",
                                          "RequiredItemId6", "RequiredItemCount1", "RequiredItemCount2",
                                          "RequiredItemCount3", "RequiredItemCount4", "RequiredItemCount5",
                                          "RequiredItemCount6", "RequiredSpell", "ObjectiveTexts1", "ObjectiveTexts2",
                                          "ObjectiveTexts3", "ObjectiveTexts4", "RewardCurrencyId1", "RewardCurrencyId2"
                                          , "RewardCurrencyId3", "RewardCurrencyId4", "RewardCurrencyCount1",
                                          "RewardCurrencyCount2", "RewardCurrencyCount3", "RewardCurrencyCount4",
                                          "RequiredCurrencyId1", "RequiredCurrencyId2", "RequiredCurrencyId3",
                                          "RequiredCurrencyId4", "RequiredCurrencyCount1", "RequiredCurrencyCount2",
                                          "RequiredCurrencyCount3", "RequiredCurrencyCount4", "QuestGiverTextWindow",
                                          "QuestGiverTextName", "QuestTurnTextWindow", "QuestTurnTargetName",
                                          "SoundAccept", "SoundTurnIn"
                                      };

            // Delete
            sqlQuery.Append(DeleteQuerySingle(QuestTemplates.Keys, primaryKey, tableName));

            // Insert
            sqlQuery.Append(InsertQueryHeader(tableStructure, tableName));

            // Insert rows
            foreach (var quest in QuestTemplates)
            {
                sqlQuery.Append(
                    "(" +
                    quest.Key + cs +
                    (int) quest.Value.Method + cs +
                    quest.Value.Level + cs +
                    quest.Value.MinLevel + cs +
                    (int) quest.Value.Sort + cs +
                    (int) quest.Value.Type + cs +
                    quest.Value.SuggestedPlayers + cs);

                foreach (var n in quest.Value.RequiredFactionId)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RequiredFactionValue)
                    sqlQuery.Append(n + cs);

                sqlQuery.Append(
                    quest.Value.NextQuestId + cs +
                    quest.Value.RewardXPId + cs +
                    quest.Value.RewardOrRequiredMoney + cs +
                    quest.Value.RewardMoneyMaxLevel + cs +
                    quest.Value.RewardSpell + cs +
                    quest.Value.RewardSpellCast + cs +
                    quest.Value.RewardHonor + cs +
                    quest.Value.RewardHonorMultiplier + cs +
                    quest.Value.SourceItemId + cs +
                    Hexify((int)quest.Value.Flags) + cs +
                    quest.Value.RewardTitleId + cs +
                    quest.Value.RequiredPlayerKills + cs +
                    quest.Value.RewardTalents + cs +
                    quest.Value.RewardArenaPoints + cs +
                    // quest.Value.RewardUnknown + cs + // Always 0
                    quest.Value.RewardSkillPoints + cs +
                    quest.Value.RewardReputationMask + cs +
                    quest.Value.QuestGiverPortrait + cs +
                    quest.Value.QuestTurnInPortrait + cs +
                    quest.Value.UnknownUInt32 + cs);

                foreach (var n in quest.Value.RewardItemId)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RewardItemCount)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RewardChoiceItemId)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RewardChoiceItemCount)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RewardFactionId)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RewardReputationId)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RewardReputationIdOverride)
                    sqlQuery.Append(n + cs);

                sqlQuery.Append(
                    quest.Value.PointMapId + cs +
                    quest.Value.PointX + cs +
                    quest.Value.PointY + cs +
                    quest.Value.PointOption + cs +
                    Stringify(quest.Value.Title) + cs +
                    Stringify(quest.Value.Objectives) + cs +
                    Stringify(quest.Value.Details) + cs +
                    Stringify(quest.Value.EndText) + cs +
                    Stringify(quest.Value.ReturnText) + cs);

                foreach (var n in quest.Value.RequiredNpcOrGo)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RequiredNpcOrGoCount)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RequiredSourceItemId)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RequiredSourceItemCount)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RequiredItemId)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RequiredItemCount)
                    sqlQuery.Append(n + cs);

                sqlQuery.Append(quest.Value.RequiredSpell + cs);

                foreach (var s in quest.Value.ObjectiveTexts)
                    sqlQuery.Append(Stringify(s) + cs);
                foreach (var n in quest.Value.RewardCurrencyId)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RewardCurrencyCount)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RequiredCurrencyId)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RequiredCurrencyCount)
                    sqlQuery.Append(n + cs);

                sqlQuery.Append(
                    Stringify(quest.Value.QuestGiverTextWindow) + cs +
                    Stringify(quest.Value.QuestGiverTextName) + cs +
                    Stringify(quest.Value.QuestTurnTextWindow) + cs +
                    Stringify(quest.Value.QuestTurnTargetName) + cs +
                    quest.Value.SoundAccept + cs +
                    quest.Value.SoundTurnIn + ")," + " -- " + quest.Value.Title + Environment.NewLine);
            }

            for (var i = sqlQuery.Length - 1; i > 0; i--)
                if (sqlQuery[i] == ',')
                {
                    sqlQuery[i] = ';';
                    break;
                }

            return sqlQuery.ToString();
        }


        public static string CreateNpcTrainerTestSQL()
        {
            var sqlQuery = new StringBuilder(string.Empty);

            const string tableName = "npc_trainer";
            const string primaryKey = "entry";
            string[] tableStructure = {"entry", "spell", "spellcost", "reqskill", "reqskillvalue", "reqlevel"};

            // Delete
            sqlQuery.Append(DeleteQuerySingle(NpcTrainers.Keys, primaryKey, tableName));

            // Insert
            sqlQuery.Append(InsertQueryHeader(tableStructure, tableName));

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
            sqlQuery.Append(DeleteQuerySingle(NpcVendors.Keys, primaryKey, tableName));

            // Insert
            sqlQuery.Append(InsertQueryHeader(tableStructure, tableName));

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
