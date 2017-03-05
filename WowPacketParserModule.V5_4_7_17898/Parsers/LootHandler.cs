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
            var guid = packet.Translator.StartBitStream(6, 4, 2, 7, 5, 3, 0, 1);
            packet.Translator.ParseBitStream(guid, 3, 2, 1, 6, 0, 5, 7, 4);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_LOOT_REMOVED)]
        public static void HandleLootRemoved(Packet packet)
        {
            var lootGUID = new byte[8];
            var guid = new byte[8];

            guid[1] = packet.Translator.ReadBit();
            lootGUID[0] = packet.Translator.ReadBit();
            lootGUID[7] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            lootGUID[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            lootGUID[6] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            lootGUID[3] = packet.Translator.ReadBit();
            lootGUID[4] = packet.Translator.ReadBit();
            lootGUID[1] = packet.Translator.ReadBit();
            lootGUID[5] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(lootGUID, 5);
            packet.Translator.ReadXORByte(lootGUID, 4);
            packet.Translator.ReadXORByte(lootGUID, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(lootGUID, 1);
            packet.Translator.ReadXORByte(lootGUID, 0);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.ReadByte("Slot");

            packet.Translator.ReadXORByte(lootGUID, 3);
            packet.Translator.ReadXORByte(lootGUID, 6);
            packet.Translator.ReadXORByte(lootGUID, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.WriteGuid("Loot GUID", lootGUID);
            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
