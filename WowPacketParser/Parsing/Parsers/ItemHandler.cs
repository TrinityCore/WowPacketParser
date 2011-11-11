using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ItemHandler
    {
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
            packet.ReadByte("Cast Count");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Glyph Index");
            packet.ReadByte("CastFlags");
            SpellHandler.ReadSpellCastTargets(packet);
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
            packet.ReadUInt32("Count");
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
            packet.ReadUInt32("Item ID");
            packet.ReadUInt32("Slot");
            packet.ReadUInt32("Count");
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
            packet.ReadUInt32("Item ID");
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
            packet.ReadUInt32("Count");
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
            if (ClientVersion.Build >= ClientVersionBuild.V4_2_2_14545) // Might be earlier
            {
                packet.ReadEnum<UnknownFlags>("Unknown Byte", TypeCode.Byte);
                packet.ReadInt32("Unknown Int32");
            }
        }

        [Parser(Opcode.SMSG_ITEM_QUERY_SINGLE_RESPONSE)]
        public static void HandleItemQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            if (entry.Value)
                return;

            var iClass = packet.ReadEnum<ItemClass>("Class", TypeCode.Int32);

            var subClass = packet.ReadInt32("Sub Class");

            var unk0 = packet.ReadInt32("Unk Int32");

            var name = new string[4];
            for (var i = 0; i < 4; i++)
                name[i] = packet.ReadCString("Name", i);

            var dispId = packet.ReadInt32("Display ID");

            var quality = packet.ReadEnum<ItemQuality>("Quality", TypeCode.Int32);

            var flags = packet.ReadEnum<ItemFlag>("Flags", TypeCode.Int32);

            var flags2 = ItemFlagExtra.None;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                flags2 = packet.ReadEnum<ItemFlagExtra>("Extra Flags", TypeCode.Int32);

            var buyPrice = packet.ReadInt32("Buy Price");

            var sellPrice = packet.ReadInt32("Sell Price");

            var invType = packet.ReadEnum<InventoryType>("Inventory Type", TypeCode.Int32);

            var allowClass = packet.ReadEnum<ClassMask>("Allowed Classes", TypeCode.Int32);

            var allowRace = packet.ReadEnum<RaceMask>("Allowed Races", TypeCode.Int32);

            var itemLvl = packet.ReadInt32("Item Level");

            var reqLvl = packet.ReadInt32("Required Level");

            var reqSkill = packet.ReadInt32("Required Skill ID");

            var reqSkLvl = packet.ReadInt32("Required Skill Level");

            var reqSpell = packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Required Spell");

            var reqHonor = packet.ReadInt32("Required Honor Rank");

            var reqCity = packet.ReadInt32("Required City Rank");

            var reqRepFaction = packet.ReadInt32("Required Rep Faction");

            var reqRepValue = packet.ReadInt32("Required Rep Value");

            var maxCount = packet.ReadInt32("Max Count");

            var stacks = packet.ReadInt32("Max Stack Size");

            var contSlots = packet.ReadInt32("Container Slots");

            int statsCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056) ? packet.ReadInt32("Stats Count") : 10;

            var statType = new ItemModType[statsCount];
            var statVal = new int[statsCount];
            for (var i = 0; i < statsCount; i++)
            {
                statType[i] = packet.ReadEnum<ItemModType>("Stat Type", TypeCode.Int32, i);
                statVal[i] = packet.ReadInt32("Stat Value", i);
            }

            var ssdId = 0;
            var ssdVal = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                ssdId = packet.ReadInt32("SSD ID");
                ssdVal = packet.ReadInt32("SSD Value");
            }

            int dmgCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767) ? 2 : 5;

            var dmgMin = new float[dmgCount];
            var dmgMax = new float[dmgCount];
            var dmgType = new DamageType[dmgCount];
            for (var i = 0; i < dmgCount; i++)
            {
                dmgMin[i] = packet.ReadSingle("Damage Min", i);
                dmgMax[i] = packet.ReadSingle("Damage Max", i);
                dmgType[i] = packet.ReadEnum<DamageType>("Damage Type", TypeCode.Int32, i);
            }

            var resistance = new int[7];
            for (var i = 0; i < 7; i++)
            {
                resistance[i] = packet.ReadInt32();
                packet.Writer.WriteLine((DamageType)i + " Resistance: " + resistance[i]);
            }

            var delay = packet.ReadInt32("Delay");

            var ammoType = packet.ReadEnum<AmmoType>("Ammo Type", TypeCode.Int32);

            var rangedMod = packet.ReadSingle("Ranged Mod");

            var spellId = new int[5];
            var spellTrigger = new ItemSpellTriggerType[5];
            var spellCharges = new int[5];
            var spellCooldown = new int[5];
            var spellCategory = new int[5];
            var spellCatCooldown = new int[5];
            for (var i = 0; i < 5; i++)
            {
                spellId[i] = packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Triggered Spell ID", i);
                spellTrigger[i] = packet.ReadEnum<ItemSpellTriggerType>("Trigger Spell Type", TypeCode.Int32, i);
                spellCharges[i] = packet.ReadInt32("Triggered Spell Charges", i);
                spellCooldown[i] = packet.ReadInt32("Triggered Spell Cooldown", i);
                spellCategory[i] = packet.ReadInt32("Triggered Spell Category", i);
                spellCatCooldown[i] = packet.ReadInt32("Triggered Spell Category Cooldown", i);
            }

            var binding = packet.ReadEnum<ItemBonding>("Bonding", TypeCode.Int32);

            var description = packet.ReadCString();

            var pageText = packet.ReadInt32("Page Text");

            var langId = packet.ReadEnum<Language>("Language", TypeCode.Int32);

            var pageMat = packet.ReadEnum<PageMaterial>("Page Material", TypeCode.Int32);

            var startQuest = packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Start Quest");

            var lockId = packet.ReadInt32("Lock ID");

            var material = packet.ReadEnum<Material>("Material", TypeCode.Int32);

            var sheath = packet.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);

            var randomProp = packet.ReadInt32("Random Property");

            var randomSuffix = packet.ReadInt32("Random Suffix");

            var block = packet.ReadInt32("Block");

            var itemSet = packet.ReadInt32("Item Set");

            var maxDura = packet.ReadInt32("Max Durability");

            var area = packet.ReadInt32("Area");

            // In this single (?) case, map 0 means no map
            var map = packet.ReadInt32();
            packet.Writer.WriteLine("Map ID: " + (map != 0 ? StoreGetters.GetName(StoreNameType.Map, map) : map + " (No map)"));

            var bagFamily = packet.ReadEnum<BagFamilyMask>("Bag Family", TypeCode.Int32);

            var totemCat = packet.ReadEnum<TotemCategory>("Totem Category", TypeCode.Int32);

            var socketColor = new ItemSocketColor[3];
            var socketItem = new int[3];
            for (var i = 0; i < 3; i++)
            {
                socketColor[i] = packet.ReadEnum<ItemSocketColor>("Socket Color", TypeCode.Int32, i);
                socketItem[i] = packet.ReadInt32("Socket Item", i);
            }

            var socketBonus = packet.ReadInt32("Socket Bonus");

            var gemProps = packet.ReadInt32("Gem Properties");

            var reqDisEnchSkill = packet.ReadInt32("Required Disenchant Skill");

            var armorDmgMod = packet.ReadSingle("Armor Damage Modifier");

            var duration = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_4_2_8209))
                duration = packet.ReadInt32("Duration");

            var limitCategory = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                limitCategory = packet.ReadInt32("Limit Category");

            var holidayId = Holiday.None;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                holidayId = packet.ReadEnum<Holiday>("Holiday", TypeCode.Int32);

            SQLStore.WriteData(SQLStore.Items.GetCommand(entry.Key, iClass, subClass, unk0, name, dispId,
                quality, flags, flags2, buyPrice, sellPrice, invType, allowClass, allowRace, itemLvl,
                reqLvl, reqSkill, reqSkLvl, reqSpell, reqHonor, reqCity, reqRepFaction, reqRepValue,
                maxCount, stacks, contSlots, statsCount, statType, statVal, ssdId, ssdVal, dmgMin, dmgMax,
                dmgType, resistance, delay, ammoType, rangedMod, spellId, spellTrigger, spellCharges,
                spellCooldown, spellCategory, spellCatCooldown, binding, description, pageText, langId,
                pageMat, startQuest, lockId, material, sheath, randomProp, randomSuffix, block, itemSet,
                maxDura, area, map, bagFamily, totemCat, socketColor, socketItem, socketBonus, gemProps,
                reqDisEnchSkill, armorDmgMod, duration, limitCategory, holidayId));
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
