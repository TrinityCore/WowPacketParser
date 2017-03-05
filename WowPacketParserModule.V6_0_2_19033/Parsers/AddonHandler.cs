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

        [Parser(Opcode.SMSG_ADDON_INFO)]
        public static void HandleServerAddonsList(Packet packet)
        {
            var int4 = packet.ReadInt32("AddonInfo");
            var int8 = packet.ReadInt32("BannedAddonInfo");

            for (var i = 0; i < int4; i++)
            {
                packet.ReadByte("Status", i);

                var bit1 = packet.ReadBit("InfoProvided", i);
                var bit2 = packet.ReadBit("KeyProvided", i);
                var bit3 = packet.ReadBit("UrlProvided", i);

                packet.ResetBitReader();

                if (bit3)
                {
                    var urlLang = packet.ReadBits(8);
                    packet.ReadWoWString("Url", urlLang, i);
                }

                if (bit1)
                {
                    packet.ReadByte("KeyVersion", i);
                    packet.ReadInt32("Revision", i);
                }

                if (bit2)
                    packet.ReadBytes("KeyData", 256, i);
            }

            for (var i = 0; i < int8; i++)
            {
                packet.ReadInt32("Id", i);

                for (var j = 0; j < 4; j++)
                {
                    packet.ReadInt32("NameMD5", i, j);
                    packet.ReadInt32("VersionMD5", i, j);
                }

                packet.ReadPackedTime("LastModified", i);
                packet.ReadInt32("Flags", i);
            }
        }
    }
}
