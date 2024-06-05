using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.SMSG_MAIL_QUERY_NEXT_TIME_RESULT)]
        public static void HandleMailQueryNextTimeResult(Packet packet)
        {
            packet.ReadSingle("NextMailTime");

            var nextCount = packet.ReadInt32("NextCount");

            for (int i = 0; i < nextCount; i++)
            {
                packet.ReadPackedGuid128("SenderGUID", i);

                packet.ReadSingle("TimeLeft", i);
                packet.ReadInt32("AltSenderID", i);
                packet.ReadByte("AltSenderType", i);
                packet.ReadInt32("StationeryID", i);
            }
        }

        [Parser(Opcode.CMSG_QUERY_NEXT_MAIL_TIME)]
        public static void HandleNullMail(Packet packet)
        {
        }
    }
}
