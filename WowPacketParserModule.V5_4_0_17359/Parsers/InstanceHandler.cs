using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.SMSG_LOAD_CUF_PROFILES)]
        public static void HandleLoadCUFProfiles(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 19);

            var strlen = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadBit("Unk 1", i);
                packet.Translator.ReadBit("Unk 2", i);
                packet.Translator.ReadBit("Unk 3", i);
                packet.Translator.ReadBit("Unk 4", i);
                packet.Translator.ReadBit("Unk 5", i);
                packet.Translator.ReadBit("Unk 6", i);
                packet.Translator.ReadBit("Unk 7", i);
                packet.Translator.ReadBit("Unk 8", i);
                packet.Translator.ReadBit("Unk 9", i);
                packet.Translator.ReadBit("Unk 10", i);
                packet.Translator.ReadBit("Unk 11", i);
                packet.Translator.ReadBit("Unk 12", i);
                packet.Translator.ReadBit("Unk 13", i);
                packet.Translator.ReadBit("Unk 14", i);
                packet.Translator.ReadBit("Unk 15", i);
                packet.Translator.ReadBit("Unk 16", i);
                packet.Translator.ReadBit("Unk 17", i);
                packet.Translator.ReadBit("Unk 18", i);
                packet.Translator.ReadBit("Unk 19", i);
                packet.Translator.ReadBit("Unk 20", i);
                packet.Translator.ReadBit("Unk 21", i);
                strlen[i] = packet.Translator.ReadBits("String length", 7, i);
                packet.Translator.ReadBit("Unk 22", i);
                packet.Translator.ReadBit("Unk 23", i);
                packet.Translator.ReadBit("Unk 24", i);
                packet.Translator.ReadBit("Unk 25", i);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt16("Unk 25", i);
                packet.Translator.ReadInt16("Frame height", i);
                packet.Translator.ReadByte("Unk 26", i);
                packet.Translator.ReadInt16("Frame width", i);
                packet.Translator.ReadByte("Unk 27", i);
                packet.Translator.ReadInt16("Unk 28", i);
                packet.Translator.ReadByte("Unk 29", i);
                packet.Translator.ReadByte("Unk 27", i);
                packet.Translator.ReadByte("Unk 31", i);
                packet.Translator.ReadWoWString("Name", strlen[i], i);
                packet.Translator.ReadInt16("Unk 32", i);
            }
        }
    }
}
