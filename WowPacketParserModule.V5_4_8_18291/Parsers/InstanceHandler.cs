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
            packet.ReadInt32E<MapDifficulty>("Difficulty");
        }

        [Parser(Opcode.CMSG_SAVE_CUF_PROFILES)]
        public static void HandleSaveCufProfiles(Packet packet)
        {
            var count = packet.ReadBits("Count", 19);

            var strlen = new uint[count];

            for (int i = 0; i < count; ++i)
            {
                packet.ReadBit("Talent spec 2", i);                    // 166
                packet.ReadBit("Main tank and assist", i);             // 136
                packet.ReadBit("Power bars", i);                       // 140
                packet.ReadBit("10 player group", i);                  // 161
                packet.ReadBit("3 player group", i);                   // 159
                packet.ReadBit("Unk 156", i);                          // 156
                packet.ReadBit("40 player group", i);                  // 164
                packet.ReadBit("2 player group", i);                   // 158
                packet.ReadBit("Keep groups together", i);             // 134
                packet.ReadBit("Class colors", i);                     // 142
                packet.ReadBit("25 player group", i);                  // 163
                packet.ReadBit("Unk 145", i);                          // 145
                strlen[i] = packet.ReadBits("String length", 7, i);    // 0
                packet.ReadBit("Pets", i);                             // 135
                packet.ReadBit("PvP", i);                              // 167
                packet.ReadBit("Dispellable debuffs", i);              // 139
                packet.ReadBit("Debuffs", i);                          // 144
                packet.ReadBit("15 player group", i);                  // 162
                packet.ReadBit("Unk 157", i);                          // 157
                packet.ReadBit("Border", i);                           // 141
                packet.ReadBit("Horizontal Groups", i);                // 143
                packet.ReadBit("Talent spec 1", i);                    // 165
                packet.ReadBit("5 player group", i);                   // 160
                packet.ReadBit("PvE", i);                              // 168
                packet.ReadBit("Incoming heals", i);                   // 137
                packet.ReadBit("Aggro highlight", i);                  // 138
            }

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt16("Frame height", i);             // 128
                packet.ReadByte("Unk 146", i);                   // 146
                packet.ReadByte("Health text", i);               // 133
                packet.ReadInt16("Frame width", i);              // 130
                packet.ReadByte("Unk 148", i);                   // 148
                packet.ReadByte("Sort by", i);                   // 132
                packet.ReadInt16("Unk 150", i);                  // 150
                packet.ReadWoWString("Name", (int)strlen[i], i); // 0
                packet.ReadByte("Unk 147", i);                   // 147
                packet.ReadInt16("Unk 152", i);                  // 152
                packet.ReadInt16("Unk 154", i);                  // 154
            }
        }

        [Parser(Opcode.SMSG_LOAD_CUF_PROFILES)]
        public static void HandleLoadCUFProfiles(Packet packet)
        {
            var count = packet.ReadBits("Count", 19);

            var strlen = new uint[count];

            for (int i = 0; i < count; ++i)
            {
                packet.ReadBit("Talent spec 1", i);                         // 165
                packet.ReadBit("3 player group", i);                        // 159
                packet.ReadBit("Unk 157", i);                               // 157
                packet.ReadBit("10 player group", i);                       // 161
                packet.ReadBit("40 player group", i);                       // 164
                packet.ReadBit("Border", i);                                // 141
                packet.ReadBit("Class colors", i);                          // 142
                packet.ReadBit("Keep groups together", i);                  // 134
                packet.ReadBit("Display power bars", i);                    // 140
                strlen[i] = packet.ReadBits("String length", 7, i);         // 0
                packet.ReadBit("Pets", i);                                  // 135
                packet.ReadBit("Aggro highlight", i);                       // 138
                packet.ReadBit("Unk 145", i);                               // 145
                packet.ReadBit("PvP", i);                                   // 167
                packet.ReadBit("Unk 156", i);                               // 156
                packet.ReadBit("Main tank and assist", i);                  // 136
                packet.ReadBit("Debuffs", i);                               // 144
                packet.ReadBit("Horizontal groups", i);                     // 143
                packet.ReadBit("Talent spec 2", i);                         // 166
                packet.ReadBit("Incoming heals", i);                        // 137
                packet.ReadBit("Dispellable debuffs", i);                   // 139
                packet.ReadBit("25 player group", i);                       // 163
                packet.ReadBit("PvE", i);                                   // 168
                packet.ReadBit("5 player group", i);                        // 160
                packet.ReadBit("15 player group", i);                       // 162
                packet.ReadBit("2 player group", i);                        // 158
            }

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt16("Unk 152", i);                             // 152
                packet.ReadInt16("Unk 154", i);                             // 154
                packet.ReadByte("Health text", i); // 0 - none, 1 - remaining, 2 - lost, 3 - percentage 133
                packet.ReadWoWString("Name", (int)strlen[i], i);            // 172
                packet.ReadByte("Unk 147", i);                              // 147
                packet.ReadByte("Unk 146", i);                              // 146
                packet.ReadInt16("Frame height", i);                        // 128
                packet.ReadByte("Unk 148", i);                              // 148
                packet.ReadByte("Sort by", i); // 0 - role, 1 - group, 2 - alphabetical 132
                packet.ReadInt16("Frame width", i);                         // 130
                packet.ReadInt16("Unk 150", i);                             // 150
            }
        }
    }
}
