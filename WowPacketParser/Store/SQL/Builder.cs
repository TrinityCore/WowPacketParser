using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.SQL
{
    public class Builder
    {
        public Builder(Stuffing stuffing)
        {
            _stuffing = stuffing;
        }

        private readonly Stuffing _stuffing;

        public string CreatureSpawns()
        {
            if (!_stuffing.Objects.Any(wowObject => wowObject.Value.Type == ObjectType.Unit))
                return string.Empty;

            var units = _stuffing.Objects.Where(x => x.Value.Type == ObjectType.Unit);

            const string tableName = "creature";
            uint count = 0;

            units = units.OrderBy(unit => unit.Key.GetEntry());

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var unit in units)
            {
                var row = new QueryBuilder.SQLInsertRow();

                var creature = unit.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(creature.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                row.CommentOut = creature.IsTemporarySpawn();

                var spawnTimeSecs = creature.GetDefaultSpawnTime();
                var movementType = 0; // TODO: Find a way to check if our unit got random movement
                var spawnDist = (movementType == 1) ? 5 : 0;

                row.AddValue("guid", "@GUID+" + count, noQuotes: true);
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
                row.Comment += " (Area: " + StoreGetters.GetName(StoreNameType.Area, creature.Area, false) + ")";
                if (row.CommentOut)
                    row.Comment += " - !!! might be temporary spawn !!!";
                else
                    ++count;

                rows.Add(row);
            }

            var result = new StringBuilder();
            // delete query for GUIDs
            var delete = new QueryBuilder.SQLDelete(new Tuple<uint, uint>(0, count), "guid", tableName, "@GUID+");
            result.Append(delete.Build());
            result.Append(Environment.NewLine);

            var sql = new QueryBuilder.SQLInsert(tableName, rows);
            result.Append(sql.Build());
            return result.ToString();
        }

        public string CreatureEquip()
        {
            if (!_stuffing.Objects.Any(wowObject => wowObject.Value.Type == ObjectType.Unit))
                return string.Empty;

            var units = _stuffing.Objects.Where(x => x.Value.Type == ObjectType.Unit);
            const string tableName = "creature_equip_template";

            ICollection<uint> key = new Collection<uint>();
            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var unit in units)
            {
                // don't save if duplicate
                if (key.Contains(unit.Key.GetEntry()))
                    continue;

                var row = new QueryBuilder.SQLInsertRow();
                var creature = unit.Value;
                UpdateField equip;
                int[] equipData = {0,0,0};

                for (var i = 0; i < 3; i++)
                    if (creature.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_VIRTUAL_ITEM_SLOT_ID1 + i), out equip))
                        equipData[i] = equip.Int32Value;

                // check if fields are empty
                if (equipData.All(value => value == 0))
                    continue;

                row.AddValue("entry", unit.Key.GetEntry());
                row.AddValue("itemEntry1", equipData[0]);
                row.AddValue("itemEntry2", equipData[1]);
                row.AddValue("itemEntry3", equipData[2]);
                row.Comment = StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                rows.Add(row);
                key.Add(unit.Key.GetEntry());
            }

            return new QueryBuilder.SQLInsert(tableName, key, "entry", rows).Build();
        }

        public string SniffData()
        {
            if (_stuffing.SniffData.IsEmpty)
                return string.Empty;

            const string tableName = "SniffData";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var data in _stuffing.SniffData)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Build",      data.FileInfo.Build);
                row.AddValue("SniffName",  (Path.GetFileName(data.FileInfo.FileName)));
                row.AddValue("TimeStamp",  data.TimeStamp);
                row.AddValue("ObjectType", data.ObjectType.ToString());
                row.AddValue("Id",         data.Id);
                row.AddValue("Data",       data.Data);
                row.AddValue("Number",     data.Number);

                if (data.ObjectType == StoreNameType.Opcode)
                    row.Comment = Opcodes.GetOpcodeName(data.Id);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows, true).Build();
        }

        public string QuestTemplate()
        {
            if (_stuffing.QuestTemplates.IsEmpty)
                return string.Empty;

            const string tableName = "quest_template";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var quest in _stuffing.QuestTemplates)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Id", quest.Key);
                row.AddValue("Method", quest.Value.Method);
                row.AddValue("Level", quest.Value.Level);
                row.AddValue("MinLevel", quest.Value.MinLevel);
                row.AddValue("ZoneOrSort", quest.Value.ZoneOrSort);
                row.AddValue("Type", quest.Value.Type);
                row.AddValue("SuggestedPlayers", quest.Value.SuggestedPlayers);

                for (var i = 0; i < quest.Value.RequiredFactionId.Length; i++)
                    row.AddValue("RequiredFactionId" + (i + 1), quest.Value.RequiredFactionId[i]);

                for (var i = 0; i < quest.Value.RequiredFactionId.Length; i++)
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

                for (var i = 0; i < quest.Value.RewardItemId.Length; i++)
                    row.AddValue("RewardItemId" + (i + 1), quest.Value.RewardItemId[i]);

                for (var i = 0; i < quest.Value.RewardItemCount.Length; i++)
                    row.AddValue("RewardItemCount" + (i + 1), quest.Value.RewardItemCount[i]);

                for (var i = 0; i < quest.Value.RewardChoiceItemId.Length; i++)
                    row.AddValue("RewardChoiceItemId" + (i + 1), quest.Value.RewardChoiceItemId[i]);

                for (var i = 0; i < quest.Value.RewardChoiceItemCount.Length; i++)
                    row.AddValue("RewardChoiceItemCount" + (i + 1), quest.Value.RewardChoiceItemCount[i]);

                for (var i = 0; i < quest.Value.RewardFactionId.Length; i++)
                    row.AddValue("RewardFactionId" + (i + 1), quest.Value.RewardFactionId[i]);

                for (var i = 0; i < quest.Value.RewardFactionValueId.Length; i++)
                    row.AddValue("RewardFactionValueId" + (i + 1), quest.Value.RewardFactionValueId[i]);

                for (var i = 0; i < quest.Value.RewardFactionValueIdOverride.Length; i++)
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

                for (var i = 0; i < quest.Value.RequiredNpcOrGo.Length; i++)
                    row.AddValue("RequiredNpcOrGo" + (i + 1), quest.Value.RequiredNpcOrGo[i]);

                for (var i = 0; i < quest.Value.RequiredNpcOrGoCount.Length; i++)
                    row.AddValue("RequiredNpcOrGoCount" + (i + 1), quest.Value.RequiredNpcOrGoCount[i]);

                for (var i = 0; i < quest.Value.RequiredSourceItemId.Length; i++)
                    row.AddValue("RequiredSourceItemId" + (i + 1), quest.Value.RequiredSourceItemId[i]);

                for (var i = 0; i < quest.Value.RequiredSourceItemCount.Length; i++)
                    row.AddValue("RequiredSourceItemCount" + (i + 1), quest.Value.RequiredSourceItemCount[i]);

                for (var i = 0; i < quest.Value.RequiredItemId.Length; i++)
                    row.AddValue("RequiredItemId" + (i + 1), quest.Value.RequiredItemId[i]);

                for (var i = 0; i < quest.Value.RequiredItemCount.Length; i++)
                    row.AddValue("RequiredItemCount" + (i + 1), quest.Value.RequiredItemCount[i]);

                row.AddValue("RequiredSpell", quest.Value.RequiredSpell);

                for (var i = 0; i < quest.Value.ObjectiveText.Length; i++)
                    row.AddValue("ObjectiveText" + (i + 1), quest.Value.ObjectiveText[i]);

                for (var i = 0; i < quest.Value.RewardCurrencyId.Length; i++)
                    row.AddValue("RewardCurrencyId" + (i + 1), quest.Value.RewardCurrencyId[i]);

                for (var i = 0; i < quest.Value.RewardCurrencyCount.Length; i++)
                    row.AddValue("RewardCurrencyCount" + (i + 1), quest.Value.RewardCurrencyCount[i]);

                for (var i = 0; i < quest.Value.RequiredCurrencyId.Length; i++)
                    row.AddValue("RequiredCurrencyId" + (i + 1), quest.Value.RequiredCurrencyId[i]);

                for (var i = 0; i < quest.Value.RequiredCurrencyCount.Length; i++)
                    row.AddValue("RequiredCurrencyCount" + (i + 1), quest.Value.RequiredCurrencyCount[i]);

                row.AddValue("QuestGiverTextWindow", quest.Value.QuestGiverTextWindow);
                row.AddValue("QuestGiverTargetName", quest.Value.QuestGiverTargetName);
                row.AddValue("QuestTurnTextWindow", quest.Value.QuestTurnTextWindow);
                row.AddValue("QuestTurnTargetName", quest.Value.QuestTurnTargetName);
                row.AddValue("SoundAccept", quest.Value.SoundAccept);
                row.AddValue("SoundTurnIn", quest.Value.SoundTurnIn);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, _stuffing.QuestTemplates.Keys, "Id", rows).Build();
        }

        public string NpcTrainer()
        {
            if (_stuffing.NpcTrainers.IsEmpty)
                return string.Empty;

            const string tableName = "npc_trainer";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcTrainer in _stuffing.NpcTrainers)
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

            return new QueryBuilder.SQLInsert(tableName, _stuffing.NpcTrainers.Keys, "entry", rows).Build();
        }

        public string NpcVendor()
        {
            if (_stuffing.NpcVendors.IsEmpty)
                return string.Empty;

            const string tableName = "npc_vendor";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcVendor in _stuffing.NpcVendors)
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

            return new QueryBuilder.SQLInsert(tableName, _stuffing.NpcVendors.Keys, "entry", rows).Build();
        }

        public string NpcTemplate()
        {
            if (_stuffing.UnitTemplates.IsEmpty)
                return string.Empty;

            // Not TDB structure
            const string tableName = "creature_template";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var unitTemplate in _stuffing.UnitTemplates)
            {
                var row = new QueryBuilder.SQLInsertRow();
                var template = unitTemplate.Value;

                row.AddValue("Id", unitTemplate.Key);
                row.AddValue("Name", template.Name);
                row.AddValue("SubName", template.SubName);
                row.AddValue("IconName", template.IconName);
                row.AddValue("TypeFlags", template.TypeFlags);
                row.AddValue("TypeFlags2", template.TypeFlags2);
                row.AddValue("Type", template.Type);
                row.AddValue("Family", template.Family);
                row.AddValue("Rank", template.Rank);
                row.AddValue("KillCredit1", template.KillCredit1);
                row.AddValue("KillCredit2", template.KillCredit2);
                row.AddValue("UnkInt", template.UnkInt);
                row.AddValue("PetSpellData", template.PetSpellData);

                for (var i = 0; i < template.DisplayIds.Length; i++)
                    row.AddValue("DisplayId" + (i + 1), template.DisplayIds[i]);

                row.AddValue("Modifier1", template.Modifier1);
                row.AddValue("Modifier2", template.Modifier2);
                row.AddValue("RacialLeader", template.RacialLeader);

                for (var i = 0; i < template.QuestItems.Length; i++)
                    row.AddValue("QuestItem" + (i + 1), template.QuestItems[i]);

                row.AddValue("MovementId", template.MovementId);
                row.AddValue("Expansion", template.Expansion);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, _stuffing.UnitTemplates.Keys, "Id", rows).Build();
        }

        public string GameObjectTemplate()
        {
            if (_stuffing.GameObjectTemplates.IsEmpty)
                return string.Empty;

            // Not TDB structure
            const string tableName = "gameobject_template";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var goTemplate in _stuffing.GameObjectTemplates)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Id", goTemplate.Key);
                row.AddValue("Type", goTemplate.Value.Type);
                row.AddValue("DisplayId", goTemplate.Value.DisplayId);
                row.AddValue("Name", goTemplate.Value.Name);
                row.AddValue("IconName", goTemplate.Value.IconName);
                row.AddValue("CastCaption", goTemplate.Value.CastCaption);
                row.AddValue("UnkString", goTemplate.Value.UnkString);

                for (var i = 0; i < goTemplate.Value.Data.Length; i++)
                    row.AddValue("Data" + (i + 1), goTemplate.Value.Data[i]);

                row.AddValue("Size", goTemplate.Value.Size);

                for (var i = 0; i < goTemplate.Value.QuestItems.Length; i++)
                    row.AddValue("QuestItem" + (i + 1), goTemplate.Value.QuestItems[i]);

                row.AddValue("UnknownUInt", goTemplate.Value.UnknownUInt);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, _stuffing.GameObjectTemplates.Keys, "Id", rows).Build();
        }

        public string GameObjectSpawns()
        {
            if (!_stuffing.Objects.Any(wowObject => wowObject.Value.Type == ObjectType.GameObject))
                return string.Empty;

            var gameobjects = _stuffing.Objects.Where(x => x.Value.Type == ObjectType.GameObject);

            const string tableName = "gameobject";
            uint count = 0;

            gameobjects = gameobjects.OrderBy(go => go.Key.GetEntry());

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var gameobject in gameobjects)
            {
                var row = new QueryBuilder.SQLInsertRow();

                var go = gameobject.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(go.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                row.CommentOut = go.IsTemporarySpawn();

                uint animprogress = 0;
                var state = 0;
                UpdateField uf;
                if (go.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(GameObjectField.GAMEOBJECT_BYTES_1), out uf))
                {
                    var bytes = uf.Int32Value;
                    state = (bytes & 0x000000FF);
                    animprogress = Convert.ToUInt32((bytes & 0xFF000000) >> 24);
                }

                var spawnTimeSecs = go.GetDefaultSpawnTime();

                row.AddValue("guid", "@GUID+" + count, noQuotes: true);
                row.AddValue("id", gameobject.Key.GetEntry());
                row.AddValue("map", go.Map);
                row.AddValue("spawnMask", 1);
                row.AddValue("phaseMask", go.PhaseMask);
                row.AddValue("position_x", go.Movement.Position.X);
                row.AddValue("position_y", go.Movement.Position.Y);
                row.AddValue("position_z", go.Movement.Position.Z);
                row.AddValue("orientation", go.Movement.Orientation);
                row.AddValue("rotation0", go.Movement.Rotation.X);
                row.AddValue("rotation1", go.Movement.Rotation.Y);
                row.AddValue("rotation2", go.Movement.Rotation.Z);
                row.AddValue("rotation3", go.Movement.Rotation.W);
                row.AddValue("spawntimesecs", spawnTimeSecs);
                row.AddValue("animprogress", animprogress);
                row.AddValue("state", state);
                row.Comment = StoreGetters.GetName(StoreNameType.GameObject, (int) gameobject.Key.GetEntry(), false);
                row.Comment += " (Area: " + StoreGetters.GetName(StoreNameType.Area, go.Area, false) + ")";
                if (row.CommentOut)
                    row.Comment += " - !!! might be temporary spawn !!!";
                else
                    ++count;

                rows.Add(row);
            }

            var result = new StringBuilder();

            // delete query for GUIDs
            var delete = new QueryBuilder.SQLDelete(new Tuple<uint, uint>(0, count), "guid", tableName, "@GUID+");
            result.Append(delete.Build());
            result.Append(Environment.NewLine);

            var sql = new QueryBuilder.SQLInsert(tableName, rows);
            result.Append(sql.Build());
            return result.ToString();
        }

        public string PageText()
        {
            if (_stuffing.PageTexts.IsEmpty)
                return string.Empty;

            const string tableName = "page_Text";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var pageText in _stuffing.PageTexts)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("entry", pageText.Key);
                row.AddValue("text", pageText.Value.Text);
                row.AddValue("next_page", pageText.Value.NextPageId);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, _stuffing.PageTexts.Keys, "entry", rows).Build();
        }

        public string NpcText()
        {
            if (_stuffing.NpcTexts.IsEmpty)
                return string.Empty;

            // Not TDB structure
            const string tableName = "npc_text";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcText in _stuffing.NpcTexts)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Id", npcText.Key);

                for (var i = 0; i < npcText.Value.Probabilities.Length; i++)
                    row.AddValue("Probability" + (i + 1), npcText.Value.Probabilities[i]);

                for (var i = 0; i < npcText.Value.Texts1.Length; i++)
                    row.AddValue("Text1_" + (i + 1), npcText.Value.Texts1[i]);

                for (var i = 0; i < npcText.Value.Texts2.Length; i++)
                    row.AddValue("Text2_" + (i + 1), npcText.Value.Texts2[i]);

                for (var i = 0; i < npcText.Value.Languages.Length; i++)
                    row.AddValue("Language" + (i + 1), npcText.Value.Languages[i]);

                for (var i = 0; i < npcText.Value.EmoteDelays[0].Length; i++)
                    for (var j = 0; j < npcText.Value.EmoteDelays[1].Length; j++)
                        row.AddValue("EmoteDelay" + (i + 1) + "_" + (j + 1), npcText.Value.EmoteDelays[i][j]);

                for (var i = 0; i < npcText.Value.EmoteIds[0].Length; i++)
                    for (var j = 0; j < npcText.Value.EmoteIds[1].Length; j++)
                        row.AddValue("EmoteId" + (i + 1) + "_" + (j + 1), npcText.Value.EmoteDelays[i][j]);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, _stuffing.NpcTexts.Keys, "Id", rows).Build();
        }

        public string Gossip()
        {
            if (_stuffing.Gossips.IsEmpty)
                return string.Empty;

            const string tableName1 = "gossip_menu";
            const string tableName2 = "gossip_menu_option";

            // TODO: Add creature_template gossip_menu_id update query or similar

            // `gossip`
            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var gossip in _stuffing.Gossips)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("entry", gossip.Key.Item1);
                row.AddValue("text_id", gossip.Key.Item2);
                row.Comment = StoreGetters.GetName(Utilities.ObjectTypeToStore(gossip.Value.ObjectType),
                                                   (int) gossip.Value.ObjectEntry, false);

                rows.Add(row);
            }

            var result = new QueryBuilder.SQLInsert(tableName1, _stuffing.Gossips.Keys, new[] { "entry", "text_id" }, rows).Build();

            // `gossip_menu_option`
            rows = new List<QueryBuilder.SQLInsertRow>();
            ICollection<Tuple<uint, uint>> keys = new Collection<Tuple<uint, uint>>();
            foreach (var gossip in _stuffing.Gossips)
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

        public string QuestPOI()
        {
            if (_stuffing.QuestPOIs.IsEmpty)
                return string.Empty;

            const string tableName1 = "quest_poi";
            const string tableName2 = "quest_poi_points";

            // Trying something..
            var orderedDict = _stuffing.QuestPOIs.OrderBy(key => key.Key.Item1);

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

            var result = new QueryBuilder.SQLInsert(tableName1, _stuffing.QuestPOIs.Keys, new[] { "questId", "id" }, rows).Build();

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

            result += new QueryBuilder.SQLInsert(tableName2, _stuffing.QuestPOIs.Keys, new[] { "questId", "id" }, rows).Build();

            return result;
        }

        public string Loot()
        {
            if (_stuffing.Loots.IsEmpty)
                return string.Empty;

            // Not TDB structure
            const string tableName = "LootTemplate";

            // Can't cast the collection directly
            ICollection<Tuple<uint, uint>> lootKeys = new Collection<Tuple<uint, uint>>();
            foreach (var tuple in _stuffing.Loots.Keys)
                lootKeys.Add(new Tuple<uint, uint>(tuple.Item1, (uint)tuple.Item2));

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var loot in _stuffing.Loots)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment =
                    StoreGetters.GetName(Utilities.ObjectTypeToStore(_stuffing.Loots.Keys.First().Item2), (int) loot.Key.Item1, false) +
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

        public string StartInformation()
        {
            var result = string.Empty;

            if (!_stuffing.StartActions.IsEmpty)
            {
                // Can't cast the collection directly
                ICollection<Tuple<uint, uint>> keys = new Collection<Tuple<uint, uint>>();
                foreach (var key in _stuffing.StartActions.Keys)
                    keys.Add(new Tuple<uint, uint>((uint) key.Item1, (uint)key.Item2));

                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var startActions in _stuffing.StartActions)
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

            if (!_stuffing.StartPositions.IsEmpty)
            {
                // Can't cast the collection directly
                ICollection<Tuple<uint, uint>> keys = new Collection<Tuple<uint, uint>>();
                foreach (var key in _stuffing.StartPositions.Keys)
                    keys.Add(new Tuple<uint, uint>((uint)key.Item1, (uint)key.Item2));

                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var startPosition in _stuffing.StartPositions)
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
                    row.AddValue("position_y", startPosition.Value.Position.Y);
                    row.AddValue("position_z", startPosition.Value.Position.Z);

                    row.Comment = StoreGetters.GetName(StoreNameType.Map, startPosition.Value.Map, false) + " - " +
                                  StoreGetters.GetName(StoreNameType.Zone, startPosition.Value.Zone, false);

                    rows.Add(row);
                }

                result += new QueryBuilder.SQLInsert("playercreateinfo", keys, new[] { "race", "class" }, rows).Build();
            }

            if (!_stuffing.StartSpells.IsEmpty)
            {
                // Can't cast the collection directly
                ICollection<Tuple<uint, uint>> keys = new Collection<Tuple<uint, uint>>();
                foreach (var key in _stuffing.StartSpells.Keys)
                    keys.Add(new Tuple<uint, uint>((uint)key.Item1, (uint)key.Item2));

                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var startSpells in _stuffing.StartSpells)
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

        public string ObjectNames()
        {
            if (_stuffing.ObjectNames.IsEmpty)
                return string.Empty;

            const string tableName = "ObjectNames";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var data in _stuffing.ObjectNames)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("ObjectType", data.Value.ObjectType.ToString());
                row.AddValue("Id", data.Key);
                row.AddValue("Name", data.Value.Name);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows, true).Build();
        }
    }
}
