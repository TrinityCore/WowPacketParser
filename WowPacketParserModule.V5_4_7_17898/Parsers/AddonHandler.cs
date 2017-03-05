using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class AddonHandler
    {
        [Parser(Opcode.SMSG_ADDON_INFO)]
        public static void HandleServerAddonsList(Packet packet)
        {
            var bits20 = packet.Translator.ReadBits(23);

            var usePublicKey = new bool[bits20];
            var hasURL = new bool[bits20];
            var bit1 = new bool[bits20];
            var urlLang = new uint[bits20];

            for (var i = 0; i < bits20; i++)
            {
                bit1[i] = packet.Translator.ReadBit();
                hasURL[i] = packet.Translator.ReadBit();
                usePublicKey[i] = packet.Translator.ReadBit();

                if (hasURL[i])
                    urlLang[i] = packet.Translator.ReadBits(8);
            }

            var bits10 = (int)packet.Translator.ReadBits(18);

            for (var i = 0; i < bits20; i++)
            {
                if (usePublicKey[i])
                    packet.Translator.ReadBytes("Name MD5", 256, i);

                if (bit1[i])
                {
                    packet.Translator.ReadInt32("Int24", i);
                    packet.Translator.ReadByte("Byte24", i);
                }

                if (hasURL[i])
                    packet.Translator.ReadWoWString("Addon URL File", urlLang[i], i);

                packet.Translator.ReadByte("Addon State", i);
            }

            for (var i = 0; i < bits10; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    packet.Translator.ReadInt32("IntED", i, j);
                    packet.Translator.ReadInt32("Int14", i, j);
                }

                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
            }
        }

        [Parser(Opcode.CMSG_ADDON_REGISTERED_PREFIXES)]
        public static void MultiplePackets(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 24);
            var lengths = new int[count];
            for (var i = 0; i < count; ++i)
                lengths[i] = (int)packet.Translator.ReadBits(5);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadWoWString("Addon", lengths[i], i);
        }
    }
}
