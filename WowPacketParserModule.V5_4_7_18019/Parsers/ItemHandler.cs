using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_7_18019.Enums;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.CMSG_AUTOBANK_ITEM)]
        [Parser(Opcode.CMSG_AUTOSTORE_BANK_ITEM)]
        [Parser(Opcode.CMSG_OPEN_ITEM)]
        [Parser(Opcode.CMSG_READ_ITEM)]
        public static void HandleReadItem(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadSByte("Bag");
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_AUTOEQUIP_ITEM)]
        public static void HandleAutoEquipItem(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadSByte("Bag");

            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_AUTOSTORE_BAG_ITEM)]
        public static void HandleAutoStoreBagItem(Packet packet)
        {
            packet.ReadSByte("SrcBag");
            packet.ReadSByte("DstBag");
            packet.ReadByte("SrcSlot");
            packet.ReadByte("unk");
        }

        [Parser(Opcode.CMSG_AUTOSTORE_LOOT_ITEM)]
        public static void HandleAutoStoreLootItem547(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var counter = packet.ReadBits("Count", 23);

                var guid = new byte[counter][];

                for (var i = 0; i < counter; ++i)
                {
                    guid[i] = new byte[8];

                    guid[i][2] = packet.ReadBit();
                    guid[i][1] = packet.ReadBit();
                    guid[i][5] = packet.ReadBit();
                    guid[i][7] = packet.ReadBit();
                    guid[i][4] = packet.ReadBit();
                    guid[i][3] = packet.ReadBit();
                    guid[i][0] = packet.ReadBit();
                    guid[i][6] = packet.ReadBit();
                }

                packet.ResetBitReader();

                for (var i = 0; i < counter; ++i)
                {
                    packet.ReadXORByte(guid[i], 0);
                    packet.ReadXORByte(guid[i], 3);
                    packet.ReadByte("Slot", i);
                    packet.ReadXORByte(guid[i], 7);
                    packet.ReadXORByte(guid[i], 2);
                    packet.ReadXORByte(guid[i], 4);
                    packet.ReadXORByte(guid[i], 1);
                    packet.ReadXORByte(guid[i], 6);
                    packet.ReadXORByte(guid[i], 5);

                    packet.WriteGuid("Lootee GUID", guid[i], i);
                }
            }
            else
            {
                packet.WriteLine("              : SMSG_???");
                //packet.Opcode = (int)Opcode.CMSG_MOUNTSPECIAL_ANIM;
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_BUY_ITEM)]
        public static void HandleBuyItem(Packet packet)
        {
            packet.ReadUInt32("Bag Slot");
            packet.ReadUInt32("Item Entry");
            packet.ReadUInt32("Item Count");
            packet.ReadUInt32("Vendor Slot");

            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            packet.ReadBits("Item Type", 2);

            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[6] = packet.ReadBit();

            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 7);

            packet.WriteGuid("Vendor Guid", guid1);
            packet.WriteGuid("Bag Guid", guid2);
        }

        [Parser(Opcode.CMSG_BUYBACK_ITEM)]
        public static void HandleBuyBackItem(Packet packet)
        {
            packet.ReadUInt32("Slot");
            var guid = packet.StartBitStream(3, 5, 0, 7, 2, 6, 1, 4);
            packet.ParseBitStream(guid, 1, 7, 6, 0, 5, 3, 4, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REPAIR_ITEM)]
        public static void HandleRepairItem(Packet packet)
        {
            var npcGUID = new byte[8];
            var itemGUID = new byte[8];
            bool guildBank;                                         // new in 2.3.2, bool that means from guild bank money

            npcGUID[3] = packet.ReadBit();
            itemGUID[3] = packet.ReadBit();
            itemGUID[6] = packet.ReadBit();
            guildBank = packet.ReadBit();
            npcGUID[2] = packet.ReadBit();
            itemGUID[0] = packet.ReadBit();
            npcGUID[7] = packet.ReadBit();
            npcGUID[6] = packet.ReadBit();
            npcGUID[0] = packet.ReadBit();
            itemGUID[7] = packet.ReadBit();
            itemGUID[4] = packet.ReadBit();
            npcGUID[5] = packet.ReadBit();
            itemGUID[5] = packet.ReadBit();
            npcGUID[4] = packet.ReadBit();
            itemGUID[2] = packet.ReadBit();
            npcGUID[1] = packet.ReadBit();
            itemGUID[1] = packet.ReadBit();

            packet.ReadXORByte(itemGUID, 6);
            packet.ReadXORByte(npcGUID, 1);
            packet.ReadXORByte(npcGUID, 0);
            packet.ReadXORByte(npcGUID, 2);
            packet.ReadXORByte(itemGUID, 2);
            packet.ReadXORByte(itemGUID, 5);
            packet.ReadXORByte(itemGUID, 1);
            packet.ReadXORByte(npcGUID, 6);
            packet.ReadXORByte(npcGUID, 7);
            packet.ReadXORByte(itemGUID, 3);
            packet.ReadXORByte(itemGUID, 7);
            packet.ReadXORByte(npcGUID, 3);
            packet.ReadXORByte(npcGUID, 4);
            packet.ReadXORByte(npcGUID, 5);
            packet.ReadXORByte(itemGUID, 0);
            packet.ReadXORByte(itemGUID, 4);

            packet.WriteGuid("Item Guid", itemGUID);
            packet.WriteGuid("NPC Guid", npcGUID);
        }

        [Parser(Opcode.CMSG_SELL_ITEM)]
        public static void HandleSellItem(Packet packet)
        {
            packet.ReadUInt32("Count");

            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[1] = packet.ReadBit();

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 2);

            packet.WriteGuid("Item Guid", guid1);
            packet.WriteGuid("Vendor Guid", guid2);
        }

        [Parser(Opcode.CMSG_REQUEST_HOTFIX)]
        public static void HandleItemRequestHotfix(Packet packet)
        {
            packet.ReadUInt32("Type");

            var count = packet.ReadBits("Count", 21);

            var guidBytes = new byte[count][];

            for (var i = 0; i < count; ++i)
                guidBytes[i] = packet.StartBitStream(2, 4, 3, 6, 7, 1, 5, 0);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guidBytes[i], 5);
                packet.ReadXORByte(guidBytes[i], 4);
                packet.ReadXORByte(guidBytes[i], 3);

                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry", i);

                packet.ReadXORByte(guidBytes[i], 7);
                packet.ReadXORByte(guidBytes[i], 0);
                packet.ReadXORByte(guidBytes[i], 2);
                packet.ReadXORByte(guidBytes[i], 1);
                packet.ReadXORByte(guidBytes[i], 6);

                packet.WriteGuid("GUID", guidBytes[i], i);
            }
        }

        [Parser(Opcode.CMSG_SPLIT_ITEM)]
        public static void HandleSplitItem(Packet packet)
        {
            packet.ReadInt32("Count");
            packet.ReadByte("SrcSlot");
            packet.ReadByte("DstSlot");
            packet.ReadByte("DstBag");
            packet.ReadByte("SrcBag");
        }

        [Parser(Opcode.CMSG_SWAP_ITEM)]
        public static void HandleSwapItem(Packet packet)
        {
            packet.ReadByte("SrcSlotAlt");
            packet.ReadByte("DstSlotAlt");

            var count = packet.ReadBits("Item Count", 2);

            var hasSlot = new Bit[count];

            for (var i = 0; i < count; ++i )
            {
                hasSlot[i] = !packet.ReadBit("hasSlot", i);
                packet.ReadBit("unkb", i);
            }

            for (var i = 0; i < count; ++i)
            {
                if (hasSlot[i])
                    packet.ReadByte("Slot", i);
                packet.ReadByte("Bag", i);
            }
        }

        [Parser(Opcode.CMSG_TRANSMOGRIFY_ITEMS)]
        public static void HandleTransmogrifyItems(Packet packet)
        {
            var npcGUID = new byte[8];

            npcGUID[3] = packet.ReadBit();
            npcGUID[2] = packet.ReadBit();
            npcGUID[4] = packet.ReadBit();
            npcGUID[5] = packet.ReadBit();
            npcGUID[1] = packet.ReadBit();
            npcGUID[0] = packet.ReadBit();
            var count = packet.ReadBits("Count", 21);
            npcGUID[7] = packet.ReadBit();
            npcGUID[6] = packet.ReadBit();

            var itemGUID = new byte[count][];
            var unk0 = new Bit[count];
            var unk1 = new Bit[count];

            for (var i = 0; i < count; ++i)
            {
                unk0[i] = packet.ReadBit("unk0", i);
                unk1[i] = packet.ReadBit("unk1", i);

                if (unk1[i])
                {
                    itemGUID[i] = packet.StartBitStream(5, 6, 4, 0, 7, 3, 1, 2);
                }
                if (unk0[i])
                {
                    itemGUID[i] = packet.StartBitStream(3, 6, 4, 0, 1, 7, 5, 2);
                }
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("NewEntries");
                packet.ReadInt32("Slots");
            }

            packet.ParseBitStream(npcGUID, 5, 4, 1, 0, 2, 7, 6, 3);

            packet.WriteGuid("npcGuid", npcGUID);

            for (var i = 0; i < count; ++i)
            {
                if (unk1[i])
                {
                    packet.ParseBitStream(itemGUID[i], 4, 0, 5, 6, 2, 7, 1, 3);
                }
                if (unk0[i])
                {
                    packet.ParseBitStream(itemGUID[i], 3, 6, 2, 7, 4, 5, 0, 1);
                }
                packet.WriteGuid("Guid", itemGUID[i], i);
            }
        }


        [Parser(Opcode.CMSG_USE_ITEM)]
        public static void HandleUseItem2(Packet packet)
        {
            packet.ReadToEnd();
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var id = packet.ReadUInt32("Entry");
            var type = packet.ReadUInt32("Type"); // See DB2Hash enum. Left like this for now to see some numbers pop. ^^
            packet.ReadTime("Hotfix date");
            var size = packet.ReadUInt32("Size");

            if (size == 0 || id < 0)
                return;

            var HashType = (DB2Hash)type;
            var itemId = (uint)id;

            switch (HashType)
            {
                case DB2Hash.Item:    // Items
                {
                    var item = Storage.ItemTemplates.ContainsKey(itemId) ? Storage.ItemTemplates[itemId].Item1 : new ItemTemplate();

                    packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
                    item.Class = packet.ReadEnum<ItemClass>("Class", TypeCode.Int32);
                    item.SubClass = packet.ReadUInt32("Sub Class");
                    item.SoundOverrideSubclass = packet.ReadInt32("Sound Override Subclass");
                    item.Material = packet.ReadEnum<Material>("Material", TypeCode.Int32);
                    item.DisplayId = packet.ReadUInt32("Display ID");
                    item.InventoryType = packet.ReadEnum<InventoryType>("Inventory Type", TypeCode.UInt32);
                    item.SheathType = packet.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);

                    Storage.ItemTemplates.Add(itemId, item, packet.TimeSpan);
                    break;
                }
                case DB2Hash.Item_sparse:    // Item-sparse
                {
                    var item = Storage.ItemTemplates.ContainsKey(itemId) ? Storage.ItemTemplates[itemId].Item1 : new ItemTemplate();

                    packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
                    item.Quality = packet.ReadEnum<ItemQuality>("Quality", TypeCode.Int32);
                    item.Flags1 = packet.ReadEnum<ItemProtoFlags>("Flags", TypeCode.UInt32);
                    item.Flags2 = packet.ReadEnum<ItemFlagExtra>("Extra Flags", TypeCode.Int32);
                    item.Unk430_1 = packet.ReadSingle("Unk430_1");
                    item.Unk430_2 = packet.ReadSingle("Unk430_2");
                    item.BuyCount = packet.ReadUInt32("Buy count");
                    item.BuyPrice = packet.ReadUInt32("Buy Price");
                    item.SellPrice = packet.ReadUInt32("Sell Price");
                    item.InventoryType = packet.ReadEnum<InventoryType>("Inventory Type", TypeCode.Int32);
                    item.AllowedClasses = packet.ReadEnum<ClassMask>("Allowed Classes", TypeCode.Int32);
                    item.AllowedRaces = packet.ReadEnum<RaceMask>("Allowed Races", TypeCode.Int32);
                    item.ItemLevel = packet.ReadUInt32("Item Level");
                    item.RequiredLevel = packet.ReadUInt32("Required Level");
                    item.RequiredSkillId = packet.ReadUInt32("Required Skill ID");
                    item.RequiredSkillLevel = packet.ReadUInt32("Required Skill Level");
                    item.RequiredSpell = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Required Spell");
                    item.RequiredHonorRank = packet.ReadUInt32("Required Honor Rank");
                    item.RequiredCityRank = packet.ReadUInt32("Required City Rank");
                    item.RequiredRepFaction = packet.ReadUInt32("Required Rep Faction");
                    item.RequiredRepValue = packet.ReadUInt32("Required Rep Value");
                    item.MaxCount = packet.ReadInt32("Max Count");
                    item.MaxStackSize = packet.ReadInt32("Max Stack Size");
                    item.ContainerSlots = packet.ReadUInt32("Container Slots");

                    item.StatTypes = new ItemModType[10];
                    for (var i = 0; i < 10; i++)
                    {
                        var statType = packet.ReadEnum<ItemModType>("Stat Type", TypeCode.Int32, i);
                        item.StatTypes[i] = statType == ItemModType.None ? ItemModType.Mana : statType; // TDB
                    }

                    item.StatValues = new int[10];
                    for (var i = 0; i < 10; i++)
                        item.StatValues[i] = packet.ReadInt32("Stat Value", i);

                    item.StatUnk1 = new int[10];
                    for (var i = 0; i < 10; i++)
                        item.StatUnk1[i] = packet.ReadInt32("Unk UInt32 1", i);

                    item.StatUnk2 = new int[10];
                    for (var i = 0; i < 10; i++)
                        item.StatUnk2[i] = packet.ReadInt32("Unk UInt32 2", i);

                    item.ScalingStatDistribution = packet.ReadInt32("Scaling Stat Distribution");
                    item.DamageType = packet.ReadEnum<DamageType>("Damage Type", TypeCode.Int32);
                    item.Delay = packet.ReadUInt32("Delay");
                    item.RangedMod = packet.ReadSingle("Ranged Mod");

                    item.TriggeredSpellIds = new int[5];
                    for (var i = 0; i < 5; i++)
                        item.TriggeredSpellIds[i] = packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Triggered Spell ID", i);

                    item.TriggeredSpellTypes = new ItemSpellTriggerType[5];
                    for (var i = 0; i < 5; i++)
                        item.TriggeredSpellTypes[i] = packet.ReadEnum<ItemSpellTriggerType>("Trigger Spell Type", TypeCode.Int32, i);

                    item.TriggeredSpellCharges = new int[5];
                    for (var i = 0; i < 5; i++)
                        item.TriggeredSpellCharges[i] = packet.ReadInt32("Triggered Spell Charges", i);

                    item.TriggeredSpellCooldowns = new int[5];
                    for (var i = 0; i < 5; i++)
                        item.TriggeredSpellCooldowns[i] = packet.ReadInt32("Triggered Spell Cooldown", i);

                    item.TriggeredSpellCategories = new uint[5];
                    for (var i = 0; i < 5; i++)
                        item.TriggeredSpellCategories[i] = packet.ReadUInt32("Triggered Spell Category", i);

                    item.TriggeredSpellCategoryCooldowns = new int[5];
                    for (var i = 0; i < 5; i++)
                        item.TriggeredSpellCategoryCooldowns[i] = packet.ReadInt32("Triggered Spell Category Cooldown", i);

                    item.Bonding = packet.ReadEnum<ItemBonding>("Bonding", TypeCode.Int32);

                    if (packet.ReadUInt16() > 0)
                        item.Name = packet.ReadCString("Name", 0);

                    for (var i = 1; i < 4; ++i)
                        if (packet.ReadUInt16() > 0)
                            packet.ReadCString("Name", i);

                    if (packet.ReadUInt16() > 0)
                        item.Description = packet.ReadCString("Description");

                    item.PageText = packet.ReadUInt32("Page Text");
                    item.Language = packet.ReadEnum<Language>("Language", TypeCode.Int32);
                    item.PageMaterial = packet.ReadEnum<PageMaterial>("Page Material", TypeCode.Int32);
                    item.StartQuestId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Start Quest");
                    item.LockId = packet.ReadUInt32("Lock ID");
                    item.Material = packet.ReadEnum<Material>("Material", TypeCode.Int32);
                    item.SheathType = packet.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);
                    item.RandomPropery = packet.ReadInt32("Random Property");
                    item.RandomSuffix = packet.ReadUInt32("Random Suffix");
                    item.ItemSet = packet.ReadUInt32("Item Set");
                    item.AreaId = (uint)packet.ReadEntryWithName<UInt32>(StoreNameType.Area, "Area");
                    // In this single (?) case, map 0 means no map
                    var map = packet.ReadInt32();
                    item.MapId = map;
                    packet.WriteLine("Map ID: " + (map != 0 ? StoreGetters.GetName(StoreNameType.Map, map) : map + " (No map)"));
                    item.BagFamily = packet.ReadEnum<BagFamilyMask>("Bag Family", TypeCode.Int32);
                    item.TotemCategory = packet.ReadEnum<TotemCategory>("Totem Category", TypeCode.Int32);

                    item.ItemSocketColors = new ItemSocketColor[3];
                    for (var i = 0; i < 3; i++)
                        item.ItemSocketColors[i] = packet.ReadEnum<ItemSocketColor>("Socket Color", TypeCode.Int32, i);

                    item.SocketContent = new uint[3];
                    for (var i = 0; i < 3; i++)
                        item.SocketContent[i] = packet.ReadUInt32("Socket Item", i);

                    item.SocketBonus = packet.ReadInt32("Socket Bonus");
                    item.GemProperties = packet.ReadInt32("Gem Properties");
                    item.ArmorDamageModifier = packet.ReadSingle("Armor Damage Modifier");
                    item.Duration = packet.ReadUInt32("Duration");
                    item.ItemLimitCategory = packet.ReadInt32("Limit Category");
                    item.HolidayId = packet.ReadEnum<Holiday>("Holiday", TypeCode.Int32);
                    item.StatScalingFactor = packet.ReadSingle("Stat Scaling Factor");
                    item.CurrencySubstitutionId = packet.ReadUInt32("Currency Substitution Id");
                    item.CurrencySubstitutionCount = packet.ReadUInt32("Currency Substitution Count");
                    packet.ReadToEnd();
                    Storage.ObjectNames.Add(itemId, new ObjectName { ObjectType = ObjectType.Item, Name = item.Name }, packet.TimeSpan);
                    break;
                }
                case DB2Hash.KeyChain: // KeyChain
                {
                    packet.ReadUInt32("Key Chain Id");
                    packet.WriteLine("Key: {0}", Utilities.ByteArrayToHexString(packet.ReadBytes(32)));
                    break;
                }

                // Cases need correction, the other DB2's need implementation etc.
                default:
                    packet.ReadToEnd();
                    break;
            }

            if (HashType == DB2Hash.Item || HashType == DB2Hash.Item_sparse) // Add item data.
                packet.AddSniffData(StoreNameType.Item, (int)itemId, "DB_REPLY");
        }

        [Parser(Opcode.SMSG_BUY_ITEM)]
        public static void HandleBuyItemResponse(Packet packet)
        {
            var guid = packet.StartBitStream(7, 0, 6, 1, 5, 2, 4, 3);
            packet.ParseBitStream(guid, 1, 5, 2, 3);
            packet.ReadInt32("VendorSlot");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadInt32("Count");
            packet.ReadXORByte(guid, 7);
            packet.ReadInt32("NewCount");
            packet.ReadXORByte(guid, 4);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_INVENTORY_CHANGE_FAILURE)]
        public static void HandleInventoryChangeFailure(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            var playerGuid = new byte[8];
            var unkGuid = new byte[8];

            packet.ReadBit("Display");
            packet.ReadBit("Created");
            playerGuid[2] = packet.ReadBit();
            playerGuid[0] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();
            unkGuid[3] = packet.ReadBit();
            unkGuid[7] = packet.ReadBit();
            unkGuid[1] = packet.ReadBit();
            unkGuid[4] = packet.ReadBit();
            unkGuid[6] = packet.ReadBit();
            packet.ReadBit("Bonus");
            playerGuid[5] = packet.ReadBit();
            playerGuid[1] = packet.ReadBit();
            unkGuid[5] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();
            unkGuid[2] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            unkGuid[0] = packet.ReadBit();
            playerGuid[3] = packet.ReadBit();
            packet.ReadBit("rcvFrom");

            packet.ReadXORByte(unkGuid, 6);
            packet.ReadInt32("Suffix Factor");
            packet.ReadXORByte(playerGuid, 1);
            packet.ReadInt32("unk1");
            packet.ReadUInt32("Count");
            packet.ReadInt32("unk2");
            packet.ReadInt32("Random Property ID");
            packet.ReadXORByte(playerGuid, 3);
            packet.ReadXORByte(unkGuid, 7);
            packet.ReadXORByte(playerGuid, 5);
            packet.ReadInt32("unk3");
            packet.ReadXORByte(playerGuid, 2);
            packet.ReadXORByte(unkGuid, 0);
            packet.ReadXORByte(unkGuid, 1);
            packet.ReadXORByte(playerGuid, 7);
            packet.ReadByte("Slot");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            packet.ReadInt32("unk4");
            packet.ReadXORByte(playerGuid, 0);
            packet.ReadXORByte(playerGuid, 4);
            packet.ReadXORByte(unkGuid, 5);
            packet.ReadXORByte(unkGuid, 2);
            packet.ReadInt32("Count in inventory");
            packet.ReadInt32("Item Slot");
            packet.ReadXORByte(playerGuid, 6);
            packet.ReadXORByte(unkGuid, 3);
            packet.ReadXORByte(unkGuid, 4);

            packet.WriteGuid("playerGuid", playerGuid);
            packet.WriteGuid("unkGuid", unkGuid);
        }
    }
}
