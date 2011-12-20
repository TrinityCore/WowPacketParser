using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Store.SQL
{
    public static class Builder
    {
        public static string CreatureSpawns()
        {
            if (!Stuffing.Objects.Any(wowObject => wowObject.Value.Type == ObjectType.Unit))
                return string.Empty;

            var units = Stuffing.Objects.Where(x => x.Value.Type == ObjectType.Unit);

            const string tableName = "creature";

            ICollection<Tuple<uint, uint>> keys = new Collection<Tuple<uint, uint>>();
            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var unit in units)
            {
                var row = new QueryBuilder.SQLInsertRow();

                var creature = unit.Value;

                // If our unit got any of the folowing updated fields set,
                // it's probably a temporary spawn
                UpdateField uf;
                creature.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_SUMMONEDBY), out uf);
                creature.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_CREATED_BY_SPELL), out uf);
                creature.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_CREATEDBY), out uf);
                var temporarySpawn = (uf != null && uf.Int32Value != 0);
                row.CommentOut = temporarySpawn;

                // If map is Eastern Kingdoms, Kalimdor, Outland, Northrend or Ebon Hold use a lower respawn time
                // TODO: Rank and if npc is needed for quest kill should change spawntime as well
                var spawnTimeSecs = (unit.Value.Map == 0 || unit.Value.Map == 1 || unit.Value.Map == 530 ||
                                     unit.Value.Map == 571 || unit.Value.Map == 609) ? 120 : 7200;
                var movementType = 0; // TODO: Find a way to check if our unit got random movement
                var spawnDist = (movementType == 1) ? 5 : 0;

                row.AddValue("guid", unit.Key.GetLow());
                row.AddValue("id", unit.Key.GetEntry());
                row.AddValue("map", creature.Map);
                row.AddValue("spawnMask", 1);
                row.AddValue("phaseMask", creature.PhaseMask);
                row.AddValue("position_x", creature.Movement.Position.X);
                row.AddValue("position_y", creature.Movement.Position.Y);
                row.AddValue("position_z", creature.Movement.Position.Z);
                row.AddValue("orientation", creature.Movement.Orientation);
                row.AddValue("spawntimesecs", spawnTimeSecs);
                row.AddValue("spawndist", spawnDist);
                row.AddValue("MovementType", movementType);
                row.Comment = StoreGetters.GetName(StoreNameType.Unit, (int) unit.Key.GetEntry(), false);
                if (temporarySpawn)
                    row.Comment += " - !!! might be temporary spawn !!!";

                rows.Add(row);
                keys.Add(new Tuple<uint, uint>((uint) unit.Key.GetLow(), unit.Key.GetEntry()));
            }

            return new QueryBuilder.SQLInsert(tableName, keys, new[] { "guid", "id" }, rows).Build();
        }

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

                row.AddValue("Id", quest.Key);
                row.AddValue("Method", quest.Value.Method);
                row.AddValue("Level", quest.Value.Level);
                row.AddValue("MinLevel", quest.Value.MinLevel);
                row.AddValue("ZoneOrSort", quest.Value.ZoneOrSort);
                row.AddValue("Type", quest.Value.Type);
                row.AddValue("SuggestedPlayers", quest.Value.SuggestedPlayers);

                for (var i = 0; i < quest.Value.RequiredFactionId.Count(); i++)
                    row.AddValue("RequiredFactionId" + (i + 1), quest.Value.RequiredFactionId[i]);

                for (var i = 0; i < quest.Value.RequiredFactionId.Count(); i++)
                    row.AddValue("RequiredFactionValue" + (i + 1), quest.Value.RequiredFactionValue[i]);

                row.AddValue("NextQuestIdChain", quest.Value.NextQuestIdChain);
                row.AddValue("RewardXPId", quest.Value.RewardXPId);
                row.AddValue("RewardOrRequiredMoney", quest.Value.RewardOrRequiredMoney);
                row.AddValue("RewardMoneyMaxLevel", quest.Value.RewardMoneyMaxLevel);
                row.AddValue("RewardSpell", quest.Value.RewardSpell);
                row.AddValue("RewardSpellCast", quest.Value.RewardSpellCast);
                row.AddValue("RewardHonor", quest.Value.RewardHonor);
                row.AddValue("RewardHonorMultiplier", quest.Value.RewardHonorMultiplier);
                row.AddValue("SourceItemId", quest.Value.SourceItemId);
                row.AddValue("Flags", quest.Value.Flags, true);
                row.AddValue("MinimapTargetMark", quest.Value.MinimapTargetMark);
                row.AddValue("RewardTitleId", quest.Value.RewardTitleId);
                row.AddValue("RequiredPlayerKills", quest.Value.RequiredPlayerKills);
                row.AddValue("RewardTalents", quest.Value.RewardTalents);
                row.AddValue("RewardArenaPoints", quest.Value.RewardArenaPoints);
                row.AddValue("RewardSkillId", quest.Value.RewardSkillId);
                row.AddValue("RewardSkillPoints", quest.Value.RewardSkillPoints);
                row.AddValue("RewardReputationMask", quest.Value.RewardReputationMask);
                row.AddValue("QuestGiverPortrait", quest.Value.QuestGiverPortrait);
                row.AddValue("QuestTurnInPortrait", quest.Value.QuestTurnInPortrait);

                for (var i = 0; i < quest.Value.RewardItemId.Count(); i++)
                    row.AddValue("RewardItemId" + (i + 1), quest.Value.RewardItemId[i]);

                for (var i = 0; i < quest.Value.RewardItemCount.Count(); i++)
                    row.AddValue("RewardItemCount" + (i + 1), quest.Value.RewardItemCount[i]);

                for (var i = 0; i < quest.Value.RewardChoiceItemId.Count(); i++)
                    row.AddValue("RewardChoiceItemId" + (i + 1), quest.Value.RewardChoiceItemId[i]);

                for (var i = 0; i < quest.Value.RewardChoiceItemCount.Count(); i++)
                    row.AddValue("RewardChoiceItemCount" + (i + 1), quest.Value.RewardChoiceItemCount[i]);

                for (var i = 0; i < quest.Value.RewardFactionId.Count(); i++)
                    row.AddValue("RewardFactionId" + (i + 1), quest.Value.RewardFactionId[i]);

                for (var i = 0; i < quest.Value.RewardFactionValueId.Count(); i++)
                    row.AddValue("RewardFactionValueId" + (i + 1), quest.Value.RewardFactionValueId[i]);

                for (var i = 0; i < quest.Value.RewardFactionValueIdOverride.Count(); i++)
                    row.AddValue("RewardFactionValueIdOverride" + (i + 1), quest.Value.RewardFactionValueIdOverride[i]);

                row.AddValue("PointMapId", quest.Value.PointMapId);
                row.AddValue("PointX", quest.Value.PointX);
                row.AddValue("PointY", quest.Value.PointY);
                row.AddValue("PointOption", quest.Value.PointOption);
                row.AddValue("Title", quest.Value.Title);
                row.AddValue("Objectives", quest.Value.Objectives);
                row.AddValue("Details", quest.Value.Details);
                row.AddValue("EndText", quest.Value.EndText);
                row.AddValue("CompletedText", quest.Value.CompletedText);

                for (var i = 0; i < quest.Value.RequiredNpcOrGo.Count(); i++)
                    row.AddValue("RequiredNpcOrGo" + (i + 1), quest.Value.RequiredNpcOrGo[i]);

                for (var i = 0; i < quest.Value.RequiredNpcOrGoCount.Count(); i++)
                    row.AddValue("RequiredNpcOrGoCount" + (i + 1), quest.Value.RequiredNpcOrGoCount[i]);

                for (var i = 0; i < quest.Value.RequiredSourceItemId.Count(); i++)
                    row.AddValue("RequiredSourceItemId" + (i + 1), quest.Value.RequiredSourceItemId[i]);

                for (var i = 0; i < quest.Value.RequiredSourceItemCount.Count(); i++)
                    row.AddValue("RequiredSourceItemCount" + (i + 1), quest.Value.RequiredSourceItemCount[i]);

                for (var i = 0; i < quest.Value.RequiredItemId.Count(); i++)
                    row.AddValue("RequiredItemId" + (i + 1), quest.Value.RequiredItemId[i]);

                for (var i = 0; i < quest.Value.RequiredItemCount.Count(); i++)
                    row.AddValue("RequiredItemCount" + (i + 1), quest.Value.RequiredItemCount[i]);

                for (var i = 0; i < quest.Value.RequiredSourceItemCount.Count(); i++)
                    row.AddValue("RequiredSourceItemCount" + (i + 1), quest.Value.RequiredSourceItemCount[i]);

                row.AddValue("RequiredSpell", quest.Value.RequiredSpell);

                for (var i = 0; i < quest.Value.ObjectiveText.Count(); i++)
                    row.AddValue("ObjectiveText" + (i + 1), quest.Value.ObjectiveText[i]);

                for (var i = 0; i < quest.Value.RewardCurrencyId.Count(); i++)
                    row.AddValue("RewardCurrencyId" + (i + 1), quest.Value.RewardCurrencyId[i]);

                for (var i = 0; i < quest.Value.RewardCurrencyCount.Count(); i++)
                    row.AddValue("RewardCurrencyCount" + (i + 1), quest.Value.RewardCurrencyCount[i]);

                for (var i = 0; i < quest.Value.RequiredCurrencyId.Count(); i++)
                    row.AddValue("RequiredCurrencyId" + (i + 1), quest.Value.RequiredCurrencyId[i]);

                for (var i = 0; i < quest.Value.RequiredCurrencyCount.Count(); i++)
                    row.AddValue("RequiredCurrencyCount" + (i + 1), quest.Value.RequiredCurrencyCount[i]);

                row.AddValue("QuestGiverTextWindow", quest.Value.QuestGiverTextWindow);
                row.AddValue("QuestGiverTargetName", quest.Value.QuestGiverTargetName);
                row.AddValue("QuestTurnTextWindow", quest.Value.QuestTurnTextWindow);
                row.AddValue("QuestTurnTargetName", quest.Value.QuestTurnTargetName);
                row.AddValue("SoundAccept", quest.Value.SoundAccept);
                row.AddValue("SoundTurnIn", quest.Value.SoundTurnIn);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, Stuffing.QuestTemplates.Keys, "Id", rows).Build();
        }

        public static string NpcTrainer()
        {
            if (Stuffing.NpcTrainers.IsEmpty)
                return string.Empty;

            const string tableName = "npc_trainer";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcTrainer in Stuffing.NpcTrainers)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment = StoreGetters.GetName(StoreNameType.Unit, (int) npcTrainer.Key, false);
                rows.Add(comment);
                foreach (var trainerSpell in npcTrainer.Value.TrainerSpells)
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue("entry", npcTrainer.Key);
                    row.AddValue("spell", trainerSpell.Spell);
                    row.AddValue("spellcost", trainerSpell.Cost);
                    row.AddValue("reqskill", trainerSpell.RequiredSkill);
                    row.AddValue("reqskillvalue", trainerSpell.RequiredSkillLevel);
                    row.AddValue("reqlevel", trainerSpell.RequiredLevel);
                    row.Comment = StoreGetters.GetName(StoreNameType.Spell, (int) trainerSpell.Spell, false);
                    rows.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, Stuffing.NpcTrainers.Keys, "entry", rows).Build();
        }

        public static string NpcVendor()
        {
            if (Stuffing.NpcVendors.IsEmpty)
                return string.Empty;

            const string tableName = "npc_vendor";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcVendor in Stuffing.NpcVendors)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment = StoreGetters.GetName(StoreNameType.Unit, (int)npcVendor.Key);
                rows.Add(comment);
                foreach (var vendorItem in npcVendor.Value.VendorItems)
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue("entry", npcVendor.Key);
                    row.AddValue("slot", vendorItem.Slot);
                    row.AddValue("item", vendorItem.ItemId);
                    row.AddValue("maxcount", vendorItem.MaxCount);
                    row.AddValue("ExtendedCost", vendorItem.ExtendedCostId);
                    row.Comment = StoreGetters.GetName(StoreNameType.Item, (int)vendorItem.ItemId, false);
                    rows.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, Stuffing.NpcVendors.Keys, "entry", rows).Build();
        }

        public static string NpcTemplate()
        {
            if (Stuffing.UnitTemplates.IsEmpty)
                return string.Empty;

            // Not TDB structure
            const string tableName = "creature_template";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var unitTemplate in Stuffing.UnitTemplates)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Id", unitTemplate.Key);
                row.AddValue("Name", unitTemplate.Value.Name);
                row.AddValue("SubName", unitTemplate.Value.SubName);
                row.AddValue("IconName", unitTemplate.Value.IconName);
                row.AddValue("TypeFlags", unitTemplate.Value.TypeFlags);
                row.AddValue("TypeFlags2", unitTemplate.Value.TypeFlags2);
                row.AddValue("Type", unitTemplate.Value.Type);
                row.AddValue("Family", unitTemplate.Value.Family);
                row.AddValue("Rank", unitTemplate.Value.Rank);
                row.AddValue("KillCredit1", unitTemplate.Value.KillCredit1);
                row.AddValue("KillCredit2", unitTemplate.Value.KillCredit2);
                row.AddValue("UnkInt", unitTemplate.Value.UnkInt);
                row.AddValue("PetSpellData", unitTemplate.Value.PetSpellData);

                for (var i = 0; i < unitTemplate.Value.DisplayIds.Count(); i++)
                    row.AddValue("DisplayId" + (i + 1), unitTemplate.Value.DisplayIds[i]);

                row.AddValue("Modifier1", unitTemplate.Value.Modifier1);
                row.AddValue("Modifier2", unitTemplate.Value.Modifier2);
                row.AddValue("RacialLeader", unitTemplate.Value.RacialLeader);

                for (var i = 0; i < unitTemplate.Value.QuestItems.Count(); i++)
                    row.AddValue("QuestItem" + (i + 1), unitTemplate.Value.QuestItems[i]);

                row.AddValue("MovementId", unitTemplate.Value.MovementId);
                row.AddValue("Expansion", unitTemplate.Value.Expansion);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, Stuffing.UnitTemplates.Keys, "Id", rows).Build();
        }

        public static string GameObjectTemplate()
        {
            if (Stuffing.GameObjectTemplates.IsEmpty)
                return string.Empty;

            // Not TDB structure
            const string tableName = "gameobject_template";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var goTemplate in Stuffing.GameObjectTemplates)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Id", goTemplate.Key);
                row.AddValue("Type", goTemplate.Value.Type);
                row.AddValue("DisplayId", goTemplate.Value.DisplayId);
                row.AddValue("Name", goTemplate.Value.Name);
                row.AddValue("IconName", goTemplate.Value.IconName);
                row.AddValue("CastCaption", goTemplate.Value.CastCaption);
                row.AddValue("UnkString", goTemplate.Value.UnkString);

                for (var i = 0; i < goTemplate.Value.Data.Count(); i++)
                    row.AddValue("Data" + (i + 1), goTemplate.Value.Data[i]);

                row.AddValue("Size", goTemplate.Value.Size);

                for (var i = 0; i < goTemplate.Value.QuestItems.Count(); i++)
                    row.AddValue("QuestItem" + (i + 1), goTemplate.Value.QuestItems[i]);

                row.AddValue("UnknownUInt", goTemplate.Value.UnknownUInt);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, Stuffing.GameObjectTemplates.Keys, "Id", rows).Build();
        }

        public static string PageText()
        {
            if (Stuffing.PageTexts.IsEmpty)
                return string.Empty;

            const string tableName = "page_Text";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var pageText in Stuffing.PageTexts)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("entry", pageText.Key);
                row.AddValue("text", pageText.Value.Text);
                row.AddValue("next_page", pageText.Value.NextPageId);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, Stuffing.PageTexts.Keys, "entry", rows).Build();
        }

        public static string NpcText()
        {
            if (Stuffing.NpcTexts.IsEmpty)
                return string.Empty;

            // Not TDB structure
            const string tableName = "npc_text";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcText in Stuffing.NpcTexts)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Id", npcText.Key);

                for (var i = 0; i < npcText.Value.Probabilities.Count(); i++)
                    row.AddValue("Probability" + (i + 1), npcText.Value.Probabilities[i]);

                for (var i = 0; i < npcText.Value.Texts1.Count(); i++)
                    row.AddValue("Text1_" + (i + 1), npcText.Value.Texts1[i]);

                for (var i = 0; i < npcText.Value.Texts2.Count(); i++)
                    row.AddValue("Text2_" + (i + 1), npcText.Value.Texts2[i]);

                for (var i = 0; i < npcText.Value.Languages.Count(); i++)
                    row.AddValue("Language" + (i + 1), npcText.Value.Languages[i]);

                for (var i = 0; i < npcText.Value.EmoteDelays[0].Count(); i++)
                    for (var j = 0; j < npcText.Value.EmoteDelays[1].Count(); j++)
                        row.AddValue("EmoteDelay" + (i + 1) + "_" + (j + 1), npcText.Value.EmoteDelays[i][j]);

                for (var i = 0; i < npcText.Value.EmoteIds[0].Count(); i++)
                    for (var j = 0; j < npcText.Value.EmoteIds[1].Count(); j++)
                        row.AddValue("EmoteId" + (i + 1) + "_" + (j + 1), npcText.Value.EmoteDelays[i][j]);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, Stuffing.NpcTexts.Keys, "Id", rows).Build();
        }

        public static string Gossip()
        {
            if (Stuffing.Gossips.IsEmpty)
                return string.Empty;

            const string tableName1 = "gossip_menu";
            const string tableName2 = "gossip_menu_option";

            // TODO: Add creature_template gossip_menu_id update query or similar

            // `gossip`
            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var gossip in Stuffing.Gossips)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("entry", gossip.Key.Item1);
                row.AddValue("text_id", gossip.Key.Item2);
                row.Comment = StoreGetters.GetName(Utilities.ObjectTypeToStore(gossip.Value.ObjectType),
                                                   (int) gossip.Value.ObjectEntry, false);

                rows.Add(row);
            }

            var result = new QueryBuilder.SQLInsert(tableName1, Stuffing.Gossips.Keys, new[] { "entry", "text_id" }, rows).Build();

            // `gossip_menu_option`
            rows = new List<QueryBuilder.SQLInsertRow>();
            ICollection<Tuple<uint, uint>> keys = new Collection<Tuple<uint, uint>>();
            foreach (var gossip in Stuffing.Gossips)
            {
                if (gossip.Value.GossipOptions != null) // Needed?
                    foreach (var gossipOption in gossip.Value.GossipOptions)
                    {
                        var row = new QueryBuilder.SQLInsertRow();

                        row.AddValue("menu_id", gossip.Key.Item1);
                        row.AddValue("id", gossipOption.Index);
                        row.AddValue("option_icon", gossipOption.OptionIcon);
                        row.AddValue("option_text", gossipOption.OptionText);
                        row.AddValue("box_coded", gossipOption.Box);
                        row.AddValue("box_money", gossipOption.RequiredMoney);
                        row.AddValue("box_text", gossipOption.BoxText);

                        rows.Add(row);

                        keys.Add(new Tuple<uint, uint>(gossip.Key.Item1, gossipOption.Index));
                    }
            }

            result += new QueryBuilder.SQLInsert(tableName2, keys, new[] { "menu_id", "id" }, rows).Build();

            return result;
        }

        public static string QuestPOI()
        {
            if (Stuffing.QuestPOIs.IsEmpty)
                return string.Empty;

            const string tableName1 = "quest_poi";
            const string tableName2 = "quest_poi_points";

            // Trying something..
            var orderedDict = Stuffing.QuestPOIs.OrderBy(key => key.Key.Item1);

            // `quest_poi`
            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var quest in orderedDict)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("questId", quest.Key.Item1);
                row.AddValue("id", quest.Key.Item2);
                row.AddValue("objIndex", quest.Value.ObjectiveIndex);
                row.AddValue("mapid", quest.Value.Map);
                row.AddValue("WorldMapAreaId", quest.Value.WorldMapAreaId);
                row.AddValue("FloorId", quest.Value.FloorId);
                row.AddValue("unk3", quest.Value.UnkInt1);
                row.AddValue("unk4", quest.Value.UnkInt2);
                row.Comment = StoreGetters.GetName(StoreNameType.Quest, (int) quest.Key.Item1, false);

                rows.Add(row);
            }

            var result = new QueryBuilder.SQLInsert(tableName1, Stuffing.QuestPOIs.Keys, new[] { "questId", "id" }, rows).Build();

            // `quest_poi_points`
            rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var quest in orderedDict)
            {
                if (quest.Value.Points != null) // Needed?
                    foreach (var point in quest.Value.Points)
                    {
                        var row = new QueryBuilder.SQLInsertRow();

                        row.AddValue("questId", quest.Key.Item1);
                        row.AddValue("id", quest.Key.Item2);
                        row.AddValue("idx", point.Index); // Not on sniffs
                        row.AddValue("x", point.X);
                        row.AddValue("y", point.Y);
                        row.Comment = StoreGetters.GetName(StoreNameType.Quest, (int)quest.Key.Item1, false);

                        rows.Add(row);
                    }
            }

            result += new QueryBuilder.SQLInsert(tableName2, Stuffing.QuestPOIs.Keys, new[] { "questId", "id" }, rows).Build();

            return result;
        }

        public static string Loot()
        {
            if (Stuffing.Loots.IsEmpty)
                return string.Empty;

            // Not TDB structure
            const string tableName = "LootTemplate";

            // Can't cast the collection directly
            ICollection<Tuple<uint, uint>> lootKeys = new Collection<Tuple<uint, uint>>();
            foreach (var tuple in Stuffing.Loots.Keys)
                lootKeys.Add(new Tuple<uint, uint>(tuple.Item1, (uint)tuple.Item2));

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var loot in Stuffing.Loots)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment =
                    StoreGetters.GetName(Utilities.ObjectTypeToStore(Stuffing.Loots.Keys.First().Item2), (int) loot.Key.Item1, false) +
                                        " (" + loot.Value.Gold + " gold)";
                rows.Add(comment);
                foreach (var lootItem in loot.Value.LootItems)
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue("Id", loot.Key.Item1);
                    row.AddValue("Type", loot.Key.Item2);
                    row.AddValue("ItemId", lootItem.ItemId);
                    row.AddValue("Count", lootItem.Count);
                    row.Comment = StoreGetters.GetName(StoreNameType.Item, (int)lootItem.ItemId, false);

                    rows.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, lootKeys, new[] {"Id", "Type" }, rows).Build();
        }

        public static string StartInformation()
        {
            var result = string.Empty;

            if (!Stuffing.StartActions.IsEmpty)
            {
                // Can't cast the collection directly
                ICollection<Tuple<uint, uint>> keys = new Collection<Tuple<uint, uint>>();
                foreach (var key in Stuffing.StartActions.Keys)
                    keys.Add(new Tuple<uint, uint>((uint) key.Item1, (uint)key.Item2));

                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var startActions in Stuffing.StartActions)
                {
                    var comment = new QueryBuilder.SQLInsertRow();
                    comment.HeaderComment = startActions.Key.Item1 + " - " + startActions.Key.Item2;
                    rows.Add(comment);

                    foreach (var action in startActions.Value.Actions)
                    {
                        var row = new QueryBuilder.SQLInsertRow();

                        row.AddValue("race", startActions.Key.Item1);
                        row.AddValue("class", startActions.Key.Item2);
                        row.AddValue("button", action.Button);
                        row.AddValue("action", action.Id);
                        row.AddValue("type", action.Type);
                        if (action.Type == ActionButtonType.Spell)
                            row.Comment = StoreGetters.GetName(StoreNameType.Spell, (int) action.Id, false);
                        if (action.Type == ActionButtonType.Item)
                            row.Comment = StoreGetters.GetName(StoreNameType.Item, (int) action.Id, false);

                        rows.Add(row);
                    }
                }

                result = new QueryBuilder.SQLInsert("playercreateinfo_action", keys, new[] { "race", "class" }, rows).Build();
            }

            if (!Stuffing.StartPositions.IsEmpty)
            {
                // Can't cast the collection directly
                ICollection<Tuple<uint, uint>> keys = new Collection<Tuple<uint, uint>>();
                foreach (var key in Stuffing.StartPositions.Keys)
                    keys.Add(new Tuple<uint, uint>((uint)key.Item1, (uint)key.Item2));

                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var startPosition in Stuffing.StartPositions)
                {
                    var comment = new QueryBuilder.SQLInsertRow();
                    comment.HeaderComment = startPosition.Key.Item1 + " - " + startPosition.Key.Item2;
                    rows.Add(comment);

                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("race", startPosition.Key.Item1);
                    row.AddValue("class", startPosition.Key.Item2);
                    row.AddValue("map", startPosition.Value.Map);
                    row.AddValue("zone", startPosition.Value.Zone);
                    row.AddValue("position_x", startPosition.Value.Position.X);
                    row.AddValue("position_x", startPosition.Value.Position.Y);
                    row.AddValue("position_x", startPosition.Value.Position.Z);

                    row.Comment = StoreGetters.GetName(StoreNameType.Map, startPosition.Value.Map, false) + " - " +
                                  StoreGetters.GetName(StoreNameType.Zone, startPosition.Value.Zone, false);

                    rows.Add(row);
                }

                result += new QueryBuilder.SQLInsert("playercreateinfo", keys, new[] { "race", "class" }, rows).Build();
            }

            if (!Stuffing.StartSpells.IsEmpty)
            {
                // Can't cast the collection directly
                ICollection<Tuple<uint, uint>> keys = new Collection<Tuple<uint, uint>>();
                foreach (var key in Stuffing.StartSpells.Keys)
                    keys.Add(new Tuple<uint, uint>((uint)key.Item1, (uint)key.Item2));

                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var startSpells in Stuffing.StartSpells)
                {
                    var comment = new QueryBuilder.SQLInsertRow();
                    comment.HeaderComment = startSpells.Key.Item1 + " - " + startSpells.Key.Item2;
                    rows.Add(comment);

                    foreach (var spell in startSpells.Value.Spells)
                    {
                        var row = new QueryBuilder.SQLInsertRow();

                        row.AddValue("race", startSpells.Key.Item1);
                        row.AddValue("class", startSpells.Key.Item2);
                        row.AddValue("Spell", spell);
                        row.AddValue("Note",  StoreGetters.GetName(StoreNameType.Spell, (int)spell, false));

                        rows.Add(row);
                    }
                }

                result = new QueryBuilder.SQLInsert("playercreateinfo_spell", keys, new[] { "race", "class" }, rows).Build();
            }

            return result;
        }
    }
}
