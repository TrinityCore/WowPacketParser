using System;
using System.Collections.Generic;
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

            // Build a dictionary with model data; model is the key
            var models = new StoreDictionary<uint, ModelData>();
            foreach (var npc in units.Select(unit => unit.Value))
            {
                uint modelId;
                if (npc.Model.HasValue)
                    modelId = npc.Model.Value;
                else
                    continue;

                // Do not add duplicate models
                if (models.ContainsKey(modelId))
                    continue;

                var model = new ModelData
                {
                    BoundingRadius = npc.BoundingRadius.GetValueOrDefault(0.306f),
                    CombatReach = npc.CombatReach.GetValueOrDefault(1.5f),
                    Gender = npc.Gender.GetValueOrDefault(Gender.Male)
                };

                models.Add(modelId, model, null);
            }

            var entries = models.Keys();
            var modelsDb = SQLDatabase.GetDict<uint, ModelData>(entries);
            return SQLUtil.CompareDicts(models, modelsDb, StoreNameType.None, "modelid");
        }

        public static string NpcTrainer()
        {
            if (Storage.NpcTrainers.IsEmpty())
                return String.Empty;

            const string tableName = "npc_trainer";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcTrainer in Storage.NpcTrainers)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment = StoreGetters.GetName(StoreNameType.Unit, (int)npcTrainer.Key, false);
                rows.Add(comment);
                foreach (var trainerSpell in npcTrainer.Value.Item1.TrainerSpells)
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
            if (Storage.NpcVendors.IsEmpty())
                return String.Empty;

            const string tableName = "npc_vendor";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcVendor in Storage.NpcVendors)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment = StoreGetters.GetName(StoreNameType.Unit, (int)npcVendor.Key);
                rows.Add(comment);
                foreach (var vendorItem in npcVendor.Value.Item1.VendorItems)
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue("entry", npcVendor.Key);
                    row.AddValue("item", vendorItem.ItemId);
                    row.AddValue("slot", vendorItem.Slot);
                    row.AddValue("maxcount", vendorItem.MaxCount);
                    row.AddValue("ExtendedCost", vendorItem.ExtendedCostId);

                    if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                        row.AddValue("Type", vendorItem.Type);

                    row.Comment = StoreGetters.GetName(vendorItem.Type <= 1 ? StoreNameType.Item : StoreNameType.Currency, (int)vendorItem.ItemId, false);
                    rows.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
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
            if (Storage.Loots.IsEmpty())
                return String.Empty;

            // Not TDB structure
            const string tableName = "LootTemplate";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var loot in Storage.Loots)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment =
                    StoreGetters.GetName(Utilities.ObjectTypeToStore(Storage.Loots.Keys().First().Item2), (int)loot.Key.Item1, false) +
                    " (" + loot.Value.Item1.Gold + " gold)";
                rows.Add(comment);
                foreach (var lootItem in loot.Value.Item1.LootItems)
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
            // TODO: This should be rewritten

            if (Storage.Gossips.IsEmpty())
                return String.Empty;

            var result = "";

            // `gossip`
            if (SQLConnector.Enabled)
            {
                var query = new StringBuilder(string.Format("SELECT `entry`,`text_id` FROM {0}.`gossip_menu` WHERE ", Settings.TDBDatabase));
                foreach (Tuple<uint, uint> gossip in Storage.Gossips.Keys())
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
                    row.Comment = StoreGetters.GetName(Utilities.ObjectTypeToStore(gossip.Value.Item1.ObjectType),
                                                       (int) gossip.Value.Item1.ObjectEntry, false);

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
                    if (gossip.Value.Item1.GossipOptions == null) continue;
                    foreach (var gossipOption in gossip.Value.Item1.GossipOptions)
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
                                        StoreGetters.GetName(Utilities.ObjectTypeToStore(gossip.Value.Item1.ObjectType),
                                                             (int)gossip.Value.Item1.ObjectEntry, false);

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

                                row.Comment = StoreGetters.GetName(Utilities.ObjectTypeToStore(gossip.Value.Item1.ObjectType),
                                                           (int)gossip.Value.Item1.ObjectEntry, false);

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
                    if (gossip.Value.Item1.GossipOptions != null)
                        foreach (var gossipOption in gossip.Value.Item1.GossipOptions)
                        {
                            var row = new QueryBuilder.SQLInsertRow();

                            row.AddValue("menu_id", gossip.Key.Item1);
                            row.AddValue("id", gossipOption.Index);
                            row.AddValue("option_icon", gossipOption.OptionIcon);
                            row.AddValue("option_text", gossipOption.OptionText);
                            row.AddValue("box_coded", gossipOption.Box);
                            row.AddValue("box_money", gossipOption.RequiredMoney);
                            row.AddValue("box_text", gossipOption.BoxText);

                            row.Comment = StoreGetters.GetName(Utilities.ObjectTypeToStore(gossip.Value.Item1.ObjectType),
                                                       (int)gossip.Value.Item1.ObjectEntry, false);

                            rows.Add(row);
                        }
                }

                result += new QueryBuilder.SQLInsert("gossip_menu_option", rows, 2).Build();
            }

            return result;
        }

        //                      entry, <minlevel, maxlevel>
        public static Dictionary<uint, Tuple<uint, uint>> GetLevels(Dictionary<Guid, Unit> units)
        {
            if (units.Count == 0)
                return null;

            var entries = units.GroupBy(unit => unit.Key.GetEntry());
            var list = new Dictionary<uint, List<uint>>();

            foreach (var pair in entries.SelectMany(entry => entry))
            {
                if (list.ContainsKey(pair.Key.GetEntry()))
                    list[pair.Key.GetEntry()].Add(pair.Value.Level.GetValueOrDefault(1));
                else
                    list.Add(pair.Key.GetEntry(), new List<uint> { pair.Value.Level.GetValueOrDefault(1) });
            }

            var result = list.ToDictionary(pair => pair.Key, pair => Tuple.Create(pair.Value.Min(), pair.Value.Max()));

            return result.Count == 0 ? null : result;
        }

        // Non-WDB data but nevertheless data that should be saved to creature_template
        public static string NpcTemplateNonWDB(Dictionary<Guid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            const string tableName = "creature_template";

            var levels = GetLevels(units);

            var templates = new StoreDictionary<uint, UnitTemplateNonWDB>();
            foreach (var unit in units)
            {
                if (templates.ContainsKey(unit.Key.GetEntry()))
                    continue;

                var npc = unit.Value;
                var template = new UnitTemplateNonWDB
                {
                    GossipMenuId = npc.GossipId,
                    MinLevel = levels[unit.Key.GetEntry()].Item1,
                    MaxLevel = levels[unit.Key.GetEntry()].Item2,
                    Faction = npc.Faction.GetValueOrDefault(35),
                    Faction2 = npc.Faction.GetValueOrDefault(35),
                    NpcFlag = (uint) npc.NpcFlags.GetValueOrDefault(NPCFlags.None),
                    SpeedWalk = npc.Movement.RunSpeed,
                    SpeedRun = npc.Movement.WalkSpeed,
                    BaseAttackTime = npc.MeleeTime.GetValueOrDefault(2000),
                    RangedAttackTime = npc.RangedTime.GetValueOrDefault(2000),
                    UnitClass = (uint) npc.Class.GetValueOrDefault(Class.Warrior),
                    UnitFlag = (uint) npc.UnitFlags.GetValueOrDefault(UnitFlags.None),
                    UnitFlag2 = (uint) npc.UnitFlags2.GetValueOrDefault(UnitFlags2.None),
                    DynamicFlag = (uint) npc.DynamicFlags.GetValueOrDefault(UnitDynamicFlags.None),
                    VehicleId = npc.Movement.VehicleId,
                    HoverHeight = npc.HoverHeight.GetValueOrDefault(1.0f)
                };

                if (template.Faction == 1 || template.Faction == 2 || template.Faction == 3 ||
                        template.Faction == 4 || template.Faction == 5 || template.Faction == 6 ||
                        template.Faction == 115 || template.Faction == 116 || template.Faction == 1610 ||
                        template.Faction == 1629 || template.Faction == 2203 || template.Faction == 2204) // player factions
                    template.Faction = 35;

                template.UnitFlag &= ~(uint)UnitFlags.IsInCombat;
                template.UnitFlag &= ~(uint)UnitFlags.PetIsAttackingTarget;
                template.UnitFlag &= ~(uint)UnitFlags.PlayerControlled;
                template.UnitFlag &= ~(uint)UnitFlags.Silenced;
                template.UnitFlag &= ~(uint)UnitFlags.PossessedByPlayer;

                templates.Add(unit.Key.GetEntry(), template, null);
            }

            var templatesDb = SQLDatabase.GetDict<uint, UnitTemplateNonWDB>(templates.Keys());
            return SQLUtil.CompareDicts(templates, templatesDb, StoreNameType.Unit);
        }

        public static string SpellsX()
        {
            if (Storage.SpellsX.IsEmpty())
                return String.Empty;

            var entries = Storage.SpellsX.Keys();
            var spellsXDb = SQLDatabase.GetDict<uint, SpellsX>(entries);

            return SQLUtil.CompareDicts(Storage.SpellsX, spellsXDb, StoreNameType.Unit);
        }

        public static string CreatureText()
        {
            if (Storage.CreatureTexts.IsEmpty())
                return string.Empty;

            // For each sound and emote, if the time they were send is in the +1/-1 seconds range of
            // our texts, add that sound and emote to our Storage.CreatureTexts

            foreach (var text in Storage.CreatureTexts)
            {
                // For each text
                foreach (var textValue in text.Value)
                {
                    // For each emote
                    foreach (var emote in Storage.Emotes)
                    {
                        // Emote packets always have a sender (guid);
                        // skip this one if it was sent by a different creature
                        if (emote.Key.GetEntry() != text.Key)
                            continue;

                        foreach (var emoteValue in emote.Value)
                        {
                            var timeSpan = textValue.Item2 - emoteValue.Item2;
                            if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                                textValue.Item1.Emote = emoteValue.Item1;
                        }
                    }

                    // For each sound
                    foreach (var sound in Storage.Sounds)
                    {
                        var timeSpan = textValue.Item2 - sound.Item2;
                        if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                            textValue.Item1.Sound = sound.Item1;
                    }
                }
            }

            /* can't use compare DB without knowing values of groupid or id
            var entries = Storage.CreatureTexts.Keys.ToList();
            var creatureTextDb = SQLDatabase.GetDict<uint, CreatureText>(entries);
            */

            const string tableName = "creature_text";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var text in Storage.CreatureTexts)
            {
                foreach (var textValue in text.Value)
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
