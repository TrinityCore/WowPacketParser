using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.CMSG_REPORT_KEYBINDING_EXECUTION_COUNTS)]
        public static void HandleReportKeybindingExecutionCounts(Packet packet)
        {
            var count = packet.ReadBits("KeyBindingsCount", 10);
            packet.ResetBitReader();

            for (var i = 0; i < count; i++)
            {
                var len1 = packet.ReadBits(6);
                var len2 = packet.ReadBits(6);
                packet.ResetBitReader();
                packet.ReadUInt32("ExecutionCount", i);
                packet.ReadWoWString("Key", len1, i);
                packet.ReadWoWString("Action", len2, i);
            }
        }
    }
}
