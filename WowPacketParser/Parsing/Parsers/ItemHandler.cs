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
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Duration");
        }

        [Parser(Opcode.CMSG_ITEM_NAME_QUERY)]
        public static void HandleItemNameQuery(Packet packet)
        {
            packet.Translator.ReadUInt32<ItemId>("Entry");
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_ITEM_NAME_QUERY_RESPONSE)]
        public static void HandleItemNameQueryResponse(Packet packet)
        {
            var entry = packet.Translator.ReadUInt32<ItemId>("Entry");
            var name = packet.Translator.ReadCString("Name");
            packet.Translator.ReadUInt32E<InventoryType>("Inventory Type");

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Item,
                ID = (int)entry,
                Name = name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_SOCKET_GEMS)]
        public static void HandleSocketGems(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            for (var i = 0; i < 3; ++i)
                packet.Translator.ReadGuid("Gem GUID", i);
        }

        [Parser(Opcode.SMSG_INVENTORY_CHANGE_FAILURE)]
        public static void HandleInventoryChangeFailure(Packet packet)
        {
            var result = packet.Translator.ReadByteE<InventoryResult>("Result");
            if (result == InventoryResult.Ok)
                return;

            packet.Translator.ReadGuid("Item GUID1");
            packet.Translator.ReadGuid("Item GUID2");
            packet.Translator.ReadSByte("Bag");

            switch (result)
            {
                case InventoryResult.CantEquipLevel:
                case InventoryResult.PurchaseLevelTooLow:
                    packet.Translator.ReadUInt32("Required Level");
                    break;
                case InventoryResult.EventAutoEquipBindConfirm:
                    packet.Translator.ReadUInt64("Unk UInt64 1");
                    packet.Translator.ReadUInt32("Unk UInt32 1");
                    packet.Translator.ReadUInt64("Unk UInt64 2");
                    break;
                case InventoryResult.ItemMaxLimitCategoryCountExceeded:
                case InventoryResult.ItemMaxLimitCategorySocketedExceeded:
                case InventoryResult.ItemMaxLimitCategoryEquippedExceeded:
                    packet.Translator.ReadUInt32("Limit Category");
                    break;
            }
        }

        [Parser(Opcode.CMSG_USE_ITEM, ClientVersionBuild.Zero, ClientVersionBuild.V3_0_3_9183)]
        public static void HandleUseItem(Packet packet)
        {
            packet.Translator.ReadSByte("Bag");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadByte("Spell Count");
            packet.Translator.ReadByte("Cast Count");
            packet.Translator.ReadGuid("GUID");

            SpellHandler.ReadSpellCastTargets(packet);
        }

        [Parser(Opcode.CMSG_USE_ITEM, ClientVersionBuild.V3_0_3_9183)]
        public static void HandleUseItem2(Packet packet)
        {
            packet.Translator.ReadSByte("Bag");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadByte("Cast Count");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Glyph Index");
            var castflag = packet.Translator.ReadByteE<CastFlag>("Cast Flags");

            SpellHandler.ReadSpellCastTargets(packet);

            if (!castflag.HasAnyFlag(CastFlag.HasTrajectory))
                return;

            packet.Translator.ReadSingle("Elevation");
            packet.Translator.ReadSingle("Missile speed");

            // Boolean if it will send MSG_MOVE_STOP
            if (!packet.ReadBoolean())
                return;

            var opcode = packet.Translator.ReadInt32();
            var remainingLength = packet.Length - packet.Position;
            var bytes = packet.Translator.ReadBytes((int)remainingLength);

            using (var newpacket = new Packet(bytes, opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName))
                Handler.Parse(newpacket, true);
        }

        [Parser(Opcode.CMSG_AUTOSTORE_LOOT_ITEM, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleAutoStoreLootItem(Packet packet)
        {
            packet.Translator.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_AUTOSTORE_LOOT_ITEM, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleAutoStoreLootItem510(Packet packet)
        {
            var counter = packet.Translator.ReadBits("Count", 25);

            var guid = new byte[counter][];

            for (var i = 0; i < counter; ++i)
            {
                guid[i] = new byte[8];

                guid[i][5] = packet.Translator.ReadBit();
                guid[i][6] = packet.Translator.ReadBit();
                guid[i][7] = packet.Translator.ReadBit();
                guid[i][4] = packet.Translator.ReadBit();
                guid[i][3] = packet.Translator.ReadBit();
                guid[i][0] = packet.Translator.ReadBit();
                guid[i][2] = packet.Translator.ReadBit();
                guid[i][1] = packet.Translator.ReadBit();
            }

            packet.Translator.ResetBitReader();

            for (var i = 0; i < counter; ++i)
            {
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadByte("Slot", i);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadXORByte(guid[i], 2);

                packet.Translator.WriteGuid("Lootee GUID", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_SWAP_INV_ITEM)]
        public static void HandleSwapInventoryItem(Packet packet)
        {
            packet.Translator.ReadByte("Slot 1");
            packet.Translator.ReadByte("Slot 2");
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("From NPC");
            packet.Translator.ReadUInt32("Created");
            packet.Translator.ReadUInt32("Unk Uint32");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadInt32("Item Slot");
            packet.Translator.ReadUInt32<ItemId>("Entry");
            packet.Translator.ReadInt32("Suffix Factor");
            packet.Translator.ReadInt32("Random Property ID");
            packet.Translator.ReadUInt32("Count");
            packet.Translator.ReadUInt32("Count of Items in inventory");
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            packet.Translator.ReadGuid("Item GUID");
            packet.Translator.ReadUInt32("Slot");
            packet.Translator.ReadUInt32("Duration");
            packet.Translator.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_BUY_BACK_ITEM)]
        public static void HandleBuyBackItem(Packet packet)
        {
            packet.Translator.ReadGuid("Vendor GUID");
            packet.Translator.ReadUInt32("Slot");
        }

        [Parser(Opcode.CMSG_GET_ITEM_PURCHASE_DATA)]
        [Parser(Opcode.SMSG_READ_ITEM_RESULT_OK)]
        [Parser(Opcode.SMSG_READ_ITEM_RESULT_FAILED)]
        [Parser(Opcode.CMSG_ITEM_PURCHASE_REFUND)]
        public static void HandleReadItem(Packet packet)
        {
            packet.Translator.ReadGuid("Item GUID");
        }

        [Parser(Opcode.SMSG_ITEM_REFUND_INFO_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemRefundInfoResponse(Packet packet)
        {
            packet.Translator.ReadGuid("Item GUID");
            packet.Translator.ReadUInt32("Money Cost");
            packet.Translator.ReadUInt32("Honor Cost");
            packet.Translator.ReadUInt32("Arena Cost");
            for (var i = 0; i < 5; ++i)
            {
                packet.Translator.ReadUInt32<ItemId>("Extended Cost Entry", i);
                packet.Translator.ReadUInt32("Extended Cost Count", i);
            }
            packet.Translator.ReadUInt32("Unk UInt32 1");
            packet.Translator.ReadUInt32("Time Left");
        }

        [Parser(Opcode.SMSG_ITEM_REFUND_INFO_RESPONSE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemRefundInfoResponse434(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(3, 5, 7, 6, 2, 4, 0, 1);

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadUInt32("Time Left");
            for (var i = 0; i < 5; ++i)
            {
                packet.Translator.ReadUInt32("Item Count", i);
                packet.Translator.ReadUInt32<ItemId>("Item Cost Entry", i);
            }

            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            for (var i = 0; i < 5; ++i)
            {
                packet.Translator.ReadUInt32("Currency Count", i);
                packet.Translator.ReadUInt32("Currency Entry", i);
            }

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadUInt32("Unk UInt32 1");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadUInt32("Money Cost");
            packet.Translator.WriteGuid("Item Guid", guid);
        }

        [Parser(Opcode.SMSG_ITEM_PURCHASE_REFUND_RESULT, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleItemRefundResult(Packet packet)
        {
            packet.Translator.ReadGuid("Item Guid");
            packet.Translator.ReadInt32("Error ID");
        }

        [Parser(Opcode.SMSG_ITEM_PURCHASE_REFUND_RESULT, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleItemRefundResult422(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(5, 0, 3, 7, 4, 1, 6, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Error ID");
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.WriteGuid("Item Guid", guid);
        }

        [Parser(Opcode.SMSG_ITEM_PURCHASE_REFUND_RESULT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemRefundResult434(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(4, 5, 1, 6, 7, 0, 3, 2);

            var unkBit = packet.Translator.ReadBit("Has Item Refund");
            packet.Translator.ReadBit("Has Money Refund");

            if (unkBit)
            {
                for (int i = 0; i < 5; ++i) // Currencies
                {
                    packet.Translator.ReadInt32("CurrencyCount", i);
                    packet.Translator.ReadInt32("Currency", i);
                }

                packet.Translator.ReadInt32("Paid Money");

                for (int i = 0; i < 5; ++i) // Items
                {
                    packet.Translator.ReadInt32("ItemCount", i);
                    packet.Translator.ReadInt32("Item", i);
                }
            }

            packet.Translator.ParseBitStream(guid, 0, 3, 1, 6, 4, 2, 7, 5);
            packet.Translator.ReadByte("Error"); // Error Id?
            packet.Translator.WriteGuid("Item Guid", guid);
        }

        [Parser(Opcode.CMSG_REPAIR_ITEM)]
        public static void HandleRepairItem(Packet packet)
        {
            packet.Translator.ReadGuid("Vendor GUID");
            packet.Translator.ReadGuid("Item GUID");
            packet.Translator.ReadBool("Use guild money");
        }

        [Parser(Opcode.CMSG_SELL_ITEM)]
        public static void HandleSellItem(Packet packet)
        {
            packet.Translator.ReadGuid("Vendor GUID");
            packet.Translator.ReadGuid("Item GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192)) // not sure when this was changed exactly
                packet.Translator.ReadUInt32("Count");
            else
                packet.Translator.ReadByte("Count");
        }

        [Parser(Opcode.SMSG_SELL_ITEM)]
        public static void HandleSellItemResponse(Packet packet)
        {
            packet.Translator.ReadGuid("Vendor GUID");
            packet.Translator.ReadGuid("Item GUID");
            packet.Translator.ReadByteE<SellResult>("Sell Result");
        }

        [Parser(Opcode.CMSG_BUY_ITEM)]
        public static void HandleBuyItem(Packet packet)
        {
            packet.Translator.ReadGuid("Vendor GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623)) // not verified
                packet.Translator.ReadByte("Type"); // 1 item, 2 currency

            packet.Translator.ReadUInt32<ItemId>("Entry");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                packet.Translator.ReadUInt32("Slot");
                packet.Translator.ReadUInt32("Count");
            }
            else
                packet.Translator.ReadByte("Count");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623)) // not verified
                packet.Translator.ReadGuid("Bag GUID");

            packet.Translator.ReadByte("Bag Slot");
        }

        [Parser(Opcode.SMSG_BUY_SUCCEEDED)]
        public static void HandleBuyItemResponse(Packet packet)
        {
            packet.Translator.ReadGuid("Vendor GUID");
            packet.Translator.ReadUInt32("Slot");
            packet.Translator.ReadInt32("New Count");
            packet.Translator.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_BUY_FAILED)]
        public static void HandleBuyFailed(Packet packet)
        {
            packet.Translator.ReadGuid("Vendor GUID");
            packet.Translator.ReadUInt32<ItemId>("Entry");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.Translator.ReadInt32("Param");
            packet.Translator.ReadByteE<BuyResult>("Result");
        }

        [Parser(Opcode.CMSG_BUY_ITEM_IN_SLOT)]
        public static void HandleBuyItemInSlot(Packet packet)
        {
            packet.Translator.ReadGuid("Vendor GUID");
            packet.Translator.ReadUInt32<ItemId>("Entry");
            packet.Translator.ReadUInt32("Slot");
            packet.Translator.ReadUInt32("Count");
            packet.Translator.ReadGuid("Bag GUID");
            packet.Translator.ReadByte("Bag Slot");
            packet.Translator.ReadByte("Count");
        }

        [Parser(Opcode.CMSG_AUTOSTORE_BANK_ITEM)]
        [Parser(Opcode.CMSG_AUTO_EQUIP_ITEM)]
        [Parser(Opcode.CMSG_AUTOBANK_ITEM)]
        [Parser(Opcode.CMSG_OPEN_ITEM)]
        [Parser(Opcode.CMSG_READ_ITEM)]
        public static void HandleAutoBankItem(Packet packet)
        {
            packet.Translator.ReadSByte("Bag");
            packet.Translator.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_DESTROY_ITEM)]
        public static void HandleDestroyItem(Packet packet)
        {
            packet.Translator.ReadSByte("Bag");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadUInt32("Count");
        }

        [Parser(Opcode.CMSG_SWAP_ITEM)]
        public static void HandleSwapItem(Packet packet)
        {
            packet.Translator.ReadSByte("Bag");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadByte("Destination Bag");
            packet.Translator.ReadByte("Destination Slot");
        }

        [Parser(Opcode.CMSG_SPLIT_ITEM)]
        public static void HandleSplitItem(Packet packet)
        {
            packet.Translator.ReadSByte("Bag");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadByte("Destination Bag");
            packet.Translator.ReadByte("Destination Slot");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192)) // not sure when this was changed exactly
                packet.Translator.ReadUInt32("Count");
            else
                packet.Translator.ReadByte("Count");
        }

        [Parser(Opcode.SMSG_ENCHANTMENT_LOG)]
        public static void HandleEnchantmentLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Target");
            packet.Translator.ReadPackedGuid("Caster");
            packet.Translator.ReadInt32<ItemId>("Item Entry");
            packet.Translator.ReadUInt32("Enchantment ID?");
        }

        [Parser(Opcode.CMSG_ITEM_QUERY_SINGLE)]
        public static void HandleItemQuerySingle(Packet packet)
        {
            packet.Translator.ReadUInt32<ItemId>("Entry");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545)) // Might be earlier
            {
                packet.Translator.ReadByteE<UnknownFlags>("Unknown Byte");
                packet.Translator.ReadInt32("Unknown Int32");
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_ITEM_QUERY_SINGLE_RESPONSE)]
        public static void HandleItemQueryResponse(Packet packet)
        {


            var entry = packet.Translator.ReadEntry("Entry");
            if (entry.Value)
                return;

            ItemTemplate item = new ItemTemplate
            {
                Entry = (uint)entry.Key,
                Class = packet.Translator.ReadInt32E<ItemClass>("Class"),
                SubClass = packet.Translator.ReadUInt32("Sub Class"),
                SoundOverrideSubclass = packet.Translator.ReadInt32("Sound Override Subclass")
            };

            var name = new string[4];
            for (int i = 0; i < 4; i++)
                name[i] = packet.Translator.ReadCString("Name", i);
            item.Name = name[0];

            item.DisplayID = packet.Translator.ReadUInt32("Display ID");

            item.Quality = packet.Translator.ReadInt32E<ItemQuality>("Quality");

            item.Flags = packet.Translator.ReadUInt32E<ItemProtoFlags>("Flags 1");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                item.FlagsExtra = packet.Translator.ReadInt32E<ItemFlagExtra>("Flags 2");

            item.BuyPrice = packet.Translator.ReadUInt32("Buy Price");

            item.SellPrice = packet.Translator.ReadUInt32("Sell Price");

            item.InventoryType = packet.Translator.ReadInt32E<InventoryType>("Inventory Type");

            item.AllowedClasses = packet.Translator.ReadInt32E<ClassMask>("Allowed Classes");

            item.AllowedRaces = packet.Translator.ReadInt32E<RaceMask>("Allowed Races");

            item.ItemLevel = packet.Translator.ReadUInt32("Item Level");

            item.RequiredLevel = packet.Translator.ReadUInt32("Required Level");

            item.RequiredSkillId = packet.Translator.ReadUInt32("Required Skill ID");

            item.RequiredSkillLevel = packet.Translator.ReadUInt32("Required Skill Level");

            item.RequiredSpell = (uint)packet.Translator.ReadInt32<SpellId>("Required Spell");

            item.RequiredHonorRank = packet.Translator.ReadUInt32("Required Honor Rank");

            item.RequiredCityRank = packet.Translator.ReadUInt32("Required City Rank");

            item.RequiredRepFaction = packet.Translator.ReadUInt32("Required Rep Faction");

            item.RequiredRepValue = packet.Translator.ReadUInt32("Required Rep Value");

            item.MaxCount = packet.Translator.ReadInt32("Max Count");

            item.MaxStackSize = packet.Translator.ReadInt32("Max Stack Size");

            item.ContainerSlots = packet.Translator.ReadUInt32("Container Slots");

            item.StatsCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056) ? packet.Translator.ReadUInt32("Stats Count") : 10;
            item.StatTypes = new ItemModType?[item.StatsCount.GetValueOrDefault()];
            item.StatValues = new int?[item.StatsCount.GetValueOrDefault()];
            for (int i = 0; i < item.StatsCount; i++)
            {
                ItemModType type = packet.Translator.ReadInt32E<ItemModType>("Stat Type", i);
                item.StatTypes[i] = type == ItemModType.None ? ItemModType.Mana : type; // TDB
                item.StatValues[i] = packet.Translator.ReadInt32("Stat Value", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                item.ScalingStatDistribution = packet.Translator.ReadInt32("SSD ID");
                item.ScalingStatValue = packet.Translator.ReadUInt32("SSD Value");
            }

            int dmgCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767) ? 2 : 5;
            item.DamageMins = new float?[dmgCount];
            item.DamageMaxs = new float?[dmgCount];
            item.DamageTypes = new DamageType?[dmgCount];
            for (int i = 0; i < dmgCount; i++)
            {
                item.DamageMins[i] = packet.Translator.ReadSingle("Damage Min", i);
                item.DamageMaxs[i] = packet.Translator.ReadSingle("Damage Max", i);
                item.DamageTypes[i] = packet.Translator.ReadInt32E<DamageType>("Damage Type", i);
            }


            item.Armor = packet.Translator.ReadUInt32("Armor");
            item.HolyResistance = packet.Translator.ReadUInt32("HolyResistance");
            item.FireResistance = packet.Translator.ReadUInt32("FireResistance");
            item.NatureResistance = packet.Translator.ReadUInt32("NatureResistance");
            item.FrostResistance = packet.Translator.ReadUInt32("FrostResistance");
            item.ShadowResistance = packet.Translator.ReadUInt32("ShadowResistance");
            item.ArcaneResistance = packet.Translator.ReadUInt32("ArcaneResistance");

            item.Delay = packet.Translator.ReadUInt32("Delay");

            item.AmmoType = packet.Translator.ReadInt32E<AmmoType>("Ammo Type");

            item.RangedMod = packet.Translator.ReadSingle("Ranged Mod");

            item.TriggeredSpellIds = new int?[5];
            item.TriggeredSpellTypes = new ItemSpellTriggerType?[5];
            item.TriggeredSpellCharges = new int?[5];
            item.TriggeredSpellCooldowns = new int?[5];
            item.TriggeredSpellCategories = new uint?[5];
            item.TriggeredSpellCategoryCooldowns = new int?[5];
            for (int i = 0; i < 5; i++)
            {
                item.TriggeredSpellIds[i] = packet.Translator.ReadInt32<SpellId>("Triggered Spell ID", i);
                item.TriggeredSpellTypes[i] = packet.Translator.ReadInt32E<ItemSpellTriggerType>("Trigger Spell Type", i);
                item.TriggeredSpellCharges[i] = packet.Translator.ReadInt32("Triggered Spell Charges", i);
                item.TriggeredSpellCooldowns[i] = packet.Translator.ReadInt32("Triggered Spell Cooldown", i);
                item.TriggeredSpellCategories[i] = packet.Translator.ReadUInt32("Triggered Spell Category", i);
                item.TriggeredSpellCategoryCooldowns[i] = packet.Translator.ReadInt32("Triggered Spell Category Cooldown", i);
            }

            item.Bonding = packet.Translator.ReadInt32E<ItemBonding>("Bonding");

            item.Description = packet.Translator.ReadCString();

            item.PageText = packet.Translator.ReadUInt32("Page Text");

            item.Language = packet.Translator.ReadInt32E<Language>("Language");

            item.PageMaterial = packet.Translator.ReadInt32E<PageMaterial>("Page Material");

            item.StartQuestId = (uint)packet.Translator.ReadInt32<QuestId>("Start Quest");

            item.LockId = packet.Translator.ReadUInt32("Lock ID");

            item.Material = packet.Translator.ReadInt32E<Material>("Material");

            item.SheathType = packet.Translator.ReadInt32E<SheathType>("Sheath Type");

            item.RandomPropery = packet.Translator.ReadInt32("Random Property");

            item.RandomSuffix = packet.Translator.ReadUInt32("Random Suffix");

            item.Block = packet.Translator.ReadUInt32("Block");

            item.ItemSet = packet.Translator.ReadUInt32("Item Set");

            item.MaxDurability = packet.Translator.ReadUInt32("Max Durability");

            item.AreaID = packet.Translator.ReadUInt32<AreaId>("Area");

            // In this single (?) case, map 0 means no map
            item.MapID = packet.Translator.ReadInt32<MapId>("Map");

            item.BagFamily = packet.Translator.ReadInt32E<BagFamilyMask>("Bag Family");

            item.TotemCategory = packet.Translator.ReadInt32E<TotemCategory>("Totem Category");

            item.ItemSocketColors = new ItemSocketColor?[3];
            item.SocketContent = new uint?[3];
            for (int i = 0; i < 3; i++)
            {
                item.ItemSocketColors[i] = packet.Translator.ReadInt32E<ItemSocketColor>("Socket Color", i);
                item.SocketContent[i] = packet.Translator.ReadUInt32("Socket Item", i);
            }

            item.SocketBonus = packet.Translator.ReadInt32("Socket Bonus");

            item.GemProperties = packet.Translator.ReadInt32("Gem Properties");

            item.RequiredDisenchantSkill = packet.Translator.ReadInt32("Required Disenchant Skill");

            item.ArmorDamageModifier = packet.Translator.ReadSingle("Armor Damage Modifier");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_4_2_8209))
                item.Duration = packet.Translator.ReadUInt32("Duration");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                item.ItemLimitCategory = packet.Translator.ReadInt32("Limit Category");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                item.HolidayID = packet.Translator.ReadInt32E<Holiday>("Holiday");

            packet.AddSniffData(StoreNameType.Item, entry.Key, "QUERY_RESPONSE");

            Storage.ItemTemplates.Add(item, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_REQUEST_HOTFIX, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemRequestHotfix434(Packet packet)
        {
            packet.Translator.ReadUInt32("Type");
            var count = packet.Translator.ReadBits("Count", 23);
            var guidBytes = new byte[count][];
            for (var i = 0; i < count; ++i)
                guidBytes[i] = packet.Translator.StartBitStream(0, 4, 7, 2, 5, 3, 6, 1);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guidBytes[i], 5);
                packet.Translator.ReadXORByte(guidBytes[i], 6);
                packet.Translator.ReadXORByte(guidBytes[i], 7);
                packet.Translator.ReadXORByte(guidBytes[i], 0);
                packet.Translator.ReadXORByte(guidBytes[i], 1);
                packet.Translator.ReadXORByte(guidBytes[i], 3);
                packet.Translator.ReadXORByte(guidBytes[i], 4);

                packet.Translator.ReadUInt32<ItemId>("Entry", i);

                packet.Translator.ReadXORByte(guidBytes[i], 2);

                packet.Translator.WriteGuid("GUID", guidBytes[i], i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_HOTFIX, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleItemRequestHotfix422(Packet packet)
        {
            packet.Translator.ReadUInt32("Type");
            var count = packet.Translator.ReadUInt32("Count");
            var guidBytes = new byte[count][];
            for (var i = 0; i < count; ++i)
                guidBytes[i] = packet.Translator.StartBitStream(7, 3, 0, 5, 6, 4, 1, 2);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt32<ItemId>("Entry", i);
                guidBytes[i] = packet.Translator.ParseBitStream(guidBytes[i], 2, 6, 3, 0, 5, 7, 1, 4);
                packet.Translator.WriteGuid("GUID", guidBytes[i], i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_HOTFIX, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleItemRequestHotFix(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");
            packet.Translator.ReadUInt32("Type");

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt32<ItemId>("Entry", i);
                packet.Translator.ReadUInt32("Unk UInt32 1", i);
                packet.Translator.ReadUInt32("Unk UInt32 2", i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleDBReply(Packet packet)
        {
            packet.Translator.ReadUInt32("Type");
            var itemId = packet.Translator.ReadUInt32<ItemId>("Entry");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.Translator.ReadUInt32("Received Type");

            var size = packet.Translator.ReadUInt32("Size");
            if (size == 0)
            {
                packet.Translator.ReadUInt32("Received Type");
                return;
            }

            packet.Translator.ReadUInt32<ItemId>("Entry");
            if (size == 32)
            {
                packet.Translator.ReadInt32E<ItemClass>("Class");
                packet.Translator.ReadUInt32("Sub Class");
                packet.Translator.ReadInt32("Unk Int32");
                packet.Translator.ReadInt32E<Material>("Material");
                packet.Translator.ReadUInt32("Display ID");
                packet.Translator.ReadUInt32E<InventoryType>("Inventory Type");
                packet.Translator.ReadInt32E<SheathType>("Sheath Type");
            }
            else
            {
                packet.Translator.ReadInt32E<ItemQuality>("Quality");
                packet.Translator.ReadUInt32E<ItemProtoFlags>("Flags");
                packet.Translator.ReadInt32E<ItemFlagExtra>("Extra Flags");
                packet.Translator.ReadInt32("Buy Price");
                packet.Translator.ReadUInt32("Sell Price");
                packet.Translator.ReadInt32E<InventoryType>("Inventory Type");
                packet.Translator.ReadInt32E<ClassMask>("Allowed Classes");
                packet.Translator.ReadInt32E<RaceMask>("Allowed Races");
                packet.Translator.ReadUInt32("Item Level");
                packet.Translator.ReadUInt32("Required Level");
                packet.Translator.ReadUInt32("Required Skill ID");
                packet.Translator.ReadUInt32("Required Skill Level");
                packet.Translator.ReadInt32<SpellId>("Required Spell");
                packet.Translator.ReadUInt32("Required Honor Rank");
                packet.Translator.ReadUInt32("Required City Rank");
                packet.Translator.ReadUInt32("Required Rep Faction");
                packet.Translator.ReadUInt32("Required Rep Value");
                packet.Translator.ReadInt32("Max Count");
                packet.Translator.ReadInt32("Max Stack Size");
                packet.Translator.ReadUInt32("Container Slots");

                for (var i = 0; i < 10; i++)
                    packet.Translator.ReadInt32E<ItemModType>("Stat Type", i);

                for (var i = 0; i < 10; i++)
                    packet.Translator.ReadInt32("Stat Value", i);

                for (var i = 0; i < 10; i++)
                    packet.Translator.ReadInt32("Scaling Value", i);

                for (var i = 0; i < 10; i++)
                    packet.Translator.ReadInt32("Unk UInt32 2", i);

                packet.Translator.ReadUInt32("Scaling Stat Distribution");
                packet.Translator.ReadInt32E<DamageType>("Damage Type");
                packet.Translator.ReadUInt32("Delay");
                packet.Translator.ReadSingle("Ranged Mod");

                for (var i = 0; i < 5; i++)
                    packet.Translator.ReadInt32<SpellId>("Triggered Spell ID", i);

                for (var i = 0; i < 5; i++)
                    packet.Translator.ReadInt32E<ItemSpellTriggerType>("Trigger Spell Type", i);

                for (var i = 0; i < 5; i++)
                    packet.Translator.ReadInt32("Triggered Spell Charges", i);

                for (var i = 0; i < 5; i++)
                    packet.Translator.ReadInt32("Triggered Spell Cooldown", i);

                for (var i = 0; i < 5; i++)
                    packet.Translator.ReadUInt32("Triggered Spell Category", i);

                for (var i = 0; i < 5; i++)
                    packet.Translator.ReadInt32("Triggered Spell Category Cooldown", i);

                packet.Translator.ReadInt32E<ItemBonding>("Bonding");

                for (var i = 0; i < 4; i++)
                    if (packet.Translator.ReadUInt16() > 0)
                        packet.Translator.ReadCString("Name", i);

                if (packet.Translator.ReadUInt16() > 0)
                    packet.Translator.ReadCString("Description");

                packet.Translator.ReadUInt32("Page Text");
                packet.Translator.ReadInt32E<Language>("Language");
                packet.Translator.ReadInt32E<PageMaterial>("Page Material");
                packet.Translator.ReadInt32<QuestId>("Start Quest");
                packet.Translator.ReadUInt32("Lock ID");
                packet.Translator.ReadInt32E<Material>("Material");
                packet.Translator.ReadInt32E<SheathType>("Sheath Type");
                packet.Translator.ReadInt32("Random Property");
                packet.Translator.ReadInt32("Random Suffix");
                packet.Translator.ReadUInt32("Item Set");
                packet.Translator.ReadUInt32("Max Durability");
                packet.Translator.ReadUInt32<AreaId>("Area");
                // In this single (?) case, map 0 means no map
                packet.Translator.ReadUInt32<MapId>("Map ID");
                packet.Translator.ReadInt32E<BagFamilyMask>("Bag Family");
                packet.Translator.ReadInt32E<TotemCategory>("Totem Category");

                for (var i = 0; i < 3; i++)
                    packet.Translator.ReadInt32E<ItemSocketColor>("Socket Color", i);

                for (var i = 0; i < 3; i++)
                    packet.Translator.ReadUInt32("Socket Item", i);

                packet.Translator.ReadUInt32("Socket Bonus");
                packet.Translator.ReadUInt32("Gem Properties");
                packet.Translator.ReadSingle("Armor Damage Modifier");
                packet.Translator.ReadInt32("Duration");
                packet.Translator.ReadInt32("Limit Category");
                packet.Translator.ReadInt32E<Holiday>("Holiday");
                packet.Translator.ReadSingle("Stat Scaling Factor");
                packet.Translator.ReadUInt32("Unk UInt32 1");
                packet.Translator.ReadUInt32("Unk UInt32 2");
            }

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.Translator.ReadUInt32("Received Type");

            packet.AddSniffData(StoreNameType.Item, (int) itemId, "DB_REPLY");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleDBReply430(Packet packet)
        {
            var size = packet.Translator.ReadUInt32("Size");
            if (size == 0)
                return;

            if (size == 32)
            {
                uint entry = packet.Translator.ReadUInt32<ItemId>("Entry");

                ItemTemplate key = new ItemTemplate {Entry = entry};
                ItemTemplate item = Storage.ItemTemplates.ContainsKey(key) ? Storage.ItemTemplates[key].Item1 : new ItemTemplate();
                item.Class = packet.Translator.ReadInt32E<ItemClass>("Class");
                item.SubClass = packet.Translator.ReadUInt32("Sub Class");
                item.SoundOverrideSubclass = packet.Translator.ReadInt32("Sound Override Subclass");
                item.Material = packet.Translator.ReadInt32E<Material>("Material");
                packet.Translator.ReadUInt32("Unk");
                item.InventoryType = packet.Translator.ReadUInt32E<InventoryType>("Inventory Type");
                item.SheathType = packet.Translator.ReadInt32E<SheathType>("Sheath Type");

                Storage.ItemTemplates.Add(item, packet.TimeSpan);
            }
            else if (size == 36)
            {
                packet.Translator.ReadUInt32("Unk");
                packet.Translator.ReadUInt32("Unk");
                packet.Translator.ReadUInt32("Unk");
                packet.Translator.ReadUInt32("Unk");
                packet.Translator.ReadUInt32("Unk");
                packet.Translator.ReadUInt32("Unk");
                packet.Translator.ReadUInt32("Unk");
                packet.Translator.ReadUInt32("Unk");
                packet.Translator.ReadUInt32("Unk");
            }
            else
            {
                uint entry = packet.Translator.ReadUInt32<ItemId>("Entry");

                ItemTemplate key = new ItemTemplate { Entry = entry };
                ItemTemplate item = Storage.ItemTemplates.ContainsKey(key) ? Storage.ItemTemplates[key].Item1 : new ItemTemplate();

                item.Quality = packet.Translator.ReadInt32E<ItemQuality>("Quality");
                item.Flags = packet.Translator.ReadUInt32E<ItemProtoFlags>("Flags 1");
                item.FlagsExtra = packet.Translator.ReadInt32E<ItemFlagExtra>("Flags 3");
                item.Unk430_1 = packet.Translator.ReadSingle("Unk430_1");
                item.Unk430_2 = packet.Translator.ReadSingle("Unk430_2");
                item.BuyCount = packet.Translator.ReadUInt32("Buy count");
                item.BuyPrice = packet.Translator.ReadUInt32("Buy Price");
                item.SellPrice = packet.Translator.ReadUInt32("Sell Price");
                item.InventoryType = packet.Translator.ReadInt32E<InventoryType>("Inventory Type");
                item.AllowedClasses = packet.Translator.ReadInt32E<ClassMask>("Allowed Classes");
                item.AllowedRaces = packet.Translator.ReadInt32E<RaceMask>("Allowed Races");
                item.ItemLevel = packet.Translator.ReadUInt32("Item Level");
                item.RequiredLevel = packet.Translator.ReadUInt32("Required Level");
                item.RequiredSkillId = packet.Translator.ReadUInt32("Required Skill ID");
                item.RequiredSkillLevel = packet.Translator.ReadUInt32("Required Skill Level");
                item.RequiredSpell = (uint)packet.Translator.ReadInt32<SpellId>("Required Spell");
                item.RequiredHonorRank = packet.Translator.ReadUInt32("Required Honor Rank");
                item.RequiredCityRank = packet.Translator.ReadUInt32("Required City Rank");
                item.RequiredRepFaction = packet.Translator.ReadUInt32("Required Rep Faction");
                item.RequiredRepValue = packet.Translator.ReadUInt32("Required Rep Value");
                item.MaxCount = packet.Translator.ReadInt32("Max Count");
                item.MaxStackSize = packet.Translator.ReadInt32("Max Stack Size");
                item.ContainerSlots = packet.Translator.ReadUInt32("Container Slots");

                item.StatTypes = new ItemModType?[10];
                for (int i = 0; i < 10; i++)
                {
                    ItemModType statType = packet.Translator.ReadInt32E<ItemModType>("Stat Type", i);
                    item.StatTypes[i] = statType == ItemModType.None ? ItemModType.Mana : statType; // TDB
                }

                item.StatValues = new int?[10];
                for (int i = 0; i < 10; i++)
                    item.StatValues[i] = packet.Translator.ReadInt32("Stat Value", i);

                item.ScalingValue = new int?[10];
                for (int i = 0; i < 10; i++)
                    item.ScalingValue[i] = packet.Translator.ReadInt32("Scaling Value", i);

                item.SocketCostRate = new int?[10];
                for (int i = 0; i < 10; i++)
                    item.SocketCostRate[i] = packet.Translator.ReadInt32("Socket Cost Rate", i);

                item.ScalingStatDistribution = packet.Translator.ReadInt32("Scaling Stat Distribution");
                item.DamageType = packet.Translator.ReadInt32E<DamageType>("Damage Type");
                item.Delay = packet.Translator.ReadUInt32("Delay");
                item.RangedMod = packet.Translator.ReadSingle("Ranged Mod");

                item.TriggeredSpellIds = new int?[5];
                for (int i = 0; i < 5; i++)
                    item.TriggeredSpellIds[i] = packet.Translator.ReadInt32<SpellId>("Triggered Spell ID", i);

                item.TriggeredSpellTypes = new ItemSpellTriggerType?[5];
                for (int i = 0; i < 5; i++)
                    item.TriggeredSpellTypes[i] = packet.Translator.ReadInt32E<ItemSpellTriggerType>("Trigger Spell Type", i);

                item.TriggeredSpellCharges = new int?[5];
                for (int i = 0; i < 5; i++)
                    item.TriggeredSpellCharges[i] = packet.Translator.ReadInt32("Triggered Spell Charges", i);

                item.TriggeredSpellCooldowns = new int?[5];
                for (int i = 0; i < 5; i++)
                    item.TriggeredSpellCooldowns[i] = packet.Translator.ReadInt32("Triggered Spell Cooldown", i);

                item.TriggeredSpellCategories = new uint?[5];
                for (int i = 0; i < 5; i++)
                    item.TriggeredSpellCategories[i] = packet.Translator.ReadUInt32("Triggered Spell Category", i);

                item.TriggeredSpellCategoryCooldowns = new int?[5];
                for (int i = 0; i < 5; i++)
                    item.TriggeredSpellCategoryCooldowns[i] = packet.Translator.ReadInt32("Triggered Spell Category Cooldown", i);

                item.Bonding = packet.Translator.ReadInt32E<ItemBonding>("Bonding");

                if (packet.Translator.ReadUInt16() > 0)
                    item.Name = packet.Translator.ReadCString("Name", 0);

                for (int i = 1; i < 4; ++i)
                    if (packet.Translator.ReadUInt16() > 0)
                        packet.Translator.ReadCString("Name", i);

                if (packet.Translator.ReadUInt16() > 0)
                    item.Description = packet.Translator.ReadCString("Description");

                item.PageText = packet.Translator.ReadUInt32("Page Text");
                item.Language = packet.Translator.ReadInt32E<Language>("Language");
                item.PageMaterial = packet.Translator.ReadInt32E<PageMaterial>("Page Material");
                item.StartQuestId = (uint)packet.Translator.ReadInt32<QuestId>("Start Quest");
                item.LockId = packet.Translator.ReadUInt32("Lock ID");
                item.Material = packet.Translator.ReadInt32E<Material>("Material");
                item.SheathType = packet.Translator.ReadInt32E<SheathType>("Sheath Type");
                item.RandomPropery = packet.Translator.ReadInt32("Random Property");
                item.RandomSuffix = packet.Translator.ReadUInt32("Random Suffix");
                item.ItemSet = packet.Translator.ReadUInt32("Item Set");
                item.AreaID = packet.Translator.ReadUInt32<AreaId>("Area");
                item.MapID = packet.Translator.ReadInt32<MapId>("Map ID");
                item.BagFamily = packet.Translator.ReadInt32E<BagFamilyMask>("Bag Family");
                item.TotemCategory = packet.Translator.ReadInt32E<TotemCategory>("Totem Category");

                item.ItemSocketColors = new ItemSocketColor?[3];
                for (int i = 0; i < 3; i++)
                    item.ItemSocketColors[i] = packet.Translator.ReadInt32E<ItemSocketColor>("Socket Color", i);

                item.SocketContent = new uint?[3];
                for (int i = 0; i < 3; i++)
                    item.SocketContent[i] = packet.Translator.ReadUInt32("Socket Item", i);

                item.SocketBonus = packet.Translator.ReadInt32("Socket Bonus");
                item.GemProperties = packet.Translator.ReadInt32("Gem Properties");
                item.ArmorDamageModifier = packet.Translator.ReadSingle("Armor Damage Modifier");
                item.Duration = packet.Translator.ReadUInt32("Duration");
                item.ItemLimitCategory = packet.Translator.ReadInt32("Limit Category");
                item.HolidayID = packet.Translator.ReadInt32E<Holiday>("Holiday");
                item.StatScalingFactor = packet.Translator.ReadSingle("Stat Scaling Factor");
                item.CurrencySubstitutionID = packet.Translator.ReadUInt32("Currency Substitution Id");
                item.CurrencySubstitutionCount = packet.Translator.ReadUInt32("Currency Substitution Count");

                Storage.ObjectNames.Add(new ObjectName { ObjectType = ObjectType.Item, ID = (int)entry, Name = item.Name }, packet.TimeSpan);
            }

            packet.Translator.ReadUInt32("Type");
            packet.Translator.ReadTime("Hotfix date");
            var itemId = packet.Translator.ReadUInt32<ItemId>("Entry");

            packet.AddSniffData(StoreNameType.Item, (int)itemId, "DB_REPLY");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleDBReply434(Packet packet)
        {
            var id = packet.Translator.ReadInt32("Entry");

            var type = packet.Translator.ReadUInt32E<DB2Hash>("Hotfix DB2 File");
            packet.Translator.ReadTime("Hotfix date");
            var size = packet.Translator.ReadUInt32("Size");
            if (size == 0)
                return;

            if (id < 0)
                return;

            var itemId = (uint)id;

            switch (type)
            {
                case DB2Hash.Item:
                {
                    ItemTemplate key = new ItemTemplate {Entry = itemId};
                    ItemTemplate item = Storage.ItemTemplates.ContainsKey(key) ? Storage.ItemTemplates[key].Item1 : new ItemTemplate();

                    packet.Translator.ReadUInt32<ItemId>("Entry");
                    item.Class = packet.Translator.ReadInt32E<ItemClass>("Class");
                    item.SubClass = packet.Translator.ReadUInt32("Sub Class");
                    item.SoundOverrideSubclass = packet.Translator.ReadInt32("Sound Override Subclass");
                    item.Material = packet.Translator.ReadInt32E<Material>("Material");
                    item.DisplayID = packet.Translator.ReadUInt32("Display ID");
                    item.InventoryType = packet.Translator.ReadUInt32E<InventoryType>("Inventory Type");
                    item.SheathType = packet.Translator.ReadInt32E<SheathType>("Sheath Type");

                    Storage.ItemTemplates.Add(item, packet.TimeSpan);
                    break;
                }
                case DB2Hash.ItemSparse:
                {
                    ItemTemplate key = new ItemTemplate { Entry = itemId };
                    ItemTemplate item = Storage.ItemTemplates.ContainsKey(key) ? Storage.ItemTemplates[key].Item1 : new ItemTemplate();

                    packet.Translator.ReadUInt32<ItemId>("Entry");
                    item.Quality = packet.Translator.ReadInt32E<ItemQuality>("Quality");
                    item.Flags = packet.Translator.ReadUInt32E<ItemProtoFlags>("Flags 1");
                    item.FlagsExtra = packet.Translator.ReadInt32E<ItemFlagExtra>("Flags 2");
                    item.Unk430_1 = packet.Translator.ReadSingle("Unk430_1");
                    item.Unk430_2 = packet.Translator.ReadSingle("Unk430_2");
                    item.BuyCount = packet.Translator.ReadUInt32("Buy count");
                    item.BuyPrice = packet.Translator.ReadUInt32("Buy Price");
                    item.SellPrice = packet.Translator.ReadUInt32("Sell Price");
                    item.InventoryType = packet.Translator.ReadInt32E<InventoryType>("Inventory Type");
                    item.AllowedClasses = packet.Translator.ReadInt32E<ClassMask>("Allowed Classes");
                    item.AllowedRaces = packet.Translator.ReadInt32E<RaceMask>("Allowed Races");
                    item.ItemLevel = packet.Translator.ReadUInt32("Item Level");
                    item.RequiredLevel = packet.Translator.ReadUInt32("Required Level");
                    item.RequiredSkillId = packet.Translator.ReadUInt32("Required Skill ID");
                    item.RequiredSkillLevel = packet.Translator.ReadUInt32("Required Skill Level");
                    item.RequiredSpell = (uint)packet.Translator.ReadInt32<SpellId>("Required Spell");
                    item.RequiredHonorRank = packet.Translator.ReadUInt32("Required Honor Rank");
                    item.RequiredCityRank = packet.Translator.ReadUInt32("Required City Rank");
                    item.RequiredRepFaction = packet.Translator.ReadUInt32("Required Rep Faction");
                    item.RequiredRepValue = packet.Translator.ReadUInt32("Required Rep Value");
                    item.MaxCount = packet.Translator.ReadInt32("Max Count");
                    item.MaxStackSize = packet.Translator.ReadInt32("Max Stack Size");
                    item.ContainerSlots = packet.Translator.ReadUInt32("Container Slots");

                    item.StatTypes = new ItemModType?[10];
                    for (int i = 0; i < 10; i++)
                    {
                        ItemModType statType = packet.Translator.ReadInt32E<ItemModType>("Stat Type", i);
                        item.StatTypes[i] = statType == ItemModType.None ? ItemModType.Mana : statType; // TDB
                    }

                    item.StatValues = new int?[10];
                    for (int i = 0; i < 10; i++)
                        item.StatValues[i] = packet.Translator.ReadInt32("Stat Value", i);

                    item.ScalingValue = new int?[10];
                    for (int i = 0; i < 10; i++)
                        item.ScalingValue[i] = packet.Translator.ReadInt32("Scaling Value", i);

                    item.SocketCostRate = new int?[10];
                    for (int i = 0; i < 10; i++)
                        item.SocketCostRate[i] = packet.Translator.ReadInt32("Socket Cost Rate", i);

                    item.ScalingStatDistribution = packet.Translator.ReadInt32("Scaling Stat Distribution");
                    item.DamageType = packet.Translator.ReadInt32E<DamageType>("Damage Type");
                    item.Delay = packet.Translator.ReadUInt32("Delay");
                    item.RangedMod = packet.Translator.ReadSingle("Ranged Mod");

                    item.TriggeredSpellIds = new int?[5];
                    for (int i = 0; i < 5; i++)
                        item.TriggeredSpellIds[i] = packet.Translator.ReadInt32<SpellId>("Triggered Spell ID", i);

                    item.TriggeredSpellTypes = new ItemSpellTriggerType?[5];
                    for (int i = 0; i < 5; i++)
                        item.TriggeredSpellTypes[i] = packet.Translator.ReadInt32E<ItemSpellTriggerType>("Trigger Spell Type", i);

                    item.TriggeredSpellCharges = new int?[5];
                    for (int i = 0; i < 5; i++)
                        item.TriggeredSpellCharges[i] = packet.Translator.ReadInt32("Triggered Spell Charges", i);

                    item.TriggeredSpellCooldowns = new int?[5];
                    for (int i = 0; i < 5; i++)
                        item.TriggeredSpellCooldowns[i] = packet.Translator.ReadInt32("Triggered Spell Cooldown", i);

                    item.TriggeredSpellCategories = new uint?[5];
                    for (int i = 0; i < 5; i++)
                        item.TriggeredSpellCategories[i] = packet.Translator.ReadUInt32("Triggered Spell Category", i);

                    item.TriggeredSpellCategoryCooldowns = new int?[5];
                    for (int i = 0; i < 5; i++)
                        item.TriggeredSpellCategoryCooldowns[i] = packet.Translator.ReadInt32("Triggered Spell Category Cooldown", i);

                    item.Bonding = packet.Translator.ReadInt32E<ItemBonding>("Bonding");

                    if (packet.Translator.ReadUInt16() > 0)
                        item.Name = packet.Translator.ReadCString("Name", 0);

                    for (int i = 1; i < 4; ++i)
                        if (packet.Translator.ReadUInt16() > 0)
                            packet.Translator.ReadCString("Name", i);

                    if (packet.Translator.ReadUInt16() > 0)
                        item.Description = packet.Translator.ReadCString("Description");

                    item.PageText = packet.Translator.ReadUInt32("Page Text");
                    item.Language = packet.Translator.ReadInt32E<Language>("Language");
                    item.PageMaterial = packet.Translator.ReadInt32E<PageMaterial>("Page Material");
                    item.StartQuestId = (uint)packet.Translator.ReadInt32<QuestId>("Start Quest");
                    item.LockId = packet.Translator.ReadUInt32("Lock ID");
                    item.Material = packet.Translator.ReadInt32E<Material>("Material");
                    item.SheathType = packet.Translator.ReadInt32E<SheathType>("Sheath Type");
                    item.RandomPropery = packet.Translator.ReadInt32("Random Property");
                    item.RandomSuffix = packet.Translator.ReadUInt32("Random Suffix");
                    item.ItemSet = packet.Translator.ReadUInt32("Item Set");
                    item.AreaID = packet.Translator.ReadUInt32<AreaId>("Area");
                    item.MapID = packet.Translator.ReadInt32("Map ID");
                    item.BagFamily = packet.Translator.ReadInt32E<BagFamilyMask>("Bag Family");
                    item.TotemCategory = packet.Translator.ReadInt32E<TotemCategory>("Totem Category");

                    item.ItemSocketColors = new ItemSocketColor?[3];
                    for (int i = 0; i < 3; i++)
                        item.ItemSocketColors[i] = packet.Translator.ReadInt32E<ItemSocketColor>("Socket Color", i);

                    item.SocketContent = new uint?[3];
                    for (int i = 0; i < 3; i++)
                        item.SocketContent[i] = packet.Translator.ReadUInt32("Socket Item", i);

                    item.SocketBonus = packet.Translator.ReadInt32("Socket Bonus");
                    item.GemProperties = packet.Translator.ReadInt32("Gem Properties");
                    item.ArmorDamageModifier = packet.Translator.ReadSingle("Armor Damage Modifier");
                    item.Duration = packet.Translator.ReadUInt32("Duration");
                    item.ItemLimitCategory = packet.Translator.ReadInt32("Limit Category");
                    item.HolidayID = packet.Translator.ReadInt32E<Holiday>("Holiday");
                    item.StatScalingFactor = packet.Translator.ReadSingle("Stat Scaling Factor");
                    item.CurrencySubstitutionID = packet.Translator.ReadUInt32("Currency Substitution Id");
                    item.CurrencySubstitutionCount = packet.Translator.ReadUInt32("Currency Substitution Count");

                    Storage.ObjectNames.Add(new ObjectName { ObjectType = ObjectType.Item, ID = (int)itemId, Name = item.Name }, packet.TimeSpan);
                    break;
                }
                case DB2Hash.KeyChain:
                {
                    packet.Translator.ReadUInt32("Key Chain Id");
                    packet.Translator.ReadBytes("Key", 32);
                    break;
                }
            }

            packet.AddSniffData(StoreNameType.Item, (int)itemId, "DB_REPLY");
        }

        [Parser(Opcode.SMSG_SOCKET_GEMS)]
        public static void HandleUpdateItemEnchantments(Packet packet)
        {
            packet.Translator.ReadGuid("Item Guid");

            for (var i = 0; i < 4; i++)
                packet.Translator.ReadInt32("Aura ID", i);
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.Translator.ReadByteE<ItemClass>("Class");
            packet.Translator.ReadUInt32E<UnknownFlags>("Mask");
        }

        [Parser(Opcode.SMSG_ITEM_COOLDOWN)]
        public static void HandleItemCooldown(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.CMSG_REFORGE_ITEM, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemSendReforge(Packet packet)
        {
            packet.Translator.ReadInt32("Reforge Entry");
            packet.Translator.ReadInt32("Slot");
            packet.Translator.ReadInt32("Bag");

            var guid = packet.Translator.StartBitStream(2,6,3,4,1,0,7,5);
            packet.Translator.ParseBitStream(guid,2,3,6,4,1,0,7,5);
            packet.Translator.WriteGuid("Reforger Guid", guid);

        }

        [Parser(Opcode.SMSG_REFORGE_RESULT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleItemReforgeResult(Packet packet)
        {
            packet.Translator.ReadBit("Successful");
        }

        [Parser(Opcode.SMSG_QUERY_ITEM_TEXT_RESPONSE)]
        public static void HandleItemTextQueryResult(Packet packet)
        {
            if (!packet.Translator.ReadBool("Empty"))
            {
                packet.Translator.ReadGuid("Item Guid");
                packet.Translator.ReadCString("Item Text");
            }
        }

        [Parser(Opcode.CMSG_ITEM_TEXT_QUERY)]
        public static void HandleItemTextQuery(Packet packet)
        {
            packet.Translator.ReadGuid("Item Guid");
        }

        [Parser(Opcode.CMSG_TRANSMOGRIFY_ITEMS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTransmogrifyITems(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 22);

            var itemGuids = new byte[count][];

            for (int i = 0; i < count; ++i)
                itemGuids[i] = packet.Translator.StartBitStream(0, 5, 6, 2, 3, 7, 4, 1);

            var npcGuid = packet.Translator.StartBitStream(7, 3, 5, 6, 1, 4, 0, 2);

            // flush bits

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt32<ItemId>("New Entry", i);

                packet.Translator.ParseBitStream(itemGuids[i], 1, 5, 0, 4, 6, 7, 3, 2);

                packet.Translator.ReadUInt32E<EquipmentSlotType>("Slot", i);

                packet.Translator.WriteGuid("ITem Guid", itemGuids[i], i);
            }

            packet.Translator.ParseBitStream(npcGuid, 7, 2, 5, 4, 3, 1, 6, 0);
            packet.Translator.WriteGuid("NPC Guid", npcGuid);
        }

        [Parser(Opcode.SMSG_ITEM_EXPIRE_PURCHASE_REFUND)]
        public static void HandleItemExpirePurchaseRefund(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(3, 0, 5, 4, 6, 2, 1, 7);
            packet.Translator.ParseBitStream(guid, 1, 0, 3, 4, 7, 6, 5, 2);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ITEM_UPGRADE_RESULT)]
        public static void HandleItemUpgradeResult(Packet packet)
        {
            packet.Translator.ReadBit("Result");
        }
    }
}
