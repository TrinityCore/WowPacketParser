using System;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class MovementHandler
    {
        [ThreadStatic]
        public static uint CurrentMapId;

        public static int CurrentPhaseMask = 1;

        public static MovementInfo ReadMovementInfo(ref Packet packet, Guid guid, int index = -1)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                return ReadMovementInfo420(ref packet, index);

            return ReadMovementInfoGen(ref packet, guid, index);
        }

        private static MovementInfo ReadMovementInfoGen(ref Packet packet, Guid guid, int index)
        {
            var info = new MovementInfo();
            info.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", TypeCode.Int32, index);

            var flagsTypeCode = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056) ? TypeCode.Int16 : TypeCode.Byte;
            info.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", flagsTypeCode, index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                if (packet.ReadGuid("GUID 2", index) != guid)
                    throw new InvalidDataException("Guids are not equal.");

            packet.ReadInt32("Time", index);

            info.Position = packet.ReadVector3("Position", index);
            info.Orientation = packet.ReadSingle("Orientation", index);

            if (info.Flags.HasAnyFlag(MovementFlag.OnTransport))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    packet.ReadPackedGuid("Transport GUID", index);
                else
                    packet.ReadGuid("Transport GUID", index);

                packet.ReadVector4("Transport Position", index);
                packet.ReadInt32("Transport Time", index);

                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadByte("Transport Seat", index);

                if (info.FlagsExtra.HasAnyFlag(MovementFlagExtra.InterpolateMove))
                    packet.ReadInt32("Transport Time", index);
            }

            if (info.Flags.HasAnyFlag(MovementFlag.Swimming | MovementFlag.Flying) ||
                info.FlagsExtra.HasAnyFlag(MovementFlagExtra.AlwaysAllowPitching))
                packet.ReadSingle("Swim Pitch", index);

            if (ClientVersion.RemovedInVersion(ClientType.Cataclysm))
                packet.ReadInt32("Fall Time", index);

            if (info.Flags.HasAnyFlag(MovementFlag.Falling))
            {
                if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                    packet.ReadInt32("Fall Time", index);

                packet.ReadSingle("Fall Velocity", index);
                packet.ReadSingle("Fall Sin Angle", index);
                packet.ReadSingle("Fall Cos Angle", index);
                packet.ReadSingle("Fall Speed", index);
            }

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
                if (info.Flags.HasAnyFlag(MovementFlag.SplineElevation))
                    packet.ReadSingle("Spline Elevation", index);

            return info;
        }

        private static MovementInfo ReadMovementInfo420(ref Packet packet, int index)
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

            info.HasSplineData = packet.ReadBit("HasSplineData", index);

            packet.ResetBitReader(); // reset bitreader

            packet.ReadGuid("GUID 2", index);

            packet.ReadInt32("Time", index);

            info.Position = packet.ReadVector3("Position", index);
            info.Orientation = packet.ReadSingle("Orientation", index);

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

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767)) // no idea when this was added exactly
                    packet.ReadByte("Transport Seat");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767)) // no idea when this was added exactly
                packet.ReadBoolean("Toggle AnimTierInTrans");

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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                // Not the best way
                ReadSplineMovement422(ref packet, pos);
                return;
            }

            var flags = packet.ReadEnum<SplineFlag>("Spline Flags", TypeCode.Int32);

            if (flags.HasAnyFlag(SplineFlag.AnimationTier))
            {
                packet.ReadEnum<MovementAnimationState>("Animation State", TypeCode.Byte);
                packet.ReadInt32("Async-time in ms");
            }

            packet.ReadInt32("Move Time");

            if (flags.HasAnyFlag(SplineFlag.Trajectory))
            {
                packet.ReadSingle("Vertical Speed");
                packet.ReadInt32("Async-time in ms");
            }

            var waypoints = packet.ReadInt32("Waypoints");

            if (flags.HasAnyFlag(SplineFlag.Flying | SplineFlag.CatmullRom))
            {
                for (var i = 0; i < waypoints; i++)
                    packet.ReadVector3("Waypoint", i);
            }
            else
            {
                var newpos = packet.ReadVector3("Waypoint Endpoint");

                var mid = new Vector3();
                mid.X = (pos.X + newpos.X) * 0.5f;
                mid.Y = (pos.Y + newpos.Y) * 0.5f;
                mid.Z = (pos.Z + newpos.Z) * 0.5f;

                for (var i = 1; i < waypoints; i++)
                {
                    var vec = packet.ReadPackedVector3();
                    vec.X += mid.X;
                    vec.Y += mid.Y;
                    vec.Z += mid.Z;

                    packet.WriteLine("[" + i + "]" + " Waypoint: " + vec);
                }
            }
        }

        private static void ReadSplineMovement422(ref Packet packet, Vector3 pos)
        {
            var flags = packet.ReadEnum<SplineFlag422>("Spline Flags", TypeCode.Int32);

            if (flags.HasAnyFlag(SplineFlag422.AnimationTier))
            {
                packet.ReadEnum<MovementAnimationState>("Animation State", TypeCode.Byte);
                packet.ReadInt32("Asynctime in ms"); // Async-time in ms
            }

            packet.ReadInt32("Move Time");

            if (flags.HasAnyFlag(SplineFlag422.Trajectory))
            {
                packet.ReadSingle("Vertical Speed");
                packet.ReadInt32("Unk Int32 2");
            }

            var waypoints = packet.ReadInt32("Waypoints");

            if (flags.HasAnyFlag(SplineFlag422.UsePathSmoothing))
            {
                for (var i = 0; i < waypoints; i++)
                    packet.ReadVector3("Waypoint", i);
            }
            else
            {
                var newpos = packet.ReadVector3("Waypoint Endpoint");

                var mid = new Vector3();
                mid.X = (pos.X + newpos.X) * 0.5f;
                mid.Y = (pos.Y + newpos.Y) * 0.5f;
                mid.Z = (pos.Z + newpos.Z) * 0.5f;

                for (var i = 1; i < waypoints; i++)
                {
                    var vec = packet.ReadPackedVector3();
                    vec.X += mid.X;
                    vec.Y += mid.Y;
                    vec.Z += mid.Z;

                    packet.WriteLine("[" + i + "]" + " Waypoint: " + vec);
                }
            }
        }

        [Parser(Opcode.SMSG_NEW_WORLD, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleEnterWorld(Packet packet)
        {
            CurrentMapId = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadVector4("Position");

            WoWObject character;
            if (Storage.Objects.TryGetValue(SessionHandler.LoginGuid, out character))
                SessionHandler.LoggedInCharacter = (Player) character;

            packet.AddSniffData(StoreNameType.Map, (int) CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_NEW_WORLD, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleNewWorld(Packet packet)
        {
            packet.ReadVector3("Position");
            CurrentMapId = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            packet.ReadSingle("Orientation");

            WoWObject character;
            if (Storage.Objects.TryGetValue(SessionHandler.LoginGuid, out character))
                SessionHandler.LoggedInCharacter = (Player) character;

            packet.AddSniffData(StoreNameType.Map, (int)CurrentMapId, "NEW_WORLD");
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
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id");
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
        }

        [Parser(Opcode.CMSG_UPDATE_MISSILE_TRAJECTORY)]
        public static void HandleUpdateMissileTrajectory(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadSingle("Elevation");
            packet.ReadSingle("Missile speed?");
            packet.ReadVector3("Current Position");
            packet.ReadVector3("Targeted Position");

            // Boolean if it will send MSG_MOVE_STOP
            if (!packet.ReadBoolean())
                return;

            var opcode = packet.ReadInt32();
            // None length is recieved, so we have to calculate the remaining bytes.
            var remainingLength = packet.Length - packet.Position;
            var bytes = packet.ReadBytes((int)remainingLength);

            using (var newpacket = new Packet(bytes, opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
                Handler.Parse(newpacket, isMultiple: true);
        }

        [Parser(Opcode.MSG_MOVE_TELEPORT_ACK)]
        public static void HandleTeleportAck(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                var guid = packet.ReadPackedGuid();
                packet.WriteLine("GUID: " + guid);

                var counter = packet.ReadInt32();
                packet.WriteLine("Movement Counter: " + counter);

                ReadMovementInfo(ref packet, guid);
            }
            else
            {
                var guid = packet.ReadPackedGuid();
                packet.WriteLine("GUID: " + guid);

                var flags = (MovementFlag)packet.ReadInt32();
                packet.WriteLine("Move Flags: " + flags);

                var time = packet.ReadInt32();
                packet.WriteLine("Time: " + time);
            }
        }

        [Parser(Opcode.MSG_MOVE_HEARTBEAT, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleMovementHeartbeat422(Packet packet)
        {
            packet.ReadEnum<MovementFlag>("Movement flags", 30);

            packet.ReadBit("HasSplineData");

            var guidBytes = new byte[8];
            guidBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);

            packet.ReadEnum<MovementFlagExtra>("Movement flags extra", 12);

            guidBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
            var splineElevation = packet.ReadBit("SplineElevation"); // OR Swimming
            var onTransport = packet.ReadBit("OnTransport");

            var transportBytes = new byte[8];
            var hasInterpolatedMovement = false;
            var time3 = false;
            if (onTransport)
            {
                transportBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
                hasInterpolatedMovement = packet.ReadBit("HasInterpolatedMovement");
                time3 = packet.ReadBit("Time3");
            }

            var swimming = packet.ReadBit("Swimming");  // OR SplineElevation
            var interPolatedTurning = packet.ReadBit("InterPolatedTurning");
            var jumping = false;
            if (interPolatedTurning)
                jumping = packet.ReadBit("Jumping");

            packet.ReadInt32("Time");
            packet.ReadVector4("Position");

            if (guidBytes[7] != 0)
                guidBytes[7] ^= packet.ReadByte();

            if (guidBytes[5] != 0)
                guidBytes[5] ^= packet.ReadByte();

            if (splineElevation)
                packet.ReadSingle("Spline Elevation");

            if (guidBytes[1] != 0)
                guidBytes[1] ^= packet.ReadByte();

            if (guidBytes[6] != 0)
                guidBytes[6] ^= packet.ReadByte();

            if (guidBytes[4] != 0)
                guidBytes[4] ^= packet.ReadByte();

            if (guidBytes[3] != 0)
                guidBytes[3] ^= packet.ReadByte();

            if (onTransport)
            {
                packet.ReadByte("Transport Seat");
                packet.ReadSingle("Transport Orientation");
                packet.ReadVector3("Transport Position");
                packet.ReadInt32("Transport Time");
                if (hasInterpolatedMovement)
                    packet.ReadInt32("Transport Time 2");

                if (transportBytes[3] != 0)
                    transportBytes[3] ^= packet.ReadByte();

                if (transportBytes[6] != 0)
                    transportBytes[6] ^= packet.ReadByte();

                if (time3)
                    packet.ReadInt32("Transport Time 3");

                if (transportBytes[7] != 0)
                    transportBytes[7] ^= packet.ReadByte();

                if (transportBytes[5] != 0)
                    transportBytes[5] ^= packet.ReadByte();

                if (transportBytes[2] != 0)
                    transportBytes[2] ^= packet.ReadByte();

                if (transportBytes[1] != 0)
                    transportBytes[1] ^= packet.ReadByte();

                if (transportBytes[0] != 0)
                    transportBytes[0] ^= packet.ReadByte();

                if (transportBytes[4] != 0)
                    transportBytes[4] ^= packet.ReadByte();

                packet.WriteLine("Transport GUID: {0}", new Guid(BitConverter.ToUInt64(transportBytes, 0)));
            }

            if (swimming)
                packet.ReadSingle("Swim Pitch");

            if (interPolatedTurning)
            {
                packet.ReadInt32("Time Fallen");
                packet.ReadSingle("Fall Start Velocity");
                if (jumping)
                {
                    packet.ReadSingle("Jump Velocity");
                    packet.ReadSingle("Jump Cos");
                    packet.ReadSingle("Jump Sin");

                }
            }

            if (guidBytes[2] != 0)
                guidBytes[2] ^= packet.ReadByte();

            if (guidBytes[0] != 0)
                guidBytes[0] ^= packet.ReadByte();

            packet.WriteLine("GUID: {0}", new Guid(BitConverter.ToUInt64(guidBytes, 0)));
        }

        [Parser(Opcode.MSG_MOVE_SET_PITCH, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleMovementSetPitch422(Packet packet)
        {
            var guidBytes = new byte[8];
            guidBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);

            packet.ReadEnum<MovementFlag>("Movement flags", 30);

            guidBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);

            packet.ReadBit("HasSplineData");

            guidBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);

            packet.ReadEnum<MovementFlagExtra>("Movement flags extra", 12);

            var splineElevation = packet.ReadBit("SplineElevation"); // OR Swimming
            var onTransport = packet.ReadBit("OnTransport");

            var transportBytes = new byte[8];
            var hasInterpolatedMovement = false;
            var time3 = false;
            if (onTransport)
            {
                transportBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);
                transportBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
                hasInterpolatedMovement = packet.ReadBit("HasInterpolatedMovement");
                time3 = packet.ReadBit("HasTime3");
            }

            var swimming = packet.ReadBit("HasPitch");  // OR SplineElevation
            var interPolatedTurning = packet.ReadBit("HasFallData");
            var jumping = false;
            if (interPolatedTurning)
                jumping = packet.ReadBit("HasFallDirection");

            packet.ReadVector3("Position");
            packet.ReadInt32("Time");
            packet.ReadSingle("Orientation");

            if (guidBytes[1] != 0) guidBytes[1] ^= packet.ReadByte();
            if (guidBytes[4] != 0) guidBytes[4] ^= packet.ReadByte();

            if (splineElevation)
                packet.ReadSingle("Spline Elevation");

            if (onTransport)
            {
                packet.ReadByte("Transport Seat");
                packet.ReadSingle("Transport Orientation");
                packet.ReadVector3("Transport Position");
                packet.ReadInt32("Transport Time");
                if (hasInterpolatedMovement)
                    packet.ReadInt32("Transport Time 2");

                if (transportBytes[3] != 0) transportBytes[3] ^= packet.ReadByte();
                if (transportBytes[6] != 0) transportBytes[6] ^= packet.ReadByte();

                if (time3)
                    packet.ReadInt32("Transport Time 3");

                if (transportBytes[7] != 0) transportBytes[7] ^= packet.ReadByte();
                if (transportBytes[5] != 0) transportBytes[5] ^= packet.ReadByte();
                if (transportBytes[2] != 0) transportBytes[2] ^= packet.ReadByte();
                if (transportBytes[1] != 0) transportBytes[1] ^= packet.ReadByte();
                if (transportBytes[0] != 0) transportBytes[0] ^= packet.ReadByte();
                if (transportBytes[4] != 0) transportBytes[4] ^= packet.ReadByte();

                packet.WriteLine("Transport GUID: {0}", new Guid(BitConverter.ToUInt64(transportBytes, 0)));
            }

            if (swimming)
                packet.ReadSingle("Swim Pitch");

            if (guidBytes[5] != 0) guidBytes[5] ^= packet.ReadByte();

            if (interPolatedTurning)
            {
                packet.ReadInt32("Time Fallen");
                packet.ReadSingle("Fall Start Velocity");
                if (jumping)
                {
                    packet.ReadSingle("Jump Velocity");
                    packet.ReadSingle("Jump Sin");
                    packet.ReadSingle("Jump Cos");

                }
            }

            if (guidBytes[0] != 0) guidBytes[0] ^= packet.ReadByte();
            if (guidBytes[3] != 0) guidBytes[3] ^= packet.ReadByte();
            if (guidBytes[6] != 0) guidBytes[6] ^= packet.ReadByte();
            if (guidBytes[7] != 0) guidBytes[7] ^= packet.ReadByte();
            if (guidBytes[2] != 0) guidBytes[2] ^= packet.ReadByte();

            packet.WriteLine("GUID: {0}", new Guid(BitConverter.ToUInt64(guidBytes, 0)));
        }

        [Parser(Opcode.MSG_MOVE_SET_FACING, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleMovementSetFacing422(Packet packet)
        {
            var info = new MovementInfo();
            var guidBytes = new byte[8];
            var transportGuidBytes = new byte[8];

            info.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            guidBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);

            info.HasSplineData = packet.ReadBit("HasSplineData");

            guidBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);

            packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            guidBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);

            var splineElevation = packet.ReadBit("HaveSplineElevation");

            var havePitch = packet.ReadBit("HavePitch");
            var haveFallData = packet.ReadBit("HaveFallData");
            var haveFallDirection = false;

            if (haveFallData)
                haveFallDirection = packet.ReadBit("HaveFallDirection");

            var haveTransportData = packet.ReadBit("HaveTransportData");

            var haveTransportTime2 = false;
            var haveTransportTime3 = false;

            if (haveTransportData)
            {
                transportGuidBytes = packet.StartBitStream(0, 6, 2, 5, 4, 1, 3, 7);
                haveTransportTime2 = packet.ReadBit("HaveTransportTime2");
                haveTransportTime3 = packet.ReadBit("HaveTransportTime3");
            }

            info.Orientation = packet.ReadSingle("Orientation");

            packet.ReadUInt32("Timestamp");

            info.Position = packet.ReadVector3("Position");

            packet.ParseBitStream(guidBytes, 7, 5);

            if (splineElevation)
                packet.ReadSingle("Spline Elevation");

            packet.ParseBitStream(guidBytes, 4, 1, 2);

            if (havePitch)
                packet.ReadSingle("Pitch");

            if (haveFallData)
            {
                packet.ReadUInt32("Fall Time");
                packet.ReadSingle("Fall Vertical Speed");
                packet.ReadSingle("Fall Horizontal Speed");

                if (haveFallDirection)
                {
                    packet.ReadSingle("Fall Cos Angle");
                    packet.ReadSingle("Fall Sin Angle");
                }
            }

            packet.ParseBitStream(guidBytes, 6, 0);

            if (haveTransportData)
            {
                packet.ReadByte("Transport Seat");
                packet.ReadSingle("Transport Orientation");
                packet.ReadVector3("Transport Position");

                packet.ReadUInt32("Transport Time");

                if (haveTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ParseBitStream(transportGuidBytes, 3, 6);

                if (haveTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ParseBitStream(transportGuidBytes, 7, 5, 2, 1, 0, 4);
            }

            packet.ParseBitStream(guidBytes, 3);

            packet.ToGuid("Guid", guidBytes);
            packet.ToGuid("Transport Guid", transportGuidBytes);
        }

        [Parser(Opcode.MSG_MOVE_TELEPORT, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleMoveTeleport422(Packet packet)
        {
            var onTransport = packet.ReadBit("OnTransport");

            var guid = packet.StartBitStream(0, 2, 6, 7, 4, 5, 3, 1);

            var unk2 = packet.ReadBit("Unk Bit Boolean 2");

            packet.ReadVector3("Destination Position");

            packet.ParseBitStream(guid, 5);
            packet.ParseBitStream(guid, 4);

            if (onTransport)
                packet.ReadGuid("Transport Guid");

            packet.ParseBitStream(guid, 2);
            packet.ParseBitStream(guid, 7);

            packet.ReadInt32("Unk 1");

            packet.ParseBitStream(guid, 1, 0, 6, 3);

            if (unk2)
                packet.ReadByte("Unk 2");

            packet.ReadSingle("Arrive Orientation");
            packet.ToGuid("Guid", guid);
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
        [Parser(Opcode.MSG_MOVE_TELEPORT, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.MSG_MOVE_SET_FACING, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.MSG_MOVE_SET_PITCH, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.MSG_MOVE_TOGGLE_COLLISION_CHEAT)]
        [Parser(Opcode.MSG_MOVE_GRAVITY_CHNG)]
        [Parser(Opcode.MSG_MOVE_ROOT)]
        [Parser(Opcode.MSG_MOVE_UNROOT)]
        [Parser(Opcode.MSG_MOVE_START_SWIM)]
        [Parser(Opcode.MSG_MOVE_STOP_SWIM)]
        [Parser(Opcode.MSG_MOVE_START_SWIM_CHEAT)]
        [Parser(Opcode.MSG_MOVE_STOP_SWIM_CHEAT)]
        [Parser(Opcode.MSG_MOVE_HEARTBEAT, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
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
            if ((ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ||
                packet.Direction == Direction.ServerToClient) && ClientVersion.Build != ClientVersionBuild.V4_2_2_14545)
                guid = packet.ReadPackedGuid("GUID");
            else
                guid = new Guid();

            ReadMovementInfo(ref packet, guid);

            if (packet.Opcode != Opcodes.GetOpcode(Opcode.MSG_MOVE_KNOCK_BACK))
                return;

            packet.ReadSingle("Sin Angle");
            packet.ReadSingle("Cos Angle");
            packet.ReadSingle("Speed");
            packet.ReadSingle("Velocity");
        }

        [Parser(Opcode.SMSG_SPLINE_SET_RUN_SPEED, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleSplineMovementSetRunSpeed422(Packet packet)
        {
            var guid = packet.StartBitStream(7, 2, 1, 3, 5, 6, 4, 0);
            packet.ParseBitStream(guid, 6, 7, 4, 3, 2, 5, 0, 1);
            packet.ReadSingle("Speed");
            packet.ToGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_SET_RUN_SPEED, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleMovementSetRunSpeed422(Packet packet)
        {
            var guid = packet.StartBitStream(1, 0, 7, 5, 2, 4, 3, 6);
            packet.ParseBitStream(guid, 1);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 6, 2, 3, 7, 4, 0, 5);
            packet.ReadUInt32("Move Event");
            packet.ToGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_SET_WALK_SPEED)]
        [Parser(Opcode.MSG_MOVE_SET_RUN_SPEED, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
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
            packet.WriteLine("GUID: " + guid);

            var counter = packet.ReadInt32();
            packet.WriteLine("Movement Counter: " + counter);

            ReadMovementInfo(ref packet, guid);

            var newSpeed = packet.ReadSingle();
            packet.WriteLine("New Speed: " + newSpeed);
        }

        [Parser(Opcode.MSG_MOVE_SET_COLLISION_HGT)]
        [Parser(Opcode.SMSG_MOVE_SET_COLLISION_HGT)]
        [Parser(Opcode.CMSG_MOVE_SET_COLLISION_HGT_ACK)]
        public static void HandleCollisionMovements(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            packet.WriteLine("GUID: " + guid);

            if (packet.Opcode != Opcodes.GetOpcode(Opcode.MSG_MOVE_SET_COLLISION_HGT))
            {
                var counter = packet.ReadInt32();
                packet.WriteLine("Movement Counter: " + counter);
            }

            if (packet.Opcode != Opcodes.GetOpcode(Opcode.SMSG_MOVE_SET_COLLISION_HGT))
                ReadMovementInfo(ref packet, guid);

            var unk = packet.ReadSingle();
            packet.WriteLine("Collision Height: " + unk);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER)]
        [Parser(Opcode.SMSG_MOUNTSPECIAL_ANIM)]
        public static void HandleSetActiveMover(Packet packet)
        {
            packet.ReadGuid("GUID");
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
            packet.WriteLine("GUID: " + guid);

            var counter = packet.ReadInt32();
            packet.WriteLine("Movement Counter: " + counter);
        }

        [Parser(Opcode.CMSG_MOVE_KNOCK_BACK_ACK)]
        [Parser(Opcode.CMSG_MOVE_WATER_WALK_ACK)]
        [Parser(Opcode.CMSG_MOVE_HOVER_ACK)]
        public static void HandleSpecialMoveAckMessages(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            packet.WriteLine("GUID: " + guid);

            var unk1 = packet.ReadInt32();
            packet.WriteLine("Unk Int32 1: " + unk1);

            ReadMovementInfo(ref packet, guid);

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_MOVE_KNOCK_BACK_ACK))
                return;

            var unk2 = packet.ReadInt32();
            packet.WriteLine("Unk Int32 2: " + unk2);
        }

        [Parser(Opcode.SMSG_SET_PHASE_SHIFT)]
        public static void HandlePhaseShift(Packet packet)
        {
            var phaseMask = packet.ReadInt32();
            packet.WriteLine("Phase Mask: 0x" + phaseMask.ToString("X8"));
            CurrentPhaseMask = phaseMask;

            packet.AddSniffData(StoreNameType.Phase, phaseMask, "PHASEMASK");
        }

        [Parser(Opcode.SMSG_SET_PHASE_SHIFT, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandlePhaseShift406(Packet packet)
        {
            packet.ReadGuid("GUID");
            var i = 0;
            int count = packet.ReadInt32();
            for (var j = 0; j < count / 2; ++j)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Unk", i, j);

            i++;
            count = packet.ReadInt32();
            for (var j = 0; j < count / 2; ++j)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Terrain Swap 1", i, j);

            i++;
            count = packet.ReadInt32();
            var phaseMask = 0;
            for (var j = 0; j < count / 2; ++j)
                phaseMask = packet.ReadInt16("Phases", ++i, j);

            i++;
            count = packet.ReadInt32();
            for (var j = 0; j < count / 2; ++j)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Terrain Swap 2", i, j);

            packet.ReadUInt32("Flag"); // can be 0, 4 or 8, 8 = normal world, others are unknown

            //CurrentPhaseMask = phaseMask;
            packet.AddSniffData(StoreNameType.Phase, phaseMask, "PHASEMASK 406");
        }

        [Parser(Opcode.SMSG_SET_PHASE_SHIFT, ClientVersionBuild.V4_2_2_14545)]
        public static void HandlePhaseShift422(Packet packet)
        {
            var bits = new bool[8];
            for (var x = 0; x < 8; ++x)
                bits[x] = packet.ReadBit();

            var bytes = new byte[8];
            if (bits[6]) bytes[0] = (byte)(packet.ReadByte() ^ 1);
            if (bits[3]) bytes[4] = (byte)(packet.ReadByte() ^ 1);

            var i = 0;
            var count = packet.ReadInt32();
            for (var j = 0; j < count / 2; ++j)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Map Swap 1", i, j);

            if (bits[5]) bytes[3] = (byte)(packet.ReadByte() ^ 1);

            packet.WriteLine("[" + i + "]" + " Mask: 0x" + packet.ReadUInt32().ToString("X2"));

            if (bits[2]) bytes[2] = (byte)(packet.ReadByte() ^ 1);

            var phaseMask = 0;
            count = packet.ReadInt32();
            for (var j = 0; j < count / 2; ++j)
                phaseMask = packet.ReadUInt16("Current Mask", i, j);

            if (bits[0]) bytes[6] = (byte)(packet.ReadByte() ^ 1);

            i++;
            count = packet.ReadInt32();
            for (var j = 0; j < count / 2; ++j)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Map Swap 1", i, j);

            if (bits[2]) bytes[7] = (byte)(packet.ReadByte() ^ 1);

            i++;
            count = packet.ReadInt32();
            for (var j = 0; j < count / 2; ++j)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Map Swap 3", i, j);

            if (bits[7]) bytes[5] = (byte)(packet.ReadByte() ^ 1);
            if (bits[1]) bytes[1] = (byte)(packet.ReadByte() ^ 1);

            var guid = new Guid(BitConverter.ToUInt64(bytes, 0));
            packet.WriteLine("GUID: {0}", guid);

            //CurrentPhaseMask = phaseMask;
            packet.AddSniffData(StoreNameType.Phase, phaseMask, "PHASEMASK 422");
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

            if (!packet.CanRead())
                return;

            packet.ReadInt32("Transport Entry");
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Transport Map ID");
        }

        [Parser(Opcode.SMSG_TRANSFER_ABORTED)]
        public static void HandleTransferAborted(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

            var reason = packet.ReadEnum<TransferAbortReason>("Reason", TypeCode.Byte);

            switch (reason)
            {
                case TransferAbortReason.DifficultyUnavailable:
                {
                    packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Byte);
                    break;
                }
                case TransferAbortReason.InsufficientExpansion:
                {
                    packet.ReadEnum<ClientType>("Expansion", TypeCode.Byte);
                    break;
                }
                case TransferAbortReason.UniqueMessage:
                {
                    packet.ReadByte("Message ID");
                    break;
                }
                default:
                    packet.ReadByte(); // Does nothing
                    break;
            }
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            packet.ReadSingle("Duration modifier");
            packet.ReadPackedGuid("GUID");
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
        [Parser(Opcode.SMSG_SPLINE_SET_RUN_SPEED, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
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
            packet.WriteLine("{"); // To be able to see what is inside this packet.
            packet.WriteLine();

            using (var pkt = packet.Inflate(packet.ReadInt32()))
            {
                while (pkt.CanRead())
                {
                    var size = pkt.ReadByte();
                    var opc = pkt.ReadInt16();
                    var data = pkt.ReadBytes(size - 2);

                    using (var newPacket = new Packet(data, opc, pkt.Time, pkt.Direction, pkt.Number, packet.Writer, packet.FileName))
                    {
                        Handler.Parse(newPacket, true);
                    }
                }
            }

            packet.WriteLine("}");
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_MOVE_KNOCK_BACK, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleMoveKnockBack422(Packet packet)
        {
            var guid = packet.StartBitStream(5, 2, 6, 3, 1, 4, 0, 7);

            packet.ParseBitStream(guid, 0);

            packet.ReadSingle("Jump Velocity");
            packet.ReadUInt32("Fall time");
            packet.ReadSingle("Fall Start Velocity");

            packet.ParseBitStream(guid, 6);

            packet.ReadSingle("Jump Cos");
            packet.ReadSingle("Jump Sin");

            packet.ParseBitStream(guid, 3, 1, 2, 4, 7, 5);

            packet.ToGuid("Guid", guid);
        }
    }
}
