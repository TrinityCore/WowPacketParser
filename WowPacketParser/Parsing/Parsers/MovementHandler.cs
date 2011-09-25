using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Misc.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class MovementHandler
    {
        public static Vector4 CurrentPosition;

        public static int CurrentMapId;

        public static int CurrentPhaseMask = 1;

        public static MovementInfo ReadMovementInfo(Packet packet, Guid guid)
        {
            var info = new MovementInfo();

            var moveFlags = (MovementFlag)packet.ReadInt32();
            Console.WriteLine("Movement Flags: " + moveFlags);
            info.Flags = moveFlags;

            var flags2 = (MovementFlagExtra)packet.ReadInt16();
            Console.WriteLine("Extra Movement Flags: " + flags2);

            var time = packet.ReadInt32("Time");
            var pos = packet.ReadVector4();
            Console.WriteLine("Position: " + pos);
            info.Position = new Vector3(pos.X, pos.Y, pos.Z);
            info.Orientation = pos.O;

            if (moveFlags.HasFlag(MovementFlag.OnTransport))
            {
                var tguid = packet.ReadPackedGuid();
                Console.WriteLine("Transport GUID: " + tguid);

                var tpos = packet.ReadVector4();
                Console.WriteLine("Transport Position: " + tpos);

                var ttime = packet.ReadInt32();
                Console.WriteLine("Transport Time: " + ttime);

                var seat = packet.ReadByte();
                Console.WriteLine("Transport Seat: " + seat);

                if (flags2.HasFlag(MovementFlagExtra.InterpolateMove))
                {
                    var ttime2 = packet.ReadInt32();
                    Console.WriteLine("Transport Time 2: " + ttime2);
                }
            }

            if (moveFlags.HasAnyFlag(MovementFlag.Swimming | MovementFlag.Flying) ||
                flags2.HasFlag(MovementFlagExtra.AlwaysAllowPitching))
            {
                var swimPitch = packet.ReadSingle();
                Console.WriteLine("Swim Pitch: " + swimPitch);
            }

            var fallTime = packet.ReadInt32();
            Console.WriteLine("Fall Time: " + fallTime);

            if (moveFlags.HasFlag(MovementFlag.Falling))
            {
                packet.ReadSingle("Fall Velocity");
                packet.ReadSingle("Fall Sin angle");
                packet.ReadSingle("Fall Cos angle");
                packet.ReadSingle("Fall Speed");
            }

            if (moveFlags.HasFlag(MovementFlag.SplineElevation))
            {
                var unkSpline = packet.ReadSingle();
                Console.WriteLine("Spline Elevation: " + unkSpline);
            }

            return info;
        }

        [Parser(Opcode.SMSG_MONSTER_MOVE)]
        [Parser(Opcode.SMSG_MONSTER_MOVE_TRANSPORT)]
        public static void HandleMonsterMove(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            if (packet.GetOpcode() == Opcode.SMSG_MONSTER_MOVE_TRANSPORT)
            {
                var transguid = packet.ReadPackedGuid();
                Console.WriteLine("Transport GUID: " + transguid);

                var transseat = packet.ReadByte();
                Console.WriteLine("Transport Seat: " + transseat);
            }

            var unkByte = packet.ReadBoolean();
            Console.WriteLine("Unk Boolean: " + unkByte);

            var pos = packet.ReadVector3();
            Console.WriteLine("Position: " + pos);

            var curTime = packet.ReadInt32();
            Console.WriteLine("Move Ticks: " + curTime);

            var type = (SplineType)packet.ReadByte();
            Console.WriteLine("Spline Type: " + type);

            switch (type)
            {
                case SplineType.FacingSpot:
                {
                    var spot = packet.ReadVector3();
                    Console.WriteLine("Facing Spot: " + spot);
                    break;
                }
                case SplineType.FacingTarget:
                {
                    var tguid = packet.ReadGuid();
                    Console.WriteLine("Facing GUID: " + tguid);
                    break;
                }
                case SplineType.FacingAngle:
                {
                    var angle = packet.ReadSingle();
                    Console.WriteLine("Facing Angle: " + angle);
                    break;
                }
                case SplineType.Stop:
                {
                    return;
                }
            }

            var flags = (SplineFlag)packet.ReadInt32();
            Console.WriteLine("Spline Flags: " + flags);

            if (flags.HasFlag(SplineFlag.AnimationTier))
            {
                var unkByte3 = (MovementAnimationState)packet.ReadByte();
                Console.WriteLine("Animation State: " + unkByte3);

                var unkInt1 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 1: " + unkInt1);
            }

            var time = packet.ReadInt32();
            Console.WriteLine("Move Time: " + time);

            if (flags.HasFlag(SplineFlag.Trajectory))
            {
                var speedZ = packet.ReadSingle();
                Console.WriteLine("Vertical Speed: " + speedZ);

                var unkInt2 = packet.ReadInt32();
                Console.WriteLine("Unk Int32 2: " + unkInt2);
            }

            var waypoints = packet.ReadInt32();
            Console.WriteLine("Waypoints: " + waypoints);

            var newpos = packet.ReadVector3();
            Console.WriteLine("Waypoint 0: " + newpos);

            if (flags.HasFlag(SplineFlag.Flying) || flags.HasFlag(SplineFlag.CatmullRom))
            {
                for (var i = 0; i < waypoints - 1; i++)
                {
                    var vec = packet.ReadVector3();
                    Console.WriteLine("Waypoint " + (i + 1) + ": " + vec);
                }
            }
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

                    Console.WriteLine("Waypoint " + (i + 1) + ": " + vec);
                }
            }
        }

        [Parser(Opcode.SMSG_NEW_WORLD)]
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleEnterWorld(Packet packet)
        {
            var mapId = packet.ReadInt32();
            Console.WriteLine("Map ID: " + Extensions.MapLine(mapId));
            CurrentMapId = mapId;

            var position = packet.ReadVector4();
            Console.WriteLine("Position: " + position);
            CurrentPosition = position;

            UpdateHandler.Objects[mapId] = new Dictionary<Guid, WoWObject>();

            if (packet.GetOpcode() != Opcode.SMSG_LOGIN_VERIFY_WORLD)
                return;

            CharacterInfo chInfo;
            if (!CharacterHandler.Characters.TryGetValue(SessionHandler.LoginGuid, out chInfo))
                return;

            SessionHandler.LoggedInCharacter = chInfo;
        }

        [Parser(Opcode.SMSG_LOGIN_SETTIMESPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            var gameTime = packet.ReadPackedTime();
            Console.WriteLine("Game Time: " + gameTime);

            var gameSpeed = packet.ReadSingle();
            Console.WriteLine("Game Speed: " + gameSpeed);

            var unk = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unk);
        }

        [Parser(Opcode.SMSG_BINDPOINTUPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            packet.ReadVector3("Position");

            Console.WriteLine("Map ID: " + Extensions.MapLine(packet.ReadInt32()));

            packet.ReadInt32("Zone ID");
        }

        [Parser(Opcode.MSG_MOVE_TELEPORT_ACK)]
        public static void HandleTeleportAck(Packet packet)
        {
            if (packet.GetDirection() == Direction.ServerToClient)
            {
                var guid = packet.ReadPackedGuid();
                Console.WriteLine("GUID: " + guid);

                var counter = packet.ReadInt32();
                Console.WriteLine("Movement Counter: " + counter);

                ReadMovementInfo(packet, guid);
            }
            else
            {
                var guid = packet.ReadPackedGuid();
                Console.WriteLine("GUID: " + guid);

                var flags = (MovementFlag)packet.ReadInt32();
                Console.WriteLine("Move Flags: " + flags);

                var time = packet.ReadInt32();
                Console.WriteLine("Time: " + time);
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
            var guid = packet.ReadPackedGuid("GUID");

            ReadMovementInfo(packet, guid);
            if (packet.GetOpcode() == Opcode.MSG_MOVE_KNOCK_BACK)
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

            ReadMovementInfo(packet, guid);

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
            Console.WriteLine("GUID: " + guid);

            var counter = packet.ReadInt32();
            Console.WriteLine("Movement Counter: " + counter);

            ReadMovementInfo(packet, guid);

            var newSpeed = packet.ReadSingle();
            Console.WriteLine("New Speed: " + newSpeed);
        }

        [Parser(Opcode.MSG_MOVE_SET_COLLISION_HGT)]
        [Parser(Opcode.SMSG_MOVE_SET_COLLISION_HGT)]
        [Parser(Opcode.CMSG_MOVE_SET_COLLISION_HGT_ACK)]
        public static void HandleCollisionMovements(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            if (packet.GetOpcode() != Opcode.MSG_MOVE_SET_COLLISION_HGT)
            {
                var counter = packet.ReadInt32();
                Console.WriteLine("Movement Counter: " + counter);
            }

            if (packet.GetOpcode() != Opcode.SMSG_MOVE_SET_COLLISION_HGT)
                ReadMovementInfo(packet, guid);

            var unk = packet.ReadSingle();
            Console.WriteLine("Collision Height: " + unk);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER)]
        [Parser(Opcode.SMSG_MOUNTSPECIAL_ANIM)]
        public static void HandleSetActiveMover(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);
        }

        [Parser(Opcode.CMSG_SUMMON_RESPONSE)]
        public static void HandleSummonResponse(Packet packet)
        {
            var summonerGuid = packet.ReadGuid();
            Console.WriteLine("Summoner GUID: " + summonerGuid);

            var agree = packet.ReadBoolean();
            Console.WriteLine("Accept: " + agree);
        }

        [Parser(Opcode.SMSG_FORCE_MOVE_ROOT)]
        [Parser(Opcode.SMSG_FORCE_MOVE_UNROOT)]
        [Parser(Opcode.SMSG_MOVE_WATER_WALK)]
        [Parser(Opcode.SMSG_MOVE_LAND_WALK)]
        public static void HandleSetMovementMessages(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            var counter = packet.ReadInt32();
            Console.WriteLine("Movement Counter: " + counter);
        }

        [Parser(Opcode.CMSG_MOVE_KNOCK_BACK_ACK)]
        [Parser(Opcode.CMSG_MOVE_WATER_WALK_ACK)]
        [Parser(Opcode.CMSG_MOVE_HOVER_ACK)]
        public static void HandleSpecialMoveAckMessages(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            var unk1 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 1: " + unk1);

            ReadMovementInfo(packet, guid);

            if (packet.GetOpcode() == Opcode.CMSG_MOVE_KNOCK_BACK_ACK)
                return;

            var unk2 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 2: " + unk2);
        }

        [Parser(Opcode.SMSG_SET_PHASE_SHIFT)]
        public static void HandlePhaseShift(Packet packet)
        {
            var phaseMask = packet.ReadInt32();
            Console.WriteLine("Phase Mask: 0x" + phaseMask.ToString("X8"));
            CurrentPhaseMask = phaseMask;
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            Console.WriteLine("Map ID: " + Extensions.MapLine(packet.ReadInt32()));

            if (packet.IsRead())
                return;

            var tEntry = packet.ReadInt32("Transport Entry");
            Console.WriteLine("Transport Entry: " + tEntry);

            Console.WriteLine("Transport Map ID: " + Extensions.MapLine(packet.ReadInt32()));
        }

        [Parser(Opcode.SMSG_TRANSFER_ABORTED)]
        public static void HandleTransferAborted(Packet packet)
        {
            Console.WriteLine("Map ID: " + Extensions.MapLine(packet.ReadInt32()));

            var code = (TransferAbortReason)packet.ReadByte();
            Console.WriteLine("Reason: " + code);

            switch (code)
            {
                case TransferAbortReason.DifficultyUnavailable:
                {
                    var arg = (MapDifficulty)packet.ReadByte();
                    Console.WriteLine("Difficulty: " + arg);
                    break;
                }
                case TransferAbortReason.InsufficientExpansion:
                {
                    var arg = (ClientType)packet.ReadByte();
                    Console.WriteLine("Expansion: " + arg);
                    break;
                }
                case TransferAbortReason.UniqueMessage:
                {
                    var arg = packet.ReadByte();
                    Console.WriteLine("Message ID: " + arg);
                    break;
                }
            }
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            var val = packet.ReadSingle();
            Console.WriteLine("Unk Single: " + val);

            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);
        }
    }
}
