using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
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

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_PATH)]
        public static void HandleAreaTriggerRePath(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");
            ReadAreaTriggerSpline(packet);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_SHAPE)]
        public static void HandleAreaTriggerReShape(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");

            packet.ResetBitReader();
            var hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline");
            var hasAreaTriggerCircularMovement = packet.ReadBit("HasAreaTriggerCircularMovement");

            if (hasAreaTriggerSpline)
                ReadAreaTriggerSpline(packet);

            if (hasAreaTriggerCircularMovement)
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
        }
    }
}
