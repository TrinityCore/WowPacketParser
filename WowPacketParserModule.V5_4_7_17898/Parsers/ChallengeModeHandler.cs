using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class ChallengeModeHandler
    {
        [Parser(Opcode.SMSG_CHALLENGE_MODE_MAP_STATS_UPDATE)]
        public static void HandleChallengeModeMapStatsUpdate(Packet packet)
        {
            var bits40 = 0;

            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadInt32("Int10");
            bits40 = (int)packet.Translator.ReadBits(23);

            for (var i = 0; i < bits40; ++i)
            {
                packet.Translator.ReadInt16("IntEA", i);
            }
        }
        [Parser(Opcode.SMSG_CHALLENGE_MODE_DELETE_LEADER_RESULT)]
        public static void HandleChallengeModeDeleteLeaderResult(Packet packet)
        {
            packet.Translator.ReadInt32("Int5");
            packet.Translator.ReadInt32("Int4");
            packet.Translator.ReadBit("unk");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_ALL_MAP_STATS)]
        public static void HandleChallengeModeAllMapStats(Packet packet)
        {
            var bits10 = 0;
            bits10 = (int)packet.Translator.ReadBits(19);

            var bits30 = new uint[bits10];
            for (var i = 0; i < bits10; ++i)
            {
                bits30[i] = packet.Translator.ReadBits(23);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int15", i);

                for (var j = 0; j < bits30[i]; ++j)
                {
                    packet.Translator.ReadInt16("Int16", i, j);
                }

                packet.Translator.ReadInt32("Int17", i);
                packet.Translator.ReadInt32("Int18", i);
            }
        }
    }
}