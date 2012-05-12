using System;
using WowPacketParser.Enums;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Store
{
    public static class Storage
    {
        // Stores opcodes read, npc/GOs/spell/item/etc IDs found in sniffs
        // and other miscellaneous stuff
        public static readonly StoreBag<SniffData> SniffData = new StoreBag<SniffData>("SniffData");

        /* Key: Guid */

        // Units, GameObjects, Players, Items
        public static readonly StoreDictionary<Guid, WoWObject> Objects = new StoreDictionary<Guid, WoWObject>("Objects");

        /* Key: Entry */

        // Templates
        public static readonly StoreDictionary<uint, GameObjectTemplate> GameObjectTemplates = new StoreDictionary<uint, GameObjectTemplate>("GameObjectTemplates");
        public static readonly StoreDictionary<uint, ItemTemplate> ItemTemplates = new StoreDictionary<uint, ItemTemplate>("ItemTemplates");
        public static readonly StoreDictionary<uint, QuestTemplate> QuestTemplates = new StoreDictionary<uint, QuestTemplate>("QuestTemplates");
        public static readonly StoreDictionary<uint, UnitTemplate> UnitTemplates = new StoreDictionary<uint, UnitTemplate>("UnitTemplates");

        // Vendor & trainer
        public static readonly StoreDictionary<uint, NpcTrainer> NpcTrainers = new StoreDictionary<uint, NpcTrainer>("NpcTrainers");
        public static readonly StoreDictionary<uint, NpcVendor> NpcVendors = new StoreDictionary<uint, NpcVendor>("NpcVendors");

        // Page & npc text
        public static readonly StoreDictionary<uint, PageText> PageTexts = new StoreDictionary<uint, PageText>("PageTexts");
        public static readonly StoreDictionary<uint, NpcText> NpcTexts = new StoreDictionary<uint, NpcText>("NpcTexts");

        // `creature_text`
        public static readonly StoreMulti<uint, CreatureText> CreatureTexts = new StoreMulti<uint, CreatureText>("CreatureTexts");

        // "Helper" stores, do not match a specific table
        public static readonly StoreMulti<Guid, EmoteType> Emotes = new StoreMulti<Guid, EmoteType>("Emotes");
        public static readonly StoreBag<uint> Sounds = new StoreBag<uint>("Sounds");
        public static readonly StoreDictionary<uint, SpellsX> SpellsX = new StoreDictionary<uint, SpellsX>("SpellsX"); // `creature_template`.`spellsX`

        /* Key: Misc */

        // Start info (Race, Class)
        public static readonly StoreDictionary<Tuple<Race, Class>, StartAction> StartActions = new StoreDictionary<Tuple<Race, Class>, StartAction>("StartActions");
        public static readonly StoreDictionary<Tuple<Race, Class>, StartSpell> StartSpells = new StoreDictionary<Tuple<Race, Class>, StartSpell>("StartSpells");
        public static readonly StoreDictionary<Tuple<Race, Class>, StartPosition> StartPositions = new StoreDictionary<Tuple<Race, Class>, StartPosition>("StartPositions");

        // Gossips (MenuId, TextId)
        public static readonly StoreDictionary<Tuple<uint, uint>, Gossip> Gossips = new StoreDictionary<Tuple<uint, uint>, Gossip>("Gossips");

        // Loot (ItemId, LootType)
        public static readonly StoreDictionary<Tuple<uint, ObjectType>, Loot> Loots = new StoreDictionary<Tuple<uint, ObjectType>, Loot>("Loots");

        // Quest POI (QuestId, Id)
        public static readonly StoreDictionary<Tuple<uint, uint>, QuestPOI> QuestPOIs = new StoreDictionary<Tuple<uint, uint>, QuestPOI>("QuestPOIs");

        // Names
        public static readonly StoreDictionary<uint, ObjectName> ObjectNames = new StoreDictionary<uint, ObjectName>("ObjectNames");

        public static void ClearContainers()
        {
            SniffData.Clear();

            Objects.Clear();

            GameObjectTemplates.Clear();
            ItemTemplates.Clear();
            QuestTemplates.Clear();
            UnitTemplates.Clear();

            NpcTrainers.Clear();
            NpcVendors.Clear();

            PageTexts.Clear();
            NpcTexts.Clear();

            SpellsX.Clear();
            CreatureTexts.Clear();
            Emotes.Clear();
            Sounds.Clear();

            StartActions.Clear();
            StartSpells.Clear();
            StartPositions.Clear();

            Gossips.Clear();

            Loots.Clear();

            QuestPOIs.Clear();

            ObjectNames.Clear();
        }
    }
}
