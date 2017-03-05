using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AreaTriggerHandler
    {
        public static void ReadAreaTriggerSpline(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("TimeToTarget", indexes);
            packet.Translator.ReadInt32("ElapsedTimeForMovement", indexes);
            var int8 = packet.Translator.ReadInt32("VerticesCount", indexes);

            for (var i = 0; i < int8; ++i)
                packet.Translator.ReadVector3("Points", indexes, i);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_PATH)]
        public static void HandleAreaTriggerRePath(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TriggerGUID");
            ReadAreaTriggerSpline(packet);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_DENIED)]
        public static void HandleAreaTriggerDenied(Packet packet)
        {
            packet.Translator.ReadInt32("AreaTriggerID");

            packet.Translator.ReadBit("Entered");
        }
    }
}
