using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class LfgHandler
    {
        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var unk48 = packet.ReadBits("unk48", 20);
            var unk40 = packet.ReadBit("unk40");
            var unk16 = packet.ReadBits("unk16", 17);
            var unk84 = new uint[unk16];
            var unk144 = new uint[unk16];
            var unk112 = new uint[unk16];
            var unk128 = new uint[unk16];
            var unk116 = new uint[unk16][];
            var unk100 = new uint[unk16][];
            var unk132 = new uint[unk16][];
            for (var i = 0; i < unk16; i++)
            {
                packet.ReadBit("unk80", i);
                packet.ReadBit("unk24", i);
                unk144[i] = packet.ReadBits("unk144", 21, i);
                unk84[i] = packet.ReadBits("unk84", 19, i);
                unk112[i] = packet.ReadBits("unk112", 20, i);
                unk116[i] = new uint[unk84[i]];
                unk100[i] = new uint[unk84[i]];
                unk132[i] = new uint[unk84[i]];
                for (var j = 0; j < unk84[i]; j++)
                {
                    unk116[i][j] = packet.ReadBits("unk116", 21, i, j);
                    unk100[i][j] = packet.ReadBits("unk100", 20, i, j);
                    unk132[i][j] = packet.ReadBits("unk132", 21, i, j);
                }
                unk128[i] = packet.ReadBits("unk128", 21, i);
            }
            var guid32 = new byte[8];
            if (unk40)
            {
                guid32 = packet.StartBitStream(5, 1, 2, 7, 3, 0, 6, 4);
                packet.ParseBitStream(guid32, 7, 2, 3, 0, 4, 5, 6, 1);
                packet.WriteGuid("guid32", guid32);
            }

            for (var i = 0; i < unk16; i++)
            {
                packet.ReadInt32("unk108", i);
                for (var j = 0; j < unk84[i]; j++)
                {
                    packet.ReadInt32("unk92", i, j);
                    for (var k = 0; k < unk116[i][j]; k++)
                    {
                        packet.ReadInt32("unk", i, j, k);
                        packet.ReadInt32("unk2", i, j, k);
                    }
                    for (var k = 0; k < unk100[i][j]; k++)
                    {
                        packet.ReadInt32("unk", i, j, k);
                        packet.ReadInt32("unk2", i, j, k);
                        packet.ReadInt32("unk3", i, j, k);
                    }
                    packet.ReadInt32("unk88", i, j);
                    for (var k = 0; k < unk132[i][j]; k++)
                    {
                        packet.ReadInt32("unk", i, j, k);
                        packet.ReadInt32("unk2", i, j, k);
                    }
                    packet.ReadInt32("unk96", i, j);
                }
                packet.ReadInt32("unk40", i);
                for (var k = 0; k < unk144[i]; k++)
                {
                    packet.ReadInt32("unk", i, k);
                    packet.ReadInt32("unk2", i, k);
                }
                packet.ReadInt32("unk68", i);
                packet.ReadInt32("unk104", i);
                for (var k = 0; k < unk112[i]; k++)
                {
                    packet.ReadInt32("unk", i, k);
                    packet.ReadInt32("unk2", i, k);
                    packet.ReadInt32("unk3", i, k);
                }
                packet.ReadInt32("unk48", i);
                packet.ReadInt32("unk56", i);
                packet.ReadInt32("unk52", i);
                packet.ReadInt32("unk72", i);
                packet.ReadInt32("unk36", i);
                packet.ReadInt32("unk40", i);
                for (var k = 0; k < unk128[i]; k++)
                {
                    packet.ReadInt32("unk", i, k);
                    packet.ReadInt32("unk2", i, k);
                }
                packet.ReadInt32("unk60", i);
                packet.ReadInt32("unk100", i);
                packet.ReadInt32("unk64", i);
                packet.ReadInt32("unk32", i);
                packet.ReadInt32("unk44", i);
                packet.ReadInt32("unk76", i);
                packet.ReadInt32("unk28", i);
            }

            for (var i = 0; i < unk48; i++)
            {
                packet.ReadInt32("unk52", i);
                packet.ReadInt32("unk56", i);
                packet.ReadInt32("unk60", i);
                packet.ReadInt32("unk64", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_QUEUE_STATUS)]
        public static void HandleLfgQueueStatusUpdate(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
