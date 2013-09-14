using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_0_17359.Enums;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_CREATURE_QUERY)]
        public static void HandleCreatureQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.ReadEnum<DB2Hash>("DB2 File", TypeCode.Int32);
            var count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 3, 4, 7, 2, 5, 1, 6, 0);
            }

            packet.ResetBitReader();
            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORBytes(guids[i], 6, 1, 2);
                packet.ReadInt32("Entry", i);
                packet.ReadXORBytes(guids[i], 4, 5, 7, 0, 3);
                packet.WriteGuid("Guid", guids[i], i);
            }
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

            var stringLens = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }

            var qItemCount = packet.ReadBits(22);
            var lenS4 = (int)packet.ReadBits(6);
            var lenS3 = (int)packet.ReadBits(11);
            var lenS5 = (int)packet.ReadBits(11);

            creature.RacialLeader = packet.ReadBit("Racial Leader");

            packet.ResetBitReader();

            creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32);
            creature.KillCredits = new uint[2];
            creature.KillCredits[1] = packet.ReadUInt32("Kill Credit 1");
            creature.DisplayIds = new uint[4];
            creature.DisplayIds[3] = packet.ReadUInt32("Display ID 3");
            creature.DisplayIds[2] = packet.ReadUInt32("Display ID 2");

            creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; ++i)
                creature.QuestItems[i] = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Quest Item", i);

            creature.Expansion = packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);

            var name = new string[8];
            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    name[i] = packet.ReadCString("Name", i);
                if (stringLens[i][1] > 1)
                    packet.ReadCString("Female Name", i);
            }
            creature.Name = name[0];

            if (lenS5 > 1)
                packet.ReadCString("string5");

            creature.Modifier2 = packet.ReadSingle("Modifier 2");
            creature.DisplayIds[0] = packet.ReadUInt32("Display ID 0");

            if (lenS4 > 1)
                creature.IconName = packet.ReadCString("Icon Name");

            creature.KillCredits[0] = packet.ReadUInt32("Kill Credit 0");
            creature.DisplayIds[1] = packet.ReadUInt32("Display ID 1");

            if (lenS3 > 1)
                creature.SubName = packet.ReadCString("Sub Name");

            creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.Modifier1 = packet.ReadSingle("Modifier 1");
            creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32);
            creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32);
            creature.MovementId = packet.ReadUInt32("Movement ID");

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            var type = packet.ReadEnum<DB2Hash>("DB2 File", TypeCode.UInt32);
            packet.ReadTime("Hotfix date");
            var entry = (uint)packet.ReadInt32("Entry");
            if ((int)entry < 0)
            {
                packet.WriteLine("Row {0} has been removed.", -(int)entry);
                return;
            }

            switch (type)
            {
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
                        db2File.ReadUInt32("Unk0"); // emote unk
                        db2File.ReadUInt32("Unk1"); // kind of type?
                        break;
                    }
                case DB2Hash.Creature: // New structure - 5.4
                    {
                        db2File.ReadUInt32("Creature Entry");
                        db2File.ReadUInt32("Item Entry 1");
                        db2File.ReadUInt32("Item Entry 2");
                        db2File.ReadUInt32("Item Entry 3");
                        db2File.ReadUInt32("Mount");
                        db2File.ReadUInt32("Display Id 1");
                        db2File.ReadUInt32("Display Id 2");
                        db2File.ReadUInt32("Display Id 3");
                        db2File.ReadUInt32("Display Id 4");
                        db2File.ReadSingle("Display Id Probability 1");
                        db2File.ReadSingle("Display Id Probability 2");
                        db2File.ReadSingle("Display Id Probability 3");
                        db2File.ReadSingle("Display Id Probability 4");
                        if (db2File.ReadUInt16() > 0)
                            db2File.ReadCString("Name");

                        if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("Sub Name");

                        if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("Unk String");

                        db2File.ReadUInt32("Rank");
                        db2File.ReadUInt32("Inhabit Type");
                        break;
                    }
                case DB2Hash.CreatureDifficulty:
                    {
                        db2File.ReadUInt32("Id");
                        db2File.ReadUInt32("Creature Entry");
                        db2File.ReadUInt32("Faction Template Id");
                        db2File.ReadUInt32("Expansion HP");
                        db2File.ReadUInt32("Min Level");
                        db2File.ReadUInt32("Max Level");
                        db2File.ReadUInt32("Unk 1");
                        db2File.ReadUInt32("Unk 2");
                        db2File.ReadUInt32("Unk 3");
                        db2File.ReadUInt32("Unk 4");
                        db2File.ReadUInt32("Unk 5");
                        break;
                    }					
                case DB2Hash.GameObjects:
                    {
                        db2File.ReadUInt32("Gameobject Entry");
                        db2File.ReadUInt32("Map");
                        db2File.ReadUInt32("Display Id");
                        db2File.ReadSingle("Position X");
                        db2File.ReadSingle("Position Y");
                        db2File.ReadSingle("Position Z");
                        db2File.ReadSingle("Rotation 1");
                        db2File.ReadSingle("Rotation 2");
                        db2File.ReadSingle("Rotation 3");
                        db2File.ReadSingle("Rotation 4");
                        db2File.ReadSingle("Size");
                        db2File.ReadUInt32("Type");
                        db2File.ReadUInt32("Data 0");
                        db2File.ReadUInt32("Data 1");
                        db2File.ReadUInt32("Data 2");
                        db2File.ReadUInt32("Data 3");

                        if (db2File.ReadUInt16() > 0)
                            db2File.ReadCString("Name");
                        break;
                    }
                case DB2Hash.Item:
                    {
                        var item = Storage.ItemTemplates.ContainsKey(entry) ? Storage.ItemTemplates[entry].Item1 : new ItemTemplate();

                        db2File.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Entry");
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
                case DB2Hash.Item_sparse:
                    {
                        var item = Storage.ItemTemplates.ContainsKey(entry) ? Storage.ItemTemplates[entry].Item1 : new ItemTemplate();

                        db2File.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Sparse Entry");
                        item.Quality = db2File.ReadEnum<ItemQuality>("Quality", TypeCode.Int32);
                        item.Flags1 = db2File.ReadEnum<ItemProtoFlags>("Flags 1", TypeCode.UInt32);
                        item.Flags2 = db2File.ReadEnum<ItemFlagExtra>("Flags 2", TypeCode.Int32);
                        item.Flags3 = db2File.ReadUInt32("Flags 3");
                        item.Unk430_1 = db2File.ReadSingle("Unk430_1");
                        item.Unk430_2 = db2File.ReadSingle("Unk430_2");
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
                case DB2Hash.KeyChain:
                    {
                        db2File.ReadUInt32("Key Chain Id");
                        db2File.WriteLine("Key: {0}", Utilities.ByteArrayToHexString(db2File.ReadBytes(32)));
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

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.ReadByte("Unk byte");
            packet.ReadInt32("Realm Id");

            var bits278 = packet.ReadBits(8);
            packet.ReadBit();
            var bits22 = packet.ReadBits(8);

            packet.ReadWoWString("Realmname", bits22);
            packet.ReadWoWString("Realmname (without white char)", bits278);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var pageText = new PageText();

            var hasData = packet.ReadBit();
            if (!hasData)
            {
                packet.ReadUInt32("Entry");
                return; // nothing to do
            }

            var textLen = packet.ReadBits(12);

            packet.ResetBitReader();
            pageText.Text = packet.ReadWoWString("Page Text", textLen);

            var entry = packet.ReadUInt32("Entry");
            pageText.NextPageId = packet.ReadUInt32("Next Page");
            packet.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(entry, pageText, packet.TimeSpan);
        }
    }
}
