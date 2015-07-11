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
        public static readonly StoreBag<SniffData> SniffData = new StoreBag<SniffData>(new List<SQLOutput> { SQLOutput.SniffData, SQLOutput.SniffDataOpcodes });

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
        public static readonly StoreDictionary<uint, GameObjectTemplate> GameObjectTemplates = new StoreDictionary<uint, GameObjectTemplate>(new List<SQLOutput> { SQLOutput.gameobject_template });
        public static readonly StoreDictionary<uint, ItemTemplate> ItemTemplates = new StoreDictionary<uint, ItemTemplate>(new List<SQLOutput> { SQLOutput.item_template });
        public static readonly StoreDictionary<uint, QuestTemplate> QuestTemplates = new StoreDictionary<uint, QuestTemplate>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly StoreDictionary<uint, QuestTemplateWod> QuestTemplatesWod = new StoreDictionary<uint, QuestTemplateWod>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly StoreDictionary<uint, QuestInfoObjective> QuestObjectives = new StoreDictionary<uint, QuestInfoObjective>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly StoreDictionary<uint, UnitTemplate> UnitTemplates = new StoreDictionary<uint, UnitTemplate>(new List<SQLOutput> { SQLOutput.creature_template });

        // Vendor & trainer
        public static readonly StoreDictionary<uint, NpcTrainer> NpcTrainers = new StoreDictionary<uint, NpcTrainer>(new List<SQLOutput> { SQLOutput.npc_trainer });
        public static readonly StoreDictionary<uint, NpcVendor> NpcVendors = new StoreDictionary<uint, NpcVendor>(new List<SQLOutput> { SQLOutput.npc_vendor });

        // Page & npc text
        public static readonly StoreDictionary<uint, PageText> PageTexts = new StoreDictionary<uint, PageText>(new List<SQLOutput> { SQLOutput.page_text });
        public static readonly StoreDictionary<uint, NpcText> NpcTexts = new StoreDictionary<uint, NpcText>(new List<SQLOutput> { SQLOutput.npc_text });
        public static readonly StoreDictionary<uint, NpcTextMop> NpcTextsMop = new StoreDictionary<uint, NpcTextMop>(new List<SQLOutput> { SQLOutput.npc_text });

        // Creature text (says, yells, etc.)
        public static readonly StoreMulti<uint, CreatureText> CreatureTexts = new StoreMulti<uint, CreatureText>(new List<SQLOutput> { SQLOutput.creature_text });

        // Points of Interest
        public static readonly StoreDictionary<uint, GossipPOI> GossipPOIs = new StoreDictionary<uint, GossipPOI>(new List<SQLOutput> { SQLOutput.points_of_interest });

        // "Helper" stores, do not match a specific table
        public static readonly StoreMulti<WowGuid, EmoteType> Emotes = new StoreMulti<WowGuid, EmoteType>(new List<SQLOutput> { SQLOutput.creature_text });
        public static readonly StoreBag<uint> Sounds = new StoreBag<uint>(new List<SQLOutput> { SQLOutput.creature_text });
        public static readonly StoreDictionary<uint, SpellsX> SpellsX = new StoreDictionary<uint, SpellsX>(new List<SQLOutput> { SQLOutput.creature_template }); // `creature_template`.`spellsX`
        public static readonly StoreDictionary<uint, QuestOffer> QuestOffers = new StoreDictionary<uint, QuestOffer>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly StoreDictionary<uint, QuestReward> QuestRewards = new StoreDictionary<uint, QuestReward>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly StoreDictionary<Tuple<uint, uint>, object> GossipSelects = new StoreDictionary<Tuple<uint, uint>, object>(new List<SQLOutput> { SQLOutput.points_of_interest, SQLOutput.gossip_menu, SQLOutput.gossip_menu_option });

        /* Key: Misc */

        // Start info (Race, Class)
        public static readonly StoreDictionary<Tuple<Race, Class>, StartAction> StartActions = new StoreDictionary<Tuple<Race, Class>, StartAction>(new List<SQLOutput> { SQLOutput.playercreateinfo_action });
        public static readonly StoreDictionary<Tuple<Race, Class>, StartSpell> StartSpells = new StoreDictionary<Tuple<Race, Class>, StartSpell>(new List<SQLOutput> { SQLOutput.playercreateinfo_spell });
        public static readonly StoreDictionary<Tuple<Race, Class>, StartPosition> StartPositions = new StoreDictionary<Tuple<Race, Class>, StartPosition>(new List<SQLOutput> { SQLOutput.playercreateinfo });

        // Gossips (MenuId, TextId)
        public static readonly StoreDictionary<Tuple<uint, uint>, Gossip> Gossips = new StoreDictionary<Tuple<uint, uint>, Gossip>(new List<SQLOutput> { SQLOutput.gossip_menu_option });

        // Loot (ItemId, LootType)
        public static readonly StoreDictionary<Tuple<uint, ObjectType>, Loot> Loots = new StoreDictionary<Tuple<uint, ObjectType>, Loot>(new List<SQLOutput> { SQLOutput.LootTemplate });

        // Quest POI (QuestId, Id)
        public static readonly StoreDictionary<Tuple<uint, uint>, QuestPOI> QuestPOIs = new StoreDictionary<Tuple<uint, uint>, QuestPOI>(new List<SQLOutput> { SQLOutput.quest_poi_points });
        public static readonly StoreDictionary<Tuple<int, int>, QuestPOIWoD> QuestPOIWoDs = new StoreDictionary<Tuple<int, int>, QuestPOIWoD>(new List<SQLOutput> { SQLOutput.quest_poi_points }); // WoD
        public static readonly StoreDictionary<Tuple<int, int, int>, QuestPOIPointWoD> QuestPOIPointWoDs = new StoreDictionary<Tuple<int, int, int>, QuestPOIPointWoD>(new List<SQLOutput> { SQLOutput.quest_poi_points }); // WoD

        // Quest Misc
        public static readonly StoreDictionary<uint, QuestGreeting> QuestGreetings = new StoreDictionary<uint, QuestGreeting>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly StoreDictionary<uint, QuestDetails> QuestDetails = new StoreDictionary<uint, QuestDetails>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly StoreDictionary<uint, QuestRequestItems> QuestRequestItems = new StoreDictionary<uint, QuestRequestItems>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly StoreDictionary<uint, QuestOfferReward> QuestOfferRewards = new StoreDictionary<uint, QuestOfferReward>(new List<SQLOutput> { SQLOutput.quest_template });

        // Names
        public static readonly StoreDictionary<uint, ObjectName> ObjectNames = new StoreDictionary<uint, ObjectName>(new List<SQLOutput> { SQLOutput.ObjectNames });

        // Defense Message
        public static readonly StoreMulti<uint, DefenseMessage> DefenseMessages = new StoreMulti<uint, DefenseMessage>(new List<SQLOutput> { SQLOutput.defense_message });

        // Vehicle Template Accessory
        public static readonly StoreMulti<uint, VehicleTemplateAccessory> VehicleTemplateAccessorys = new StoreMulti<uint, VehicleTemplateAccessory>(new List<SQLOutput> { SQLOutput.vehicle_template_accessory });

        // Weather updates
        public static readonly StoreBag<WeatherUpdate> WeatherUpdates = new StoreBag<WeatherUpdate>(new List<SQLOutput> { SQLOutput.weather_updates });

        // Npc Spell Click
        public static readonly StoreBag<WowGuid> NpcSpellClicks = new StoreBag<WowGuid>(new List<SQLOutput> { SQLOutput.npc_spellclick_spells });
        public static readonly StoreBag<NpcSpellClick> SpellClicks = new StoreBag<NpcSpellClick>(new List<SQLOutput> { SQLOutput.npc_spellclick_spells });

        // Quest Misc
        public static readonly StoreDictionary<Tuple<uint, string>, BroadcastTextLocale> BroadcastTextLocales = new StoreDictionary<Tuple<uint, string>, BroadcastTextLocale>(new List<HotfixSQLOutput> { HotfixSQLOutput.broadcast_text_locale });
        public static readonly StoreDictionary<Tuple<uint, string>, LocalesQuest> LocalesQuests = new StoreDictionary<Tuple<uint, string>, LocalesQuest>(new List<SQLOutput> { SQLOutput.locales_quest });
        public static readonly StoreDictionary<Tuple<uint, string>, LocalesQuestObjectives> LocalesQuestObjectives = new StoreDictionary<Tuple<uint, string>, LocalesQuestObjectives>(new List<SQLOutput> { SQLOutput.locales_quest_objectives });

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
            QuestTemplatesWod.Clear();
            QuestObjectives.Clear();
            UnitTemplates.Clear();

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
            QuestOffers.Clear();
            QuestRewards.Clear();
            GossipSelects.Clear();

            StartActions.Clear();
            StartSpells.Clear();
            StartPositions.Clear();

            Gossips.Clear();

            Loots.Clear();

            QuestPOIs.Clear();

            QuestGreetings.Clear();
            QuestDetails.Clear();
            QuestRequestItems.Clear();
            QuestOfferRewards.Clear();

            ObjectNames.Clear();

            DefenseMessages.Clear();

            VehicleTemplateAccessorys.Clear();

            WeatherUpdates.Clear();

            NpcSpellClicks.Clear();
            SpellClicks.Clear();
        }
    }
}
