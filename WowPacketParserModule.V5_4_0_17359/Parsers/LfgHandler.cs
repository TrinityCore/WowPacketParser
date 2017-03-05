using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class LfgHandler
    {
        [Parser(Opcode.CMSG_DUNGEON_FINDER_GET_SYSTEM_INFO)]
        public static void HandleDungeonFinderGetSystemInfo(Packet packet)
        {
            packet.ReadByte("Unk Byte");
            packet.ReadBit("Unk boolean");
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var guid = new byte[8];

            var bit18 = packet.ReadBit();
            if (bit18)
                packet.StartBitStream(guid, 7, 2, 1, 6, 3, 5, 0, 4);

            var bits30 = packet.ReadBits(17);

            var bits7C = new uint[bits30];
            var bits6C = new uint[bits30];
            var bits40 = new uint[bits30];
            var bits1C = new uint[bits30][];
            var bits2C = new uint[bits30][];
            var bitsC = new uint[bits30][];
            var bit4 = new bool[bits30];
            var bits5C = new uint[bits30];
            var bit3C = new bool[bits30];

            for (var i = 0; i < bits30; i++)
            {
                bits7C[i] = packet.ReadBits(21);
                bits6C[i] = packet.ReadBits(21);
                bits40[i] = packet.ReadBits(19);

                bits1C[i] = new uint[bits40[i]];
                bits2C[i] = new uint[bits40[i]];
                bitsC[i] = new uint[bits40[i]];

                for (var j = 0; j < bits40[i]; j++)
                {
                    bits1C[i][j] = packet.ReadBits(21);
                    bits2C[i][j] = packet.ReadBits(21);
                    bitsC[i][j] = packet.ReadBits(20);
                }

                bit4[i] = packet.ReadBit();
                bits5C[i] = packet.ReadBits(20);
                bit3C[i] = packet.ReadBit();
            }

            var bits20 = packet.ReadBits(20);
            for (var i = 0; i < bits30; i++)
            {
                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);

                for (var j = 0; j < bits6C[i]; j++)
                {
                    packet.ReadInt32("Int34", i, j);
                    packet.ReadInt32("Int34", i, j);
                }

                packet.ReadInt32("Int34", i);

                for (var j = 0; j < bits5C[i]; j++)
                {
                    packet.ReadInt32("Int34", i, j);
                    packet.ReadInt32("Int0", i, j);
                    packet.ReadInt32("Int34", i, j);
                }

                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);

                for (var j = 0; j < bits40[i]; j++)
                {
                    packet.ReadInt32("Int0", i, j);

                    for (var k = 0; k < bitsC[i][j]; k++)
                    {
                        packet.ReadInt32("Int0", i, j, k);
                        packet.ReadInt32("Int34", i, j, k);
                        packet.ReadInt32("Int34", i, j, k);
                    }

                    packet.ReadInt32("Int34", i, j);

                    for (var k = 0; k < bits2C[i][j]; k++)
                    {
                        packet.ReadInt32("Int34", i, j, k);
                        packet.ReadInt32("Int34", i, j, k);
                    }

                    for (var k = 0; k < bits1C[i][j]; k++)
                    {
                        packet.ReadInt32("Int34", i, j, k);
                        packet.ReadInt32("Int34", i, j, k);
                    }

                    packet.ReadInt32("Int34", i, j);
                }

                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);

                for (var j = 0; j < bits7C[i]; j++)
                {
                    packet.ReadInt32("Int34", i, j);
                    packet.ReadInt32("Int34", i, j);
                }

                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);
                packet.ReadInt32("Int34", i);
            }

            for (var i = 0; i < bits20; i++)
            {
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("Int24", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
            }

            if (bit18)
            {
                packet.ParseBitStream(guid, 3, 1, 2, 6, 4, 7, 0, 5);
                packet.WriteGuid("Guid", guid);
            }
        }
    }
}
