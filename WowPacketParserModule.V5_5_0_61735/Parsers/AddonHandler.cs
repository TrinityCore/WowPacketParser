using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class AddonHandler
    {
        [Parser(Opcode.CMSG_CHAT_REGISTER_ADDON_PREFIXES)]
        public static void HandleChatRegisterAddonPrefixes(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                packet.ResetBitReader();
                var lengths = (int)packet.ReadBits(5);
                packet.ReadWoWString("Addon", lengths, i);
            }
        }

        [Parser(Opcode.CMSG_CHAT_UNREGISTER_ALL_ADDON_PREFIXES)]
        public static void HandleAddonZero(Packet packet)
        {
        }
    }
}
