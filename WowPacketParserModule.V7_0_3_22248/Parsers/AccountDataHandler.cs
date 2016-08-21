using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AccountDataHandler
    {

        [Parser(Opcode.CMSG_SAVE_CLIENT_VARIABLES)]
        public static void HandleSaveClientVarables(Packet packet)
        {
            var varablesCount = packet.ReadUInt32("VarablesCount");

            for (var i = 0; i < varablesCount; ++i)
            {
                var variableNameLen = packet.ReadBits(6);
                var valueLen = packet.ReadBits(10);

                packet.WriteLine($" VariableName: \"{ packet.ReadWoWString((int)variableNameLen) }\" Value: \"{ packet.ReadWoWString((int)valueLen) }\"");
            }
        }
    }
}
