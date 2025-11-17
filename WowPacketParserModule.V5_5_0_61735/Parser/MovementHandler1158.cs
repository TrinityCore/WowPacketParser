using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParserModule.V6_0_2_19033.Enums;
using static WowPacketParserModule.V7_0_3_22248.Enums.ProtoExtensions;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class MovementHandler1158
    {
        public static void ReadVignetteData(Packet packet, params object[] idx)
        {
            packet.ReadVector3("Position", idx);
            packet.ReadPackedGuid128("ObjGUID", idx);
            packet.ReadInt32("VignetteID", idx);
            packet.ReadUInt32<AreaId>("ZoneID", idx);
            packet.ReadUInt32("WMOGroupID", idx);
            packet.ReadUInt32("WMODoodadPlacementID", idx);
            packet.ReadSingle("HealthPercent", idx);
            packet.ReadUInt16("RecommendedGroupSizeMin", idx);
            packet.ReadUInt16("RecommendedGroupSizeMax", idx);
        }

        public static void ReadVignetteDataSet(Packet packet, params object[] idx)
        {
            var idCount = packet.ReadUInt32();
            var dataCount = packet.ReadUInt32();

            for (var i = 0u; i < idCount; ++i)
                packet.ReadPackedGuid128("IDs", idx, i);

            // Added VignetteClientData
            for (var i = 0u; i < dataCount; ++i)
                ReadVignetteData(packet, idx, "Data", i);
        }

        public static void ReadMovementForce(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("ID", idx);
            packet.ReadVector3("Origin", idx);
            packet.ReadVector3("Direction", idx);
            packet.ReadUInt32("TransportID", idx);
            packet.ReadSingle("Magnitude", idx);
            packet.ReadInt32("MovementForceID", idx);
            packet.ReadInt32("Unknown1110_1", idx);
            packet.ReadInt32("Unknown1110_2", idx);
            packet.ReadUInt32("Flags", idx);

            packet.ResetBitReader();
            packet.ReadBitsE<MovementForceType>("Type", 2, idx);
        }

        public static void ReadMonsterSplineSpellEffectExtraData(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("TargetGUID", indexes);
            packet.ReadUInt32("SpellVisualID", indexes);
            packet.ReadUInt32("ProgressCurveID", indexes);
            packet.ReadUInt32("ParabolicCurveID", indexes);
            packet.ReadSingle("JumpGravity", indexes);
        }

        public static SplineJump ReadMonsterSplineJumpExtraData(Packet packet, params object[] indexes)
        {
            SplineJump jump = new();
            jump.Gravity = packet.ReadSingle("JumpGravity", indexes);
            jump.StartTime = packet.ReadUInt32("StartTime", indexes);
            jump.Duration = packet.ReadUInt32("Duration", indexes);
            return jump;
        }

        public static void ReadMonsterSplineTurnData(Packet packet, params object[] indexes)
        {
            packet.ReadSingle("StartFacing", indexes);
            packet.ReadSingle("TotalTurnRads", indexes);
            packet.ReadSingle("RadsPerSec", indexes);
        }

        public static void ReadMonsterSplineFilter(Packet packet, params object[] indexes)
        {
            var count = packet.ReadUInt32("MonsterSplineFilterKey", indexes);
            packet.ReadSingle("BaseSpeed", indexes);
            packet.ReadInt16("StartOffset", indexes);
            packet.ReadSingle("DistToPrevFilterKey", indexes);
            packet.ReadInt16("AddedToStart", indexes);

            for (int i = 0; i < count; i++)
            {
                packet.ReadInt16("IDx", indexes, i);
                packet.ReadUInt16("Speed", indexes, i);
            }

            packet.ResetBitReader();
            packet.ReadBits("FilterFlags", 2, indexes);
        }

        public static void ReadMovementSpline(Packet packet, Vector3 pos, params object[] indexes)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove;
            var splineFlag = packet.ReadUInt32E<SplineFlag>("Flags", indexes);
            monsterMove.Flags = splineFlag.ToUniversal();

            var type = packet.ReadByteE<SplineFacingType>("Face", indexes);

            monsterMove.ElapsedTime = packet.ReadInt32("Elapsed", indexes);
            monsterMove.MoveTime = packet.ReadUInt32("MoveTime", indexes);
            packet.ReadUInt32("FadeObjectTime", indexes);

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

            if (hasSplineFilter)
                ReadMonsterSplineFilter(packet, indexes, "MonsterSplineFilter");

            var endpos = new Vector3();
            double distance = 0.0f;
            if (pointsCount > 0)
            {
                var prevpos = pos;
                for (var i = 0; i < pointsCount; ++i)
                {
                    var spot = packet.ReadVector3("Points", indexes, i);
                    monsterMove.Points.Add(spot);
                    distance += Vector3.GetDistance(prevpos, spot);
                    prevpos = spot;

                    // client always taking first point
                    if (i == 0)
                        endpos = spot;
                }
            }

            if (packedDeltasCount > 0)
            {
                // Calculate mid pos
                var mid = (pos + endpos) * 0.5f;

                // ignore distance set by Points array if packed deltas are used
                distance = 0;

                var prevpos = pos;
                for (var i = 0; i < packedDeltasCount; ++i)
                {
                    var vec = mid - packet.ReadPackedVector3();
                    packet.AddValue("WayPoints", vec, indexes, i);
                    monsterMove.PackedPoints.Add(vec);
                    distance += Vector3.GetDistance(prevpos, vec);
                    prevpos = vec;
                }
                distance += Vector3.GetDistance(prevpos, endpos);
            }

            if (hasSpellEffectExtraData)
                ReadMonsterSplineSpellEffectExtraData(packet, indexes, "MonsterSplineSpellEffectExtra");

            if (hasJumpExtraData)
                monsterMove.Jump = ReadMonsterSplineJumpExtraData(packet, indexes, "MonsterSplineJumpExtraData");

            if (hasTurnData)
                ReadMonsterSplineTurnData(packet, indexes, "MonsterSplineTurnData");

            if (hasAnimTier)
            {
                packet.ReadInt32("TierTransitionID", indexes);
                packet.ReadUInt32("StartTime", indexes);
                packet.ReadUInt32("EndTime", indexes);
                packet.ReadByte("AnimTier", indexes);
            }

            if (endpos.X != 0 && endpos.Y != 0 && endpos.Z != 0)
                WowPacketParser.Parsing.Parsers.MovementHandler.PrintComputedSplineMovementParams(packet, distance, monsterMove, indexes);
        }

        public static void ReadMovementMonsterSpline(Packet packet, Vector3 pos, WowGuid guid, params object[] indexes)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove;
            monsterMove.Id = packet.ReadUInt32("Id", indexes);

            packet.ResetBitReader();

            packet.ReadBit("CrzTeleport", indexes);
            packet.ReadBit("StopUseFaceDirection", indexes);
            packet.ReadBits("StopDistanceTolerance", 3, indexes);

            ReadMovementSpline(packet, pos, indexes, "MovementSpline");
        }

        public static MovementInfo.TransportInfo ReadTransportData(Packet packet, params object[] idx)
        {
            var transportInfo = new MovementInfo.TransportInfo();

            transportInfo.Guid = packet.ReadPackedGuid128("Guid", idx);
            transportInfo.Offset = packet.ReadVector4("Position", idx);
            packet.ReadByte("Seat", idx);
            packet.ReadInt32("MoveTime", idx);

            packet.ResetBitReader();
            var hasPrevMoveTime = packet.ReadBit("HasPrevMoveTime", idx);
            var hasVehicleRecID = packet.ReadBit("HasVehicleRecID", idx);

            if (hasPrevMoveTime)
                packet.ReadUInt32("PrevMoveTime", idx);

            if (hasVehicleRecID)
                packet.ReadUInt32("VehicleRecID", idx);

            return transportInfo;
        }

        public static void ReadInertiaData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("ID", idx);
            packet.ReadVector3("Force", idx);
            packet.ReadUInt32("Lifetime", idx);
        }

        public static void ReadAdvFlyingData(Packet packet, params object[] idx)
        {
            packet.ReadSingle("ForwardVelocity", idx);
            packet.ReadSingle("UpVelocity", idx);
        }

        public static void ReadFallData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("FallTime", idx);
            packet.ReadSingle("ZSpeed", idx);

            packet.ResetBitReader();
            var hasFallDirection = packet.ReadBit("HasFallDirection", idx);
            if (hasFallDirection)
            {
                packet.ReadSingle("SinAngle", idx);
                packet.ReadSingle("CosAngle", idx);
                packet.ReadSingle("XYSpeed", idx);
            }
        }

        public static void ReadDriveStatusData(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadSingle("Speed", idx);
            packet.ReadSingle("MovementAngle", idx);
            packet.ReadBit("Accelerating", idx);
            packet.ReadBit("Drifting", idx);
        }

        public static MovementInfo ReadMovementStats(Packet packet, params object[] idx)
        {
            MovementInfo info = new();
            info.MoverGuid = packet.ReadPackedGuid128("MoverGUID", idx);
            info.Flags = (uint)packet.ReadUInt32E<MovementFlag>("MovementFlags", idx);
            info.Flags2 = (uint)packet.ReadUInt32E<MovementFlag2>("MovementFlags2", idx);
            info.Flags3 = (uint)packet.ReadUInt32E<MovementFlag3>("MovementFlags3", idx);
            packet.ReadUInt32("MoveTime", idx);
            var position = packet.ReadVector4("Position", idx);
            info.Position = new Vector3 { X = position.X, Y = position.Y, Z = position.Z };
            info.Orientation = position.O;

            packet.ReadSingle("Pitch", idx);
            packet.ReadSingle("StepUpStartElevation", idx);

            var int152 = packet.ReadInt32("RemoveForcesCount", idx);
            packet.ReadInt32("MoveIndex", idx);

            for (var i = 0; i < int152; i++)
                packet.ReadPackedGuid128("RemoveForcesIDs", idx, i);

            packet.ResetBitReader();

            var hasStandingOnGameObjectGUID = packet.ReadBit("HasStandingOnGameObjectGUID", idx);
            var hasTransport = packet.ReadBit("HasTransportData", idx);
            var hasFall = packet.ReadBit("HasFallData", idx);
            packet.ReadBit("HasSpline", idx);

            packet.ReadBit("HeightChangeFailed", idx);
            packet.ReadBit("RemoteTimeValid", idx);
            var hasInertia = packet.ReadBit("HasInertia", idx);
            var hasAdvFlying = packet.ReadBit("HasAdvFlying", idx);
            var hasDriveStatus = packet.ReadBit("HasDriveStatus", idx);

            if (hasTransport)
                info.Transport = ReadTransportData(packet, idx, "TransportData");

            if (hasStandingOnGameObjectGUID)
                packet.ReadPackedGuid128("StandingOnGameObjectGUID", idx);

            if (hasInertia)
                ReadInertiaData(packet, idx, "Inertia");

            if (hasAdvFlying)
                ReadAdvFlyingData(packet, idx, "AdvFlying");

            if (hasFall)
                ReadFallData(packet, idx, "FallData");

            if (hasDriveStatus)
                ReadDriveStatusData(packet, idx, "DriveStatus");
            return info;
        }

        public static void ReadMoveStateChange(Packet packet, params object[] idx)
        {
            var opcode = packet.ReadInt32();
            var opcodeName = Opcodes.GetOpcodeName(opcode, packet.Direction);
            packet.AddValue("MessageID", $"{opcodeName} (0x{opcode.ToString("X4")})", idx);

            packet.ReadInt32("SequenceIndex", idx);

            packet.ResetBitReader();

            var hasSpeed = packet.ReadBit("HasSpeed", idx);
            var hasSpeedRange = packet.ReadBit("HasSpeedRange", idx);
            var hasKnockBack = packet.ReadBit("HasKnockBack", idx);
            var hasVehicle = packet.ReadBit("HasVehicle", idx);
            var hasCollisionHeight = packet.ReadBit("HasCollisionHeight", idx);
            var hasMovementForce = packet.ReadBit("HasMovementForce", idx);
            var hasMovementForceGUID = packet.ReadBit("HasMovementForceGUID", idx);
            var hasMovementInertiaID = packet.ReadBit("HasMovementInertiaID", idx);;
            var hasMovementInertiaLifetimeMs = packet.ReadBit("HasMovementInertiaLifetimeMs", idx);
            var hasDriveCapabilityID = packet.ReadBit("HasDriveCapabilityRecID", idx);

            if (hasMovementForce)
                ReadMovementForce(packet, "MovementForce", idx);

            if (hasSpeed)
                packet.ReadSingle("Speed", idx);

            if (hasSpeedRange)
            {
                packet.ReadSingle("SpeedRangeMin", idx);
                packet.ReadSingle("SpeedRangeMax", idx);
            }

            if (hasKnockBack)
            {
                packet.ReadSingle("HorzSpeed", idx);
                packet.ReadVector2("Direction", idx);
                packet.ReadSingle("InitVertSpeed", idx);
            }

            if (hasVehicle)
                packet.ReadInt32("VehicleRecID", idx);

            if (hasCollisionHeight)
            {
                packet.ReadSingle("Height", idx);
                packet.ReadSingle("Scale", idx);
                packet.ReadByte("UpdateCollisionHeightReason", idx);
            }

            if (hasMovementForceGUID)
                packet.ReadPackedGuid128("MovementForceGUID", idx);

            if (hasMovementInertiaID)
                packet.ReadUInt32("MovementInertiaID", idx);

            if (hasMovementInertiaLifetimeMs)
                packet.ReadUInt32("MovementInertiaLifetimeMs", idx);

            if (hasDriveCapabilityID)
                packet.ReadInt32("DriveCapabilityRecID", idx);
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE, ClientBranch.Classic)]
        public static void HandleOnMonsterMove(Packet packet)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove = new();
            var moverGuid = packet.ReadPackedGuid128("MoverGUID");
            monsterMove.Mover = moverGuid;
            Vector3 pos = monsterMove.Position = packet.ReadVector3("Position");

            ReadMovementMonsterSpline(packet, pos, moverGuid, "MovementMonsterSpline");
        }
    }
}
