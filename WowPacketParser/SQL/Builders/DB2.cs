using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class DB2
    {
        [BuilderMethod]
        public static string HotfixData()
        {
            if (Storage.HotfixDatas.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                return String.Empty;

            var entries = Storage.HotfixDatas.Keys();
            var templatesDb = SQLDatabase.GetDict<DB2Hash, int, uint, HotfixData>(entries, "TableHash", "RecordID", "Timestamp", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.HotfixDatas, templatesDb, StoreNameType.None, StoreNameType.None, StoreNameType.None, "TableHash", "RecordID", "Timestamp");
        }

        [BuilderMethod]
        public static string AreaPOI()
        {
            if (Storage.AreaPOIs.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.area_poi))
                return String.Empty;

            var entries = Storage.AreaPOIs.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, AreaPOI>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.AreaPOIs, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string AreaPOIState()
        {
            if (Storage.AreaPOIStates.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.area_poi_state))
                return String.Empty;

            var entries = Storage.AreaPOIStates.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, AreaPOIState>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.AreaPOIStates, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string BattlePetSpecies()
        {
            if (Storage.BattlePetSpecies.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.battle_pet_species))
                return String.Empty;

            var entries = Storage.BattlePetSpecies.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, BattlePetSpecies>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.BattlePetSpecies, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string BroadcastText()
        {
            if (Storage.BroadcastTexts.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.broadcast_text))
                return String.Empty;

            var entries = Storage.BroadcastTexts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, BroadcastText>(entries, "Id", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.BroadcastTexts, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string ChrUpgradeTier()
        {
            if (Storage.ChrUpgradeTiers.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.chr_upgrade))
                return String.Empty;

            var entries = Storage.ChrUpgradeTiers.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ChrUpgradeTier>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.ChrUpgradeTiers, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string ChrUpgradeBucket()
        {
            if (Storage.ChrUpgradeBuckets.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.chr_upgrade))
                return String.Empty;

            var entries = Storage.ChrUpgradeBuckets.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ChrUpgradeBucket>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.ChrUpgradeBuckets, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string ChrUpgradeBucketSpell()
        {
            if (Storage.ChrUpgradeBucketSpells.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.chr_upgrade))
                return String.Empty;

            var entries = Storage.ChrUpgradeBucketSpells.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ChrUpgradeBucketSpell>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.ChrUpgradeBucketSpells, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string Creature()
        {
            if (Storage.Creatures.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.creatureDB2))
                return String.Empty;

            var entries = Storage.Creatures.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, Creature>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.Creatures, templatesDb, StoreNameType.Unit, "ID");
        }

        [BuilderMethod]
        public static string CreatureDifficulty()
        {
            if (Storage.CreatureDifficultys.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.creature_difficulty))
                return String.Empty;

            var rowsIns = new List<QueryBuilder.SQLInsertRow>();
            var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();
            foreach (var creature in Storage.CreatureDifficultys)
            {
                if (SQLDatabase.CreatureDifficultyStores != null)
                {
                    if (SQLDatabase.CreatureDifficultyStores.ContainsKey(creature.Key))
                    {
                        foreach (var creatureDiff in SQLDatabase.CreatureDifficultyStores)
                        {
                            var row = new QueryBuilder.SQLUpdateRow();

                            if (!Utilities.EqualValues(creature.Key, creatureDiff.Key))
                                continue;

                            if (!Utilities.EqualValues(creatureDiff.Value.CreatureID, creature.Value.Item1.CreatureID))
                                row.AddValue("CreatureID", creature.Value.Item1.CreatureID);

                            if (!Utilities.EqualValues(creatureDiff.Value.FactionID, creature.Value.Item1.FactionID))
                                row.AddValue("FactionID", creature.Value.Item1.FactionID);

                            if (!Utilities.EqualValues(creatureDiff.Value.Expansion, creature.Value.Item1.Expansion))
                                row.AddValue("Expansion", creature.Value.Item1.Expansion);

                            if (!Utilities.EqualValues(creatureDiff.Value.MinLevel, creature.Value.Item1.MinLevel))
                                row.AddValue("MinLevel", creature.Value.Item1.MinLevel);

                            if (!Utilities.EqualValues(creatureDiff.Value.MaxLevel, creature.Value.Item1.MaxLevel))
                                row.AddValue("MaxLevel", creature.Value.Item1.MaxLevel);

                            for (int i = 0; i < 5; i++)
                                if (!Utilities.EqualValues(creatureDiff.Value.Flags[i], creature.Value.Item1.Flags[i]))
                                    row.AddValue("Flags" + (i + 1), creature.Value.Item1.Flags[i]);

                            if (!Utilities.EqualValues(creatureDiff.Value.VerifiedBuild, creature.Value.Item1.VerifiedBuild))
                                row.AddValue("VerifiedBuild", creature.Value.Item1.VerifiedBuild);

                            row.AddWhere("Id", creature.Key);

                            row.Table = "creature_difficulty";

                            if (row.ValueCount != 0)
                                rowsUpd.Add(row);
                        }
                    }
                    else // insert
                    {
                        var row = new QueryBuilder.SQLInsertRow();

                        row.AddValue("ID", creature.Key);
                        row.AddValue("CreatureID", creature.Value.Item1.CreatureID);

                        row.AddValue("FactionID", creature.Value.Item1.FactionID);
                        row.AddValue("Expansion", creature.Value.Item1.Expansion);

                        row.AddValue("MinLevel", creature.Value.Item1.MinLevel);
                        row.AddValue("MaxLevel", creature.Value.Item1.MaxLevel);

                        for (int i = 0; i < 5; i++)
                            row.AddValue("Flags" + (i + 1), creature.Value.Item1.Flags[i]);

                        row.AddValue("VerifiedBuild", creature.Value.Item1.VerifiedBuild);

                        rowsIns.Add(row);
                    }
                }
                else // insert
                {
                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("ID", creature.Key);
                    row.AddValue("CreatureID", creature.Value.Item1.CreatureID);

                    row.AddValue("FactionID", creature.Value.Item1.FactionID);
                    row.AddValue("Expansion", creature.Value.Item1.Expansion);

                    row.AddValue("MinLevel", creature.Value.Item1.MinLevel);
                    row.AddValue("MaxLevel", creature.Value.Item1.MaxLevel);

                    for (int i = 0; i < 5; i++)
                        row.AddValue("Flags" + (i + 1), creature.Value.Item1.Flags[i]);

                    row.AddValue("VerifiedBuild", creature.Value.Item1.VerifiedBuild);

                    rowsIns.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert("creature_difficulty", rowsIns).Build() +
                new QueryBuilder.SQLUpdate(rowsUpd).Build();
        }

        [BuilderMethod]
        public static string CurvePoint()
        {
            if (Storage.CurvePoints.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.curve_point))
                return String.Empty;

            var entries = Storage.CurvePoints.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, CurvePoint>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.CurvePoints, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string HolidayData()
        {
            if (Storage.Holidays.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.holidays))
                return String.Empty;

            var entries = Storage.Holidays.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, HolidayData>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.Holidays, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string Item()
        {
            if (Storage.Items.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item))
                return String.Empty;

            var entries = Storage.Items.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, Item>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.Items, templatesDb, StoreNameType.Item, "ID");
        }

        [BuilderMethod]
        public static string ItemAppearance()
        {
            if (Storage.ItemAppearances.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_appearance))
                return String.Empty;

            var entries = Storage.ItemAppearances.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ItemAppearance>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.ItemAppearances, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string ItemBonus()
        {
            if (Storage.ItemBonuses.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_bonus))
                return String.Empty;

            var entries = Storage.ItemBonuses.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ItemBonus>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.ItemBonuses, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string ItemBonusTreeNode()
        {
            if (Storage.ItemBonusTreeNodes.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_bonus_tree_node))
                return String.Empty;

            var entries = Storage.ItemBonusTreeNodes.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ItemBonusTreeNode>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.ItemBonusTreeNodes, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string ItemCurrencyCost()
        {
            if (Storage.ItemCurrencyCosts.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_currency_cost))
                return String.Empty;

            var entries = Storage.ItemCurrencyCosts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ItemCurrencyCost>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.ItemCurrencyCosts, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string ItemEffect()
        {
            if (Storage.ItemEffects.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_extended_cost))
                return String.Empty;

            var entries = Storage.ItemEffects.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ItemEffect>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.ItemEffects, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string ItemExtendedCost()
        {
            if (Storage.ItemExtendedCosts.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_extended_cost))
                return String.Empty;

            var entries = Storage.ItemExtendedCosts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ItemExtendedCost>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.ItemExtendedCosts, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string ItemModifiedAppearance()
        {
            if (Storage.ItemModifiedAppearances.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_extended_cost))
                return String.Empty;

            var entries = Storage.ItemModifiedAppearances.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ItemModifiedAppearance>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.ItemModifiedAppearances, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string ItemSparse()
        {
            if (Storage.ItemSparses.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_sparse))
                return String.Empty;

            var entries = Storage.ItemSparses.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ItemSparse>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.ItemSparses, templatesDb, StoreNameType.Item, "ID");
        }

        [BuilderMethod]
        public static string GameObjects()
        {
            if (Storage.GameObjects.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.gameobjectDB2))
                return String.Empty;

            var entries = Storage.GameObjects.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, GameObjects>(entries, "Id", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.GameObjects, templatesDb, StoreNameType.GameObject, "Id");
        }

        [BuilderMethod]
        public static string KeyChain()
        {
            if (Storage.KeyChains.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.key_chain))
                return String.Empty;

            var entries = Storage.KeyChains.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, KeyChain>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.KeyChains, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string Location()
        {
            if (Storage.Locations.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.location))
                return String.Empty;

            var entries = Storage.Locations.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, Location>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.Locations, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string Mount()
        {
            if (Storage.Mounts.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.mount))
                return String.Empty;

            var entries = Storage.Mounts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, Mount>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.Mounts, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string OverrideSpellData()
        {
            if (Storage.OverrideSpellDatas.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.override_spell_data))
                return String.Empty;

            var entries = Storage.OverrideSpellDatas.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, OverrideSpellData>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.OverrideSpellDatas, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string PhaseXPhaseGroup()
        {
            if (Storage.PhaseXPhaseGroups.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.phase_group))
                return String.Empty;

            var entries = Storage.PhaseXPhaseGroups.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, PhaseXPhaseGroup>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.PhaseXPhaseGroups, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string SceneScript()
        {
            if (Storage.SceneScripts.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.scene_script))
                return String.Empty;

            var entries = Storage.SceneScripts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SceneScript>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SceneScripts, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string SpellAuraRestrictions()
        {
            if (Storage.SpellAuraRestrictions.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_aura_restrictions))
                return String.Empty;

            var entries = Storage.SpellAuraRestrictions.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SpellAuraRestrictions>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SpellAuraRestrictions, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string SpellCastingRequirements()
        {
            if (Storage.SpellCastingRequirements.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_casting_requirements))
                return String.Empty;

            var entries = Storage.SpellCastingRequirements.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SpellCastingRequirements>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SpellCastingRequirements, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string SpellClassOptions()
        {
            if (Storage.SpellClassOptions.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_class_options))
                return String.Empty;

            var entries = Storage.SpellClassOptions.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SpellClassOptions>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SpellClassOptions, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string SpellEffectGroupSize()
        {
            if (Storage.SpellEffectGroupSizes.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_effect_group_size))
                return String.Empty;

            var entries = Storage.SpellEffectGroupSizes.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SpellEffectGroupSize>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SpellEffectGroupSizes, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string SpellLearnSpell()
        {
            if (Storage.SpellLearnSpells.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_learn_spell))
                return String.Empty;

            var entries = Storage.SpellLearnSpells.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SpellLearnSpell>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SpellLearnSpells, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string SpellMisc()
        {
            if (Storage.SpellMiscs.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_misc))
                return String.Empty;

            var entries = Storage.SpellMiscs.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SpellMisc>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SpellMiscs, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string SpellPower()
        {
            if (Storage.SpellPowers.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_power))
                return String.Empty;

            var entries = Storage.SpellPowers.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SpellPower>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SpellPowers, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string SpellReagents()
        {
            if (Storage.SpellReagents.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_power))
                return String.Empty;

            var entries = Storage.SpellReagents.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SpellReagents>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SpellReagents, templatesDb, StoreNameType.None, "ID");
        }

        public static string SpellRuneCost()
        {
            if (Storage.SpellRuneCosts.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_power))
                return String.Empty;

            var entries = Storage.SpellRuneCosts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SpellRuneCost>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SpellRuneCosts, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string SpellTotems()
        {
            if (Storage.SpellTotems.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_totems))
                return String.Empty;

            var entries = Storage.SpellTotems.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SpellTotems>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SpellTotems, templatesDb, StoreNameType.None, "ID");
        }

        public static string TaxiNodes()
        {
            if (Storage.TaxiNodes.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.taxi_nodes))
                return String.Empty;

            var entries = Storage.TaxiNodes.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, TaxiNodes>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.TaxiNodes, templatesDb, StoreNameType.None, "ID");
        }

        public static string TaxiPathNode()
        {
            if (Storage.TaxiPathNodes.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.taxi_path_node))
                return String.Empty;

            var entries = Storage.TaxiPathNodes.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, TaxiPathNode>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.TaxiPathNodes, templatesDb, StoreNameType.None, "ID");
        }

        public static string TaxiPath()
        {
            if (Storage.TaxiPaths.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.taxi_path))
                return String.Empty;

            var entries = Storage.TaxiPaths.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, TaxiPath>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.TaxiPaths, templatesDb, StoreNameType.None, "ID");
        }

        [BuilderMethod]
        public static string Toy()
        {
            if (Storage.Toys.IsEmpty())
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.toy))
                return String.Empty;

            var entries = Storage.Toys.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, Toy>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.Toys, templatesDb, StoreNameType.None, "ID");
        }
    }
}
