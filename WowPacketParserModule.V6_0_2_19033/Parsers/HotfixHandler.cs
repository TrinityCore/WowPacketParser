using System;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
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

            var count = ClientVersion.AddedInVersion(ClientVersionBuild.V6_0_3_19103) ? packet.ReadBits("Count", 13) : packet.ReadUInt32("Count");

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
            var type = packet.ReadUInt32E<DB2Hash>("TableHash");
            var entry = (uint)packet.ReadInt32("RecordID");
            var allow = (int)entry >= 0;
            packet.ReadTime("Timestamp");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                allow = packet.ReadBit("Allow");

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            var hotfixData = new HotfixData();

            if (allow)
            {
                if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)))
                {
                    hotfixData.Deleted = false;
                    Storage.HotfixDatas.Add(new Tuple<DB2Hash, int, uint>(type, (int)entry, Storage.HotfixDataStore[new Tuple<DB2Hash, int>(type, (int)entry)].Item1.Timestamp), hotfixData);
                }
            }
            else
            {
                if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, -(int)entry)))
                {
                    hotfixData.Deleted = true;
                    Storage.HotfixDatas.Add(new Tuple<DB2Hash, int, uint>(type, -(int)entry, Storage.HotfixDataStore[new Tuple<DB2Hash, int>(type, -(int)entry)].Item1.Timestamp), hotfixData);
                }
                packet.WriteLine("Row {0} has been removed.", -(int) entry);
                return;
            }

            switch (type)
            {
                case DB2Hash.BroadcastText:
                {
                    var broadcastText = new BroadcastText();

                    var id = db2File.ReadEntry("Id");
                    broadcastText.Language = db2File.ReadInt32("Language");
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

                    broadcastText.SoundId = db2File.ReadUInt32("Sound Id");
                    broadcastText.UnkEmoteId = db2File.ReadUInt32("Unk MoP 1"); // unk emote
                    broadcastText.Type = db2File.ReadUInt32("Unk MoP 2"); // kind of type?

                    if (BinaryPacketReader.GetLocale() != LocaleConstant.enUS)
                    {
                        var broadcastTextLocale = new BroadcastTextLocale
                        {
                            MaleText_lang = broadcastText.MaleText,
                            FemaleText_lang = broadcastText.FemaleText
                        };

                        Storage.BroadcastTextLocales.Add(Tuple.Create((uint)id.Key, BinaryPacketReader.GetClientLocale()), broadcastTextLocale, packet.TimeSpan);
                    }

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.BroadcastTexts.Add((uint) id.Key, broadcastText, packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.None, id.Key, "BROADCAST_TEXT");
                    break;
                }
                case DB2Hash.Creature: // New structure - 6.0.2
                {
                    var creature = new Creature();

                    var id = db2File.ReadEntry("CreatureID");
                    creature.Type = db2File.ReadUInt32E<CreatureType>("Type");

                    creature.ItemID = new uint[5];
                    for (var i = 0; i < 3; ++i)
                        creature.ItemID[i] = db2File.ReadUInt32<ItemId>("ItemID", i);

                    creature.Mount = db2File.ReadUInt32("Mount");

                    creature.DisplayID = new uint[5];
                    for (var i = 0; i < 4; ++i)
                        creature.DisplayID[i] = db2File.ReadUInt32("DisplayID", i);

                    creature.DisplayIDProbability = new float[5];
                    for (var i = 0; i < 4; ++i)
                        creature.DisplayIDProbability[i] = db2File.ReadSingle("DisplayIDProbability", i);

                    if (db2File.ReadUInt16() > 0)
                        creature.Name = db2File.ReadCString("Name");

                    if (db2File.ReadUInt16() > 0)
                        creature.FemaleName = db2File.ReadCString("FemaleName");

                    if (db2File.ReadUInt16() > 0)
                        creature.SubName = db2File.ReadCString("SubName");

                    if (db2File.ReadUInt16() > 0)
                        creature.FemaleSubName = db2File.ReadCString("FemaleSubName");

                    creature.Rank = db2File.ReadUInt32("Rank");
                    creature.InhabitType = db2File.ReadUInt32("InhabitType");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.Creatures.Add((uint)id.Key, creature, packet.TimeSpan);
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

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.CreatureDifficultys.Add((uint)id.Key, creatureDifficulty, packet.TimeSpan);
                    break;
                }
                case DB2Hash.CurvePoint:
                {
                    var curvePoint = new CurvePoint();
                    var id = db2File.ReadUInt32("ID");
                    curvePoint.CurveID = db2File.ReadUInt32("CurveID");
                    curvePoint.Index = db2File.ReadUInt32("Index");
                    curvePoint.X = db2File.ReadSingle("X");
                    curvePoint.Y = db2File.ReadSingle("Y");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.CurvePoints.Add(id, curvePoint, packet.TimeSpan);
                    break;
                }
                case DB2Hash.GameObjects: // New structure - 6.0.2
                {
                    var gameObjects = new GameObjects();

                    var id = db2File.ReadEntry("ID");

                    gameObjects.MapID = db2File.ReadUInt32("Map");

                    gameObjects.DisplayId = db2File.ReadUInt32("DisplayID");

                    gameObjects.PositionX = db2File.ReadSingle("PositionX");
                    gameObjects.PositionY = db2File.ReadSingle("PositionY");
                    gameObjects.PositionZ = db2File.ReadSingle("PositionZ");
                    gameObjects.RotationX = db2File.ReadSingle("RotationX");
                    gameObjects.RotationY = db2File.ReadSingle("RotationY");
                    gameObjects.RotationZ = db2File.ReadSingle("RotationZ");
                    gameObjects.RotationW = db2File.ReadSingle("RotationW");

                    gameObjects.Size = db2File.ReadSingle("Size");

                    db2File.ReadInt32("Phase Use Flags");
                    gameObjects.PhaseId = db2File.ReadUInt32("PhaseID");
                    gameObjects.PhaseGroupId = db2File.ReadUInt32("PhaseGroupID");

                    gameObjects.Type = db2File.ReadInt32E<GameObjectType>("Type");

                    gameObjects.Data = new int[8];
                    for (var i = 0; i < gameObjects.Data.Length; i++)
                        gameObjects.Data[i] = db2File.ReadInt32("Data", i);

                    if (db2File.ReadUInt16() > 0)
                        gameObjects.Name = db2File.ReadCString("Name");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.GameObjects.Add((uint)id.Key, gameObjects, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Item: // New structure - 6.0.2
                {
                    var item = new Item();

                    var id = db2File.ReadUInt32<ItemId>("Item ID");
                    item.Class = db2File.ReadInt32E<ItemClass>("Class");
                    item.SubClass = db2File.ReadUInt32("Sub Class");
                    item.SoundOverrideSubclass = db2File.ReadInt32("Sound Override Subclass");
                    item.Material = db2File.ReadInt32E<Material>("Material");
                    item.InventoryType = db2File.ReadUInt32E<InventoryType>("Inventory Type");
                    item.Sheath = db2File.ReadUInt32E<SheathType>("Sheath");

                    item.FileDataID = db2File.ReadUInt32("FileDataID");
                    item.GroupSoundsID = db2File.ReadUInt32("GroupSoundsID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.Items.Add(id, item, packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.Item, (int) entry, "DB_REPLY");
                    break;
                }
                case DB2Hash.ItemEffect:
                {
                    var itemEffect = new ItemEffect();

                    var id = db2File.ReadUInt32("ID");

                    itemEffect.ItemID = db2File.ReadUInt32<ItemId>("ItemID");
                    itemEffect.OrderIndex = db2File.ReadUInt32<ItemId>("OrderIndex");
                    itemEffect.SpellID = db2File.ReadUInt32<SpellId>("SpellID");
                    itemEffect.Trigger = db2File.ReadUInt32("Trigger");
                    itemEffect.Charges = db2File.ReadUInt32("Charges");
                    itemEffect.Cooldown = db2File.ReadInt32("Cooldown");
                    itemEffect.Category = db2File.ReadUInt32("Category");
                    itemEffect.CategoryCooldown = db2File.ReadInt32("CategoryCooldown");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemEffects.Add(id, itemEffect, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemModifiedAppearance:
                {
                    var itemModifiedAppearance = new ItemModifiedAppearance();

                    var id = db2File.ReadUInt32("ID");

                    itemModifiedAppearance.ItemID = db2File.ReadUInt32<ItemId>("ItemID");
                    itemModifiedAppearance.AppearanceModID = db2File.ReadUInt32<ItemId>("AppearanceModID");
                    itemModifiedAppearance.AppearanceID = db2File.ReadUInt32<SpellId>("AppearanceID");
                    itemModifiedAppearance.IconFileDataID = db2File.ReadUInt32("IconFileDataID");
                    itemModifiedAppearance.Index = db2File.ReadUInt32("Index");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemModifiedAppearances.Add(id, itemModifiedAppearance, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemExtendedCost: // New structure - 6.0.2
                {
                    var itemExtendedCost = new ItemExtendedCost();

                    var id = db2File.ReadUInt32("ItemExtendedCostID");
                    if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_1_0_19678))
                    {
                        itemExtendedCost.RequiredHonorPoints = db2File.ReadUInt32("RequiredHonorPoints");
                        itemExtendedCost.RequiredArenaPoints = db2File.ReadUInt32("RequiredArenaPoints");
                    }

                    itemExtendedCost.RequiredArenaSlot = db2File.ReadUInt32("RequiredArenaSlot");

                    itemExtendedCost.RequiredItem = new uint[5];
                    for (var i = 0; i < 5; ++i)
                        itemExtendedCost.RequiredItem[i] = db2File.ReadUInt32("RequiredItem", i);

                    itemExtendedCost.RequiredItemCount = new uint[5];
                    for (var i = 0; i < 5; ++i)
                        itemExtendedCost.RequiredItemCount[i] = db2File.ReadUInt32("RequiredItemCount", i);

                    itemExtendedCost.RequiredPersonalArenaRating = db2File.ReadUInt32("RequiredPersonalArenaRating");
                    itemExtendedCost.ItemPurchaseGroup = db2File.ReadUInt32("ItemPurchaseGroup");

                    itemExtendedCost.RequiredCurrency = new uint[5];
                    for (var i = 0; i < 5; ++i)
                        itemExtendedCost.RequiredCurrency[i] = db2File.ReadUInt32("RequiredCurrency", i);

                    itemExtendedCost.RequiredCurrencyCount = new uint[5];
                    for (var i = 0; i < 5; ++i)
                        itemExtendedCost.RequiredCurrencyCount[i] = db2File.ReadUInt32("RequiredCurrencyCount", i);

                    itemExtendedCost.RequiredFactionId = db2File.ReadUInt32("RequiredFactionId");
                    itemExtendedCost.RequiredFactionStanding = db2File.ReadUInt32("RequiredFactionStanding");
                    itemExtendedCost.RequirementFlags = db2File.ReadUInt32("RequirementFlags");
                    itemExtendedCost.RequiredAchievement = db2File.ReadUInt32<AchievementId>("RequiredAchievement");
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                        itemExtendedCost.RequiredMoney = db2File.ReadUInt32("RequiredMoney");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemExtendedCosts.Add(id, itemExtendedCost, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemCurrencyCost:
                {
                    var itemCurrencyCost = new ItemCurrencyCost();

                    var id = db2File.ReadUInt32("ID");
                    itemCurrencyCost.ItemId = db2File.ReadUInt32<ItemId>("ItemID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemCurrencyCosts.Add(id, itemCurrencyCost, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Mount:
                {
                    var mount = new Mount();
                    var id = db2File.ReadUInt32("ID");
                    mount.MountTypeId = db2File.ReadUInt32("MountTypeId");
                    mount.DisplayId = db2File.ReadUInt32("DisplayId");
                    mount.Flags = db2File.ReadUInt32("Flags");

                    var nameLength = db2File.ReadUInt16();
                    mount.Name = db2File.ReadWoWString("Name", nameLength);

                    var descriptionLength = db2File.ReadUInt16();
                    mount.Description = db2File.ReadWoWString("Description", descriptionLength);

                    var sourceDescriptionLength = db2File.ReadUInt16();
                    mount.SourceDescription = db2File.ReadWoWString("SourceDescription", sourceDescriptionLength);

                    mount.Source = db2File.ReadUInt32("Source");
                    mount.SpellId = db2File.ReadUInt32("SpellId");
                    mount.PlayerConditionId = db2File.ReadUInt32("PlayerConditionId");

                    Storage.Mounts.Add(id, mount, packet.TimeSpan);
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
                case DB2Hash.Holidays:
                {
                    var holiday = new HolidayData();

                    var id = db2File.ReadUInt32("ID");

                    holiday.Duration = new uint[10];
                    for (var i = 0; i < 10; i++)
                        holiday.Duration[i] = db2File.ReadUInt32("Duration", i);

                    holiday.Date = new uint[16];
                    for (var i = 0; i < 16; i++)
                        holiday.Date[i] = db2File.ReadUInt32("Date", i);

                    holiday.Region = db2File.ReadUInt32("Region");
                    holiday.Looping = db2File.ReadUInt32("Looping");

                    holiday.CalendarFlags = new uint[10];
                    for (var i = 0; i < 10; i++)
                        holiday.CalendarFlags[i] = db2File.ReadUInt32("CalendarFlags", i);

                    holiday.HolidayNameID = db2File.ReadUInt32("HolidayNameID");
                    holiday.HolidayDescriptionID = db2File.ReadUInt32("HolidayDescriptionID");

                    var textureFilenameLength = db2File.ReadUInt16();
                    holiday.TextureFilename = db2File.ReadWoWString("SourceDescription", textureFilenameLength);

                    holiday.Priority = db2File.ReadUInt32("Priority");
                    holiday.CalendarFilterType = db2File.ReadUInt32("CalendarFilterType");
                    holiday.Flags = db2File.ReadUInt32("Flags");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.Holidays.Add(id, holiday, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemAppearance:
                {
                    var itemAppearance = new ItemAppearance();

                    var id = db2File.ReadUInt32("ID");

                    itemAppearance.DisplayID = db2File.ReadUInt32("Display ID");
                    itemAppearance.IconFileDataID = db2File.ReadUInt32("File Data ID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemAppearances.Add(id, itemAppearance, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemBonus:
                {
                    var itemBonus = new ItemBonus();

                    var id = db2File.ReadUInt32("ID");

                    itemBonus.BonusListID = db2File.ReadUInt32("Bonus List ID");
                    itemBonus.Type = db2File.ReadUInt32("Type");

                    itemBonus.Value = new uint[2];
                    for (var i = 0; i < 2; i++)
                        itemBonus.Value[i] = db2File.ReadUInt32("Value", i);

                    itemBonus.Index = db2File.ReadUInt32("Index");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemBonuses.Add(id, itemBonus, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemBonusTreeNode:
                {
                    var itemBonusTreeNode = new ItemBonusTreeNode();
                    var id = db2File.ReadUInt32("ID");

                    itemBonusTreeNode.BonusTreeID = db2File.ReadUInt32("BonusTreeID");
                    itemBonusTreeNode.BonusTreeModID = db2File.ReadUInt32("BonusTreeModID");
                    itemBonusTreeNode.SubTreeID = db2File.ReadUInt32("SubTreeID");
                    itemBonusTreeNode.BonusListID = db2File.ReadUInt32("BonusListID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemBonusTreeNodes.Add(id, itemBonusTreeNode, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Item_sparse: // New structure - 6.0.2
                {
                    var item = new ItemSparse();

                    var id = db2File.ReadUInt32<ItemId>("Item Sparse Entry");

                    item.Quality = db2File.ReadInt32E<ItemQuality>("Quality");
                    item.Flags1 = db2File.ReadUInt32E<ItemProtoFlags>("Flags1");
                    item.Flags2 = db2File.ReadInt32E<ItemFlagExtra>("Flags2");
                    item.Flags3 = db2File.ReadUInt32("Flags3");
                    item.Unk1 = db2File.ReadSingle("Unk430_1");
                    item.Unk2 = db2File.ReadSingle("Unk430_2");
                    item.BuyCount = db2File.ReadUInt32("Buy count");
                    item.BuyPrice = db2File.ReadUInt32("Buy Price");
                    item.SellPrice = db2File.ReadUInt32("Sell Price");
                    item.InventoryType = db2File.ReadInt32E<InventoryType>("Inventory Type");
                    item.AllowedClasses = db2File.ReadInt32E<ClassMask>("Allowed Classes");
                    item.AllowedRaces = db2File.ReadInt32E<RaceMask>("Allowed Races");
                    item.ItemLevel = db2File.ReadUInt32("Item Level");
                    item.RequiredLevel = db2File.ReadUInt32("Required Level");
                    item.RequiredLevel = db2File.ReadUInt32("RequiredSkillID");
                    item.RequiredSkill = db2File.ReadUInt32("Required Skill Level");
                    item.RequiredSpell = (uint) db2File.ReadInt32<SpellId>("Required Spell");
                    item.RequiredHonorRank = db2File.ReadUInt32("Required Honor Rank");
                    item.RequiredCityRank = db2File.ReadUInt32("Required City Rank");
                    item.RequiredReputationFaction = db2File.ReadUInt32("RequiredReputationFaction");
                    item.RequiredReputationRank = db2File.ReadUInt32("RequiredReputationRank");
                    item.MaxCount = db2File.ReadInt32("MaxCount");
                    item.Stackable = db2File.ReadInt32("Stackable");
                    item.ContainerSlots = db2File.ReadUInt32("ContainerSlots");

                    item.ItemStatType = new ItemModType[10];
                    for (var i = 0; i < 10; i++)
                        item.ItemStatType[i] = db2File.ReadInt32E<ItemModType>("ItemStatType", i);

                    item.ItemStatValue = new int[10];
                    for (var i = 0; i < 10; i++)
                        item.ItemStatValue[i] = db2File.ReadInt32("ItemStatValue", i);

                    item.ItemStatAllocation = new int[10];
                    for (var i = 0; i < 10; i++)
                        item.ItemStatAllocation[i] = db2File.ReadInt32("ItemStatAllocation", i);

                    item.ItemStatSocketCostMultiplier = new int[10];
                    for (var i = 0; i < 10; i++)
                        item.ItemStatSocketCostMultiplier[i] = db2File.ReadInt32("ItemStatSocketCostMultiplier", i);

                    item.ScalingStatDistribution = db2File.ReadInt32("ScalingStatDistribution");
                    item.DamageType = db2File.ReadInt32E<DamageType>("DamageType");
                    item.Delay = db2File.ReadUInt32("Delay");
                    item.RangedModRange = db2File.ReadSingle("RangedModRange");
                    item.Bonding = db2File.ReadInt32E<ItemBonding>("Bonding");

                    item.Name = new string[4];
                    for (var i = 0; i < 4; ++i)
                    {
                        var nameLength = db2File.ReadUInt16();
                        item.Name[i] = db2File.ReadWoWString("Name", nameLength, i);
                    }

                    var descriptionLength = db2File.ReadUInt16();
                    item.Description = db2File.ReadWoWString("Description", descriptionLength);

                    item.PageText = db2File.ReadUInt32("PageText");
                    item.LanguageID = db2File.ReadInt32E<Language>("LanguageID");
                    item.PageMaterial = db2File.ReadInt32E<PageMaterial>("PageMaterial");
                    item.StartQuest = db2File.ReadUInt32<QuestId>("StartQuest");
                    item.LockID = db2File.ReadUInt32("LockID");
                    item.Material = db2File.ReadInt32E<Material>("Material");
                    item.Sheath = db2File.ReadInt32E<SheathType>("Sheath");
                    item.RandomProperty = db2File.ReadInt32("RandomProperty");
                    item.RandomSuffix = db2File.ReadUInt32("Random Suffix");
                    item.ItemSet = db2File.ReadUInt32("Item Set");
                    item.Area = db2File.ReadUInt32<AreaId>("Area");
                    item.Map = db2File.ReadInt32<MapId>("Map");
                    item.BagFamily = db2File.ReadInt32E<BagFamilyMask>("BagFamily");
                    item.TotemCategory = db2File.ReadInt32E<TotemCategory>("TotemCategory");

                    item.SocketColor = new ItemSocketColor[3];
                    for (var i = 0; i < 3; i++)
                        item.SocketColor[i] = db2File.ReadInt32E<ItemSocketColor>("SocketColor", i);

                    item.SocketBonus = db2File.ReadInt32("SocketBonus");
                    item.GemProperties = db2File.ReadInt32("GemProperties");
                    item.ArmorDamageModifier = db2File.ReadSingle("ArmorDamageModifier");
                    item.Duration = db2File.ReadUInt32("Duration");
                    item.ItemLimitCategory = db2File.ReadInt32("LimitCategory");
                    item.HolidayID = db2File.ReadInt32E<Holiday>("HolidayID");
                    item.StatScalingFactor = db2File.ReadSingle("StatScalingFactor");
                    item.CurrencySubstitutionID = db2File.ReadUInt32("CurrencySubstitutionID");
                    item.CurrencySubstitutionCount = db2File.ReadUInt32("CurrencySubstitutionCount");
                    item.ItemNameDescriptionID = db2File.ReadUInt32("ItemNameDescriptionID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemSparses.Add(id, item, packet.TimeSpan);

                    Storage.ObjectNames.Add(entry, new ObjectName {ObjectType = ObjectType.Item, Name = item.Name[0]}, packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.Item, (int) entry, "DB_REPLY");
                    break;
                }
                case DB2Hash.KeyChain:
                {
                    var key = new KeyChain();
                    var id = db2File.ReadUInt32("ID");

                    key.Key = new byte[32];
                    for (var i = 0; i < 32; i++)
                        key.Key[i] = db2File.ReadByte("Key", i);

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.KeyChains.Add(id, key, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SceneScript: // lua ftw!
                {
                    var sceneScript = new SceneScript();

                    var id = db2File.ReadUInt32("SceneScriptID");

                    var nameLength = db2File.ReadUInt16();
                    sceneScript.Name = db2File.ReadWoWString("Name", nameLength);

                    var scriptLength = db2File.ReadUInt16();
                    sceneScript.Script = db2File.ReadWoWString("Script", scriptLength);

                    sceneScript.PreviousSceneScriptPart = db2File.ReadUInt32("PreviousSceneScriptPart");
                    sceneScript.NextSceneScriptPart = db2File.ReadUInt32("NextSceneScriptPart");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SceneScripts.Add(id, sceneScript, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellMisc: // New structure - 6.0.2
                {
                    var spellMisc = new SpellMisc();

                    var id = db2File.ReadEntry("ID");

                    spellMisc.Attributes = db2File.ReadUInt32("Attributes");
                    spellMisc.AttributesEx = db2File.ReadUInt32("AttributesEx");
                    spellMisc.AttributesExB = db2File.ReadUInt32("AttributesExB");
                    spellMisc.AttributesExC = db2File.ReadUInt32("AttributesExC");
                    spellMisc.AttributesExD = db2File.ReadUInt32("AttributesExD");
                    spellMisc.AttributesExE = db2File.ReadUInt32("AttributesExE");
                    spellMisc.AttributesExF = db2File.ReadUInt32("AttributesExF");
                    spellMisc.AttributesExG = db2File.ReadUInt32("AttributesExG");
                    spellMisc.AttributesExH = db2File.ReadUInt32("AttributesExH");
                    spellMisc.AttributesExI = db2File.ReadUInt32("AttributesExI");
                    spellMisc.AttributesExJ = db2File.ReadUInt32("AttributesExJ");
                    spellMisc.AttributesExK = db2File.ReadUInt32("AttributesExK");
                    spellMisc.AttributesExL = db2File.ReadUInt32("AttributesExL");
                    spellMisc.AttributesExM = db2File.ReadUInt32("AttributesExM");
                    spellMisc.CastingTimeIndex = db2File.ReadUInt32("CastingTimeIndex");
                    spellMisc.DurationIndex = db2File.ReadUInt32("DurationIndex");
                    spellMisc.RangeIndex = db2File.ReadUInt32("RangeIndex");
                    spellMisc.Speed = db2File.ReadSingle("Speed");

                    if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173))
                    {
                        spellMisc.SpellVisualID = new uint[2];
                        for (var i = 0; i < 2; ++i)
                            spellMisc.SpellVisualID[i] = db2File.ReadUInt32("SpellVisualID", i);
                    }

                    spellMisc.SpellIconID = db2File.ReadUInt32("SpellIconID");
                    spellMisc.ActiveIconID = db2File.ReadUInt32("ActiveIconID");
                    spellMisc.SchoolMask = db2File.ReadUInt32("SchoolMask");
                    spellMisc.MultistrikeSpeedMod = db2File.ReadSingle("MultistrikeSpeedMod");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellMiscs.Add((uint)id.Key, spellMisc, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellAuraRestrictions:
                {
                    var spellAuraRestrictions = new SpellAuraRestrictions();

                    var id = db2File.ReadEntry("ID");

                    spellAuraRestrictions.CasterAuraState = db2File.ReadUInt32("CasterAuraState");
                    spellAuraRestrictions.TargetAuraState = db2File.ReadUInt32("TargetAuraState");
                    spellAuraRestrictions.ExcludeCasterAuraState = db2File.ReadUInt32("ExcludeCasterAuraState");
                    spellAuraRestrictions.ExcludeTargetAuraState = db2File.ReadUInt32("ExcludeTargetAuraState");
                    spellAuraRestrictions.CasterAuraSpell = db2File.ReadUInt32("CasterAuraSpell");
                    spellAuraRestrictions.TargetAuraSpell = db2File.ReadUInt32("TargetAuraSpell");
                    spellAuraRestrictions.ExcludeCasterAuraSpell = db2File.ReadUInt32("ExcludeCasterAuraSpell");
                    spellAuraRestrictions.ExcludeTargetAuraSpell = db2File.ReadUInt32("ExcludeTargetAuraSpell");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellAuraRestrictions.Add((uint)id.Key, spellAuraRestrictions, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellCastingRequirements:
                {
                    var spellCastingRequirements = new SpellCastingRequirements();

                    var id = db2File.ReadEntry("ID");

                    spellCastingRequirements.FacingCasterFlags = db2File.ReadUInt32("FacingCasterFlags");
                    spellCastingRequirements.MinFactionID = db2File.ReadUInt32("MinFactionID");
                    spellCastingRequirements.MinReputation = db2File.ReadUInt32("MinReputation");
                    spellCastingRequirements.RequiredAreasID = db2File.ReadUInt32("RequiredAreasID");
                    spellCastingRequirements.RequiredAuraVision = db2File.ReadUInt32("RequiredAuraVision");
                    spellCastingRequirements.RequiresSpellFocus = db2File.ReadUInt32("RequiresSpellFocus");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellCastingRequirements.Add((uint)id.Key, spellCastingRequirements, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellClassOptions:
                {
                    var spellClassOptions = new SpellClassOptions();

                    var id = db2File.ReadEntry("ID");

                    spellClassOptions.ModalNextSpell = db2File.ReadUInt32("ModalNextSpell");

                    spellClassOptions.SpellClassMask = new uint[4];
                    for (var i = 0; i < 2; ++i)
                        spellClassOptions.SpellClassMask[i] = db2File.ReadUInt32("SpellClassMask", i);

                    spellClassOptions.SpellClassSet = db2File.ReadUInt32("SpellClassSet");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellClassOptions.Add((uint)id.Key, spellClassOptions, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellEffectGroupSize:
                {
                    var spellEffectGroupSize = new SpellEffectGroupSize();

                    var id = db2File.ReadEntry("ID");

                    spellEffectGroupSize.SpellEffectID = db2File.ReadUInt32("SpellEffectID");
                    spellEffectGroupSize.Size = db2File.ReadSingle("Size");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellEffectGroupSizes.Add((uint)id.Key, spellEffectGroupSize, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellLearnSpell:
                {
                    var spellLearnSpell = new SpellLearnSpell();

                    var id = db2File.ReadEntry("ID");

                    spellLearnSpell.LearnSpellID = db2File.ReadUInt32("LearnSpellID");
                    spellLearnSpell.SpellID = db2File.ReadUInt32("SpellID");
                    spellLearnSpell.OverridesSpellID = db2File.ReadUInt32("OverridesSpellID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellLearnSpells.Add((uint)id.Key, spellLearnSpell, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellTotems:
                {
                    var spellTotems = new SpellTotems();

                    var id = db2File.ReadEntry("ID");

                    spellTotems.RequiredTotemCategoryID = new uint[2];
                    for (var i = 0; i < 2; ++i)
                        spellTotems.RequiredTotemCategoryID[i] = db2File.ReadUInt32("RequiredTotemCategoryID", i);

                    spellTotems.Totem = new uint[2];
                    for (var i = 0; i < 2; ++i)
                        spellTotems.Totem[i] = db2File.ReadUInt32("Totem", i);

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellTotems.Add((uint)id.Key, spellTotems, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellPower:
                {
                    var spellPower = new SpellPower();

                    var id = db2File.ReadEntry("ID");

                    spellPower.SpellID = db2File.ReadUInt32("SpellID");
                    spellPower.PowerIndex = db2File.ReadUInt32("PowerIndex");
                    spellPower.ManaCost = db2File.ReadUInt32("ManaCost");
                    spellPower.ManaCostPerLevel = db2File.ReadUInt32("ManaCostPerLevel");
                    spellPower.ManaCostPerSecond = db2File.ReadUInt32("ManaCostPerSecond");
                    spellPower.ManaCostAdditional = db2File.ReadUInt32("ManaCostAdditional");
                    spellPower.PowerDisplayID = db2File.ReadUInt32("PowerDisplayID");
                    spellPower.UnitPowerBarID = db2File.ReadUInt32("UnitPowerBarID");

                    spellPower.ManaCostPercentage = db2File.ReadSingle("UnitPowerBarID");
                    spellPower.ManaCostPercentagePerSecond = db2File.ReadSingle("UnitPowerBarID");

                    spellPower.RequiredAura = db2File.ReadUInt32("RequiredAura");

                    spellPower.HealthCostPercentage = db2File.ReadSingle("HealthCostPercentage");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellPowers.Add((uint)id.Key, spellPower, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellReagents:
                {
                    var spellReagents = new SpellReagents();

                    var id = db2File.ReadEntry("ID");

                    spellReagents.Reagent = new int[8];
                    for (var i = 0; i < 2; ++i)
                        spellReagents.Reagent[i] = db2File.ReadInt32("Reagent", i);

                    spellReagents.ReagentCount = new uint[8];
                    for (var i = 0; i < 2; ++i)
                        spellReagents.ReagentCount[i] = db2File.ReadUInt32("ReagentCount", i);

                    spellReagents.CurrencyID = db2File.ReadUInt32("CurrencyID");
                    spellReagents.CurrencyCount = db2File.ReadUInt32("CurrencyCount");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellReagents.Add((uint)id.Key, spellReagents, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellRuneCost:
                {
                    var spellRuneCost = new SpellRuneCost();

                    var id = db2File.ReadEntry("ID");

                    spellRuneCost.Blood = db2File.ReadUInt32("Blood");
                    spellRuneCost.Unholy = db2File.ReadUInt32("Unholy");
                    spellRuneCost.Frost = db2File.ReadUInt32("Frost");
                    spellRuneCost.Chromatic = db2File.ReadUInt32("Chromatic");
                    spellRuneCost.RunicPower = db2File.ReadUInt32("RunicPower");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellRuneCosts.Add((uint)id.Key, spellRuneCost, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Toy: // New structure - 6.0.2
                {
                    var toy = new Toy();

                    var id = db2File.ReadUInt32("ID");

                    toy.ItemID = db2File.ReadUInt32<ItemId>("ItemID");
                    toy.Flags = db2File.ReadUInt32("Flags");

                    var descriptionLength = db2File.ReadUInt16();
                    toy.Description = db2File.ReadWoWString("Description", descriptionLength);

                    toy.SourceType = db2File.ReadInt32("SourceType");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.Toys.Add(id, toy, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Vignette:
                {
                    db2File.ReadUInt32("ID");
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
                case DB2Hash.AreaPOI:
                {
                    var areaPOI = new AreaPOI();

                    var id = db2File.ReadUInt32("Id");

                    areaPOI.Flags = db2File.ReadUInt32("Flags");
                    areaPOI.Flags = db2File.ReadUInt32("Importance");
                    areaPOI.FactionID = db2File.ReadUInt32("FactionID");
                    areaPOI.MapID = db2File.ReadUInt32("MapID");
                    areaPOI.AreaID = db2File.ReadUInt32("AreaID");
                    areaPOI.Icon = db2File.ReadUInt32("Icon");

                    areaPOI.PositionX = db2File.ReadSingle("PositionX");
                    areaPOI.PositionY = db2File.ReadSingle("PositionY");

                    var len1 = db2File.ReadUInt16();
                    areaPOI.Name = db2File.ReadWoWString("Name", len1);

                    var len2 = db2File.ReadUInt16();
                    areaPOI.Description = db2File.ReadWoWString("Description", len2);

                    areaPOI.WorldStateID = db2File.ReadUInt32("WorldStateID");
                    areaPOI.PlayerConditionID = db2File.ReadUInt32("PlayerConditionID");
                    areaPOI.WorldMapLink = db2File.ReadUInt32("WorldMapLink");
                    areaPOI.PortLocID = db2File.ReadUInt32("PortLocID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.AreaPOIs.Add(id, areaPOI, packet.TimeSpan);
                    break;
                }
                case DB2Hash.AreaPOIState:
                {
                    var areaPOIState = new AreaPOIState();

                    var id = db2File.ReadUInt32("Id");

                    areaPOIState.AreaPOIID = db2File.ReadUInt32("AreaPOIID");
                    areaPOIState.State = db2File.ReadUInt32("State");
                    areaPOIState.Icon = db2File.ReadUInt32("Icon");

                    var len2 = db2File.ReadUInt16();
                    areaPOIState.Description = db2File.ReadWoWString("Description", len2);

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.AreaPOIStates.Add(id, areaPOIState, packet.TimeSpan);
                    break;
                }
                case DB2Hash.TaxiNodes:
                {
                    var taxiNodes = new TaxiNodes();

                    var id = db2File.ReadUInt32("Id");

                    taxiNodes.MapID = db2File.ReadUInt32("MapID");

                    taxiNodes.PosX = db2File.ReadSingle("PosX");
                    taxiNodes.PosY = db2File.ReadSingle("PosY");
                    taxiNodes.PosZ = db2File.ReadSingle("PosZ");

                    var len = db2File.ReadInt16();
                    taxiNodes.Name = db2File.ReadWoWString("Name", len);

                    taxiNodes.MountCreatureID = new uint[2];
                    for (var i = 0; i < 2; ++i)
                        taxiNodes.MountCreatureID[i] = db2File.ReadUInt32("MountCreatureID", i);

                    taxiNodes.ConditionID = db2File.ReadUInt32("ConditionID");
                    taxiNodes.LearnableIndex = db2File.ReadUInt32("LearnableIndex");
                    taxiNodes.Flags = db2File.ReadUInt32("Flags");

                    taxiNodes.MapOffsetX = db2File.ReadSingle("MapOffsetX");
                    taxiNodes.MapOffsetY = db2File.ReadSingle("MapOffsetY");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.TaxiNodes.Add(id, taxiNodes, packet.TimeSpan);
                    break;
                }
                case DB2Hash.TaxiPathNode:
                {
                    var taxiPathNode = new TaxiPathNode();

                    var id = db2File.ReadUInt32("Id");

                    taxiPathNode.PathID = db2File.ReadUInt32("PathID");
                    taxiPathNode.NodeIndex = db2File.ReadUInt32("NodeIndex");
                    taxiPathNode.MapID = db2File.ReadUInt32("MapID");

                    taxiPathNode.LocX = db2File.ReadSingle("LocX");
                    taxiPathNode.LocY = db2File.ReadSingle("LocY");
                    taxiPathNode.LocZ = db2File.ReadSingle("LocZ");

                    taxiPathNode.Flags = db2File.ReadUInt32("Flags");
                    taxiPathNode.Delay = db2File.ReadUInt32("Delay");
                    taxiPathNode.ArrivalEventID = db2File.ReadUInt32("ArrivalEventID");
                    taxiPathNode.DepartureEventID = db2File.ReadUInt32("DepartureEventID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.TaxiPathNodes.Add(id, taxiPathNode, packet.TimeSpan);
                    break;
                }
                case DB2Hash.TaxiPath:
                {
                    var taxiPath = new TaxiPath();

                    var id = db2File.ReadUInt32("Id");

                    taxiPath.From = db2File.ReadUInt32("From");
                    taxiPath.To = db2File.ReadUInt32("To");
                    taxiPath.Cost = db2File.ReadUInt32("Cost");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.TaxiPaths.Add(id, taxiPath, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Location:
                {
                    var location = new Location();

                    var id = db2File.ReadUInt32("Id");

                    location.LocX = db2File.ReadSingle("LocX");
                    location.LocY = db2File.ReadSingle("LocY");
                    location.LocZ = db2File.ReadSingle("LocZ");

                    location.Rotation = new float[3];
                    for (var i = 0; i < 3; ++i)
                        location.Rotation[i] = db2File.ReadSingle("Rotation", i);

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.Locations.Add(id, location, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ChrUpgradeTier:
                {
                    var chrUpgradeTier = new ChrUpgradeTier();

                    var id = db2File.ReadEntry("Id");

                    chrUpgradeTier.TierIndex = db2File.ReadUInt32("TierIndex");

                    var len = db2File.ReadUInt16();
                    chrUpgradeTier.Name = db2File.ReadWoWString("Name", len);

                    chrUpgradeTier.TalentTier = db2File.ReadUInt32("TalentTier");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ChrUpgradeTiers.Add((uint)id.Key, chrUpgradeTier, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ChrUpgradeBucket:
                {
                    var chrUpgradeBucket = new ChrUpgradeBucket();

                    var id = db2File.ReadEntry("Id");

                    chrUpgradeBucket.TierID = db2File.ReadUInt32("TierID");
                    chrUpgradeBucket.SpecializationID = db2File.ReadUInt32("SpecializationID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ChrUpgradeBuckets.Add((uint)id.Key, chrUpgradeBucket, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ChrUpgradeBucketSpell:
                {
                    var chrUpgradeBucketSpell = new ChrUpgradeBucketSpell();

                    var id = db2File.ReadEntry("Id");

                    chrUpgradeBucketSpell.BucketID = db2File.ReadUInt32("BucketID");
                    chrUpgradeBucketSpell.SpellID = db2File.ReadUInt32("SpellID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ChrUpgradeBucketSpells.Add((uint)id.Key, chrUpgradeBucketSpell, packet.TimeSpan);
                    break;
                }
                case DB2Hash.BattlePetSpecies:
                {
                    var battlePetSpecies = new BattlePetSpecies();

                    var id = db2File.ReadEntry("Id");

                    battlePetSpecies.CreatureID = db2File.ReadUInt32("CreatureID");
                    battlePetSpecies.IconFileID = db2File.ReadUInt32("IconFileID");
                    battlePetSpecies.SummonSpellID = db2File.ReadUInt32("SummonSpellID");
                    battlePetSpecies.PetType = db2File.ReadInt32("PetType");
                    battlePetSpecies.Source = db2File.ReadUInt32("Source");
                    battlePetSpecies.Flags = db2File.ReadUInt32("Flags");

                    var len1 = db2File.ReadUInt16();
                    battlePetSpecies.SourceText = db2File.ReadWoWString("SourceText", len1);

                    var len2 = db2File.ReadUInt16();
                    battlePetSpecies.Description = db2File.ReadWoWString("Description", len2);

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.BattlePetSpecies.Add((uint)id.Key, battlePetSpecies, packet.TimeSpan);
                    break;
                }
                case DB2Hash.OverrideSpellData:
                {
                    var overrideSpellData = new OverrideSpellData();
                    var id = db2File.ReadUInt32("Id");

                    overrideSpellData.SpellID = new uint[10];
                    for (var i = 0; i < 10; ++i)
                        overrideSpellData.SpellID[i] = db2File.ReadUInt32("SpellID", i);

                    overrideSpellData.Flags = db2File.ReadUInt32("Flags");
                    overrideSpellData.PlayerActionbarFileDataID = db2File.ReadUInt32("PlayerActionbarFileDataID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.OverrideSpellDatas.Add(id, overrideSpellData, packet.TimeSpan);
                    break;
                }
                case DB2Hash.PhaseXPhaseGroup:
                {
                    var phaseXPhaseGroup = new PhaseXPhaseGroup();

                    var id = db2File.ReadUInt32("Id");

                    phaseXPhaseGroup.PhaseID = db2File.ReadUInt32("PhaseID");
                    phaseXPhaseGroup.PhaseGroupID = db2File.ReadUInt32("PhaseGroupID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.PhaseXPhaseGroups.Add(id, phaseXPhaseGroup, packet.TimeSpan);
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
                            var key = "Block Value " + i;
                            var value = blockVal.UInt32Value + "/" + blockVal.SingleValue;
                            packet.AddValue(key, value);
                        }
                        else
                        {
                            var left = db2File.Length - db2File.Position;
                            for (var j = 0; j < left; ++j)
                            {
                                var key = "Byte Value " + i;
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
                packet.WriteLine(string.Format("Packet not fully read! Current position is {0}, length is {1}, and diff is {2}.",
                    db2File.Position, db2File.Length, db2File.Length - db2File.Position));

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
                var hotfixData = new HotfixData();

                var tableHash = packet.ReadUInt32E<DB2Hash>("TableHash", i);
                var recordID = packet.ReadInt32("RecordID", i);
                hotfixData.Timestamp = packet.ReadUInt32("Timestamp", i);

                Storage.HotfixDataStore.Add(Tuple.Create(tableHash, recordID), hotfixData);
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
