using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class ScenarioHandler
    {
        [Parser(Opcode.SMSG_SCENARIO_STATE)]
        public static void HandleScenarioState(Packet packet)
        {
            packet.ReadInt32("ScenarioID");
            packet.ReadInt32("CurrentStep");
            packet.ReadUInt32<DifficultyId>("DifficultyID");
            packet.ReadUInt32("WaveCurrent");
            packet.ReadUInt32("WaveMax");
            packet.ReadUInt32("TimerDuration");
            var criteriaProgressCount = packet.ReadUInt32("CriteriaProgressCount");
            var bonusObjectiveDataCount = packet.ReadUInt32("BonusObjectiveDataCount");
            var pickedStepsCount = packet.ReadUInt32("PickedStepsCount");
            var spellsCount = packet.ReadUInt32("SpellsCount");

            for (var i = 0; i < pickedStepsCount; i++)
                packet.ReadUInt32("PickedStep", i);

            packet.ResetBitReader();
            packet.ReadBit("ScenarioComplete");

            for (var i = 0; i < criteriaProgressCount; i++)
                AchievementHandler.ReadCriteriaProgress(packet, "CriteriaProgress", i);

            for (var i = 0; i < bonusObjectiveDataCount; i++)
                V7_0_3_22248.Parsers.ScenarioHandler.ReadBonusObjectiveData(packet, "BonusObjectiveData", i);

            for (var i = 0; i < spellsCount; i++)
                V7_0_3_22248.Parsers.ScenarioHandler.ReadScenarioSpellUpdate(packet, "ScenarioSpellUpdate", i);
        }

        [Parser(Opcode.SMSG_SCENARIO_POIS)]
        public static void HandleScenarioPOIs(Packet packet)
        {
            var scenarioPOIDataCount = packet.ReadUInt32("ScenarioPOIDataCount");
            for (var i = 0; i < scenarioPOIDataCount; i++)
            {
                int citeriaTreeId = packet.ReadInt32("CriteriaTreeID");

                var scenarioBlobDataCount = packet.ReadUInt32("ScenarioBlobDataCount");
                for (var j = 0u; j < scenarioBlobDataCount; j++)
                {
                    var scenarioPOI = new ScenarioPOI
                    {
                        CriteriaTreeID = citeriaTreeId
                    };

                    scenarioPOI.BlobIndex = packet.ReadInt32("BlobID", i, j);
                    scenarioPOI.Idx1 = j;
                    scenarioPOI.MapID = packet.ReadInt32<MapId>("MapID", i, j);
                    scenarioPOI.WorldMapAreaId = packet.ReadInt32("UiMapID", i, j);
                    scenarioPOI.Priority = packet.ReadInt32("Priority", i, j);
                    scenarioPOI.Flags = packet.ReadInt32("Flags", i, j);
                    scenarioPOI.WorldEffectID = packet.ReadInt32("WorldEffectID", i, j);
                    scenarioPOI.PlayerConditionID = packet.ReadInt32("PlayerConditionID", i, j);
                    scenarioPOI.NavigationPlayerConditionID = packet.ReadInt32("NavigationPlayerConditionID", i, j);

                    Storage.ScenarioPOIs.Add(scenarioPOI, packet.TimeSpan);

                    var scenarioPOIPointDataCount = packet.ReadUInt32("ScenarioPOIPointDataCount", i, j);
                    for (var k = 0u; k < scenarioPOIPointDataCount; k++)
                    {
                        var scenarioPOIPoint = new ScenarioPOIPoint
                        {
                            CriteriaTreeID = citeriaTreeId
                        };

                        scenarioPOIPoint.Idx1 = j;
                        scenarioPOIPoint.Idx2 = k;
                        scenarioPOIPoint.X = packet.ReadInt32("X", i, j, k);
                        scenarioPOIPoint.Y = packet.ReadInt32("Y", i, j, k);
                        scenarioPOIPoint.Z = packet.ReadInt32("Z", i, j, k);

                        Storage.ScenarioPOIPoints.Add(scenarioPOIPoint, packet.TimeSpan);
                    }
                }
            }
        }
    }
}
