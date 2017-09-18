using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE, ClientVersionBuild.V7_2_5_24330)]
        public static void HandleHighestThreatlistUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadPackedGuid128("HighestThreatGUID");

            var count = packet.ReadUInt32("ThreatListCount");

            // ThreatInfo
            for (var i = 0; i < count; i++)
            {
                packet.ReadPackedGuid128("UnitGUID", i);
                packet.ReadInt64("Threat", i);
            }
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE, ClientVersionBuild.V7_2_5_24330)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            var count = packet.ReadInt32("ThreatListCount");

            for (int i = 0; i < count; i++)
            {
                packet.ReadPackedGuid128("TargetGUID", i);
                packet.ReadInt64("Threat", i);
            }
        }
    }
}
