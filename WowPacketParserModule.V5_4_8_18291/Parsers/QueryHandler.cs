using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_CREATURE_QUERY)]
        public static void HandleCreatureQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry"); // +5

            var creature = new UnitTemplate();
            var hasData = packet.ReadBit(); //+16
            if (!hasData)
                return; // nothing to do

            creature.DisplayIds = new uint[4];
            creature.KillCredits = new uint[2];

            var bits24 = packet.ReadBits(11); //+7
            var qItemCount = packet.ReadBits(22); //+72
            var bits1C = (int)packet.ReadBits(11); //+9

            var stringLens = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }

            creature.RacialLeader = packet.ReadBit("Racial Leader"); //+68
            var bits2C = packet.ReadBits(6); //+136

            if (bits1C > 1)
                packet.ReadCString("String1C");

            creature.KillCredits[0] = packet.ReadUInt32(); //+27
            creature.DisplayIds[3] = packet.ReadUInt32(); //+32
            creature.DisplayIds[2] = packet.ReadUInt32(); //+31
            creature.Expansion = packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32); //+24
            creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32); //+12
            creature.Modifier1 = packet.ReadSingle("Modifier 1"); //+15

            //for (var i = 0; i < 2; ++i)
            //{
                creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);
                creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum
            //}

            creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32); //+14
            creature.MovementId = packet.ReadUInt32("Movement ID"); //+23

            var name = new string[4];
            var femaleName = new string[4];
            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][1] > 1)
                    femaleName[i] = packet.ReadCString("Female Name", i);
                if (stringLens[i][0] > 1)
                    name[i] = packet.ReadCString("Name", i);
            }
            creature.Name = name[0];
            creature.femaleName = femaleName[0];

            if (bits24 > 1)
                creature.SubName = packet.ReadCString("Sub Name");

            creature.DisplayIds[0] = packet.ReadUInt32(); //+29
            creature.DisplayIds[1] = packet.ReadUInt32(); //+30

            if (bits2C > 1)
                creature.IconName = packet.ReadCString("Icon Name"); //+100

            creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; ++i)
                creature.QuestItems[i] = (uint)packet.ReadEntry<Int32>(StoreNameType.Item, "Quest Item", i); //+72

            creature.KillCredits[1] = packet.ReadUInt32(); //+28
            creature.Modifier2 = packet.ReadSingle("Modifier 2"); //+16
            creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32); //+13

            for (var i = 0; i < 4; ++i)
                packet.AddValue("Display ID", creature.DisplayIds[i], i);
            for (var i = 0; i < 2; ++i)
                packet.AddValue("Kill Credit", creature.KillCredits[i], i);

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
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
                packet.StartBitStream(guids[i], 6, 3, 0, 1, 4, 5, 7, 2);
            }

            packet.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 1);

                packet.ReadInt32("Entry", i);

                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 5);
                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 7);
                packet.ReadXORByte(guids[i], 2);
                packet.ReadXORByte(guids[i], 3);

                packet.WriteGuid("Guid", guids[i], i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var entry = (uint)packet.ReadInt32("Entry");
            packet.ReadTime("Hotfix date");
            var type = packet.ReadEnum<DB2Hash>("DB2 File", TypeCode.UInt32);

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            if ((int)entry < 0)
            {
                packet.WriteLine("Row {0} has been removed.", -(int)entry);
                return;
            }

            switch (type)
            {
                case DB2Hash.BroadcastText:
                {
                    var broadcastText = new BroadcastText();

                    var Id = db2File.ReadEntry("Id");
                    broadcastText.language = db2File.ReadUInt32("Language");
                    if (db2File.ReadUInt16() > 0)
                        broadcastText.MaleText = db2File.ReadCString("Male Text");
                    if (db2File.ReadUInt16() > 0)
                        broadcastText.FemaleText = db2File.ReadCString("Female Text");

                    broadcastText.EmoteID = new uint[3];
                    broadcastText.EmoteDelay = new uint[3];
                    for (var i = 0; i < 3; ++i)
                        broadcastText.EmoteID[i] = (uint) db2File.ReadInt32("Emote ID", i);
                    for (var i = 0; i < 3; ++i)
                        broadcastText.EmoteDelay[i] = (uint) db2File.ReadInt32("Emote Delay", i);

                    broadcastText.soundId = db2File.ReadUInt32("Sound Id");
                    broadcastText.unk1 = db2File.ReadUInt32("Unk MoP 1"); // unk emote
                    broadcastText.unk2 = db2File.ReadUInt32("Unk MoP 2"); // kind of type?

                    Storage.BroadcastTexts.Add((uint) Id.Key, broadcastText, packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.BroadcastText, Id.Key, "BROADCAST_TEXT");
                    break;
                }
                case DB2Hash.Creature: // New structure - 5.4.0
                {
                    db2File.ReadUInt32("Creature Id");
                    db2File.ReadUInt32("Item Id 1");
                    db2File.ReadUInt32("Item Id 2");
                    db2File.ReadUInt32("Item Id 3");
                    db2File.ReadUInt32("Mount");
                    for (var i = 0; i < 4; ++i)
                        db2File.ReadInt32("Display Id", i);

                    for (var i = 0; i < 4; ++i)
                        db2File.ReadSingle("Display Id Probability", i);

                    if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("Name");

                    if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("SubName");

                    if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("Female SubName");

                    db2File.ReadUInt32("Rank");
                    db2File.ReadUInt32("Inhabit Type");
                    break;
                }
                case DB2Hash.CreatureDifficulty:
                {
                    var creatureDifficulty = new CreatureDifficulty();

                    db2File.ReadUInt32("Id");
                    var Id = db2File.ReadEntry("Creature Id");
                    creatureDifficulty.Faction = db2File.ReadUInt32("Faction Template Id");
                    creatureDifficulty.Expansion = db2File.ReadInt32("Expansion");
                    creatureDifficulty.MinLevel = db2File.ReadInt32("Min Level");
                    creatureDifficulty.MaxLevel = db2File.ReadInt32("Max Level");
                    db2File.ReadUInt32("Unk MoP 1");
                    db2File.ReadUInt32("Unk MoP 2");
                    db2File.ReadUInt32("Unk MoP 3");
                    db2File.ReadUInt32("Unk MoP 4");
                    db2File.ReadUInt32("Unk MoP 5");

                    Storage.CreatureDifficultys.Add((uint) Id.Key, creatureDifficulty, packet.TimeSpan);
                    break;
                }
                case DB2Hash.GameObjects:
                {
                    var gameObjectTemplateDB2 = new GameObjectTemplateDB2();
                    var gameObjectTemplatePositionDB2 = new GameObjectTemplatePositionDB2();

                    var Id = db2File.ReadEntry("GameObject Id");

                    gameObjectTemplatePositionDB2.map = db2File.ReadUInt32("Map");

                    gameObjectTemplateDB2.DisplayId = db2File.ReadUInt32("Display Id");

                    gameObjectTemplatePositionDB2.positionX = db2File.ReadSingle("Position X");
                    gameObjectTemplatePositionDB2.positionY = db2File.ReadSingle("Position Y");
                    gameObjectTemplatePositionDB2.positionZ = db2File.ReadSingle("Position Z");
                    gameObjectTemplatePositionDB2.rotationX = db2File.ReadSingle("Rotation X");
                    gameObjectTemplatePositionDB2.rotationY = db2File.ReadSingle("Rotation Y");
                    gameObjectTemplatePositionDB2.rotationZ = db2File.ReadSingle("Rotation Z");
                    gameObjectTemplatePositionDB2.rotationW = db2File.ReadSingle("Rotation W");

                    gameObjectTemplateDB2.Size = db2File.ReadSingle("Size");
                    gameObjectTemplateDB2.Type = db2File.ReadEnum<GameObjectType>("Type", TypeCode.Int32);

                    gameObjectTemplateDB2.Data = new int[4];
                    for (var i = 0; i < gameObjectTemplateDB2.Data.Length; i++)
                        gameObjectTemplateDB2.Data[i] = db2File.ReadInt32("Data", i);

                    if (db2File.ReadUInt16() > 0)
                        gameObjectTemplateDB2.Name = db2File.ReadCString("Name");

                    Storage.GameObjectTemplateDB2s.Add((uint) Id.Key, gameObjectTemplateDB2, packet.TimeSpan);
                    if (gameObjectTemplatePositionDB2.positionX != 0.0f &&
                        gameObjectTemplatePositionDB2.positionY != 0.0f &&
                        gameObjectTemplatePositionDB2.positionZ != 0.0f)
                        Storage.GameObjectTemplatePositionDB2s.Add((uint) Id.Key, gameObjectTemplatePositionDB2,
                            packet.TimeSpan);
                    break;
                }
                case DB2Hash.Item:
                {
                    var item = Storage.ItemTemplates.ContainsKey(entry)
                        ? Storage.ItemTemplates[entry].Item1
                        : new ItemTemplate();

                    db2File.ReadEntry<UInt32>(StoreNameType.Item, "Item Entry");
                    item.Class = db2File.ReadEnum<ItemClass>("Class", TypeCode.Int32);
                    item.SubClass = db2File.ReadUInt32("Sub Class");
                    item.SoundOverrideSubclass = db2File.ReadInt32("Sound Override Subclass");
                    item.Material = db2File.ReadEnum<Material>("Material", TypeCode.Int32);
                    item.DisplayId = db2File.ReadUInt32("Display ID");
                    item.InventoryType = db2File.ReadEnum<InventoryType>("Inventory Type", TypeCode.UInt32);
                    item.SheathType = db2File.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);

                    Storage.ItemTemplates.Add(entry, item, packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.Item, (int) entry, "DB_REPLY");
                    break;
                }
                case DB2Hash.ItemExtendedCost:
                {
                    db2File.ReadUInt32("Item Extended Cost Entry");
                    db2File.ReadUInt32("Required Honor Points");
                    db2File.ReadUInt32("Required Arena Points");
                    db2File.ReadUInt32("Required Arena Slot");
                    for (var i = 0; i < 5; ++i)
                        db2File.ReadUInt32("Required Item", i);

                    for (var i = 0; i < 5; ++i)
                        db2File.ReadUInt32("Required Item Count", i);

                    db2File.ReadUInt32("Required Personal Arena Rating");
                    db2File.ReadUInt32("Item Purchase Group");
                    for (var i = 0; i < 5; ++i)
                        db2File.ReadUInt32("Required Currency", i);

                    for (var i = 0; i < 5; ++i)
                        db2File.ReadUInt32("Required Currency Count", i);

                    db2File.ReadUInt32("Required Faction Id");
                    db2File.ReadUInt32("Required Faction Standing");
                    db2File.ReadUInt32("Requirement Flags");
                    db2File.ReadUInt32("Required Guild Level");
                    db2File.ReadEntry<Int32>(StoreNameType.Achievement, "Required Achievement");
                    break;
                }
                case DB2Hash.ItemCurrencyCost:
                {
                    db2File.ReadUInt32("Id");
                    db2File.ReadEntry<UInt32>(StoreNameType.Item, "Item Entry");
                    break;
                }
                case DB2Hash.RulesetItemUpgrade:
                {
                    db2File.ReadUInt32("Id");
                    db2File.ReadUInt32("Item Upgrade Level");
                    db2File.ReadUInt32("Item Upgrade Id");
                    db2File.ReadEntry<UInt32>(StoreNameType.Item, "Item Entry");
                    break;
                }
                case DB2Hash.Item_sparse:
                {
                    var item = Storage.ItemTemplates.ContainsKey(entry)
                        ? Storage.ItemTemplates[entry].Item1
                        : new ItemTemplate();

                    db2File.ReadEntry<UInt32>(StoreNameType.Item, "Item Sparse Entry");
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
                    item.RequiredSpell = (uint) db2File.ReadEntry<Int32>(StoreNameType.Spell, "Required Spell");
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
                        item.TriggeredSpellIds[i] = db2File.ReadEntry<Int32>(StoreNameType.Spell,
                            "Triggered Spell ID", i);

                    item.TriggeredSpellTypes = new ItemSpellTriggerType[5];
                    for (var i = 0; i < 5; i++)
                        item.TriggeredSpellTypes[i] = db2File.ReadEnum<ItemSpellTriggerType>("Trigger Spell Type",
                            TypeCode.Int32, i);

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
                        item.TriggeredSpellCategoryCooldowns[i] = db2File.ReadInt32(
                            "Triggered Spell Category Cooldown", i);

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
                    item.StartQuestId = (uint) db2File.ReadEntry<Int32>(StoreNameType.Quest, "Start Quest");
                    item.LockId = db2File.ReadUInt32("Lock ID");
                    item.Material = db2File.ReadEnum<Material>("Material", TypeCode.Int32);
                    item.SheathType = db2File.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);
                    item.RandomPropery = db2File.ReadInt32("Random Property");
                    item.RandomSuffix = db2File.ReadUInt32("Random Suffix");
                    item.ItemSet = db2File.ReadUInt32("Item Set");
                    item.AreaId = db2File.ReadEntry<UInt32>(StoreNameType.Area, "Area");
                    item.MapId = db2File.ReadEntry<Int32>(StoreNameType.Map, "Map ID");
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

                    Storage.ObjectNames.Add(entry, new ObjectName {ObjectType = ObjectType.Item, Name = item.Name},
                        packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.Item, (int) entry, "DB_REPLY");
                    break;
                }
                case DB2Hash.KeyChain:
                {
                    db2File.ReadUInt32("Key Chain Id");
                    db2File.ReadBytes("Key", 32);
                    break;
                }
                case DB2Hash.SceneScript: // lua ftw!
                {
                    db2File.ReadUInt32("Scene Script Id");
                    if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("Name");

                    if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("Script");
                    db2File.ReadUInt32("Previous Scene Script Part");
                    db2File.ReadUInt32("Next Scene Script Part");
                    break;
                }
                case DB2Hash.Vignette:
                {
                    db2File.ReadUInt32("Vignette Entry");
                    if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("Name");

                    db2File.ReadUInt32("Icon");
                    db2File.ReadUInt32("Flag"); // not 100% sure (8 & 32 as values only) - todo verify with more data
                    db2File.ReadSingle("Unk Float 1");
                    db2File.ReadSingle("Unk Float 2");
                    break;
                }
                case DB2Hash.WbAccessControlList:
                {
                    db2File.ReadUInt32("Id");

                    if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("Address");

                    db2File.ReadUInt32("Unk MoP 1");
                    db2File.ReadUInt32("Unk MoP 2");
                    db2File.ReadUInt32("Unk MoP 3");
                    db2File.ReadUInt32("Unk MoP 4"); // flags?
                    break;
                }
                default:
                {
                    db2File.AddValue("Unknown DB2 file type", string.Format("{0} (0x{0:x})", type));
                    for (var i = 0;; ++i)
                    {
                        if (db2File.Length - 4 >= db2File.Position)
                        {
                            var blockVal = db2File.ReadUpdateField();
                            string key = "Block Value " + i;
                            string value = blockVal.UInt32Value + "/" + blockVal.SingleValue;
                            packet.AddValue(key, value);
                        }
                        else
                        {
                            var left = db2File.Length - db2File.Position;
                            for (var j = 0; j < left; ++j)
                            {
                                string key = "Byte Value " + i;
                                var value = db2File.ReadByte();
                                packet.AddValue(key, value);
                            }
                            break;
                        }
                    }
                    break;
                }
            }

            db2File.ClosePacket();
        }

        [Parser(Opcode.SMSG_NAME_QUERY_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var guid1 = new byte[8];

            var bit18 = false;

            var nameLen = 0;

            guid1[3] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[1] = packet.ReadBit();

            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 2);

            var hasData = packet.ReadByte("HasData");
            if (hasData == 0)
            {
                packet.ReadInt32("Realm Id");
                packet.ReadInt32("AccountId");
                packet.ReadEnum<Class>("Class", TypeCode.Byte);
                packet.ReadEnum<Race>("Race", TypeCode.Byte);
                packet.ReadByte("Level");
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
            }

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 3);

            if (hasData == 0)
            {
                guid4[2] = packet.ReadBit();
                guid4[7] = packet.ReadBit();
                guid5[7] = packet.ReadBit();
                guid5[2] = packet.ReadBit();
                guid5[0] = packet.ReadBit();
                bit18 = packet.ReadBit();

                guid4[4] = packet.ReadBit();
                guid5[5] = packet.ReadBit();
                guid4[1] = packet.ReadBit();
                guid4[3] = packet.ReadBit();
                guid4[0] = packet.ReadBit();

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                guid5[6] = packet.ReadBit();
                guid5[3] = packet.ReadBit();
                guid4[5] = packet.ReadBit();
                guid5[1] = packet.ReadBit();
                guid5[4] = packet.ReadBit();

                nameLen = (int)packet.ReadBits(6);

                guid4[6] = packet.ReadBit();

                packet.ReadXORByte(guid5, 6);
                packet.ReadXORByte(guid5, 0);
                packet.ReadWoWString("Name", nameLen);
                packet.ReadXORByte(guid4, 5);
                packet.ReadXORByte(guid4, 2);
                packet.ReadXORByte(guid5, 3);
                packet.ReadXORByte(guid4, 4);
                packet.ReadXORByte(guid4, 3);
                packet.ReadXORByte(guid5, 4);
                packet.ReadXORByte(guid5, 2);
                packet.ReadXORByte(guid4, 7);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);


                packet.ReadXORByte(guid4, 6);
                packet.ReadXORByte(guid5, 7);
                packet.ReadXORByte(guid5, 1);
                packet.ReadXORByte(guid4, 1);
                packet.ReadXORByte(guid5, 5);
                packet.ReadXORByte(guid4, 0);

                packet.WriteGuid("Guid4", guid4);
                packet.WriteGuid("Guid5", guid5);
            }

            packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.ReadBit();
            var hasRealmId2 = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasRealmId1 = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ParseBitStream(guid, 7, 5, 1, 2, 6, 3, 0, 4);

            if (hasRealmId2)
                packet.ReadInt32("Realm Id 2");

            if (hasRealmId1)
                packet.ReadInt32("Realm Id 1");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PAGE_TEXT_QUERY)]
        public static void HandlePageTextQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Entry");

            packet.StartBitStream(guid, 2, 1, 3, 7, 6, 4, 0, 5);
            packet.ParseBitStream(guid, 0, 6, 3, 5, 1, 7, 4, 2);

            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var pageText = new PageText();

            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var textLen = packet.ReadBits(12);

            pageText.NextPageId = packet.ReadUInt32("Next Page");
            packet.ReadUInt32("Entry");

            pageText.Text = packet.ReadWoWString("Page Text", textLen);

            var entry = packet.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(entry, pageText, packet.TimeSpan);
        }
    }
}
