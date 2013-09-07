using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_3_0_16981.Enums;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_CREATURE_QUERY)]
        public static void HandleCreatureQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var creature = new UnitTemplate();

            var lenS3 = (int)packet.ReadBits(11);
            creature.RacialLeader = packet.ReadBit("Racial Leader");

            var stringLens = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }

            var lenS4 = (int)packet.ReadBits(6);
            var lenS5 = (int)packet.ReadBits(11);
            var qItemCount = packet.ReadBits(22);

            packet.ResetBitReader();

            var name = new string[8];
            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    packet.ReadCString("Female Name", i);
                if (stringLens[i][1] > 1)
                    name[i] = packet.ReadCString("Name", i);
            }
            creature.Name = name[0];

            creature.Modifier1 = packet.ReadSingle("Modifier 1");
            if (lenS3 > 1)
                creature.SubName = packet.ReadCString("Sub Name");

            creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32);

            creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; ++i)
                creature.QuestItems[i] = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Quest Item", i);

            creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32);
            creature.KillCredits = new uint[2];
            for (var i = 0; i < 2; ++i)
                creature.KillCredits[i] = packet.ReadUInt32("Kill Credit", i);
            creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32);
            if (lenS4 > 1)
                creature.IconName = packet.ReadCString("Icon Name");

            creature.DisplayIds = new uint[4];
            creature.DisplayIds[1] = packet.ReadUInt32("Display ID 1");
            creature.DisplayIds[0] = packet.ReadUInt32("Display ID 0");
            creature.MovementId = packet.ReadUInt32("Movement ID");
            creature.DisplayIds[3] = packet.ReadUInt32("Display ID 3");

            creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            if (lenS5 > 1)
                packet.ReadCString("string5");
            creature.DisplayIds[2] = packet.ReadUInt32("Display ID 2");
            creature.Modifier2 = packet.ReadSingle("Modifier 2");
            creature.Expansion = packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");

            var GUID = new byte[8];
            GUID = packet.StartBitStream(7, 1, 4, 3, 0, 2, 6, 5);
            packet.ParseBitStream(GUID, 4, 5, 6, 7, 1, 0, 2, 3);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAME)]
        public static void HandlePlayerQueryName(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var bit16 = packet.ReadBit("bit16");
            guid[6] = packet.ReadBit();
            var bit24 = packet.ReadBit("bit24");

            packet.ParseBitStream(guid, 6, 0, 2, 3, 4, 5, 7, 1);

            if (bit24)
                packet.ReadUInt32("unk28");

            if (bit16)
                packet.ReadUInt32("unk20");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REALM_QUERY)]
        public static void HandleRealmQuery(Packet packet)
        {
            packet.ReadUInt32("Realm Id");
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.ReadUInt32("Realm Id");
            var byte20 = packet.ReadByte("byte20");

            var bits22 = packet.ReadBits(8);
            var bits278 = packet.ReadBits(8);
            packet.ReadBit();

            packet.ReadWoWString("Realmname (without white char)", bits278);
            packet.ReadWoWString("Realmname", bits22);
        }

        [Parser(Opcode.SMSG_NAME_QUERY_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var guid = new byte[8];

            var bit16 = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            if (bit16)
                for (var i = 0; i < 5; ++i)
                    packet.ReadBits("bits", 7);

            var bits32 = packet.ReadBits(6);
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bit83 = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            packet.ReadXORByte(guid, 1);
            packet.ReadWoWString("Name: ", bits32);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);

            packet.ReadByte("Race");
            packet.ReadByte("unk81");
            packet.ReadByte("Gender");

            if (bit16)
                for (var i = 0; i < 5; ++i)
                    packet.ReadCString("Declined Name");

            packet.ReadByte("Class");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadUInt32("unk84");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var type = packet.ReadEnum<DB2Hash>("DB2 File", TypeCode.UInt32);
            packet.ReadTime("Hotfix date");
            var size = packet.ReadInt32("Size");

            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            var entry = (uint)packet.ReadInt32("Entry");
            if ((int)entry < 0)
            {
                packet.WriteLine("Row {0} has been removed.", -(int)entry);
                return;
            }

            switch (type)
            {
                case DB2Hash.Item:    // Item.db2
                    {
                        var item = Storage.ItemTemplates.ContainsKey(entry) ? Storage.ItemTemplates[entry].Item1 : new ItemTemplate();

                        db2File.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
                        item.Class = db2File.ReadEnum<ItemClass>("Class", TypeCode.Int32);
                        item.SubClass = db2File.ReadUInt32("Sub Class");
                        item.SoundOverrideSubclass = db2File.ReadInt32("Sound Override Subclass");
                        item.Material = db2File.ReadEnum<Material>("Material", TypeCode.Int32);
                        item.DisplayId = db2File.ReadUInt32("Display ID");
                        item.InventoryType = db2File.ReadEnum<InventoryType>("Inventory Type", TypeCode.UInt32);
                        item.SheathType = db2File.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);

                        Storage.ItemTemplates.Add(entry, item, packet.TimeSpan);
                        packet.AddSniffData(StoreNameType.Item, (int)entry, "DB_REPLY");
                        break;
                    }
                case DB2Hash.Item_sparse:    // Item-sparse.db2
                    {
                        var item = Storage.ItemTemplates.ContainsKey(entry) ? Storage.ItemTemplates[entry].Item1 : new ItemTemplate();

                        db2File.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
                        item.Quality = db2File.ReadEnum<ItemQuality>("Quality", TypeCode.Int32);
                        item.Flags = db2File.ReadEnum<ItemProtoFlags>("Flags", TypeCode.UInt32);
                        item.ExtraFlags = db2File.ReadEnum<ItemFlagExtra>("Extra Flags", TypeCode.Int32);
                        item.Unk430_1 = db2File.ReadSingle("Unk430_1");
                        item.Unk430_2 = db2File.ReadSingle("Unk430_2");
                        item.Unk530_1 = db2File.ReadSingle("Unk530_1");
                        item.BuyCount = db2File.ReadUInt32("Buy count");
                        item.BuyPrice = db2File.ReadUInt32("Buy Price");
                        item.SellPrice = db2File.ReadUInt32("Sell Price");
                        item.InventoryType = db2File.ReadEnum<InventoryType>("Inventory Type", TypeCode.Int32);
                        item.AllowedClasses = db2File.ReadEnum<ClassMask>("Allowed Classes", TypeCode.Int32);
                        item.AllowedRaces = db2File.ReadEnum<RaceMask>("Allowed Races", TypeCode.Int32);
                        item.ItemLevel = db2File.ReadUInt32("Item Level");
                        item.RequiredLevel = db2File.ReadUInt32("Required Level");
                        item.RequiredSkillId = db2File.ReadUInt32("Required Skill ID");
                        item.RequiredSkillLevel = db2File.ReadUInt32("Required Skill Level");
                        item.RequiredSpell = (uint)db2File.ReadEntryWithName<Int32>(StoreNameType.Spell, "Required Spell");
                        item.RequiredHonorRank = db2File.ReadUInt32("Required Honor Rank");
                        item.RequiredCityRank = db2File.ReadUInt32("Required City Rank");
                        item.RequiredRepFaction = db2File.ReadUInt32("Required Rep Faction");
                        item.RequiredRepValue = db2File.ReadUInt32("Required Rep Value");
                        item.MaxCount = db2File.ReadInt32("Max Count");
                        item.MaxStackSize = db2File.ReadInt32("Max Stack Size");
                        item.ContainerSlots = db2File.ReadUInt32("Container Slots");

                        item.StatTypes = new ItemModType[10];
                        for (var i = 0; i < 10; i++)
                        {
                            var statType = db2File.ReadEnum<ItemModType>("Stat Type", TypeCode.Int32, i);
                            item.StatTypes[i] = statType == ItemModType.None ? ItemModType.Mana : statType; // TDB
                        }

                        item.StatValues = new int[10];
                        for (var i = 0; i < 10; i++)
                            item.StatValues[i] = db2File.ReadInt32("Stat Value", i);

                        item.StatUnk1 = new int[10];
                        for (var i = 0; i < 10; i++)
                            item.StatUnk1[i] = db2File.ReadInt32("Unk UInt32 1", i);

                        item.StatUnk2 = new int[10];
                        for (var i = 0; i < 10; i++)
                            item.StatUnk2[i] = db2File.ReadInt32("Unk UInt32 2", i);

                        item.ScalingStatDistribution = db2File.ReadInt32("Scaling Stat Distribution");
                        item.DamageType = db2File.ReadEnum<DamageType>("Damage Type", TypeCode.Int32);
                        item.Delay = db2File.ReadUInt32("Delay");
                        item.RangedMod = db2File.ReadSingle("Ranged Mod");

                        item.TriggeredSpellIds = new int[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellIds[i] = db2File.ReadEntryWithName<Int32>(StoreNameType.Spell, "Triggered Spell ID", i);

                        item.TriggeredSpellTypes = new ItemSpellTriggerType[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellTypes[i] = db2File.ReadEnum<ItemSpellTriggerType>("Trigger Spell Type", TypeCode.Int32, i);

                        item.TriggeredSpellCharges = new int[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellCharges[i] = db2File.ReadInt32("Triggered Spell Charges", i);

                        item.TriggeredSpellCooldowns = new int[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellCooldowns[i] = db2File.ReadInt32("Triggered Spell Cooldown", i);

                        item.TriggeredSpellCategories = new uint[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellCategories[i] = db2File.ReadUInt32("Triggered Spell Category", i);

                        item.TriggeredSpellCategoryCooldowns = new int[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellCategoryCooldowns[i] = db2File.ReadInt32("Triggered Spell Category Cooldown", i);

                        item.Bonding = db2File.ReadEnum<ItemBonding>("Bonding", TypeCode.Int32);

                        if (db2File.ReadUInt16() > 0)
                            item.Name = db2File.ReadCString("Name", 0);

                        for (var i = 1; i < 4; ++i)
                            if (db2File.ReadUInt16() > 0)
                                db2File.ReadCString("Name", i);

                        if (db2File.ReadUInt16() > 0)
                            item.Description = db2File.ReadCString("Description");

                        item.PageText = db2File.ReadUInt32("Page Text");
                        item.Language = db2File.ReadEnum<Language>("Language", TypeCode.Int32);
                        item.PageMaterial = db2File.ReadEnum<PageMaterial>("Page Material", TypeCode.Int32);
                        item.StartQuestId = (uint)db2File.ReadEntryWithName<Int32>(StoreNameType.Quest, "Start Quest");
                        item.LockId = db2File.ReadUInt32("Lock ID");
                        item.Material = db2File.ReadEnum<Material>("Material", TypeCode.Int32);
                        item.SheathType = db2File.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);
                        item.RandomPropery = db2File.ReadInt32("Random Property");
                        item.RandomSuffix = db2File.ReadUInt32("Random Suffix");
                        item.ItemSet = db2File.ReadUInt32("Item Set");
                        item.AreaId = (uint)db2File.ReadEntryWithName<UInt32>(StoreNameType.Area, "Area");
                        // In this single (?) case, map 0 means no map
                        var map = db2File.ReadInt32();
                        item.MapId = map;
                        db2File.WriteLine("Map ID: " + (map != 0 ? StoreGetters.GetName(StoreNameType.Map, map) : map + " (No map)"));
                        item.BagFamily = db2File.ReadEnum<BagFamilyMask>("Bag Family", TypeCode.Int32);
                        item.TotemCategory = db2File.ReadEnum<TotemCategory>("Totem Category", TypeCode.Int32);

                        item.ItemSocketColors = new ItemSocketColor[3];
                        for (var i = 0; i < 3; i++)
                            item.ItemSocketColors[i] = db2File.ReadEnum<ItemSocketColor>("Socket Color", TypeCode.Int32, i);

                        item.SocketContent = new uint[3];
                        for (var i = 0; i < 3; i++)
                            item.SocketContent[i] = db2File.ReadUInt32("Socket Item", i);

                        item.SocketBonus = db2File.ReadInt32("Socket Bonus");
                        item.GemProperties = db2File.ReadInt32("Gem Properties");
                        item.ArmorDamageModifier = db2File.ReadSingle("Armor Damage Modifier");
                        item.Duration = db2File.ReadUInt32("Duration");
                        item.ItemLimitCategory = db2File.ReadInt32("Limit Category");
                        item.HolidayId = db2File.ReadEnum<Holiday>("Holiday", TypeCode.Int32);
                        item.StatScalingFactor = db2File.ReadSingle("Stat Scaling Factor");
                        item.CurrencySubstitutionId = db2File.ReadUInt32("Currency Substitution Id");
                        item.CurrencySubstitutionCount = db2File.ReadUInt32("Currency Substitution Count");

                        Storage.ObjectNames.Add(entry, new ObjectName { ObjectType = ObjectType.Item, Name = item.Name }, packet.TimeSpan);
                        packet.AddSniffData(StoreNameType.Item, (int)entry, "DB_REPLY");
                        break;
                    }
                case DB2Hash.KeyChain: // KeyChain.db2
                    {
                        db2File.ReadUInt32("Key Chain Id");
                        db2File.WriteLine("Key: {0}", Utilities.ByteArrayToHexString(db2File.ReadBytes(32)));
                        break;
                    }
                case DB2Hash.Creature: // Creature.db2
                    {
                        db2File.ReadUInt32("Npc Entry");
                        db2File.ReadUInt32("Item Entry 1");
                        db2File.ReadUInt32("Item Entry 2");
                        db2File.ReadUInt32("Item Entry 3");
                        db2File.ReadUInt32("Projectile Entry 1");
                        db2File.ReadUInt32("Projectile Entry 2");
                        db2File.ReadUInt32("Mount");
                        db2File.ReadUInt32("Display Id 1");
                        db2File.ReadUInt32("Display Id 2");
                        db2File.ReadUInt32("Display Id 3");
                        db2File.ReadUInt32("Display Id 4");
                        db2File.ReadSingle("Float1");
                        db2File.ReadSingle("Float2");
                        db2File.ReadSingle("Float3");
                        db2File.ReadSingle("Float4");
                        if (db2File.ReadUInt16() > 0)
                            db2File.ReadCString("Name");

                        db2File.ReadUInt32("InhabitType");
                        break;
                    }
                case DB2Hash.BroadcastText:
                    {
                        db2File.ReadUInt32("Broadcast Text Entry");
                        db2File.ReadUInt32("Language");
                        if (db2File.ReadUInt16() > 0)
                            db2File.ReadCString("Male Text");
                        if (db2File.ReadUInt16() > 0)
                            db2File.ReadCString("Female Text");
 
                        for (var i = 0; i < 3; ++i)
                            db2File.ReadInt32("Emote ID", i);
                        for (var i = 0; i < 3; ++i)
                            db2File.ReadInt32("Emote Delay", i);
 
                        db2File.ReadUInt32("Sound Id");
                        db2File.ReadUInt32("Unk0");
                        db2File.ReadUInt32("Unk1"); // kind of type?
                        break;
                    }
                default:
                    {
                        db2File.WriteLine("Unknown DB2 file type: {0} (0x{0:x})", type);
                        for (var i = 0; ; ++i)
                        {
                            if (db2File.Length - 4 >= db2File.Position)
                            {
                                var blockVal = db2File.ReadUpdateField();
                                string key = "Block Value " + i;
                                string value = blockVal.UInt32Value + "/" + blockVal.SingleValue;
                                packet.WriteLine(key + ": " + value);
                            }
                            else
                            {
                                var left = db2File.Length - db2File.Position;
                                for (var j = 0; j < left; ++j)
                                {
                                    string key = "Byte Value " + i;
                                    var value = db2File.ReadByte();
                                    packet.WriteLine(key + ": " + value);
                                }
                                break;
                            }
                        }
                        break;
                    }
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var pageText = new PageText();

            var entry = packet.ReadUInt32("Entry");
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var textLen = (int)packet.ReadBits(12);
            packet.ResetBitReader();
            pageText.Text = packet.ReadWoWString("Page Text", textLen);
            pageText.NextPageId = packet.ReadUInt32("Next Page");
            packet.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(entry, pageText, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUEST_QUERY_RESPONSE)]
        public static void HandleQuestQueryResponse510(Packet packet)
        {
            var hasData = packet.ReadBit();
            if (!hasData)
            {
                packet.ReadUInt32("Entry");
                return; // nothing to do
            }

            var quest = new QuestTemplate();

            var QuestTurnTextWindow = (int)packet.ReadBits(10);
            var Details = (int)packet.ReadBits(12);
            var QuestGiverTextWindow = (int)packet.ReadBits(10);
            var len1658 = (int)packet.ReadBits(9);
            var CompletedText = (int)packet.ReadBits(11);
            var len158 = (int)packet.ReadBits(12);
            var QuestGiverTargetName = (int)packet.ReadBits(8);
            var Title = (int)packet.ReadBits(9);
            var QuestTurnTargetName = (int)packet.ReadBits(8);
            var count = (int)packet.ReadBits("Requirement Count", 19);

            var len2949_20 = new int[count];
            var counter = new int[count];
            for (var i = 0; i < count; ++i)
            {
                len2949_20[i] = (int)packet.ReadBits(8);
                counter[i] = (int)packet.ReadBits(22);
            }
            packet.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.ReadWoWString("string2949+20", len2949_20[i], i);
                packet.ReadInt32("int2949+16", i);
                packet.ReadByte("byte2949+5", i);
                packet.ReadByte("byte2949+4", i);
                packet.ReadInt32("int2949+12", i);

                for (var j = 0; j < counter[i]; ++j)
                    packet.ReadInt32("Unk UInt32", i, j);

                packet.ReadInt32("Unk UInt32", i);
                packet.ReadInt32("int2949+8", i);
            }

            packet.ReadWoWString("string158", len158);
            quest.NextQuestIdChain = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Next Chain Quest");
            packet.ReadInt32("int2971");
            quest.RewardSpellCast = packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Reward Spell Cast");
            packet.ReadInt32("int2955");
            quest.RewardHonorMultiplier = packet.ReadSingle("Reward Honor Multiplier");
            packet.ReadInt32("int2970");
            packet.ReadInt32("int2984");
            packet.ReadInt32("int2979");
            quest.MinLevel = packet.ReadInt32("Min Level");
            quest.RewardSkillPoints = packet.ReadUInt32("RewSkillPoints");
            quest.QuestGiverPortrait = packet.ReadUInt32("QuestGiverPortrait");
            packet.ReadInt32("int21");
            quest.Type = packet.ReadEnum<QuestType>("Type", TypeCode.Int32);
            for (var i = 0; i < 5; ++i)
            {
                packet.ReadInt32("int2986+40");
                packet.ReadInt32("int2986+0");
                packet.ReadInt32("int2986+20");
            }

            packet.ReadInt32("int2960");
            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("int3001+16");
                packet.ReadInt32("int3001+0");
            }
            quest.SuggestedPlayers = packet.ReadUInt32("Suggested Players");
            packet.ReadInt32("int2972");
            packet.ReadInt32("int2959");
            quest.Title = packet.ReadWoWString("Title", Title);
            packet.ReadInt32("int2965");
            packet.ReadInt32("int2978");
            quest.RewardSkillId = packet.ReadUInt32("RewSkillId");
            packet.ReadInt32("int2982");
            packet.ReadInt32("int2968");
            packet.ReadInt32("int2964");
            packet.ReadInt32("int2957");
            packet.ReadInt32("int2969");
            packet.ReadInt32("int1786");
            quest.SoundAccept = packet.ReadUInt32("Sound Accept");
            packet.ReadInt32("int2981");
            packet.ReadInt32("int2961");
            packet.ReadInt32("int15");
            packet.ReadInt32("int2967");
            quest.CompletedText = packet.ReadWoWString("Completed Text", CompletedText);
            packet.ReadInt32("int25");
            packet.ReadInt32("Quest Id");
            quest.PointY = packet.ReadSingle("Point Y");
            packet.ReadInt32("int2974");
            packet.ReadInt32("int2952");
            quest.Details = packet.ReadWoWString("Details", Details);
            quest.Level = packet.ReadInt32("Level");
            quest.PointMapId = packet.ReadUInt32("Point Map ID");
            packet.ReadWoWString("string1658", len1658);
            quest.PointX = packet.ReadSingle("Point X");
            packet.ReadInt32("int17");
            packet.ReadInt32("int2962");
            quest.QuestGiverTextWindow = packet.ReadWoWString("QuestGiver Text Window", QuestGiverTextWindow);
            packet.ReadInt32("int2963");
            packet.ReadInt32("int2985");
            quest.Method = packet.ReadEnum<QuestMethod>("Method", TypeCode.Int32);
            quest.RewardReputationMask = packet.ReadUInt32("RewRepMask");
            packet.ReadInt32("int2953");
            packet.ReadInt32("int2983");
            packet.ReadInt32("int9");
            quest.QuestGiverTargetName = packet.ReadWoWString("QuestGiver Target Name", QuestGiverTargetName);
            quest.ZoneOrSort = packet.ReadEnum<QuestSort>("Sort", TypeCode.Int32);
            packet.ReadInt32("int1788");
            quest.SoundTurnIn = packet.ReadUInt32("Sound TurnIn");
            quest.SourceItemId = (uint) packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Source Item ID");
            quest.QuestTurnTargetName = packet.ReadWoWString("QuestTurn Target Name", QuestTurnTargetName);
            quest.QuestTurnInPortrait = packet.ReadUInt32("QuestTurnInPortrait");
            quest.Flags = packet.ReadEnum<QuestFlags>("Flags", TypeCode.UInt32);
            packet.ReadInt32("int2954");
            packet.ReadInt32("int2958");
            quest.RewardMoneyMaxLevel = packet.ReadUInt32("Reward Money Max Level");
            packet.ReadInt32("int1787");
            quest.QuestTurnTextWindow = packet.ReadWoWString("QuestTurn Text Window", QuestTurnTextWindow);
            packet.ReadInt32("int2977");
            packet.ReadInt32("int2980");
            packet.ReadInt32("int2975");
            quest.RewardSpell = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Reward Spell");
            quest.RewardOrRequiredMoney = packet.ReadInt32("Reward Money");
            packet.ReadInt32("int2973");
            packet.ReadInt32("int2966");
            packet.ReadInt32("int2976");
            quest.PointOption = packet.ReadUInt32("Point Opt");
            packet.ReadInt32("int2956");

            var id = packet.ReadEntry("Quest ID");

            packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");
            Storage.QuestTemplates.Add((uint)id.Key, quest, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcText();

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            npcText.Probabilities = new float[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                pkt.ReadInt32("Broadcast Text Id", i);
        }
    }
}
