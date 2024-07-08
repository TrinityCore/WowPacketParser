using Google.Protobuf.WellKnownTypes;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParserModule.V7_0_3_22248.Enums;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using SplineFacingType = WowPacketParserModule.V6_0_2_19033.Enums.SplineFacingType;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class MovementHandler
    {
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
            packet.ReadPackedGuid128("GUID", idx);
            packet.ReadVector4("Force", idx);
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
            return info;
        }

        public static SplineJump ReadMonsterSplineJumpExtraData(Packet packet, params object[] indexes)
        {
            SplineJump jump = new();
            jump.Gravity = packet.ReadSingle("JumpGravity", indexes);
            jump.StartTime = packet.ReadUInt32("StartTime", indexes);
            jump.Duration = packet.ReadUInt32("Duration", indexes);
            return jump;
        }

        public static void ReadMonsterSplineSpellEffectExtraData(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("TargetGUID", indexes);
            packet.ReadUInt32("SpellVisualID", indexes);
            packet.ReadUInt32("ProgressCurveID", indexes);
            packet.ReadUInt32("ParabolicCurveID", indexes);
            packet.ReadSingle("JumpGravity", indexes);
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
            monsterMove.Flags = packet.ReadUInt32E<SplineFlag>("Flags", indexes).ToUniversal();
            monsterMove.ElapsedTime = packet.ReadInt32("Elapsed", indexes);
            monsterMove.MoveTime = packet.ReadUInt32("MoveTime", indexes);
            packet.ReadUInt32("FadeObjectTime", indexes);

            packet.ReadByte("Mode", indexes);
            monsterMove.TransportGuid = packet.ReadPackedGuid128("TransportGUID", indexes);
            monsterMove.VehicleSeat = packet.ReadSByte("VehicleSeat", indexes);

            packet.ResetBitReader();

            var type = packet.ReadBitsE<SplineFacingType>("Face", 2, indexes);
            var pointsCount = packet.ReadBits("PointsCount", 16, indexes);
            packet.ReadBit("VehicleExitVoluntary", indexes);
            packet.ReadBit("Interpolate", indexes);
            var packedDeltasCount = packet.ReadBits("PackedDeltasCount", 16, indexes);
            var hasSplineFilter = packet.ReadBit("HasSplineFilter", indexes);
            var hasSpellEffectExtraData = packet.ReadBit("HasSpellEffectExtraData", indexes);
            var hasJumpExtraData = packet.ReadBit("HasJumpExtraData", indexes);
            var hasAnimTier = packet.ReadBit("HasAnimTierTransition", indexes);

            if (hasSplineFilter)
                ReadMonsterSplineFilter(packet, indexes, "MonsterSplineFilter");

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

            Vector3 endpos = new Vector3();

            double overallDist = 0.0f;
            for (int i = 0; i < pointsCount; i++)
            {
                var spot = packet.ReadVector3();

                // euclidean distance
                overallDist += Math.Sqrt(Math.Pow(spot.X - pos.X, 2) + Math.Pow(spot.Y - pos.Y, 2) + Math.Pow(spot.Z - pos.Z, 2));

                // client always taking first point
                if (i == 0)
                    endpos = spot;

                monsterMove.Points.Add(spot);
                packet.AddValue("Points", spot, indexes, i);
            }

            var waypoints = new Vector3[packedDeltasCount];
            for (int i = 0; i < packedDeltasCount; i++)
            {
                var packedDeltas = packet.ReadPackedVector3();
                waypoints[i].X = packedDeltas.X;
                waypoints[i].Y = packedDeltas.Y;
                waypoints[i].Z = packedDeltas.Z;
            }

            if (hasSpellEffectExtraData)
                ReadMonsterSplineSpellEffectExtraData(packet, indexes, "MonsterSplineSpellEffectExtra");

            if (hasJumpExtraData)
                monsterMove.Jump = ReadMonsterSplineJumpExtraData(packet, indexes, "MonsterSplineJumpExtraData");

            if (hasAnimTier)
            {
                packet.ReadInt32("TierTransitionID", indexes);
                packet.ReadUInt32("StartTime", indexes);
                packet.ReadUInt32("EndTime", indexes);
                packet.ReadByte("AnimTier", indexes);
            }

            // Calculate mid pos
            var mid = new Vector3
            {
                X = (pos.X + endpos.X) * 0.5f,
                Y = (pos.Y + endpos.Y) * 0.5f,
                Z = (pos.Z + endpos.Z) * 0.5f
            };

            for (var i = 0; i < packedDeltasCount; ++i)
            {
                var vec = new Vector3
                {
                    X = mid.X - waypoints[i].X,
                    Y = mid.Y - waypoints[i].Y,
                    Z = mid.Z - waypoints[i].Z
                };
                monsterMove.PackedPoints.Add(vec);
                packet.AddValue("WayPoints", vec, indexes, i);
            }

            float moveTimeInSec = (float)monsterMove.MoveTime / 1000;
            float speedXY = (float)overallDist / moveTimeInSec;
            packet.AddValue("CalculatedSpeedXY", speedXY, indexes);
        }

        public static void ReadMovementMonsterSpline(Packet packet, Vector3 pos, params object[] indexes)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove;
            monsterMove.Id = packet.ReadUInt32("Id", indexes);
            packet.ResetBitReader();

            packet.ReadBit("CrzTeleport", indexes);
            packet.ReadBits("StopDistanceTolerance", 3, indexes);

            ReadMovementSpline(packet, pos, indexes, "MovementSpline");
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleOnMonsterMove(Packet packet)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove = new();
            monsterMove.Mover = packet.ReadPackedGuid128("MoverGUID");
            Vector3 pos = monsterMove.Position = packet.ReadVector3("Position");

            ReadMovementMonsterSpline(packet, pos, "MovementMonsterSpline");
        }

        [Parser(Opcode.SMSG_BIND_POINT_UPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            packet.ReadVector3("Position");
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadUInt32<MapId>("Map");
            packet.ReadInt32<AreaId>("AreaId");

        }

        [Parser(Opcode.SMSG_VIGNETTE_UPDATE)]
        public static void HandleVignetteUpdate(Packet packet)
        {
            packet.ReadBit("ForceUpdate");
            packet.ReadBit("Unk_Bit_901");

            // VignetteInstanceIDList
            var int1 = packet.ReadUInt32("RemovedCount");
            for (var i = 0; i < int1; ++i)
                packet.ReadPackedGuid128("IDs", i);

            // Added
            var int2 = packet.ReadUInt32("AddedCount");
            for (var i = 0; i < int2; ++i)
                packet.ReadPackedGuid128("IDs", i);

            // Added VignetteClientData
            var int3 = packet.ReadUInt32("VignetteClientDataCount");
            for (var i = 0; i < int3; ++i)
            {
                packet.ReadVector3("Position", i);
                packet.ReadPackedGuid128("ObjGUID", i);
                packet.ReadInt32("VignetteID", i);
                packet.ReadUInt32<AreaId>("AreaID", i);
                packet.ReadUInt32("Unk901_1", i);
                packet.ReadUInt32("Unk901_2", i);
            }

            // Updated
            var int4 = packet.ReadUInt32("UpdatedCount");
            for (var i = 0; i < int4; ++i)
                packet.ReadPackedGuid128("IDs", i);

            // Updated VignetteClientData
            var int5 = packet.ReadUInt32("VignetteClientDataCount");
            for (var i = 0; i < int5; ++i)
            {
                packet.ReadVector3("Position", i);
                packet.ReadPackedGuid128("ObjGUID", i);
                packet.ReadInt32("VignetteID", i);
                packet.ReadUInt32<AreaId>("AreaID", i);
                packet.ReadUInt32("Unk901_1", i);
                packet.ReadUInt32("Unk901_2", i);
            }
        }

        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            packet.ReadVector4("Position");
            packet.ReadUInt32("Reason");
        }

        [Parser(Opcode.SMSG_LOGIN_SET_TIME_SPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            PacketLoginSetTimeSpeed setTime = packet.Holder.LoginSetTimeSpeed = new();
            setTime.ServerTime = packet.ReadPackedTime("ServerTime").ToUniversalTime().ToTimestamp();
            setTime.GameTime = packet.ReadPackedTime("GameTime").ToUniversalTime().ToTimestamp();
            setTime.NewSpeed = packet.ReadSingle("NewSpeed");
            setTime.ServerTimeHolidayOffset = packet.ReadInt32("ServerTimeHolidayOffset");
            setTime.GameTimeHolidayOffset = packet.ReadInt32("GameTimeHolidayOffset");
        }

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            var phaseShift = packet.Holder.PhaseShift = new PacketPhaseShift();
            CoreParsers.MovementHandler.ClearPhases();
            phaseShift.Client = packet.ReadPackedGuid128("Client");
            // PhaseShiftData
            packet.ReadInt32E<PhaseShiftFlags>("PhaseShiftFlags");
            var count = packet.ReadInt32("PhaseShiftCount");
            phaseShift.PersonalGuid = packet.ReadPackedGuid128("PersonalGUID");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32E<PhaseFlags>("PhaseFlags", i);

                var id = packet.ReadUInt16();
                phaseShift.Phases.Add(id);

                if (Settings.UseDBC && DBC.Phase.ContainsKey(id))
                {
                    packet.WriteLine($"[{i}] ID: {id} ({StoreGetters.GetName(StoreNameType.PhaseId, id, false)}) Flags: {(DBCPhaseFlags)DBC.Phase[id].Flags}");
                }
                else
                    packet.AddValue($"ID", id, i);

                CoreParsers.MovementHandler.ActivePhases.Add(id, true);
            }

            if (DBC.Phases.Any())
            {
                foreach (var phaseGroup in DBC.GetPhaseGroups(CoreParsers.MovementHandler.ActivePhases.Keys))
                    packet.WriteLine($"PhaseGroup: {phaseGroup} Phases: {string.Join(" - ", DBC.Phases[phaseGroup])}");
            }
            var visibleMapIDsCount = packet.ReadInt32("VisibleMapIDsCount") / 2;
            for (var i = 0; i < visibleMapIDsCount; ++i)
                phaseShift.VisibleMaps.Add((uint)packet.ReadInt16<MapId>("VisibleMapID", i));
            var preloadMapIDCount = packet.ReadInt32("PreloadMapIDsCount") / 2;
            for (var i = 0; i < preloadMapIDCount; ++i)
                phaseShift.PreloadMaps.Add((uint)packet.ReadInt16<MapId>("PreloadMapID", i));
            var uiMapPhaseIdCount = packet.ReadInt32("UiMapPhaseIDsCount") / 2;
            for (var i = 0; i < uiMapPhaseIdCount; ++i)
                phaseShift.UiMapPhase.Add((uint)packet.ReadInt16("UiMapPhaseId", i));

            CoreParsers.MovementHandler.WritePhaseChanges(packet);
        }

        [Parser(Opcode.CMSG_MOVE_INIT_ACTIVE_MOVER_COMPLETE)]
        public static void HandleMoveInitActiveMoverComplete(Packet packet)
        {
            packet.ReadUInt32("Ticks");
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED)]
        public static void HandleMoveTimeSkipped(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadInt32("Time");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_KNOCK_BACK)]
        [Parser(Opcode.SMSG_MOVE_UPDATE)]
        public static void HandlePlayerMove(Packet packet)
        {
            ReadMovementStats(packet, "MovementStats");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_MOD_MOVEMENT_FORCE_MAGNITUDE)]
        public static void HandleMoveUpdateModMovementForceMagnitude(Packet packet)
        {
            ReadMovementStats(packet, "MovementStats");
            packet.ReadSingle("Speed");
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadSingle("SplineDist");
        }

        [Parser(Opcode.CMSG_MOVE_CHANGE_TRANSPORT)]
        [Parser(Opcode.CMSG_MOVE_DISMISS_VEHICLE)]
        [Parser(Opcode.CMSG_MOVE_FALL_LAND)]
        [Parser(Opcode.CMSG_MOVE_FALL_RESET)]
        [Parser(Opcode.CMSG_MOVE_HEARTBEAT)]
        [Parser(Opcode.CMSG_MOVE_JUMP)]
        [Parser(Opcode.CMSG_MOVE_REMOVE_MOVEMENT_FORCES)]
        [Parser(Opcode.CMSG_MOVE_SET_FACING)]
        [Parser(Opcode.CMSG_MOVE_SET_FACING_HEARTBEAT)]
        [Parser(Opcode.CMSG_MOVE_SET_FLY)]
        [Parser(Opcode.CMSG_MOVE_SET_PITCH)]
        [Parser(Opcode.CMSG_MOVE_SET_RUN_MODE)]
        [Parser(Opcode.CMSG_MOVE_SET_WALK_MODE)]
        [Parser(Opcode.CMSG_MOVE_START_ASCEND)]
        [Parser(Opcode.CMSG_MOVE_START_BACKWARD)]
        [Parser(Opcode.CMSG_MOVE_START_DESCEND)]
        [Parser(Opcode.CMSG_MOVE_START_FORWARD)]
        [Parser(Opcode.CMSG_MOVE_START_PITCH_DOWN)]
        [Parser(Opcode.CMSG_MOVE_START_PITCH_UP)]
        [Parser(Opcode.CMSG_MOVE_START_SWIM)]
        [Parser(Opcode.CMSG_MOVE_START_TURN_LEFT)]
        [Parser(Opcode.CMSG_MOVE_START_TURN_RIGHT)]
        [Parser(Opcode.CMSG_MOVE_START_STRAFE_LEFT)]
        [Parser(Opcode.CMSG_MOVE_START_STRAFE_RIGHT)]
        [Parser(Opcode.CMSG_MOVE_STOP)]
        [Parser(Opcode.CMSG_MOVE_STOP_ASCEND)]
        [Parser(Opcode.CMSG_MOVE_STOP_PITCH)]
        [Parser(Opcode.CMSG_MOVE_STOP_STRAFE)]
        [Parser(Opcode.CMSG_MOVE_STOP_SWIM)]
        [Parser(Opcode.CMSG_MOVE_STOP_TURN)]
        [Parser(Opcode.CMSG_MOVE_DOUBLE_JUMP)]
        public static void HandleClientPlayerMove(Packet packet)
        {
            var stats = ReadMovementStats(packet);
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 };
        }

        [Parser(Opcode.SMSG_MOVE_TELEPORT)]
        public static void HandleMoveTeleport(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
            packet.ReadVector3("Position");
            packet.ReadSingle("Facing");
            packet.ReadByte("PreloadWorld");
            var hasTransport = packet.ReadBit("HasTransport");
            var hasVehicleTeleport = packet.ReadBit("HasVehicleTeleport");

            // VehicleTeleport
            if (hasVehicleTeleport)
            {
                packet.ReadByte("VehicleSeatIndex");
                packet.ReadBit("VehicleExitVoluntary");
                packet.ReadBit("VehicleExitTeleport");
            }

            if (hasTransport)
                packet.ReadPackedGuid128("TransportGUID");
        }

        [Parser(Opcode.CMSG_MOVE_TELEPORT_ACK)]
        public static void HandleMoveTeleportAck(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("AckIndex");
            packet.ReadInt32("MoveTime");
        }

        [Parser(Opcode.SMSG_MOVE_SET_COLLISION_HEIGHT)]
        public static void HandleSetCollisionHeight(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
            packet.ReadSingle("Height");
            packet.ReadSingle("Scale");
            packet.ReadByte("Reason");
            packet.ReadUInt32("MountDisplayID");
            packet.ReadInt32("ScaleDuration");
        }

        [Parser(Opcode.CMSG_MOVE_SET_COLLISION_HEIGHT_ACK)]
        public static void HandleMoveSetCollisionHeightAck(Packet packet)
        {
            V6_0_2_19033.Parsers.MovementHandler.ReadMovementAck(packet, "MovementAck");
            packet.ReadSingle("Height");
            packet.ReadInt32("MountDisplayID");
            packet.ReadByte("Reason");
        }

        [Parser(Opcode.SMSG_MOVE_SET_ACTIVE_MOVER)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_ROOT)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_UNROOT)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_DISABLE_GRAVITY)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_ENABLE_GRAVITY)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_DISABLE_COLLISION)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_ENABLE_COLLISION)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_FEATHER_FALL)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_NORMAL_FALL)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_HOVER)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_UNSET_HOVER)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_WATER_WALK)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_START_SWIM)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_STOP_SWIM)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_RUN_MODE)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_WALK_MODE)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_FLYING)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_UNSET_FLYING)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_LAND_WALK)]
        public static void HandleSplineMoveGravityDisable(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_WALK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_UPDATE_RUN_SPEED)]
        [Parser(Opcode.SMSG_MOVE_UPDATE_RUN_BACK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_UPDATE_SWIM_SPEED)]
        [Parser(Opcode.SMSG_MOVE_UPDATE_SWIM_BACK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_UPDATE_TURN_RATE)]
        [Parser(Opcode.SMSG_MOVE_UPDATE_FLIGHT_SPEED)]
        [Parser(Opcode.SMSG_MOVE_UPDATE_FLIGHT_BACK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_UPDATE_PITCH_RATE)]
        public static void HandleMovementUpdateSpeed(Packet packet)
        {
            ReadMovementStats(packet, "MovementStats");
            packet.ReadSingle("Speed");
        }

        [Parser(Opcode.SMSG_MOVE_SET_RUN_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_SWIM_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_WALK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_RUN_BACK_SPEED)]
        public static void HandleMovementIndexSpeed(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");

            packet.ReadSingle("Speed");
        }

        [Parser(Opcode.SMSG_MOVE_ENABLE_GRAVITY)]
        [Parser(Opcode.SMSG_MOVE_DISABLE_GRAVITY)]
        [Parser(Opcode.SMSG_MOVE_SET_LAND_WALK)]
        [Parser(Opcode.SMSG_MOVE_ROOT)]
        [Parser(Opcode.SMSG_MOVE_SET_CAN_FLY)]
        [Parser(Opcode.SMSG_MOVE_ENABLE_TRANSITION_BETWEEN_SWIM_AND_FLY)]
        [Parser(Opcode.SMSG_MOVE_SET_HOVERING)]
        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_FLY)]
        [Parser(Opcode.SMSG_MOVE_DISABLE_TRANSITION_BETWEEN_SWIM_AND_FLY)]
        [Parser(Opcode.SMSG_MOVE_UNSET_HOVERING)]
        [Parser(Opcode.SMSG_MOVE_UNROOT)]
        [Parser(Opcode.SMSG_MOVE_SET_WATER_WALK)]
        [Parser(Opcode.SMSG_MOVE_SET_FEATHER_FALL)]
        [Parser(Opcode.SMSG_MOVE_SET_NORMAL_FALL)]
        [Parser(Opcode.SMSG_MOVE_SET_IGNORE_MOVEMENT_FORCES)]
        [Parser(Opcode.SMSG_MOVE_UNSET_IGNORE_MOVEMENT_FORCES)]
        public static void HandleMovementIndex(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
        }
    }
}
