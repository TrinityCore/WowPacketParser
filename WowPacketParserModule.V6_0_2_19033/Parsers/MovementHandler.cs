using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using MovementFlag = WowPacketParserModule.V6_0_2_19033.Enums.MovementFlag;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class MovementHandler
    {
        public static void ReadMovementStats(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID", idx);

            packet.Translator.ReadUInt32("MoveIndex", idx);
            packet.Translator.ReadVector4("Position", idx);

            packet.Translator.ReadSingle("Pitch", idx);
            packet.Translator.ReadSingle("StepUpStartElevation", idx);

            var int152 = packet.Translator.ReadInt32("RemoveForcesCount", idx);
            packet.Translator.ReadInt32("MoveTime", idx);

            for (var i = 0; i < int152; i++)
                packet.Translator.ReadPackedGuid128("RemoveForcesIDs", idx, i);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30, idx);
            packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? 16 : 15, idx);

            var hasTransport = packet.Translator.ReadBit("Has Transport Data", idx);
            var hasFall = packet.Translator.ReadBit("Has Fall Data", idx);
            packet.Translator.ReadBit("HasSpline", idx);
            packet.Translator.ReadBit("HeightChangeFailed", idx);
            packet.Translator.ReadBit("RemoteTimeValid", idx);

            if (hasTransport)
                ReadTransportData(packet, idx, "TransportData");

            if (hasFall)
                ReadFallData(packet, idx, "FallData");
        }

        public static void ReadTransportData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("TransportGuid", idx);
            packet.Translator.ReadVector4("TransportPosition", idx);
            packet.Translator.ReadByte("TransportSeat", idx);
            packet.Translator.ReadInt32("TransportMoveTime", idx);

            packet.Translator.ResetBitReader();

            var hasPrevMoveTime = packet.Translator.ReadBit("HasPrevMoveTime", idx);
            var hasVehicleRecID = packet.Translator.ReadBit("HasVehicleRecID", idx);

            if (hasPrevMoveTime)
                packet.Translator.ReadUInt32("PrevMoveTime", idx);

            if (hasVehicleRecID)
                packet.Translator.ReadUInt32("VehicleRecID", idx);
        }

        public static void ReadFallData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt32("FallTime", idx);
            packet.Translator.ReadSingle("JumpVelocity", idx);

            packet.Translator.ResetBitReader();

            var bit20 = packet.Translator.ReadBit("HasFallDirection", idx);
            if (bit20)
            {
                packet.Translator.ReadVector2("Direction", idx);
                packet.Translator.ReadSingle("HorizontalSpeed", idx);
            }
        }

        public static void ReadMovementAck(Packet packet)
        {
            ReadMovementStats(packet);
            packet.Translator.ReadInt32("AckIndex");
        }

        public static void ReadMovementForce(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("ID", idx);
            packet.Translator.ReadVector3("Direction", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802)) // correct?
                packet.Translator.ReadVector3("TransportPosition", idx);
            packet.Translator.ReadInt32("TransportID", idx);
            packet.Translator.ReadSingle("Magnitude", idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("Type", 2, idx);
        }

        public static void ReadMoveStateChange(Packet packet, params object[] idx)
        {
            var opcode = packet.Translator.ReadInt16();
            var opcodeName = Opcodes.GetOpcodeName(opcode, packet.Direction);
            packet.AddValue("MessageID", $"{ opcodeName } (0x{ opcode.ToString("X4") })", idx);

            packet.Translator.ReadInt32("SequenceIndex", idx);

            packet.Translator.ResetBitReader();

            var hasSpeed = packet.Translator.ReadBit("HasSpeed", idx);
            var hasKnockBack = packet.Translator.ReadBit("HasKnockBack", idx);
            var hasVehicle = packet.Translator.ReadBit("HasVehicle", idx);
            var hasCollisionHeight = packet.Translator.ReadBit("HasCollisionHeight", idx);
            var hasMovementForce = packet.Translator.ReadBit("HasMovementForce", idx);
            var hasMoverGUID = packet.Translator.ReadBit("HasMoverGUID", idx);

            if (hasSpeed)
                packet.Translator.ReadSingle("Speed", idx);

            if (hasKnockBack)
            {
                packet.Translator.ReadSingle("HorzSpeed", idx);
                packet.Translator.ReadVector2("InitVertSpeed", idx);
                packet.Translator.ReadSingle("InitVertSpeed", idx);
            }

            if (hasVehicle)
                packet.Translator.ReadInt32("VehicleRecID", idx);

            if (hasCollisionHeight)
            {
                packet.Translator.ReadSingle("Height", idx);
                packet.Translator.ReadSingle("Scale", idx);
                packet.Translator.ReadBits("UpdateCollisionHeightReason", 2, idx);
            }

            if (hasMovementForce)
                ReadMovementForce(packet, "MovementForce", idx);

            if (hasMoverGUID)
                packet.Translator.ReadPackedGuid128("MoverGUID", idx);
        }

        [Parser(Opcode.CMSG_WORLD_PORT_RESPONSE)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            WowPacketParser.Parsing.Parsers.MovementHandler.CurrentMapId = (uint)packet.Translator.ReadInt32<MapId>("Map");
            packet.Translator.ReadVector4("Position");
            packet.Translator.ReadUInt32("Reason");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            WowPacketParser.Parsing.Parsers.MovementHandler.CurrentMapId = (uint)packet.Translator.ReadInt32<MapId>("Map");
            packet.Translator.ReadVector4("Position");
            packet.Translator.ReadUInt32("Reason");

            packet.AddSniffData(StoreNameType.Map, (int)WowPacketParser.Parsing.Parsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_LOGIN_SET_TIME_SPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.Translator.ReadPackedTime("ServerTime");
            packet.Translator.ReadPackedTime("GameTime");
            packet.Translator.ReadSingle("NewSpeed");
            packet.Translator.ReadInt32("ServerTimeHolidayOffset");
            packet.Translator.ReadInt32("GameTimeHolidayOffset");
        }

        [Parser(Opcode.SMSG_GAME_TIME_UPDATE)]
        public static void HandleGameTimeUpdate(Packet packet)
        {
            packet.Translator.ReadPackedTime("ServerTime");
            packet.Translator.ReadPackedTime("GameTime");
            packet.Translator.ReadInt32("ServerTimeHolidayOffset");
            packet.Translator.ReadInt32("GameTimeHolidayOffset");
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED)]
        public static void HandleMoveTimeSkipped(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GUID");
            packet.Translator.ReadInt32("Time");
        }

        [Parser(Opcode.CMSG_MOVE_CHANGE_TRANSPORT)]
        [Parser(Opcode.CMSG_MOVE_DISMISS_VEHICLE)]
        [Parser(Opcode.CMSG_MOVE_FALL_LAND)]
        [Parser(Opcode.CMSG_MOVE_FALL_RESET)]
        [Parser(Opcode.CMSG_MOVE_HEARTBEAT)]
        [Parser(Opcode.CMSG_MOVE_JUMP)]
        [Parser(Opcode.CMSG_MOVE_REMOVE_MOVEMENT_FORCES)]
        [Parser(Opcode.CMSG_MOVE_SET_FACING)]
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
        [Parser(Opcode.SMSG_MOVE_UPDATE_KNOCK_BACK)]
        [Parser(Opcode.SMSG_MOVE_UPDATE)]
        public static void HandlePlayerMove(Packet packet)
        {
            ReadMovementStats(packet);
        }

        public static void ReadMonsterSplineFilter(Packet packet, params object[] indexes)
        {
            var count = packet.Translator.ReadUInt32("MonsterSplineFilterKey", indexes);
            packet.Translator.ReadSingle("BaseSpeed", indexes);
            packet.Translator.ReadUInt16("StartOffset", indexes);
            packet.Translator.ReadSingle("DistToPrevFilterKey", indexes);

            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadInt16("IDx", indexes, i);
                packet.Translator.ReadUInt16("Speed", indexes, i);
            }

            packet.Translator.ReadUInt16("AddedToStart", indexes);

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBits("FilterFlags", 2, indexes);
        }

        public static void ReadMovementSpline(Packet packet, Vector3 pos, params object[] indexes)
        {
            packet.Translator.ReadInt32E<SplineFlag434>("Flags", indexes);
            packet.Translator.ReadByte("AnimTier", indexes);
            packet.Translator.ReadUInt32("TierTransStartTime", indexes);
            packet.Translator.ReadInt32("Elapsed", indexes);
            packet.Translator.ReadUInt32("MoveTime", indexes);
            packet.Translator.ReadSingle("JumpGravity", indexes);
            packet.Translator.ReadUInt32("SpecialTime", indexes);
            var pointsCount = packet.Translator.ReadInt32("PointsCount", indexes);

            packet.Translator.ReadByte("Mode", indexes);
            packet.Translator.ReadByte("VehicleExitVoluntary", indexes);

            packet.Translator.ReadPackedGuid128("TransportGUID", indexes);
            packet.Translator.ReadSByte("VehicleSeat", indexes);

            var packedDeltasCount = packet.Translator.ReadInt32("PackedDeltasCount", indexes);

            Vector3 endpos = new Vector3();
            for (int i = 0; i < pointsCount; i++)
            {
                var spot = packet.Translator.ReadVector3();

                // client always taking first point
                if (i == 0)
                    endpos = spot;

                packet.AddValue("Points", spot, indexes, i);
            }

            var waypoints = new Vector3[packedDeltasCount];
            for (int i = 0; i < packedDeltasCount; i++)
            {
                var packedDeltas = packet.Translator.ReadPackedVector3();
                waypoints[i].X = packedDeltas.X;
                waypoints[i].Y = packedDeltas.Y;
                waypoints[i].Z = packedDeltas.Z;
            }

            packet.Translator.ResetBitReader();
            var type = packet.Translator.ReadBits("Face", 2, indexes);
            var monsterSplineFilter = packet.Translator.ReadBit("HasMonsterSplineFilter", indexes);

            switch (type)
            {
                case 1:
                    packet.Translator.ReadVector3("FaceSpot", indexes);
                    break;
                case 2:
                    packet.Translator.ReadSingle("FaceDirection", indexes);
                    packet.Translator.ReadPackedGuid128("FacingGUID", indexes);
                    break;
                case 3:
                    packet.Translator.ReadSingle("FaceDirection", indexes);
                    break;
            }

            if (monsterSplineFilter)
                ReadMonsterSplineFilter(packet, indexes, "MonsterSplineFilter");

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
                packet.AddValue("WayPoints", vec, indexes, i);
            }
        }

        public static void ReadMovementMonsterSpline(Packet packet, Vector3 pos, params object[] indexes)
        {
            packet.Translator.ReadUInt32("Id", indexes);
            packet.Translator.ReadVector3("Destination", indexes);

            ReadMovementSpline(packet, pos, indexes, "MovementSpline");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("CrzTeleport", indexes);
            packet.Translator.ReadBits("UnkBit", 2, indexes);
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleOnMonsterMove(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            var pos = packet.Translator.ReadVector3("Position");

            ReadMovementMonsterSpline(packet, pos, "MovementMonsterSpline");
        }

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Client");

            // PhaseShiftData
            packet.Translator.ReadInt32("PhaseShiftFlags");
            var count = packet.Translator.ReadInt32("PhaseShiftCount");
            packet.Translator.ReadPackedGuid128("PersonalGUID");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt16("PhaseFlags", i);
                packet.Translator.ReadInt16("Id", i);
            }

            var preloadMapIDCount = packet.Translator.ReadInt32("PreloadMapIDsCount") / 2;
            for (var i = 0; i < preloadMapIDCount; ++i)
                packet.Translator.ReadInt16<MapId>("PreloadMapID", i);

            var uiWorldMapAreaIDSwapsCount = packet.Translator.ReadInt32("UiWorldMapAreaIDSwap") / 2;
            for (var i = 0; i < uiWorldMapAreaIDSwapsCount; ++i)
                packet.Translator.ReadInt16<MapId>("UiWorldMapAreaIDSwaps", i);

            var visibleMapIDsCount = packet.Translator.ReadInt32("VisibleMapIDsCount") / 2;
            for (var i = 0; i < visibleMapIDsCount; ++i)
                packet.Translator.ReadInt16<MapId>("VisibleMapID", i);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_RUN_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_SWIM_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_RUN_BACK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_WALK_SPEED)]
        public static void HandleSplineSetSpeed(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadSingle("Speed");
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadSingle("SplineDist");
        }

        [Parser(Opcode.SMSG_VIGNETTE_UPDATE)]
        public static void HandleVignetteUpdate(Packet packet)
        {
            packet.Translator.ReadBit("ForceUpdate");

            // VignetteInstanceIDList
            var int1 = packet.Translator.ReadInt32("RemovedCount");
            for (var i = 0; i < int1; ++i)
                packet.Translator.ReadPackedGuid128("IDs", i);

            // Added
            var int2 = packet.Translator.ReadInt32("AddedCount");
            for (var i = 0; i < int2; ++i)
                packet.Translator.ReadPackedGuid128("IDs", i);

            // VignetteClientData
            var int3 = packet.Translator.ReadInt32("VignetteClientDataCount");
            for (var i = 0; i < int3; ++i)
            {
                packet.Translator.ReadVector3("Position", i);
                packet.Translator.ReadPackedGuid128("ObjGUID", i);
                packet.Translator.ReadInt32("VignetteID", i);
                packet.Translator.ReadInt32("Unk", i);
            }

            // Updated
            var int4 = packet.Translator.ReadInt32("UpdatedCount");
            for (var i = 0; i < int4; ++i)
                packet.Translator.ReadPackedGuid128("IDs", i);

            // VignetteClientData
            var int5 = packet.Translator.ReadInt32("VignetteClientDataCount");
            for (var i = 0; i < int5; ++i)
            {
                packet.Translator.ReadVector3("Position", i);
                packet.Translator.ReadPackedGuid128("ObjGUID", i);
                packet.Translator.ReadInt32("VignetteID", i);
                packet.Translator.ReadInt32("Unk", i);
            }
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("MapID");

            packet.Translator.ResetBitReader();

            var hasShipTransferPending = packet.Translator.ReadBit();
            var hasTransferSpell = packet.Translator.ReadBit();

            if (hasShipTransferPending)
            {
                packet.Translator.ReadUInt32<GOId>("ID");
                packet.Translator.ReadInt32<MapId>("OriginMapID");
            }

            if (hasTransferSpell)
                packet.Translator.ReadUInt32<SpellId>("TransferSpellID");
        }

        [Parser(Opcode.SMSG_TRANSFER_ABORTED)]
        public static void HandleTransferAborted(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("MapID");
            packet.Translator.ReadByte("Arg");
            packet.Translator.ReadBitsE<TransferAbortReason>("TransfertAbort", 5);
        }

        [Parser(Opcode.SMSG_ABORT_NEW_WORLD)]
        public static void HandleAbortNewWorld(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_TELEPORT)]
        public static void HandleMoveUpdateTeleport(Packet packet)
        {
            ReadMovementStats(packet);

            var int32 = packet.Translator.ReadInt32("MovementForcesCount");
            for (int i = 0; i < int32; i++)
                ReadMovementForce(packet, i, "MovementForce");

            packet.Translator.ResetBitReader();

            var bit260 = packet.Translator.ReadBit("HasWalkSpeed");
            var bit276 = packet.Translator.ReadBit("HasRunSpeed");
            var bit52 = packet.Translator.ReadBit("HasRunBackSpeed");
            var bit28 = packet.Translator.ReadBit("HasSwimSpeed");
            var bit76 = packet.Translator.ReadBit("HasSwimBackSpeed");
            var bit20 = packet.Translator.ReadBit("HasFlightSpeed");
            var bit268 = packet.Translator.ReadBit("HasFlightBackSpeed");
            var bit60 = packet.Translator.ReadBit("HasTurnRate");
            var bit68 = packet.Translator.ReadBit("HasPitchRate");

            if (bit260)
                packet.Translator.ReadSingle("WalkSpeed");

            if (bit276)
                packet.Translator.ReadSingle("RunSpeed");

            if (bit52)
                packet.Translator.ReadSingle("RunBackSpeed");

            if (bit28)
                packet.Translator.ReadSingle("SwimSpeed");

            if (bit76)
                packet.Translator.ReadSingle("SwimBackSpeed");

            if (bit20)
                packet.Translator.ReadSingle("FlightSpeed");

            if (bit268)
                packet.Translator.ReadSingle("FlightBackSpeed");

            if (bit60)
                packet.Translator.ReadSingle("TurnRate");

            if (bit68)
                packet.Translator.ReadSingle("PitchRate");
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
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadInt32("SequenceIndex");
        }

        [Parser(Opcode.SMSG_MOVE_SET_RUN_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_SWIM_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_WALK_SPEED)]
        [Parser(Opcode.SMSG_MOVE_SET_RUN_BACK_SPEED)]
        public static void HandleMovementIndexSpeed(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadInt32("SequenceIndex");

            packet.Translator.ReadSingle("Speed");
        }

        [Parser(Opcode.SMSG_MOVE_SET_VEHICLE_REC_ID)]
        public static void HandleMoveSetVehicleRecID(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadInt32("SequenceIndex");

            packet.Translator.ReadInt32("VehicleRecID");
        }

        [Parser(Opcode.SMSG_MOVE_SET_COLLISION_HEIGHT)]
        public static void HandleSetCollisionHeight(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadInt32("SequenceIndex");
            packet.Translator.ReadSingle("Scale");
            packet.Translator.ReadSingle("Height");
            packet.Translator.ReadInt32("MountDisplayID");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("Reason", 2);
        }

        [Parser(Opcode.SMSG_MOVE_TELEPORT)]
        public static void HandleMoveTeleport(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadInt32("SequenceIndex");
            packet.Translator.ReadVector3("Position");
            packet.Translator.ReadSingle("Facing");

            var bit72 = packet.Translator.ReadBit("HasTransport");
            var bit56 = packet.Translator.ReadBit("HasVehicleTeleport");

            if (bit72)
                packet.Translator.ReadPackedGuid128("TransportGUID");

            // VehicleTeleport
            if (bit56)
            {
                packet.Translator.ReadByte("VehicleSeatIndex");
                packet.Translator.ReadBit("VehicleExitVoluntary");
                packet.Translator.ReadBit("VehicleExitTeleport");
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
        public static void HandleMovementAck(Packet packet)
        {
            ReadMovementAck(packet);
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_FLIGHT_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_RUN_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_RUN_BACK_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_SWIM_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_WALK_SPEED_CHANGE_ACK)]
        public static void HandleMovementSpeedAck(Packet packet)
        {
            ReadMovementAck(packet);
            packet.Translator.ReadSingle("Speed");
        }

        [Parser(Opcode.CMSG_MOVE_REMOVE_MOVEMENT_FORCE_ACK)]
        public static void HandleMoveRemoveMovementForceAck(Packet packet)
        {
            ReadMovementAck(packet);
            packet.Translator.ReadPackedGuid128("TriggerGUID");
        }

        [Parser(Opcode.CMSG_MOVE_SET_VEHICLE_REC_ID_ACK)]
        public static void HandleMoveSetVehicleRecIdAck(Packet packet)
        {
            ReadMovementAck(packet);
            packet.Translator.ReadInt32("VehicleRecID");
        }

        [Parser(Opcode.SMSG_MOVE_REMOVE_MOVEMENT_FORCE)]
        public static void HandleMoveRemoveMovementForce(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadInt32("SequenceIndex");
            packet.Translator.ReadPackedGuid128("TriggerGUID");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_REMOVE_MOVEMENT_FORCE)]
        public static void HandleMoveUpdateRemoveMovementForce(Packet packet)
        {
            ReadMovementStats(packet);
            packet.Translator.ReadPackedGuid128("TriggerGUID");
        }

        [Parser(Opcode.CMSG_MOVE_APPLY_MOVEMENT_FORCE_ACK)]
        public static void HandleMoveApplyMovementForceAck(Packet packet)
        {
            ReadMovementAck(packet);
            ReadMovementForce(packet, "MovementForce");
        }

        [Parser(Opcode.SMSG_MOVE_APPLY_MOVEMENT_FORCE)]
        public static void HandleMoveApplyMovementForce(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadInt32("SequenceIndex");

            ReadMovementForce(packet, "MovementForce");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_APPLY_MOVEMENT_FORCE)]
        public static void HandleMoveUpdateApplyMovementForce(Packet packet)
        {
            ReadMovementStats(packet);
            ReadMovementForce(packet, "MovementForce");
        }

        [Parser(Opcode.CMSG_MOVE_SET_COLLISION_HEIGHT_ACK)]
        public static void HandleMoveSetCollisionHeightAck(Packet packet)
        {
            ReadMovementAck(packet);
            packet.Translator.ReadSingle("Height");
            packet.Translator.ReadInt32("MountDisplayID");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("Reason", 2);
        }

        [Parser(Opcode.CMSG_MOVE_TELEPORT_ACK)]
        public static void HandleMoveTeleportAck(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadInt32("AckIndex");
            packet.Translator.ReadInt32("MoveTime");
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
            packet.Translator.ReadPackedGuid128("MoverGUID");
        }

        [Parser(Opcode.SMSG_SET_PLAY_HOVER_ANIM)]
        public static void HandlePlayHoverAnim(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("UnitGUID");
            packet.Translator.ReadBit("PlayHoverAnim");
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
            ReadMovementStats(packet);
            packet.Translator.ReadSingle("Speed");
        }

        [Parser(Opcode.SMSG_ADJUST_SPLINE_DURATION)]
        public static void HandleAdjustSplineDuration(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
            packet.Translator.ReadSingle("Scale");
        }

        [Parser(Opcode.SMSG_CONTROL_UPDATE)]
        public static void HandleClientControlUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
            packet.Translator.ReadBit("On");
        }

        [Parser(Opcode.CMSG_MOVE_SPLINE_DONE)]
        public static void HandleMoveSplineDone(Packet packet)
        {
            ReadMovementStats(packet);
            packet.Translator.ReadInt32("SplineID");
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER)]
        public static void HandleSetActiveMover(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ActiveMover");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_COLLISION_HEIGHT)]
        public static void HandleMoveUpdateCollisionHeight434(Packet packet)
        {
            ReadMovementStats(packet);
            packet.Translator.ReadSingle("Height");
            packet.Translator.ReadSingle("Scale");
        }

        [Parser(Opcode.SMSG_SET_VEHICLE_REC_ID)]
        public static void HandleSetVehicleRecID(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("VehicleGUID");
            packet.Translator.ReadInt32("VehicleRecID");
        }

        [Parser(Opcode.SMSG_SPECIAL_MOUNT_ANIM)]
        public static void HandleSpecialMountAnim(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("UnitGUID");
        }

        [Parser(Opcode.SMSG_MOVE_KNOCK_BACK)]
        public static void HandleMoveKnockBack(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadInt32("SequenceIndex");
            packet.Translator.ReadVector2("Direction");
            packet.Translator.ReadSingle("HorzSpeed");
            packet.Translator.ReadSingle("VertSpeed");
        }

        [Parser(Opcode.CMSG_DISCARDED_TIME_SYNC_ACKS)]
        public static void HandleDiscardedTimeSyncAcks(Packet packet)
        {
            packet.Translator.ReadUInt32("MaxSequenceIndex");
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESPONSE_DROPPED)]
        public static void HandleTimeSyncResponseDropped(Packet packet)
        {
            packet.Translator.ReadUInt32("SequenceIndexFirst");
            packet.Translator.ReadUInt32("SequenceIndexLast");
        }

        [Parser(Opcode.SMSG_PLAYER_SKINNED)]
        public static void HandlePlayerSkinned(Packet packet)
        {
            packet.Translator.ReadBit("FreeRepop");
        }

        [Parser(Opcode.SMSG_MOVE_SET_COMPOUND_STATE)]
        public static void HandleMoveSetCompoundState(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");

            var moveStateChangeCount = packet.Translator.ReadInt32("MoveStateChangeCount");
            for (int i = 0; i < moveStateChangeCount; i++)
                ReadMoveStateChange(packet, "MoveStateChange", i);
        }

        [Parser(Opcode.SMSG_MOVE_SET_ANIM_KIT)]
        public static void HandleSetMovementAnimKit(Packet packet)
        {
            var guid = packet.Translator.ReadPackedGuid128("Unit");
            var animKitID = packet.Translator.ReadUInt16("AnimKitID");

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
            ReadMovementStats(packet);
            packet.Translator.ReadPackedGuid128("DstVehicle");
            packet.Translator.ReadByte("DstSeatIndex");
        }

        [Parser(Opcode.CMSG_REQUEST_VEHICLE_SWITCH_SEAT)]
        public static void HandleRequestVehicleSwitchSeat(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Vehicle");
            packet.Translator.ReadByte("SeatIndex");
        }
    }
}
