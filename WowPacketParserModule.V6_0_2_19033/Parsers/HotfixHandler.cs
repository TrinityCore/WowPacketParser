using System;
using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class HotfixHandler
    {
        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDbQueryBulk(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");

            uint count = ClientVersion.AddedInVersion(ClientVersionBuild.V6_0_3_19103) ? packet.ReadBits("Count", 13) : packet.ReadUInt32("Count");

            for (int i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Guid");
                packet.ReadInt32("Entry", i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            DB2Hash type = packet.ReadUInt32E<DB2Hash>("TableHash");
            uint entry = (uint)packet.ReadInt32("RecordID");
            bool allow = (int)entry >= 0;
            packet.ReadTime("Timestamp");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                allow = packet.ReadBit("Allow");

            int size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            Packet db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            HotfixData hotfixData = new HotfixData
            {
                TableHash = type,
            };

            if (allow)
            {
                if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)))
                {
                    hotfixData.Deleted = false;
                    hotfixData.RecordID = (int)entry;
                    hotfixData.Timestamp =
                        Storage.HotfixDataStore[new Tuple<DB2Hash, int>(type, (int)entry)].Item1.Timestamp;
                    Storage.HotfixDatas.Add(hotfixData);
                }
            }
            else
            {
                if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, -(int)entry)))
                {
                    hotfixData.Deleted = true;
                    hotfixData.RecordID = -(int)entry;
                    hotfixData.Timestamp =
                        Storage.HotfixDataStore[new Tuple<DB2Hash, int>(type, -(int)entry)].Item1.Timestamp;
                    Storage.HotfixDatas.Add(hotfixData);
                }
                packet.WriteLine("Row {0} has been removed.", -(int) entry);
                return;
            }

            switch (type)
            {
                case DB2Hash.BroadcastText:
                {
                    BroadcastText broadcastText = new BroadcastText();

                    var id = db2File.ReadEntry("Id");
                    broadcastText.ID = (uint)id.Key;
                    broadcastText.Language = db2File.ReadInt32("Language");
                    ushort maletextLength = db2File.ReadUInt16();
                    broadcastText.MaleText = db2File.ReadWoWString("Male Text", maletextLength);
                    ushort femaletextLength = db2File.ReadUInt16();
                    broadcastText.FemaleText = db2File.ReadWoWString("Female Text", femaletextLength);

                    broadcastText.EmoteID = new uint?[3];
                    broadcastText.EmoteDelay = new uint?[3];
                    for (int i = 0; i < 3; ++i)
                        broadcastText.EmoteID[i] = (uint) db2File.ReadInt32("Emote ID", i);
                    for (int i = 0; i < 3; ++i)
                        broadcastText.EmoteDelay[i] = (uint) db2File.ReadInt32("Emote Delay", i);

                    broadcastText.SoundId = db2File.ReadUInt32("Sound Id");
                    broadcastText.UnkEmoteId = db2File.ReadUInt32("Unk MoP 1"); // unk emote
                    broadcastText.Type = db2File.ReadUInt32("Unk MoP 2"); // kind of type?

                    if (BinaryPacketReader.GetLocale() != LocaleConstant.enUS)
                    {
                        BroadcastTextLocale broadcastTextLocale = new BroadcastTextLocale
                        {
                            ID = (uint)id.Key,
                            Locale = BinaryPacketReader.GetClientLocale(),
                            MaleTextLang = broadcastText.MaleText,
                            FemaleTextLang = broadcastText.FemaleText
                        };

                        Storage.BroadcastTextLocales.Add(broadcastTextLocale, packet.TimeSpan);
                    }

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.BroadcastTexts.Add(broadcastText, packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.None, id.Key, "BROADCAST_TEXT");
                    break;
                }
                case DB2Hash.Creature: // New structure - 6.0.2
                {
                    CreatureDB2 creature = new CreatureDB2();

                    creature.ID = (uint)db2File.ReadEntry("CreatureID").Key;
                    creature.Type = db2File.ReadUInt32E<CreatureType>("Type");

                    creature.ItemID = new uint?[5];
                    for (int i = 0; i < 3; ++i)
                        creature.ItemID[i] = db2File.ReadUInt32<ItemId>("ItemID", i);

                    creature.Mount = db2File.ReadUInt32("Mount");

                    creature.DisplayID = new uint?[5];
                    for (int i = 0; i < 4; ++i)
                        creature.DisplayID[i] = db2File.ReadUInt32("DisplayID", i);

                    creature.DisplayIDProbability = new float?[5];
                    for (int i = 0; i < 4; ++i)
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
                        Storage.Creatures.Add(creature, packet.TimeSpan);
                    break;
                }
                case DB2Hash.CreatureDifficulty:
                {
                    CreatureDifficulty creatureDifficulty = new CreatureDifficulty();

                    creatureDifficulty.ID = (uint)db2File.ReadEntry("Id").Key;

                    creatureDifficulty.CreatureID = db2File.ReadUInt32("Creature Id");
                    creatureDifficulty.FactionID = db2File.ReadUInt32("Faction Template Id");
                    creatureDifficulty.Expansion = db2File.ReadInt32("Expansion");
                    creatureDifficulty.MinLevel = db2File.ReadInt32("Min Level");
                    creatureDifficulty.MaxLevel = db2File.ReadInt32("Max Level");

                    creatureDifficulty.Flags = new uint?[5];
                    for (int i = 0; i < 5; ++i)
                        creatureDifficulty.Flags[i] = db2File.ReadUInt32("Flags", i);

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.CreatureDifficulties.Add(creatureDifficulty, packet.TimeSpan);
                    break;
                }
                case DB2Hash.CriteriaTree:
                {
                    var id = db2File.ReadUInt32("ID");
                    db2File.ReadUInt32("CriteriaID");
                    db2File.ReadUInt64("Amount");
                    db2File.ReadUInt32("Operator");
                    db2File.ReadUInt32("Parent");
                    db2File.ReadUInt32("Flags");
                    if (db2File.ReadUInt16() > 0)
                        db2File.ReadCString("Description");
                    db2File.ReadUInt32("OrderIndex");
                    break;
                }
                case DB2Hash.CurvePoint:
                {
                    CurvePoint curvePoint = new CurvePoint();

                    curvePoint.ID = db2File.ReadUInt32("ID");

                    curvePoint.CurveID = db2File.ReadUInt32("CurveID");
                    curvePoint.Index = db2File.ReadUInt32("Index");
                    curvePoint.X = db2File.ReadSingle("X");
                    curvePoint.Y = db2File.ReadSingle("Y");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.CurvePoints.Add(curvePoint, packet.TimeSpan);
                    break;
                }
                case DB2Hash.GameObjects: // New structure - 6.0.2
                {
                    GameObjects gameObjects = new GameObjects();

                    gameObjects.ID = (uint)db2File.ReadEntry("ID").Key;

                    gameObjects.MapID = db2File.ReadUInt32("Map");

                    gameObjects.DisplayID = db2File.ReadUInt32("DisplayID");

                    gameObjects.PositionX = db2File.ReadSingle("PositionX");
                    gameObjects.PositionY = db2File.ReadSingle("PositionY");
                    gameObjects.PositionZ = db2File.ReadSingle("PositionZ");
                    gameObjects.RotationX = db2File.ReadSingle("RotationX");
                    gameObjects.RotationY = db2File.ReadSingle("RotationY");
                    gameObjects.RotationZ = db2File.ReadSingle("RotationZ");
                    gameObjects.RotationW = db2File.ReadSingle("RotationW");

                    gameObjects.Size = db2File.ReadSingle("Size");

                    db2File.ReadInt32("Phase Use Flags");
                    gameObjects.PhaseID = db2File.ReadUInt32("PhaseID");
                    gameObjects.PhaseGroupID = db2File.ReadUInt32("PhaseGroupID");

                    gameObjects.Type = db2File.ReadInt32E<GameObjectType>("Type");

                    gameObjects.Data = new int?[8];
                    for (int i = 0; i < gameObjects.Data.Length; i++)
                        gameObjects.Data[i] = db2File.ReadInt32("Data", i);

                    if (db2File.ReadUInt16() > 0)
                        gameObjects.Name = db2File.ReadCString("Name");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.GameObjectsBag.Add(gameObjects, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Item: // New structure - 6.0.2
                {
                    Item item = new Item();

                    item.ID = db2File.ReadUInt32<ItemId>("Item ID");

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
                        Storage.Items.Add(item, packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.Item, (int) entry, "DB_REPLY");
                    break;
                }
                case DB2Hash.ItemEffect:
                {
                    ItemEffect itemEffect = new ItemEffect();

                    itemEffect.ID = db2File.ReadUInt32("ID");

                    itemEffect.ItemID = db2File.ReadUInt32<ItemId>("ItemID");
                    itemEffect.OrderIndex = db2File.ReadUInt32<ItemId>("OrderIndex");
                    itemEffect.SpellID = db2File.ReadUInt32<SpellId>("SpellID");
                    itemEffect.Trigger = db2File.ReadUInt32("Trigger");
                    itemEffect.Charges = db2File.ReadUInt32("Charges");
                    itemEffect.Cooldown = db2File.ReadInt32("Cooldown");
                    itemEffect.Category = db2File.ReadUInt32("Category");
                    itemEffect.CategoryCooldown = db2File.ReadInt32("CategoryCooldown");
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20182))
                        itemEffect.ChrSpecializationID = db2File.ReadUInt32("ChrSpecializationID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemEffects.Add(itemEffect, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemModifiedAppearance:
                {
                    ItemModifiedAppearance itemModifiedAppearance = new ItemModifiedAppearance();

                    itemModifiedAppearance.ID = db2File.ReadUInt32("ID");

                    itemModifiedAppearance.ItemID = db2File.ReadUInt32<ItemId>("ItemID");
                    itemModifiedAppearance.AppearanceModID = db2File.ReadUInt32<ItemId>("AppearanceModID");
                    itemModifiedAppearance.AppearanceID = db2File.ReadUInt32<SpellId>("AppearanceID");
                    itemModifiedAppearance.IconFileDataID = db2File.ReadUInt32("IconFileDataID");
                    itemModifiedAppearance.Index = db2File.ReadUInt32("Index");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemModifiedAppearances.Add(itemModifiedAppearance, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemExtendedCost: // New structure - 6.0.2
                {
                    ItemExtendedCost itemExtendedCost = new ItemExtendedCost();

                    itemExtendedCost.ID = db2File.ReadUInt32("ItemExtendedCostID");

                    if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_1_0_19678))
                    {
                        db2File.ReadUInt32("RequiredHonorPoints");
                        db2File.ReadUInt32("RequiredArenaPoints");
                    }

                    itemExtendedCost.RequiredArenaSlot = db2File.ReadUInt32("RequiredArenaSlot");

                    itemExtendedCost.RequiredItem = new uint?[5];
                    for (int i = 0; i < 5; ++i)
                        itemExtendedCost.RequiredItem[i] = db2File.ReadUInt32("RequiredItem", i);

                    itemExtendedCost.RequiredItemCount = new uint?[5];
                    for (int i = 0; i < 5; ++i)
                        itemExtendedCost.RequiredItemCount[i] = db2File.ReadUInt32("RequiredItemCount", i);

                    itemExtendedCost.RequiredPersonalArenaRating = db2File.ReadUInt32("RequiredPersonalArenaRating");
                    itemExtendedCost.ItemPurchaseGroup = db2File.ReadUInt32("ItemPurchaseGroup");

                    itemExtendedCost.RequiredCurrency = new uint?[5];
                    for (int i = 0; i < 5; ++i)
                        itemExtendedCost.RequiredCurrency[i] = db2File.ReadUInt32("RequiredCurrency", i);

                    itemExtendedCost.RequiredCurrencyCount = new uint?[5];
                    for (int i = 0; i < 5; ++i)
                        itemExtendedCost.RequiredCurrencyCount[i] = db2File.ReadUInt32("RequiredCurrencyCount", i);

                    itemExtendedCost.RequiredFactionId = db2File.ReadUInt32("RequiredFactionId");
                    itemExtendedCost.RequiredFactionStanding = db2File.ReadUInt32("RequiredFactionStanding");
                    itemExtendedCost.RequirementFlags = db2File.ReadUInt32("RequirementFlags");
                    itemExtendedCost.RequiredAchievement = db2File.ReadUInt32<AchievementId>("RequiredAchievement");
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                        itemExtendedCost.RequiredMoney = db2File.ReadUInt32("RequiredMoney");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemExtendedCosts.Add(itemExtendedCost, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemCurrencyCost:
                {
                    ItemCurrencyCost itemCurrencyCost = new ItemCurrencyCost();

                    itemCurrencyCost.ID = db2File.ReadUInt32("ID");
                    itemCurrencyCost.ItemID = db2File.ReadUInt32<ItemId>("ItemID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemCurrencyCosts.Add(itemCurrencyCost, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Mount:
                {
                    Mount mount = new Mount();

                    mount.ID = db2File.ReadUInt32("ID");

                    mount.MountTypeID = db2File.ReadUInt32("MountTypeId");
                    mount.DisplayID = db2File.ReadUInt32("DisplayId");
                    mount.Flags = db2File.ReadUInt32("Flags");

                    ushort nameLength = db2File.ReadUInt16();
                    mount.Name = db2File.ReadWoWString("Name", nameLength);

                    ushort descriptionLength = db2File.ReadUInt16();
                    mount.Description = db2File.ReadWoWString("Description", descriptionLength);

                    ushort sourceDescriptionLength = db2File.ReadUInt16();
                    mount.SourceDescription = db2File.ReadWoWString("SourceDescription", sourceDescriptionLength);

                    mount.Source = db2File.ReadUInt32("Source");
                    mount.SpellID = db2File.ReadUInt32("SpellId");
                    mount.PlayerConditionID = db2File.ReadUInt32("PlayerConditionId");

                    Storage.Mounts.Add(mount, packet.TimeSpan);
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
                    Holidays holiday = new Holidays();

                    holiday.ID = db2File.ReadUInt32("ID");

                    holiday.Duration = new uint?[10];
                    for (int i = 0; i < 10; i++)
                        holiday.Duration[i] = db2File.ReadUInt32("Duration", i);

                    holiday.Date = new uint?[16];
                    for (int i = 0; i < 16; i++)
                        holiday.Date[i] = db2File.ReadUInt32("Date", i);

                    holiday.Region = db2File.ReadUInt32("Region");
                    holiday.Looping = db2File.ReadUInt32("Looping");

                    holiday.CalendarFlags = new uint?[10];
                    for (int i = 0; i < 10; i++)
                        holiday.CalendarFlags[i] = db2File.ReadUInt32("CalendarFlags", i);

                    holiday.HolidayNameID = db2File.ReadUInt32("HolidayNameID");
                    holiday.HolidayDescriptionID = db2File.ReadUInt32("HolidayDescriptionID");

                    ushort textureFilenameLength = db2File.ReadUInt16();
                    holiday.TextureFilename = db2File.ReadWoWString("SourceDescription", textureFilenameLength);

                    holiday.Priority = db2File.ReadUInt32("Priority");
                    holiday.CalendarFilterType = db2File.ReadUInt32("CalendarFilterType");
                    holiday.Flags = db2File.ReadUInt32("Flags");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.HolidaysBag.Add(holiday, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemAppearance:
                {
                    ItemAppearance itemAppearance = new ItemAppearance();

                    itemAppearance.ID = db2File.ReadUInt32("ID");

                    itemAppearance.DisplayID = db2File.ReadUInt32("Display ID");
                    itemAppearance.IconFileDataID = db2File.ReadUInt32("File Data ID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemAppearances.Add(itemAppearance, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemBonus:
                {
                    ItemBonus itemBonus = new ItemBonus();

                    itemBonus.ID = db2File.ReadUInt32("ID");

                    itemBonus.BonusListID = db2File.ReadUInt32("Bonus List ID");
                    itemBonus.Type = db2File.ReadUInt32("Type");

                    itemBonus.Value = new uint?[2];
                    for (int i = 0; i < 2; i++)
                        itemBonus.Value[i] = db2File.ReadUInt32("Value", i);

                    itemBonus.Index = db2File.ReadUInt32("Index");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemBonuses.Add(itemBonus, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemBonusTreeNode:
                {
                    ItemBonusTreeNode itemBonusTreeNode = new ItemBonusTreeNode();
                    itemBonusTreeNode.ID = db2File.ReadUInt32("ID");

                    itemBonusTreeNode.BonusTreeID = db2File.ReadUInt32("BonusTreeID");
                    itemBonusTreeNode.BonusTreeModID = db2File.ReadUInt32("BonusTreeModID");
                    itemBonusTreeNode.SubTreeID = db2File.ReadUInt32("SubTreeID");
                    itemBonusTreeNode.BonusListID = db2File.ReadUInt32("BonusListID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ItemBonusTreeNodes.Add(itemBonusTreeNode, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Item_sparse: // New structure - 6.0.2
                {
                    ItemSparse item = new ItemSparse();

                    item.ID = db2File.ReadUInt32<ItemId>("Item Sparse Entry");

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
                    item.RequiredSkill = db2File.ReadUInt32("Required Skill");
                    item.RequiredSkillRank = db2File.ReadUInt32("Required Skill Rank");
                    item.RequiredSpell = db2File.ReadUInt32<SpellId>("Required Spell");
                    item.RequiredHonorRank = db2File.ReadUInt32("Required Honor Rank");
                    item.RequiredCityRank = db2File.ReadUInt32("Required City Rank");
                    item.RequiredReputationFaction = db2File.ReadUInt32("RequiredReputationFaction");
                    item.RequiredReputationRank = db2File.ReadUInt32("RequiredReputationRank");
                    item.MaxCount = db2File.ReadInt32("MaxCount");
                    item.Stackable = db2File.ReadInt32("Stackable");
                    item.ContainerSlots = db2File.ReadUInt32("ContainerSlots");

                    item.ItemStatType = new ItemModType?[10];
                    for (int i = 0; i < 10; i++)
                        item.ItemStatType[i] = db2File.ReadInt32E<ItemModType>("ItemStatType", i);

                    item.ItemStatValue = new int?[10];
                    for (int i = 0; i < 10; i++)
                        item.ItemStatValue[i] = db2File.ReadInt32("ItemStatValue", i);

                    item.ItemStatAllocation = new int?[10];
                    for (int i = 0; i < 10; i++)
                        item.ItemStatAllocation[i] = db2File.ReadInt32("ItemStatAllocation", i);

                    item.ItemStatSocketCostMultiplier = new float?[10];
                    for (int i = 0; i < 10; i++)
                        item.ItemStatSocketCostMultiplier[i] = db2File.ReadSingle("ItemStatSocketCostMultiplier", i);

                    item.ScalingStatDistribution = db2File.ReadInt32("ScalingStatDistribution");
                    item.DamageType = db2File.ReadInt32E<DamageType>("DamageType");
                    item.Delay = db2File.ReadUInt32("Delay");
                    item.RangedModRange = db2File.ReadSingle("RangedModRange");
                    item.Bonding = db2File.ReadInt32E<ItemBonding>("Bonding");

                    ushort nameLength = db2File.ReadUInt16();
                    item.Name = db2File.ReadWoWString("Name", nameLength);

                    ushort nameLength2 = db2File.ReadUInt16();
                    item.Name2 = db2File.ReadWoWString("Name2", nameLength2);

                    ushort nameLength3 = db2File.ReadUInt16();
                    item.Name3 = db2File.ReadWoWString("Name3", nameLength3);

                    ushort nameLength4 = db2File.ReadUInt16();
                    item.Name4 = db2File.ReadWoWString("Name4", nameLength4);

                    ushort descriptionLength = db2File.ReadUInt16();
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

                    item.SocketColor = new ItemSocketColor?[3];
                    for (int i = 0; i < 3; i++)
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
                        Storage.ItemSparses.Add(item, packet.TimeSpan);

                    Storage.ObjectNames.Add(new ObjectName {ObjectType = ObjectType.Item, ID = (int)entry, Name = item.Name}, packet.TimeSpan);
                    packet.AddSniffData(StoreNameType.Item, (int) entry, "DB_REPLY");
                    break;
                }
                case DB2Hash.KeyChain:
                {
                    KeyChain key = new KeyChain();

                    key.ID = db2File.ReadUInt32("ID");

                    key.Key = new byte?[32];
                    for (int i = 0; i < 32; i++)
                        key.Key[i] = db2File.ReadByte("Key", i);

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.KeyChains.Add(key, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SceneScript: // lua ftw!
                {
                    SceneScript sceneScript = new SceneScript();

                    sceneScript.ID = db2File.ReadUInt32("SceneScriptID");

                    ushort nameLength = db2File.ReadUInt16();
                    sceneScript.Name = db2File.ReadWoWString("Name", nameLength);

                    ushort scriptLength = db2File.ReadUInt16();
                    sceneScript.Script = db2File.ReadWoWString("Script", scriptLength);

                    sceneScript.PreviousSceneScriptPart = db2File.ReadUInt32("PreviousSceneScriptPart");
                    sceneScript.NextSceneScriptPart = db2File.ReadUInt32("NextSceneScriptPart");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SceneScripts.Add(sceneScript, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellMisc: // New structure - 6.0.2
                {
                    SpellMisc spellMisc = new SpellMisc();

                    spellMisc.ID = (uint)db2File.ReadEntry("ID").Key;

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
                        for (int i = 0; i < 2; ++i)
                            db2File.ReadUInt32("SpellVisualID", i);
                    }

                    spellMisc.SpellIconID = db2File.ReadUInt32("SpellIconID");
                    spellMisc.ActiveIconID = db2File.ReadUInt32("ActiveIconID");
                    spellMisc.SchoolMask = db2File.ReadUInt32("SchoolMask");
                    spellMisc.MultistrikeSpeedMod = db2File.ReadSingle("MultistrikeSpeedMod");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellMiscs.Add(spellMisc, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellAuraRestrictions:
                {
                    SpellAuraRestrictions spellAuraRestrictions = new SpellAuraRestrictions();

                    spellAuraRestrictions.ID = (uint)db2File.ReadEntry("ID").Key;

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
                        Storage.SpellAuraRestrictionsBag.Add(spellAuraRestrictions, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellCastingRequirements:
                {
                    SpellCastingRequirements spellCastingRequirements = new SpellCastingRequirements();

                    spellCastingRequirements.ID = (uint)db2File.ReadEntry("ID").Key;

                    spellCastingRequirements.FacingCasterFlags = db2File.ReadUInt32("FacingCasterFlags");
                    spellCastingRequirements.MinFactionID = db2File.ReadUInt32("MinFactionID");
                    spellCastingRequirements.MinReputation = db2File.ReadUInt32("MinReputation");
                    spellCastingRequirements.RequiredAreasID = db2File.ReadUInt32("RequiredAreasID");
                    spellCastingRequirements.RequiredAuraVision = db2File.ReadUInt32("RequiredAuraVision");
                    spellCastingRequirements.RequiresSpellFocus = db2File.ReadUInt32("RequiresSpellFocus");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellCastingRequirementsBag.Add(spellCastingRequirements, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellClassOptions:
                {
                    SpellClassOptions spellClassOptions = new SpellClassOptions();

                    spellClassOptions.ID = (uint)db2File.ReadEntry("ID").Key;

                    spellClassOptions.ModalNextSpell = db2File.ReadUInt32("ModalNextSpell");

                    spellClassOptions.SpellClassMask = new uint?[4];
                    for (int i = 0; i < 2; ++i)
                        spellClassOptions.SpellClassMask[i] = db2File.ReadUInt32("SpellClassMask", i);

                    spellClassOptions.SpellClassSet = db2File.ReadUInt32("SpellClassSet");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellClassOptionsBag.Add(spellClassOptions, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellEffectGroupSize:
                {
                    SpellEffectGroupSize spellEffectGroupSize = new SpellEffectGroupSize();

                    spellEffectGroupSize.ID = (uint)db2File.ReadEntry("ID").Key;

                    spellEffectGroupSize.SpellEffectID = db2File.ReadUInt32("SpellEffectID");
                    spellEffectGroupSize.Size = db2File.ReadSingle("Size");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellEffectGroupSizes.Add(spellEffectGroupSize, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellLearnSpell:
                {
                    SpellLearnSpell spellLearnSpell = new SpellLearnSpell();

                    spellLearnSpell.ID = (uint)db2File.ReadEntry("ID").Key;

                    spellLearnSpell.LearnSpellID = db2File.ReadUInt32("LearnSpellID");
                    spellLearnSpell.SpellID = db2File.ReadUInt32("SpellID");
                    spellLearnSpell.OverridesSpellID = db2File.ReadUInt32("OverridesSpellID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellLearnSpells.Add(spellLearnSpell, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellTotems:
                {
                    SpellTotems spellTotems = new SpellTotems();

                    spellTotems.ID = (uint)db2File.ReadEntry("ID").Key;

                    spellTotems.RequiredTotemCategoryID = new uint?[2];
                    for (int i = 0; i < 2; ++i)
                        spellTotems.RequiredTotemCategoryID[i] = db2File.ReadUInt32("RequiredTotemCategoryID", i);

                    spellTotems.Totem = new uint?[2];
                    for (int i = 0; i < 2; ++i)
                        spellTotems.Totem[i] = db2File.ReadUInt32("Totem", i);

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellTotemsBag.Add(spellTotems, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellPower:
                {
                    SpellPower spellPower = new SpellPower();

                    spellPower.ID = (uint)db2File.ReadEntry("ID").Key;

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
                        Storage.SpellPowers.Add(spellPower, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellReagents:
                {
                    SpellReagents spellReagents = new SpellReagents();

                    spellReagents.ID = (uint)db2File.ReadEntry("ID").Key;

                    spellReagents.Reagent = new int?[8];
                    for (int i = 0; i < 2; ++i)
                        spellReagents.Reagent[i] = db2File.ReadInt32("Reagent", i);

                    spellReagents.ReagentCount = new uint?[8];
                    for (int i = 0; i < 2; ++i)
                        spellReagents.ReagentCount[i] = db2File.ReadUInt32("ReagentCount", i);

                    spellReagents.CurrencyID = db2File.ReadUInt32("CurrencyID");
                    spellReagents.CurrencyCount = db2File.ReadUInt32("CurrencyCount");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellReagentsBag.Add(spellReagents, packet.TimeSpan);
                    break;
                }
                case DB2Hash.SpellRuneCost:
                {
                    SpellRuneCost spellRuneCost = new SpellRuneCost();

                    spellRuneCost.ID = (uint)db2File.ReadEntry("ID").Key;

                    spellRuneCost.Blood = db2File.ReadUInt32("Blood");
                    spellRuneCost.Unholy = db2File.ReadUInt32("Unholy");
                    spellRuneCost.Frost = db2File.ReadUInt32("Frost");
                    spellRuneCost.Chromatic = db2File.ReadUInt32("Chromatic");
                    spellRuneCost.RunicPower = db2File.ReadUInt32("RunicPower");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.SpellRuneCosts.Add(spellRuneCost, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Toy: // New structure - 6.0.2
                {
                    Toy toy = new Toy();

                    toy.ID = db2File.ReadUInt32("ID");

                    toy.ItemID = db2File.ReadUInt32<ItemId>("ItemID");
                    toy.Flags = db2File.ReadUInt32("Flags");

                    ushort descriptionLength = db2File.ReadUInt16();
                    toy.Description = db2File.ReadWoWString("Description", descriptionLength);

                    toy.SourceType = db2File.ReadInt32("SourceType");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.Toys.Add(toy, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Vignette:
                {
                    db2File.ReadUInt32("ID");
                    ushort nameLength = db2File.ReadUInt16();
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

                    ushort addressLength = db2File.ReadUInt16();
                    db2File.ReadWoWString("Address", addressLength);

                    db2File.ReadUInt32("Unk MoP 1");
                    db2File.ReadUInt32("Unk MoP 2");
                    db2File.ReadUInt32("Unk MoP 3");
                    db2File.ReadUInt32("Unk MoP 4"); // flags?
                    break;
                }
                case DB2Hash.AreaPOI:
                {
                    AreaPOI areaPOI = new AreaPOI();

                    areaPOI.ID = db2File.ReadUInt32("Id");

                    areaPOI.Flags = db2File.ReadUInt32("Flags");
                    areaPOI.Importance = db2File.ReadUInt32("Importance");
                    areaPOI.FactionID = db2File.ReadUInt32("FactionID");
                    areaPOI.MapID = db2File.ReadUInt32("MapID");
                    areaPOI.AreaID = db2File.ReadUInt32("AreaID");
                    areaPOI.Icon = db2File.ReadUInt32("Icon");

                    areaPOI.PositionX = db2File.ReadSingle("PositionX");
                    areaPOI.PositionY = db2File.ReadSingle("PositionY");

                    ushort len1 = db2File.ReadUInt16();
                    areaPOI.Name = db2File.ReadWoWString("Name", len1);

                    ushort len2 = db2File.ReadUInt16();
                    areaPOI.Description = db2File.ReadWoWString("Description", len2);

                    areaPOI.WorldStateID = db2File.ReadUInt32("WorldStateID");
                    areaPOI.PlayerConditionID = db2File.ReadUInt32("PlayerConditionID");
                    areaPOI.WorldMapLink = db2File.ReadUInt32("WorldMapLink");
                    areaPOI.PortLocID = db2File.ReadUInt32("PortLocID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.AreaPOIs.Add(areaPOI, packet.TimeSpan);
                    break;
                }
                case DB2Hash.AreaPOIState:
                {
                    AreaPOIState areaPOIState = new AreaPOIState();

                    areaPOIState.ID = db2File.ReadUInt32("Id");

                    areaPOIState.AreaPoiID = db2File.ReadUInt32("AreaPOIID");
                    areaPOIState.State = db2File.ReadUInt32("State");
                    areaPOIState.Icon = db2File.ReadUInt32("Icon");

                    ushort len2 = db2File.ReadUInt16();
                    areaPOIState.Description = db2File.ReadWoWString("Description", len2);

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.AreaPOIStates.Add(areaPOIState, packet.TimeSpan);
                    break;
                }
                case DB2Hash.TaxiNodes:
                {
                    TaxiNodes taxiNodes = new TaxiNodes();

                    taxiNodes.ID = db2File.ReadUInt32("Id");

                    taxiNodes.MapID = db2File.ReadUInt32("MapID");

                    taxiNodes.PosX = db2File.ReadSingle("PosX");
                    taxiNodes.PosY = db2File.ReadSingle("PosY");
                    taxiNodes.PosZ = db2File.ReadSingle("PosZ");

                    short len = db2File.ReadInt16();
                    taxiNodes.Name = db2File.ReadWoWString("Name", len);

                    taxiNodes.MountCreatureID = new uint?[2];
                    for (int i = 0; i < 2; ++i)
                        taxiNodes.MountCreatureID[i] = db2File.ReadUInt32("MountCreatureID", i);

                    taxiNodes.ConditionID = db2File.ReadUInt32("ConditionID");
                    taxiNodes.LearnableIndex = db2File.ReadUInt32("LearnableIndex");
                    taxiNodes.Flags = db2File.ReadUInt32("Flags");

                    taxiNodes.MapOffsetX = db2File.ReadSingle("MapOffsetX");
                    taxiNodes.MapOffsetY = db2File.ReadSingle("MapOffsetY");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.TaxiNodesBag.Add(taxiNodes, packet.TimeSpan);
                    break;
                }
                case DB2Hash.TaxiPathNode:
                {
                    TaxiPathNode taxiPathNode = new TaxiPathNode();

                    taxiPathNode.ID = db2File.ReadUInt32("Id");

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
                        Storage.TaxiPathNodes.Add(taxiPathNode, packet.TimeSpan);
                    break;
                }
                case DB2Hash.TaxiPath:
                {
                    TaxiPath taxiPath = new TaxiPath();

                    taxiPath.ID = db2File.ReadUInt32("Id");

                    taxiPath.From = db2File.ReadUInt32("From");
                    taxiPath.To = db2File.ReadUInt32("To");
                    taxiPath.Cost = db2File.ReadUInt32("Cost");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.TaxiPaths.Add(taxiPath, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Location:
                {
                    Location location = new Location();

                    location.ID = db2File.ReadUInt32("Id");

                    location.LocX = db2File.ReadSingle("LocX");
                    location.LocY = db2File.ReadSingle("LocY");
                    location.LocZ = db2File.ReadSingle("LocZ");

                    location.Rotation = new float?[3];
                    for (int i = 0; i < 3; ++i)
                        location.Rotation[i] = db2File.ReadSingle("Rotation", i);

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.Locations.Add(location, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ChrUpgradeTier:
                {
                    ChrUpgradeTier chrUpgradeTier = new ChrUpgradeTier();

                    chrUpgradeTier.ID = (uint)db2File.ReadEntry("Id").Key;

                    chrUpgradeTier.TierIndex = db2File.ReadUInt32("TierIndex");

                    ushort len = db2File.ReadUInt16();
                    chrUpgradeTier.Name = db2File.ReadWoWString("Name", len);

                    chrUpgradeTier.TalentTier = db2File.ReadUInt32("TalentTier");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ChrUpgradeTiers.Add(chrUpgradeTier, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ChrUpgradeBucket:
                {
                    ChrUpgradeBucket chrUpgradeBucket = new ChrUpgradeBucket();

                    chrUpgradeBucket.ID = (uint)db2File.ReadEntry("Id").Key;

                    chrUpgradeBucket.TierID = db2File.ReadUInt32("TierID");
                    chrUpgradeBucket.SpecializationID = db2File.ReadUInt32("SpecializationID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ChrUpgradeBuckets.Add(chrUpgradeBucket, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ChrUpgradeBucketSpell:
                {
                    ChrUpgradeBucketSpell chrUpgradeBucketSpell = new ChrUpgradeBucketSpell();

                    chrUpgradeBucketSpell.ID = (uint)db2File.ReadEntry("Id").Key;

                    chrUpgradeBucketSpell.BucketID = db2File.ReadUInt32("BucketID");
                    chrUpgradeBucketSpell.SpellID = db2File.ReadUInt32("SpellID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.ChrUpgradeBucketSpells.Add(chrUpgradeBucketSpell, packet.TimeSpan);
                    break;
                }
                case DB2Hash.BattlePetSpecies:
                {
                    BattlePetSpecies battlePetSpecies = new BattlePetSpecies();

                    battlePetSpecies.ID = (uint)db2File.ReadEntry("Id").Key;

                    battlePetSpecies.CreatureID = db2File.ReadUInt32("CreatureID");
                    battlePetSpecies.IconFileID = db2File.ReadUInt32("IconFileID");
                    battlePetSpecies.SummonSpellID = db2File.ReadUInt32("SummonSpellID");
                    battlePetSpecies.PetType = db2File.ReadInt32("PetType");
                    battlePetSpecies.Source = db2File.ReadUInt32("Source");
                    battlePetSpecies.Flags = db2File.ReadUInt32("Flags");

                    ushort len1 = db2File.ReadUInt16();
                    battlePetSpecies.SourceText = db2File.ReadWoWString("SourceText", len1);

                    ushort len2 = db2File.ReadUInt16();
                    battlePetSpecies.Description = db2File.ReadWoWString("Description", len2);

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.BattlePetSpeciesBag.Add(battlePetSpecies, packet.TimeSpan);
                    break;
                }
                case DB2Hash.OverrideSpellData:
                {
                    OverrideSpellData overrideSpellData = new OverrideSpellData();

                    overrideSpellData.ID = db2File.ReadUInt32("Id");

                    overrideSpellData.SpellID = new uint?[10];
                    for (int i = 0; i < 10; ++i)
                        overrideSpellData.SpellID[i] = db2File.ReadUInt32("SpellID", i);

                    overrideSpellData.Flags = db2File.ReadUInt32("Flags");
                    overrideSpellData.PlayerActionbarFileDataID = db2File.ReadUInt32("PlayerActionbarFileDataID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.OverrideSpellDatas.Add(overrideSpellData, packet.TimeSpan);
                    break;
                }
                case DB2Hash.PhaseXPhaseGroup:
                {
                    PhaseXPhaseGroup phaseXPhaseGroup = new PhaseXPhaseGroup();

                    phaseXPhaseGroup.ID = db2File.ReadUInt32("Id");

                    phaseXPhaseGroup.PhaseID = db2File.ReadUInt32("PhaseID");
                    phaseXPhaseGroup.PhaseGroupID = db2File.ReadUInt32("PhaseGroupID");

                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)) && Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data) ||
                        !Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.hotfix_data))
                        Storage.PhaseXPhaseGroups.Add(phaseXPhaseGroup, packet.TimeSpan);
                    break;
                }
                default:
                {
                    db2File.AddValue("Unknown DB2 file type", string.Format("{0} (0x{0:x})", type));
                    for (int i = 0;; ++i)
                    {
                        if (db2File.Length - 4 >= db2File.Position)
                        {
                            UpdateField blockVal = db2File.ReadUpdateField();
                            string key = "Block Value " + i;
                            string value = blockVal.UInt32Value + "/" + blockVal.SingleValue;
                            packet.AddValue(key, value);
                        }
                        else
                        {
                            long left = db2File.Length - db2File.Position;
                            for (int j = 0; j < left; ++j)
                            {
                                string key = "Byte Value " + i;
                                byte value = db2File.ReadByte();
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
                packet.WriteLine(
                    $"Packet not fully read! Current position is {db2File.Position}, length is {db2File.Length}, and diff is {db2File.Length - db2File.Position}.");

                if (db2File.Length < 300) // If the packet isn't "too big" and it is not full read, print its hex table
                    packet.AsHex();

                packet.Status = ParsedStatus.WithErrors;
            }
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY_BLOB)]
        public static void HandleHotfixNotifyBlob(Packet packet)
        {
            uint count = packet.ReadUInt32("HotfixCount");

            for (int i = 0; i < count; ++i)
            {
                HotfixData hotfixData = new HotfixData();

                DB2Hash tableHash = packet.ReadUInt32E<DB2Hash>("TableHash", i);
                int recordID = packet.ReadInt32("RecordID", i);
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
