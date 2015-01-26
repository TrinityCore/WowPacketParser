using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class HotfixHandler
    {
        [Parser(Opcode.SMSG_HOTFIX_NOTIFY_BLOB)]
        public static void HandleHotfixInfo(Packet packet)
        {
            var count = packet.ReadBits("Count", 20);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32E<DB2Hash>("Hotfix DB2 File", i);
                packet.ReadTime("Hotfix date", i);
                packet.ReadInt32("Hotfixed entry", i);
            }
        }
    }
}
