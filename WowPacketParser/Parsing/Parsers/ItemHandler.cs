using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.SMSG_ITEM_TIME_UPDATE)]
        public static void HandleItemTimeUpdate(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Duration");
        }

        [Parser(Opcode.CMSG_ITEM_NAME_QUERY)]
        public static void HandleItemNameQuery(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_ITEM_NAME_QUERY_RESPONSE)]
        public static void HandleItemNameQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            var name = packet.ReadCString("Name");
            packet.ReadEnum<InventoryType>("Inventory Type", TypeCode.UInt32);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Item,
                Name = name,
            };
            Storage.ObjectNames.Add((uint)entry, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_SOCKET_GEMS)]
        public static void HandleSocketGems(Packet packet)
        {
            packet.ReadGuid("GUID");
            for (var i = 0; i < 3; ++i)
                packet.ReadGuid("Gem GUID", i);
        }

        [Parser(Opcode.SMSG_INVENTORY_CHANGE_FAILURE)]
        public static void HandleInventoryChangeFailure(Packet packet)
        {
            var result = packet.ReadEnum<InventoryResult>("Result", TypeCode.Byte);
            if (result == InventoryResult.Ok)
                return;

            packet.ReadGuid("Item GUID1");
            packet.ReadGuid("Item GUID2");
            packet.ReadSByte("Bag");

            switch (result)
            {
                case InventoryResult.CantEquipLevel:
                case InventoryResult.PurchaseLevelTooLow:
                    packet.ReadUInt32("Required Level");
                    break;
                case InventoryResult.EventAutoEquipBindConfirm:
                    packet.ReadUInt64("Unk UInt64 1");
                    packet.ReadUInt32("Unk UInt32 1");
                    packet.ReadUInt64("Unk UInt64 2");
                    break;
                case InventoryResult.ItemMaxLimitCategoryCountExceeded:
                case InventoryResult.ItemMaxLimitCategorySocketedExceeded:
                case InventoryResult.ItemMaxLimitCategoryEquippedExceeded:
                    packet.ReadUInt32("Limit Category");
                    break;
            }
        }

        [Parser(Opcode.CMSG_USE_ITEM, ClientVersionBuild.Zero, ClientVersionBuild.V3_0_3_9183)]
        public static void HandleUseItem(Packet packet)
        {
            packet.ReadSByte("Bag");
            packet.ReadByte("Slot");
            packet.ReadByte("Spell Count");
            packet.ReadByte("Cast Count");
            packet.ReadGuid("GUID");

            SpellHandler.ReadSpellCastTargets(ref packet);
        }

        [Parser(Opcode.CMSG_USE_ITEM, ClientVersionBuild.V3_0_3_9183)]
        public static void HandleUseItem2(Packet packet)
        {
            packet.ReadSByte("Bag");
            packet.ReadByte("Slot");
            packet.ReadByte("Cast Count");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Glyph Index");
            var castflag = packet.ReadEnum<CastFlag>("Cast Flags", TypeCode.Byte);

            SpellHandler.ReadSpellCastTargets(ref packet);

            if (!castflag.HasAnyFlag(CastFlag.HasTrajectory))
                return;

            packet.ReadSingle("Elevation");
            packet.ReadSingle("Missile speed");

            // Boolean if it will send MSG_MOVE_STOP
            if (!packet.ReadBoolean())
                return;

            var opcode = packet.ReadInt32();
            var remainingLength = packet.Length - packet.Position;
            var bytes = packet.ReadBytes((int)remainingLength);

            using (var newpacket = new Packet(bytes, opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
                Handler.Parse(newpacket, true);
        }

        [Parser(Opcode.CMSG_AUTOSTORE_LOOT_ITEM, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleAutoStoreLootItem(Packet packet)
        {
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_AUTOSTORE_LOOT_ITEM, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleAutoStoreLootItem510(Packet packet)
        {
            var counter = packet.ReadBits("Count", 25);

            var guid = new byte[counter][];

            for (var i = 0; i < counter; ++i)
            {
                guid[i] = new byte[8];

                guid[i][5] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
            }

            packet.ResetBitReader();

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadByte("Slot", i);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(guid[i], 2);

                packet.WriteGuid("Lootee GUID", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_SWAP_INV_ITEM)]
        public static void HandleSwapInventoryItem(Packet packet)
        {
            packet.ReadByte("Slot 1");
            packet.ReadByte("Slot 2");
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("From NPC");
            packet.ReadUInt32("Created");
            packet.ReadUInt32("Unk Uint32");
            packet.ReadByte("Slot");
            packet.ReadInt32("Item Slot");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            packet.ReadInt32("Suffix Factor");
            packet.ReadInt32("Random Property ID");
            packet.ReadUInt32("Count");
            packet.ReadUInt32("Count of Items in inventory");
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            packet.ReadGuid("Item GUID");
            packet.ReadUInt32("Slot");
            packet.ReadUInt32("Duration");
            packet.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_BUYBACK_ITEM)]
        public static void HandleBuyBackItem(Packet packet)
        {
            packet.ReadGuid("Vendor GUID");
            packet.ReadUInt32("Slot");
        }

        [Parser(Opcode.CMSG_ITEM_REFUND_INFO)]
        [Parser(Opcode.SMSG_READ_ITEM_OK)]
        [Parser(Opcode.SMSG_READ_ITEM_FAILED)]
        [Parser(Opcode.CMSG_ITEM_REFUND)]
        public static void HandleReadItem(Packet packet)
        {
            packet.ReadGuid("Item GUID");
        }

        [Parser(Opcode.SMSG_ITEM_REFUND_INFO_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemRefundInfoResponse(Packet packet)
        {
            packet.ReadGuid("Item GUID");
            packet.ReadUInt32("Money Cost");
            packet.ReadUInt32("Honor Cost");
            packet.ReadUInt32("Arena Cost");
            for (var i = 0; i < 5; ++i)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Extended Cost Entry", i);
                packet.ReadUInt32("Extended Cost Count", i);
            }
            packet.ReadUInt32("Unk UInt32 1");
            packet.ReadUInt32("Time Left");
        }

        [Parser(Opcode.SMSG_ITEM_REFUND_INFO_RESPONSE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemRefundInfoResponse434(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 7, 6, 2, 4, 0, 1);

            packet.ReadXORByte(guid, 7);
            packet.ReadUInt32("Time Left");
            for (var i = 0; i < 5; ++i)
            {
                packet.ReadUInt32("Item Count", i);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Cost Entry", i);
            }

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            for (var i = 0; i < 5; ++i)
            {
                packet.ReadUInt32("Currency Count", i);
                packet.ReadUInt32("Currency Entry", i);
            }

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadUInt32("Unk UInt32 1");
            packet.ReadXORByte(guid, 0);
            packet.ReadUInt32("Money Cost");
            packet.WriteGuid("Item Guid", guid);
        }

        [Parser(Opcode.SMSG_ITEM_REFUND_RESULT, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleItemRefundResult(Packet packet)
        {
            packet.ReadGuid("Item Guid");
            packet.ReadInt32("Error ID");
        }

        [Parser(Opcode.SMSG_ITEM_REFUND_RESULT, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleItemRefundResult422(Packet packet)
        {
            var guid = packet.StartBitStream(5, 0, 3, 7, 4, 1, 6, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Error ID");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.WriteGuid("Item Guid", guid);
        }

        [Parser(Opcode.SMSG_ITEM_REFUND_RESULT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemRefundResult434(Packet packet)
        {
            var guid = packet.StartBitStream(4, 5, 1, 6, 7, 0, 3, 2);

            var unkBit = packet.ReadBit("Has Item Refund");
            packet.ReadBit("Has Money Refund");

            if (unkBit)
            {
                for (int i = 0; i < 5; ++i) // Currencies
                {
                    packet.ReadInt32("CurrencyCount", i);
                    packet.ReadInt32("Currency", i);
                }

                packet.ReadInt32("Paid Money");

                for (int i = 0; i < 5; ++i) // Items
                {
                    packet.ReadInt32("ItemCount", i);
                    packet.ReadInt32("Item", i);
                }
            }

            packet.ParseBitStream(guid, 0, 3, 1, 6, 4, 2, 7, 5);
            packet.ReadByte("Error"); // Error Id?
            packet.WriteGuid("Item Guid", guid);
        }

        [Parser(Opcode.CMSG_REPAIR_ITEM)]
        public static void HandleRepairItem(Packet packet)
        {
            packet.ReadGuid("Vendor GUID");
            packet.ReadGuid("Item GUID");
            packet.ReadBoolean("Use guild money");
        }

        [Parser(Opcode.CMSG_SELL_ITEM)]
        public static void HandleSellItem(Packet packet)
        {
            packet.ReadGuid("Vendor GUID");
            packet.ReadGuid("Item GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192)) // not sure when this was changed exactly
                packet.ReadUInt32("Count");
            else
                packet.ReadByte("Count");
        }

        [Parser(Opcode.SMSG_SELL_ITEM)]
        public static void HandleSellItemResponse(Packet packet)
        {
            packet.ReadGuid("Vendor GUID");
            packet.ReadGuid("Item GUID");
            packet.ReadEnum<SellResult>("Sell Result", TypeCode.Byte);
        }

        [Parser(Opcode.CMSG_BUY_ITEM)]
        public static void HandleBuyItem(Packet packet)
        {
            packet.ReadGuid("Vendor GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623)) // not verified
                packet.ReadByte("Type"); // 1 item, 2 currency

            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                packet.ReadUInt32("Slot");
                packet.ReadUInt32("Count");
            }
            else
                packet.ReadByte("Count");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623)) // not verified
                packet.ReadGuid("Bag GUID");

            packet.ReadByte("Bag Slot");
        }

        [Parser(Opcode.SMSG_BUY_ITEM)]
        public static void HandleBuyItemResponse(Packet packet)
        {
            packet.ReadGuid("Vendor GUID");
            packet.ReadUInt32("Slot");
            packet.ReadInt32("New Count");
            packet.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_BUY_FAILED)]
        public static void HandleBuyFailed(Packet packet)
        {
            packet.ReadGuid("Vendor GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadInt32("Param");
            packet.ReadEnum<BuyResult>("Result", TypeCode.Byte);
        }

        [Parser(Opcode.CMSG_BUY_ITEM_IN_SLOT)]
        public static void HandleBuyItemInSlot(Packet packet)
        {
            packet.ReadGuid("Vendor GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            packet.ReadUInt32("Slot");
            packet.ReadUInt32("Count");
            packet.ReadGuid("Bag GUID");
            packet.ReadByte("Bag Slot");
            packet.ReadByte("Count");
        }

        [Parser(Opcode.CMSG_AUTOSTORE_BANK_ITEM)]
        [Parser(Opcode.CMSG_AUTOEQUIP_ITEM)]
        [Parser(Opcode.CMSG_AUTOBANK_ITEM)]
        [Parser(Opcode.CMSG_OPEN_ITEM)]
        [Parser(Opcode.CMSG_READ_ITEM)]
        public static void HandleAutoBankItem(Packet packet)
        {
            packet.ReadSByte("Bag");
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_DESTROY_ITEM)]
        public static void HandleDestroyItem(Packet packet)
        {
            packet.ReadSByte("Bag");
            packet.ReadByte("Slot");
            packet.ReadUInt32("Count");
        }

        [Parser(Opcode.CMSG_SWAP_ITEM)]
        public static void HandleSwapItem(Packet packet)
        {
            packet.ReadSByte("Bag");
            packet.ReadByte("Slot");
            packet.ReadByte("Destination Bag");
            packet.ReadByte("Destination Slot");
        }

        [Parser(Opcode.CMSG_SPLIT_ITEM)]
        public static void HandleSplitItem(Packet packet)
        {
            packet.ReadSByte("Bag");
            packet.ReadByte("Slot");
            packet.ReadByte("Destination Bag");
            packet.ReadByte("Destination Slot");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192)) // not sure when this was changed exactly
                packet.ReadUInt32("Count");
            else
                packet.ReadByte("Count");
        }

        [Parser(Opcode.SMSG_ENCHANTMENTLOG)]
        public static void HandleEnchantmentLog(Packet packet)
        {
            packet.ReadPackedGuid("Target");
            packet.ReadPackedGuid("Caster");
            packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item Entry");
            packet.ReadUInt32("Enchantment ID?");
        }

        [Parser(Opcode.CMSG_ITEM_QUERY_SINGLE)]
        public static void HandleItemQuerySingle(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545)) // Might be earlier
            {
                packet.ReadEnum<UnknownFlags>("Unknown Byte", TypeCode.Byte);
                packet.ReadInt32("Unknown Int32");
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_ITEM_QUERY_SINGLE_RESPONSE)]
        public static void HandleItemQueryResponse(Packet packet)
        {
            var item = new ItemTemplate();

            var entry = packet.ReadEntry("Entry");
            if (entry.Value)
                return;

            item.Class = packet.ReadEnum<ItemClass>("Class", TypeCode.Int32);

            item.SubClass = packet.ReadUInt32("Sub Class");

            item.SoundOverrideSubclass = packet.ReadInt32("Sound Override Subclass");

            var name = new string[4];
            for (var i = 0; i < 4; i++)
                name[i] = packet.ReadCString("Name", i);
            item.Name = name[0];

            item.DisplayId = packet.ReadUInt32("Display ID");

            item.Quality = packet.ReadEnum<ItemQuality>("Quality", TypeCode.Int32);

            item.Flags = packet.ReadEnum<ItemProtoFlags>("Flags", TypeCode.UInt32);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                item.ExtraFlags = packet.ReadEnum<ItemFlagExtra>("Extra Flags", TypeCode.Int32);

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

            item.StatsCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056) ? packet.ReadUInt32("Stats Count") : 10;
            item.StatTypes = new ItemModType[item.StatsCount];
            item.StatValues = new int[item.StatsCount];
            for (var i = 0; i < item.StatsCount; i++)
            {
                var type = packet.ReadEnum<ItemModType>("Stat Type", TypeCode.Int32, i);
                item.StatTypes[i] = type == ItemModType.None ? ItemModType.Mana : type; // TDB
                item.StatValues[i] = packet.ReadInt32("Stat Value", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                item.ScalingStatDistribution = packet.ReadInt32("SSD ID");
                item.ScalingStatValue = packet.ReadUInt32("SSD Value");
            }

            var dmgCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767) ? 2 : 5;
            item.DamageMins = new float[dmgCount];
            item.DamageMaxs = new float[dmgCount];
            item.DamageTypes = new DamageType[dmgCount];
            for (var i = 0; i < dmgCount; i++)
            {
                item.DamageMins[i] = packet.ReadSingle("Damage Min", i);
                item.DamageMaxs[i] = packet.ReadSingle("Damage Max", i);
                item.DamageTypes[i] = packet.ReadEnum<DamageType>("Damage Type", TypeCode.Int32, i);
            }

            item.Resistances = new DamageType[7];
            for (var i = 0; i < 7; i++)
                item.Resistances[i] = packet.ReadEnum<DamageType>("Resistance", TypeCode.UInt32);

            item.Delay = packet.ReadUInt32("Delay");

            item.AmmoType = packet.ReadEnum<AmmoType>("Ammo Type", TypeCode.Int32);

            item.RangedMod = packet.ReadSingle("Ranged Mod");

            item.TriggeredSpellIds = new int[5];
            item.TriggeredSpellTypes = new ItemSpellTriggerType[5];
            item.TriggeredSpellCharges = new int[5];
            item.TriggeredSpellCooldowns = new int[5];
            item.TriggeredSpellCategories = new uint[5];
            item.TriggeredSpellCategoryCooldowns = new int[5];
            for (var i = 0; i < 5; i++)
            {
                item.TriggeredSpellIds[i] = packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Triggered Spell ID", i);
                item.TriggeredSpellTypes[i] = packet.ReadEnum<ItemSpellTriggerType>("Trigger Spell Type", TypeCode.Int32, i);
                item.TriggeredSpellCharges[i] = packet.ReadInt32("Triggered Spell Charges", i);
                item.TriggeredSpellCooldowns[i] = packet.ReadInt32("Triggered Spell Cooldown", i);
                item.TriggeredSpellCategories[i] = packet.ReadUInt32("Triggered Spell Category", i);
                item.TriggeredSpellCategoryCooldowns[i] = packet.ReadInt32("Triggered Spell Category Cooldown", i);
            }

            item.Bonding = packet.ReadEnum<ItemBonding>("Bonding", TypeCode.Int32);

            item.Description = packet.ReadCString();

            item.PageText = packet.ReadUInt32("Page Text");

            item.Language = packet.ReadEnum<Language>("Language", TypeCode.Int32);

            item.PageMaterial = packet.ReadEnum<PageMaterial>("Page Material", TypeCode.Int32);

            item.StartQuestId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Start Quest");

            item.LockId = packet.ReadUInt32("Lock ID");

            item.Material = packet.ReadEnum<Material>("Material", TypeCode.Int32);

            item.SheathType = packet.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);

            item.RandomPropery = packet.ReadInt32("Random Property");

            item.RandomSuffix = packet.ReadUInt32("Random Suffix");

            item.Block = packet.ReadUInt32("Block");

            item.ItemSet = packet.ReadUInt32("Item Set");

            item.MaxDurability = packet.ReadUInt32("Max Durability");

            item.AreaId = (uint) packet.ReadEntryWithName<UInt32>(StoreNameType.Area, "Area");

            // In this single (?) case, map 0 means no map
            item.MapId = packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map");

            item.BagFamily = packet.ReadEnum<BagFamilyMask>("Bag Family", TypeCode.Int32);

            item.TotemCategory = packet.ReadEnum<TotemCategory>("Totem Category", TypeCode.Int32);

            item.ItemSocketColors = new ItemSocketColor[3];
            item.SocketContent = new uint[3];
            for (var i = 0; i < 3; i++)
            {
                item.ItemSocketColors[i] = packet.ReadEnum<ItemSocketColor>("Socket Color", TypeCode.Int32, i);
                item.SocketContent[i] = packet.ReadUInt32("Socket Item", i);
            }

            item.SocketBonus = packet.ReadInt32("Socket Bonus");

            item.GemProperties = packet.ReadInt32("Gem Properties");

            item.RequiredDisenchantSkill = packet.ReadInt32("Required Disenchant Skill");

            item.ArmorDamageModifier = packet.ReadSingle("Armor Damage Modifier");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_4_2_8209))
                item.Duration = packet.ReadUInt32("Duration");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                item.ItemLimitCategory = packet.ReadInt32("Limit Category");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                item.HolidayId = packet.ReadEnum<Holiday>("Holiday", TypeCode.Int32);

            packet.AddSniffData(StoreNameType.Item, entry.Key, "QUERY_RESPONSE");

            Storage.ItemTemplates.Add((uint) entry.Key, item, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_REQUEST_HOTFIX, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemRequestHotfix434(Packet packet)
        {
            packet.ReadUInt32("Type");
            var count = packet.ReadBits("Count", 23);
            var guidBytes = new byte[count][];
            for (var i = 0; i < count; ++i)
                guidBytes[i] = packet.StartBitStream(0, 4, 7, 2, 5, 3, 6, 1);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guidBytes[i], 5);
                packet.ReadXORByte(guidBytes[i], 6);
                packet.ReadXORByte(guidBytes[i], 7);
                packet.ReadXORByte(guidBytes[i], 0);
                packet.ReadXORByte(guidBytes[i], 1);
                packet.ReadXORByte(guidBytes[i], 3);
                packet.ReadXORByte(guidBytes[i], 4);

                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry", i);

                packet.ReadXORByte(guidBytes[i], 2);

                packet.WriteGuid("GUID", guidBytes[i], i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_HOTFIX, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleItemRequestHotfix422(Packet packet)
        {
            packet.ReadUInt32("Type");
            var count = packet.ReadUInt32("Count");
            var guidBytes = new byte[count][];
            for (var i = 0; i < count; ++i)
                guidBytes[i] = packet.StartBitStream(7, 3, 0, 5, 6, 4, 1, 2);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry", i);
                guidBytes[i] = packet.ParseBitStream(guidBytes[i], 2, 6, 3, 0, 5, 7, 1, 4);
                packet.WriteGuid("GUID", guidBytes[i], i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_HOTFIX, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleItemRequestHotFix(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            packet.ReadUInt32("Type");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry", i);
                packet.ReadUInt32("Unk UInt32 1", i);
                packet.ReadUInt32("Unk UInt32 2", i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleDBReply(Packet packet)
        {
            packet.ReadUInt32("Type");
            var itemId = packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadUInt32("Received Type");

            var size = packet.ReadUInt32("Size");
            if (size == 0)
            {
                packet.ReadUInt32("Received Type");
                return;
            }

            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            if (size == 32)
            {
                packet.ReadEnum<ItemClass>("Class", TypeCode.Int32);
                packet.ReadUInt32("Sub Class");
                packet.ReadInt32("Unk Int32");
                packet.ReadEnum<Material>("Material", TypeCode.Int32);
                packet.ReadUInt32("Display ID");
                packet.ReadEnum<InventoryType>("Inventory Type", TypeCode.UInt32);
                packet.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);
            }
            else
            {
                packet.ReadEnum<ItemQuality>("Quality", TypeCode.Int32);
                packet.ReadEnum<ItemProtoFlags>("Flags", TypeCode.UInt32);
                packet.ReadEnum<ItemFlagExtra>("Extra Flags", TypeCode.Int32);
                packet.ReadInt32("Buy Price");
                packet.ReadUInt32("Sell Price");
                packet.ReadEnum<InventoryType>("Inventory Type", TypeCode.Int32);
                packet.ReadEnum<ClassMask>("Allowed Classes", TypeCode.Int32);
                packet.ReadEnum<RaceMask>("Allowed Races", TypeCode.Int32);
                packet.ReadUInt32("Item Level");
                packet.ReadUInt32("Required Level");
                packet.ReadUInt32("Required Skill ID");
                packet.ReadUInt32("Required Skill Level");
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Required Spell");
                packet.ReadUInt32("Required Honor Rank");
                packet.ReadUInt32("Required City Rank");
                packet.ReadUInt32("Required Rep Faction");
                packet.ReadUInt32("Required Rep Value");
                packet.ReadInt32("Max Count");
                packet.ReadInt32("Max Stack Size");
                packet.ReadUInt32("Container Slots");

                for (var i = 0; i < 10; i++)
                    packet.ReadEnum<ItemModType>("Stat Type", TypeCode.Int32, i);

                for (var i = 0; i < 10; i++)
                    packet.ReadInt32("Stat Value", i);

                for (var i = 0; i < 10; i++)
                    packet.ReadInt32("Unk UInt32 1", i);

                for (var i = 0; i < 10; i++)
                    packet.ReadInt32("Unk UInt32 2", i);

                packet.ReadUInt32("Scaling Stat Distribution");
                packet.ReadEnum<DamageType>("Damage Type", TypeCode.Int32);
                packet.ReadUInt32("Delay");
                packet.ReadSingle("Ranged Mod");

                for (var i = 0; i < 5; i++)
                    packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Triggered Spell ID", i);

                for (var i = 0; i < 5; i++)
                    packet.ReadEnum<ItemSpellTriggerType>("Trigger Spell Type", TypeCode.Int32, i);

                for (var i = 0; i < 5; i++)
                    packet.ReadInt32("Triggered Spell Charges", i);

                for (var i = 0; i < 5; i++)
                    packet.ReadInt32("Triggered Spell Cooldown", i);

                for (var i = 0; i < 5; i++)
                    packet.ReadUInt32("Triggered Spell Category", i);

                for (var i = 0; i < 5; i++)
                    packet.ReadInt32("Triggered Spell Category Cooldown", i);

                packet.ReadEnum<ItemBonding>("Bonding", TypeCode.Int32);

                for (var i = 0; i < 4; i++)
                    if (packet.ReadUInt16() > 0)
                        packet.ReadCString("Name", i);

                if (packet.ReadUInt16() > 0)
                    packet.ReadCString("Description");

                packet.ReadUInt32("Page Text");
                packet.ReadEnum<Language>("Language", TypeCode.Int32);
                packet.ReadEnum<PageMaterial>("Page Material", TypeCode.Int32);
                packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Start Quest");
                packet.ReadUInt32("Lock ID");
                packet.ReadEnum<Material>("Material", TypeCode.Int32);
                packet.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);
                packet.ReadInt32("Random Property");
                packet.ReadInt32("Random Suffix");
                packet.ReadUInt32("Item Set");
                packet.ReadUInt32("Max Durability");
                packet.ReadEntryWithName<UInt32>(StoreNameType.Area, "Area");
                // In this single (?) case, map 0 means no map
                var map = packet.ReadUInt32();
                packet.WriteLine("Map ID: " + (map != 0 ? StoreGetters.GetName(StoreNameType.Map, (int) map) : map + " (No map)"));
                packet.ReadEnum<BagFamilyMask>("Bag Family", TypeCode.Int32);
                packet.ReadEnum<TotemCategory>("Totem Category", TypeCode.Int32);

                for (var i = 0; i < 3; i++)
                    packet.ReadEnum<ItemSocketColor>("Socket Color", TypeCode.Int32, i);

                for (var i = 0; i < 3; i++)
                    packet.ReadUInt32("Socket Item", i);

                packet.ReadUInt32("Socket Bonus");
                packet.ReadUInt32("Gem Properties");
                packet.ReadSingle("Armor Damage Modifier");
                packet.ReadInt32("Duration");
                packet.ReadInt32("Limit Category");
                packet.ReadEnum<Holiday>("Holiday", TypeCode.Int32);
                packet.ReadSingle("Stat Scaling Factor");
                packet.ReadUInt32("Unk UInt32 1");
                packet.ReadUInt32("Unk UInt32 2");
            }

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadUInt32("Received Type");

            packet.AddSniffData(StoreNameType.Item, itemId, "DB_REPLY");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleDBReply430(Packet packet)
        {
            var size = packet.ReadUInt32("Size");
            if (size == 0)
                return;

            if (size == 32)
            {
                var itemId2 = packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
                var item = Storage.ItemTemplates.ContainsKey((uint) itemId2) ? Storage.ItemTemplates[(uint) itemId2].Item1 : new ItemTemplate();
                item.Class = packet.ReadEnum<ItemClass>("Class", TypeCode.Int32);
                item.SubClass = packet.ReadUInt32("Sub Class");
                item.SoundOverrideSubclass = packet.ReadInt32("Sound Override Subclass");
                item.Material = packet.ReadEnum<Material>("Material", TypeCode.Int32);
                packet.ReadUInt32("Unk");
                item.InventoryType = packet.ReadEnum<InventoryType>("Inventory Type", TypeCode.UInt32);
                item.SheathType = packet.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);

                Storage.ItemTemplates.Add((uint)itemId2, item, packet.TimeSpan);
            }
            else if (size == 36)
            {
                packet.ReadUInt32("Unk");
                packet.ReadUInt32("Unk");
                packet.ReadUInt32("Unk");
                packet.ReadUInt32("Unk");
                packet.ReadUInt32("Unk");
                packet.ReadUInt32("Unk");
                packet.ReadUInt32("Unk");
                packet.ReadUInt32("Unk");
                packet.ReadUInt32("Unk");
            }
            else
            {
                var itemId2 = packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
                var item = Storage.ItemTemplates.ContainsKey((uint)itemId2) ? Storage.ItemTemplates[(uint)itemId2].Item1 : new ItemTemplate();

                item.Quality = packet.ReadEnum<ItemQuality>("Quality", TypeCode.Int32);
                item.Flags = packet.ReadEnum<ItemProtoFlags>("Flags", TypeCode.UInt32);
                item.ExtraFlags = packet.ReadEnum<ItemFlagExtra>("Extra Flags", TypeCode.Int32);
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

                Storage.ObjectNames.Add((uint)itemId2, new ObjectName { ObjectType = ObjectType.Item, Name = item.Name }, packet.TimeSpan);
            }

            packet.ReadUInt32("Type");
            packet.ReadTime("Hotfix date");
            var itemId = (uint)packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");

            packet.AddSniffData(StoreNameType.Item, (int)itemId, "DB_REPLY");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleDBReply434(Packet packet)
        {
            var id = packet.ReadInt32("Entry");
            var type = packet.ReadUInt32("Type");
            packet.ReadTime("Hotfix date");
            var size = packet.ReadUInt32("Size");
            if (size == 0)
                return;

            if (id < 0)
                return;

            var itemId = (uint)id;

            switch (type)
            {
                case 0x50238EC2:    // Item
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
                case 0x919BE54E:    // Item-sparse
                {
                    var item = Storage.ItemTemplates.ContainsKey(itemId) ? Storage.ItemTemplates[itemId].Item1 : new ItemTemplate();

                    packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
                    item.Quality = packet.ReadEnum<ItemQuality>("Quality", TypeCode.Int32);
                    item.Flags = packet.ReadEnum<ItemProtoFlags>("Flags", TypeCode.UInt32);
                    item.ExtraFlags = packet.ReadEnum<ItemFlagExtra>("Extra Flags", TypeCode.Int32);
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

                    Storage.ObjectNames.Add(itemId, new ObjectName { ObjectType = ObjectType.Item, Name = item.Name }, packet.TimeSpan);
                    break;
                }
                case 0x6D8A2694: // KeyChain
                {
                    packet.ReadUInt32("Key Chain Id");
                    packet.WriteLine("Key: {0}", Utilities.ByteArrayToHexString(packet.ReadBytes(32)));
                    break;
                }
            }

            packet.AddSniffData(StoreNameType.Item, (int)itemId, "DB_REPLY");
        }

        [Parser(Opcode.SMSG_UPDATE_ITEM_ENCHANTMENTS)]
        public static void HandleUpdateItemEnchantments(Packet packet)
        {
            packet.ReadGuid("Item Guid");

            for (var i = 0; i < 4; i++)
                packet.ReadInt32("Aura ID", i);
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.ReadEnum<ItemClass>("Class", TypeCode.Byte);
            packet.ReadEnum<UnknownFlags>("Mask",TypeCode.UInt32);
        }

        [Parser(Opcode.SMSG_ITEM_COOLDOWN)]
        public static void HandleItemCooldown(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.CMSG_REFORGE_ITEM, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemSendReforge(Packet packet)
        {
            packet.ReadInt32("Reforge Entry");
            packet.ReadInt32("Slot");
            packet.ReadInt32("Bag");

            var guid = packet.StartBitStream(2,6,3,4,1,0,7,5);
            packet.ParseBitStream(guid,2,3,6,4,1,0,7,5);
            packet.WriteGuid("Reforger Guid", guid);

        }

        [Parser(Opcode.SMSG_REFORGE_RESULT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemReforgeResult(Packet packet)
        {
            packet.ReadBit("Successful");
        }

        [Parser(Opcode.SMSG_ITEM_TEXT_QUERY_RESPONSE)]
        public static void HandleItemTextQueryResult(Packet packet)
        {
            if (!packet.ReadBoolean("Empty"))
            {
                packet.ReadGuid("Item Guid");
                packet.ReadCString("Item Text");
            }
        }

        [Parser(Opcode.CMSG_ITEM_TEXT_QUERY)]
        public static void HandleItemTextQuery(Packet packet)
        {
            packet.ReadGuid("Item Guid");
        }

        [Parser(Opcode.CMSG_TRANSMOGRIFY_ITEMS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTransmogrifyITems(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);

            var itemGuids = new byte[count][];

            for (int i = 0; i < count; ++i)
                itemGuids[i] = packet.StartBitStream(0, 5, 6, 2, 3, 7, 4, 1);

            var npcGuid = packet.StartBitStream(7, 3, 5, 6, 1, 4, 0, 2);

            // flush bits

            for (int i = 0; i < count; ++i)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "New Entry", i);

                packet.ParseBitStream(itemGuids[i], 1, 5, 0, 4, 6, 7, 3, 2);

                packet.ReadEnum<EquipmentSlotType>("Slot", TypeCode.UInt32, i);

                packet.WriteGuid("ITem Guid", itemGuids[i], i);
            }

            packet.ParseBitStream(npcGuid, 7, 2, 5, 4, 3, 1, 6, 0);
            packet.WriteGuid("NPC Guid", npcGuid);
        }

        [Parser(Opcode.SMSG_ITEM_EXPIRE_PURCHASE_REFUND)]
        public static void HandleItemExpirePurchaseRefund(Packet packet)
        {
            var guid = packet.StartBitStream(3, 0, 5, 4, 6, 2, 1, 7);
            packet.ParseBitStream(guid, 1, 0, 3, 4, 7, 6, 5, 2);
            packet.WriteGuid("Guid", guid);
        }
    }
}
