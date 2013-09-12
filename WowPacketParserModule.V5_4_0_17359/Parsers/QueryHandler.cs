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
    }
}
