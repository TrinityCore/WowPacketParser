using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class AreaTriggerHandler
    {
        public static void ReadAreaTriggerSpline(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("TimeToTarget", indexes);
            packet.ReadInt32("ElapsedTimeForMovement", indexes);

            packet.ResetBitReader();

            var verticesCount = packet.ReadBits("VerticesCount", 16, indexes);

            for (var i = 0; i < verticesCount; ++i)
                packet.ReadVector3("Points", indexes, i);
        }
        public static void ReadAreaTriggerMovementScript(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("SpellScriptID", indexes);
            packet.ReadVector3("Center", indexes);
        }

        public static void ReadAreaTriggerCircularMovement(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            var hasTarget = packet.ReadBit("HasTarget");
            var hasCenter = packet.ReadBit("HasCenter");
            packet.ReadBit("CounterClockwise");
            packet.ReadBit("CanLoop");

            packet.ReadUInt32("TimeToTarget");
            packet.ReadInt32("ElapsedTimeForMovement");
            packet.ReadUInt32("StartDelay");
            packet.ReadSingle("Radius");
            packet.ReadSingle("BlendFromRadius");
            packet.ReadSingle("InitialAngel");
            packet.ReadSingle("ZOffset");

            if (hasTarget)
                packet.ReadPackedGuid128("TargetGUID");

            if (hasCenter)
                packet.ReadVector3("Center");
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_PATH)]
        public static void HandleAreaTriggerReShape(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");

            packet.ResetBitReader();
            var hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline");
            var hasAreaTriggerCircularMovement = packet.ReadBit("HasAreaTriggerCircularMovement");
            var hasAreaTriggerMovementScript = packet.ReadBit("HasAreaTriggerMovementScript");

            if (hasAreaTriggerSpline)
                ReadAreaTriggerSpline(packet, "Spline");

            if (hasAreaTriggerMovementScript)
                ReadAreaTriggerMovementScript(packet, "MovementScript");

            if (hasAreaTriggerCircularMovement)
                ReadAreaTriggerCircularMovement(packet, "CircularMovement");
        }
    }
}
