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
        public static readonly StoreDictionary<uint, BroadcastText> BroadcastTexts = new StoreDictionary<uint, BroadcastText>(new List<SQLOutput> { SQLOutput.broadcast_text });
        public static readonly StoreDictionary<uint, CurvePoint> CurvePoints = new StoreDictionary<uint, CurvePoint>(new List<SQLOutput> { SQLOutput.curve_point });
        public static readonly StoreDictionary<uint, SpellMisc> SpellMiscs = new StoreDictionary<uint, SpellMisc>(new List<SQLOutput> { SQLOutput.spell_misc });
        public static readonly StoreDictionary<uint, CreatureDifficulty> CreatureDifficultys = new StoreDictionary<uint, CreatureDifficulty>(new List<SQLOutput> { SQLOutput.creature_template });
        public static readonly StoreDictionary<uint, GameObjectTemplateDB2> GameObjectTemplateDB2s = new StoreDictionary<uint, GameObjectTemplateDB2>(new List<SQLOutput> { SQLOutput.gameobject_template });

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

        public static void ClearContainers()
        {
            SniffData.Clear();

            Objects.Clear();

            CreatureDifficultys.Clear();
            GameObjectTemplateDB2s.Clear();
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
