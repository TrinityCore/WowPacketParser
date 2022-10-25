using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.CMSG_SET_DUNGEON_DIFFICULTY)]
        [Parser(Opcode.CMSG_SET_RAID_DIFFICULTY)]
        [Parser(Opcode.SMSG_PLAYER_DIFFICULTY_CHANGE)]
        public static void HandleSetDifficulty(Packet packet)
        {
            packet.ReadInt32E<MapDifficulty>("Difficulty");
        }

        [Parser(Opcode.SMSG_INSTANCE_INFO)]
        public static void HandleInstanceInfo(Packet packet)
        {
            var size = packet.ReadBits("LocksCount", 20);
            var instanceIDs = new byte[size][];

            for (int i = 0; i < size; ++i)
            {
                instanceIDs[i] = new byte[8];

                instanceIDs[i][1] = packet.ReadBit();
                packet.ReadBit("Locked", i);
                instanceIDs[i][0] = packet.ReadBit();
                instanceIDs[i][4] = packet.ReadBit();
                instanceIDs[i][2] = packet.ReadBit();
                instanceIDs[i][3] = packet.ReadBit();
                instanceIDs[i][5] = packet.ReadBit();
                instanceIDs[i][6] = packet.ReadBit();
                instanceIDs[i][7] = packet.ReadBit();
                packet.ReadBit("Extended", i);
            }

            for (int i = 0; i < size; ++i)
            {
                packet.ReadInt32("TimeRemaining", i);

                packet.ReadXORByte(instanceIDs[i], 0);
                packet.ReadXORByte(instanceIDs[i], 3);
                packet.ReadInt32<MapId>("MapID", i);
                packet.ReadXORByte(instanceIDs[i], 2);
                packet.ReadXORByte(instanceIDs[i], 4);
                packet.ReadInt32<DifficultyId>("DifficultyID", i);
                packet.ReadXORByte(instanceIDs[i], 7);
                packet.ReadInt32("CompletedMask", i);
                packet.ReadXORByte(instanceIDs[i], 6);
                packet.ReadXORByte(instanceIDs[i], 5);
                packet.ReadXORByte(instanceIDs[i], 1);
                packet.WriteGuid("InstanceID", instanceIDs[i], i);
            }
        }
    }
}
