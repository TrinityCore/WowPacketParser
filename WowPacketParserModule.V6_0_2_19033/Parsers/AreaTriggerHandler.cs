using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AreaTriggerHandler
    {
        public static void ReadAreaTriggerSpline(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("TimeToTarget", indexes);
            packet.ReadInt32("ElapsedTimeForMovement", indexes);
            var int8 = packet.ReadInt32("VerticesCount", indexes);

            for (var i = 0; i < int8; ++i)
                packet.ReadVector3("Points", indexes, i);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_PATH)]
        public static void HandleAreaTriggerRePath(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");
            ReadAreaTriggerSpline(packet);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_DENIED)]
        public static void HandleAreaTriggerDenied(Packet packet)
        {
            packet.ReadInt32("AreaTriggerID");

            packet.ReadBit("Entered");
        }
    }
}
