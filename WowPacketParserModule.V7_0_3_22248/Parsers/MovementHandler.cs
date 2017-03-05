using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using MovementFlag = WowPacketParserModule.V6_0_2_19033.Enums.MovementFlag;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
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

            packet.Translator.ReadBitsE<MovementFlag>("MovementFlags", 30, idx);
            packet.Translator.ReadBitsE<MovementFlagExtra>("ExtraMovementFlags", 18, idx);

            var hasTransport = packet.Translator.ReadBit("HasTransportData", idx);
            var hasFall = packet.Translator.ReadBit("HasFallData", idx);
            packet.Translator.ReadBit("HasSpline", idx);
            packet.Translator.ReadBit("HeightChangeFailed", idx);
            packet.Translator.ReadBit("RemoteTimeValid", idx);

            if (hasTransport)
                V6_0_2_19033.Parsers.MovementHandler.ReadTransportData(packet, idx, "TransportData");

            if (hasFall)
                V6_0_2_19033.Parsers.MovementHandler.ReadFallData(packet, idx, "FallData");
        }

        public static void ReadMovementAck(Packet packet, params object[] idx)
        {
            ReadMovementStats(packet, idx);
            packet.Translator.ReadInt32("AckIndex", idx);
        }

        public static void ReadMovementForce(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("ID", idx);
            packet.Translator.ReadVector3("Origin", idx);
            packet.Translator.ReadVector3("Direction", idx);
            packet.Translator.ReadVector3("TransportPosition", idx);
            packet.Translator.ReadInt32("TransportID", idx);
            packet.Translator.ReadSingle("Magnitude", idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("Type", 2, idx);
        }

        public static void ReadMovementMonsterSpline(Packet packet, Vector3 pos, params object[] indexes)
        {
            packet.Translator.ReadUInt32("Id", indexes);
            packet.Translator.ReadVector3("Destination", indexes);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("CrzTeleport", indexes);
            packet.Translator.ReadBits("StopDistanceTolerance", 3, indexes);

            ReadMovementSpline(packet, pos, indexes, "MovementSpline");
        }

        public static void ReadMonsterSplineFilter(Packet packet, params object[] indexes)
        {
            var count = packet.Translator.ReadUInt32("MonsterSplineFilterKey", indexes);
            packet.Translator.ReadSingle("BaseSpeed", indexes);
            packet.Translator.ReadUInt16("StartOffset", indexes);
            packet.Translator.ReadSingle("DistToPrevFilterKey", indexes);
            packet.Translator.ReadUInt16("AddedToStart", indexes);

            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadInt16("IDx", indexes, i);
                packet.Translator.ReadUInt16("Speed", indexes, i);
            }

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBits("FilterFlags", 2, indexes);
        }

        public static void ReadMonsterSplineSpellEffectExtraData(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID", indexes);
            packet.Translator.ReadUInt32("SpellVisualID", indexes);
            packet.Translator.ReadUInt32("ProgressCurveID", indexes);
            packet.Translator.ReadUInt32("ParabolicCurveID", indexes);
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

            packet.Translator.ReadByte("Mode", indexes);
            packet.Translator.ReadByte("VehicleExitVoluntary", indexes);

            packet.Translator.ReadPackedGuid128("TransportGUID", indexes);
            packet.Translator.ReadSByte("VehicleSeat", indexes);

            packet.Translator.ResetBitReader();

            var type = packet.Translator.ReadBits("Face", 2, indexes);
            var pointsCount = packet.Translator.ReadBits("PointsCount", 16, indexes);
            var packedDeltasCount = packet.Translator.ReadBits("PackedDeltasCount", 16, indexes);
            var hasSplineFilter = packet.Translator.ReadBit("HasSplineFilter", indexes);
            var hasSpellEffectExtraData = packet.Translator.ReadBit("HasSpellEffectExtraData", indexes);

            if (hasSplineFilter)
                ReadMonsterSplineFilter(packet, indexes, "MonsterSplineFilter");

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

            if (hasSpellEffectExtraData)
                ReadMonsterSplineSpellEffectExtraData(packet, "MonsterSplineSpellEffectExtra");

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

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleOnMonsterMove(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            var pos = packet.Translator.ReadVector3("Position");

            ReadMovementMonsterSpline(packet, pos, "MovementMonsterSpline");
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
        [Parser(Opcode.CMSG_MOVE_DOUBLE_JUMP)]
        public static void HandlePlayerMove(Packet packet)
        {
            ReadMovementStats(packet, "MovementStats");
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
        [Parser(Opcode.CMSG_MOVE_ENABLE_DOUBLE_JUMP_ACK)]
        public static void HandleMovementAck(Packet packet)
        {
            ReadMovementAck(packet, "MovementAck");
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_FLIGHT_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_RUN_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_RUN_BACK_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_SWIM_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_MOVE_FORCE_WALK_SPEED_CHANGE_ACK)]
        public static void HandleMovementSpeedAck(Packet packet)
        {
            ReadMovementAck(packet, "MovementAck");
            packet.Translator.ReadSingle("Speed");
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
            packet.Translator.ReadSingle("Speed");
        }

        [Parser(Opcode.CMSG_MOVE_SPLINE_DONE)]
        public static void HandleMoveSplineDone(Packet packet)
        {
            ReadMovementStats(packet, "MovementStats");
            packet.Translator.ReadInt32("SplineID");
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_COLLISION_HEIGHT)]
        public static void HandleMoveUpdateCollisionHeight434(Packet packet)
        {
            ReadMovementStats(packet, "MovementStats");
            packet.Translator.ReadSingle("Height");
            packet.Translator.ReadSingle("Scale");
        }

        [Parser(Opcode.CMSG_MOVE_SET_COLLISION_HEIGHT_ACK)]
        public static void HandleMoveSetCollisionHeightAck(Packet packet)
        {
            ReadMovementAck(packet, "MovementAck");
            packet.Translator.ReadSingle("Height");
            packet.Translator.ReadInt32("MountDisplayID");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("Reason", 2);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_TELEPORT)]
        public static void HandleMoveUpdateTeleport(Packet packet)
        {
            ReadMovementStats(packet, "MovementStats");

            var int32 = packet.Translator.ReadInt32("MovementForcesCount");
            for (int i = 0; i < int32; i++)
                ReadMovementForce(packet, i, "MovementForce");

            packet.Translator.ResetBitReader();

            var hasWalkSpeed = packet.Translator.ReadBit("HasWalkSpeed");
            var hasRunSpeed = packet.Translator.ReadBit("HasRunSpeed");
            var hasRunBackSpeed = packet.Translator.ReadBit("HasRunBackSpeed");
            var hasSwimSpeed = packet.Translator.ReadBit("HasSwimSpeed");
            var hasSwimBackSpeed = packet.Translator.ReadBit("HasSwimBackSpeed");
            var hasFlightSpeed = packet.Translator.ReadBit("HasFlightSpeed");
            var hasFlightBackSpeed = packet.Translator.ReadBit("HasFlightBackSpeed");
            var hasTurnRate = packet.Translator.ReadBit("HasTurnRate");
            var hasPitchRate = packet.Translator.ReadBit("HasPitchRate");

            if (hasWalkSpeed)
                packet.Translator.ReadSingle("WalkSpeed");

            if (hasRunSpeed)
                packet.Translator.ReadSingle("RunSpeed");

            if (hasRunBackSpeed)
                packet.Translator.ReadSingle("RunBackSpeed");

            if (hasSwimSpeed)
                packet.Translator.ReadSingle("SwimSpeed");

            if (hasSwimBackSpeed)
                packet.Translator.ReadSingle("SwimBackSpeed");

            if (hasFlightSpeed)
                packet.Translator.ReadSingle("FlightSpeed");

            if (hasFlightBackSpeed)
                packet.Translator.ReadSingle("FlightBackSpeed");

            if (hasTurnRate)
                packet.Translator.ReadSingle("TurnRate");

            if (hasPitchRate)
                packet.Translator.ReadSingle("PitchRate");
        }

        [Parser(Opcode.SMSG_MOVE_TELEPORT)]
        public static void HandleMoveTeleport(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadInt32("SequenceIndex");
            packet.Translator.ReadVector3("Position");
            packet.Translator.ReadSingle("Facing");
            packet.Translator.ReadByte("PreloadWorld");

            var hasVehicleTeleport = packet.Translator.ReadBit("HasVehicleTeleport");
            var hasTransport = packet.Translator.ReadBit("HasTransport");

            // VehicleTeleport
            if (hasVehicleTeleport)
            {
                packet.Translator.ReadByte("VehicleSeatIndex");
                packet.Translator.ReadBit("VehicleExitVoluntary");
                packet.Translator.ReadBit("VehicleExitTeleport");
            }

            if (hasTransport)
                packet.Translator.ReadPackedGuid128("TransportGUID");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            WowPacketParser.Parsing.Parsers.MovementHandler.CurrentMapId = (uint)packet.Translator.ReadInt32<MapId>("Map");
            packet.Translator.ReadVector4("Position");
            packet.Translator.ReadUInt32("Reason");
            packet.Translator.ReadVector3("MovementOffset");

            packet.AddSniffData(StoreNameType.Map, (int)WowPacketParser.Parsing.Parsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_MOVE_ENABLE_DOUBLE_JUMP)]
        public static void HandleMoveEnableDoubleJump(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadUInt32("SequenceId");
        }


        [Parser(Opcode.SMSG_MOVE_SET_COLLISION_HEIGHT, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleSetCollisionHeight(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MoverGUID");
            packet.Translator.ReadInt32("SequenceIndex");
            packet.Translator.ReadSingle("Scale");
            packet.Translator.ReadSingle("Height");
            packet.Translator.ReadUInt32("MountDisplayID");
            packet.Translator.ReadInt32("ScaleDuration");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("Reason", 2);
        }
    }
}
