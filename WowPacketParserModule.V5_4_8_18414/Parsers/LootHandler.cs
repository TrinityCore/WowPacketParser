using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class LootHandler
    {
        [Parser(Opcode.CMSG_LOOT)]
        public static void HandleLoot(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_LOOT_MASTER_GIVE)]
        public static void HandleLootMasterGive(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_LOOT_METHOD)]
        public static void HandleLootMethod(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_LOOT_MONEY)]
        public static void HandleLootMoney(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_LOOT_RELEASE)]
        public static void HandleLootRelease(Packet packet)
        {
            var guid = packet.StartBitStream(7, 4, 2, 3, 0, 5, 6, 1);
            packet.ParseBitStream(guid, 0, 6, 4, 2, 5, 3, 7, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_LOOT_ROLL)]
        public static void HandleLootRoll(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_OPT_OUT_OF_LOOT)]
        public static void HandleOptOutOfLoot(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_LOOT_CURRENCY)]
        [Parser(Opcode.SMSG_CURRENCY_LOOT_REMOVED)]
        public static void HandleLootCurrency(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_ALL_PASSED)]
        public static void HandleLootAllPassed(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_CLEAR_MONEY)]
        public static void HandleLootClearMoney(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_CONTENTS)]
        public static void HandleLootContents(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_LIST)]
        public static void Handle(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_MASTER_LIST)]
        public static void HandleLootMasterList(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_MONEY_NOTIFY)]
        public static void HandleLootMoneyNotify(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_RELEASE_RESPONSE)]
        public static void HandleLootReleaseResponse(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_REMOVED)]
        public static void HandleLootRemoved(Packet packet)
        {
            var guid = new byte[8];
            var lootGuid = new byte[8];

            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            lootGuid[0] = packet.ReadBit();
            lootGuid[1] = packet.ReadBit();
            lootGuid[2] = packet.ReadBit();
            lootGuid[7] = packet.ReadBit();
            lootGuid[6] = packet.ReadBit();
            lootGuid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            lootGuid[3] = packet.ReadBit();
            lootGuid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ReadXORByte(lootGuid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(lootGuid, 7);
            packet.ReadXORByte(lootGuid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(lootGuid, 5);
            packet.ReadXORByte(lootGuid, 3);
            packet.ReadXORByte(lootGuid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadByte("LootSlot");
            packet.ReadXORByte(lootGuid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(lootGuid, 4);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("LootGuid", lootGuid);
        }

        [Parser(Opcode.SMSG_LOOT_RESPONSE)]
        public static void HandleLootResponse(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_ROLL)]
        public static void HandleLootRollResponse(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_ROLL_WON)]
        public static void HandleLootWon(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_SLOT_CHANGED)]
        public static void HandleLootSlotChanged(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOOT_START_ROLL)]
        public static void HandleStartLoot(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
