using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.SMSG_MAIL_QUERY_NEXT_TIME_RESULT, ClientVersionBuild.V11_2_5_63506)]
        public static void HandleMailQueryNextTimeResult(Packet packet)
        {
            packet.ReadSingle("NextMailTime");

            var count = packet.ReadUInt32("NextCount");

            for (var i = 0u; i < count; i++)
            {
                packet.ReadPackedGuid128("SenderGUID", i);
                packet.ReadSingle("TimeLeft", i);
                packet.ReadInt32("AltSenderID", i);
                packet.ReadInt32("AltSenderType", i);
                packet.ReadInt32("StationeryID", i);
            }
        }
    }
}
