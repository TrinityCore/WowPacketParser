using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_8_18414.Enums;
using WowPacketParser.V5_4_8_18414.Parsers;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.CMSG_AUTOBANK_ITEM)]
        public static void HandleAutoBankItem(Packet packet)
        {
            var unk1 = new byte[4];
            var unk2 = new byte[4];
            packet.ReadByte("Slot");
            packet.ReadSByte("Bag");
            var cnt = packet.ReadBits("Count", 2);
            for (var i = 0; i < cnt; i++)
            {
                unk1[i] = packet.ReadBit("unk1", i);
                unk2[i] = packet.ReadBit("unk2", i);
            }
            for (var j = 0; j < cnt; j++)
            {
                if (unk1[j]>0)
                    packet.ReadByte("Byte1", j);
                if (unk2[j]>0)
                    packet.ReadByte("Byte2", j);
            }
        }

        [Parser(Opcode.CMSG_AUTOEQUIP_ITEM)]
        public static void HandleAutoEquipItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_AUTOSTORE_BAG_ITEM)]
        public static void HandleAutoStoreBagItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_AUTOSTORE_BANK_ITEM)]
        public static void HandleAutostoreBankItem(Packet packet)
        {
            var hasByte1 = new bool[4];
            var hasByte2 = new bool[4];
            packet.ReadByte("Slot");
            packet.ReadSByte("Bag");
            var cnt = packet.ReadBits("Count", 2);
            for (var i = 0; i < cnt; i++)
            {
                hasByte1[i] = packet.ReadBit("has byte1", i);
                hasByte2[i] = packet.ReadBit("has byte2", i);
            }
            for (var i = 0; i < cnt; i++)
            {
                if (hasByte1[i])
                    packet.ReadByte("Byte1", i);
                if (hasByte2[i])
                    packet.ReadByte("Byte2", i);
            }
        }

        [Parser(Opcode.CMSG_AUTOSTORE_LOOT_ITEM)]
        public static void HandleAutoStoreLootItem(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var counter = packet.ReadBits("Count", 25);

                var guid = new byte[counter][];

                for (var i = 0; i < counter; i++)
                    guid[i] = packet.StartBitStream(2, 7, 0, 6, 5, 3, 1, 4);

                packet.ResetBitReader();

                for (var i = 0; i < counter; i++)
                {
                    packet.ParseBitStream(guid[i], 0, 4, 1, 7, 6, 5, 3, 2);
                    packet.ReadByte("Slot", i);

                    packet.WriteGuid("Lootee GUID", guid[i], i);
                }
            }
            else
            {
                packet.WriteLine("              : SMSG_???");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_BUY_ITEM)]
        public static void HandleBuyItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_BUYBACK_ITEM)]
        public static void HandleBuyBackItem(Packet packet)
        {
            packet.ReadUInt32("Slot");
            var guid = packet.StartBitStream(2, 3, 0, 4, 1, 7, 5, 6);
            packet.ParseBitStream(guid, 0, 6, 1, 7, 5, 2, 3, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_DESTROY_ITEM)]
        public static void HandleDestroyItem(Packet packet)
        {
            packet.ReadUInt32("Count"); // 16
            packet.ReadSByte("Bag"); // 21
            packet.ReadByte("Slot"); // 20
        }

        [Parser(Opcode.CMSG_OPEN_ITEM)]
        public static void HandleOpenItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_READ_ITEM)]
        public static void HandleReadItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_REFORGE_ITEM)]
        public static void HandleReforgeItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_REPAIR_ITEM)]
        public static void HandleRepairItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_REQUEST_HOTFIX)]
        public static void HandleItemRequestHotfix(Packet packet)
        {
            packet.ReadEnum<DB2Hash>("Type", TypeCode.UInt32);

            var count = packet.ReadBits("Count", 21);

            var guid = new byte[count][];

            for (var i = 0; i < count; i++)
                guid[i] = packet.StartBitStream(6, 3, 0, 1, 4, 5, 7, 2);

            packet.ResetBitReader();

            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid[i], 1);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry", i);
                packet.ParseBitStream(guid[i], 0, 5, 6, 4, 7, 2, 3);
                packet.WriteGuid("GUID", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_SELL_ITEM)]
        public static void HandleSellItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SPLIT_ITEM)]
        public static void HandleSplitItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SWAP_INV_ITEM)]
        public static void HandleSwapInventoryItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SWAP_ITEM)]
        public static void HandleSwapItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_TRANSMOGRIFY_ITEMS)]
        public static void HandleTransmogrifyItems(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_USE_ITEM)]
        public static void HandleUseItem2(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_BUY_FAILED)]
        public static void HandleBuyFailed(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_BUY_ITEM)]
        public static void HandleBuyItemResponse(Packet packet)
        {
            packet.ReadToEnd();
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var id = packet.ReadInt32("Entry");
            packet.ReadTime("Hotfix date");
            var HashType = packet.ReadEnum<DB2Hash>("Type", TypeCode.UInt32); // See DB2Hash enum. Left like this for now to see some numbers pop. ^^
            var size = packet.ReadUInt32("Size");

            if (size == 0 || id < 0)
                return;

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
                    item.Flags = packet.ReadEnum<ItemProtoFlags>("Flags", TypeCode.UInt32);
                    item.ExtraFlags = packet.ReadEnum<ItemFlagExtra>("Extra Flags", TypeCode.Int32);
                    item.Unk430_1 = packet.ReadSingle("Unk430_1");
                    item.Unk430_2 = packet.ReadSingle("Unk430_2");
                    packet.ReadSingle("unk");
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

                    for (var i = 1; i < 4; i++)
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
                case DB2Hash.KeyChain: // KeyChain
                {
                    packet.ReadUInt32("Key Chain Id");
                    packet.WriteLine("Key: {0}", Utilities.ByteArrayToHexString(packet.ReadBytes(32)));
                    break;
                }
                case DB2Hash.Creature:
                {
                    var unit = Storage.UnitTemplates.ContainsKey(itemId) ? Storage.UnitTemplates[itemId].Item1 : new UnitTemplate();

                    packet.ReadEntryWithName<UInt32>(StoreNameType.Unit, "Entry");
                    packet.ReadBytes(48);
                    packet.ReadInt16("NameLen");
                    unit.Name = packet.ReadCString("Name");
                    packet.ReadInt16("SubNameLen");
                    unit.SubName = packet.ReadCString("SubName");
                    packet.ReadBytes(10);

                    Storage.UnitTemplates.Add(itemId, unit, packet.TimeSpan);
                    break;
                }

                // Cases need correction, the other DB2's need implementation etc.
                default: packet.AsHex(); break;
            }

            if (HashType == DB2Hash.Item || HashType == DB2Hash.Item_sparse) // Add item data.
                packet.AddSniffData(StoreNameType.Item, (int)itemId, "DB_REPLY");

            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_INVENTORY_CHANGE_FAILURE)]
        public static void HandleInventoryChangeFailure(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_ITEM_TIME_UPDATE)]
        public static void HandleItemTimeUpdate(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_REFORGE_RESULT)]
        public static void HandleReforgeResult(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SELL_ITEM)]
        public static void HandleSellItemResponse(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficiency(Packet packet)
        {

            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadEnum<UnknownFlags>("Mask", TypeCode.UInt32);
                packet.ReadEnum<ItemClass>("Class", TypeCode.Byte);
            }
            else
            {
                packet.WriteLine("              : CMSG_VOID_STORAGE_TRANSFER");
                VoidStorageHandler.HandleVoidStorageTransfer(packet);
            }
        }
    }
}
