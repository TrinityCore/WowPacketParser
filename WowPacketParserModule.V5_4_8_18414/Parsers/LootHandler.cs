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
            var guid = new byte[8];
            var guid2 = new byte[8];
            var unk43 = 4 - (packet.ReadBit() ? 1 : 0); // 43
            packet.WriteLine("Unk43 {0}", unk43);
            var hasLootType = !packet.ReadBit("!hasLootType"); // 42
            guid[4] = packet.ReadBit(); // 52
            var count64 = packet.ReadBits("count64", 20); // 64
            guid2[2] = packet.ReadBit(); // 58
            guid2[3] = packet.ReadBit(); // 59
            guid2[7] = packet.ReadBit(); // 63
            guid2[1] = packet.ReadBit(); // 57
            guid[6] = packet.ReadBit(); // 54
            guid[7] = packet.ReadBit(); // 55
            var hasGold = !packet.ReadBit("!hasGold"); //20
            var unk16 = packet.ReadBit("unk16"); // 16
            var unk40 = !packet.ReadBit("!unk40"); // 40
            var unk41 = packet.ReadBit("unk41"); // 41
            guid[5] = packet.ReadBit(); // 53
            guid2[6] = packet.ReadBit(); // 62
            var unk24 = packet.ReadBits("unk24", 19); // 24
            guid2[0] = packet.ReadBit(); // 56
            var unk140 = new uint[unk24];
            var unk144 = new bool[unk24];
            var unk132 = new bool[unk24];
            var unk133 = new bool[unk24];
            var unk136 = new uint[unk24];
            for (var i = 0; i < unk24; i++)
            {
                unk140[i] = packet.ReadBits("unk140", 3, i); // 140
                unk144[i] = packet.ReadBit("unk144", i);
                unk132[i] = !packet.ReadBit("!unk132", i);
                unk133[i] = !packet.ReadBit("!unk133", i);
                unk136[i] = packet.ReadBits("unk136", 2, i) ;
            }
            guid[1] = packet.ReadBit(); // 49
            guid[0] = packet.ReadBit(); // 48
            var unk284 = new uint[count64];
            for (var i = 0; i < count64; i++)
                unk284[i] = packet.ReadBits("unk284", 3, i); // 284
            guid2[5] = packet.ReadBit(); // 61
            guid[3] = packet.ReadBit(); // 51
            guid2[4] = packet.ReadBit(); // 60
            var unk80 = !packet.ReadBit("!unk80");
            guid[2] = packet.ReadBit(); // 50
            if (unk80)
                packet.ReadByte("unk80"); // 80
            for (var i = 0; i < unk24; i++)
            {
                packet.ReadInt32("unk124", i); // 124
                packet.ReadInt32("unk120", i); // 120
                packet.ReadInt32("unk112", i); // 112
                var unk148 = packet.ReadInt32("unk148", i); // 148
                packet.ReadBytes(unk148);

                if (unk132[i])
                    packet.ReadByte("unk132", i);
                packet.ReadInt32("unk128", i); // 128
                if (unk133[i])
                    packet.ReadByte("unk133", i); // 133
                packet.ReadInt32("unk116", i); // 116
            }
            packet.ParseBitStream(guid2, 2);
            if (hasGold)
                packet.ReadInt32("Gold"); // 20
            packet.ParseBitStream(guid2, 7); // 63
            packet.ParseBitStream(guid, 5); // 53
            packet.ParseBitStream(guid2, 3); // 59
            if (unk43!=3)
                packet.ReadByte("unk43");
            packet.ParseBitStream(guid2, 4); // 60
            if (hasLootType)
                packet.ReadByte("LootType"); // 42
            packet.ParseBitStream(guid, 4); // 52
            packet.ParseBitStream(guid2, 5); // 61
            for (var i = 0; i < count64; i++)
            {
                packet.ReadInt32("unk276", i); // 276
                packet.ReadByte("unk280", i); // 280
                packet.ReadInt32("unk272", i); // 272
            }
            packet.ParseBitStream(guid, 2, 3); // 50 51
            if (unk40)
                packet.ReadByte("unk40"); // 40
            packet.ParseBitStream(guid2, 1); // 57
            packet.ParseBitStream(guid, 0); // 48
            packet.ParseBitStream(guid2, 0); // 56
            packet.ParseBitStream(guid, 6, 7, 1); // 54 55 49
            packet.ParseBitStream(guid2, 6); // 62

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
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
