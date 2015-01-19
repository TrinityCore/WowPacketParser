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

            packet.StartBitStream(guid, 7, 4, 6, 2, 0, 1, 3, 5);
            packet.ParseBitStream(guid, 4, 2, 6, 0, 5, 3, 7, 1);

            packet.WriteGuid("Guid", guid);

        }

        [Parser(Opcode.SMSG_AUCTION_HELLO_RESPONSE)]
        public static void HandleServerAuctionHello(Packet packet)
        {
            var guid = new byte[8];

            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            packet.ReadBit("Enabled");
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadInt32("AuctionHouse ID");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }
    }
}
