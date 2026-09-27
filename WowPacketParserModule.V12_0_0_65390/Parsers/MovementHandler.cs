using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParserModule.V6_0_2_19033.Enums;
using SplineFlag = WowPacketParserModule.V6_0_2_19033.Enums.SplineFlag;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class MovementHandler
    {
        private struct MovementSplineData
        {
            public Vector3 Destination = new();
            public readonly List<Vector3> Points = [];
            public readonly List<Vector3> PackedDeltas = [];

            public MovementSplineData()
            {
            }
        }

        private static MovementSplineData ReadMovementSpline(Packet packet, params object[] indexes)
        {
            MovementSplineData movementSplineData = new ();
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove;
            var splineFlag = packet.ReadUInt32E<SplineFlag>("Flags", indexes);
            monsterMove.Flags = splineFlag.ToUniversal();

            var type = packet.ReadByteE<SplineFacingType>("Face", indexes);

            monsterMove.ElapsedTime = packet.ReadInt32("Elapsed", indexes);
            monsterMove.MoveTime = packet.ReadUInt32("MoveTime", indexes);
            monsterMove.FadeObjectTime = packet.ReadUInt32("FadeObjectTime", indexes);

            packet.ReadByte("Mode", indexes);

            monsterMove.TransportGuid = packet.ReadPackedGuid128("TransportGUID", indexes);
            monsterMove.VehicleSeat = packet.ReadSByte("VehicleSeat", indexes);

            switch (type)
            {
                case SplineFacingType.Spot:
                    monsterMove.LookPosition = packet.ReadVector3("FaceSpot", indexes);
                    break;
                case SplineFacingType.Target:
                    SplineLookTarget lookTarget = monsterMove.LookTarget = new();
                    lookTarget.Orientation = packet.ReadSingle("FaceDirection", indexes);
                    lookTarget.Target = packet.ReadPackedGuid128("FacingGUID", indexes);
                    break;
                case SplineFacingType.Angle:
                    monsterMove.LookOrientation = packet.ReadSingle("FaceDirection", indexes);
                    break;
                default:
                    break;
            }

            packet.ResetBitReader();

            var pointsCount = packet.ReadBits("PointsCount", 16, indexes);
            packet.ReadBit("VehicleExitVoluntary", indexes);
            packet.ReadBit("Interpolate", indexes);
            var packedDeltasCount = packet.ReadBits("PackedDeltasCount", 16, indexes);
            var hasSplineFilter = packet.ReadBit("HasSplineFilter", indexes);
            var hasSpellEffectExtraData = packet.ReadBit("HasSpellEffectExtraData", indexes);
            var hasJumpExtraData = packet.ReadBit("HasJumpExtraData", indexes);
            var hasTurnData = packet.ReadBit("HasTurnData", indexes);
            var hasAnimTier = packet.ReadBit("HasAnimTierTransition", indexes);
            var hasSpellVisualData = packet.ReadBit("HasSpellVisualData", indexes);

            for (var i = 0; i < pointsCount; ++i)
            {
                var spot = packet.ReadVector3();
                movementSplineData.Points.Add(spot);

                // client always taking first point
                if (i == 0)
                    movementSplineData.Destination = spot;
            }

            for (var i = 0; i < packedDeltasCount; ++i)
                movementSplineData.PackedDeltas.Add(packet.ReadPackedVector3());

            if (hasSplineFilter)
                V8_0_1_27101.Parsers.MovementHandler.ReadMonsterSplineFilter(packet, indexes, "MonsterSplineFilter");

            if (hasSpellEffectExtraData)
                monsterMove.SpellEffect = V8_0_1_27101.Parsers.MovementHandler.ReadMonsterSplineSpellEffectExtraData(packet, indexes, "MonsterSplineSpellEffectExtra");

            if (hasJumpExtraData)
                monsterMove.Jump = V8_0_1_27101.Parsers.MovementHandler.ReadMonsterSplineJumpExtraData(packet, indexes, "MonsterSplineJumpExtraData");

            if (hasTurnData)
                V8_0_1_27101.Parsers.MovementHandler.ReadMonsterSplineTurnData(packet, indexes, "MonsterSplineTurnData");

            if (hasAnimTier)
            {
                packet.ReadInt32("TierTransitionID", indexes);
                monsterMove.AnimTier = packet.ReadByte("AnimTier", indexes);
                packet.ReadUInt32("StartTime", indexes);
                packet.ReadUInt32("EndTime", indexes);
            }

            if (hasSpellVisualData)
            {
                for (var i = 0; i < 16; ++i)
                {
                    packet.ReadInt32("SpellID", indexes, "SpellVisualData", i);
                    V9_0_1_36216.Parsers.SpellHandler.ReadSpellCastVisual(packet, indexes, "SpellVisualData", i, "Visual");
                    packet.ReadInt32("StartNodeIndex", indexes, "SpellVisualData", i);
                }
            }

            return movementSplineData;
        }

        private static MovementSplineData ReadMovementMonsterSpline(Packet packet, WowGuid guid, params object[] indexes)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove;
            monsterMove.Id = packet.ReadUInt32("Id", indexes);

            var splineData = ReadMovementSpline(packet, indexes, "MovementSpline");

            packet.ResetBitReader();
            packet.ReadBit("CrzTeleport", indexes);
            packet.ReadBit("StopUseFaceDirection", indexes);
            packet.ReadBits("StopDistanceTolerance", 3, indexes);

            return splineData;
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleOnMonsterMove(Packet packet)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove = new();
            var moverGuid = packet.ReadPackedGuid128("MoverGUID");
            monsterMove.Mover = moverGuid;
            var splineData = ReadMovementMonsterSpline(packet, moverGuid, "MovementMonsterSpline");
            Vector3 pos = monsterMove.Position = packet.ReadVector3("Position");

            var distance = 0.0;
            if (splineData.Points.Count > 0)
            {
                var prevpos = pos;
                for (var i = 0; i < splineData.Points.Count; ++i)
                {
                    var spot = splineData.Points[i];
                    packet.AddValue("Points", spot, i);
                    monsterMove.Points.Add(spot);
                    distance += Vector3.GetDistance(prevpos, spot);
                    prevpos = spot;
                }
            }

            if (splineData.PackedDeltas.Count > 0)
            {
                // Calculate mid pos
                var mid = (pos + splineData.Destination) * 0.5f;

                // ignore distance set by Points array if packed deltas are used
                distance = 0;

                var prevpos = pos;
                for (var i = 0; i < splineData.PackedDeltas.Count; ++i)
                {
                    var vec = mid - splineData.PackedDeltas[i];
                    packet.AddValue("WayPoints", vec, i);
                    monsterMove.PackedPoints.Add(vec);
                    distance += Vector3.GetDistance(prevpos, vec);
                    prevpos = vec;
                }
                distance += Vector3.GetDistance(prevpos, splineData.Destination);
            }

            if (splineData.Destination.X != 0 && splineData.Destination.Y != 0 && splineData.Destination.Z != 0)
                WowPacketParser.Parsing.Parsers.MovementHandler.PrintComputedSplineMovementParams(packet, distance, monsterMove);
        }

        [Parser(Opcode.SMSG_MOVE_TELEPORT, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleMoveTeleport(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
            packet.ReadVector3("Position");
            packet.ReadSingle("Facing");

            var hasTransport = packet.ReadBit("HasTransport");
            var hasVehicleTeleport = packet.ReadBit("HasVehicleTeleport");
            packet.ReadBit("PreloadWorld");

            if (hasTransport)
                packet.ReadPackedGuid128("TransportGUID");

            if (hasVehicleTeleport)
            {
                packet.ReadByte("VehicleSeatIndex");

                packet.ResetBitReader();
                packet.ReadBit("VehicleExitVoluntary");
                packet.ReadBit("VehicleExitTeleport");
            }
        }
    }
}
