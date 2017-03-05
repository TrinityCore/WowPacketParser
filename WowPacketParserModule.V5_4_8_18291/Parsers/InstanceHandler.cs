using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.CMSG_SET_DUNGEON_DIFFICULTY)]
        [Parser(Opcode.CMSG_SET_RAID_DIFFICULTY)]
        [Parser(Opcode.SMSG_RAID_DIFFICULTY_SET)]
        public static void HandleSetDifficulty(Packet packet)
        {
            packet.Translator.ReadInt32E<MapDifficulty>("Difficulty");
        }

        [Parser(Opcode.CMSG_SAVE_CUF_PROFILES)]
        public static void HandleSaveCufProfiles(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 19);

            var strlen = new uint[count];

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadBit("Talent spec 2", i);                    // 166
                packet.Translator.ReadBit("Main tank and assist", i);             // 136
                packet.Translator.ReadBit("Power bars", i);                       // 140
                packet.Translator.ReadBit("10 player group", i);                  // 161
                packet.Translator.ReadBit("3 player group", i);                   // 159
                packet.Translator.ReadBit("Unk 156", i);                          // 156
                packet.Translator.ReadBit("40 player group", i);                  // 164
                packet.Translator.ReadBit("2 player group", i);                   // 158
                packet.Translator.ReadBit("Keep groups together", i);             // 134
                packet.Translator.ReadBit("Class colors", i);                     // 142
                packet.Translator.ReadBit("25 player group", i);                  // 163
                packet.Translator.ReadBit("Unk 145", i);                          // 145
                strlen[i] = packet.Translator.ReadBits("String length", 7, i);    // 0
                packet.Translator.ReadBit("Pets", i);                             // 135
                packet.Translator.ReadBit("PvP", i);                              // 167
                packet.Translator.ReadBit("Dispellable debuffs", i);              // 139
                packet.Translator.ReadBit("Debuffs", i);                          // 144
                packet.Translator.ReadBit("15 player group", i);                  // 162
                packet.Translator.ReadBit("Unk 157", i);                          // 157
                packet.Translator.ReadBit("Border", i);                           // 141
                packet.Translator.ReadBit("Horizontal Groups", i);                // 143
                packet.Translator.ReadBit("Talent spec 1", i);                    // 165
                packet.Translator.ReadBit("5 player group", i);                   // 160
                packet.Translator.ReadBit("PvE", i);                              // 168
                packet.Translator.ReadBit("Incoming heals", i);                   // 137
                packet.Translator.ReadBit("Aggro highlight", i);                  // 138
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt16("Frame height", i);             // 128
                packet.Translator.ReadByte("Unk 146", i);                   // 146
                packet.Translator.ReadByte("Health text", i);               // 133
                packet.Translator.ReadInt16("Frame width", i);              // 130
                packet.Translator.ReadByte("Unk 148", i);                   // 148
                packet.Translator.ReadByte("Sort by", i);                   // 132
                packet.Translator.ReadInt16("Unk 150", i);                  // 150
                packet.Translator.ReadWoWString("Name", (int)strlen[i], i); // 0
                packet.Translator.ReadByte("Unk 147", i);                   // 147
                packet.Translator.ReadInt16("Unk 152", i);                  // 152
                packet.Translator.ReadInt16("Unk 154", i);                  // 154
            }
        }

        [Parser(Opcode.SMSG_LOAD_CUF_PROFILES)]
        public static void HandleLoadCUFProfiles(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 19);

            var strlen = new uint[count];

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadBit("Talent spec 1", i);                         // 165
                packet.Translator.ReadBit("3 player group", i);                        // 159
                packet.Translator.ReadBit("Unk 157", i);                               // 157
                packet.Translator.ReadBit("10 player group", i);                       // 161
                packet.Translator.ReadBit("40 player group", i);                       // 164
                packet.Translator.ReadBit("Border", i);                                // 141
                packet.Translator.ReadBit("Class colors", i);                          // 142
                packet.Translator.ReadBit("Keep groups together", i);                  // 134
                packet.Translator.ReadBit("Display power bars", i);                    // 140
                strlen[i] = packet.Translator.ReadBits("String length", 7, i);         // 0
                packet.Translator.ReadBit("Pets", i);                                  // 135
                packet.Translator.ReadBit("Aggro highlight", i);                       // 138
                packet.Translator.ReadBit("Unk 145", i);                               // 145
                packet.Translator.ReadBit("PvP", i);                                   // 167
                packet.Translator.ReadBit("Unk 156", i);                               // 156
                packet.Translator.ReadBit("Main tank and assist", i);                  // 136
                packet.Translator.ReadBit("Debuffs", i);                               // 144
                packet.Translator.ReadBit("Horizontal groups", i);                     // 143
                packet.Translator.ReadBit("Talent spec 2", i);                         // 166
                packet.Translator.ReadBit("Incoming heals", i);                        // 137
                packet.Translator.ReadBit("Dispellable debuffs", i);                   // 139
                packet.Translator.ReadBit("25 player group", i);                       // 163
                packet.Translator.ReadBit("PvE", i);                                   // 168
                packet.Translator.ReadBit("5 player group", i);                        // 160
                packet.Translator.ReadBit("15 player group", i);                       // 162
                packet.Translator.ReadBit("2 player group", i);                        // 158
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt16("Unk 152", i);                             // 152
                packet.Translator.ReadInt16("Unk 154", i);                             // 154
                packet.Translator.ReadByte("Health text", i); // 0 - none, 1 - remaining, 2 - lost, 3 - percentage 133
                packet.Translator.ReadWoWString("Name", (int)strlen[i], i);            // 172
                packet.Translator.ReadByte("Unk 147", i);                              // 147
                packet.Translator.ReadByte("Unk 146", i);                              // 146
                packet.Translator.ReadInt16("Frame height", i);                        // 128
                packet.Translator.ReadByte("Unk 148", i);                              // 148
                packet.Translator.ReadByte("Sort by", i); // 0 - role, 1 - group, 2 - alphabetical 132
                packet.Translator.ReadInt16("Frame width", i);                         // 130
                packet.Translator.ReadInt16("Unk 150", i);                             // 150
            }
        }
    }
}
