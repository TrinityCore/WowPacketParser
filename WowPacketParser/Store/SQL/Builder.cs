using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        public static string SniffData()
        {
            if (Stuffing.SniffData.IsEmpty)
                return string.Empty;

            const string tableName = "SniffData";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var data in Stuffing.SniffData)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Build",      data.FileInfo.Build);
                row.AddValue("SniffName",  (Path.GetFileName(data.FileInfo.FileName)));
                row.AddValue("TimeStamp",  data.TimeStamp);
                row.AddValue("ObjectType", data.ObjectType.ToString());
                row.AddValue("Id",         data.Id);
                row.AddValue("Data",       data.Data);
                row.AddValue("Number",     data.Number);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows, true).Build();
        }

        public static string QuestTemplate()
        {
            if (Stuffing.QuestTemplates.IsEmpty)
                return string.Empty;

            const string tableName = "quest_template";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var quest in Stuffing.QuestTemplates)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Id"                                      , quest.Key);
                row.AddValue("Method"                                  , quest.Value.Method);
                row.AddValue("Level"                                   , quest.Value.Level);
                row.AddValue("MinLevel"                                , quest.Value.MinLevel);
                row.AddValue("ZoneOrSort"                              , quest.Value.ZoneOrSort);
                row.AddValue("Type"                                    , quest.Value.Type);
                row.AddValue("SuggestedPlayers"                        , quest.Value.SuggestedPlayers);
                
                for (var i = 0; i < quest.Value.RequiredFactionId.Count(); i++)
                    row.AddValue("RequiredFactionId" + (i + 1)         , quest.Value.RequiredFactionId[i]);
                
                for (var i = 0; i < quest.Value.RequiredFactionId.Count(); i++)
                    row.AddValue("RequiredFactionValue" + (i + 1)      , quest.Value.RequiredFactionValue[i]);
                
                row.AddValue("NextQuestIdChain"                        , quest.Value.NextQuestIdChain);
                row.AddValue("RewardXPId"                              , quest.Value.RewardXPId);
                row.AddValue("RewardOrRequiredMoney"                   , quest.Value.RewardOrRequiredMoney);
                row.AddValue("RewardMoneyMaxLevel"                     , quest.Value.RewardMoneyMaxLevel);
                row.AddValue("RewardSpell"                             , quest.Value.RewardSpell);
                row.AddValue("RewardSpellCast"                         , quest.Value.RewardSpellCast);
                row.AddValue("RewardHonor"                             , quest.Value.RewardHonor);
                row.AddValue("RewardHonorMultiplier"                   , quest.Value.RewardHonorMultiplier);
                row.AddValue("SourceItemId"                            , quest.Value.SourceItemId);
                row.AddValue("Flags"                                   , quest.Value.Flags, true);
                row.AddValue("MinimapTargetMark"                       , quest.Value.MinimapTargetMark);
                row.AddValue("RewardTitleId"                           , quest.Value.RewardTitleId);
                row.AddValue("RequiredPlayerKills"                     , quest.Value.RequiredPlayerKills);
                row.AddValue("RewardTalents"                           , quest.Value.RewardTalents);
                row.AddValue("RewardArenaPoints"                       , quest.Value.RewardArenaPoints);
                row.AddValue("RewardSkillId"                           , quest.Value.RewardSkillId);
                row.AddValue("RewardSkillPoints"                       , quest.Value.RewardSkillPoints);
                row.AddValue("RewardReputationMask"                    , quest.Value.RewardReputationMask);
                row.AddValue("QuestGiverPortrait"                      , quest.Value.QuestGiverPortrait);
                row.AddValue("QuestTurnInPortrait"                     , quest.Value.QuestTurnInPortrait);
                
                for (var i = 0; i < quest.Value.RewardItemId.Count(); i++)
                    row.AddValue("RewardItemId" + (i + 1)              , quest.Value.RewardItemId[i]);
                
                for (var i = 0; i < quest.Value.RewardItemCount.Count(); i++)
                    row.AddValue("RewardItemCount" + (i + 1)           , quest.Value.RewardItemCount[i]);
                
                for (var i = 0; i < quest.Value.RewardChoiceItemId.Count(); i++)
                    row.AddValue("RewardChoiceItemId" + (i + 1)        , quest.Value.RewardChoiceItemId[i]);
                
                for (var i = 0; i < quest.Value.RewardChoiceItemCount.Count(); i++)
                    row.AddValue("RewardChoiceItemCount" + (i + 1)     , quest.Value.RewardChoiceItemCount[i]);
                
                for (var i = 0; i < quest.Value.RewardFactionId.Count(); i++)
                    row.AddValue("RewardFactionId" + (i + 1)           , quest.Value.RewardFactionId[i]);
                
                for (var i = 0; i < quest.Value.RewardFactionValueId.Count(); i++)
                    row.AddValue("RewardFactionValueId" + (i + 1)      , quest.Value.RewardFactionValueId[i]);
                
                for (var i = 0; i < quest.Value.RewardFactionValueIdOverride.Count(); i++)
                    row.AddValue("RewardFactionValueIdOverride" + (i + 1), quest.Value.RewardFactionValueIdOverride[i]);
                
                row.AddValue("PointMapId"                              , quest.Value.PointMapId);
                row.AddValue("PointX"                                  , quest.Value.PointX);
                row.AddValue("PointY"                                  , quest.Value.PointY);
                row.AddValue("PointOption"                             , quest.Value.PointOption);
                row.AddValue("Title"                                   , quest.Value.Title);
                row.AddValue("Objectives"                              , quest.Value.Objectives);
                row.AddValue("Details"                                 , quest.Value.Details);
                row.AddValue("EndText"                                 , quest.Value.EndText);
                row.AddValue("CompletedText"                           , quest.Value.CompletedText);
                
                for (var i = 0; i < quest.Value.RequiredNpcOrGo.Count(); i++)
                    row.AddValue("RequiredNpcOrGo" + (i + 1)           , quest.Value.RequiredNpcOrGo[i]);
                
                for (var i = 0; i < quest.Value.RequiredNpcOrGoCount.Count(); i++)
                    row.AddValue("RequiredNpcOrGoCount" + (i + 1)      , quest.Value.RequiredNpcOrGoCount[i]);
                
                for (var i = 0; i < quest.Value.RequiredSourceItemId.Count(); i++)
                    row.AddValue("RequiredSourceItemId" + (i + 1)      , quest.Value.RequiredSourceItemId[i]);
                
                for (var i = 0; i < quest.Value.RequiredSourceItemCount.Count(); i++)
                    row.AddValue("RequiredSourceItemCount" + (i + 1)   , quest.Value.RequiredSourceItemCount[i]);
                
                for (var i = 0; i < quest.Value.RequiredItemId.Count(); i++)
                    row.AddValue("RequiredItemId" + (i + 1)            , quest.Value.RequiredItemId[i]);
                
                for (var i = 0; i < quest.Value.RequiredItemCount.Count(); i++)
                    row.AddValue("RequiredItemCount" + (i + 1)         , quest.Value.RequiredItemCount[i]);
                
                for (var i = 0; i < quest.Value.RequiredSourceItemCount.Count(); i++)
                    row.AddValue("RequiredSourceItemCount" + (i + 1)   , quest.Value.RequiredSourceItemCount[i]);
                
                row.AddValue("RequiredSpell"                           , quest.Value.RequiredSpell);
                
                for (var i = 0; i < quest.Value.ObjectiveText.Count(); i++)
                    row.AddValue("ObjectiveText" + (i + 1)             , quest.Value.ObjectiveText[i]);
                
                for (var i = 0; i < quest.Value.RewardCurrencyId.Count(); i++)
                    row.AddValue("RewardCurrencyId" + (i + 1)          , quest.Value.RewardCurrencyId[i]);
                
                for (var i = 0; i < quest.Value.RewardCurrencyCount.Count(); i++)
                    row.AddValue("RewardCurrencyCount" + (i + 1)       , quest.Value.RewardCurrencyCount[i]);
                
                for (var i = 0; i < quest.Value.RequiredCurrencyId.Count(); i++)
                    row.AddValue("RequiredCurrencyId" + (i + 1)        , quest.Value.RequiredCurrencyId[i]);
                
                for (var i = 0; i < quest.Value.RequiredCurrencyCount.Count(); i++)
                    row.AddValue("RequiredCurrencyCount" + (i + 1)     , quest.Value.RequiredCurrencyCount[i]);
                
                row.AddValue("QuestGiverTextWindow"                    , quest.Value.QuestGiverTextWindow);
                row.AddValue("QuestGiverTargetName"                    , quest.Value.QuestGiverTargetName);
                row.AddValue("QuestTurnTextWindow"                     , quest.Value.QuestTurnTextWindow);
                row.AddValue("QuestTurnTargetName"                     , quest.Value.QuestTurnTargetName);
                row.AddValue("SoundAccept"                             , quest.Value.SoundAccept);
                row.AddValue("SoundTurnIn"                             , quest.Value.SoundTurnIn);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, Stuffing.QuestTemplates.Keys, new[] { "Id" }, rows).Build();
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
