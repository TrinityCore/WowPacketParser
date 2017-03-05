using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class HotfixHandler
    {
        [Parser(Opcode.SMSG_HOTFIX_NOTIFY_BLOB)]
        public static void HandleHotfixInfo(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 20);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadTime("Hotfix date", i);
                packet.Translator.ReadInt32("Hotfixed entry", i);
                packet.Translator.ReadInt32E<DB2Hash>("Hotfix DB2 File", i);
            }
        }
        

    }
}
