using Google.Protobuf.WellKnownTypes;
using System.Linq;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParserModule.V6_0_2_19033.Enums;
using static WowPacketParserModule.V7_0_3_22248.Enums.ProtoExtensions;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class MovementHandler
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
            var hasTurnData = packet.ReadBit("HasTurnData", indexes);
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
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_5_1_63311))
                {
                    packet.ReadInt32("TierTransitionID", indexes);
                    packet.ReadByte("AnimTier", indexes);
                    packet.ReadUInt32("StartTime", indexes);
                    packet.ReadUInt32("EndTime", indexes);
                }
                else
                {
                    packet.ReadInt32("TierTransitionID", indexes);
                    packet.ReadUInt32("StartTime", indexes);
                    packet.ReadUInt32("EndTime", indexes);
                    packet.ReadByte("AnimTier", indexes);
                }
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

        [Parser(Opcode.SMSG_VIGNETTE_UPDATE)]
        public static void HandleVignetteUpdate(Packet packet)
        {
            packet.ReadBit("ForceUpdate");
            packet.ReadBit("InFogOfWar");

            var removedCount = packet.ReadUInt32();

            ReadVignetteDataSet(packet, "Added");
            ReadVignetteDataSet(packet, "Updated");

            for (var i = 0; i < removedCount; ++i)
                packet.ReadPackedGuid128("IDs", i, "Removed");
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

        [Parser(Opcode.SMSG_BIND_POINT_UPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            packet.ReadVector3("Position");
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadUInt32<MapId>("Map");
            packet.ReadInt32<AreaId>("AreaId");

        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            packet.ReadVector4("Position");
            packet.ReadUInt32("Reason");
            packet.ReadVector3("MovementOffset");
            packet.ReadInt32("Counter");

            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            packet.ReadVector4("Position");
            packet.ReadUInt32("Reason");
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            packet.ReadInt32<MapId>("MapID");
            packet.ReadVector3("OldMapPosition");

            packet.ResetBitReader();

            var hasShipTransferPending = packet.ReadBit();
            var hasTransferSpell = packet.ReadBit();
            var hasTaxiPathID = packet.ReadBit();

            if (hasShipTransferPending)
            {
                packet.ReadUInt32<GOId>("ID");
                packet.ReadInt32<MapId>("OriginMapID");
            }

            if (hasTransferSpell)
                packet.ReadUInt32<SpellId>("TransferSpellID");

            if (hasTaxiPathID)
                packet.ReadUInt32("TaxiPathID");
        }

        [Parser(Opcode.SMSG_ADJUST_SPLINE_DURATION)]
        public static void HandleAdjustSplineDuration(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadSingle("Scale");
        }

        [Parser(Opcode.SMSG_CONTROL_UPDATE)]
        public static void HandleClientControlUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadBit("On");
        }

        [Parser(Opcode.SMSG_TRANSFER_ABORTED)]
        public static void HandleTransferAborted(Packet packet)
        {
            packet.ReadInt32<MapId>("MapID");
            packet.ReadByte("Arg");
            packet.ReadInt32("MapDifficultyXConditionID");
            packet.ReadBitsE<TransferAbortReason>("TransfertAbort", 6);
        }

        [Parser(Opcode.SMSG_GAME_TIME_UPDATE)]
        public static void HandleGameTimeUpdate(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");
            packet.ReadPackedTime("GameTime");
            packet.ReadInt32("ServerTimeHolidayOffset");
            packet.ReadInt32("GameTimeHolidayOffset");
        }

        [Parser(Opcode.SMSG_GAME_TIME_SET)]
        public static void HandleGameTimeSet(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");
            packet.ReadPackedTime("GameTime");
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

        [Parser(Opcode.SMSG_TIME_SYNC_REQUEST)]
        public static void HandleTimeSyncReq(Packet packet)
        {
            packet.ReadInt32("Count");
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE, ClientBranch.MoP)]
        public static void HandleOnMonsterMove(Packet packet)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove = new();
            var moverGuid = packet.ReadPackedGuid128("MoverGUID");
            monsterMove.Mover = moverGuid;
            Vector3 pos = monsterMove.Position = packet.ReadVector3("Position");

            ReadMovementMonsterSpline(packet, pos, moverGuid, "MovementMonsterSpline");
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

        [Parser(Opcode.SMSG_MOVE_UPDATE_COLLISION_HEIGHT)]
        public static void HandleMoveUpdateCollisionHeight434(Packet packet)
        {
            ReadMovementStats(packet);
            packet.ReadSingle("Height");
            packet.ReadSingle("Scale");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_KNOCK_BACK)]
        [Parser(Opcode.SMSG_MOVE_UPDATE)]
        public static void HandlePlayerMove(Packet packet)
        {
            ReadMovementStats(packet, "MovementStats");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_TELEPORT)]
        public static void HandleMoveUpdateTeleport(Packet packet)
        {
            ReadMovementStats(packet, "MovementStats");

            var movementForcesCount = packet.ReadUInt32("MovementForcesCount");

            packet.ResetBitReader();

            var hasWalkSpeed = packet.ReadBit("HasWalkSpeed");
            var hasRunSpeed = packet.ReadBit("HasRunSpeed");
            var hasRunBackSpeed = packet.ReadBit("HasRunBackSpeed");
            var hasSwimSpeed = packet.ReadBit("HasSwimSpeed");
            var hasSwimBackSpeed = packet.ReadBit("HasSwimBackSpeed");
            var hasFlightSpeed = packet.ReadBit("HasFlightSpeed");
            var hasFlightBackSpeed = packet.ReadBit("HasFlightBackSpeed");
            var hasTurnRate = packet.ReadBit("HasTurnRate");
            var hasPitchRate = packet.ReadBit("HasPitchRate");

            for (int i = 0; i < movementForcesCount; i++)
                ReadMovementForce(packet, i, "MovementForce");

            if (hasWalkSpeed)
                packet.ReadSingle("WalkSpeed");

            if (hasRunSpeed)
                packet.ReadSingle("RunSpeed");

            if (hasRunBackSpeed)
                packet.ReadSingle("RunBackSpeed");

            if (hasSwimSpeed)
                packet.ReadSingle("SwimSpeed");

            if (hasSwimBackSpeed)
                packet.ReadSingle("SwimBackSpeed");

            if (hasFlightSpeed)
                packet.ReadSingle("FlightSpeed");

            if (hasFlightBackSpeed)
                packet.ReadSingle("FlightBackSpeed");

            if (hasTurnRate)
                packet.ReadSingle("TurnRate");

            if (hasPitchRate)
                packet.ReadSingle("PitchRate");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_MOD_MOVEMENT_FORCE_MAGNITUDE)]
        public static void HandleMoveUpdateModMovementForceMagnitude(Packet packet)
        {
            ReadMovementStats(packet, "MovementStats");
            packet.ReadSingle("Speed");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_APPLY_MOVEMENT_FORCE)]
        public static void HandleMoveUpdateApplyMovementForce(Packet packet)
        {
            ReadMovementStats(packet);
            ReadMovementForce(packet, "MovementForce");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_REMOVE_MOVEMENT_FORCE)]
        public static void HandleMoveUpdateRemoveMovementForce(Packet packet)
        {
            ReadMovementStats(packet);
            packet.ReadPackedGuid128("TriggerGUID");
        }

        [Parser(Opcode.SMSG_MOVE_SET_RUN_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_SWIM_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_WALK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_RUN_BACK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_BACK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_MOD_MOVEMENT_FORCE_MAGNITUDE)]
        [Parser(Opcode.SMSG_MOVE_SET_PITCH_RATE)]
        [Parser(Opcode.SMSG_MOVE_SET_SWIM_BACK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_TURN_RATE)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_AIR_FRICTION)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_MAX_VEL)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_LIFT_COEFFICIENT)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_DOUBLE_JUMP_VEL_MOD)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_GLIDE_START_MIN_HEIGHT)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_ADD_IMPULSE_MAX_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_SURFACE_FRICTION)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_OVER_MAX_DECELERATION)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_LAUNCH_SPEED_COEFFICIENT)]
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
        [Parser(Opcode.SMSG_MOVE_DISABLE_COLLISION)]
        [Parser(Opcode.SMSG_MOVE_DISABLE_DOUBLE_JUMP)]
        [Parser(Opcode.SMSG_MOVE_DISABLE_INERTIA)]
        [Parser(Opcode.SMSG_MOVE_ENABLE_COLLISION)]
        [Parser(Opcode.SMSG_MOVE_ENABLE_DOUBLE_JUMP)]
        [Parser(Opcode.SMSG_MOVE_ENABLE_INERTIA)]
        [Parser(Opcode.SMSG_MOVE_SET_CAN_TURN_WHILE_FALLING)]
        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_TURN_WHILE_FALLING)]
        [Parser(Opcode.SMSG_MOVE_SET_CAN_ADV_FLY)]
        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_ADV_FLY)]
        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_DRIVE)]
        public static void HandleMovementIndex(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
        }

        [Parser(Opcode.SMSG_MOVE_KNOCK_BACK)]
        public static void HandleMoveKnockBack(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
            packet.ReadVector2("Direction");
            packet.ReadSingle("HorzSpeed");
            packet.ReadSingle("VertSpeed");
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

        [Parser(Opcode.SMSG_MOVE_SET_VEHICLE_REC_ID)]
        public static void HandleMoveSetVehicleRecID(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");

            packet.ReadInt32("VehicleRecID");
        }

        [Parser(Opcode.SMSG_MOVE_APPLY_MOVEMENT_FORCE)]
        public static void HandleMoveApplyMovementForce(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");

            ReadMovementForce(packet, "MovementForce");
        }

        [Parser(Opcode.SMSG_MOVE_REMOVE_MOVEMENT_FORCE)]
        public static void HandleMoveRemoveMovementForce(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
            packet.ReadPackedGuid128("TriggerGUID");
        }

        [Parser(Opcode.SMSG_MOVE_SET_COMPOUND_STATE)]
        public static void HandleMoveSetCompoundState(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");

            var moveStateChangeCount = packet.ReadInt32("MoveStateChangeCount");
            for (int i = 0; i < moveStateChangeCount; i++)
                ReadMoveStateChange(packet, "MoveStateChange", i);
        }

        [Parser(Opcode.SMSG_MOVE_SKIP_TIME)]
        public static void HandleMoveSkipTime(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadUInt32("TimeSkipped");
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadSingle("SplineDist");
        }

        [Parser(Opcode.SMSG_MOVE_APPLY_INERTIA)]
        public static void HandleMoveApplyMovementInertia(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadUInt32("SequenceIndex");
            packet.ReadUInt32("IntertiaID");
            packet.ReadUInt32("InertiaLifetimeMs");
        }

        [Parser(Opcode.SMSG_MOVE_REMOVE_INERTIA)]
        public static void HandleMoveRemoveMovementInertia(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadUInt32("SequenceIndex");
            packet.ReadUInt32("IntertiaID");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_APPLY_INERTIA)]
        public static void HandleMoveUpdateApplyMovementInertia(Packet packet)
        {
            ReadMovementStats(packet, "Status");
            packet.ReadInt32("InertiaID");
            packet.ReadUInt32("InertiaInitialLifetimeMs");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_REMOVE_INERTIA)]
        public static void HandleMoveUpdateRemoveMovementInertia(Packet packet)
        {
            ReadMovementStats(packet, "Status");
            packet.ReadInt32("InertiaID");
        }

        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_BANKING_RATE)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_PITCHING_RATE_DOWN)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_PITCHING_RATE_UP)]
        [Parser(Opcode.SMSG_MOVE_SET_ADV_FLYING_TURN_VELOCITY_THRESHOLD)]
        public static void HandleMovementIndexSpeedRange(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");

            packet.ReadSingle("SpeedMin");
            packet.ReadSingle("SpeedMax");
        }

        [Parser(Opcode.SMSG_MOVE_SET_CAN_DRIVE)]
        public static void HandleMoveSetCanDrive(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
            packet.ReadInt32("DriveCapabilityID");
        }

        [Parser(Opcode.SMSG_ABORT_NEW_WORLD)]
        public static void HandleMovementZero(Packet packet)
        {
        }
    }
}
