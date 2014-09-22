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
            var count = packet.ReadBits("Count", 19);

            var strlen = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                packet.ReadBit("Unk 1", i);
                packet.ReadBit("Unk 2", i);
                packet.ReadBit("Unk 3", i);
                packet.ReadBit("Unk 4", i);
                packet.ReadBit("Unk 5", i);
                packet.ReadBit("Unk 6", i);
                packet.ReadBit("Unk 7", i);
                packet.ReadBit("Unk 8", i);
                packet.ReadBit("Unk 9", i);
                packet.ReadBit("Unk 10", i);
                packet.ReadBit("Unk 11", i);
                packet.ReadBit("Unk 12", i);
                packet.ReadBit("Unk 13", i);
                packet.ReadBit("Unk 14", i);
                packet.ReadBit("Unk 15", i);
                packet.ReadBit("Unk 16", i);
                packet.ReadBit("Unk 17", i);
                packet.ReadBit("Unk 18", i);
                packet.ReadBit("Unk 19", i);
                packet.ReadBit("Unk 20", i);
                packet.ReadBit("Unk 21", i);
                strlen[i] = packet.ReadBits("String length", 7, i);
                packet.ReadBit("Unk 22", i);
                packet.ReadBit("Unk 23", i);
                packet.ReadBit("Unk 24", i);
                packet.ReadBit("Unk 25", i);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt16("Unk 25", i);
                packet.ReadInt16("Frame height", i);
                packet.ReadByte("Unk 26", i);
                packet.ReadInt16("Frame width", i);
                packet.ReadByte("Unk 27", i);
                packet.ReadInt16("Unk 28", i);
                packet.ReadByte("Unk 29", i);
                packet.ReadByte("Unk 27", i);
                packet.ReadByte("Unk 31", i);
                packet.ReadWoWString("Name", strlen[i], i);
                packet.ReadInt16("Unk 32", i);
            }
        }
    }
}
