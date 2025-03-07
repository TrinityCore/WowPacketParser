using System;
using System.Linq;
using Google.Protobuf.WellKnownTypes;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using SplineFacingType = WowPacketParserModule.V6_0_2_19033.Enums.SplineFacingType;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class MovementHandler
    {
        public static MovementInfo ReadMovementAck(Packet packet, params object[] idx)
        {
            var stats = Substructures.MovementHandler.ReadMovementStats(packet, idx);
            packet.ReadInt32("AckIndex", idx);
            return stats;
        }

        public static void ReadMovementForce(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("ID", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_0_3_22248)) // might be earlier
                packet.ReadVector3("Origin", idx);
            packet.ReadVector3("Direction", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802) && ClientVersion.RemovedInVersion(ClientVersionBuild.V7_0_3_22248)) // correct?
                packet.ReadVector3("TransportPosition", idx);
            packet.ReadUInt32("TransportID", idx);
            packet.ReadSingle("Magnitude", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_5_43903))
                packet.ReadInt32("MovementForceID", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_0_59347))
            {
                packet.ReadInt32("Unknown1110_1", idx);
                packet.ReadInt32("Unknown1110_2", idx);
                packet.ReadUInt32("Flags", idx);
            }

            packet.ResetBitReader();
            packet.ReadBitsE<MovementForceType>("Type", 2, idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185) && ClientVersion.RemovedInVersion(ClientVersionBuild.V9_2_5_43903))
                if (packet.ReadBit())
                    packet.ReadInt32("MovementForceID", idx);
        }

        public static void ReadMoveStateChange(Packet packet, params object[] idx)
        {
            var opcode = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_5_57171))
                opcode = packet.ReadInt32();
            else
                packet.ReadInt16();

            var opcodeName = Opcodes.GetOpcodeName(opcode, packet.Direction);
            packet.AddValue("MessageID", $"{opcodeName} (0x{opcode:X4})", idx);

            packet.ReadInt32("SequenceIndex", idx);

            packet.ResetBitReader();

            var hasSpeed = packet.ReadBit("HasSpeed", idx);
            var hasRange = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_0_46181))
                hasRange = packet.ReadBit("HasRange", idx);
            var hasKnockBack = packet.ReadBit("HasKnockBack", idx);
            var hasVehicle = packet.ReadBit("HasVehicle", idx);
            var hasCollisionHeight = packet.ReadBit("HasCollisionHeight", idx);
            var hasMovementForce = packet.ReadBit("HasMovementForce", idx);
            var hasMovementForceGUID = packet.ReadBit("HasMovementForceGUID", idx);
            var hasMovementInertiaGUID = false;
            var hasMovementInertiaID = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_0_46181))
                hasMovementInertiaID = packet.ReadBit("HasMovementInertiaID", idx);
            else
                hasMovementInertiaGUID = packet.ReadBit("HasMovementInertiaGUID", idx);

            var hasMovementInertiaLifetimeMs = packet.ReadBit("HasMovementInertiaLifetimeMs", idx);
            var hasDriveCapabilityID = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_0_59347))
                hasDriveCapabilityID = packet.ReadBit("HasDriveCapabilityRecID", idx);

            if (hasSpeed)
                packet.ReadSingle("Speed", idx);

            if (hasRange)
            {
                packet.ReadSingle("SpeedMin", idx);
                packet.ReadSingle("SpeedMax", idx);
            }

            if (hasKnockBack)
            {
                packet.ReadSingle("HorzSpeed", idx);
                packet.ReadVector2("InitVertSpeed", idx);
                packet.ReadSingle("InitVertSpeed", idx);
            }

            if (hasVehicle)
                packet.ReadInt32("VehicleRecID", idx);

            if (hasCollisionHeight)
            {
                packet.ReadSingle("Height", idx);
                packet.ReadSingle("Scale", idx);
                packet.ReadBits("UpdateCollisionHeightReason", 2, idx);
            }

            if (hasMovementForce)
                ReadMovementForce(packet, "MovementForce", idx);

            if (hasMovementForceGUID)
                packet.ReadPackedGuid128("MovementForceGUID", idx);

            if (hasMovementInertiaGUID)
                packet.ReadPackedGuid128("MovementInertiaGUID", idx);

            if (hasMovementInertiaID)
                packet.ReadInt32("MovementInertiaID", idx);

            if (hasMovementInertiaLifetimeMs)
                packet.ReadUInt32("MovementInertiaLifetimeMs", idx);

            if (hasDriveCapabilityID)
                packet.ReadInt32("DriveCapabilityRecID", idx);
        }

        [Parser(Opcode.CMSG_WORLD_PORT_RESPONSE)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            packet.ReadVector4("Position");
            packet.ReadUInt32("Reason");

            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
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

        [Parser(Opcode.SMSG_GAME_TIME_UPDATE)]
        public static void HandleGameTimeUpdate(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");
            packet.ReadPackedTime("GameTime");
            packet.ReadInt32("ServerTimeHolidayOffset");
            packet.ReadInt32("GameTimeHolidayOffset");
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED)]
        public static void HandleMoveTimeSkipped(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadInt32("Time");
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
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLY)]
        [Parser(Opcode.CMSG_MOVE_START_DRIVE_FORWARD)]
        public static void HandleClientPlayerMove(Packet packet)
        {
            var stats = Substructures.MovementHandler.ReadMovementStats(packet);
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 };
        }


        [Parser(Opcode.SMSG_MOVE_UPDATE_KNOCK_BACK)]
        [Parser(Opcode.SMSG_MOVE_UPDATE)]
        public static void HandlePlayerMove(Packet packet)
        {
            Substructures.MovementHandler.ReadMovementStats(packet, "MovementStats");
        }

        public static void ReadMonsterSplineFilter(Packet packet, params object[] indexes)
        {
            var count = packet.ReadUInt32("MonsterSplineFilterKey", indexes);
            packet.ReadSingle("BaseSpeed", indexes);
            packet.ReadUInt16("StartOffset", indexes);
            packet.ReadSingle("DistToPrevFilterKey", indexes);

            for (int i = 0; i < count; i++)
            {
                packet.ReadInt16("IDx", indexes, i);
                packet.ReadUInt16("Speed", indexes, i);
            }

            packet.ReadUInt16("AddedToStart", indexes);

            packet.ResetBitReader();
            packet.ReadBits("FilterFlags", 2, indexes);
        }

        public static void ReadMovementSpline(Packet packet, Vector3 pos, params object[] indexes)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove;
            SplineJump jump = monsterMove.Jump = new();
            monsterMove.Flags = packet.ReadInt32E<SplineFlag434>("Flags", indexes).ToUniversal();
            packet.ReadByte("AnimTier", indexes);
            packet.ReadUInt32("TierTransStartTime", indexes);
            monsterMove.ElapsedTime = packet.ReadInt32("Elapsed", indexes);
            monsterMove.MoveTime = packet.ReadUInt32("MoveTime", indexes);
            jump.Gravity = packet.ReadSingle("JumpGravity", indexes);
            jump.Duration = packet.ReadUInt32("SpecialTime", indexes);
            var pointsCount = packet.ReadInt32("PointsCount", indexes);

            packet.ReadByte("Mode", indexes);
            packet.ReadByte("VehicleExitVoluntary", indexes);

            monsterMove.TransportGuid = packet.ReadPackedGuid128("TransportGUID", indexes);
            monsterMove.VehicleSeat = packet.ReadSByte("VehicleSeat", indexes);

            var packedDeltasCount = packet.ReadInt32("PackedDeltasCount", indexes);

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

            packet.ResetBitReader();
            var type = packet.ReadBitsE<SplineFacingType>("Face", 2, indexes);
            var monsterSplineFilter = packet.ReadBit("HasMonsterSplineFilter", indexes);

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
            }

            if (monsterSplineFilter)
                ReadMonsterSplineFilter(packet, indexes, "MonsterSplineFilter");

            if (endpos.X != 0 && endpos.Y != 0 && endpos.Z != 0)
            {
                packet.AddValue("Computed Distance", distance, indexes);
                packet.AddValue("Computed Speed", (distance / monsterMove.MoveTime) * 1000, indexes);
            }
        }

        public static void ReadMovementMonsterSpline(Packet packet, Vector3 pos, params object[] indexes)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove;
            monsterMove.Id = packet.ReadUInt32("Id", indexes);
            monsterMove.Destination = packet.ReadVector3("Destination", indexes);

            ReadMovementSpline(packet, pos, indexes, "MovementSpline");

            packet.ResetBitReader();

            packet.ReadBit("CrzTeleport", indexes);
            packet.ReadBits("UnkBit", 2, indexes);
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleOnMonsterMove(Packet packet)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove = new();
            monsterMove.Mover = packet.ReadPackedGuid128("MoverGUID");
            Vector3 pos = monsterMove.Position = packet.ReadVector3("Position");

            ReadMovementMonsterSpline(packet, pos, "MovementMonsterSpline");
        }

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            var phaseShift = packet.Holder.PhaseShift = new PacketPhaseShift();
            CoreParsers.MovementHandler.ClearPhases();

            phaseShift.Client = packet.ReadPackedGuid128("Client");

            // PhaseShiftData
            packet.ReadInt32("PhaseShiftFlags");
            var count = packet.ReadInt32("PhaseShiftCount");
            phaseShift.PersonalGuid = packet.ReadPackedGuid128("PersonalGUID");
            for (var i = 0; i < count; ++i)
            {
                var flags = packet.ReadUInt16("PhaseFlags", i);
                var id = packet.ReadUInt16("Id", i);
                phaseShift.Phases.Add(id);
                CoreParsers.MovementHandler.ActivePhases.Add(id, true);
            }

            if (Settings.UseDBC && DBC.Phases.Any())
            {
                foreach (var phaseGroup in DBC.GetPhaseGroups(CoreParsers.MovementHandler.ActivePhases.Keys))
                    packet.WriteLine($"PhaseGroup: { phaseGroup } Phases: { string.Join(" - ", DBC.Phases[phaseGroup]) }");
            }

            var preloadMapIDCount = packet.ReadInt32("PreloadMapIDsCount") / 2;
            for (var i = 0; i < preloadMapIDCount; ++i)
                phaseShift.PreloadMaps.Add((uint)packet.ReadInt16<MapId>("PreloadMapID", i));

            var uiWorldMapAreaIDSwapsCount = packet.ReadInt32("UiWorldMapAreaIDSwap") / 2;
            for (var i = 0; i < uiWorldMapAreaIDSwapsCount; ++i)
                phaseShift.UiMapPhase.Add((uint)packet.ReadInt16("UiWorldMapAreaIDSwaps", i));

            var visibleMapIDsCount = packet.ReadInt32("VisibleMapIDsCount") / 2;
            for (var i = 0; i < visibleMapIDsCount; ++i)
                phaseShift.VisibleMaps.Add((uint)packet.ReadInt16<MapId>("VisibleMapID", i));

            CoreParsers.MovementHandler.WritePhaseChanges(packet);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_RUN_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_SWIM_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_RUN_BACK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_WALK_SPEED)]
        public static void HandleSplineSetSpeed(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadSingle("Speed");
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadSingle("SplineDist");
        }

        [Parser(Opcode.SMSG_VIGNETTE_UPDATE)]
        public static void HandleVignetteUpdate(Packet packet)
        {
            packet.ReadBit("ForceUpdate");

            // VignetteInstanceIDList
            var int1 = packet.ReadInt32("RemovedCount");
            for (var i = 0; i < int1; ++i)
                packet.ReadPackedGuid128("IDs", i);

            // Added
            var int2 = packet.ReadInt32("AddedCount");
            for (var i = 0; i < int2; ++i)
                packet.ReadPackedGuid128("IDs", i);

            // VignetteClientData
            var int3 = packet.ReadInt32("VignetteClientDataCount");
            for (var i = 0; i < int3; ++i)
            {
                packet.ReadVector3("Position", i);
                packet.ReadPackedGuid128("ObjGUID", i);
                packet.ReadInt32("VignetteID", i);
                packet.ReadInt32("Unk", i);
            }

            // Updated
            var int4 = packet.ReadInt32("UpdatedCount");
            for (var i = 0; i < int4; ++i)
                packet.ReadPackedGuid128("IDs", i);

            // VignetteClientData
            var int5 = packet.ReadInt32("VignetteClientDataCount");
            for (var i = 0; i < int5; ++i)
            {
                packet.ReadVector3("Position", i);
                packet.ReadPackedGuid128("ObjGUID", i);
                packet.ReadInt32("VignetteID", i);
                packet.ReadInt32("Unk", i);
            }
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            packet.ReadInt32<MapId>("MapID");

            packet.ResetBitReader();

            var hasShipTransferPending = packet.ReadBit();
            var hasTransferSpell = packet.ReadBit();

            if (hasShipTransferPending)
            {
                packet.ReadUInt32<GOId>("ID");
                packet.ReadInt32<MapId>("OriginMapID");
            }

            if (hasTransferSpell)
                packet.ReadUInt32<SpellId>("TransferSpellID");
        }

        [Parser(Opcode.SMSG_TRANSFER_ABORTED)]
        public static void HandleTransferAborted(Packet packet)
        {
            packet.ReadInt32<MapId>("MapID");
            packet.ReadByte("Arg");
            packet.ReadBitsE<TransferAbortReason>("TransfertAbort", 5);
        }

        [Parser(Opcode.SMSG_ABORT_NEW_WORLD)]
        public static void HandleAbortNewWorld(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_TELEPORT)]
        public static void HandleMoveUpdateTeleport(Packet packet)
        {
            Substructures.MovementHandler.ReadMovementStats(packet);

            var int32 = packet.ReadInt32("MovementForcesCount");
            for (int i = 0; i < int32; i++)
                ReadMovementForce(packet, i, "MovementForce");

            packet.ResetBitReader();

            var bit260 = packet.ReadBit("HasWalkSpeed");
            var bit276 = packet.ReadBit("HasRunSpeed");
            var bit52 = packet.ReadBit("HasRunBackSpeed");
            var bit28 = packet.ReadBit("HasSwimSpeed");
            var bit76 = packet.ReadBit("HasSwimBackSpeed");
            var bit20 = packet.ReadBit("HasFlightSpeed");
            var bit268 = packet.ReadBit("HasFlightBackSpeed");
            var bit60 = packet.ReadBit("HasTurnRate");
            var bit68 = packet.ReadBit("HasPitchRate");

            if (bit260)
                packet.ReadSingle("WalkSpeed");

            if (bit276)
                packet.ReadSingle("RunSpeed");

            if (bit52)
                packet.ReadSingle("RunBackSpeed");

            if (bit28)
                packet.ReadSingle("SwimSpeed");

            if (bit76)
                packet.ReadSingle("SwimBackSpeed");

            if (bit20)
                packet.ReadSingle("FlightSpeed");

            if (bit268)
                packet.ReadSingle("FlightBackSpeed");

            if (bit60)
                packet.ReadSingle("TurnRate");

            if (bit68)
                packet.ReadSingle("PitchRate");
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
        [Parser(Opcode.SMSG_MOVE_ENABLE_INERTIA)]
        [Parser(Opcode.SMSG_MOVE_DISABLE_INERTIA)]
        [Parser(Opcode.SMSG_MOVE_SET_CAN_ADV_FLY)]
        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_ADV_FLY)]
        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_DRIVE)]
        public static void HandleMovementIndex(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
        }

        [Parser(Opcode.SMSG_MOVE_SET_RUN_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_SWIM_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_WALK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_RUN_BACK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_MOD_MOVEMENT_FORCE_MAGNITUDE)]
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

        [Parser(Opcode.SMSG_MOVE_SET_VEHICLE_REC_ID)]
        public static void HandleMoveSetVehicleRecID(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");

            packet.ReadInt32("VehicleRecID");
        }

        [Parser(Opcode.SMSG_MOVE_SET_COLLISION_HEIGHT)]
        public static void HandleSetCollisionHeight(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
            packet.ReadSingle("Scale");
            packet.ReadSingle("Height");
            packet.ReadInt32("MountDisplayID");

            packet.ResetBitReader();

            packet.ReadBits("Reason", 2);
        }

        [Parser(Opcode.SMSG_MOVE_TELEPORT)]
        public static void HandleMoveTeleport(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
            packet.ReadVector3("Position");
            packet.ReadSingle("Facing");

            var bit72 = packet.ReadBit("HasTransport");
            var bit56 = packet.ReadBit("HasVehicleTeleport");

            if (bit72)
                packet.ReadPackedGuid128("TransportGUID");

            // VehicleTeleport
            if (bit56)
            {
                packet.ReadByte("VehicleSeatIndex");
                packet.ReadBit("VehicleExitVoluntary");
                packet.ReadBit("VehicleExitTeleport");
            }
        }

        [Parser(Opcode.CMSG_MOVE_GRAVITY_DISABLE_ACK)]
        [Parser(Opcode.CMSG_MOVE_GRAVITY_ENABLE_ACK)]
        [Parser(Opcode.CMSG_MOVE_HOVER_ACK)]
        [Parser(Opcode.CMSG_MOVE_KNOCK_BACK_ACK)]
        [Parser(Opcode.CMSG_MOVE_WATER_WALK_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_ROOT_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_UNROOT_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_CAN_FLY_ACK)]
        [Parser(Opcode.CMSG_MOVE_ENABLE_SWIM_TO_FLY_TRANS_ACK)]
        [Parser(Opcode.CMSG_MOVE_FEATHER_FALL_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_CAN_TURN_WHILE_FALLING_ACK)]
        [Parser(Opcode.CMSG_MOVE_INERTIA_ENABLE_ACK)]
        [Parser(Opcode.CMSG_MOVE_INERTIA_DISABLE_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_CAN_ADV_FLY_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_CAN_DRIVE_ACK)]
        public static void HandleMovementAck(Packet packet)
        {
            var stats = ReadMovementAck(packet);
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 };
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_FLIGHT_BACK_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_FLIGHT_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_PITCH_RATE_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_RUN_BACK_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_RUN_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_SWIM_BACK_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_SWIM_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_TURN_RATE_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_WALK_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_MOD_MOVEMENT_FORCE_MAGNITUDE_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_ADD_IMPULSE_MAX_SPEED_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_AIR_FRICTION_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_DOUBLE_JUMP_VEL_MOD_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_GLIDE_START_MIN_HEIGHT_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_LAUNCH_SPEED_COEFFICIENT_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_LIFT_COEFFICIENT_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_MAX_VEL_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_OVER_MAX_DECELERATION_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_SURFACE_FRICTION_ACK)]
        public static void HandleMovementSpeedAck(Packet packet)
        {
            var stats = ReadMovementAck(packet);
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 };
            packet.ReadSingle("Speed");
        }

        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_BANKING_RATE_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_PITCHING_RATE_DOWN_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_PITCHING_RATE_UP_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_TURN_VELOCITY_THRESHOLD_ACK)]
        public static void HandleMovementSpeedRangeAck(Packet packet)
        {
            var stats = ReadMovementAck(packet);
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 };
            packet.ReadSingle("SpeedMin");
            packet.ReadSingle("SpeedMax");
        }

        [Parser(Opcode.CMSG_MOVE_REMOVE_MOVEMENT_FORCE_ACK)]
        public static void HandleMoveRemoveMovementForceAck(Packet packet)
        {
            var stats = ReadMovementAck(packet);
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 };
            packet.ReadPackedGuid128("TriggerGUID");
        }

        [Parser(Opcode.CMSG_MOVE_SET_VEHICLE_REC_ID_ACK)]
        public static void HandleMoveSetVehicleRecIdAck(Packet packet)
        {
            var stats = ReadMovementAck(packet);
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 };
            packet.ReadInt32("VehicleRecID");
        }

        [Parser(Opcode.SMSG_MOVE_REMOVE_MOVEMENT_FORCE)]
        public static void HandleMoveRemoveMovementForce(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
            packet.ReadPackedGuid128("TriggerGUID");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_REMOVE_MOVEMENT_FORCE)]
        public static void HandleMoveUpdateRemoveMovementForce(Packet packet)
        {
            Substructures.MovementHandler.ReadMovementStats(packet);
            packet.ReadPackedGuid128("TriggerGUID");
        }

        [Parser(Opcode.CMSG_MOVE_APPLY_MOVEMENT_FORCE_ACK)]
        public static void HandleMoveApplyMovementForceAck(Packet packet)
        {
            var stats = ReadMovementAck(packet);
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 };
            ReadMovementForce(packet, "MovementForce");
        }

        [Parser(Opcode.SMSG_MOVE_APPLY_MOVEMENT_FORCE)]
        public static void HandleMoveApplyMovementForce(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");

            ReadMovementForce(packet, "MovementForce");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_APPLY_MOVEMENT_FORCE)]
        public static void HandleMoveUpdateApplyMovementForce(Packet packet)
        {
            Substructures.MovementHandler.ReadMovementStats(packet);
            ReadMovementForce(packet, "MovementForce");
        }

        [Parser(Opcode.CMSG_MOVE_SET_COLLISION_HEIGHT_ACK)]
        public static void HandleMoveSetCollisionHeightAck(Packet packet)
        {
            var stats = ReadMovementAck(packet);
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 };
            packet.ReadSingle("Height");
            packet.ReadInt32("MountDisplayID");

            packet.ResetBitReader();

            packet.ReadBits("Reason", 2);
        }

        [Parser(Opcode.CMSG_MOVE_TELEPORT_ACK)]
        public static void HandleMoveTeleportAck(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("AckIndex");
            packet.ReadInt32("MoveTime");
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

        [Parser(Opcode.SMSG_SET_PLAY_HOVER_ANIM)]
        public static void HandlePlayHoverAnim(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadBit("PlayHoverAnim");
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
            Substructures.MovementHandler.ReadMovementStats(packet, "MovementStats");
            packet.ReadSingle("Speed");
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

        [Parser(Opcode.CMSG_MOVE_SPLINE_DONE)]
        public static void HandleMoveSplineDone(Packet packet)
        {
            Substructures.MovementHandler.ReadMovementStats(packet);
            packet.ReadInt32("SplineID");
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER)]
        public static void HandleSetActiveMover(Packet packet)
        {
            packet.ReadPackedGuid128("ActiveMover");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_COLLISION_HEIGHT)]
        public static void HandleMoveUpdateCollisionHeight434(Packet packet)
        {
            Substructures.MovementHandler.ReadMovementStats(packet);
            packet.ReadSingle("Height");
            packet.ReadSingle("Scale");
        }

        [Parser(Opcode.SMSG_SET_VEHICLE_REC_ID)]
        public static void HandleSetVehicleRecID(Packet packet)
        {
            packet.ReadPackedGuid128("VehicleGUID");
            packet.ReadInt32("VehicleRecID");
        }

        [Parser(Opcode.SMSG_SPECIAL_MOUNT_ANIM)]
        public static void HandleSpecialMountAnim(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
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

        [Parser(Opcode.CMSG_DISCARDED_TIME_SYNC_ACKS)]
        public static void HandleDiscardedTimeSyncAcks(Packet packet)
        {
            packet.ReadUInt32("MaxSequenceIndex");
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESPONSE_DROPPED)]
        public static void HandleTimeSyncResponseDropped(Packet packet)
        {
            packet.ReadUInt32("SequenceIndexFirst");
            packet.ReadUInt32("SequenceIndexLast");
        }

        [Parser(Opcode.SMSG_PLAYER_SKINNED)]
        public static void HandlePlayerSkinned(Packet packet)
        {
            packet.ReadBit("FreeRepop");
        }

        [Parser(Opcode.SMSG_MOVE_SET_COMPOUND_STATE)]
        public static void HandleMoveSetCompoundState(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");

            var moveStateChangeCount = packet.ReadInt32("MoveStateChangeCount");
            for (int i = 0; i < moveStateChangeCount; i++)
                ReadMoveStateChange(packet, "MoveStateChange", i);
        }

        [Parser(Opcode.SMSG_MOVE_SET_ANIM_KIT)]
        public static void HandleSetMovementAnimKit(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("Unit");
            var animKitID = packet.ReadUInt16("AnimKitID");

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                {
                    var timeSpan = Storage.Objects[guid].Item2 - packet.TimeSpan;
                    if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                        ((Unit)Storage.Objects[guid].Item1).MovementAnimKit = animKitID;
                }
        }

        [Parser(Opcode.CMSG_MOVE_CHANGE_VEHICLE_SEATS)]
        public static void HandleMoveChangeVehicleSeats(Packet packet)
        {
            Substructures.MovementHandler.ReadMovementStats(packet);
            packet.ReadPackedGuid128("DstVehicle");
            packet.ReadByte("DstSeatIndex");
        }

        [Parser(Opcode.CMSG_REQUEST_VEHICLE_SWITCH_SEAT)]
        public static void HandleRequestVehicleSwitchSeat(Packet packet)
        {
            packet.ReadPackedGuid128("Vehicle");
            packet.ReadByte("SeatIndex");
        }
    }
}
