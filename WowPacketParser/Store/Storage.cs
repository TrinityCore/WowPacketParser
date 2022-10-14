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

        // Templates
        public static readonly DataBag<AreaTriggerTemplate> AreaTriggerTemplates = new DataBag<AreaTriggerTemplate>(new List<SQLOutput> { SQLOutput.areatrigger_template });
        public static readonly DataBag<AreaTriggerCreatePropertiesOrbit> AreaTriggerCreatePropertiesOrbits = new DataBag<AreaTriggerCreatePropertiesOrbit>(new List<SQLOutput> { SQLOutput.areatrigger_create_properties_orbit });
        public static readonly DataBag<AreaTriggerCreatePropertiesPolygonVertex> AreaTriggerCreatePropertiesPolygonVertices = new DataBag<AreaTriggerCreatePropertiesPolygonVertex>(new List<SQLOutput> { SQLOutput.areatrigger_create_properties_polygon_vertex });
        public static readonly DataBag<AreaTriggerCreatePropertiesSplinePoint> AreaTriggerCreatePropertiesSplinePoints = new DataBag<AreaTriggerCreatePropertiesSplinePoint>(new List<SQLOutput> { SQLOutput.areatrigger_create_properties_spline_point });
        public static readonly DataBag<ConversationActor> ConversationActors = new DataBag<ConversationActor>(new List<SQLOutput> { SQLOutput.conversation_actors });
        public static readonly DataBag<ConversationActorTemplate> ConversationActorTemplates = new DataBag<ConversationActorTemplate>(new List<SQLOutput> { SQLOutput.conversation_actor_template });
        public static readonly DataBag<ConversationLineTemplate> ConversationLineTemplates = new DataBag<ConversationLineTemplate>(new List<SQLOutput> { SQLOutput.conversation_line_template });
        public static readonly DataBag<ConversationTemplate> ConversationTemplates = new DataBag<ConversationTemplate>(new List<SQLOutput> { SQLOutput.conversation_template });
        public static readonly DataBag<GameObjectTemplate> GameObjectTemplates = new DataBag<GameObjectTemplate>(new List<SQLOutput> { SQLOutput.gameobject_template });
        public static readonly DataBag<GameObjectTemplateQuestItem> GameObjectTemplateQuestItems = new DataBag<GameObjectTemplateQuestItem>(new List<SQLOutput> { SQLOutput.gameobject_template });
        public static readonly DataBag<GameObjectQuestStarter> GameObjectQuestStarters = new DataBag<GameObjectQuestStarter>(new List<SQLOutput> { SQLOutput.gameobject_queststarter });
        public static readonly DataBag<GameObjectQuestEnder> GameObjectQuestEnders = new DataBag<GameObjectQuestEnder>(new List<SQLOutput> { SQLOutput.gameobject_questender });
        public static readonly DataBag<ItemTemplate> ItemTemplates = new DataBag<ItemTemplate>(new List<SQLOutput> { SQLOutput.item_template });
        public static readonly DataBag<QuestTemplate> QuestTemplates = new DataBag<QuestTemplate>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly DataBag<QuestObjective> QuestObjectives = new DataBag<QuestObjective>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly DataBag<QuestVisualEffect> QuestVisualEffects = new DataBag<QuestVisualEffect>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly DataBag<QuestRewardDisplaySpell> QuestRewardDisplaySpells = new DataBag<QuestRewardDisplaySpell>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly StoreDictionary<uint, CreatureTemplate> CreatureTemplates = new StoreDictionary<uint, CreatureTemplate>(new List<SQLOutput> { SQLOutput.creature_template });
        public static readonly DataBag<CreatureTemplateClassic> CreatureTemplatesClassic = new DataBag<CreatureTemplateClassic>(new List<SQLOutput> { SQLOutput.creature_template });
        public static readonly DataBag<CreatureTemplateNonWDB> CreatureTemplatesNonWDB = new DataBag<CreatureTemplateNonWDB>(new List<SQLOutput> { SQLOutput.creature_template });
        public static readonly DataBag<CreatureTemplateQuestItem> CreatureTemplateQuestItems = new DataBag<CreatureTemplateQuestItem>(new List<SQLOutput> { SQLOutput.creature_template });
        public static readonly DataBag<CreatureTemplateScaling> CreatureTemplateScalings = new DataBag<CreatureTemplateScaling>(new List<SQLOutput> { SQLOutput.creature_template_scaling });
        public static readonly DataBag<CreatureTemplateModel> CreatureTemplateModels = new DataBag<CreatureTemplateModel>(new List<SQLOutput> { SQLOutput.creature_template });
        public static readonly DataBag<CreatureTemplateSpell> CreatureTemplateSpells = new DataBag<CreatureTemplateSpell>(new List<SQLOutput> { SQLOutput.creature_template });
        public static readonly DataBag<CreatureQuestStarter> CreatureQuestStarters = new DataBag<CreatureQuestStarter>(new List<SQLOutput> { SQLOutput.creature_queststarter });
        public static readonly DataBag<CreatureQuestEnder> CreatureQuestEnders = new DataBag<CreatureQuestEnder>(new List<SQLOutput> { SQLOutput.creature_questender });
        // Vendor & trainer
        public static readonly DataBag<NpcTrainer> NpcTrainers = new DataBag<NpcTrainer>(new List<SQLOutput> { SQLOutput.npc_trainer }); // legacy 3.3.5 support
        public static readonly DataBag<NpcVendor> NpcVendors = new DataBag<NpcVendor>(new List<SQLOutput> { SQLOutput.npc_vendor });
        public static readonly DataBag<Trainer> Trainers = new DataBag<Trainer>(new List<SQLOutput> { SQLOutput.trainer });
        public static readonly DataBag<TrainerSpell> TrainerSpells = new DataBag<TrainerSpell>(new List<SQLOutput> { SQLOutput.trainer });
        public static readonly DataBag<CreatureTrainer> CreatureTrainers = new DataBag<CreatureTrainer>(new List<SQLOutput> { SQLOutput.trainer });

        // Page & npc text
        public static readonly DataBag<PageText> PageTexts = new DataBag<PageText>(new List<SQLOutput> { SQLOutput.page_text });
        public static readonly DataBag<NpcText> NpcTexts = new DataBag<NpcText>(new List<SQLOutput> { SQLOutput.npc_text });
        public static readonly DataBag<NpcTextMop> NpcTextsMop = new DataBag<NpcTextMop>(new List<SQLOutput> { SQLOutput.npc_text });
        public static readonly StoreDictionary<uint /*menuID*/, NpcText925> GossipToNpcTextMap = new(new List<SQLOutput> { SQLOutput.npc_text });

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
        public static readonly DataBag<PlayerCreateInfo> StartPositions = new DataBag<PlayerCreateInfo>(new List<SQLOutput> { SQLOutput.playercreateinfo });

        // Gossips (MenuId, TextId)
        public static readonly Dictionary<uint, uint> CreatureDefaultGossips = new Dictionary<uint, uint>();
        public static readonly DataBag<GossipMenu> Gossips = new DataBag<GossipMenu>(new List<SQLOutput> { SQLOutput.gossip_menu });
        public static readonly DataBag<GossipMenu925> Gossips925 = new DataBag<GossipMenu925>(new List<SQLOutput> { SQLOutput.gossip_menu });
        public static readonly DataBag<GossipMenuAddon> GossipMenuAddons = new DataBag<GossipMenuAddon>(new List<SQLOutput> { SQLOutput.gossip_menu_addon });
        public static readonly StoreDictionary<(uint?, uint?), GossipMenuOption> GossipMenuOptions = new StoreDictionary<(uint?, uint?), GossipMenuOption>(new List<SQLOutput> { SQLOutput.gossip_menu_option });
        public static readonly DataBag<GossipMenuOptionAddon> GossipMenuOptionAddons = new DataBag<GossipMenuOptionAddon>(new List<SQLOutput> { SQLOutput.gossip_menu_option });

        // Quest POI (QuestId, Id)
        public static readonly DataBag<QuestPOI> QuestPOIs = new DataBag<QuestPOI>(new List<SQLOutput> { SQLOutput.quest_poi_points });
        public static readonly DataBag<QuestPOIPoint> QuestPOIPoints = new DataBag<QuestPOIPoint>(new List<SQLOutput> { SQLOutput.quest_poi_points }); // WoD

        // Quest Misc
        public static readonly DataBag<QuestGreeting> QuestGreetings = new DataBag<QuestGreeting>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly DataBag<QuestDetails> QuestDetails = new DataBag<QuestDetails>(new List<SQLOutput> { SQLOutput.quest_template });
        public static readonly DataBag<QuestRequestItems> QuestRequestItems = new DataBag<QuestRequestItems>(new List<SQLOutput> { SQLOutput.quest_template });

        // Names
        public static readonly DataBag<ObjectName> ObjectNames = new DataBag<ObjectName>();

        // Vehicle Template Accessory
        public static readonly DataBag<VehicleTemplateAccessory> VehicleTemplateAccessories = new DataBag<VehicleTemplateAccessory>(new List<SQLOutput> { SQLOutput.vehicle_template_accessory });

        // Weather updates
        public static readonly DataBag<WeatherUpdate> WeatherUpdates = new DataBag<WeatherUpdate>(new List<SQLOutput> { SQLOutput.weather_updates });

        // Npc Spell Click
        public static readonly StoreBag<WowGuid> NpcSpellClicks = new StoreBag<WowGuid>(new List<SQLOutput> { SQLOutput.npc_spellclick_spells });
        public static readonly DataBag<NpcSpellClick> SpellClicks = new DataBag<NpcSpellClick>(new List<SQLOutput> { SQLOutput.npc_spellclick_spells });

        // Locales
        public static readonly DataBag<CreatureTemplateLocale> LocalesCreatures = new DataBag<CreatureTemplateLocale>(new List<SQLOutput> { SQLOutput.creature_template_locale });
        public static readonly DataBag<LocalesQuest> LocalesQuests = new DataBag<LocalesQuest>(new List<SQLOutput> { SQLOutput.locales_quest });
        public static readonly DataBag<QuestObjectivesLocale> LocalesQuestObjectives = new DataBag<QuestObjectivesLocale>(new List<SQLOutput> { SQLOutput.locales_quest_objectives });
        public static readonly DataBag<QuestOfferRewardLocale> LocalesQuestOfferRewards = new DataBag<QuestOfferRewardLocale>(new List<SQLOutput> { SQLOutput.locales_quest });
        public static readonly DataBag<QuestGreetingLocale> LocalesQuestGreeting = new DataBag<QuestGreetingLocale>(new List<SQLOutput> { SQLOutput.locales_quest });
        public static readonly DataBag<QuestRequestItemsLocale> LocalesQuestRequestItems = new DataBag<QuestRequestItemsLocale>(new List<SQLOutput> { SQLOutput.locales_quest });
        public static readonly DataBag<PageTextLocale> LocalesPageText = new DataBag<PageTextLocale>(new List<SQLOutput> { SQLOutput.page_text_locale });

        // Spell Target Position
        public static readonly DataBag<SpellTargetPosition> SpellTargetPositions = new DataBag<SpellTargetPosition>(new List<SQLOutput> { SQLOutput.spell_target_position });

        public static readonly DataBag<HotfixData> HotfixDatas = new DataBag<HotfixData>(new List<SQLOutput> { SQLOutput.hotfix_data });
        public static readonly DataBag<HotfixBlob> HotfixBlobs = new DataBag<HotfixBlob>(new List<SQLOutput> { SQLOutput.hotfix_blob });
        public static readonly DataBag<HotfixOptionalData> HotfixOptionalDatas = new DataBag<HotfixOptionalData>(new List<SQLOutput> { SQLOutput.hotfix_optional_data });
        // Scenes
        public static readonly DataBag<SceneTemplate> Scenes = new DataBag<SceneTemplate>(new List<SQLOutput> { SQLOutput.scene_template });

        // Scenario
        public static readonly DataBag<ScenarioPOI> ScenarioPOIs = new DataBag<ScenarioPOI>(new List<SQLOutput> { SQLOutput.scenario_poi });
        public static readonly DataBag<ScenarioPOIPoint> ScenarioPOIPoints = new DataBag<ScenarioPOIPoint>(new List<SQLOutput> { SQLOutput.scenario_poi });

        public static readonly DataBag<BroadcastText> BroadcastTexts = new DataBag<BroadcastText>(new List<SQLOutput> { SQLOutput.broadcast_text });
        public static readonly DataBag<BroadcastTextLocale> BroadcastTextLocales = new DataBag<BroadcastTextLocale>(new List<SQLOutput> { SQLOutput.broadcast_text_locale });

        //Player Choice
        public static readonly DataBag<PlayerChoiceTemplate> PlayerChoices = new DataBag<PlayerChoiceTemplate>(new List<SQLOutput> { SQLOutput.playerchoice });
        public static readonly DataBag<PlayerChoiceLocaleTemplate> PlayerChoiceLocales = new DataBag<PlayerChoiceLocaleTemplate>(new List<SQLOutput> { SQLOutput.playerchoice });
        public static readonly DataBag<PlayerChoiceResponseTemplate> PlayerChoiceResponses = new DataBag<PlayerChoiceResponseTemplate>(new List<SQLOutput> { SQLOutput.playerchoice });
        public static readonly DataBag<PlayerChoiceResponseLocaleTemplate> PlayerChoiceResponseLocales = new DataBag<PlayerChoiceResponseLocaleTemplate>(new List<SQLOutput> { SQLOutput.playerchoice });
        public static readonly DataBag<PlayerChoiceResponseRewardTemplate> PlayerChoiceResponseRewards = new DataBag<PlayerChoiceResponseRewardTemplate>(new List<SQLOutput> { SQLOutput.playerchoice });
        public static readonly DataBag<PlayerChoiceResponseRewardCurrencyTemplate> PlayerChoiceResponseRewardCurrencies = new DataBag<PlayerChoiceResponseRewardCurrencyTemplate>(new List<SQLOutput> { SQLOutput.playerchoice });
        public static readonly DataBag<PlayerChoiceResponseRewardFactionTemplate> PlayerChoiceResponseRewardFactions = new DataBag<PlayerChoiceResponseRewardFactionTemplate>(new List<SQLOutput> { SQLOutput.playerchoice });
        public static readonly DataBag<PlayerChoiceResponseRewardItemTemplate> PlayerChoiceResponseRewardItems = new DataBag<PlayerChoiceResponseRewardItemTemplate>(new List<SQLOutput> { SQLOutput.playerchoice });

        public static void ClearContainers()
        {
            SniffData.Clear();

            Objects.Clear();

            AreaTriggerCreatePropertiesOrbits.Clear();
            AreaTriggerCreatePropertiesPolygonVertices.Clear();
            AreaTriggerCreatePropertiesSplinePoints.Clear();
            AreaTriggerTemplates.Clear();
            ConversationActors.Clear();
            ConversationActorTemplates.Clear();
            ConversationLineTemplates.Clear();
            ConversationTemplates.Clear();
            GameObjectTemplates.Clear();
            GameObjectTemplateQuestItems.Clear();
            ItemTemplates.Clear();
            QuestTemplates.Clear();
            QuestObjectives.Clear();
            QuestVisualEffects.Clear();
            QuestRewardDisplaySpells.Clear();
            CreatureTemplates.Clear();
            CreatureTemplatesClassic.Clear();
            CreatureTemplatesNonWDB.Clear();
            CreatureTemplateQuestItems.Clear();
            CreatureTemplateScalings.Clear();
            CreatureTemplateModels.Clear();
            CreatureTemplateSpells.Clear();

            NpcTrainers.Clear();
            NpcVendors.Clear();
            Trainers.Clear();
            TrainerSpells.Clear();
            CreatureTrainers.Clear();

            PageTexts.Clear();
            NpcTexts.Clear();
            NpcTextsMop.Clear();
            GossipToNpcTextMap.Clear();

            CreatureTexts.Clear();

            GossipPOIs.Clear();

            Emotes.Clear();
            Sounds.Clear();
            SpellsX.Clear();
            QuestOfferRewards.Clear();
            GossipSelects.Clear();

            StartActions.Clear();
            StartPositions.Clear();

            Gossips.Clear();
            Gossips925.Clear();
            GossipMenuAddons.Clear();
            GossipMenuOptions.Clear();
            GossipMenuOptionAddons.Clear();

            QuestPOIs.Clear();
            QuestPOIPoints.Clear();

            QuestGreetings.Clear();
            QuestDetails.Clear();
            QuestRequestItems.Clear();

            ObjectNames.Clear();

            VehicleTemplateAccessories.Clear();

            WeatherUpdates.Clear();

            NpcSpellClicks.Clear();
            SpellClicks.Clear();

            SpellTargetPositions.Clear();

            LocalesCreatures.Clear();
            LocalesQuests.Clear();
            LocalesQuestObjectives.Clear();
            LocalesQuestOfferRewards.Clear();
            LocalesQuestGreeting.Clear();
            LocalesQuestRequestItems.Clear();
            LocalesPageText.Clear();

            HotfixDatas.Clear();
            HotfixBlobs.Clear();
            HotfixOptionalDatas.Clear();

            Scenes.Clear();

            ScenarioPOIs.Clear();
            ScenarioPOIPoints.Clear();

            BroadcastTexts.Clear();
            BroadcastTextLocales.Clear();

            PlayerChoices.Clear();
            PlayerChoiceLocales.Clear();
            PlayerChoiceResponses.Clear();
            PlayerChoiceResponseLocales.Clear();
            PlayerChoiceResponseRewards.Clear();
            PlayerChoiceResponseRewardCurrencies.Clear();
            PlayerChoiceResponseRewardFactions.Clear();
            PlayerChoiceResponseRewardItems.Clear();
        }
    }
}
