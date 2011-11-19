using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class MovementHandler
    {
        public static Vector4 CurrentPosition;

        public static uint CurrentMapId;

        public static int CurrentPhaseMask = 1;

        public static MovementInfo ReadMovementInfo(ref Packet packet, Guid guid)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                return ReadMovementInfo420(ref packet, guid, -1);

            return ReadMovementInfo(ref packet, guid, -1);
        }

        public static MovementInfo ReadMovementInfo(ref Packet packet, Guid guid, int index)
        {
            string prefix = index < 0 ? string.Empty : "[" + index + "] ";

            var info = new MovementInfo();
            info.Flags = packet.ReadEnum<MovementFlag>(prefix + "Movement Flags", TypeCode.Int32);

            var flagsTypeCode = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056) ? TypeCode.Int16 : TypeCode.Byte;
            var flags = packet.ReadEnum<MovementFlagExtra>(prefix + "Extra Movement Flags", flagsTypeCode);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                if (packet.ReadGuid(prefix + "GUID 2") != guid)
                    packet.Writer.WriteLine("GUIDS NOT EQUAL"); // Fo debuggingz

            packet.ReadInt32(prefix + "Time");

            var pos = packet.ReadVector4(prefix + "Position");
            info.Position = new Vector3(pos.X, pos.Y, pos.Z);
            info.Orientation = pos.O;

            if (info.Flags.HasAnyFlag(MovementFlag.OnTransport))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    packet.ReadPackedGuid(prefix + "Transport GUID");
                else
                    packet.ReadGuid(prefix + "Transport GUID");

                packet.ReadVector4(prefix + "Transport Position");
                packet.ReadInt32(prefix + "Transport Time");

                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadByte(prefix + "Transport Seat");

                if (flags.HasAnyFlag(MovementFlagExtra.InterpolateMove))
                    packet.ReadInt32(prefix + "Transport Time");
            }

            if (info.Flags.HasAnyFlag(MovementFlag.Swimming | MovementFlag.Flying) ||
                flags.HasAnyFlag(MovementFlagExtra.AlwaysAllowPitching))
                packet.ReadSingle(prefix + "Swim Pitch");

            if (ClientVersion.RemovedInVersion(ClientType.Cataclysm))
                packet.ReadInt32(prefix + "Fall Time");

            if (info.Flags.HasAnyFlag(MovementFlag.Falling))
            {
                if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                    packet.ReadInt32(prefix + "Fall Time");

                packet.ReadSingle(prefix + "Fall Velocity");
                packet.ReadSingle(prefix + "Fall Sin angle");
                packet.ReadSingle(prefix + "Fall Cos angle");
                packet.ReadSingle(prefix + "Fall Speed");
            }

            if (info.Flags.HasAnyFlag(MovementFlag.SplineElevation))
                packet.ReadSingle(prefix + "Spline Elevation");

            return info;
        }

        public static MovementInfo ReadMovementInfo420(ref Packet packet, Guid guid, int index)
        {
            var info = new MovementInfo();

            info.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

            packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12, index);

            var onTransport = packet.ReadBit("OnTransport", index);
            var hasInterpolatedMovement = false;
            var time3 = false;
            if (onTransport)
            {
                hasInterpolatedMovement = packet.ReadBit("HasInterpolatedMovement", index);
                time3 = packet.ReadBit("Time3", index);
            }

            var swimming = packet.ReadBit("Swimming", index);
            var interPolatedTurning = packet.ReadBit("InterPolatedTurning", index);


            var jumping = false;
            if (interPolatedTurning)
                jumping = packet.ReadBit("Jumping", index);

            var splineElevation = packet.ReadBit("SplineElevation", index);

            packet.ReadBit("HasSplineData", index);

            packet.ResetBitReader(); // reset bitreader

            packet.ReadGuid("GUID 2", index);

            packet.ReadInt32("Time", index);
            var pos = packet.ReadVector4("Position", index);
            info.Position = new Vector3(pos.X, pos.Y, pos.Z);
            info.Orientation = pos.O;

            if (onTransport)
            {
                packet.ReadGuid("Transport GUID", index);
                packet.ReadVector4("Transport Position", index);
                packet.ReadByte("Transport Seat", index);
                packet.ReadInt32("Transport Time", index);
                if (hasInterpolatedMovement)
                    packet.ReadInt32("Transport Time 2", index);
                if (time3)
                    packet.ReadInt32("Transport Time 3", index);
            }
            if (swimming)
                packet.ReadSingle("Swim Pitch", index);

            if (interPolatedTurning)
            {
                packet.ReadInt32("Time Fallen", index);
                packet.ReadSingle("Fall Start Velocity", index);
                if (jumping)
                {
                    packet.ReadSingle("Jump Sin", index);
                    packet.ReadSingle("Jump Cos", index);
                    packet.ReadSingle("Jump Velocity", index);

                }
            }
            if (splineElevation)
                packet.ReadSingle("Spline Elevation", index);

            return info;
        }

        [Parser(Opcode.SMSG_MONSTER_MOVE)]
        [Parser(Opcode.SMSG_MONSTER_MOVE_TRANSPORT)]
        public static void HandleMonsterMove(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_MONSTER_MOVE_TRANSPORT))
            {
                packet.ReadPackedGuid("Transport GUID");

                packet.ReadByte("Transport Seat");
            }

            packet.ReadBoolean("Unk Boolean"); // Something to do with IsVehicleExitVoluntary ?

            var pos = packet.ReadVector3("Position");

            packet.ReadInt32("Move Ticks");

            var type = packet.ReadEnum<SplineType>("Spline Type", TypeCode.Byte);

            switch (type)
            {
                case SplineType.FacingSpot:
                {
                    packet.ReadVector3("Facing Spot");
                    break;
                }
                case SplineType.FacingTarget:
                {
                    packet.ReadGuid("Facing GUID");
                    break;
                }
                case SplineType.FacingAngle:
                {
                    packet.ReadSingle("Facing Angle");
                    break;
                }
                case SplineType.Stop:
                    return;
            }

            var flags = packet.ReadEnum<SplineFlag>("Spline Flags", TypeCode.Int32);

            if (flags.HasAnyFlag(SplineFlag.AnimationTier))
            {
                packet.ReadEnum<MovementAnimationState>("Animation State", TypeCode.Byte);

                packet.ReadInt32("Unk Int32 1");
            }

            if (flags.HasAnyFlag(SplineFlag.Falling)) // Could be SplineFlag.UsePathSmoothing
            {
                packet.ReadInt32("Unknown");
                packet.ReadInt16("Unknown");
                packet.ReadInt16("Unknown");
            }

            packet.ReadInt32("Move Time");

            if (flags.HasAnyFlag(SplineFlag.Trajectory))
            {
                packet.ReadSingle("Vertical Speed");

                packet.ReadInt32("Unk Int32 2");
            }

            var waypoints = packet.ReadInt32("Waypoints");

            var newpos = packet.ReadVector3("Waypoint 0");

            if (flags.HasAnyFlag(SplineFlag.Flying) || flags.HasAnyFlag(SplineFlag.CatmullRom))
                for (var i = 0; i < waypoints - 1; i++)
                    packet.ReadVector3("Waypoint " + (i + 1));
            else
            {
                var mid = new Vector3();
                mid.X = (pos.X + newpos.X) * 0.5f;
                mid.Y = (pos.Y + newpos.Y) * 0.5f;
                mid.Z = (pos.Z + newpos.Z) * 0.5f;

                for (var i = 0; i < waypoints - 1; i++)
                {
                    var vec = packet.ReadPackedVector3();
                    vec.X += mid.X;
                    vec.Y += mid.Y;
                    vec.Z += mid.Z;

                    packet.Writer.WriteLine("Waypoint " + (i + 1) + ": " + vec);
                }
            }
        }

        [Parser(Opcode.SMSG_NEW_WORLD)]
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleEnterWorld(Packet packet)
        {
            var mapId = packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

            CurrentMapId = (uint)mapId;

            var position = packet.ReadVector4();
            packet.Writer.WriteLine("Position: " + position);
            CurrentPosition = position;

            UpdateHandler.Objects[CurrentMapId] = new Dictionary<Guid, WoWObject>();

            if (packet.Opcode != Opcodes.GetOpcode(Opcode.SMSG_LOGIN_VERIFY_WORLD))
                return;

            Player chInfo;
            if (!CharacterHandler.Characters.TryGetValue(SessionHandler.LoginGuid, out chInfo))
                return;

            SessionHandler.LoggedInCharacter = chInfo;
        }

        [Parser(Opcode.SMSG_LOGIN_SETTIMESPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.ReadPackedTime("Game Time");
            packet.ReadSingle("Game Speed");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_BINDPOINTUPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            packet.ReadVector3("Position");

            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

            packet.ReadInt32("Zone ID");
        }

        [Parser(Opcode.MSG_MOVE_TELEPORT_ACK)]
        public static void HandleTeleportAck(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                var guid = packet.ReadPackedGuid();
                packet.Writer.WriteLine("GUID: " + guid);

                var counter = packet.ReadInt32();
                packet.Writer.WriteLine("Movement Counter: " + counter);

                ReadMovementInfo(ref packet, guid);
            }
            else
            {
                var guid = packet.ReadPackedGuid();
                packet.Writer.WriteLine("GUID: " + guid);

                var flags = (MovementFlag)packet.ReadInt32();
                packet.Writer.WriteLine("Move Flags: " + flags);

                var time = packet.ReadInt32();
                packet.Writer.WriteLine("Time: " + time);
            }
        }

        [Parser(Opcode.MSG_MOVE_START_FORWARD)]
        [Parser(Opcode.MSG_MOVE_START_BACKWARD)]
        [Parser(Opcode.MSG_MOVE_STOP)]
        [Parser(Opcode.MSG_MOVE_START_STRAFE_LEFT)]
        [Parser(Opcode.MSG_MOVE_START_STRAFE_RIGHT)]
        [Parser(Opcode.MSG_MOVE_STOP_STRAFE)]
        [Parser(Opcode.MSG_MOVE_START_ASCEND)]
        [Parser(Opcode.MSG_MOVE_START_DESCEND)]
        [Parser(Opcode.MSG_MOVE_STOP_ASCEND)]
        [Parser(Opcode.MSG_MOVE_JUMP)]
        [Parser(Opcode.MSG_MOVE_START_TURN_LEFT)]
        [Parser(Opcode.MSG_MOVE_START_TURN_RIGHT)]
        [Parser(Opcode.MSG_MOVE_STOP_TURN)]
        [Parser(Opcode.MSG_MOVE_START_PITCH_UP)]
        [Parser(Opcode.MSG_MOVE_START_PITCH_DOWN)]
        [Parser(Opcode.MSG_MOVE_STOP_PITCH)]
        [Parser(Opcode.MSG_MOVE_SET_RUN_MODE)]
        [Parser(Opcode.MSG_MOVE_SET_WALK_MODE)]
        [Parser(Opcode.MSG_MOVE_TELEPORT)]
        [Parser(Opcode.MSG_MOVE_SET_FACING)]
        [Parser(Opcode.MSG_MOVE_SET_PITCH)]
        [Parser(Opcode.MSG_MOVE_TOGGLE_COLLISION_CHEAT)]
        [Parser(Opcode.MSG_MOVE_GRAVITY_CHNG)]
        [Parser(Opcode.MSG_MOVE_ROOT)]
        [Parser(Opcode.MSG_MOVE_UNROOT)]
        [Parser(Opcode.MSG_MOVE_START_SWIM)]
        [Parser(Opcode.MSG_MOVE_STOP_SWIM)]
        [Parser(Opcode.MSG_MOVE_START_SWIM_CHEAT)]
        [Parser(Opcode.MSG_MOVE_STOP_SWIM_CHEAT)]
        [Parser(Opcode.MSG_MOVE_HEARTBEAT)]
        [Parser(Opcode.MSG_MOVE_FALL_LAND)]
        [Parser(Opcode.MSG_MOVE_UPDATE_CAN_FLY)]
        [Parser(Opcode.MSG_MOVE_UPDATE_CAN_TRANSITION_BETWEEN_SWIM_AND_FLY)]
        [Parser(Opcode.MSG_MOVE_KNOCK_BACK)]
        [Parser(Opcode.MSG_MOVE_HOVER)]
        [Parser(Opcode.MSG_MOVE_FEATHER_FALL)]
        [Parser(Opcode.MSG_MOVE_WATER_WALK)]
        [Parser(Opcode.CMSG_MOVE_FALL_RESET)]
        [Parser(Opcode.CMSG_MOVE_SET_FLY)]
        [Parser(Opcode.CMSG_MOVE_CHNG_TRANSPORT)]
        [Parser(Opcode.CMSG_MOVE_NOT_ACTIVE_MOVER)]
        [Parser(Opcode.CMSG_DISMISS_CONTROLLED_VEHICLE)]
        public static void HandleMovementMessages(Packet packet)
        {
            Guid guid;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ||
                packet.Direction == Direction.ServerToClient)
                guid = packet.ReadPackedGuid("GUID");
            else
                guid = new Guid();

            ReadMovementInfo(ref packet, guid);
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.MSG_MOVE_KNOCK_BACK))
            {
                packet.ReadSingle("Sin Angle");
                packet.ReadSingle("Cos Angle");
                packet.ReadSingle("Speed");
                packet.ReadSingle("Velocity");
            }
        }

        [Parser(Opcode.MSG_MOVE_SET_WALK_SPEED)]
        [Parser(Opcode.MSG_MOVE_SET_RUN_SPEED)]
        [Parser(Opcode.MSG_MOVE_SET_RUN_BACK_SPEED)]
        [Parser(Opcode.MSG_MOVE_SET_SWIM_SPEED)]
        [Parser(Opcode.MSG_MOVE_SET_SWIM_BACK_SPEED)]
        [Parser(Opcode.MSG_MOVE_SET_TURN_RATE)]
        [Parser(Opcode.MSG_MOVE_SET_FLIGHT_SPEED)]
        [Parser(Opcode.MSG_MOVE_SET_FLIGHT_BACK_SPEED)]
        [Parser(Opcode.MSG_MOVE_SET_PITCH_RATE)]
        public static void HandleMovementSetSpeed(Packet packet)
        {
            var guid = packet.ReadPackedGuid("GUID");
            ReadMovementInfo(ref packet, guid);
            packet.ReadSingle("Speed");
        }

        [Parser(Opcode.CMSG_FORCE_RUN_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_FORCE_RUN_BACK_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_FORCE_SWIM_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_FORCE_SWIM_BACK_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_FORCE_WALK_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_FORCE_TURN_RATE_CHANGE_ACK)]
        [Parser(Opcode.CMSG_FORCE_FLIGHT_SPEED_CHANGE_ACK)]
        [Parser(Opcode.CMSG_FORCE_FLIGHT_BACK_SPEED_CHANGE_ACK)]
        public static void HandleSpeedChangeMessage(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            packet.Writer.WriteLine("GUID: " + guid);

            var counter = packet.ReadInt32();
            packet.Writer.WriteLine("Movement Counter: " + counter);

            ReadMovementInfo(ref packet, guid);

            var newSpeed = packet.ReadSingle();
            packet.Writer.WriteLine("New Speed: " + newSpeed);
        }

        [Parser(Opcode.MSG_MOVE_SET_COLLISION_HGT)]
        [Parser(Opcode.SMSG_MOVE_SET_COLLISION_HGT)]
        [Parser(Opcode.CMSG_MOVE_SET_COLLISION_HGT_ACK)]
        public static void HandleCollisionMovements(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            packet.Writer.WriteLine("GUID: " + guid);

            if (packet.Opcode != Opcodes.GetOpcode(Opcode.MSG_MOVE_SET_COLLISION_HGT))
            {
                var counter = packet.ReadInt32();
                packet.Writer.WriteLine("Movement Counter: " + counter);
            }

            if (packet.Opcode != Opcodes.GetOpcode(Opcode.SMSG_MOVE_SET_COLLISION_HGT))
                ReadMovementInfo(ref packet, guid);

            var unk = packet.ReadSingle();
            packet.Writer.WriteLine("Collision Height: " + unk);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER)]
        [Parser(Opcode.SMSG_MOUNTSPECIAL_ANIM)]
        public static void HandleSetActiveMover(Packet packet)
        {
            var guid = packet.ReadGuid();
            packet.Writer.WriteLine("GUID: " + guid);
        }

        [Parser(Opcode.SMSG_SUMMON_REQUEST)]
        public static void HandleSummonRequest(Packet packet)
        {
            packet.ReadGuid("Summoner GUID");
            packet.ReadInt32("Unk int 1");
            packet.ReadInt32("Unk int 2");
        }

        [Parser(Opcode.CMSG_SUMMON_RESPONSE)]
        public static void HandleSummonResponse(Packet packet)
        {
            packet.ReadGuid("Summoner GUID");
            packet.ReadBoolean("Accept");
        }

        [Parser(Opcode.SMSG_FORCE_MOVE_ROOT)]
        [Parser(Opcode.SMSG_FORCE_MOVE_UNROOT)]
        [Parser(Opcode.SMSG_MOVE_WATER_WALK)]
        [Parser(Opcode.SMSG_MOVE_LAND_WALK)]
        public static void HandleSetMovementMessages(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            packet.Writer.WriteLine("GUID: " + guid);

            var counter = packet.ReadInt32();
            packet.Writer.WriteLine("Movement Counter: " + counter);
        }

        [Parser(Opcode.CMSG_MOVE_KNOCK_BACK_ACK)]
        [Parser(Opcode.CMSG_MOVE_WATER_WALK_ACK)]
        [Parser(Opcode.CMSG_MOVE_HOVER_ACK)]
        public static void HandleSpecialMoveAckMessages(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            packet.Writer.WriteLine("GUID: " + guid);

            var unk1 = packet.ReadInt32();
            packet.Writer.WriteLine("Unk Int32 1: " + unk1);

            ReadMovementInfo(ref packet, guid);

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_MOVE_KNOCK_BACK_ACK))
                return;

            var unk2 = packet.ReadInt32();
            packet.Writer.WriteLine("Unk Int32 2: " + unk2);
        }

        [Parser(Opcode.SMSG_SET_PHASE_SHIFT)]
        public static void HandlePhaseShift(Packet packet)
        {
            var phaseMask = packet.ReadInt32();
            packet.Writer.WriteLine("Phase Mask: 0x" + phaseMask.ToString("X8"));
            CurrentPhaseMask = phaseMask;
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

            if (!packet.CanRead())
                return;

            var tEntry = packet.ReadInt32("Transport Entry");
            packet.Writer.WriteLine("Transport Entry: " + tEntry);

            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Transport Map ID");
        }

        [Parser(Opcode.SMSG_TRANSFER_ABORTED)]
        public static void HandleTransferAborted(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

            var code = (TransferAbortReason)packet.ReadByte();
            packet.Writer.WriteLine("Reason: " + code);

            switch (code)
            {
                case TransferAbortReason.DifficultyUnavailable:
                    {
                        var arg = (MapDifficulty)packet.ReadByte();
                        packet.Writer.WriteLine("Difficulty: " + arg);
                        break;
                    }
                case TransferAbortReason.InsufficientExpansion:
                    {
                        var arg = (ClientType)packet.ReadByte();
                        packet.Writer.WriteLine("Expansion: " + arg);
                        break;
                    }
                case TransferAbortReason.UniqueMessage:
                    {
                        var arg = packet.ReadByte();
                        packet.Writer.WriteLine("Message ID: " + arg);
                        break;
                    }
            }
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            var val = packet.ReadSingle();
            packet.Writer.WriteLine("Unk Single: " + val);

            var guid = packet.ReadPackedGuid();
            packet.Writer.WriteLine("GUID: " + guid);
        }

        [Parser(Opcode.SMSG_CLIENT_CONTROL_UPDATE)]
        public static void HandleClientControlUpdate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadByte("AllowMove");
        }

        [Parser(Opcode.SMSG_MOVE_KNOCK_BACK)]
        public static void HandleMoveKnockBack(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadUInt32("Counter");
            packet.ReadSingle("X direction");
            packet.ReadSingle("Y direction");
            packet.ReadSingle("Horizontal Speed");
            packet.ReadSingle("Vertical Speed");
        }

        [Parser(Opcode.MSG_MOVE_TIME_SKIPPED)]
        public static void HandleMoveTimeSkipped(Packet packet)
        {
            packet.ReadPackedGuid("Guid");
            packet.ReadInt32("Time");
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_ROOT)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_UNROOT)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_GRAVITY_ENABLE)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_GRAVITY_DISABLE)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_FEATHER_FALL)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_NORMAL_FALL)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_HOVER)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_UNSET_HOVER)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_WATER_WALK)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_LAND_WALK)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_START_SWIM)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_STOP_SWIM)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_RUN_MODE)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_WALK_MODE)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_FLYING)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_UNSET_FLYING)]
        public static void HandleSplineMovementMessages(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.SMSG_SPLINE_SET_WALK_SPEED)]
        [Parser(Opcode.SMSG_SPLINE_SET_RUN_SPEED)]
        [Parser(Opcode.SMSG_SPLINE_SET_SWIM_SPEED)]
        [Parser(Opcode.SMSG_SPLINE_SET_FLIGHT_SPEED)]
        [Parser(Opcode.SMSG_SPLINE_SET_RUN_BACK_SPEED)]
        [Parser(Opcode.SMSG_SPLINE_SET_SWIM_BACK_SPEED)]
        [Parser(Opcode.SMSG_SPLINE_SET_FLIGHT_BACK_SPEED)]
        [Parser(Opcode.SMSG_SPLINE_SET_TURN_RATE)]
        [Parser(Opcode.SMSG_SPLINE_SET_PITCH_RATE)]
        public static void HandleSplineMovementSetSpeed(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadSingle("Amount");
        }

        [Parser(Opcode.SMSG_COMPRESSED_MOVES)]
        public static void HandleCompressedMoves(Packet packet)
        {
            var pkt = packet.Inflate(packet.ReadInt32());
            packet.Writer.WriteLine("{"); // To be able to see what is inside this packet.
            packet.Writer.WriteLine();

            while (pkt.CanRead())
            {
                var size = pkt.ReadByte();
                pkt.GetLength();
                var opc = pkt.ReadInt16();

                size -= 2; // TODO: Should not be needed! Is here because size is by some reason always 2 bits too high
                byte[] input = pkt.ReadBytes(size);
                var newPacket = new Packet(input, opc, pkt.Time, pkt.Direction, pkt.Number, packet.Writer);
                Statistics.Total += 1;
                Handler.Parse(newPacket);
            }

            packet.Writer.WriteLine("}");
            packet.ReadToEnd();
        }
    }
}
