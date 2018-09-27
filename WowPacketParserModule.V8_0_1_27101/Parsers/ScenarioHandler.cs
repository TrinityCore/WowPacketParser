using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ScenarioHandler
    {
        [Parser(Opcode.SMSG_SCENARIO_POIS)]
        public static void HandleScenarioPOIs(Packet packet)
        {
            var scenarioPOIDataCount = packet.ReadUInt32("ScenarioPOIDataCount");
            for (var i = 0; i < scenarioPOIDataCount; i++)
            {                
                int citeriaTreeId = packet.ReadInt32("CriteriaTreeID");

                var scenarioBlobDataCount = packet.ReadUInt32("ScenarioBlobDataCount");
                for (uint j = 0; j < scenarioBlobDataCount; j++)
                {
                    ScenarioPOI scenarioPOI = new ScenarioPOI
                    {
                        CriteriaTreeID = citeriaTreeId
                    };

                    scenarioPOI.BlobIndex = packet.ReadInt32("BlobID", i, j);
                    scenarioPOI.Idx1 = j;
                    scenarioPOI.MapID = packet.ReadInt32<MapId>("MapID", i, j);
                    scenarioPOI.WorldMapAreaId = packet.ReadInt32("UiMapID", i, j);
                    scenarioPOI.Priority =packet.ReadInt32("Priority", i, j);
                    scenarioPOI.Flags = packet.ReadInt32("Flags", i, j);
                    scenarioPOI.WorldEffectID = packet.ReadInt32("WorldEffectID", i, j);
                    scenarioPOI.PlayerConditionID = packet.ReadInt32("PlayerConditionID", i, j);

                    Storage.ScenarioPOIs.Add(scenarioPOI, packet.TimeSpan);

                    var scenarioPOIPointDataCount = packet.ReadUInt32("ScenarioPOIPointDataCount", i, j);
                    for (uint k = 0; k < scenarioPOIPointDataCount; k++)
                    {
                        ScenarioPOIPoint scenarioPOIPoint = new ScenarioPOIPoint
                        {
                            CriteriaTreeID = citeriaTreeId
                        };

                        scenarioPOIPoint.Idx1 = j;
                        scenarioPOIPoint.Idx2 = k;
                        scenarioPOIPoint.X = packet.ReadInt32("X", i, j, k);
                        scenarioPOIPoint.Y = packet.ReadInt32("Y", i, j, k);

                        Storage.ScenarioPOIPoints.Add(scenarioPOIPoint, packet.TimeSpan);
                    }
                }
            }
        }
    }
}
