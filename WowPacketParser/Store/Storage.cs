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
        public static readonly StoreDictionary<Tuple<DB2Hash, int, uint>, HotfixData> HotfixDatas = new StoreDictionary<Tuple<DB2Hash, int, uint>, HotfixData>(new List<HotfixSQLOutput> { HotfixSQLOutput.hotfix_data });

        public static readonly StoreDictionary<uint, AreaPOI> AreaPOIs = new StoreDictionary<uint, AreaPOI>(new List<HotfixSQLOutput> { HotfixSQLOutput.area_poi });
        public static readonly StoreDictionary<uint, AreaPOIState> AreaPOIStates = new StoreDictionary<uint, AreaPOIState>(new List<HotfixSQLOutput> { HotfixSQLOutput.area_poi_state });
        public static readonly StoreDictionary<uint, BattlePetSpecies> BattlePetSpecies = new StoreDictionary<uint, BattlePetSpecies>(new List<HotfixSQLOutput> { HotfixSQLOutput.battle_pet_species });
        public static readonly StoreDictionary<uint, BroadcastText> BroadcastTexts = new StoreDictionary<uint, BroadcastText>(new List<HotfixSQLOutput> { HotfixSQLOutput.broadcast_text });
        public static readonly StoreDictionary<uint, ChrUpgradeTier> ChrUpgradeTiers = new StoreDictionary<uint, ChrUpgradeTier>(new List<HotfixSQLOutput> { HotfixSQLOutput.chr_upgrade });
        public static readonly StoreDictionary<uint, ChrUpgradeBucket> ChrUpgradeBuckets = new StoreDictionary<uint, ChrUpgradeBucket>(new List<HotfixSQLOutput> { HotfixSQLOutput.chr_upgrade });
        public static readonly StoreDictionary<uint, ChrUpgradeBucketSpell> ChrUpgradeBucketSpells = new StoreDictionary<uint, ChrUpgradeBucketSpell>(new List<HotfixSQLOutput> { HotfixSQLOutput.chr_upgrade });
        public static readonly StoreDictionary<uint, Creature> Creatures = new StoreDictionary<uint, Creature>(new List<HotfixSQLOutput> { HotfixSQLOutput.creatureDB2 });
        public static readonly StoreDictionary<uint, CreatureDifficulty> CreatureDifficultys = new StoreDictionary<uint, CreatureDifficulty>(new List<HotfixSQLOutput> { HotfixSQLOutput.creature_difficulty });
        public static readonly StoreDictionary<uint, CurvePoint> CurvePoints = new StoreDictionary<uint, CurvePoint>(new List<HotfixSQLOutput> { HotfixSQLOutput.curve_point });
        public static readonly StoreDictionary<uint, GameObjects> GameObjects = new StoreDictionary<uint, GameObjects>(new List<HotfixSQLOutput> { HotfixSQLOutput.gameobjectDB2 });
        public static readonly StoreDictionary<uint, HolidayData> Holidays = new StoreDictionary<uint, HolidayData>(new List<HotfixSQLOutput> { HotfixSQLOutput.holidays });
        public static readonly StoreDictionary<uint, Item> Items = new StoreDictionary<uint, Item>(new List<HotfixSQLOutput> { HotfixSQLOutput.item });
        public static readonly StoreDictionary<uint, ItemAppearance> ItemAppearances = new StoreDictionary<uint, ItemAppearance>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_appearance });
        public static readonly StoreDictionary<uint, ItemBonus> ItemBonuses = new StoreDictionary<uint, ItemBonus>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_bonus });
        public static readonly StoreDictionary<uint, ItemBonusTreeNode> ItemBonusTreeNodes = new StoreDictionary<uint, ItemBonusTreeNode>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_bonus_tree_node });
        public static readonly StoreDictionary<uint, ItemCurrencyCost> ItemCurrencyCosts = new StoreDictionary<uint, ItemCurrencyCost>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_currency_cost });
        public static readonly StoreDictionary<uint, ItemEffect> ItemEffects = new StoreDictionary<uint, ItemEffect>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_effect });
        public static readonly StoreDictionary<uint, ItemExtendedCost> ItemExtendedCosts = new StoreDictionary<uint, ItemExtendedCost>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_extended_cost });
        public static readonly StoreDictionary<uint, ItemModifiedAppearance> ItemModifiedAppearances = new StoreDictionary<uint, ItemModifiedAppearance>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_modified_appearance });
        public static readonly StoreDictionary<uint, ItemSparse> ItemSparses = new StoreDictionary<uint, ItemSparse>(new List<HotfixSQLOutput> { HotfixSQLOutput.item_sparse });
        public static readonly StoreDictionary<uint, KeyChain> KeyChains = new StoreDictionary<uint, KeyChain>(new List<HotfixSQLOutput> { HotfixSQLOutput.key_chain });
        public static readonly StoreDictionary<uint, Location> Locations = new StoreDictionary<uint, Location>(new List<HotfixSQLOutput> { HotfixSQLOutput.location });
        public static readonly StoreDictionary<uint, Mount> Mounts = new StoreDictionary<uint, Mount>(new List<HotfixSQLOutput> { HotfixSQLOutput.mount });
        public static readonly StoreDictionary<uint, OverrideSpellData> OverrideSpellDatas = new StoreDictionary<uint, OverrideSpellData>(new List<HotfixSQLOutput> { HotfixSQLOutput.override_spell_data });
        public static readonly StoreDictionary<uint, PhaseXPhaseGroup> PhaseXPhaseGroups = new StoreDictionary<uint, PhaseXPhaseGroup>(new List<HotfixSQLOutput> { HotfixSQLOutput.phase_group });
        public static readonly StoreDictionary<uint, SceneScript> SceneScripts = new StoreDictionary<uint, SceneScript>(new List<HotfixSQLOutput> { HotfixSQLOutput.scene_script });
        public static readonly StoreDictionary<uint, SpellAuraRestrictions> SpellAuraRestrictions = new StoreDictionary<uint, SpellAuraRestrictions>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_aura_restrictions });
        public static readonly StoreDictionary<uint, SpellCastingRequirements> SpellCastingRequirements = new StoreDictionary<uint, SpellCastingRequirements>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_casting_requirements });
        public static readonly StoreDictionary<uint, SpellClassOptions> SpellClassOptions = new StoreDictionary<uint, SpellClassOptions>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_class_options });
        public static readonly StoreDictionary<uint, SpellEffectGroupSize> SpellEffectGroupSizes = new StoreDictionary<uint, SpellEffectGroupSize>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_effect_group_size });
        public static readonly StoreDictionary<uint, SpellLearnSpell> SpellLearnSpells = new StoreDictionary<uint, SpellLearnSpell>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_learn_spell });
        public static readonly StoreDictionary<uint, SpellMisc> SpellMiscs = new StoreDictionary<uint, SpellMisc>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_misc });
        public static readonly StoreDictionary<uint, SpellTotems> SpellTotems = new StoreDictionary<uint, SpellTotems>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_totems });
        public static readonly StoreDictionary<uint, SpellPower> SpellPowers = new StoreDictionary<uint, SpellPower>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_power });
        public static readonly StoreDictionary<uint, SpellReagents> SpellReagents = new StoreDictionary<uint, SpellReagents>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_power });
        public static readonly StoreDictionary<uint, SpellRuneCost> SpellRuneCosts = new StoreDictionary<uint, SpellRuneCost>(new List<HotfixSQLOutput> { HotfixSQLOutput.spell_rune_cost });
        public static readonly StoreDictionary<uint, TaxiNodes> TaxiNodes = new StoreDictionary<uint, TaxiNodes>(new List<HotfixSQLOutput> { HotfixSQLOutput.taxi_nodes });
        public static readonly StoreDictionary<uint, TaxiPathNode> TaxiPathNodes = new StoreDictionary<uint, TaxiPathNode>(new List<HotfixSQLOutput> { HotfixSQLOutput.taxi_path_node });
        public static readonly StoreDictionary<uint, TaxiPath> TaxiPaths = new StoreDictionary<uint, TaxiPath>(new List<HotfixSQLOutput> { HotfixSQLOutput.taxi_path_node });
        public static readonly StoreDictionary<uint, Toy> Toys = new StoreDictionary<uint, Toy>(new List<HotfixSQLOutput> { HotfixSQLOutput.toy });

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
        public static readonly DataBag<GossipMenu> Gossips = new DataBag<GossipMenu>(new List<SQLOutput> { SQLOutput.gossip_menu_option });
        public static readonly DataBag<GossipMenuOption> GossipMenuOptions = new DataBag<GossipMenuOption>(new List<SQLOutput> { SQLOutput.gossip_menu_option }); 

        // Quest POI (QuestId, Id)
        public static readonly DataBag<QuestPOI> QuestPOIs = new DataBag<QuestPOI>(new List<SQLOutput> { SQLOutput.quest_poi_points });
        public static readonly DataBag<QuestPOIPoint> QuestPOIPoints = new DataBag<QuestPOIPoint>(new List<SQLOutput> { SQLOutput.quest_poi_points }); // WoD

        // Quest Misc
        public static readonly DataBag<QuestGreeting> QuestGreetings = new DataBag<QuestGreeting>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly DataBag<QuestDetails> QuestDetails = new DataBag<QuestDetails>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly DataBag<QuestRequestItems> QuestRequestItems = new DataBag<QuestRequestItems>(new List<SQLOutput> { SQLOutput.quest_template });
        //public static readonly StoreDictionary<uint, QuestOfferReward> QuestOfferRewards = new StoreDictionary<uint, QuestOfferReward>(new List<SQLOutput> { SQLOutput.quest_template });

        // Names
        public static readonly DataBag<ObjectName> ObjectNames = new DataBag<ObjectName>(new List<SQLOutput> { SQLOutput.ObjectNames });

        // Vehicle Template Accessory
        public static readonly StoreBag<VehicleTemplateAccessory> VehicleTemplateAccessorys = new StoreBag<VehicleTemplateAccessory>(new List<SQLOutput> { SQLOutput.vehicle_template_accessory });

        // Weather updates
        public static readonly DataBag<WeatherUpdate> WeatherUpdates = new DataBag<WeatherUpdate>(new List<SQLOutput> { SQLOutput.weather_updates });

        // Npc Spell Click
        public static readonly StoreBag<WowGuid> NpcSpellClicks = new StoreBag<WowGuid>(new List<SQLOutput> { SQLOutput.npc_spellclick_spells });
        public static readonly DataBag<NpcSpellClick> SpellClicks = new DataBag<NpcSpellClick>(new List<SQLOutput> { SQLOutput.npc_spellclick_spells });

        // Quest Misc
        public static readonly StoreDictionary<Tuple<uint, string>, BroadcastTextLocale> BroadcastTextLocales = new StoreDictionary<Tuple<uint, string>, BroadcastTextLocale>(new List<HotfixSQLOutput> { HotfixSQLOutput.broadcast_text_locale });
        public static readonly StoreDictionary<Tuple<uint, string>, LocalesQuest> LocalesQuests = new StoreDictionary<Tuple<uint, string>, LocalesQuest>(new List<SQLOutput> { SQLOutput.locales_quest });
        public static readonly DataBag<QuestObjectivesLocale> LocalesQuestObjectives = new DataBag<QuestObjectivesLocale>(new List<SQLOutput> { SQLOutput.locales_quest_objectives });

        public static void ClearContainers()
        {
            SniffData.Clear();

            Objects.Clear();

            CreatureDifficultys.Clear();
            GameObjects.Clear();
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
            //QuestOffers.Clear();
            //QuestRewards.Clear();
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
