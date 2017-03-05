using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.CMSG_SAVE_CUF_PROFILES)]
        public static void HandleSaveCufProfiles(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 19);

            var strlen = new uint[count];

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadBit("10 player group", i);
                packet.Translator.ReadBit("25 player group", i);
                packet.Translator.ReadBit("Pets", i);
                packet.Translator.ReadBit("Border", i);
                packet.Translator.ReadBit("Dispellable debuffs", i);
                packet.Translator.ReadBit("15 player group", i);
                packet.Translator.ReadBit("Incoming heals", i);
                packet.Translator.ReadBit("Unk 156", i);
                strlen[i] = packet.Translator.ReadBits("String length", 7, i);
                packet.Translator.ReadBit("Talent spec 1", i);
                packet.Translator.ReadBit("Keep groups together", i);
                packet.Translator.ReadBit("Unk 157", i);
                packet.Translator.ReadBit("2 player group", i);
                packet.Translator.ReadBit("Main tank and assist", i);
                packet.Translator.ReadBit("40 player group", i);
                packet.Translator.ReadBit("Unk 145", i);
                packet.Translator.ReadBit("Display power bars", i);
                packet.Translator.ReadBit("PvE", i);
                packet.Translator.ReadBit("3 player group", i);
                packet.Translator.ReadBit("Class colors", i);
                packet.Translator.ReadBit("Aggro highlight", i);
                packet.Translator.ReadBit("PvP", i);
                packet.Translator.ReadBit("Talent spec 2", i);
                packet.Translator.ReadBit("Debuffs", i);
                packet.Translator.ReadBit("Horizontal Groups", i);
                packet.Translator.ReadBit("5 player group", i);
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadByte("Sort by", i);
                packet.Translator.ReadByte("Unk 146", i);
                packet.Translator.ReadInt16("Unk 154", i);
                packet.Translator.ReadWoWString("Name", (int)strlen[i], i);
                packet.Translator.ReadByte("Unk 147", i);
                packet.Translator.ReadByte("Health text", i);
                packet.Translator.ReadInt16("Unk 152", i);
                packet.Translator.ReadInt16("Unk 150", i);
                packet.Translator.ReadInt16("Frame height", i);
                packet.Translator.ReadByte("Unk 148", i);
                packet.Translator.ReadInt16("Frame width", i);
            }
        }

        [Parser(Opcode.SMSG_LOAD_CUF_PROFILES)]
        public static void HandleLoadCUFProfiles(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 19);

            var strlen = new uint[count];

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadBit("Aggro highlight", i);
                packet.Translator.ReadBit("Unk 145", i);
                packet.Translator.ReadBit("25 player group", i);
                packet.Translator.ReadBit("2 player group", i);
                packet.Translator.ReadBit("Keep groups together", i);
                packet.Translator.ReadBit("Dispellable debuffs", i);
                packet.Translator.ReadBit("Talent spec 1", i);
                packet.Translator.ReadBit("3 player group", i);
                packet.Translator.ReadBit("5 player group", i);
                packet.Translator.ReadBit("40 player group", i);
                packet.Translator.ReadBit("Unk 157", i);
                strlen[i] = packet.Translator.ReadBits("String length", 7, i);
                packet.Translator.ReadBit("Main tank and assist", i);
                packet.Translator.ReadBit("10 player group", i);
                packet.Translator.ReadBit("Debuffs", i);
                packet.Translator.ReadBit("PvP", i);
                packet.Translator.ReadBit("Unk 156", i);
                packet.Translator.ReadBit("Talent spec 2", i);
                packet.Translator.ReadBit("Border", i);
                packet.Translator.ReadBit("Incoming heals", i);
                packet.Translator.ReadBit("Horizontal groups", i);
                packet.Translator.ReadBit("PvE", i);
                packet.Translator.ReadBit("15 player group", i);
                packet.Translator.ReadBit("Class colors", i);
                packet.Translator.ReadBit("Display power bars", i);
                packet.Translator.ReadBit("Pets", i);
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadWoWString("Name", (int)strlen[i], i);
                packet.Translator.ReadInt16("Unk 154", i);
                packet.Translator.ReadByte("Unk 147", i);
                packet.Translator.ReadByte("Sort by", i); // 0 - role, 1 - group, 2 - alphabetical
                packet.Translator.ReadByte("Unk 146", i);
                packet.Translator.ReadInt16("Frame width", i);
                packet.Translator.ReadInt16("Unk 152", i);
                packet.Translator.ReadByte("Health text", i); // 0 - none, 1 - remaining, 2 - lost, 3 - percentage
                packet.Translator.ReadInt16("Unk 150", i);
                packet.Translator.ReadByte("Unk 148", i);
                packet.Translator.ReadInt16("Frame height", i);
            }
        }
    }
}
