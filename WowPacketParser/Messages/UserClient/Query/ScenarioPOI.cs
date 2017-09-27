using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Query
{
    public unsafe struct ScenarioPOI
    {
        public List<int> MissingScenarioPOITreeIDs;

        [Parser(Opcode.CMSG_QUERY_SCENARIO_POI)]
        public static void HandleQueryScenarioPOI(Packet packet)
        {
            var missingScenarioPOITreeCount = packet.ReadInt32("MissingScenarioPOITreeCount");
            for (var i = 0; i < missingScenarioPOITreeCount; i++)
                packet.ReadInt32("MissingScenarioPOITreeIDs", i);
        }
    }
}
