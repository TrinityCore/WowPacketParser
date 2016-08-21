using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ScenarioHandler
    {
        [Parser(Opcode.CMSG_QUERY_SCENARIO_POI)]
        public static void HandleQueryScenarioPOI(Packet packet)
        {
            var missingScenarioPOITreeCount = packet.ReadInt32("MissingScenarioPOITreeCount");
            for (var i = 0; i < missingScenarioPOITreeCount; i++)
                packet.ReadInt32("MissingScenarioPOITreeIDs", i);
        }

        [Parser(Opcode.SMSG_SCENARIO_POIS)]
        public static void HandleScenarioPOIs(Packet packet)
        {
            var scenarioPOIDataCount = packet.ReadInt32("ScenarioPOIDataCount");
            for (var i = 0; i < scenarioPOIDataCount; i++)
            {
                packet.ReadInt32("CriteriaTreeID");

                var scenarioBlobDataCount = packet.ReadInt32("ScenarioBlobDataCount");
                for (int j = 0; j < scenarioBlobDataCount; j++)
                {
                    packet.ReadInt32("BlobID", i, j);
                    packet.ReadInt32("MapID", i, j);
                    packet.ReadInt32("WorldMapAreaID", i, j);
                    packet.ReadInt32("Floor", i, j);
                    packet.ReadInt32("Priority", i, j);
                    packet.ReadInt32("Flags", i, j);
                    packet.ReadInt32("WorldEffectID", i, j);
                    packet.ReadInt32("PlayerConditionID", i, j);

                    var scenarioPOIPointDataCount = packet.ReadInt32("ScenarioPOIPointDataCount", i, j);
                    for (int k = 0; k < scenarioPOIPointDataCount; k++)
                    {
                        packet.ReadInt32("X", i, j, k);
                        packet.ReadInt32("Y", i, j, k);
                    }
                }
            }
        }

        [Parser(Opcode.SMSG_SCENARIO_BOOT)]
        public static void HandleScenarioZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_SCENARIO_PROGRESS_UPDATE)]
        public static void HandleScenarioProgressUpdate(Packet packet)
        {
            AchievementHandler.ReadCriteriaProgress(packet, "Progress"); //Time might not fit 
        }

        [Parser(Opcode.SMSG_SCENARIO_STATE)]
        public static void HandleScenarioState(Packet packet)
        {
            packet.ReadInt32("ScenarioID");
            packet.ReadInt32("CurrentStep");
            packet.ReadInt32("DifficultyID");
            packet.ReadInt32("WaveCurrent");
            packet.ReadInt32("WaveMax");
            packet.ReadInt32("TimerDuration");

            var int36 = packet.ReadInt32("CriteriaProgressCount");
            var int20 = packet.ReadInt32("BonusObjectiveDataCount");
            var int46 = packet.ReadInt32("StepProgressIndex");
            var int56 = packet.ReadInt32("7.x Unk 1");

            for (int i = 0; i < int46; i++)
            {
                packet.ReadInt32("Step", i);
            }

            packet.ResetBitReader();
            packet.ReadBit("ScenarioCompleted");

            for (int i = 0; i < int36; i++)
                AchievementHandler.ReadCriteriaProgress(packet, "CriteriaProgress", i);

            for (int i = 0; i < int20; i++)
            {
                packet.ReadInt32("BonusObjectiveID", i);

                packet.ResetBitReader();
                packet.ReadBit("ObjectiveComplete", i);
            }

            for (int i = 0; i < int56; i++)
            {
                packet.ReadInt32("7.x Unk 1 - int", i);

                packet.ResetBitReader();
                packet.ReadBit("7.x Unk 1 - bit", i);
            }
        }

        [Parser(Opcode.SMSG_SCENARIO_COMPLETED)]
        public static void HandleScenarioCompleted(Packet packet)
        {
            packet.ReadInt32("ScenarioID");
        }
    }
}
