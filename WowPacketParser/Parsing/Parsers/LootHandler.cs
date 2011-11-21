using System;
using System.Collections.Generic;
using System.Text;
using WowPacketParser.Enums;
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
            packet.ReadBoolean("Solo Loot");
        }

        [Parser(Opcode.CMSG_LOOT)]
        [Parser(Opcode.CMSG_LOOT_RELEASE)]
        public static void HandleLoot(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_LOOT_MASTER_GIVE)]
        public static void HandleLootMasterGive(Packet packet)
        {
            packet.ReadGuid("Loot GUID");
            packet.ReadByte("Slot");
            packet.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_LOOT_METHOD)]
        public static void HandleLootMethod(Packet packet)
        {
            packet.ReadEnum<LootMethod>("Loot Method", TypeCode.UInt32);
            packet.ReadGuid("Master GUID");
            packet.ReadEnum<ItemQuality>("Loot Threshold", TypeCode.UInt32);
        }


        [Parser(Opcode.CMSG_OPT_OUT_OF_LOOT)]
        public static void HandleOptOutOfLoot(Packet packet)
        {
            packet.ReadUInt32("Always Pass");
        }

        [Parser(Opcode.SMSG_LOOT_ALL_PASSED)]
        public static void HandleLootAllPassed(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Slot");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
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

        [Parser(Opcode.SMSG_LOOT_RELEASE_RESPONSE)]
        public static void HandleLootReleaseResponse(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBoolean("Unk Bool");
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
            var lootType = packet.ReadEnum<LootType>("Loot Type", TypeCode.Byte);
            if (lootType == LootType.Unk0)
            {
                packet.ReadByte("Slot");
                return;
            }

            loot.Gold = packet.ReadUInt32("Gold");

            var count = packet.ReadByte("Drop Count");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                packet.ReadByte("unk");

            loot.LootItems = new List<LootItem>(count);
            for (var i = 0; i < count; ++i)
            {
                var lootItem = new LootItem();
                packet.ReadByte("Slot", i);
                lootItem.ItemId = (uint) packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry", i);
                lootItem.Count = packet.ReadUInt32("Count", i);
                packet.ReadUInt32("Display ID", i);
                packet.ReadInt32("Random Suffix", i);
                packet.ReadInt32("Random Property Id", i);
                lootItem.SlotType = packet.ReadEnum<LootSlotType>("Slot Type", TypeCode.Byte, i);
                loot.LootItems.Add(lootItem);
            }

            Stuffing.Loots.TryAdd(new Tuple<uint, LootType>(guid.GetEntry(), lootType), loot);
        }

        [Parser(Opcode.CMSG_LOOT_ROLL)]
        public static void HandleLootRoll(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Slot");
            packet.ReadEnum<LootRollType>("Roll Type", TypeCode.Byte);
        }

        [Parser(Opcode.SMSG_LOOT_ROLL)]
        public static void HandleLootRollResponse(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Slot");
            packet.ReadGuid("Player GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            packet.ReadInt32("Random Property Id");
            packet.ReadInt32("Random Suffix");
            packet.ReadByte("Roll Number");
            packet.ReadEnum<LootRollType>("Roll Type", TypeCode.Byte);
            packet.ReadBoolean("Auto Pass");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL_WON)]
        public static void HandleLootWon(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Slot");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            packet.ReadInt32("Random Property Id");
            packet.ReadInt32("Random Suffix");
            packet.ReadGuid("Player GUID");
            packet.ReadByte("Roll Number");
            packet.ReadEnum<LootRollType>("Roll Type", TypeCode.Byte);
        }

        [Parser(Opcode.SMSG_LOOT_START_ROLL)]
        public static void HandleStartLoot(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadUInt32("Slot");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            packet.ReadInt32("Random Suffix");
            packet.ReadInt32("Random Property Id");
            packet.ReadUInt32("Count");
            packet.ReadUInt32("Count Down");
            packet.ReadEnum<LootVoteFlags>("Roll Vote Mask", TypeCode.Byte);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                packet.ReadByte("unk"); //amount of players? need verification.
        }

        [Parser(Opcode.SMSG_LOOT_SLOT_CHANGED)]
        public static void HandleLootSlotChanged(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Slot");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            packet.ReadUInt32("Display ID");
            packet.ReadInt32("Unk UInt32 1");
            packet.ReadInt32("Unk UInt32 2"); // only seen 0
            packet.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_LOOT_CLEAR_MONEY)]
        [Parser(Opcode.CMSG_LOOT_MONEY)]
        public static void HandleNullLoot(Packet packet)
        {
        }
    }
}
