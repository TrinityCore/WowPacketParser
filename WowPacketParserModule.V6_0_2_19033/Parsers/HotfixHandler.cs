using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class HotfixHandler
    {
        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDbQueryBulk(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");

            uint count;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_0_3_19103))
                count = packet.ReadBits("Count", 13);
            else
                count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Guid");
                packet.ReadInt32("Entry", i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var type = packet.ReadUInt32E<DB2Hash>("DB2 File");
            var entry = (uint)packet.ReadInt32("Entry");
            packet.ReadTime("Hotfix date");

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

                    var id = db2File.ReadEntry("Id");
                    broadcastText.language = db2File.ReadUInt32("Language");
                    var maletextLength = db2File.ReadUInt16();
                    broadcastText.MaleText = db2File.ReadWoWString("Male Text", maletextLength);
                    var femaletextLength = db2File.ReadUInt16();
                    broadcastText.FemaleText = db2File.ReadWoWString("Female Text", femaletextLength);

                    broadcastText.EmoteID = new uint[3];
                    broadcastText.EmoteDelay = new uint[3];
                    for (var i = 0; i < 3; ++i)
                        broadcastText.EmoteID[i] = (uint) db2File.ReadInt32("Emote ID", i);
                    for (var i = 0; i < 3; ++i)
                        broadcastText.EmoteDelay[i] = (uint) db2File.ReadInt32("Emote Delay", i);

                    broadcastText.soundId = db2File.ReadUInt32("Sound Id");
                    broadcastText.unk1 = db2File.ReadUInt32("Unk MoP 1"); // unk emote
                    broadcastText.unk2 = db2File.ReadUInt32("Unk MoP 2"); // kind of type?

                    Storage.BroadcastTexts.Add((uint) id.Key, broadcastText, packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.BroadcastText, id.Key, "BROADCAST_TEXT");
                    break;
                }
                case DB2Hash.Creature: // New structure - 6.0.2
                {
                    db2File.ReadUInt32("Creature ID");
                    db2File.ReadInt32E<CreatureType>("Type");

                    for (var i = 0; i < 3; ++i)
                        db2File.ReadUInt32<ItemId>("Item ID", i);

                    db2File.ReadUInt32("Mount");
                    for (var i = 0; i < 4; ++i)
                        db2File.ReadInt32("Display ID", i);

                    for (var i = 0; i < 4; ++i)
                        db2File.ReadSingle("Display ID Probability", i);

                    if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("Name");

                    if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("Female Name");

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

                    var id = db2File.ReadEntry("Id");
                    creatureDifficulty.CreatureID = db2File.ReadUInt32("Creature Id");
                    creatureDifficulty.FactionID = db2File.ReadUInt32("Faction Template Id");
                    creatureDifficulty.Expansion = db2File.ReadInt32("Expansion");
                    creatureDifficulty.MinLevel = db2File.ReadInt32("Min Level");
                    creatureDifficulty.MaxLevel = db2File.ReadInt32("Max Level");

                    creatureDifficulty.Flags = new uint[5];
                    for (var i = 0; i < 5; ++i)
                        creatureDifficulty.Flags[i] = db2File.ReadUInt32("Flags", i);

                    Storage.CreatureDifficultys.Add((uint)id.Key, creatureDifficulty, packet.TimeSpan);
                    break;
                }
                case DB2Hash.GameObjects: // New structure - 6.0.2
                {
                    var gameObjectTemplateDB2 = new GameObjectTemplateDB2();

                    var id = db2File.ReadEntry("ID");

                    gameObjectTemplateDB2.MapID = db2File.ReadUInt32("Map");

                    gameObjectTemplateDB2.DisplayId = db2File.ReadUInt32("DisplayID");

                    gameObjectTemplateDB2.PositionX = db2File.ReadSingle("PositionX");
                    gameObjectTemplateDB2.PositionY = db2File.ReadSingle("PositionY");
                    gameObjectTemplateDB2.PositionZ = db2File.ReadSingle("PositionZ");
                    gameObjectTemplateDB2.RotationX = db2File.ReadSingle("RotationX");
                    gameObjectTemplateDB2.RotationY = db2File.ReadSingle("RotationY");
                    gameObjectTemplateDB2.RotationZ = db2File.ReadSingle("RotationZ");
                    gameObjectTemplateDB2.RotationW = db2File.ReadSingle("RotationW");

                    gameObjectTemplateDB2.Size = db2File.ReadSingle("Size");

                    db2File.ReadInt32("Phase Use Flags");
                    gameObjectTemplateDB2.PhaseId = db2File.ReadInt32("PhaseID");
                    gameObjectTemplateDB2.PhaseGroupId = db2File.ReadInt32("PhaseGroupID");

                    gameObjectTemplateDB2.Type = db2File.ReadInt32E<GameObjectType>("Type");

                    gameObjectTemplateDB2.Data = new int[8];
                    for (var i = 0; i < gameObjectTemplateDB2.Data.Length; i++)
                        gameObjectTemplateDB2.Data[i] = db2File.ReadInt32("Data", i);

                    if (db2File.ReadUInt16() > 0)
                        gameObjectTemplateDB2.Name = db2File.ReadCString("Name");

                    Storage.GameObjectTemplateDB2s.Add((uint)id.Key, gameObjectTemplateDB2, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Item: // New structure - 6.0.2
                {
                    var item = Storage.ItemTemplates.ContainsKey(entry)
                        ? Storage.ItemTemplates[entry].Item1
                        : new ItemTemplate();

                    db2File.ReadUInt32<ItemId>("Item ID");
                    item.Class = db2File.ReadInt32E<ItemClass>("Class");
                    item.SubClass = db2File.ReadUInt32("Sub Class");
                    item.SoundOverrideSubclass = db2File.ReadInt32("Sound Override Subclass");
                    item.Material = db2File.ReadInt32E<Material>("Material");
                    item.InventoryType = db2File.ReadUInt32E<InventoryType>("Inventory Type");
                    item.SheathType = db2File.ReadInt32E<SheathType>("Sheath Type");
                    db2File.ReadInt32("Icon File Data ID");
                    db2File.ReadInt32("Item Group Sounds ID");

                    Storage.ItemTemplates.Add(entry, item, packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.Item, (int) entry, "DB_REPLY");
                    break;
                }
                case DB2Hash.ItemExtendedCost: // New structure - 6.0.2
                {
                    db2File.ReadUInt32("Item Extended Cost ID");
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

                    db2File.ReadUInt32("Required Faction ID");
                    db2File.ReadUInt32("Required Faction Standing");
                    db2File.ReadUInt32("Requirement Flags");
                    db2File.ReadInt32<AchievementId>("Required Achievement");
                    break;
                }
                case DB2Hash.ItemCurrencyCost:
                {
                    db2File.ReadUInt32("ID");
                    db2File.ReadUInt32<ItemId>("Item ID");
                    break;
                }
                case DB2Hash.RulesetItemUpgrade:
                {
                    db2File.ReadUInt32("ID");
                    db2File.ReadUInt32("Item Upgrade Level");
                    db2File.ReadUInt32("Item Upgrade ID");
                    db2File.ReadUInt32<ItemId>("Item ID");
                    break;
                }
                case DB2Hash.Item_sparse: // New structure - 6.0.2
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

                    item.StatUnk1 = new int[10];
                    for (var i = 0; i < 10; i++)
                        item.StatUnk1[i] = db2File.ReadInt32("Unk UInt32 1", i);

                    item.StatUnk2 = new int[10];
                    for (var i = 0; i < 10; i++)
                        item.StatUnk2[i] = db2File.ReadInt32("Unk UInt32 2", i);

                    item.ScalingStatDistribution = db2File.ReadInt32("Scaling Stat Distribution");
                    item.DamageType = db2File.ReadInt32E<DamageType>("Damage Type");
                    item.Delay = db2File.ReadUInt32("Delay");
                    item.RangedMod = db2File.ReadSingle("Ranged Mod");
                    item.Bonding = db2File.ReadInt32E<ItemBonding>("Bonding");

                    var nameLength = db2File.ReadUInt16();
                    item.Name = db2File.ReadWoWString("Name", nameLength, 0);

                    for (var i = 1; i < 4; ++i)
                        if (db2File.ReadUInt16() > 0)
                            db2File.ReadCString("Name", i);

                    var descriptionLength = db2File.ReadUInt16();
                    item.Description = db2File.ReadWoWString("Description", descriptionLength);

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

                    item.SocketBonus = db2File.ReadInt32("Socket Bonus");
                    item.GemProperties = db2File.ReadInt32("Gem Properties");
                    item.ArmorDamageModifier = db2File.ReadSingle("Armor Damage Modifier");
                    item.Duration = db2File.ReadUInt32("Duration");
                    item.ItemLimitCategory = db2File.ReadInt32("Limit Category");
                    item.HolidayId = db2File.ReadInt32E<Holiday>("Holiday");
                    item.StatScalingFactor = db2File.ReadSingle("Stat Scaling Factor");
                    item.CurrencySubstitutionId = db2File.ReadUInt32("Currency Substitution Id");
                    item.CurrencySubstitutionCount = db2File.ReadUInt32("Currency Substitution Count");
                    item.ItemNameDescriptionId = db2File.ReadUInt32("Item Name Description ID");

                    Storage.ObjectNames.Add(entry, new ObjectName {ObjectType = ObjectType.Item, Name = item.Name},
                        packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.Item, (int) entry, "DB_REPLY");
                    break;
                }
                case DB2Hash.KeyChain:
                {
                    db2File.ReadUInt32("Key Chain ID");
                    db2File.ReadBytes("Key", 32);
                    break;
                }
                case DB2Hash.SceneScript: // lua ftw!
                {
                    db2File.ReadUInt32("Scene Script ID");
                    var nameLength = db2File.ReadUInt16();
                    db2File.ReadWoWString("Name", nameLength);

                    var scriptLength = db2File.ReadUInt16();
                    db2File.ReadWoWString("Script", scriptLength);
                    db2File.ReadUInt32("Previous Scene Script Part");
                    db2File.ReadUInt32("Next Scene Script Part");
                    break;
                }
                case DB2Hash.SpellMisc: // New structure - 6.0.2
                {
                    var spellMisc = new SpellMisc();

                    var id = db2File.ReadEntry("ID");

                    spellMisc.Attributes = new uint[14];
                    for (var i = 0; i < 14; ++i)
                        spellMisc.Attributes[i] = db2File.ReadUInt32("Attributes", i);

                    spellMisc.CastingTimeIndex = db2File.ReadUInt32("CastingTimeIndex");
                    spellMisc.DurationIndex = db2File.ReadUInt32("DurationIndex");
                    spellMisc.RangeIndex = db2File.ReadUInt32("RangeIndex");
                    spellMisc.Speed = db2File.ReadSingle("Speed");

                    spellMisc.SpellVisualID = new uint[14];
                    for (var i = 0; i < 2; ++i)
                        spellMisc.SpellVisualID[i] = db2File.ReadUInt32("SpellVisualID", i);

                    spellMisc.SpellIconID = db2File.ReadUInt32("SpellIconID");
                    spellMisc.ActiveIconID = db2File.ReadUInt32("ActiveIconID");
                    spellMisc.SchoolMask = db2File.ReadUInt32("SchoolMask");
                    spellMisc.UnkWoD1 = db2File.ReadSingle("UnkWoD1");

                    Storage.SpellMiscs.Add((uint)id.Key, spellMisc, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Toy: // New structure - 6.0.2
                {
                    db2File.ReadUInt32("ID");
                    db2File.ReadUInt32<ItemId>("Item ID");
                    db2File.ReadUInt32("Flags");

                    var descriptionLength = db2File.ReadUInt16();
                    db2File.ReadWoWString("Description", descriptionLength);

                    db2File.ReadInt32("Source Type");
                    break;
                }
                case DB2Hash.Vignette:
                {
                    db2File.ReadUInt32("Vignette ID");
                    var nameLength = db2File.ReadUInt16();
                    db2File.ReadWoWString("Name", nameLength);

                    db2File.ReadUInt32("Icon");
                    db2File.ReadUInt32("Flag"); // not 100% sure (8 & 32 as values only) - todo verify with more data
                    db2File.ReadSingle("Unk Float 1");
                    db2File.ReadSingle("Unk Float 2");
                    break;
                }
                case DB2Hash.WbAccessControlList:
                {
                    db2File.ReadUInt32("Id");

                    var addressLength = db2File.ReadUInt16();
                    db2File.ReadWoWString("Address", addressLength);

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

            if (db2File.Length != db2File.Position)
            {
                packet.WriteLine("Packet not fully read! Current position is {0}, length is {1}, and diff is {2}.",
                    db2File.Position, db2File.Length, db2File.Length - db2File.Position);

                if (db2File.Length < 300) // If the packet isn't "too big" and it is not full read, print its hex table
                    packet.AsHex();

                packet.Status = ParsedStatus.WithErrors;
            }
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY_BLOB)]
        public static void HandleHotfixNotifyBlob(Packet packet)
        {
            var count = packet.ReadUInt32("HotfixCount");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32E<DB2Hash>("TableHash", i);
                packet.ReadInt32("RecordID", i);
                packet.ReadTime("Timestamp", i);
            }
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY)]
        public static void HandleHotfixNotify(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("TableHash");
            packet.ReadInt32("RecordID");
            packet.ReadTime("Timestamp");
        }
    }
}
