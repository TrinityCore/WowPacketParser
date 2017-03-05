using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class LootHandler
    {
        [Parser(Opcode.CMSG_LOOT_UNIT)]
        public static void HandleLoot(Packet packet)
        {
            var guid = packet.StartBitStream(6, 4, 2, 7, 5, 3, 0, 1);
            packet.ParseBitStream(guid, 3, 2, 1, 6, 0, 5, 7, 4);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_LOOT_REMOVED)]
        public static void HandleLootRemoved(Packet packet)
        {
            var lootGUID = new byte[8];
            var guid = new byte[8];

            guid[1] = packet.ReadBit();
            lootGUID[0] = packet.ReadBit();
            lootGUID[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            lootGUID[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            lootGUID[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            lootGUID[3] = packet.ReadBit();
            lootGUID[4] = packet.ReadBit();
            lootGUID[1] = packet.ReadBit();
            lootGUID[5] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ReadXORByte(lootGUID, 5);
            packet.ReadXORByte(lootGUID, 4);
            packet.ReadXORByte(lootGUID, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(lootGUID, 1);
            packet.ReadXORByte(lootGUID, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);

            packet.ReadByte("Slot");

            packet.ReadXORByte(lootGUID, 3);
            packet.ReadXORByte(lootGUID, 6);
            packet.ReadXORByte(lootGUID, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Loot GUID", lootGUID);
            packet.WriteGuid("Guid", guid);
        }
    }
}
