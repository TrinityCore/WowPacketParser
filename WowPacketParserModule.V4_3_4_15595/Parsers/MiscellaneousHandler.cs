using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_3_4_15595.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_NOTIFICATION)]
        public static void HandleNotification(Packet packet)
        {
            var length = packet.ReadBits(13);
            packet.ReadWoWString("Message", length);
        }

        [Parser(Opcode.CMSG_REQUEST_HONOR_STATS)]
        public static void HandleRequestHonorStats(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 7, 3, 2, 4, 0, 6);

            packet.ParseBitStream(guid, 4, 7, 0, 5, 1, 6, 2, 3);

            packet.WriteGuid("Guid", guid);
        }
    }
}
