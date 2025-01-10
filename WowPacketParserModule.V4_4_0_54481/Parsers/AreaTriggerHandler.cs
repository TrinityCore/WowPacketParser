using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class AreaTriggerHandler
    {
        public static List<AreaTriggerCreatePropertiesSplinePoint> ReadAreaTriggerSpline(AreaTriggerCreateProperties createProperties, Packet packet, params object[] indexes)
        {
            var moveTime = packet.ReadInt32("TimeToTarget", indexes);
            packet.ReadInt32("ElapsedTimeForMovement", indexes);

            packet.ResetBitReader();

            var pointCount = (int)packet.ReadBits("PointsCount", 16, indexes);
            var points = new List<AreaTriggerCreatePropertiesSplinePoint>(pointCount);
            var distance = 0.0;
            var prevPoint = new Vector3();

            for (var i = 0u; i < pointCount; ++i)
            {
                var point = packet.ReadVector3("Points", indexes, i);
                if (createProperties != null)
                {
                    points.Add(new AreaTriggerCreatePropertiesSplinePoint()
                    {
                        areatriggerGuid = createProperties.Guid,
                        Idx = i,
                        X = point.X,
                        Y = point.Y,
                        Z = point.Z
                    });
                }

                if (i > 1 && i < pointCount - 1) // ignore first and last points (dummy spline points)
                    distance += Vector3.GetDistance(prevPoint, point);

                prevPoint = point;
            }

            if (distance > 0)
            {
                packet.AddValue("Computed Distance", distance, indexes);
                double speed = packet.AddValue("Computed Speed", (distance / moveTime) * 1000, indexes);
                if (createProperties != null)
                    createProperties.Speed = (float)speed;
            }

            return points;
        }

        public static AreaTriggerCreatePropertiesOrbit ReadAreaTriggerOrbit(AreaTriggerCreateProperties createProperties, Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            var orbit = new AreaTriggerCreatePropertiesOrbit();
            orbit.CreateProperties = createProperties;

            var hasTarget = packet.ReadBit("HasPathTarget", indexes);
            var hasCenter = packet.ReadBit("HasCenter", indexes);
            orbit.CounterClockwise = packet.ReadBit("CounterClockwise", indexes);
            orbit.CanLoop = packet.ReadBit("CanLoop", indexes);

            var moveTime = packet.ReadUInt32("TimeToTarget", indexes);
            packet.ReadInt32("ElapsedTimeForMovement", indexes);
            orbit.StartDelay = packet.ReadUInt32("StartDelay", indexes);
            orbit.CircleRadius = packet.ReadSingle("Radius", indexes);
            orbit.BlendFromRadius = packet.ReadSingle("BlendFromRadius", indexes);
            orbit.InitialAngle = packet.ReadSingle("InitialAngle", indexes);
            orbit.ZOffset = packet.ReadSingle("ZOffset", indexes);

            if (hasTarget)
                packet.ReadPackedGuid128("PathTarget", indexes);

            if (hasCenter)
                packet.ReadVector3("Center", indexes);

            var distance = 2 * Math.PI * orbit.CircleRadius.Value;
            packet.AddValue("Computed Distance", distance, indexes);
            var speed = packet.AddValue("Computed Speed", (distance / moveTime) * 1000, indexes);
            if (createProperties != null)
                createProperties.Speed = (float)speed;

            return orbit;
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_DENIED)]
        public static void HandleAreaTriggerDenied(Packet packet)
        {
            packet.ReadInt32("AreaTriggerID");
            packet.ReadBit("Entered");
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_MESSAGE)]
        public static void HandleAreaTriggerMessage(Packet packet)
        {
            packet.ReadUInt32("AreaTriggerID");
            packet.ReadUInt32("MessageID");
        }

        public static void ReadAreaTriggerMovementScript(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("SpellScriptID", indexes);
            packet.ReadVector3("Center", indexes);
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_RE_PATH)]
        public static void HandleAreaTriggerReShape(Packet packet)
        {
            var areaTriggerGuid = packet.ReadPackedGuid128("TriggerGUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                packet.ReadPackedGuid128("Unused_1100");

            Storage.Objects.TryGetValue(areaTriggerGuid, out WoWObject createProperties);

            packet.ResetBitReader();
            var hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline");
            var hasAreaTriggerOrbit = packet.ReadBit("HasAreaTriggerOrbit");
            var hasAreaTriggerMovementScript = packet.ReadBit("HasAreaTriggerMovementScript");

            if (hasAreaTriggerSpline)
                ReadAreaTriggerSpline((AreaTriggerCreateProperties)createProperties, packet, "Spline");

            if (hasAreaTriggerMovementScript)
                ReadAreaTriggerMovementScript(packet, "MovementScript");

            if (hasAreaTriggerOrbit)
                ReadAreaTriggerOrbit((AreaTriggerCreateProperties)createProperties, packet, "Orbit");
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.ReadEntry("AreaTriggerID");
            var entered = packet.ReadBit("Entered");
            packet.ReadBit("FromClient");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
            packet.Holder.ClientAreaTrigger = new() { Enter = entered, AreaTrigger = (uint)entry.Key };
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_NO_CORPSE)]
        public static void HandleCorpseNull(Packet packet)
        {
        }
    }
}
