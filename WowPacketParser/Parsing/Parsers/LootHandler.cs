using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class LootHandler
    {
        [Parser(Opcode.SMSG_LOOT_MONEY_NOTIFY)]
        public static void HandleLootMoneyNotify(Packet packet)
        {
            packet.ReadUInt32("Gold");

            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing)) // no idea when this was added, doesn't exist in 2.4.1
                packet.ReadBool("Solo Loot"); // true = YOU_LOOT_MONEY, false = LOOT_MONEY_SPLIT

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_0_15005)) // remove confirmed for 430
                packet.ReadUInt32("Guild Gold");
        }

        [Parser(Opcode.CMSG_LOOT_UNIT, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        [Parser(Opcode.CMSG_LOOT_RELEASE, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleLoot(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_LOOT_UNIT, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleLoot510(Packet packet)
        {
            var guid = packet.StartBitStream(1, 2, 7, 3, 6, 0, 4, 5);
            packet.ParseBitStream(guid, 1, 3, 5, 4, 0, 7, 6, 2);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_LOOT_RELEASE, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleLootRelease510(Packet packet)
        {
            var guid = packet.StartBitStream(4, 0, 6, 2, 3, 7, 1, 5);
            packet.ParseBitStream(guid, 0, 4, 1, 6, 7, 5, 3, 2);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_LOOT_MASTER_GIVE)]
        public static void HandleLootMasterGive(Packet packet)
        {
            packet.ReadGuid("Loot GUID");
            packet.ReadByte("Slot");
            packet.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_SET_LOOT_METHOD)]
        public static void HandleLootMethod(Packet packet)
        {
            packet.ReadUInt32E<LootMethod>("Loot Method");
            packet.ReadGuid("Master GUID");
            packet.ReadUInt32E<ItemQuality>("Loot Threshold");
        }

        [Parser(Opcode.CMSG_OPT_OUT_OF_LOOT)]
        public static void HandleOptOutOfLoot(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.ReadBool("Always Pass");
            else
                packet.ReadUInt32("Always Pass");
        }

        [Parser(Opcode.SMSG_LOOT_ALL_PASSED)]
        public static void HandleLootAllPassed(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Slot");
            packet.ReadUInt32<ItemId>("Entry");
            packet.ReadInt32("Random Property Id");
            packet.ReadInt32("Random Suffix");
        }

        [Parser(Opcode.SMSG_LOOT_LIST)]
        public static void Handle(Packet packet)
        {
            packet.ReadGuid("Creature GUID");
            packet.ReadPackedGuid("Master Loot GUID?");
            packet.ReadPackedGuid("Looter GUID");
        }

        [Parser(Opcode.SMSG_LOOT_RELEASE)]
        public static void HandleLootReleaseResponse(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBool("Unk Bool"); // true calls CGUnit_C::UpdateLootAnimKit and CGameUI::CloseLoot
        }

        [Parser(Opcode.SMSG_LOOT_REMOVED)]
        public static void HandleLootRemoved(Packet packet)
        {
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.SMSG_LOOT_RESPONSE)]
        public static void HandleLootResponse(Packet packet)
        {
            var loot = new Loot();

            var guid = packet.ReadGuid("GUID");
            var lootType = packet.ReadByteE<LootType>("Loot Type");
            if (lootType == LootType.None)
            {
                packet.ReadByte("Slot");
                return;
            }

            loot.Gold = packet.ReadUInt32("Gold");

            var count = packet.ReadByte("Drop Count");

            byte currencyCount = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                currencyCount = packet.ReadByte("Currency Count");

            loot.LootItems = new List<LootItem>(count);
            for (var i = 0; i < count; ++i)
            {
                var lootItem = new LootItem();
                packet.ReadByte("Slot", i);
                lootItem.ItemId = packet.ReadUInt32<ItemId>("Entry", i);
                lootItem.Count = packet.ReadUInt32("Count", i);
                packet.ReadUInt32("Display ID", i);
                packet.ReadInt32("Random Suffix", i);
                packet.ReadInt32("Random Property Id", i);
                packet.ReadByteE<LootSlotType>("Slot Type", i);
                loot.LootItems.Add(lootItem);
            }

            for (int i = 0; i < currencyCount; ++i)
            {
                packet.ReadByte("Slot", i);
                packet.ReadInt32("Currency Id", i);
                packet.ReadInt32("Count", i); // unconfirmed
            }

            // Items do not have item id in its guid, we need to query the wowobject store go
            if (guid.GetObjectType() == ObjectType.Item)
            {
                WoWObject item;
                UpdateField itemEntry;
                if (Storage.Objects.TryGetValue(guid, out item))
                    if (item.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(ObjectField.OBJECT_FIELD_ENTRY), out itemEntry))
                    {
                        Storage.Loots.Add(new Tuple<uint, ObjectType>(itemEntry.UInt32Value, guid.GetObjectType()), loot, packet.TimeSpan);
                        return;
                    }
            }

            Storage.Loots.Add(new Tuple<uint, ObjectType>(guid.GetEntry(), guid.GetObjectType()), loot, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_LOOT_ROLL)]
        public static void HandleLootRoll(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Slot");
            packet.ReadByteE<LootRollType>("Roll Type");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL)]
        public static void HandleLootRollResponse(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Slot");
            packet.ReadGuid("Player GUID");
            packet.ReadUInt32<ItemId>("Entry");
            packet.ReadInt32("Random Property Id");
            packet.ReadInt32("Random Suffix");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.ReadInt32("Roll Number");
            else
                packet.ReadByte("Roll Number");
            packet.ReadByteE<LootRollType>("Roll Type");
            packet.ReadBool("Auto Pass");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL_WON)]
        public static void HandleLootWon(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Slot");
            packet.ReadUInt32<ItemId>("Entry");
            packet.ReadInt32("Random Property Id");
            packet.ReadInt32("Random Suffix");
            packet.ReadGuid("Player GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.ReadInt32("Roll Number");
            else
                packet.ReadByte("Roll Number");
            packet.ReadByteE<LootRollType>("Roll Type");
        }

        [Parser(Opcode.SMSG_LOOT_START_ROLL)]
        public static void HandleStartLoot(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32<MapId>("Map ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685)) // probably earlier
                packet.ReadUInt32("Slot");
            packet.ReadUInt32<ItemId>("Entry");
            packet.ReadInt32("Random Suffix");
            packet.ReadInt32("Random Property Id");
            packet.ReadUInt32("Count");
            packet.ReadUInt32("Roll time");
            packet.ReadByteE<LootVoteFlags>("Roll Vote Mask");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                packet.ReadByte("unk"); //amount of players? need verification.
        }

        [Parser(Opcode.SMSG_LOOT_SLOT_CHANGED)]
        public static void HandleLootSlotChanged(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Slot");
            packet.ReadUInt32<ItemId>("Entry");
            packet.ReadUInt32("Display ID");
            packet.ReadInt32("Unk UInt32 1");
            packet.ReadInt32("Unk UInt32 2"); // only seen 0
            packet.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_LOOT_MASTER_LIST)]
        public static void HandleLootMasterList(Packet packet)
        {
            var count = packet.ReadByte("Count");
            for (var i = 0; i < count; i++)
                packet.ReadGuid("GUID", i);
        }

        [Parser(Opcode.SMSG_LOOT_CONTENTS)] //4.3.4
        public static void HandleLootContents(Packet packet)
        {
            var count1 = packet.ReadBits("Loot Items Count", 21);
            for (var i = 0; i < count1; i++)
            {
                packet.ReadUInt32("Display ID", i);
                packet.ReadInt32("Random Suffix Factor", i);
                packet.ReadInt32("Item Count", i);
                packet.ReadUInt32<ItemId>("Item Entry", i);
                packet.ReadInt32("Unk Int32", i); // possibly random property id or looted count
            }
        }

        [Parser(Opcode.CMSG_LOOT_CURRENCY)]
        [Parser(Opcode.SMSG_CURRENCY_LOOT_REMOVED)]
        public static void HandleLootCurrency(Packet packet)
        {
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.SMSG_LOOT_CLEAR_MONEY)]
        [Parser(Opcode.CMSG_LOOT_MONEY)]
        public static void HandleNullLoot(Packet packet)
        {
        }
    }
}
