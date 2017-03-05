using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class AuctionHouseHandler
    {
        [Parser(Opcode.CMSG_AUCTION_HELLO_REQUEST)]
        public static void HandleClientAuctionHello(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 4, 6, 2, 0, 1, 3, 5);
            packet.Translator.ParseBitStream(guid, 4, 2, 6, 0, 5, 3, 7, 1);

            packet.Translator.WriteGuid("Guid", guid);

        }

        [Parser(Opcode.SMSG_AUCTION_HELLO_RESPONSE)]
        public static void HandleServerAuctionHello(Packet packet)
        {
            var guid = new byte[8];

            guid[2] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Enabled");
            guid[3] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadInt32("AuctionHouse ID");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
