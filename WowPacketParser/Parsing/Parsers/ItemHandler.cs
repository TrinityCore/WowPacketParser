using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
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
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            packet.ReadCString("Name");
            packet.ReadEnum<InventoryType>("Inventory Type", TypeCode.UInt32);
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

        [Parser(Opcode.CMSG_USE_ITEM)]
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
            packet.ReadByte("CastFlags");

            SpellHandler.ReadSpellCastTargets(ref packet);
        }

        [Parser(Opcode.CMSG_AUTOSTORE_LOOT_ITEM)]
        public static void HandleAutoStoreLootItem(Packet packet)
        {
            packet.ReadByte("Slot");
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
        public static void HandleReadItem(Packet packet)
        {
            packet.ReadGuid("Item GUID");
        }

        [Parser(Opcode.SMSG_ITEM_REFUND_INFO_RESPONSE)]
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
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                packet.ReadUInt32("Slot");
                packet.ReadUInt32("Count");
            }
            else
                packet.ReadByte("Count");

            packet.ReadByte("Unk");
        }

        [Parser(Opcode.SMSG_BUY_ITEM)]
        public static void HandleBuyItemResponse(Packet packet)
        {
            packet.ReadGuid("Vendor GUID");
            packet.ReadUInt32("Slot");
            packet.ReadInt32("New Count");
            packet.ReadUInt32("Count");
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

        [Parser(Opcode.CMSG_DESTROYITEM)]
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

        [Parser(Opcode.SMSG_ITEM_QUERY_SINGLE_RESPONSE)]
        public static void HandleItemQueryResponse(Packet packet)
        {
            var item = new ItemTemplate();

            var entry = packet.ReadEntry("Entry");
            if (entry.Value)
                return;

            item.Class = packet.ReadEnum<ItemClass>("Class", TypeCode.Int32);

            item.SubClass = packet.ReadUInt32("Sub Class");

            item.UnkInt32 = packet.ReadInt32("Unk Int32");

            var name = new string[4];
            for (var i = 0; i < 4; i++)
                name[i] = packet.ReadCString("Name", i);
            item.Name = name[0];

            item.DisplayId = packet.ReadUInt32("Display ID");

            item.Quality = packet.ReadEnum<ItemQuality>("Quality", TypeCode.Int32);

            item.Flags = packet.ReadEnum<ItemFlag>("Flags", TypeCode.Int32);

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

            item.MaxCount = packet.ReadUInt32("Max Count");

            item.MaxStackSize = packet.ReadUInt32("Max Stack Size");

            item.ContainerSlots = packet.ReadUInt32("Container Slots");

            item.StatsCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056) ? packet.ReadUInt32("Stats Count") : 10;
            item.StatTypes = new ItemModType[item.StatsCount];
            item.StatValues = new int[item.StatsCount];
            for (var i = 0; i < item.StatsCount; i++)
            {
                item.StatTypes[i] = packet.ReadEnum<ItemModType>("Stat Type", TypeCode.Int32, i);
                item.StatValues[i] = packet.ReadInt32("Stat Value", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                item.ScalingStatDistribution = packet.ReadUInt32("SSD ID");
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

            item.AreaId = packet.ReadUInt32("Area");

            // In this single (?) case, map 0 means no map
            item.MapId = packet.ReadUInt32();
            packet.Writer.WriteLine("Map ID: " + (item.MapId != 0 ? StoreGetters.GetName(StoreNameType.Map, (int) item.MapId) : item.MapId + " (No map)"));

            item.BagFamily = packet.ReadEnum<BagFamilyMask>("Bag Family", TypeCode.Int32);

            item.TotemCategory = packet.ReadEnum<TotemCategory>("Totem Category", TypeCode.Int32);

            item.ItemSocketColors = new ItemSocketColor[3];
            item.SocketContent = new uint[3];
            for (var i = 0; i < 3; i++)
            {
                item.ItemSocketColors[i] = packet.ReadEnum<ItemSocketColor>("Socket Color", TypeCode.Int32, i);
                item.SocketContent[i] = packet.ReadUInt32("Socket Item", i);
            }

            item.SocketBonus = packet.ReadUInt32("Socket Bonus");

            item.GemProperties = packet.ReadUInt32("Gem Properties");

            item.RequiredDisenchantSkill = packet.ReadInt32("Required Disenchant Skill");

            item.ArmorDamageModifier = packet.ReadSingle("Armor Damage Modifier");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_4_2_8209))
                item.Duration = packet.ReadInt32("Duration");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                item.ItemLimitCategory = packet.ReadInt32("Limit Category");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                item.HolidayId = packet.ReadEnum<Holiday>("Holiday", TypeCode.Int32);

            Stuffing.ItemTemplates.TryAdd((uint) entry.Key, item);
        }

        [Parser(Opcode.SMSG_UPDATE_ITEM_ENCHANTMENTS)]
        public static void HandleUpdateItemEnchantments(Packet packet)
        {
            for (var i = 0; i < 4; i++)
                packet.ReadInt32("Aura ID", i);
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.ReadEnum<ItemClass>("Class", TypeCode.Byte);
            packet.ReadInt32("Mask");
        }

        [Parser(Opcode.SMSG_ITEM_COOLDOWN)]
        public static void HandleItemCooldown(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

        }
    }
}
