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
            var count = packet.Translator.ReadInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                var lengths = (int)packet.Translator.ReadBits(5);
                packet.Translator.ResetBitReader();
                packet.Translator.ReadWoWString("Addon", lengths, i);

            }
        }

        [Parser(Opcode.SMSG_ADDON_INFO)]
        public static void HandleServerAddonsList(Packet packet)
        {
            var int4 = packet.Translator.ReadInt32("AddonInfo");
            var int8 = packet.Translator.ReadInt32("BannedAddonInfo");

            for (var i = 0; i < int4; i++)
            {
                packet.Translator.ReadByte("Status", i);

                var bit1 = packet.Translator.ReadBit("InfoProvided", i);
                var bit2 = packet.Translator.ReadBit("KeyProvided", i);
                var bit3 = packet.Translator.ReadBit("UrlProvided", i);

                packet.Translator.ResetBitReader();

                if (bit3)
                {
                    var urlLang = packet.Translator.ReadBits(8);
                    packet.Translator.ReadWoWString("Url", urlLang, i);
                }

                if (bit1)
                {
                    packet.Translator.ReadByte("KeyVersion", i);
                    packet.Translator.ReadInt32("Revision", i);
                }

                if (bit2)
                    packet.Translator.ReadBytes("KeyData", 256, i);
            }

            for (var i = 0; i < int8; i++)
            {
                packet.Translator.ReadInt32("Id", i);

                for (var j = 0; j < 4; j++)
                {
                    packet.Translator.ReadInt32("NameMD5", i, j);
                    packet.Translator.ReadInt32("VersionMD5", i, j);
                }

                packet.Translator.ReadPackedTime("LastModified", i);
                packet.Translator.ReadInt32("Flags", i);
            }
        }
    }
}
