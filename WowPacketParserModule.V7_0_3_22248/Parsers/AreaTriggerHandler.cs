using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AreaTriggerHandler
    {
        public static List<SpellAreatriggerSpline> ReadAreaTriggerSpline(SpellAreaTrigger areaTrigger, Packet packet, params object[] indexes)
        {
            packet.ReadInt32("TimeToTarget", indexes);
            packet.ReadInt32("ElapsedTimeForMovement", indexes);

            packet.ResetBitReader();

            var pointCount = (int) packet.ReadBits("PointsCount", 16, indexes);
            var points = new List<SpellAreatriggerSpline>(pointCount);

            for (var i = 0u; i < pointCount; ++i)
            {
                var point = packet.ReadVector3("Points", indexes, i);
                if (areaTrigger != null)
                {
                    points.Add(new SpellAreatriggerSpline()
                    {
                        areatriggerGuid = areaTrigger.Guid,
                        Idx = i,
                        X = point.X,
                        Y = point.Y,
                        Z = point.Z
                    });
                }
            }

            return points;
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_PATH)]
        public static void HandleAreaTriggerRePath(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");
            ReadAreaTriggerSpline(null, packet);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_SHAPE)]
        public static void HandleAreaTriggerReShape(Packet packet)
        {
            packet.ReadPackedGuid128("TriggerGUID");

            packet.ResetBitReader();
            var hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline");
            var hasAreaTriggerOrbit = packet.ReadBit("HasAreaTriggerOrbit");

            if (hasAreaTriggerSpline)
                ReadAreaTriggerSpline(null, packet);

            if (hasAreaTriggerOrbit)
                ReadAreaTriggerOrbit(packet, "Orbit");
        }

        public static void ReadAreaTriggerOrbit(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            var hasTarget = packet.ReadBit("HasPathTarget");
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
                packet.ReadPackedGuid128("PathTarget");

            if (hasCenter)
                packet.ReadVector3("Center");
        }
    }
}
