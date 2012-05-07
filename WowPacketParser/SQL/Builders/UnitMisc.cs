using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.SQL.Builders
{
    public static class UnitMisc
    {
        public static string Addon(Dictionary<Guid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            const string tableName = "creature_template_addon";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var unit in units)
            {
                var npc = unit.Value;

                var row = new QueryBuilder.SQLInsertRow();
                row.AddValue("entry", unit.Key.GetEntry());
                row.AddValue("mount", npc.Mount);
                row.AddValue("bytes1", npc.Bytes1, true);
                row.AddValue("bytes2", npc.Bytes2, true);

                var auras = string.Empty;
                var commentAuras = string.Empty;
                if (npc.Auras != null && npc.Auras.Count() != 0)
                {
                    foreach (var aura in npc.Auras)
                    {
                        if (aura == null) continue;
                        if (!aura.AuraFlags.HasAnyFlag(AuraFlag.NotCaster)) continue; // usually "template auras" do not have caster
                        auras += aura.SpellId + " ";
                        commentAuras += StoreGetters.GetName(StoreNameType.Spell, (int) aura.SpellId, false) + ", ";
                    }
                    auras = auras.TrimEnd(' ');
                    commentAuras = commentAuras.TrimEnd(',', ' ');
                }
                row.AddValue("auras", auras);

                row.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                if (!String.IsNullOrWhiteSpace(auras))
                    row.Comment += " - " + commentAuras;

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
        }

        public static string ModelData(Dictionary<Guid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            const string tableName = "creature_model_info";

            // Build a dictionary with model data; model is the key
            var models = new SortedDictionary<uint, Tuple<float, float, Gender>>();
            foreach (var unit in units)
            {
                var npc = unit.Value;

                if (npc.Model == null)
                    continue;
                var model = (uint)npc.Model;

                // Do not add duplicate models
                if (models.ContainsKey(model))
                    continue;

                var boundingRadius = 0.0f;
                if (npc.BoundingRadius != null)
                    boundingRadius = (float)npc.BoundingRadius;

                var combatReach = 0.0f;
                if (npc.CombatReach != null)
                    combatReach = (float)npc.CombatReach;

                var gender = Gender.None;
                if (npc.Gender != null)
                    gender = (Gender)npc.Gender;

                models.Add(model, Tuple.Create(boundingRadius, combatReach, gender));
            }

            Dictionary<uint, dynamic> modelsDb = null;
            if (SQLConnector.Enabled)
            {
                modelsDb = SQLDatabase.GetDict<uint>(string.Format(
                    "SELECT `modelid`, `bounding_radius`, `combat_reach`," +
                    "`gender` FROM `{0}`.{1} WHERE `modelid` IN ({2});", Settings.TDBDatabase, tableName, String.Join(",", models.Keys)));
            }

            var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();
            var rowsIns = new List<QueryBuilder.SQLInsertRow>();
            foreach (var model in models)
            {
                if (modelsDb != null && modelsDb.Count != 0)
                {
                    if (modelsDb.ContainsKey(model.Key)) // possible update
                    {
                        var row = new QueryBuilder.SQLUpdateRow();

                        if (!Utilities.EqualValues(modelsDb[model.Key].Item1, model.Value.Item1))
                            row.AddValue("bounding_radius", model.Value.Item1);

                        if (!Utilities.EqualValues(modelsDb[model.Key].Item2, model.Value.Item2))
                            row.AddValue("combat_reach", model.Value.Item2);

                        if (!Utilities.EqualValues(modelsDb[model.Key].Item3, model.Value.Item3))
                            row.AddValue("gender", model.Value.Item3);

                        row.AddWhere("entry", model.Key);
                        row.Table = tableName;

                        if (row.ValueCount != 0)
                            rowsUpd.Add(row);
                    }
                    else // insert new
                    {
                        var row = new QueryBuilder.SQLInsertRow();
                        row.AddValue("entry", model.Key);
                        row.AddValue("bounding_radius", model.Value.Item1);
                        row.AddValue("combat_reach", model.Value.Item2);
                        row.AddValue("gender", model.Value.Item3);
                        rowsIns.Add(row);
                    }
                }
                else // no db values, simply do inserts
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue("entry", model.Key);
                    row.AddValue("bounding_radius", model.Value.Item1);
                    row.AddValue("combat_reach", model.Value.Item2);
                    row.AddValue("gender", model.Value.Item3);
                    rowsIns.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, rowsIns).Build() +
                   new QueryBuilder.SQLUpdate(rowsUpd).Build();
        }

        public static string NpcTrainer()
        {
            if (Storage.NpcTrainers.IsEmpty)
                return String.Empty;

            const string tableName = "npc_trainer";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcTrainer in Storage.NpcTrainers)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment = StoreGetters.GetName(StoreNameType.Unit, (int)npcTrainer.Key, false);
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
                    row.Comment = StoreGetters.GetName(StoreNameType.Spell, (int)trainerSpell.Spell, false);
                    rows.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
        }

        public static string NpcVendor()
        {
            if (Storage.NpcVendors.IsEmpty)
                return String.Empty;

            const string tableName = "npc_vendor";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcVendor in Storage.NpcVendors)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment = StoreGetters.GetName(StoreNameType.Unit, (int)npcVendor.Key);
                rows.Add(comment);
                foreach (var vendorItem in npcVendor.Value.VendorItems)
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue("entry", npcVendor.Key);
                    row.AddValue("item", vendorItem.ItemId);
                    row.AddValue("slot", vendorItem.Slot);
                    row.AddValue("maxcount", vendorItem.MaxCount);
                    row.AddValue("ExtendedCost", vendorItem.ExtendedCostId);
                    row.Comment = StoreGetters.GetName(StoreNameType.Item, (int)vendorItem.ItemId, false);
                    rows.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, rows, 2).Build();
        }

        public static string CreatureEquip(Dictionary<Guid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            const string tableName = "creature_equip_template";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var unit in units)
            {
                var row = new QueryBuilder.SQLInsertRow();
                var creature = unit.Value;
                var equipData = creature.Equipment;

                // check if fields are empty
                if (equipData == null || equipData.All(value => value == 0))
                    continue;

                row.AddValue("entry", unit.Key.GetEntry());
                row.AddValue("itemEntry1", equipData[0]);
                row.AddValue("itemEntry2", equipData[1]);
                row.AddValue("itemEntry3", equipData[2]);
                row.Comment = StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
        }

        public static string CreatureMovement(Dictionary<Guid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            const string tableName = "creature_movement";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var unit in units)
            {
                var row = new QueryBuilder.SQLInsertRow();

                var npc = unit.Value;

                row.AddValue("Id", unit.Key.GetEntry());
                row.AddValue("MovementFlags", npc.Movement.Flags, true);
                row.AddValue("MovementFlagsExtra", npc.Movement.FlagsExtra, true);
                row.AddValue("ufBytes1", npc.Bytes1, true);
                row.AddValue("ufBytes2", npc.Bytes2, true);
                row.AddValue("ufFlags", npc.UnitFlags, true);
                row.AddValue("ufFlags2", npc.UnitFlags2, true);

                row.Comment = StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                /*
                row.Comment += " - MoveFlags: " + npc.Movement.Flags + " - MoveFlags2: " + npc.Movement.FlagsExtra;
                row.Comment += " - Bytes1: " + npc.Bytes1 + " - Bytes2: " + npc.Bytes2 + " - UnitFlags: " + npc.UnitFlags;
                row.Comment += " - UnitFlags2: " + npc.UnitFlags2;
                 */
                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows, ignore: true, withDelete: false).Build();
        }

        public static string Loot()
        {
            if (Storage.Loots.IsEmpty)
                return String.Empty;

            // Not TDB structure
            const string tableName = "LootTemplate";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var loot in Storage.Loots)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment =
                    StoreGetters.GetName(Utilities.ObjectTypeToStore(Storage.Loots.Keys.First().Item2), (int)loot.Key.Item1, false) +
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

            return new QueryBuilder.SQLInsert(tableName, rows, 2).Build();
        }

        // TODO: Re-write Gossip()
        [Obsolete("This is *stupid*, I'll do it in a better way later")]
        [DBTableName("creature_template")]
        private struct UnitGossip
        {
            [DBFieldName("gossip_menu_id")]
            public uint GossipId;
        }

        public static string Gossip()
        {
            if (Storage.Gossips.IsEmpty)
                return String.Empty;

            // `creature_template`
            var gossipIds = new Dictionary<uint, UnitGossip>();
            foreach (var gossip in Storage.Gossips)
            {
                if (gossip.Value.ObjectType != ObjectType.Unit) continue;
                // no support for entries with multiple gossips (i.e changed by script)
                if (gossipIds.ContainsKey(gossip.Value.ObjectEntry)) continue;

                UnitGossip a;
                a.GossipId = gossip.Key.Item1;
                gossipIds.Add(gossip.Value.ObjectEntry, a);
            }

            var entries = gossipIds.Keys.ToList();
            var gossipIdsDb = SQLDatabase.GetDict<uint, UnitGossip>(entries);
            var result = SQLUtil.CompareDicts(gossipIds, gossipIdsDb, StoreNameType.Unit);

            // `gossip`
            if (SQLConnector.Enabled)
            {
                var query = new StringBuilder(string.Format("SELECT `entry`,`text_id` FROM {0}.`gossip_menu` WHERE ", Settings.TDBDatabase));
                foreach (Tuple<uint, uint> gossip in Storage.Gossips.Keys)
                {
                    query.Append("(`entry`=").Append(gossip.Item1).Append(" AND ");
                    query.Append("`text_id`=").Append(gossip.Item2).Append(") OR ");
                }
                query.Remove(query.Length - 4, 4).Append(";");

                var rows = new List<QueryBuilder.SQLInsertRow>();
                using (var reader = SQLConnector.ExecuteQuery(query.ToString()))
                {
                    if (reader != null)
                        while (reader.Read())
                        {
                            var values = new object[2];
                            var count = reader.GetValues(values);
                            if (count != 2)
                                break; // error in query

                            var entry = Convert.ToUInt32(values[0]);
                            var textId = Convert.ToUInt32(values[1]);

                            // our table is small, 2 fields and both are PKs; no need for updates
                            if (!Storage.Gossips.ContainsKey(Tuple.Create(entry, textId)))
                            {
                                var row = new QueryBuilder.SQLInsertRow();
                                row.AddValue("entry", entry);
                                row.AddValue("text_id", textId);
                                row.Comment = StoreGetters.GetName(StoreNameType.Unit, // BUG: GOs can send gossips too
                                                                   (int) entry, false);
                                rows.Add(row);
                            }
                        }
                }
                result += new QueryBuilder.SQLInsert("gossip_menu", rows, 2).Build();
            }
            else
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var gossip in Storage.Gossips)
                {
                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("entry", gossip.Key.Item1);
                    row.AddValue("text_id", gossip.Key.Item2);
                    row.Comment = StoreGetters.GetName(Utilities.ObjectTypeToStore(gossip.Value.ObjectType),
                                                       (int) gossip.Value.ObjectEntry, false);

                    rows.Add(row);
                }

                result += new QueryBuilder.SQLInsert("gossip_menu", rows, 2).Build();
            }

            // `gossip_menu_option`
            if (SQLConnector.Enabled)
            {
                var rowsIns = new List<QueryBuilder.SQLInsertRow>();
                var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();

                foreach (var gossip in Storage.Gossips)
                {
                    if (gossip.Value.GossipOptions == null) continue;
                    foreach (var gossipOption in gossip.Value.GossipOptions)
                    {
                        var query =       //         0     1       2         3         4        5         6
                            string.Format("SELECT menu_id,id,option_icon,box_coded,box_money,box_text,option_text " +
                                          "FROM {2}.gossip_menu_option WHERE menu_id={0} AND id={1};", gossip.Key.Item1,
                                          gossipOption.Index, Settings.TDBDatabase);
                        using (var reader = SQLConnector.ExecuteQuery(query))
                        {
                            if (reader.HasRows) // possible update
                            {
                                while (reader.Read())
                                {
                                    var row = new QueryBuilder.SQLUpdateRow();

                                    if (!Utilities.EqualValues(reader.GetValue(2), gossipOption.OptionIcon))
                                        row.AddValue("option_icon", gossipOption.OptionIcon);

                                    if (!Utilities.EqualValues(reader.GetValue(3), gossipOption.Box))
                                        row.AddValue("box_coded", gossipOption.Box);

                                    if (!Utilities.EqualValues(reader.GetValue(4), gossipOption.RequiredMoney))
                                        row.AddValue("box_money", gossipOption.RequiredMoney);

                                    if (!Utilities.EqualValues(reader.GetValue(5), gossipOption.BoxText))
                                        row.AddValue("box_text", gossipOption.BoxText);

                                    if (!Utilities.EqualValues(reader.GetValue(6), gossipOption.OptionText))
                                        row.AddValue("option_text", gossipOption.OptionText);

                                    row.AddWhere("menu_id", gossip.Key.Item1);
                                    row.AddWhere("id", gossipOption.Index);

                                    row.Comment =
                                        StoreGetters.GetName(Utilities.ObjectTypeToStore(gossip.Value.ObjectType),
                                                             (int) gossip.Value.ObjectEntry, false);

                                    row.Table = "gossip_menu_option";

                                    if (row.ValueCount != 0)
                                        rowsUpd.Add(row);
                                }
                            }
                            else // insert
                            {
                                var row = new QueryBuilder.SQLInsertRow();

                                row.AddValue("menu_id", gossip.Key.Item1);
                                row.AddValue("id", gossipOption.Index);
                                row.AddValue("option_icon", gossipOption.OptionIcon);
                                row.AddValue("option_text", gossipOption.OptionText);
                                row.AddValue("box_coded", gossipOption.Box);
                                row.AddValue("box_money", gossipOption.RequiredMoney);
                                row.AddValue("box_text", gossipOption.BoxText);

                                row.Comment = StoreGetters.GetName(Utilities.ObjectTypeToStore(gossip.Value.ObjectType),
                                                           (int)gossip.Value.ObjectEntry, false);

                                rowsIns.Add(row);
                            }
                        }
                    }
                }
                result += new QueryBuilder.SQLInsert("gossip_menu_option", rowsIns, 2).Build() +
                          new QueryBuilder.SQLUpdate(rowsUpd).Build();
            }
            else
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var gossip in Storage.Gossips)
                {
                    if (gossip.Value.GossipOptions != null)
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

                            row.Comment = StoreGetters.GetName(Utilities.ObjectTypeToStore(gossip.Value.ObjectType),
                                                       (int) gossip.Value.ObjectEntry, false);

                            rows.Add(row);
                        }
                }

                result += new QueryBuilder.SQLInsert("gossip_menu_option", rows, 2).Build();
            }

            return result;
        }

        // Non-WDB data but nevertheless data that should be saved to creature_template
        public static string NpcTemplateNonWDB(Dictionary<Guid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            const string tableName = "creature_template";

            var rows = new List<QueryBuilder.SQLUpdateRow>();
            ICollection<uint> key = new Collection<uint>();

            foreach (var unit in units)
            {
                // don't save duplicates
                if (key.Contains(unit.Key.GetEntry()))
                    continue;

                var row = new QueryBuilder.SQLUpdateRow();
                var npc = unit.Value;

                var name = StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);

                // Only movement flags in 335 are being read correctly - fix them and remove this if
                if (ClientVersion.Build == ClientVersionBuild.V3_3_5a_12340)
                {
                    if (npc.Movement.Flags.HasAnyFlag(MovementFlag.CanFly) && npc.Movement.Flags.HasAnyFlag(MovementFlag.WalkMode))
                        row.AddValue("InhabitType", InhabitType.Ground | InhabitType.Air, true);
                    else if (npc.Movement.Flags.HasAnyFlag(MovementFlag.DisableGravity))
                        row.AddValue("InhabitType", InhabitType.Air, true);
                }

                row.AddValue("HoverHeight", npc.HoverHeight, 1);
                row.AddValue("WalkSpeed", npc.Movement.WalkSpeed, 1);
                row.AddValue("RunSpeed", npc.Movement.RunSpeed, 1.142857);
                row.AddValue("VehicleId", npc.Movement.VehicleId, 0u);
                row.AddValue("Size", npc.Size, 1u);
                row.AddValue("Level", npc.Level, 1u); // min/max
                row.AddValue("Faction", npc.Faction, 35u); // faction_A, faction_H
                row.AddValue("UnitFlags", npc.UnitFlags, UnitFlags.None, true);
                row.AddValue("BaseAttackTime", npc.MeleeTime, 2000u);
                row.AddValue("RangeAttackTime", npc.RangedTime, 0u); // 2000?
                row.AddValue("Model", npc.Model, 0u); // model1, model2, ...
                row.AddValue("DynamicFlags", npc.DynamicFlags, UnitDynamicFlags.None, true);
                row.AddValue("NpcFlags", npc.NpcFlags, NPCFlags.None, true);

                if (npc.Resistances != null)
                    for (var i = 1; i < npc.Resistances.Length; ++i) // No armor
                        row.AddValue("Resistances" + i, npc.Resistances[i], 0u);

                // row.AddValue("ManaMod", npc.ManaMod, 1); this is not mod, it needs to be calculated
                // row.AddValue("HealthMod", npc.HealthMod, 1);
                row.AddValue("Class", npc.Class, Class.Warrior);
                //row.AddValue("Race", npc.Race, Race.None);

                row.AddWhere("entry", unit.Key.GetEntry());
                row.Table = tableName;
                row.Comment = name;

                rows.Add(row);
                key.Add(unit.Key.GetEntry());
            }

            return new QueryBuilder.SQLUpdate(rows).Build();
        }

        public static string SpellsX()
        {
            if (Storage.SpellsX.IsEmpty)
                return String.Empty;

            var entries = Storage.SpellsX.Keys.ToList();
            var spellsXDb = SQLDatabase.GetDict<uint, SpellsX>(entries);

            return SQLUtil.CompareDicts(Storage.SpellsX, spellsXDb, StoreNameType.Unit);
        }

        public static string CreatureText()
        {
            if (Storage.CreatureTexts.Count == 0)
                return string.Empty;

            // For each sound and emote, if the time they were send is in the +1/-1 seconds range of
            // our texts, add that sound and emote to our Storage.CreatureTexts

            foreach (KeyValuePair<uint, List<Tuple<CreatureText, DateTime>>> text in Storage.CreatureTexts)
            {
                // For each text
                foreach (Tuple<CreatureText, DateTime> textValue in text.Value)
                {
                    // For each emote
                    foreach (KeyValuePair<uint, List<Tuple<EmoteType, DateTime>>> emote in Storage.Emotes)
                    {
                        // Emote packets always have a sender (guid);
                        // skip this one if it was sent by a different creature
                        if (emote.Key != text.Key)
                            continue;

                        foreach (Tuple<EmoteType, DateTime> emoteValue in emote.Value)
                        {
                            if ((textValue.Item2 - emoteValue.Item2).Duration() <= TimeSpan.FromSeconds(1))
                                textValue.Item1.Emote = emoteValue.Item1;
                        }
                    }

                    // For each sound
                    foreach (KeyValuePair<uint, List<DateTime>> sound in Storage.Sounds)
                    {
                        foreach (DateTime soundValue in sound.Value)
                        {
                            if ((textValue.Item2 - soundValue).Duration() <= TimeSpan.FromSeconds(1))
                                textValue.Item1.Sound = sound.Key;
                        }
                    }
                }
            }

            /* DB comparer not implemented yet
            var entries = Storage.CreatureTexts.Keys.ToList();
            var creatureTextDb = SQLDatabase.GetDict<uint, CreatureText>(entries);
            */

            const string tableName = "creature_text";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (KeyValuePair<uint, List<Tuple<CreatureText, DateTime>>> text in Storage.CreatureTexts)
            {
                foreach (Tuple<CreatureText, DateTime> textValue in text.Value)
                {
                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("entry", text.Key);
                    row.AddValue("groupid", "x", false, true);
                    row.AddValue("id", "x", false, true);
                    row.AddValue("text", textValue.Item1.Text);
                    row.AddValue("type", textValue.Item1.Type);
                    row.AddValue("language", textValue.Item1.Language);
                    row.AddValue("probability", 100.0);
                    row.AddValue("emote", textValue.Item1.Emote);
                    row.AddValue("duration", 0);
                    row.AddValue("sound", textValue.Item1.Sound);
                    row.AddValue("comment", textValue.Item1.Comment);

                    rows.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, rows, 1, false).Build();
        }
    }
}
