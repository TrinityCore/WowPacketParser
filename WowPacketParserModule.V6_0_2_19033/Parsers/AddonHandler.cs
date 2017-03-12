using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AddonHandler
    {
        [Parser(Opcode.CMSG_CHAT_REGISTER_ADDON_PREFIXES)]
        public static void MultiplePackets(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                var lengths = (int)packet.ReadBits(5);
                packet.ResetBitReader();
                packet.ReadWoWString("Addon", lengths, i);

            }
        }
    }
}
