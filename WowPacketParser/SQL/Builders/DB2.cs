using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class DB2
    {
        [BuilderMethod(true)]
        public static string HotfixData()
        {
            if (Storage.HotfixDatas.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.HotfixDatas, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.HotfixDatas, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string AreaPOI()
        {
            if (Storage.AreaPOIs.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.area_poi))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.AreaPOIs, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.AreaPOIs, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string AreaPOIState()
        {
            if (Storage.AreaPOIStates.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.area_poi_state))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.AreaPOIStates, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.AreaPOIStates, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string BattlePetSpecies()
        {
            if (Storage.BattlePetSpeciesBag.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.battle_pet_species))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.BattlePetSpeciesBag, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.BattlePetSpeciesBag, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string BroadcastText()
        {
            if (Storage.BroadcastTexts.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.broadcast_text))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.BroadcastTexts, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.BroadcastTexts, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string ChrUpgradeTier()
        {
            if (Storage.ChrUpgradeTiers.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.chr_upgrade))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ChrUpgradeTiers, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.ChrUpgradeTiers, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string ChrUpgradeBucket()
        {
            if (Storage.ChrUpgradeBuckets.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.chr_upgrade))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ChrUpgradeBuckets, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.ChrUpgradeBuckets, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string ChrUpgradeBucketSpell()
        {
            if (Storage.ChrUpgradeBucketSpells.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.chr_upgrade))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ChrUpgradeBucketSpells, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.ChrUpgradeBucketSpells, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string Creature()
        {
            if (Storage.Creatures.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.creatureDB2))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.Creatures, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.Creatures, templatesDb, StoreNameType.Unit);
        }

        [BuilderMethod(true)]
        public static string CreatureDifficulty()
        {
            if (Storage.CreatureDifficulties.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.creature_difficulty))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.CreatureDifficulties, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.CreatureDifficulties, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string CurvePoint()
        {
            if (Storage.CurvePoints.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.curve_point))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.CurvePoints, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.CurvePoints, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string HolidayData()
        {
            if (Storage.HolidaysBag.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.holidays))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.HolidaysBag, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.HolidaysBag, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string Item()
        {
            if (Storage.Items.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.Items, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.Items, templatesDb, StoreNameType.Item);
        }

        [BuilderMethod(true)]
        public static string ItemAppearance()
        {
            if (Storage.ItemAppearances.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_appearance))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ItemAppearances, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.ItemAppearances, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string ItemBonus()
        {
            if (Storage.ItemBonuses.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_bonus))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ItemBonuses, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.ItemBonuses, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string ItemBonusTreeNode()
        {
            if (Storage.ItemBonusTreeNodes.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_bonus_tree_node))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ItemBonusTreeNodes, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.ItemBonusTreeNodes, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string ItemCurrencyCost()
        {
            if (Storage.ItemCurrencyCosts.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_currency_cost))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ItemCurrencyCosts, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.ItemCurrencyCosts, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string ItemEffect()
        {
            if (Storage.ItemEffects.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_effect))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ItemEffects, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.ItemEffects, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string ItemExtendedCost()
        {
            if (Storage.ItemExtendedCosts.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_extended_cost))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ItemExtendedCosts, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.ItemExtendedCosts, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string ItemModifiedAppearance()
        {
            if (Storage.ItemModifiedAppearances.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_modified_appearance))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ItemModifiedAppearances, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.ItemModifiedAppearances, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string ItemSparse()
        {
            if (Storage.ItemSparses.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.item_sparse))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ItemSparses, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.ItemSparses, templatesDb, StoreNameType.Item);
        }

        [BuilderMethod(true)]
        public static string GameObjects()
        {
            if (Storage.GameObjectsBag.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.gameobjectDB2))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.GameObjectsBag, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.GameObjectsBag, templatesDb, StoreNameType.GameObject);
        }

        [BuilderMethod(true)]
        public static string KeyChain()
        {
            if (Storage.KeyChains.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.key_chain))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.KeyChains, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.KeyChains, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string Location()
        {
            if (Storage.Locations.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.location))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.Locations, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.Locations, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string Mount()
        {
            if (Storage.Mounts.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.mount))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.Mounts, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.Mounts, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string OverrideSpellData()
        {
            if (Storage.OverrideSpellDatas.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.override_spell_data))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.OverrideSpellDatas, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.OverrideSpellDatas, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string PhaseXPhaseGroup()
        {
            if (Storage.PhaseXPhaseGroups.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.phase_group))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.PhaseXPhaseGroups, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.PhaseXPhaseGroups, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string SceneScript()
        {
            if (Storage.SceneScripts.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.scene_script))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SceneScripts, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.SceneScripts, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string SpellAuraRestrictions()
        {
            if (Storage.SpellAuraRestrictionsBag.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_aura_restrictions))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellAuraRestrictionsBag, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.SpellAuraRestrictionsBag, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string SpellCastingRequirements()
        {
            if (Storage.SpellCastingRequirementsBag.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_casting_requirements))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellCastingRequirementsBag, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.SpellCastingRequirementsBag, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string SpellClassOptions()
        {
            if (Storage.SpellClassOptionsBag.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_class_options))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellClassOptionsBag, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.SpellClassOptionsBag, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string SpellEffectGroupSize()
        {
            if (Storage.SpellEffectGroupSizes.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_effect_group_size))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellEffectGroupSizes, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.SpellEffectGroupSizes, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string SpellLearnSpell()
        {
            if (Storage.SpellLearnSpells.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_learn_spell))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellLearnSpells, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.SpellLearnSpells, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string SpellMisc()
        {
            if (Storage.SpellMiscs.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_misc))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellMiscs, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.SpellMiscs, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string SpellPower()
        {
            if (Storage.SpellPowers.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_power))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellPowers, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.SpellPowers, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string SpellReagents()
        {
            if (Storage.SpellReagentsBag.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_power))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellReagentsBag, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.SpellReagentsBag, templatesDb, StoreNameType.None);
        }

        public static string SpellRuneCost()
        {
            if (Storage.SpellRuneCosts.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_rune_cost))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellRuneCosts, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.SpellRuneCosts, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string SpellTotems()
        {
            if (Storage.SpellTotemsBag.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.spell_totems))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellTotemsBag, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.SpellTotemsBag, templatesDb, StoreNameType.None);
        }

        public static string TaxiNodes()
        {
            if (Storage.TaxiNodesBag.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.taxi_nodes))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.TaxiNodesBag, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.TaxiNodesBag, templatesDb, StoreNameType.None);
        }

        public static string TaxiPathNode()
        {
            if (Storage.TaxiPathNodes.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.taxi_path_node))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.TaxiPathNodes, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.TaxiPathNodes, templatesDb, StoreNameType.None);
        }

        public static string TaxiPath()
        {
            if (Storage.TaxiPaths.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.taxi_path))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.TaxiPaths, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.TaxiPaths, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string Toy()
        {
            if (Storage.Toys.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.toy))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.Toys, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.Toys, templatesDb, StoreNameType.None);
        }
    }
}
