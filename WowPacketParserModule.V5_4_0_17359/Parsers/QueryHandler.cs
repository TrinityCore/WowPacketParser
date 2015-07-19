using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_QUERY_CREATURE)]
        public static void HandleCreatureQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");
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
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
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

            creature.Type = packet.ReadInt32E<CreatureType>("Type");
            creature.KillCredits = new uint[2];
            creature.KillCredits[1] = packet.ReadUInt32("Kill Credit 1");
            creature.DisplayIds = new uint[4];
            creature.DisplayIds[3] = packet.ReadUInt32("Display ID 3");
            creature.DisplayIds[2] = packet.ReadUInt32("Display ID 2");

            creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; ++i)
                creature.QuestItems[i] = (uint)packet.ReadInt32<ItemId>("Quest Item", i);

            creature.Expansion = packet.ReadUInt32E<ClientType>("Expansion");

            var name = new string[8];
            var femaleName = new string[4];
            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    name[i] = packet.ReadCString("Name", i);
                if (stringLens[i][1] > 1)
                    femaleName[i] = packet.ReadCString("Female Name", i);
            }
            creature.Name = name[0];
            creature.FemaleName = femaleName[0];

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

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.Modifier1 = packet.ReadSingle("Modifier 1");
            creature.Family = packet.ReadInt32E<CreatureFamily>("Family");
            creature.Rank = packet.ReadInt32E<CreatureRank>("Rank");
            creature.MovementId = packet.ReadUInt32("Movement ID");

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name
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

            var type = packet.ReadUInt32E<DB2Hash>("DB2 File");
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
                    var broadcastText = new BroadcastText();

                    var Id = db2File.ReadEntry("Id");
                    broadcastText.Language = db2File.ReadInt32("Language");
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

                    broadcastText.SoundId = db2File.ReadUInt32("Sound Id");
                    broadcastText.UnkEmoteId = db2File.ReadUInt32("Unk MoP 1"); // unk emote
                    broadcastText.Type = db2File.ReadUInt32("Unk MoP 2"); // kind of type?

                    Storage.BroadcastTexts.Add((uint) Id.Key, broadcastText, packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.None, Id.Key, "BROADCAST_TEXT");
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

                    var Id = db2File.ReadEntry("Id");
                    creatureDifficulty.CreatureID = db2File.ReadUInt32("Creature Id");
                    creatureDifficulty.FactionID = db2File.ReadUInt32("Faction Template Id");
                    creatureDifficulty.Expansion = db2File.ReadInt32("Expansion");
                    creatureDifficulty.MinLevel = db2File.ReadInt32("Min Level");
                    creatureDifficulty.MaxLevel = db2File.ReadInt32("Max Level");

                    creatureDifficulty.Flags = new uint[5];
                    for (var i = 0; i < 5; ++i)
                        creatureDifficulty.Flags[i] = db2File.ReadUInt32("Flags", i);

                    Storage.CreatureDifficultys.Add((uint)Id.Key, creatureDifficulty, packet.TimeSpan);
                    break;
                }
                case DB2Hash.GameObjects:
                {
                    var gameObject = new GameObjects();

                    var Id = db2File.ReadEntry("GameObject Id");

                    gameObject.MapID = db2File.ReadUInt32("Map");

                    gameObject.DisplayId = db2File.ReadUInt32("Display Id");

                    gameObject.PositionX = db2File.ReadSingle("Position X");
                    gameObject.PositionY = db2File.ReadSingle("Position Y");
                    gameObject.PositionZ = db2File.ReadSingle("Position Z");
                    gameObject.RotationX = db2File.ReadSingle("Rotation X");
                    gameObject.RotationY = db2File.ReadSingle("Rotation Y");
                    gameObject.RotationZ = db2File.ReadSingle("Rotation Z");
                    gameObject.RotationW = db2File.ReadSingle("Rotation W");

                    gameObject.Size = db2File.ReadSingle("Size");
                    gameObject.Type = db2File.ReadInt32E<GameObjectType>("Type");

                    gameObject.Data = new int[4];
                    for (var i = 0; i < gameObject.Data.Length; i++)
                        gameObject.Data[i] = db2File.ReadInt32("Data", i);

                    if (db2File.ReadUInt16() > 0)
                        gameObject.Name = db2File.ReadCString("Name");

                    Storage.GameObjects.Add((uint)Id.Key, gameObject, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Item:
                {
                    var item = Storage.ItemTemplates.ContainsKey(entry)
                        ? Storage.ItemTemplates[entry].Item1
                        : new ItemTemplate();

                    db2File.ReadUInt32<ItemId>("Item Entry");
                    item.Class = db2File.ReadInt32E<ItemClass>("Class");
                    item.SubClass = db2File.ReadUInt32("Sub Class");
                    item.SoundOverrideSubclass = db2File.ReadInt32("Sound Override Subclass");
                    item.Material = db2File.ReadInt32E<Material>("Material");
                    item.DisplayId = db2File.ReadUInt32("Display ID");
                    item.InventoryType = db2File.ReadUInt32E<InventoryType>("Inventory Type");
                    item.SheathType = db2File.ReadInt32E<SheathType>("Sheath Type");

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
                    db2File.ReadUInt32("Required Achievement");
                    break;
                }
                case DB2Hash.Item_sparse:
                {
                    var item = Storage.ItemTemplates.ContainsKey(entry)
                        ? Storage.ItemTemplates[entry].Item1
                        : new ItemTemplate();

                    db2File.ReadUInt32<ItemId>("Item Sparse Entry");
                    item.Quality = db2File.ReadInt32E<ItemQuality>("Quality");
                    item.Flags1 = db2File.ReadUInt32E<ItemProtoFlags>("Flags 1");
                    item.Flags2 = db2File.ReadInt32E<ItemFlagExtra>("Flags 2");
                    item.Flags3 = db2File.ReadUInt32("Flags 3");
                    item.Unk430_1 = db2File.ReadSingle("Unk430_1");
                    item.Unk430_2 = db2File.ReadSingle("Unk430_2");
                    item.BuyCount = db2File.ReadUInt32("Buy count");
                    item.BuyPrice = db2File.ReadUInt32("Buy Price");
                    item.SellPrice = db2File.ReadUInt32("Sell Price");
                    item.InventoryType = db2File.ReadInt32E<InventoryType>("Inventory Type");
                    item.AllowedClasses = db2File.ReadInt32E<ClassMask>("Allowed Classes");
                    item.AllowedRaces = db2File.ReadInt32E<RaceMask>("Allowed Races");
                    item.ItemLevel = db2File.ReadUInt32("Item Level");
                    item.RequiredLevel = db2File.ReadUInt32("Required Level");
                    item.RequiredSkillId = db2File.ReadUInt32("Required Skill ID");
                    item.RequiredSkillLevel = db2File.ReadUInt32("Required Skill Level");
                    item.RequiredSpell = (uint) db2File.ReadInt32<SpellId>("Required Spell");
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
                        var statType = db2File.ReadInt32E<ItemModType>("Stat Type", i);
                        item.StatTypes[i] = statType == ItemModType.None ? ItemModType.Mana : statType; // TDB
                    }

                    item.StatValues = new int[10];
                    for (var i = 0; i < 10; i++)
                        item.StatValues[i] = db2File.ReadInt32("Stat Value", i);

                    item.ScalingValue = new int[10];
                    for (var i = 0; i < 10; i++)
                        item.ScalingValue[i] = db2File.ReadInt32("Scaling Value", i);

                    item.SocketCostRate = new int[10];
                    for (var i = 0; i < 10; i++)
                        item.SocketCostRate[i] = db2File.ReadInt32("Socket Cost Rate", i);

                    item.ScalingStatDistribution = db2File.ReadInt32("Scaling Stat Distribution");
                    item.DamageType = db2File.ReadInt32E<DamageType>("Damage Type");
                    item.Delay = db2File.ReadUInt32("Delay");
                    item.RangedMod = db2File.ReadSingle("Ranged Mod");

                    item.TriggeredSpellIds = new int[5];
                    for (var i = 0; i < 5; i++)
                        item.TriggeredSpellIds[i] = db2File.ReadInt32<SpellId>("Triggered Spell ID", i);

                    item.TriggeredSpellTypes = new ItemSpellTriggerType[5];
                    for (var i = 0; i < 5; i++)
                        item.TriggeredSpellTypes[i] = db2File.ReadInt32E<ItemSpellTriggerType>("Trigger Spell Type", i);

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

                    item.Bonding = db2File.ReadInt32E<ItemBonding>("Bonding");

                    if (db2File.ReadUInt16() > 0)
                        item.Name = db2File.ReadCString("Name", 0);

                    for (var i = 1; i < 4; ++i)
                        if (db2File.ReadUInt16() > 0)
                            db2File.ReadCString("Name", i);

                    if (db2File.ReadUInt16() > 0)
                        item.Description = db2File.ReadCString("Description");

                    item.PageText = db2File.ReadUInt32("Page Text");
                    item.Language = db2File.ReadInt32E<Language>("Language");
                    item.PageMaterial = db2File.ReadInt32E<PageMaterial>("Page Material");
                    item.StartQuestId = (uint) db2File.ReadInt32<QuestId>("Start Quest");
                    item.LockId = db2File.ReadUInt32("Lock ID");
                    item.Material = db2File.ReadInt32E<Material>("Material");
                    item.SheathType = db2File.ReadInt32E<SheathType>("Sheath Type");
                    item.RandomPropery = db2File.ReadInt32("Random Property");
                    item.RandomSuffix = db2File.ReadUInt32("Random Suffix");
                    item.ItemSet = db2File.ReadUInt32("Item Set");
                    item.AreaId = db2File.ReadUInt32<AreaId>("Area");
                    item.MapId = db2File.ReadInt32<MapId>("Map ID");
                    item.BagFamily = db2File.ReadInt32E<BagFamilyMask>("Bag Family");
                    item.TotemCategory = db2File.ReadInt32E<TotemCategory>("Totem Category");

                    item.ItemSocketColors = new ItemSocketColor[3];
                    for (var i = 0; i < 3; i++)
                        item.ItemSocketColors[i] = db2File.ReadInt32E<ItemSocketColor>("Socket Color", i);

                    item.SocketContent = new uint[3];
                    for (var i = 0; i < 3; i++)
                        item.SocketContent[i] = db2File.ReadUInt32("Socket Item", i);

                    item.SocketBonus = db2File.ReadInt32("Socket Bonus");
                    item.GemProperties = db2File.ReadInt32("Gem Properties");
                    item.ArmorDamageModifier = db2File.ReadSingle("Armor Damage Modifier");
                    item.Duration = db2File.ReadUInt32("Duration");
                    item.ItemLimitCategory = db2File.ReadInt32("Limit Category");
                    item.HolidayId = db2File.ReadInt32E<Holiday>("Holiday");
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
                            string value = blockVal.UInt32Value + "/" + blockVal.SingleValue;
                            packet.AddValue("Block Value " + i, value);
                        }
                        else
                        {
                            var left = db2File.Length - db2File.Position;
                            for (var j = 0; j < left; ++j)
                            {
                                var value = db2File.ReadByte();
                                packet.AddValue("Byte Value " + i, value);
                            }
                            break;
                        }
                    }
                    break;
                }
            }

            db2File.ClosePacket(false);
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
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
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
            pageText.NextPageID = packet.ReadUInt32("Next Page");
            packet.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(entry, pageText, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcTextMop();

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
            npcText.BroadcastTextId = new uint[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.ReadUInt32("Broadcast Text Id", i);

            pkt.ClosePacket(false);

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add((uint)entry.Key, npcText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var guid = new byte[8];
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.StartBitStream(guid, 5, 7, 3, 0, 4, 1, 6, 2);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);

            var hasData = packet.ReadByte("Byte18");
            if (hasData == 0)
            {
                packet.ReadInt32("Int24");
                packet.ReadByte("Race");
                packet.ReadByte("Gender");
                packet.ReadByte("Level");
                packet.ReadByte("Class");
                packet.ReadInt32("Realm Id");
            }

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);

            if (hasData == 0)
            {
                guid2[6] = packet.ReadBit();
                guid1[7] = packet.ReadBit();

                var bits38 = packet.ReadBits(6);

                packet.StartBitStream(guid2, 1, 7, 2);

                guid1[4] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                guid2[0] = packet.ReadBit();
                guid1[1] = packet.ReadBit();

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                guid1[3] = packet.ReadBit();
                guid2[3] = packet.ReadBit();

                packet.StartBitStream(guid1, 5, 0);
                guid2[5] = packet.ReadBit();

                packet.ReadBit(); // fake bit

                guid1[2] = packet.ReadBit();
                guid1[6] = packet.ReadBit();

                packet.ReadWoWString("Name", bits38);

                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(guid1, 2);
                packet.ReadXORByte(guid1, 4);
                packet.ReadXORByte(guid2, 5);
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid2, 7);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);

                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(guid1, 7);
                packet.ReadXORByte(guid1, 1);
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid1, 0);
                packet.ReadXORByte(guid2, 2);
                packet.ReadXORByte(guid1, 5);

                packet.WriteGuid("Guid1", guid1);
                packet.WriteGuid("Guid2", guid2);
            }

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 7, 0, 1);

            var hasRealmId2 = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasRealmId1 = packet.ReadBit();
            packet.StartBitStream(guid, 3, 2, 4);
            packet.ParseBitStream(guid, 0, 1, 3, 4, 6, 5, 2, 7);

            if (hasRealmId1)
                packet.ReadInt32("Realm Id 1");

            if (hasRealmId2)
                packet.ReadInt32("Realm Id 2");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");

            var guid = new byte[8];
            packet.StartBitStream(guid, 7, 3, 1, 5, 6, 4, 0, 2);
            packet.ParseBitStream(guid, 1, 5, 2, 7, 3, 6, 4, 0);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            var guid = new byte[8];
            packet.ReadInt32("Entry");

            packet.StartBitStream(guid, 0, 7, 5, 2, 1, 3, 4, 6);
            packet.ParseBitStream(guid, 7, 4, 6, 5, 2, 3, 0, 1);

            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            var hasData = packet.ReadBit();
            if (!hasData)
            {
                packet.ReadUInt32("Entry");
                return; // nothing to do
            }

            var quest = new QuestTemplate();

            var bits907 = packet.ReadBits(12);
            var count = packet.ReadBits(19);

            var len2949_20 = new uint[count];
            var counter = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                len2949_20[i] = packet.ReadBits(8);
                counter[i] = packet.ReadBits(22);
            }

            var bits2432 = packet.ReadBits(11);
            var bits2048 = packet.ReadBits(8);
            var bits2112 = packet.ReadBits(10);
            var bits157 = packet.ReadBits(12);
            var bits1657 = packet.ReadBits(9);
            var bits2368 = packet.ReadBits(8);
            var bits1792 = packet.ReadBits(10);
            var bits29 = packet.ReadBits(9);

            packet.ReadInt32("Int2E34");
            packet.ReadInt32("Int4C");
            packet.ReadSingle("Float54");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Int2E10", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntEA", i);
                packet.ReadWoWString("StringEA", len2949_20[i], i);
                packet.ReadByte("ByteED", i);
                packet.ReadByte("ByteEA", i);

                for (var j = 0; j < counter[i]; ++j)
                    packet.ReadInt32("Int118", i, j);
            }

            packet.ReadInt32("Int58");
            packet.ReadInt32("Int2E54");
            packet.ReadSingle("Float6C");
            packet.ReadWoWString("String2500", bits2368);
            packet.ReadInt32("Int34");
            packet.ReadWoWString("String19E4", bits1657);
            packet.ReadInt32("Int2E74");

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("int3001+16", i);
                packet.ReadInt32("int3001+0", i);
            }

            packet.ReadInt32("Int1C");
            packet.ReadSingle("Float68");
            packet.ReadInt32("Int2E28");

            for (var i = 0; i < 5; ++i)
            {
                packet.ReadInt32("int2986+40", i);
                packet.ReadInt32("int2986+0", i);
                packet.ReadInt32("int2986+20", i);
            }

            packet.ReadInt32("Int1BE8");
            packet.ReadInt32("Int2E7C");
            packet.ReadInt32("Int1BF8");
            packet.ReadInt32("Int1BFC");
            packet.ReadInt32("Int2E90");
            packet.ReadInt32("Int2E48");
            packet.ReadWoWString("String74", bits29);
            packet.ReadInt32("Int2E2C");
            packet.ReadInt32("Int2E50");
            packet.ReadInt32("Int2E64");
            packet.ReadInt32("Int1BEC");
            packet.ReadInt32("Int60");
            packet.ReadInt32("Int2E88");
            packet.ReadInt32("Int2E94");
            packet.ReadInt32("Int2E6C");
            packet.ReadInt32("Int14");
            packet.ReadInt32("Int2E20");
            packet.ReadInt32("Int2E30");
            packet.ReadInt32("Int2E24");
            packet.ReadInt32("Int1BF0");
            packet.ReadInt32("Int2E4C");
            packet.ReadInt32("Int2E68");
            packet.ReadInt32("Int20");
            packet.ReadInt32("Int1BF4");
            packet.ReadWoWString("String2100", bits2112);
            packet.ReadInt32("Int2E08");
            packet.ReadInt32("Int38");
            packet.ReadInt32("Int5C");
            packet.ReadWoWString("String2600", bits2432);
            packet.ReadInt32("Int24");
            packet.ReadInt32("Int2E58");
            packet.ReadInt32("Int30");
            packet.ReadInt32("Int64");
            packet.ReadInt32("Int44");
            packet.ReadInt32("Int2E00");
            packet.ReadInt32("Int2E44");
            packet.ReadInt32("Int2EA0");
            packet.ReadInt32("Int28");
            packet.ReadInt32("Int2E1C");
            packet.ReadInt32("Int40");
            packet.ReadWoWString("StringE2C", bits907);
            packet.ReadInt32("Int2E60");
            packet.ReadWoWString("String2000", bits2048);
            packet.ReadInt32("Int2E70");
            packet.ReadInt32("Int2E5C");
            packet.ReadInt32("Int18");
            packet.ReadInt32("Int50");
            packet.ReadInt32("Int1BE4");
            packet.ReadWoWString("String1C00", bits1792);
            packet.ReadInt32("Int3C");
            packet.ReadInt32("Int2C");
            packet.ReadWoWString("String274", bits157);
            packet.ReadInt32("Int48");
            packet.ReadInt32("Int2E80");
            packet.ReadInt32("Int2E40");
            packet.ReadInt32("Int2E9C");
            packet.ReadInt32("Int2E84");
            packet.ReadInt32("Int2E38");
            packet.ReadInt32("Int2E04");
            packet.ReadInt32("Int2E98");
            packet.ReadInt32("Int2E3C");
            packet.ReadInt32("Int2E78");
            packet.ReadInt32("Int70");
            packet.ReadInt32("Int2E8C");

            var id = packet.ReadInt32("Int2F00");

            //packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");
            //Storage.QuestTemplates.Add((uint)id.Key, quest, packet.TimeSpan);
        }
    }
}
