using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
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
                npc.LoadValuesFromUpdateFields();

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
                        if (aura.CasterGuid.Full == 0 || aura.AuraFlags.HasAnyFlag(AuraFlag.NotCaster)) // usually "template auras" do not have caster
                        {
                            auras += aura.SpellId + " ";
                            commentAuras += StoreGetters.GetName(StoreNameType.Spell, (int) aura.SpellId, false) + ", ";
                        }
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
                npc.LoadValuesFromUpdateFields();

                if (npc.Model == null)
                    continue;
                var model = (uint)npc.Model;

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

            Dictionary<uint, Tuple<float, float, Gender>> modelsDb = null;
            if (SQLConnector.Enabled)
            {
                Dictionary<uint, object> modelsDbTemp = SQLDatabase.GetDict<uint>(string.Format(
                    "SELECT `modelid`, `bounding_radius`, `combat_reach`," +
                    "`gender` FROM `world`.{0} WHERE `modelid` IN ({1});", tableName, models.Keys.ToList().Split()));

                modelsDb = new Dictionary<uint, Tuple<float, float, Gender>>();

                foreach (var ele in modelsDbTemp)
                {
                    var a = (Tuple<Object, Object, Object>)ele.Value;
                    var b = Tuple.Create((float)a.Item1, (float)a.Item2, (Gender)Enum.Parse(typeof(Gender), a.Item3.ToString()));

                    modelsDb.Add(ele.Key, b);
                }
            }

            var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();
            var rowsIns = new List<QueryBuilder.SQLInsertRow>();
            foreach (var model in models)
            {
                if (modelsDb != null && modelsDb.Count != 0)
                {
                    if (modelsDb.ContainsKey(model.Key)) // update
                    {
                        var row = new QueryBuilder.SQLUpdateRow();

                        if (Math.Abs(modelsDb[model.Key].Item1 - model.Value.Item1) > 0.01)
                            row.AddValue("bounding_radius", model.Value.Item1);

                        if (Math.Abs(modelsDb[model.Key].Item2 - model.Value.Item2) > 0.01)
                            row.AddValue("combat_reach", model.Value.Item2);

                        if (modelsDb[model.Key].Item3 != model.Value.Item3)
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
                npc.LoadValuesFromUpdateFields();

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

        public static string Gossip()
        {
            if (Storage.Gossips.IsEmpty)
                return String.Empty;

            const string tableName1 = "gossip_menu";
            const string tableName2 = "gossip_menu_option";

            // TODO: Add creature_template gossip_menu_id update query or similar

            // `gossip`
            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var gossip in Storage.Gossips)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("entry", gossip.Key.Item1);
                row.AddValue("text_id", gossip.Key.Item2);
                row.Comment = StoreGetters.GetName(Utilities.ObjectTypeToStore(gossip.Value.ObjectType),
                                                   (int)gossip.Value.ObjectEntry, false);

                rows.Add(row);
            }

            var result = new QueryBuilder.SQLInsert(tableName1, rows, 2).Build();

            // `gossip_menu_option`
            rows = new List<QueryBuilder.SQLInsertRow>();
            ICollection<Tuple<uint, uint>> keys = new Collection<Tuple<uint, uint>>();
            foreach (var gossip in Storage.Gossips)
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

                        keys.Add(Tuple.Create(gossip.Key.Item1, gossipOption.Index));
                    }
            }

            result += new QueryBuilder.SQLInsert(tableName2, rows, 2).Build();

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
                npc.LoadValuesFromUpdateFields();

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
    }
}
