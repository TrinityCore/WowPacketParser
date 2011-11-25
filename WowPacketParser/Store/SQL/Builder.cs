using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.SQL
{
    public static class Builder
    {
        private const string cs = SQLUtil.CommaSeparator;

        public static string QuestTemplate()
        {
            if (Stuffing.QuestTemplates.IsEmpty)
                return string.Empty;

            var sqlQuery = new StringBuilder(String.Empty);

            // Not TDB structure
            const string tableName = "quest_template";
            const string primaryKey = "Id";
            string[] tableStructure = {
                                          "Id", "Method", "Level", "MinLevel", "ZoneOrSort", "Type", "SuggestedPlayers",
                                          "RequiredFactionId1", "RequiredFactionId2", "RequiredFactionValue1",
                                          "RequiredFactionValue2", "NextQuestId", "RewardXPId", "RewardOrRequiredMoney",
                                          "RewardMoneyMaxLevel", "RewardSpell", "RewardSpellCast", "RewardHonor",
                                          "RewardHonorMultiplier", "SourceItemId", "Flags", "RewardTitleId",
                                          "RequiredPlayerKills", "RewardTalents", "RewardArenaPoints", "RewardSkillId",
                                          "RewardSkillPoints", "RewardReputationMask", "QuestGiverPortrait",
                                          "QuestTurnInPortrait", "RewardItemId1", "RewardItemId2",
                                          "RewardItemId3", "RewardItemId4", "RewardItemCount1", "RewardItemCount2",
                                          "RewardItemCount3", "RewardItemCount4", "RewardChoiceItemId1",
                                          "RewardChoiceItemId2", "RewardChoiceItemId3", "RewardChoiceItemId4",
                                          "RewardChoiceItemId5", "RewardChoiceItemId6", "RewardChoiceItemCount1",
                                          "RewardChoiceItemCount2", "RewardChoiceItemCount3", "RewardChoiceItemCount4",
                                          "RewardChoiceItemCount5", "RewardChoiceItemCount6", "RewardFactionId1",
                                          "RewardFactionId2", "RewardFactionId3", "RewardFactionId4", "RewardFactionId5",
                                          "RewardReputationId1", "RewardReputationId2", "RewardReputationId3",
                                          "RewardReputationId4", "RewardReputationId5", "RewardReputationIdOverride1",
                                          "RewardReputationIdOverride2", "RewardReputationIdOverride3",
                                          "RewardReputationIdOverride4", "RewardReputationIdOverride5", "PointMapId",
                                          "PointX", "PointY", "PointOption", "Title", "Objectives", "Details", "EndText",
                                          "ReturnText", "RequiredNpcOrGo1", "RequiredNpcOrGo2", "RequiredNpcOrGo3",
                                          "RequiredNpcOrGo4", "RequiredNpcOrGoCount1", "RequiredNpcOrGoCount2",
                                          "RequiredNpcOrGoCount3", "RequiredNpcOrGoCount4", "RequiredSourceItemId1",
                                          "RequiredSourceItemId2", "RequiredSourceItemId3", "RequiredSourceItemId4",
                                          "RequiredSourceItemCount1", "RequiredSourceItemCount2",
                                          "RequiredSourceItemCount3", "RequiredSourceItemCount4", "RequiredItemId1",
                                          "RequiredItemId2", "RequiredItemId3", "RequiredItemId4", "RequiredItemId5",
                                          "RequiredItemId6", "RequiredItemCount1", "RequiredItemCount2",
                                          "RequiredItemCount3", "RequiredItemCount4", "RequiredItemCount5",
                                          "RequiredItemCount6", "RequiredSpell", "ObjectiveText1", "ObjectiveText2",
                                          "ObjectiveText3", "ObjectiveText4", "RewardCurrencyId1", "RewardCurrencyId2",
                                          "RewardCurrencyId3", "RewardCurrencyId4", "RewardCurrencyCount1",
                                          "RewardCurrencyCount2", "RewardCurrencyCount3", "RewardCurrencyCount4",
                                          "RequiredCurrencyId1", "RequiredCurrencyId2", "RequiredCurrencyId3",
                                          "RequiredCurrencyId4", "RequiredCurrencyCount1", "RequiredCurrencyCount2",
                                          "RequiredCurrencyCount3", "RequiredCurrencyCount4", "QuestGiverTextWindow",
                                          "QuestGiverTextName", "QuestTurnTextWindow", "QuestTurnTargetName",
                                          "SoundAccept", "SoundTurnIn"
                                      };

            // Delete
            sqlQuery.Append(SQLUtil.DeleteQuerySingle(Stuffing.QuestTemplates.Keys, primaryKey, tableName));

            // Insert
            sqlQuery.Append(SQLUtil.InsertQueryHeader(tableStructure, tableName));

            // Insert rows
            foreach (var quest in Stuffing.QuestTemplates)
            {
                sqlQuery.Append(
                    "(" +
                    quest.Key + cs +
                    (int)quest.Value.Method + cs +
                    quest.Value.Level + cs +
                    quest.Value.MinLevel + cs +
                    (int)quest.Value.ZoneOrSort + cs +
                    (int)quest.Value.Type + cs +
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
                    SQLUtil.Hexify((int)quest.Value.Flags) + cs +
                    quest.Value.RewardTitleId + cs +
                    quest.Value.RequiredPlayerKills + cs +
                    quest.Value.RewardTalents + cs +
                    quest.Value.RewardArenaPoints + cs +
                    // quest.Value.RewardUnknown + cs + // Always 0
                    quest.Value.RewardSkillId + cs +
                    quest.Value.RewardSkillPoints + cs +
                    quest.Value.RewardReputationMask + cs +
                    quest.Value.QuestGiverPortrait + cs +
                    quest.Value.QuestTurnInPortrait + cs);

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
                    SQLUtil.Stringify(quest.Value.Title) + cs +
                    SQLUtil.Stringify(quest.Value.Objectives) + cs +
                    SQLUtil.Stringify(quest.Value.Details) + cs +
                    SQLUtil.Stringify(quest.Value.EndText) + cs +
                    SQLUtil.Stringify(quest.Value.ReturnText) + cs);

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
                    sqlQuery.Append(SQLUtil.Stringify(s) + cs);
                foreach (var n in quest.Value.RewardCurrencyId)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RewardCurrencyCount)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RequiredCurrencyId)
                    sqlQuery.Append(n + cs);
                foreach (var n in quest.Value.RequiredCurrencyCount)
                    sqlQuery.Append(n + cs);

                sqlQuery.Append(
                    SQLUtil.Stringify(quest.Value.QuestGiverTextWindow) + cs +
                    SQLUtil.Stringify(quest.Value.QuestGiverTextName) + cs +
                    SQLUtil.Stringify(quest.Value.QuestTurnTextWindow) + cs +
                    SQLUtil.Stringify(quest.Value.QuestTurnTargetName) + cs +
                    quest.Value.SoundAccept + cs +
                    quest.Value.SoundTurnIn + ")," + " -- " + quest.Value.Title + Environment.NewLine);
            }

            return sqlQuery.ReplaceLast(',', ';').ToString();
        }

        public static string NpcTrainer()
        {
            if (Stuffing.NpcTrainers.IsEmpty)
                return string.Empty;

            var sqlQuery = new StringBuilder(String.Empty);

            const string tableName = "npc_trainer";
            const string primaryKey = "entry";
            string[] tableStructure = { "entry", "spell", "spellcost", "reqskill", "reqskillvalue", "reqlevel" };

            // Delete
            sqlQuery.Append(SQLUtil.DeleteQuerySingle(Stuffing.NpcTrainers.Keys, primaryKey, tableName));

            // Insert
            sqlQuery.Append(SQLUtil.InsertQueryHeader(tableStructure, tableName));

            // Insert rows
            foreach (var npcTrainer in Stuffing.NpcTrainers)
            {
                sqlQuery.Append("-- " + StoreGetters.GetName(StoreNameType.Unit, (int)npcTrainer.Key) +
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
                                    StoreGetters.GetName(StoreNameType.Spell, (int)trainerSpell.Spell, false) +
                                    Environment.NewLine);
                }
            }

            return sqlQuery.ReplaceLast(',', ';').ToString();
        }

        public static string NpcVendor()
        {
            if (Stuffing.NpcVendors.IsEmpty)
                return string.Empty;

            var sqlQuery = new StringBuilder(String.Empty);

            const string tableName = "npc_vendor";
            const string primaryKey = "entry";
            string[] tableStructure = { "entry", "slot", "item", "maxcount", "ExtendedCost" };

            // Delete
            sqlQuery.Append(SQLUtil.DeleteQuerySingle(Stuffing.NpcVendors.Keys, primaryKey, tableName));

            // Insert
            sqlQuery.Append(SQLUtil.InsertQueryHeader(tableStructure, tableName));

            // Insert rows
            foreach (var npcVendor in Stuffing.NpcVendors)
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

        public static string NpcTemplate()
        {
            if (Stuffing.UnitTemplates.IsEmpty)
                return string.Empty;

            var sqlQuery = new StringBuilder(String.Empty);

            // Not TDB structure
            const string tableName = "creature_template";
            const string primaryKey = "Id";
            string[] tableStructure = {
                                          "Id", "Name", "SubName", "IconName", "TypeFlags", "TypeFlags2", "Type ",
                                          "Family ", "Rank ", "KillCredit1 ", "KillCredit2 ", "UnkInt ", "PetSpellData",
                                          "DisplayId1", "DisplayId2", "DisplayId3", "DisplayId4", "Modifier1 ",
                                          "Modifier2 ", "RacialLeader", "QuestItem1", "QuestItem2", "QuestItem3",
                                          "QuestItem4", "QuestItem5", "QuestItem6", "MovementId ", "Expansion"
                                      };

            // Delete
            sqlQuery.Append(SQLUtil.DeleteQuerySingle(Stuffing.UnitTemplates.Keys, primaryKey, tableName));

            // Insert
            sqlQuery.Append(SQLUtil.InsertQueryHeader(tableStructure, tableName));

            // Insert rows
            foreach (var unitTemplate in Stuffing.UnitTemplates)
            {
                sqlQuery.Append(
                    "(" +
                    unitTemplate.Key + cs +
                    SQLUtil.Stringify(unitTemplate.Value.Name) + cs +
                    SQLUtil.Stringify(unitTemplate.Value.SubName) + cs +
                    SQLUtil.Stringify(unitTemplate.Value.IconName) + cs +
                    SQLUtil.Hexify((int) unitTemplate.Value.TypeFlags) + cs +
                    SQLUtil.Hexify((int) unitTemplate.Value.TypeFlags2) + cs +
                    (int) unitTemplate.Value.Type + cs +
                    (int) unitTemplate.Value.Family + cs +
                    (int) unitTemplate.Value.Rank + cs +
                    unitTemplate.Value.KillCredit1 + cs +
                    unitTemplate.Value.KillCredit2 + cs +
                    unitTemplate.Value.UnkInt + cs +
                    unitTemplate.Value.PetSpellData + cs);

                foreach (var n in unitTemplate.Value.DisplayIds)
                    sqlQuery.Append(n + cs);

                sqlQuery.Append(
                    unitTemplate.Value.Modifier1 + cs +
                    unitTemplate.Value.Modifier2 + cs +
                    (unitTemplate.Value.RacialLeader ? 1 : 0) + cs);

                foreach (var n in unitTemplate.Value.QuestItems)
                    sqlQuery.Append(n + cs);

                sqlQuery.Append(
                    unitTemplate.Value.MovementId + cs +
                    (int) unitTemplate.Value.Expansion + ")," + Environment.NewLine);

            }

            return sqlQuery.ReplaceLast(',', ';').ToString();
        }

        public static string GameObjectTemplate()
        {
            if (Stuffing.GameObjectTemplates.IsEmpty)
                return string.Empty;

            var sqlQuery = new StringBuilder(String.Empty);

            // Not TDB structure (data got 32 fields, not 24)
            const string tableName = "gameobject_template";
            const string primaryKey = "Id";
            string[] tableStructure = {
                                          "Id", "Type", "DisplayId", "Name", "IconName", "CastCaption", "UnkString",
                                          "Data1", "Data2", "Data3", "Data4", "Data5", "Data6", "Data7", "Data8",
                                          "Data9", "Data10", "Data11", "Data12", "Data13", "Data14", "Data15", "Data16",
                                          "Data17", "Data18", "Data19", "Data20", "Data21", "Data22", "Data23", "Data24",
                                          "Data25", "Data26", "Data27", "Data28", "Data29", "Data30", "Data31",
                                          "Data32", "Size", "QuestItem1", "QuestItem2", "QuestItem3", "QuestItem4",
                                          "QuestItem5", "QuestItem6", "UnknownUInt"
                                      };

            // Delete
            sqlQuery.Append(SQLUtil.DeleteQuerySingle(Stuffing.GameObjectTemplates.Keys, primaryKey, tableName));

            // Insert
            sqlQuery.Append(SQLUtil.InsertQueryHeader(tableStructure, tableName));

            // Insert rows
            foreach (var goTemplate in Stuffing.GameObjectTemplates)
            {
                sqlQuery.Append(
                    "(" +
                    goTemplate.Key + cs +
                    (int) goTemplate.Value.Type + cs +
                    goTemplate.Value.DisplayId + cs +
                    SQLUtil.Stringify(goTemplate.Value.Name) + cs +
                    SQLUtil.Stringify(goTemplate.Value.IconName) + cs +
                    SQLUtil.Stringify(goTemplate.Value.CastCaption) + cs +
                    SQLUtil.Stringify(goTemplate.Value.UnkString) + cs);

                foreach (var n in goTemplate.Value.Data)
                    sqlQuery.Append(n + cs);

                sqlQuery.Append(
                    goTemplate.Value.Size + cs);

                foreach (var n in goTemplate.Value.QuestItems)
                    sqlQuery.Append(n + cs);

                sqlQuery.Append(
                    goTemplate.Value.UnknownUInt + ")," + Environment.NewLine);
            }

            return sqlQuery.ReplaceLast(',', ';').ToString();
        }

        public static string PageText()
        {
            if (Stuffing.PageTexts.IsEmpty)
                return string.Empty;

            var sqlQuery = new StringBuilder(String.Empty);

            const string tableName = "page_Text";
            const string primaryKey = "entry";
            string[] tableStructure = {"entry", "text", "next_page"};

            // Delete
            sqlQuery.Append(SQLUtil.DeleteQuerySingle(Stuffing.PageTexts.Keys, primaryKey, tableName));

            // Insert
            sqlQuery.Append(SQLUtil.InsertQueryHeader(tableStructure, tableName));

            // Insert rows
            foreach (var pageText in Stuffing.PageTexts)
            {
                sqlQuery.Append(
                    "(" +
                    pageText.Key + cs +
                    SQLUtil.Stringify(pageText.Value.Text) + cs +
                    pageText.Value.NextPageId + ")," + Environment.NewLine);
            }

            return sqlQuery.ReplaceLast(',', ';').ToString();
        }

        public static string NpcText()
        {
            if (Stuffing.NpcTexts.IsEmpty)
                return string.Empty;

            var sqlQuery = new StringBuilder(String.Empty);

            // Not TDB structure (data got 32 fields, not 24)
            const string tableName = "npc_text";
            const string primaryKey = "Id";
            string[] tableStructure = {
                                          "Id", "Probability1", "Probability2", "Probability3", "Probability4",
                                          "Probability5", "Probability6", "Probability7", "Probability8", "Text1_1",
                                          "Text1_2", "Text1_3", "Text1_4", "Text1_5", "Text1_6", "Text1_7", "Text1_8",
                                          "Text2_1", "Text2_2", "Text2_3", "Text2_4", "Text2_5", "Text2_6", "Text2_7",
                                          "Text2_8", "Language1", "Language2", "Language3", "Language4", "Language5",
                                          "Language6", "Language7", "Language8", "EmoteId1_1", "EmoteId1_2",
                                          "EmoteId1_3", "EmoteId2_1", "EmoteId2_2", "EmoteId2_3", "EmoteId3_1",
                                          "EmoteId3_2", "EmoteId3_3", "EmoteId4_1", "EmoteId4_2", "EmoteId4_3",
                                          "EmoteId5_1", "EmoteId5_2", "EmoteId5_3", "EmoteId6_1", "EmoteId6_2",
                                          "EmoteId6_3", "EmoteId7_1", "EmoteId7_2", "EmoteId7_3", "EmoteId8_1",
                                          "EmoteId8_2", "EmoteId8_3", "EmoteId1_1", "EmoteId1_2", "EmoteId1_3",
                                          "EmoteId2_1", "EmoteId2_2", "EmoteId2_3", "EmoteId3_1", "EmoteId3_2",
                                          "EmoteId3_3", "EmoteId4_1", "EmoteId4_2", "EmoteId4_3", "EmoteId5_1",
                                          "EmoteId5_2", "EmoteId5_3", "EmoteId6_1", "EmoteId6_2", "EmoteId6_3",
                                          "EmoteId7_1", "EmoteId7_2", "EmoteId7_3", "EmoteId8_1", "EmoteId8_2",
                                          "EmoteId8_3"
                                      };

            // Delete
            sqlQuery.Append(SQLUtil.DeleteQuerySingle(Stuffing.NpcTexts.Keys, primaryKey, tableName));

            // Insert
            sqlQuery.Append(SQLUtil.InsertQueryHeader(tableStructure, tableName));

            // Insert rows
            foreach (var npcText in Stuffing.NpcTexts)
            {
                sqlQuery.Append("(" + npcText.Key + cs);

                foreach (var n in npcText.Value.Probabilities)
                    sqlQuery.Append(n + cs);

                foreach (var s in npcText.Value.Texts1)
                    sqlQuery.Append(SQLUtil.Stringify(s) + cs);

                foreach (var s in npcText.Value.Texts2)
                    sqlQuery.Append(SQLUtil.Stringify(s) + cs);

                foreach (int n in npcText.Value.Languages)
                    sqlQuery.Append(n + cs);

                foreach (var a in npcText.Value.EmoteDelays)
                    foreach (var n in a)
                        sqlQuery.Append(n + cs);

                var itr = 0;
                foreach (var a in npcText.Value.EmoteIds)
                    foreach (int n in a)
                    {
                        itr++;
                        sqlQuery.Append(n);
                        if (itr != npcText.Value.EmoteIds.Length * a.Length)
                            sqlQuery.Append(cs);
                    }

                sqlQuery.Append(")," + Environment.NewLine);
            }

            return sqlQuery.ReplaceLast(',', ';').ToString();
        }

        public static string Gossip()
        {
            if (Stuffing.Gossips.IsEmpty)
                return string.Empty;

            var sqlQuery = new StringBuilder(String.Empty);

            // Not TDB structure (data got 32 fields, not 24)
            const string tableName1 = "gossip_menu";
            string[] primaryKey1 = {"entry", "text_id"};
            string[] tableStructure1 = {"entry", "text_id"};

            const string tableName2 = "gossip_menu_option";
            const string primaryKey2 = "menu_id";
            string[] tableStructure2 = {
                                           "menu_id", "id", "option_icon", "option_text", "box_coded",
                                           "box_money", "box_text"
                                       };

            // Delete1
            sqlQuery.Append(SQLUtil.DeleteQueryDouble(Stuffing.Gossips.Keys, primaryKey1, tableName1));

            // Insert1
            sqlQuery.Append(SQLUtil.InsertQueryHeader(tableStructure1, tableName1));

            // Insert1 rows
            foreach (var pair in Stuffing.Gossips.Keys)
                sqlQuery.Append("(" + pair.Item1 + cs + pair.Item2 + ")," + Environment.NewLine);

            sqlQuery = sqlQuery.ReplaceLast(',', ';');

            // We need a collection of the first items of a tuple
            var keyCollection = new Collection<uint>();
            foreach (var key in Stuffing.Gossips.Keys)
                keyCollection.Add(key.Item1);

            // Delete2
            sqlQuery.Append(SQLUtil.DeleteQuerySingle(keyCollection, primaryKey2, tableName2));

            // If no gossip options exists, return what we got so far
            if (!Stuffing.Gossips.Values.Any(gossip => gossip.GossipOptions != null))
                return sqlQuery.ToString();

            // Insert2
            sqlQuery.Append(SQLUtil.InsertQueryHeader(tableStructure2, tableName2));

            // Insert2 rows
            foreach (var gossip in Stuffing.Gossips)
            {
                if (gossip.Value.GossipOptions != null)
                    foreach (var gossipOption in gossip.Value.GossipOptions)
                    {
                        sqlQuery.Append(
                            "(" +
                            gossip.Key.Item1 + cs +
                            gossipOption.Index + cs +
                            gossipOption.OptionIcon + cs +
                            SQLUtil.Stringify(gossipOption.OptionText) + cs +
                            (gossipOption.Box ? "1" : "0") + cs +
                            gossipOption.RequiredMoney + cs +
                            SQLUtil.Stringify(gossipOption.BoxText) + ")," + Environment.NewLine);
                    }
            }

            return sqlQuery.ReplaceLast(',', ';').ToString();
        }

        public static string Loot()
        {
            if (Stuffing.Loots.IsEmpty)
                return string.Empty;

            var sqlQuery = new StringBuilder(String.Empty);

            // Not TDB structure
            const string tableName = "LootTemplate";
            string[] primaryKey = { "Id", "Type" };
            string[] tableStructure = {"Id", "Type", "ItemId", "Count"};

            // Can't cast the collection directly
            ICollection<Tuple<uint, uint>> lootKeys = new Collection<Tuple<uint, uint>>();
            foreach (var tuple in Stuffing.Loots.Keys)
            {
                lootKeys.Add(new Tuple<uint, uint>(tuple.Item1, (uint)tuple.Item2));
            }

            // Delete
            sqlQuery.Append(SQLUtil.DeleteQueryDouble(lootKeys, primaryKey, tableName));

            // Insert
            sqlQuery.Append(SQLUtil.InsertQueryHeader(tableStructure, tableName));

            // Insert rows
            foreach (var loot in Stuffing.Loots)
            {
                StoreNameType storeType = StoreNameType.None;
                switch (Stuffing.Loots.Keys.First().Item2)
                {
                    case ObjectType.Item:
                        storeType = StoreNameType.Item;
                        break;
                    case ObjectType.Corpse:
                    case ObjectType.Unit:
                        storeType = StoreNameType.Unit;
                        break;
                    case ObjectType.Container:
                    case ObjectType.GameObject:
                        storeType = StoreNameType.GameObject;
                        break;
                }
                sqlQuery.Append("-- " + StoreGetters.GetName(storeType, (int) loot.Key.Item1) +
                                "(" + loot.Value.Gold + " gold)" + Environment.NewLine);
                foreach (var lootItem in loot.Value.LootItems)
                {
                    sqlQuery.Append(
                        "(" +
                        loot.Key.Item1 + cs +
                        (int) loot.Key.Item2 + cs +
                        lootItem.ItemId + cs +
                        lootItem.Count + ")," + " -- " +
                        StoreGetters.GetName(StoreNameType.Item, (int)lootItem.ItemId, false) +
                        Environment.NewLine);

                }
            }

            return sqlQuery.ReplaceLast(',', ';').ToString();
        }
    }
}
