using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Store
{
    public static class Storage
    {
        // Stores opcodes read, npc/GOs/spell/item/etc IDs found in sniffs
        // and other miscellaneous stuff
        public static readonly DataBag<SniffData> SniffData = new DataBag<SniffData>(new List<SQLOutput> { SQLOutput.SniffData, SQLOutput.SniffDataOpcodes });

        /* Key: Guid */

        // Units, GameObjects, Players, Items
        public static readonly StoreDictionary<WowGuid, WoWObject> Objects = new StoreDictionary<WowGuid, WoWObject>(new List<SQLOutput>());

        /* Key: Entry */

        // DB2
        public static readonly StoreDictionary<Tuple<DB2Hash, int>, HotfixData> HotfixDataStore = new StoreDictionary<Tuple<DB2Hash, int>, HotfixData>(new List<HotfixSQLOutput> { HotfixSQLOutput.hotfix_data });
        public static readonly DataBag<HotfixData> HotfixDatas = new DataBag<HotfixData>(new List<HotfixSQLOutput> { HotfixSQLOutput.hotfix_data });

        public static readonly DataBag<AreaPOI> AreaPOIs = new DataBag<AreaPOI>(new List<HotfixSQLOutput> { HotfixSQLOutput.area_poi });
        public static readonly DataBag<AreaPOIState> AreaPOIStates = new DataBag<AreaPOIState>(new List<HotfixSQLOutput> { HotfixSQLOutput.area_poi_state });
        public static readonly DataBag<BattlePetSpecies> BattlePetSpeciesBag = new DataBag<BattlePetSpecies>(new List<HotfixSQLOutput> { HotfixSQLOutput.battle_pet_species });
        public static readonly DataBag<BroadcastText> BroadcastTexts = new DataBag<BroadcastText>(new List<HotfixSQLOutput> { HotfixSQLOutput.broadcast_text });
        public static readonly DataBag<ChrUpgradeTier> ChrUpgradeTiers = new DataBag<ChrUpgradeTier>(new List<HotfixSQLOutput> { HotfixSQLOutput.chr_upgrade });
        public static readonly DataBag<ChrUpgradeBucket> ChrUpgradeBuckets = new DataBag<ChrUpgradeBucket>(new List<HotfixSQLOutput> { HotfixSQLOutput.chr_upgrade });
        public static readonly DataBag<ChrUpgradeBucketSpell> ChrUpgradeBucketSpells = new DataBag<ChrUpgradeBucketSpell>(new List<HotfixSQLOutput> { HotfixSQLOutput.chr_upgrade });
        public static readonly DataBag<CreatureDB2> Creatures = new DataBag<CreatureDB2>(new List<HotfixSQLOutput> { HotfixSQLOutput.creatureDB2 });
        public static readonly DataBag<CreatureDifficulty> CreatureDifficulties = new DataBag<CreatureDifficulty>(new List<HotfixSQLOutput> { HotfixSQLOutput.creature_difficulty });
        public static readonly DataBag<CurvePoint> CurvePoints = new DataBag<CurvePoint>(new List<HotfixSQLOutput> { HotfixSQLOutput.curve_point });
        public static readonly DataBag<GameObjects> GameObjectsBag = new DataBag<GameObjects>(new List<HotfixSQLOutput> { HotfixSQLOutput.gameobjectDB2 });
        public static readonly DataBag<Holidays> HolidaysBag = new DataBag<Holidays>(new List<HotfixSQLOutput> { HotfixSQLOutput.holidays });
        public static readonly DataBag<Item> Items = new DataBag<Item>(new List<HotfixSQLOutput> { HotfixSQLOutput.item });
        public static readonly DataBag<ItemAppearance> ItemAppearances = new DataBag<ItemAppearance>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_appearance });
        public static readonly DataBag<ItemBonus> ItemBonuses = new DataBag<ItemBonus>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_bonus });
        public static readonly DataBag<ItemBonusTreeNode> ItemBonusTreeNodes = new DataBag<ItemBonusTreeNode>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_bonus_tree_node });
        public static readonly DataBag<ItemCurrencyCost> ItemCurrencyCosts = new DataBag<ItemCurrencyCost>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_currency_cost });
        public static readonly DataBag<ItemEffect> ItemEffects = new DataBag<ItemEffect>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_effect });
        public static readonly DataBag<ItemExtendedCost> ItemExtendedCosts = new DataBag<ItemExtendedCost>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_extended_cost });
        public static readonly DataBag<ItemModifiedAppearance> ItemModifiedAppearances = new DataBag<ItemModifiedAppearance>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_modified_appearance });
        public static readonly DataBag<ItemSparse> ItemSparses = new DataBag<ItemSparse>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_sparse });
        public static readonly DataBag<KeyChain> KeyChains = new DataBag<KeyChain>(new List<HotfixSQLOutput> { HotfixSQLOutput.key_chain });
        public static readonly DataBag<Location> Locations = new DataBag<Location>(new List<HotfixSQLOutput> { HotfixSQLOutput.location });
        public static readonly DataBag<Mount> Mounts = new DataBag<Mount>(new List<HotfixSQLOutput> { HotfixSQLOutput.mount });
        public static readonly DataBag<OverrideSpellData> OverrideSpellDatas = new DataBag<OverrideSpellData>(new List<HotfixSQLOutput> { HotfixSQLOutput.override_spell_data });
        public static readonly DataBag<PhaseXPhaseGroup> PhaseXPhaseGroups = new DataBag<PhaseXPhaseGroup>(new List<HotfixSQLOutput> { HotfixSQLOutput.phase_group });
        public static readonly DataBag<SceneScript> SceneScripts = new DataBag<SceneScript>(new List<HotfixSQLOutput> { HotfixSQLOutput.scene_script });
        public static readonly DataBag<SpellAuraRestrictions> SpellAuraRestrictionsBag = new DataBag<SpellAuraRestrictions>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_aura_restrictions });
        public static readonly DataBag<SpellCastingRequirements> SpellCastingRequirementsBag = new DataBag<SpellCastingRequirements>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_casting_requirements });
        public static readonly DataBag<SpellClassOptions> SpellClassOptionsBag = new DataBag<SpellClassOptions>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_class_options });
        public static readonly DataBag<SpellEffectGroupSize> SpellEffectGroupSizes = new DataBag<SpellEffectGroupSize>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_effect_group_size });
        public static readonly DataBag<SpellLearnSpell> SpellLearnSpells = new DataBag<SpellLearnSpell>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_learn_spell });
        public static readonly DataBag<SpellMisc> SpellMiscs = new DataBag<SpellMisc>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_misc });
        public static readonly DataBag<SpellTotems> SpellTotemsBag = new DataBag<SpellTotems>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_totems });
        public static readonly DataBag<SpellPower> SpellPowers = new DataBag<SpellPower>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_power });
        public static readonly DataBag<SpellReagents> SpellReagentsBag = new DataBag<SpellReagents>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_power });
        public static readonly DataBag<SpellRuneCost> SpellRuneCosts = new DataBag<SpellRuneCost>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_rune_cost });
        public static readonly DataBag<TaxiNodes> TaxiNodesBag = new DataBag<TaxiNodes>(new List<HotfixSQLOutput> { HotfixSQLOutput.taxi_nodes });
        public static readonly DataBag<TaxiPath> TaxiPaths = new DataBag<TaxiPath>(new List<HotfixSQLOutput> { HotfixSQLOutput.taxi_path });
        public static readonly DataBag<TaxiPathNode> TaxiPathNodes = new DataBag<TaxiPathNode>(new List<HotfixSQLOutput> { HotfixSQLOutput.taxi_path_node });
        public static readonly DataBag<Toy> Toys = new DataBag<Toy>(new List<HotfixSQLOutput> { HotfixSQLOutput.toy });

        // Templates
        public static readonly DataBag<GameObjectTemplate> GameObjectTemplates = new DataBag<GameObjectTemplate>(new List<SQLOutput> { SQLOutput.gameobject_template });
        public static readonly DataBag<ItemTemplate> ItemTemplates = new DataBag<ItemTemplate>(new List<SQLOutput> { SQLOutput.item_template });
        public static readonly DataBag<QuestTemplate> QuestTemplates = new DataBag<QuestTemplate>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly DataBag<QuestObjective> QuestObjectives = new DataBag<QuestObjective>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly DataBag<QuestVisualEffect> QuestVisualEffects = new DataBag<QuestVisualEffect>(new List<SQLOutput> { SQLOutput.quest_template }); 
        public static readonly DataBag<CreatureTemplate> CreatureTemplates = new DataBag<CreatureTemplate>(new List<SQLOutput> { SQLOutput.creature_template });

        // Vendor & trainer
        public static readonly DataBag<NpcTrainer> NpcTrainers = new DataBag<NpcTrainer>(new List<SQLOutput> { SQLOutput.npc_trainer });
        public static readonly DataBag<NpcVendor> NpcVendors = new DataBag<NpcVendor>(new List<SQLOutput> { SQLOutput.npc_vendor });

        // Page & npc text
        public static readonly DataBag<PageText> PageTexts = new DataBag<PageText>(new List<SQLOutput> { SQLOutput.page_text });
        public static readonly DataBag<NpcText> NpcTexts = new DataBag<NpcText>(new List<SQLOutput> { SQLOutput.npc_text });
        public static readonly DataBag<NpcTextMop> NpcTextsMop = new DataBag<NpcTextMop>(new List<SQLOutput> { SQLOutput.npc_text });

        // Creature text (says, yells, etc.)
        public static readonly StoreMulti<uint, CreatureText> CreatureTexts = new StoreMulti<uint, CreatureText>(new List<SQLOutput> { SQLOutput.creature_text });

        // Points of Interest
        public static readonly DataBag<PointsOfInterest> GossipPOIs = new DataBag<PointsOfInterest>(new List<SQLOutput> { SQLOutput.points_of_interest });

        // "Helper" stores, do not match a specific table
        public static readonly StoreMulti<WowGuid, EmoteType> Emotes = new StoreMulti<WowGuid, EmoteType>(new List<SQLOutput> { SQLOutput.creature_text });
        public static readonly StoreBag<uint> Sounds = new StoreBag<uint>(new List<SQLOutput> { SQLOutput.creature_text });
        public static readonly StoreDictionary<uint, List<uint?>> SpellsX = new StoreDictionary<uint, List<uint?>>(new List<SQLOutput> { SQLOutput.creature_template }); // `creature_template`.`spellsX`
        public static readonly DataBag<QuestOfferReward> QuestOfferRewards = new DataBag<QuestOfferReward>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly StoreDictionary<Tuple<uint, uint>, object> GossipSelects = new StoreDictionary<Tuple<uint, uint>, object>(new List<SQLOutput> { SQLOutput.points_of_interest, SQLOutput.gossip_menu, SQLOutput.gossip_menu_option });

        /* Key: Misc */

        // Start info (Race, Class)
        public static readonly DataBag<PlayerCreateInfoAction> StartActions = new DataBag<PlayerCreateInfoAction>(new List<SQLOutput> { SQLOutput.playercreateinfo_action });
        public static readonly DataBag<PlayerCreateInfo>StartPositions = new DataBag<PlayerCreateInfo>(new List<SQLOutput> { SQLOutput.playercreateinfo });

        // Gossips (MenuId, TextId)
        public static readonly DataBag<GossipMenu> Gossips = new DataBag<GossipMenu>(new List<SQLOutput> { SQLOutput.gossip_menu });
        public static readonly DataBag<GossipMenuOption> GossipMenuOptions = new DataBag<GossipMenuOption>(new List<SQLOutput> { SQLOutput.gossip_menu_option }); 

        // Quest POI (QuestId, Id)
        public static readonly DataBag<QuestPOI> QuestPOIs = new DataBag<QuestPOI>(new List<SQLOutput> { SQLOutput.quest_poi_points });
        public static readonly DataBag<QuestPOIPoint> QuestPOIPoints = new DataBag<QuestPOIPoint>(new List<SQLOutput> { SQLOutput.quest_poi_points }); // WoD

        // Quest Misc
        public static readonly DataBag<QuestGreeting> QuestGreetings = new DataBag<QuestGreeting>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly DataBag<QuestDetails> QuestDetails = new DataBag<QuestDetails>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly DataBag<QuestRequestItems> QuestRequestItems = new DataBag<QuestRequestItems>(new List<SQLOutput> { SQLOutput.quest_template });

        // Names
        public static readonly DataBag<ObjectName> ObjectNames = new DataBag<ObjectName>(new List<SQLOutput> { SQLOutput.ObjectNames });

        // Vehicle Template Accessory
        public static readonly DataBag<VehicleTemplateAccessory> VehicleTemplateAccessorys = new DataBag<VehicleTemplateAccessory>(new List<SQLOutput> { SQLOutput.vehicle_template_accessory });

        // Weather updates
        public static readonly DataBag<WeatherUpdate> WeatherUpdates = new DataBag<WeatherUpdate>(new List<SQLOutput> { SQLOutput.weather_updates });

        // Npc Spell Click
        public static readonly StoreBag<WowGuid> NpcSpellClicks = new StoreBag<WowGuid>(new List<SQLOutput> { SQLOutput.npc_spellclick_spells });
        public static readonly DataBag<NpcSpellClick> SpellClicks = new DataBag<NpcSpellClick>(new List<SQLOutput> { SQLOutput.npc_spellclick_spells });

        // Quest Misc
        public static readonly DataBag<BroadcastTextLocale> BroadcastTextLocales = new DataBag<BroadcastTextLocale>(new List<HotfixSQLOutput> { HotfixSQLOutput.broadcast_text_locale });
        public static readonly DataBag<LocalesQuest> LocalesQuests = new DataBag<LocalesQuest>(new List<SQLOutput> { SQLOutput.locales_quest });
        public static readonly DataBag<QuestObjectivesLocale> LocalesQuestObjectives = new DataBag<QuestObjectivesLocale>(new List<SQLOutput> { SQLOutput.locales_quest_objectives });

        public static void ClearContainers()
        {
            SniffData.Clear();

            Objects.Clear();

            CreatureDifficulties.Clear();
            GameObjectsBag.Clear();
            BroadcastTexts.Clear();
            SpellMiscs.Clear();

            GameObjectTemplates.Clear();
            ItemTemplates.Clear();
            QuestTemplates.Clear();
            QuestObjectives.Clear();
            CreatureTemplates.Clear();

            NpcTrainers.Clear();
            NpcVendors.Clear();

            PageTexts.Clear();
            NpcTexts.Clear();
            NpcTextsMop.Clear();

            CreatureTexts.Clear();

            GossipPOIs.Clear();

            Emotes.Clear();
            Sounds.Clear();
            SpellsX.Clear();

            GossipSelects.Clear();

            StartActions.Clear();
            StartPositions.Clear();

            Gossips.Clear();

            QuestPOIs.Clear();

            QuestGreetings.Clear();
            QuestDetails.Clear();
            QuestRequestItems.Clear();
            QuestOfferRewards.Clear();

            ObjectNames.Clear();

            VehicleTemplateAccessorys.Clear();

            WeatherUpdates.Clear();

            NpcSpellClicks.Clear();
            SpellClicks.Clear();
        }
    }
}
