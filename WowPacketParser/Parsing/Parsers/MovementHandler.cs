using System;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
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
            if (ClientVersion.Build == ClientVersionBuild.V4_2_0_14333)
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
                    info.TransportGuid = packet.ReadPackedGuid("Transport GUID", index);
                else
                    info.TransportGuid = packet.ReadGuid("Transport GUID", index);

                info.TransportOffset = packet.ReadVector4("Transport Position", index);
                packet.ReadInt32("Transport Time", index);

                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadByte("Transport Seat", index);

                if (info.FlagsExtra.HasAnyFlag(MovementFlagExtra.InterpolateMove))
                    packet.ReadInt32("Transport Time", index);
            }

            if (info.Flags.HasAnyFlag(MovementFlag.Swimming | MovementFlag.Flying) ||
                info.FlagsExtra.HasAnyFlag(MovementFlagExtra.AlwaysAllowPitching))
                packet.ReadSingle("Swim Pitch", index);

            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
            {
                if (info.FlagsExtra.HasAnyFlag(MovementFlagExtra.InterpolateTurning))
                {
                    packet.ReadInt32("Fall Time", index);
                    packet.ReadSingle("Fall Velocity", index);

                    if (info.Flags.HasAnyFlag(MovementFlag.Falling))
                    {
                        packet.ReadSingle("Fall Sin Angle", index);
                        packet.ReadSingle("Fall Cos Angle", index);
                        packet.ReadSingle("Fall Speed", index);
                    }
                }
            }
            else
            {
                packet.ReadInt32("Fall Time", index);
                if (info.Flags.HasAnyFlag(MovementFlag.Falling))
                {
                    packet.ReadSingle("Fall Velocity", index);
                    packet.ReadSingle("Fall Sin Angle", index);
                    packet.ReadSingle("Fall Cos Angle", index);
                    packet.ReadSingle("Fall Speed", index);
                }
            }

            // HACK: "generic" movement flags are wrong for 4.2.2
            if (info.Flags.HasAnyFlag(MovementFlag.SplineElevation) && ClientVersion.Build != ClientVersionBuild.V4_2_2_14545)
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
                info.TransportGuid = packet.ReadGuid("Transport GUID", index);
                info.TransportOffset = packet.ReadVector4("Transport Position", index);
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
            var guid = packet.ReadPackedGuid("GUID");

            if (Storage.Objects != null && Storage.Objects.ContainsKey(guid))
            {
                var obj = Storage.Objects[guid].Item1;
                UpdateField uf;
                if (obj.UpdateFields != null && obj.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_FLAGS), out uf))
                    if ((uf.UInt32Value & (uint)UnitFlags.IsInCombat) == 0) // movement could be because of aggro so ignore that
                        obj.Movement.HasWpsOrRandMov = true;
            }

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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
            {
                // Not the best way
                ReadSplineMovement510(ref packet, pos);
                return;
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
            {
                // Not the best way
                ReadSplineMovement434(ref packet, pos);
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

        private static void ReadSplineMovement510(ref Packet packet, Vector3 pos)
        {
            var flags = packet.ReadEnum<SplineFlag434>("Spline Flags", TypeCode.Int32);

            if (flags.HasAnyFlag(SplineFlag434.Animation))
            {
                packet.ReadEnum<MovementAnimationState>("Animation State", TypeCode.Byte);
                packet.ReadInt32("Asynctime in ms"); // Async-time in ms
            }

            packet.ReadInt32("Move Time");

            if (flags.HasAnyFlag(SplineFlag434.Parabolic))
            {
                packet.ReadSingle("Vertical Speed");
                packet.ReadInt32("Async-time in ms");
            }

            var waypoints = packet.ReadInt32("Waypoints");

            if (flags.HasAnyFlag(SplineFlag434.UncompressedPath))
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

                if (waypoints != 1)
                {
                    var vec = packet.ReadPackedVector3();
                    vec.X += mid.X;
                    vec.Y += mid.Y;
                    vec.Z += mid.Z;
                    packet.WriteLine("[0] Waypoint: " + vec);

                    if (waypoints > 2)
                    {
                        for (var i = 1; i < waypoints - 1; ++i)
                        {
                            vec = packet.ReadPackedVector3();
                            vec.X += mid.X;
                            vec.Y += mid.Y;
                            vec.Z += mid.Z;

                            packet.WriteLine("[" + i + "]" + " Waypoint: " + vec);
                        }
                    }
                }
            }

            var unkLoopCounter = packet.ReadUInt16("Unk UInt16");
            if (unkLoopCounter > 1)
            {
                packet.ReadSingle("Unk Float 1");
                packet.ReadUInt16("Unk UInt16 1");
                packet.ReadUInt16("Unk UInt16 2");
                packet.ReadSingle("Unk Float 2");
                packet.ReadUInt16("Unk UInt16 3");

                for (var i = 0; i < unkLoopCounter; i++)
                {
                    packet.ReadUInt16("Unk UInt16 1", i);
                    packet.ReadUInt16("Unk UInt16 2", i);
                }
            }
        }

        private static void ReadSplineMovement434(ref Packet packet, Vector3 pos)
        {
            var flags = packet.ReadEnum<SplineFlag434>("Spline Flags", TypeCode.Int32);

            if (flags.HasAnyFlag(SplineFlag434.Animation))
            {
                packet.ReadEnum<MovementAnimationState>("Animation State", TypeCode.Byte);
                packet.ReadInt32("Asynctime in ms"); // Async-time in ms
            }

            packet.ReadInt32("Move Time");

            if (flags.HasAnyFlag(SplineFlag434.Parabolic))
            {
                packet.ReadSingle("Vertical Speed");
                packet.ReadInt32("Async-time in ms");
            }

            var waypoints = packet.ReadInt32("Waypoints");

            if (flags.HasAnyFlag(SplineFlag434.UncompressedPath))
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

                if (waypoints != 1)
                {
                    var vec = packet.ReadPackedVector3();
                    vec.X += mid.X;
                    vec.Y += mid.Y;
                    vec.Z += mid.Z;
                    packet.WriteLine("[0] Waypoint: " + vec);

                    if (waypoints > 2)
                    {
                        for (var i = 1; i < waypoints - 1; ++i)
                        {
                            vec = packet.ReadPackedVector3();
                            vec.X += mid.X;
                            vec.Y += mid.Y;
                            vec.Z += mid.Z;

                            packet.WriteLine("[" + i + "]" + " Waypoint: " + vec);
                        }
                    }
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

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleEnterWorld(Packet packet)
        {
            CurrentMapId = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadVector4("Position");

            packet.AddSniffData(StoreNameType.Map, (int) CurrentMapId, "NEW_WORLD");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleNewWorld422(Packet packet)
        {
            packet.ReadVector3("Position");
            CurrentMapId = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            packet.ReadSingle("Orientation");

            packet.AddSniffData(StoreNameType.Map, (int)CurrentMapId, "NEW_WORLD");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleNewWorld434(Packet packet)
        {
            packet.ReadSingle("X");
            packet.ReadSingle("Orientation");
            packet.ReadSingle("Y");
            CurrentMapId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            packet.ReadSingle("Z"); // seriously...

            packet.AddSniffData(StoreNameType.Map, (int)CurrentMapId, "NEW_WORLD");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleNewWorld510(Packet packet)
        {
            CurrentMapId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            packet.ReadSingle("Y");
            packet.ReadSingle("Orientation");
            packet.ReadSingle("X");
            packet.ReadSingle("Z");

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
            packet.ReadSingle("Missile speed");
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
                Handler.Parse(newpacket, true);
        }

        [Parser(Opcode.MSG_MOVE_TELEPORT_ACK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTeleportAck(Packet packet)
        {
            var guid = packet.ReadPackedGuid("Guid");

            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadInt32("Movement Counter");
                ReadMovementInfo(ref packet, guid);
            }
            else
            {
                packet.ReadEnum<MovementFlag>("Move Flags", TypeCode.Int32);
                packet.ReadInt32("Time");
            }
        }

        [Parser(Opcode.MSG_MOVE_TELEPORT_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveTeleportAck434(Packet packet)
        {
            packet.ReadInt32("Unk Int32 1");
            packet.ReadInt32("Unk Int32 2");
            var guid = packet.StartBitStream(5, 0, 1, 6, 3, 7, 2, 4);
            packet.ParseBitStream(guid, 4, 2, 7, 6, 5, 1, 3, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_HEARTBEAT, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleMovementHeartbeat422(Packet packet)
        {
            packet.ReadEnum<MovementFlag>("Movement flags", 30);

            packet.ReadBit("HasSplineData");

            var guidBytes = new byte[8];
            guidBytes[0] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            guidBytes[3] = packet.ReadBit();

            packet.ReadEnum<MovementFlagExtra>("Movement flags extra", 12);

            guidBytes[5] = packet.ReadBit();
            var splineElevation = packet.ReadBit("SplineElevation"); // OR Swimming
            var onTransport = packet.ReadBit("OnTransport");

            var transportBytes = new byte[8];
            var hasInterpolatedMovement = false;
            var time3 = false;
            if (onTransport)
            {
                transportBytes = packet.StartBitStream(0, 6, 2, 5, 4, 1, 3, 7);
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

            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 5);

            if (splineElevation)
                packet.ReadSingle("Spline Elevation");

            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 3);

            if (onTransport)
            {
                packet.ReadByte("Transport Seat");
                packet.ReadSingle("Transport Orientation");
                packet.ReadVector3("Transport Position");
                packet.ReadInt32("Transport Time");
                if (hasInterpolatedMovement)
                    packet.ReadInt32("Transport Time 2");

                packet.ReadXORByte(transportBytes, 3);
                packet.ReadXORByte(transportBytes, 6);

                if (time3)
                    packet.ReadInt32("Transport Time 3");

                packet.ReadXORByte(transportBytes, 7);
                packet.ReadXORByte(transportBytes, 5);
                packet.ReadXORByte(transportBytes, 2);
                packet.ReadXORByte(transportBytes, 1);
                packet.ReadXORByte(transportBytes, 0);
                packet.ReadXORByte(transportBytes, 4);

                packet.WriteGuid("Transport Guid", transportBytes);
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

            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 0);

            packet.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.MSG_MOVE_HEARTBEAT, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMovementHeartbeat433(Packet packet)
        {
            packet.ReadEnum<MovementFlag>("Movement flags", 30);

            packet.ReadBit("HasSplineData");

            var guidBytes = new byte[8];
            guidBytes[0] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            guidBytes[3] = packet.ReadBit();

            packet.ReadEnum<MovementFlagExtra>("Movement flags extra", 12);

            guidBytes[5] = packet.ReadBit();
            var splineElevation = packet.ReadBit("SplineElevation"); // OR Swimming
            var onTransport = packet.ReadBit("OnTransport");

            var transportBytes = new byte[8];
            var hasInterpolatedMovement = false;
            var time3 = false;
            if (onTransport)
            {
                transportBytes = packet.StartBitStream(0, 6, 2, 5, 4, 1, 3, 7);
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

            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 5);

            if (splineElevation)
                packet.ReadSingle("Spline Elevation");

            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 3);

            if (onTransport)
            {
                packet.ReadByte("Transport Seat");
                packet.ReadSingle("Transport Orientation");
                packet.ReadVector3("Transport Position");
                packet.ReadInt32("Transport Time");
                if (hasInterpolatedMovement)
                    packet.ReadInt32("Transport Time 2");

                packet.ReadXORByte(transportBytes, 3);
                packet.ReadXORByte(transportBytes, 6);

                if (time3)
                    packet.ReadInt32("Transport Time 3");

                packet.ReadXORByte(transportBytes, 7);
                packet.ReadXORByte(transportBytes, 5);
                packet.ReadXORByte(transportBytes, 2);
                packet.ReadXORByte(transportBytes, 1);
                packet.ReadXORByte(transportBytes, 0);
                packet.ReadXORByte(transportBytes, 4);

                packet.WriteGuid("Transport Guid", transportBytes);
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

            packet.ReadXORByte(guidBytes, 2);

            packet.ReadXORByte(guidBytes, 0);

            packet.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.MSG_MOVE_HEARTBEAT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveHeartbeat434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasFallData = packet.ReadBit("Has fall data");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            packet.ReadBit("Has Spline");
            packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();

            if (hasTrans)
            {
                hasTransTime3 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 3, 6, 1, 7, 2, 5, 0, 4);

            if (hasTrans)
            {
                var tpos = new Vector4();
                tpos.Z = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                tpos.Y = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 7);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 6);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasO)
                pos.O = packet.ReadSingle();

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_SET_PITCH, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleMovementSetPitch422(Packet packet)
        {
            var guidBytes = new byte[8];
            guidBytes[1] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            guidBytes[3] = packet.ReadBit();

            packet.ReadEnum<MovementFlag>("Movement flags", 30);

            guidBytes[5] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();

            packet.ReadBit("HasSplineData");

            guidBytes[4] = packet.ReadBit();

            packet.ReadEnum<MovementFlagExtra>("Movement flags extra", 12);

            var splineElevation = packet.ReadBit("SplineElevation"); // OR Swimming
            var onTransport = packet.ReadBit("OnTransport");

            var transportBytes = new byte[8];
            var hasInterpolatedMovement = false;
            var time3 = false;
            if (onTransport)
            {
                transportBytes = packet.StartBitStream(0, 6, 2, 5, 4, 1, 3, 7);
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

            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 4);

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

                packet.ReadXORByte(transportBytes, 3);
                packet.ReadXORByte(transportBytes, 6);

                if (time3)
                    packet.ReadInt32("Transport Time 3");

                packet.ReadXORByte(transportBytes, 7);
                packet.ReadXORByte(transportBytes, 5);
                packet.ReadXORByte(transportBytes, 2);
                packet.ReadXORByte(transportBytes, 1);
                packet.ReadXORByte(transportBytes, 0);
                packet.ReadXORByte(transportBytes, 4);

                packet.WriteGuid("Transport Guid", transportBytes);
            }

            if (swimming)
                packet.ReadSingle("Swim Pitch");

            packet.ReadXORByte(guidBytes, 5);

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

            packet.ReadXORByte(guidBytes, 0);
            packet.ReadXORByte(guidBytes, 3);
            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 2);

            packet.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.MSG_MOVE_SET_PITCH, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetPitch434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasMovementFlags = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[6] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[4] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[2] = packet.ReadBit();
            packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[3] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 3, 7, 1, 6, 0, 5, 2, 4);

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 6);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 2);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 7);
                tpos.X = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_SET_FACING, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleMovementSetFacing422(Packet packet)
        {
            var info = new MovementInfo();
            var guidBytes = new byte[8];
            var transportGuidBytes = new byte[8];

            info.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            guidBytes[4] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();

            info.HasSplineData = packet.ReadBit("HasSplineData");

            guidBytes[3] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();

            packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            guidBytes[0] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();

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

            packet.WriteGuid("Guid", guidBytes);
            packet.WriteGuid("Transport Guid", transportGuidBytes);
        }

        [Parser(Opcode.MSG_MOVE_SET_FACING, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetFacing434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[6] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[4] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[0] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasMovementFlags2 = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasMovementFlags = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadBit();
            guid[1] = packet.ReadBit();

            if (hasTrans)
            {
                hasTransTime3 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 6, 7, 2, 0, 4, 1, 5, 3);

            if (hasTrans)
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 0);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 4);
                tpos.Z = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 3);
                packet.ReadSByte("Transport Seat");
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 7);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_TELEPORT, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveTeleport422(Packet packet)
        {
            var onTransport = packet.ReadBit("OnTransport");

            var guid = packet.StartBitStream(0, 2, 6, 7, 4, 5, 3, 1);

            var unk2 = packet.ReadBit("Unk Bit Boolean 2");

            packet.ReadVector3("Destination Position");

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);

            if (onTransport)
                packet.ReadGuid("Transport Guid");

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);

            packet.ReadInt32("Unk 1");

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);

            if (unk2)
                packet.ReadByte("Unk 2");

            packet.ReadSingle("Arrive Orientation");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_TELEPORT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveTeleport434(Packet packet)
        {
            var guid = new byte[8];
            var transGuid = new byte[8];
            var pos = new Vector4();

            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bit48 = packet.ReadBit();
            if (bit48)
            {
                packet.ReadBit("Unk bit 50");
                packet.ReadBit("Unk bit 51");
            }

            var onTransport = packet.ReadBit("On transport");
            guid[1] = packet.ReadBit();
            if (onTransport)
                transGuid = packet.StartBitStream(1, 3, 2, 5, 0, 7, 6, 4);

            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            if (onTransport)
            {
                packet.ParseBitStream(transGuid, 5, 6, 1, 7, 0, 2, 4, 3);
                packet.WriteGuid("Transport Guid", transGuid);
            }

            packet.ReadUInt32("Time");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 4);
            pos.O = packet.ReadSingle();
            packet.ReadXORByte(guid, 7);
            pos.Z = packet.ReadSingle();
            if (bit48)
                packet.ReadUInt32("Unk int");

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            pos.Y = packet.ReadSingle();

            packet.WriteLine("Destination: {0}", pos);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_STOP, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleMoveStop422(Packet packet)
        {
            var info = new MovementInfo();
            var guidBytes = new byte[8];
            var transportGuidBytes = new byte[8];

            guidBytes[2] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();

            info.HasSplineData = packet.ReadBit("HasSplineData");

            info.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            guidBytes[4] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            guidBytes[3] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();

            packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            guidBytes[1] = packet.ReadBit();

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

            var splineElevation = packet.ReadBit("HaveSplineElevation");

            info.Orientation = packet.ReadSingle("Orientation");

            packet.ReadUInt32("Timestamp");

            info.Position = packet.ReadVector3("Position");

            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 3);

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

            packet.ReadXORByte(guidBytes, 5);
            packet.ReadXORByte(guidBytes, 7);

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

            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 0);

            if (splineElevation)
                packet.ReadSingle("Spline Elevation");

            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 4);

            packet.WriteGuid("Guid", guidBytes);
            packet.WriteGuid("Transport Guid", transportGuidBytes);
        }

        [Parser(Opcode.MSG_MOVE_STOP, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStop434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            packet.ReadBit("Has Spline");
            var hasO = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");

            if (hasTrans)
            {
                transportGuid[7] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 6, 3, 0, 4, 2, 1, 5, 7);

            if (hasTrans)
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadUInt32("Transport Time");
                packet.ReadSByte("Transport Seat");
                tpos.Z = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 0);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 3);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.X = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 6);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
            }

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_PLAYER_MOVE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandlePlayerMove422(Packet packet)
        {
            var info = new MovementInfo();
            var guidBytes = new byte[8];
            var transportGuidBytes = new byte[8];

            var splineElevation = packet.ReadBit("HaveSplineElevation");
            var haveTransportData = packet.ReadBit("HaveTransportData");
            guidBytes[5] = packet.ReadBit();

            var haveTransportTime2 = false;
            var haveTransportTime3 = false;
            if (haveTransportData)
            {
                transportGuidBytes[2] = packet.ReadBit();
                transportGuidBytes[4] = packet.ReadBit();
                transportGuidBytes[1] = packet.ReadBit();
                transportGuidBytes[3] = packet.ReadBit();
                transportGuidBytes[0] = packet.ReadBit();
                haveTransportTime2 = packet.ReadBit("HaveTransportTime2");
                transportGuidBytes[7] = packet.ReadBit();
                haveTransportTime3 = packet.ReadBit("HaveTransportTime3");
                transportGuidBytes[6] = packet.ReadBit();
                transportGuidBytes[5] = packet.ReadBit();
            }

            guidBytes[7] = packet.ReadBit();
            guidBytes[3] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();
            info.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30);
            var havePitch = packet.ReadBit("HavePitch");
            guidBytes[2] = packet.ReadBit();
            packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);
            guidBytes[6] = packet.ReadBit();

            var haveFallData = packet.ReadBit("HaveFallData");
            var haveFallDirection = false;
            if (haveFallData)
                haveFallDirection = packet.ReadBit("HaveFallDirection");

            info.HasSplineData = packet.ReadBit("HasSplineData");

            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 0);

            info.Orientation = packet.ReadSingle("Orientation");

            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 7);

            if (splineElevation)
                packet.ReadSingle("Spline Elevation");

            if (haveTransportData)
            {
                packet.ReadXORByte(transportGuidBytes, 4);
                packet.ReadXORByte(transportGuidBytes, 2);
                packet.ReadSingle("Transport Orientation");
                packet.ReadUInt32("Transport Time");
                packet.ReadByte("Transport Seat");
                packet.ReadXORByte(transportGuidBytes, 3);
                packet.ReadVector3("Transport Position");
                packet.ReadXORByte(transportGuidBytes, 1);

                if (haveTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                if (haveTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuidBytes, 5);
                packet.ReadXORByte(transportGuidBytes, 0);
                packet.ReadXORByte(transportGuidBytes, 6);
                packet.ReadXORByte(transportGuidBytes, 7);
            }

            packet.ReadXORByte(guidBytes, 2);
            packet.ReadUInt32("Timestamp");
            packet.ReadXORByte(guidBytes, 1);

            if (havePitch)
                packet.ReadSingle("Pitch");

            info.Position = packet.ReadVector3("Position");
            packet.ReadXORByte(guidBytes, 5);
            packet.ReadXORByte(guidBytes, 3);

            if (haveFallData)
            {
                packet.ReadSingle("Fall Horizontal Speed");

                if (haveFallDirection)
                {
                    packet.ReadSingle("Fall Cos Angle");
                    packet.ReadSingle("Fall Sin Angle");
                }

                packet.ReadSingle("Fall Vertical Speed");
                packet.ReadUInt32("Fall Time");
            }

            packet.WriteGuid("Guid", guidBytes);
            packet.WriteGuid("Transport Guid", transportGuidBytes);
        }

        [Parser(Opcode.SMSG_PLAYER_MOVE, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandlePlayerMove(Packet packet)
        {
        }

        [Parser(Opcode.MSG_MOVE_START_FORWARD, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_START_BACKWARD, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_STOP, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.MSG_MOVE_START_STRAFE_LEFT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_START_STRAFE_RIGHT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_STOP_STRAFE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_START_ASCEND, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_START_DESCEND, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_STOP_ASCEND, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_JUMP, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_START_TURN_LEFT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_START_TURN_RIGHT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_STOP_TURN, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_START_PITCH_UP, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_START_PITCH_DOWN, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_STOP_PITCH, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_SET_RUN_MODE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_SET_WALK_MODE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_TELEPORT, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.MSG_MOVE_SET_FACING, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.MSG_MOVE_SET_PITCH, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.MSG_MOVE_TOGGLE_COLLISION_CHEAT)]
        [Parser(Opcode.MSG_MOVE_GRAVITY_CHNG)]
        [Parser(Opcode.MSG_MOVE_ROOT)]
        [Parser(Opcode.MSG_MOVE_UNROOT)]
        [Parser(Opcode.MSG_MOVE_START_SWIM, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_STOP_SWIM, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_START_SWIM_CHEAT)]
        [Parser(Opcode.MSG_MOVE_STOP_SWIM_CHEAT)]
        [Parser(Opcode.MSG_MOVE_HEARTBEAT, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.MSG_MOVE_FALL_LAND, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.MSG_MOVE_UPDATE_CAN_FLY)]
        [Parser(Opcode.MSG_MOVE_UPDATE_CAN_TRANSITION_BETWEEN_SWIM_AND_FLY)]
        [Parser(Opcode.MSG_MOVE_KNOCK_BACK)]
        [Parser(Opcode.MSG_MOVE_HOVER)]
        [Parser(Opcode.MSG_MOVE_FEATHER_FALL)]
        [Parser(Opcode.MSG_MOVE_WATER_WALK)]
        [Parser(Opcode.CMSG_MOVE_FALL_RESET, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_MOVE_SET_FLY)]
        [Parser(Opcode.CMSG_MOVE_CHNG_TRANSPORT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_MOVE_NOT_ACTIVE_MOVER, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_DISMISS_CONTROLLED_VEHICLE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
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

        [Parser(Opcode.CMSG_MOVE_CHNG_TRANSPORT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveChngTransport434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            packet.ReadBit("Has Spline");
            var hasO = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[1] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasMovementFlags = !packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[3] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 7, 5, 1, 2, 6, 4, 0, 3);

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.Y = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 6);
                tpos.Z = packet.ReadSingle();
                tpos.X = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 4);
                tpos.O = packet.ReadSingle();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_SPLINE_DONE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSplineDone(Packet packet)
        {
            var guid = packet.ReadPackedGuid("Guid");
            ReadMovementInfo(ref packet, guid);
            packet.ReadInt32("Movement Counter"); // Possibly
        }

        [Parser(Opcode.MSG_MOVE_START_ASCEND, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStartAscend434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[2] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasFallData = packet.ReadBit("Has fall data");
            packet.ReadBit("Has Spline");
            guid[3] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasO = !packet.ReadBit();
            guid[4] = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasTrans)
            {
                transportGuid[0] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 6, 3, 1, 4, 2, 0, 5, 7);

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 2);
                tpos.O = packet.ReadSingle();
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.Z = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 6);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 1);
                tpos.X = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_DESCEND, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStartDescend434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            guid[0] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasFallData = packet.ReadBit("Has fall data");
            guid[4] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            packet.ReadBit("Has Spline");
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[0] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 2, 7, 6, 0, 1, 5, 4, 3);

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.Z = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 6);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 1);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 4);
                tpos.Y = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 2);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP_ASCEND, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStopAscend434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[7] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            packet.ReadBit("Has Spline");
            packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasTrans = packet.ReadBit("Has transport");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[5] = packet.ReadBit();

            if (hasTrans)
            {
                hasTransTime2 = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid = packet.StartBitStream(1, 3, 2, 5, 7, 4, 6, 0);
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 7, 5, 4, 3, 2, 1, 0, 6);

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 5);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 6);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 0);
                tpos.X = packet.ReadSingle();
                tpos.Z = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_PITCH_DOWN, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStartPitchDown434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var hasMovementFlags = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            var hasO = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[2] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[1] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 3, 7, 0, 5, 2, 6, 4, 1);

            if (hasTrans)
            {
                var tpos = new Vector4();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 3);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 1);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadUInt32("Transport Time");
                tpos.Z = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 7);
                tpos.O = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_PITCH_UP, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStartPitchUp434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            guid[4] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasFallData = packet.ReadBit("Has fall data");
            guid[0] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasO = !packet.ReadBit();
            guid[7] = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasTrans)
            {
                transportGuid[7] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 0, 3, 4, 6, 7, 1, 5, 2);

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadUInt32("Transport Time");
                tpos.Z = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 5);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP_PITCH, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStopPitch434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            packet.ReadBit("Has Spline");
            var hasO = !packet.ReadBit();
            packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasTrans = packet.ReadBit("Has transport");
            var hasMovementFlags = !packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[7] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 1, 7, 0, 6, 4, 3, 5, 2);

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 2);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.X = packet.ReadSingle();
                tpos.O = packet.ReadSingle();

                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 1);
                tpos.Z = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadUInt32("Transport Time");
                tpos.Y = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 7);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_SWIM, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStartSwim434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            guid[3] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[0] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[5] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[2] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[2] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 0, 2, 1, 5, 4, 6, 3, 7);

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasTrans)
            {
                var tpos = new Vector4();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 2);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadUInt32("Transport Time");
                tpos.X = packet.ReadSingle();
                tpos.O = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 4);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadSByte("Transport Seat");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP_SWIM, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStopSwim434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            packet.ReadBit("Has Spline");
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasMovementFlags = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            var hasO = !packet.ReadBit();
            packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");

            if (hasTrans)
            {
                hasTransTime3 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 0, 4, 3, 6, 7, 1, 5, 2);

            if (hasTrans)
            {
                var tpos = new Vector4();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 4);
                packet.ReadUInt32("Transport Time");
                tpos.Z = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 2);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 5);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                tpos.O = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_RUN_SPEED, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleSplineMovementSetRunSpeed422(Packet packet)
        {
            var guid = packet.StartBitStream(7, 2, 1, 3, 5, 6, 4, 0);
            packet.ParseBitStream(guid, 6, 7, 4, 3, 2, 5, 0, 1);
            packet.ReadSingle("Speed");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_RUN_SPEED, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMovementSetRunSpeed430(Packet packet)
        {
            var guid = packet.StartBitStream(2, 6, 4, 1, 3, 0, 7, 5);
            packet.ParseBitStream(guid, 2, 4, 3);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 0, 6, 5, 1, 7);
            packet.WriteGuid("Guid", guid);
        }


        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_RUN_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetRunSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(4, 0, 5, 7, 6, 3, 1, 2);
            packet.ParseBitStream(guid, 0, 7, 6, 5, 3, 4);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 2, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_FALL_LAND, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveFallLand434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            var hasTrans = packet.ReadBit("Has transport");
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasO = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[4] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[5] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[0] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 1, 7, 4, 3, 6, 0, 2, 5);

            if (hasTrans)
            {
                var tpos = new Vector4();
                packet.ReadUInt32("Transport Time");
                tpos.Z = packet.ReadSingle();
                tpos.Y = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 1);
                packet.ReadSByte("Transport Seat");

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 4);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 2);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_JUMP, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveJump434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[5] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[3] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[7] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            packet.ReadBit("Has Spline");
            var hasO = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            packet.ReadBit();
            guid[2] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 6, 5, 4, 0, 2, 3, 7, 1);

            if (hasTrans)
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 7);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 0);
                tpos.O = packet.ReadSingle();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadUInt32("Transport Time");

                packet.ReadXORByte(transportGuid, 6);
                tpos.Z = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 5);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_STRAFE_LEFT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStartStrafeLeft434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[5] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[6] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[1] = packet.ReadBit();
            packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasO = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            var hasMovementFlags = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasTrans)
            {
                hasTransTime2 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 2, 6, 3, 1, 0, 7, 4, 5);

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasTrans)
            {
                var tpos = new Vector4();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 2);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 3);
                tpos.Z = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 7);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 1);
                tpos.O = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 4);
                tpos.X = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_STRAFE_RIGHT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStartStrafeRight434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[1] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasMovementFlags = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[0] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[7] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 7, 5, 3, 1, 2, 4, 6, 0);

            if (hasTrans)
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 6);
                tpos.Y = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadSByte("Transport Seat");
                tpos.X = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 7);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP_STRAFE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStopStrafe434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[2] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[7] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[3] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            var hasMovementFlags = !packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[0] = packet.ReadBit();
            packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[7] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 2, 7, 3, 4, 5, 6, 1, 0);

            if (hasTrans)
            {
                var tpos = new Vector4();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 6);
                tpos.Z = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadUInt32("Transport Time");
                tpos.O = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                tpos.Y = packet.ReadSingle();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 7);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_BACKWARD, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStartBackward434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var hasTrans = packet.ReadBit("Has transport");
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[7] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasO = !packet.ReadBit();
            packet.ReadBit("Has Spline");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");

            if (hasTrans)
            {
                transportGuid[3] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 6, 7, 4, 1, 5, 0, 2, 3);

            if (hasTrans)
            {
                var tpos = new Vector4();
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 5);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                tpos.X = packet.ReadSingle();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 4);
                tpos.O = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 3);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_TURN_LEFT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStartTurnLeft434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            packet.ReadBit("Has Spline");
            var hasMovementFlags = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[6] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasFallData = packet.ReadBit("Has fall data");

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasTrans)
            {
                transportGuid[3] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 0, 4, 7, 5, 6, 3, 2, 1);

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasTrans)
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 0);
                tpos.X = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadSByte("Transport Seat");
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 3);
                tpos.Y = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 7);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_TURN_RIGHT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStartTurnRight434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[0] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[7] = packet.ReadBit();
            packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasFallData = packet.ReadBit("Has fall data");

            if (hasTrans)
            {
                transportGuid[1] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 5, 0, 7, 3, 2, 1, 4, 6);

            if (hasTrans)
            {
                var tpos = new Vector4();
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadSByte("Transport Seat");
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.Z = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 3);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_SET_RUN_SPEED, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleMovementSetRunSpeed422(Packet packet)
        {
            var guid = packet.StartBitStream(1, 0, 7, 5, 2, 4, 3, 6);
            packet.ParseBitStream(guid, 1);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 6, 2, 3, 7, 4, 0, 5);
            packet.ReadUInt32("Move Event");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_STOP_TURN, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStopTurn434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            var hasMovementFlags = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasTrans = packet.ReadBit("Has transport");
            guid[3] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[7] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[2] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 3, 2, 6, 4, 0, 7, 1, 5);

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            if (hasTrans)
            {
                var tpos = new Vector4();
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 3);
                tpos.O = packet.ReadSingle();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 4);
                tpos.Y = packet.ReadSingle();
                tpos.Z = packet.ReadSingle();

                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 6);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
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

        [Parser(Opcode.SMSG_FORCE_WALK_SPEED_CHANGE)]
        [Parser(Opcode.SMSG_FORCE_RUN_SPEED_CHANGE)]
        [Parser(Opcode.SMSG_FORCE_RUN_BACK_SPEED_CHANGE)]
        [Parser(Opcode.SMSG_FORCE_SWIM_SPEED_CHANGE)]
        [Parser(Opcode.SMSG_FORCE_SWIM_BACK_SPEED_CHANGE)]
        [Parser(Opcode.SMSG_FORCE_TURN_RATE_CHANGE)]
        [Parser(Opcode.SMSG_FORCE_FLIGHT_SPEED_CHANGE)]
        [Parser(Opcode.SMSG_FORCE_FLIGHT_BACK_SPEED_CHANGE)]
        [Parser(Opcode.SMSG_FORCE_PITCH_RATE_CHANGE)]
        public static void HandleForceSpeedChange(Packet packet)
        {
            packet.ReadPackedGuid("Guid");
            packet.ReadUInt32("MoveEvent"); // Movement Counter?

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_FORCE_RUN_SPEED_CHANGE))
                packet.ReadByte("Unk Byte");

            packet.ReadSingle("New Speed");
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
            var guid = packet.ReadPackedGuid("Guid");
            packet.ReadInt32("Movement Counter");

            ReadMovementInfo(ref packet, guid);

            packet.ReadSingle("New Speed");
        }

        [Parser(Opcode.MSG_MOVE_SET_COLLISION_HGT)]
        [Parser(Opcode.SMSG_MOVE_SET_COLLISION_HGT)]
        [Parser(Opcode.CMSG_MOVE_SET_COLLISION_HGT_ACK)]
        public static void HandleCollisionMovements(Packet packet)
        {
            var guid = packet.ReadPackedGuid("Guid");

            if (packet.Opcode != Opcodes.GetOpcode(Opcode.MSG_MOVE_SET_COLLISION_HGT))
                packet.ReadInt32("Movement Counter");

            if (packet.Opcode != Opcodes.GetOpcode(Opcode.SMSG_MOVE_SET_COLLISION_HGT))
                ReadMovementInfo(ref packet, guid);

            packet.ReadSingle("Collision Height");
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        [Parser(Opcode.SMSG_MOUNTSPECIAL_ANIM)]
        public static void HandleSetActiveMover(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSetActiveMover430(Packet packet)
        {
            var guid = packet.StartBitStream(7, 2, 0, 4, 3, 5, 6, 1);
            packet.ParseBitStream(guid, 1, 3, 2, 6, 0, 5, 4, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSetActiveMover434(Packet packet)
        {
            var guid = packet.StartBitStream(7, 2, 1, 0, 4, 5, 6, 3);
            packet.ParseBitStream(guid, 3, 2, 4, 0, 5, 1, 6, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_FORCE_MOVE_ROOT)]
        [Parser(Opcode.SMSG_FORCE_MOVE_UNROOT)]
        [Parser(Opcode.SMSG_MOVE_WATER_WALK)]
        [Parser(Opcode.SMSG_MOVE_LAND_WALK)]
        [Parser(Opcode.SMSG_MOVE_SET_HOVER)]
        [Parser(Opcode.SMSG_MOVE_UNSET_HOVER)]
        [Parser(Opcode.SMSG_MOVE_SET_CAN_FLY)]
        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_FLY)]
        [Parser(Opcode.SMSG_MOVE_SET_CAN_TRANSITION_BETWEEN_SWIM_AND_FLY)]
        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_TRANSITION_BETWEEN_SWIM_AND_FLY)]
        [Parser(Opcode.SMSG_MOVE_FEATHER_FALL)]
        [Parser(Opcode.SMSG_MOVE_NORMAL_FALL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSetMovementMessages(Packet packet)
        {
            packet.ReadPackedGuid("Guid");
            packet.ReadInt32("Movement Counter");
        }

        [Parser(Opcode.CMSG_MOVE_WATER_WALK_ACK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_MOVE_FEATHER_FALL_ACK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_MOVE_HOVER_ACK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_MOVE_SET_CAN_FLY_ACK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_MOVE_SET_CAN_TRANSITION_BETWEEN_SWIM_AND_FLY_ACK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSpecialMoveAckMessages(Packet packet)
        {
            var guid = packet.ReadPackedGuid("Guid");
            packet.ReadInt32("Movement Counter");
            ReadMovementInfo(ref packet, guid);
            packet.ReadSingle("Unk float");
        }

        [Parser(Opcode.CMSG_MOVE_KNOCK_BACK_ACK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_FORCE_MOVE_UNROOT_ACK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_FORCE_MOVE_ROOT_ACK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSpecialMoveAckMessages2(Packet packet)
        {
            var guid = packet.ReadPackedGuid("Guid");
            packet.ReadInt32("Movement Counter");

            ReadMovementInfo(ref packet, guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_SET_PHASE_SHIFT, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandlePhaseShift(Packet packet)
        {
            CurrentPhaseMask = packet.ReadInt32("Phase Mask");

            packet.AddSniffData(StoreNameType.Phase, CurrentPhaseMask, "PHASEMASK");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_SET_PHASE_SHIFT, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_1_0_13914)]
        public static void HandlePhaseShift406(Packet packet)
        {
            packet.ReadGuid("GUID");
            var i = 0;
            int count = packet.ReadInt32("Count");
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

        [HasSniffData]
        [Parser(Opcode.SMSG_SET_PHASE_SHIFT, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandlePhaseShift422(Packet packet)
        {
            var guid = new byte[8];

            guid[6] = packet.ReadBit();//0
            guid[1] = packet.ReadBit();//1
            guid[7] = packet.ReadBit();//2
            guid[4] = packet.ReadBit();//3
            guid[2] = packet.ReadBit();//4
            guid[3] = packet.ReadBit();//5
            guid[0] = packet.ReadBit();//6
            guid[5] = packet.ReadBit();//7

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);

            var i = 0;
            var count = packet.ReadInt32();
            for (var j = 0; j < count / 2; ++j)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Map Swap 1", i, j);

            packet.ReadXORByte(guid, 3);

            packet.WriteLine("[" + i + "]" + " Mask: 0x" + packet.ReadUInt32().ToString("X2"));

            packet.ReadXORByte(guid, 2);

            var phaseMask = -1;
            count = packet.ReadInt32();
            for (var j = 0; j < count / 2; ++j)
                phaseMask = packet.ReadUInt16("Current Mask", i, j);

            packet.ReadXORByte(guid, 6);

            i++;
            count = packet.ReadInt32();
            for (var j = 0; j < count / 2; ++j)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Map Swap 1", i, j);

            packet.ReadXORByte(guid, 7);

            i++;
            count = packet.ReadInt32();
            for (var j = 0; j < count / 2; ++j)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Map Swap 3", i, j);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);

            if (phaseMask != -1)
            {
                CurrentPhaseMask = phaseMask;
                packet.AddSniffData(StoreNameType.Phase, phaseMask, "PHASEMASK 422");
            }
        }

        [Parser(Opcode.SMSG_SET_PHASE_SHIFT, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandlePhaseShift434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 1, 6, 4, 5, 0, 7);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);

            var count = packet.ReadUInt32() / 2;
            packet.WriteLine("WorldMapArea swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("WorldMapArea swap", i);

            packet.ReadXORByte(guid, 1);

            packet.ReadUInt32("UInt32");

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);

            count = packet.ReadUInt32() / 2;
            packet.WriteLine("Inactive Terrain swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Inactive Terrain swap", i);

            count = packet.ReadUInt32() / 2;
            packet.WriteLine("Phases count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("Phase id", i); // Phase.dbc

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);

            count = packet.ReadUInt32() / 2;
            packet.WriteLine("Active Terrain swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Active Terrain swap", i);

            packet.ReadXORByte(guid, 5);
            packet.WriteLine("GUID {0}", new Guid(BitConverter.ToUInt64(guid, 0)));
        }

        [Parser(Opcode.SMSG_SET_PHASE_SHIFT, ClientVersionBuild.V5_1_0_16309)]
        public static void HandlePhaseShift510(Packet packet)
        {
            var guid = packet.StartBitStream(6, 4, 7, 2, 0, 1, 3, 5);
            packet.ReadXORByte(guid, 4);

            var count = packet.ReadUInt32() / 2;
            packet.WriteLine("WorldMapArea swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("WorldMapArea swap", i);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);

            count = packet.ReadUInt32() / 2;
            packet.WriteLine("Phases count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("Phase id", i); // Phase.dbc

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);

            count = packet.ReadUInt32() / 2;
            packet.WriteLine("Active Terrain swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Active Terrain swap", i);

            packet.ReadUInt32("UInt32");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);

            count = packet.ReadUInt32() / 2;
            packet.WriteLine("Inactive Terrain swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Inactive Terrain swap", i);

            packet.WriteLine("GUID {0}", new Guid(BitConverter.ToUInt64(guid, 0)));
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleTransferPending(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

            if (!packet.CanRead())
                return;

            packet.ReadInt32("Transport Entry");
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Transport Map ID");
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTransferPending430(Packet packet)
        {
            var bit1 = packet.ReadBit();
            var hasTransport = packet.ReadBit();

            if (bit1)
                packet.ReadUInt32("Unk int");

            if (hasTransport)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Transport Map ID");
                packet.ReadInt32("Transport Entry");
            }

            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTransferPending434(Packet packet)
        {
            var bit1 = packet.ReadBit();
            var hasTransport = packet.ReadBit();
            if (hasTransport)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Transport Map ID");
                packet.ReadInt32("Transport Entry");
            }

            if (bit1)
                packet.ReadUInt32("Unk int");

            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
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

        [Parser(Opcode.SMSG_MOVE_KNOCK_BACK, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
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
        public static void HandleMoveTimeSkippedMsg(Packet packet)
        {
            packet.ReadPackedGuid("Guid");
            packet.ReadInt32("Time");
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveTimeSkipped(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadInt32("Time");
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveTimeSkipped434(Packet packet)
        {
            packet.ReadUInt32("Time");
            var guid = packet.StartBitStream(5, 1, 3, 7, 6, 0, 4, 2);
            packet.ParseBitStream(guid, 7, 1, 2, 4, 3, 6, 0, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_ROOT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_UNROOT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_GRAVITY_ENABLE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_GRAVITY_DISABLE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_FEATHER_FALL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_NORMAL_FALL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_HOVER, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_UNSET_HOVER, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_WATER_WALK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_LAND_WALK)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_START_SWIM, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_STOP_SWIM, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_RUN_MODE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_WALK_MODE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_FLYING, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_UNSET_FLYING, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMovementMessages(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_WALK_SPEED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_RUN_SPEED, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_SWIM_SPEED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_FLIGHT_SPEED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_RUN_BACK_SPEED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_SWIM_BACK_SPEED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_FLIGHT_BACK_SPEED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_TURN_RATE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_PITCH_RATE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMovementSetSpeed(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadSingle("Amount");
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_FLIGHT_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetFlightSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(7, 4, 0, 1, 3, 6, 5, 2);
            packet.ParseBitStream(guid, 0, 5, 4, 7, 3, 2, 1, 6);
            packet.ReadSingle("Speed");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_SWIM_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetSwimSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(4, 2, 5, 0, 7, 6, 3, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_WALK_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetWalkSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 6, 7, 3, 5, 1, 2, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_RUN_BACK_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetRunBackSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(1, 2, 6, 0, 3, 7, 5, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.WriteGuid("Guid", guid);
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
                        Handler.Parse(newPacket, true);
                }
            }

            packet.WriteLine("}");
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_MOVE_KNOCK_BACK, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleMoveKnockBack422(Packet packet)
        {
            var guid = packet.StartBitStream(5, 2, 6, 3, 1, 4, 0, 7);

            packet.ReadXORByte(guid, 0);

            packet.ReadSingle("Jump Velocity");
            packet.ReadUInt32("Fall time");
            packet.ReadSingle("Fall Start Velocity");

            packet.ReadXORByte(guid, 6);

            packet.ReadSingle("Jump Cos");
            packet.ReadSingle("Jump Sin");

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_START_FORWARD, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveStartForward434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[4] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasTrans)
            {
                transportGuid = packet.StartBitStream(3, 4, 6, 2, 5, 0, 7, 1);
                hasTransTime3 = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 2, 4, 6, 1, 7, 3, 5, 0);

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
            }

            if (hasTrans)
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 3);
                tpos.Y = packet.ReadSingle();
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 7);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadSByte("Transport Seat");
                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_PLAYER_MOVE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlePlayerMove434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasFallDirection = false;
            var pos = new Vector4();

            var hasFallData = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            packet.ReadBit("Has spline data");
            var hasTime = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            if (hasMovementFlagsExtra)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            guid[7] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            packet.ReadBit();
            guid[4] = packet.ReadBit();
            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            guid[5] = packet.ReadBit();
            var hasTransport = packet.ReadBit();
            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement flags", 30);

            var hasTransportTime3 = false;
            var hasTransportTime2 = false;
            if (hasTransport)
            {
                transportGuid[3] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
            }

            var hasPitch = !packet.ReadBit();
            packet.ReadXORByte(guid, 5);
            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.ReadXORByte(guid, 7);
            pos.Y = packet.ReadSingle();
            packet.ReadXORByte(guid, 3);
            if (hasTransport)
            {
                var tpos = new Vector4();
                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 6);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 5);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 0);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 3);
                tpos.Y = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            packet.ReadXORByte(guid, 4);
            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 6);
            pos.Z = packet.ReadSingle();
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.ReadXORByte(guid, 2);
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.ReadXORByte(guid, 0);
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.ReadXORByte(guid, 1);
            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_MOVE_SET_COLLISION_HEIGHT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSetCollisionHeight434(Packet packet)
        {
            packet.ReadBits("Unknown bits", 2);
            var guid = packet.StartBitStream(6, 1, 4, 7, 5, 2, 0, 3);

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadUInt32("Time");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadSingle("Collision height");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_SET_RUN_MODE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetRunMode434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasMovementFlags2 = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            var hasMovementFlags = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[5] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasO = !packet.ReadBit();
            guid[2] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[6] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 3, 6, 0, 7, 4, 1, 5, 2);

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasTrans)
            {
                var tpos = new Vector4();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 3);
                tpos.X = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 1);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 7);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadUInt32("Transport Time");

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 0);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 6);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_SET_WALK_MODE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetWalkMode434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[6] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[7] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[4] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");

            if (hasTrans)
            {
                transportGuid[2] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 5, 6, 7, 3, 0, 2, 1);

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadSByte("Transport Seat");
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 4);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.O = packet.ReadSingle();
                tpos.X = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 7);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_SET_CAN_FLY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetCanFly434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var hasTrans = packet.ReadBit("Has transport");
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[0] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            packet.ReadBit("Has Spline");
            var hasO = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");

            if (hasTrans)
            {
                transportGuid[3] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 2, 0, 4, 7, 5, 1, 3, 6);

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadSByte("Transport Seat");
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 0);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 6);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_DISMISS_CONTROLLED_VEHICLE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleDismissControlledVehicle434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasFallData = packet.ReadBit("Has fall data");
            var hasTrans = packet.ReadBit("Has transport");
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[1] = packet.ReadBit();
            packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[3] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[5] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[5] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 6, 3, 1, 5, 2, 4, 7, 0);

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            if (hasTrans)
            {
                var tpos = new Vector4();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 7);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 6);
                packet.ReadUInt32("Transport Time");
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadSByte("Transport Seat");
                tpos.Y = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 2);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_SET_PLAY_HOVER_ANIM)]
        public static void HandlePlayHoverAnim(Packet packet)
        {
            var guid = new byte[8];
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            packet.ReadBit("unk");
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();

            packet.ParseBitStream(guid, 3, 2, 1, 7, 0, 5, 4, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_MOVE_SPLINE_DONE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSplineDone434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            packet.ReadInt32("Unk Int32 1"); // ##
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[6] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[2] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[4] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[3] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            packet.ReadBit("Has Spline");
            var hasMovementFlags2 = !packet.ReadBit();
            guid[7] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[1] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 7, 4, 5, 6, 0, 1, 2, 3);

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadUInt32("Fall time");
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 1);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 7);
                packet.ReadUInt32("Transport Time");

                tpos.Y = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                tpos.Z = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 4);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_SET_CAN_TRANSITION_BETWEEN_SWIM_AND_FLY_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetCanTransitionBetweenSwimAndFlyAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.X = packet.ReadSingle();
            guid[3] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            var hasMovementFlags = !packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasTime = !packet.ReadBit("Has timestamp");
            packet.ReadBit("Has Spline");
            packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[7] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[4] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 3, 2, 0, 4, 1, 5, 7, 6);

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.Y = packet.ReadSingle();
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadUInt32("Transport Time");
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 7);
                tpos.X = packet.ReadSingle();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 6);
                tpos.O = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_SWIM_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveUpdateSwimSpeed434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasFallDirection = false;
            var pos = new Vector4();

            var hasMovementFlags = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            packet.ReadBit("Has spline data");
            var hasTime = !packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement flags", 30);

            var hasFallData = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasTransport = packet.ReadBit();
            packet.ReadBit();
            guid[7] = packet.ReadBit();

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            if (hasTransport)
            {
                transportGuid[4] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
            }

            var hasMovementFlagsExtra = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            guid[1] = packet.ReadBit();

            if (hasTransport)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 2);
                tpos.X = packet.ReadSingle();

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 6);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 4);
                tpos.Y = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 7);
                tpos.Z = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 5);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            pos.X = packet.ReadSingle();

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical speed");
            }

            packet.ReadXORByte(guid, 7);

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            pos.Y = packet.ReadSingle();

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            pos.Z = packet.ReadSingle();
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 4);

            if (hasO)
                pos.O = packet.ReadSingle();

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_UPDATE_RUN_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveUpdateRunSpeed434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            packet.ReadSingle("Speed");
            guid[6] = packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            packet.ReadBit("Has spline data");
            var hasMovementFlags = !packet.ReadBit();
            var hasTime = !packet.ReadBit();
            guid[1] = packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            guid[3] = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement flags", 30);

            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasTransport = packet.ReadBit();

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            if (hasTransport)
            {
                transportGuid[5] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
            }

            var hasFallData = packet.ReadBit();

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            guid[4] = packet.ReadBit();
            packet.ReadBit();

            if (hasTransport)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 5);
                tpos.X = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadSByte("Transport Seat");

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 2);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.Z = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.ReadXORByte(guid, 6);

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);

            if (hasO)
                pos.O = packet.ReadSingle();

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);


            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_UPDATE_FLIGHT_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveUpdateFlightSpeed434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            packet.ReadSingle("Speed");
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            var hasMovementFlags = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement flags", 30);

            var hasFallData = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasTime = !packet.ReadBit();
            var hasTransport = packet.ReadBit();

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            if (hasTransport)
            {
                transportGuid[3] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
            }

            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            packet.ReadBit("Has spline data");

            if (hasMovementFlagsExtra)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            guid[0] = packet.ReadBit();

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);

            if (hasTransport)
            {
                var tpos = new Vector4();

                tpos.O = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 6);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 3);
                tpos.Y = packet.ReadSingle();

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 1);
                packet.ReadSByte("Transport Seat");
                tpos.X = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical speed");
                packet.ReadUInt32("Fall time");
            }

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);

            if (hasO)
                pos.O = packet.ReadSingle();

            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_COLLISION_HEIGHT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveUpdateCollisionHeight434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1");
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var hasTransport = packet.ReadBit();

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            if (hasTransport)
            {
                transportGuid[6] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
            }

            guid[3] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasTime = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            packet.ReadBit(); // not sure (offset 157);
            guid[2] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            guid[1] = packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            packet.ReadBit("Has spline data"); // not sure (offset 156)

            if (hasMovementFlagsExtra)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement flags", 30);

            packet.ReadXORByte(guid, 3);

            if (hasTransport)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 7);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 4);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 5);
                tpos.O = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 0);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadSByte("Transport Seat");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.ReadXORByte(guid, 6);

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal speed");
                }

                packet.ReadSingle("Vertical speed");
                packet.ReadUInt32("Fall time");
            }

            packet.ReadXORByte(guid, 7);

            if (hasO)
                pos.O = packet.ReadSingle();

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_UPDATE_TELEPORT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveUpdateTeleport434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            var hasO = !packet.ReadBit();
            packet.ReadBit("Has spline data");
            var hasMovementFlags = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasTransport = packet.ReadBit();
            guid[5] = packet.ReadBit();

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            if (hasTransport)
            {
                transportGuid[1] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
            }

            packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            var hasTime = !packet.ReadBit();

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            var hasSplineElevation = !packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement flags", 30);

            guid[1] = packet.ReadBit();
            packet.ReadXORByte(guid, 7);

            if (hasTransport)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 4);
                tpos.O = packet.ReadSingle();

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 1);

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadSByte("Transport Seat");
                packet.ReadUInt32("Transport Time");
                tpos.Y = packet.ReadSingle();
                tpos.X = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            packet.ReadXORByte(guid, 6);

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasO)
                pos.O = packet.ReadSingle();

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical speed");
            }

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_SWIM_BACK_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetSwimBackSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 1, 3, 6, 4, 5, 7, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_FLIGHT_BACK_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetFlightBackSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 1, 6, 5, 0, 3, 4, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_TURN_RATE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetTurnRate434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 4, 6, 1, 3, 5, 7, 0);
            packet.ReadSingle("Rate");
            packet.ParseBitStream(guid, 1, 5, 3, 2, 7, 4, 6, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_PITCH_RATE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetPitchRate434(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 6, 1, 0, 4, 7, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadSingle("Rate");
            packet.ReadXORByte(guid, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_ROOT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveRoot434(Packet packet)
        {
            var guid = packet.StartBitStream(5, 4, 6, 1, 3, 7, 2, 0);
            packet.ParseBitStream(guid, 2, 1, 7, 3, 5, 0, 6, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_UNROOT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveUnroot434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 1, 6, 5, 3, 2, 7, 4);
            packet.ParseBitStream(guid, 6, 3, 1, 5, 2, 0, 7, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_GRAVITY_ENABLE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveGravityEnable434(Packet packet)
        {
            var guid = packet.StartBitStream(5, 4, 7, 1, 3, 6, 2, 0);
            packet.ParseBitStream(guid, 7, 3, 4, 2, 1, 6, 0, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_GRAVITY_DISABLE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveGravityDisable434(Packet packet)
        {
            var guid = packet.StartBitStream(7, 3, 4, 2, 5, 1, 0, 6);
            packet.ParseBitStream(guid, 7, 1, 3, 4, 6, 2, 5, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_COLLISION_ENABLE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveCollisionEnable434(Packet packet)
        {
            var guid = packet.StartBitStream(3, 4, 7, 6, 1, 0, 2, 5);
            packet.ParseBitStream(guid, 1, 3, 7, 2, 0, 6, 4, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_COLLISION_DISABLE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveCollisionDisable434(Packet packet)
        {
            var guid = packet.StartBitStream(3, 7, 1, 0, 4, 2, 6, 5);
            packet.ParseBitStream(guid, 3, 5, 6, 7, 2, 1, 0, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_FEATHER_FALL, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveFeatherFall434(Packet packet)
        {
            var guid = packet.StartBitStream(3, 2, 7, 5, 4, 6, 1, 0);
            packet.ParseBitStream(guid, 1, 4, 7, 6, 2, 0, 5, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_NORMAL_FALL, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveNormalFall434(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 1, 0, 7, 6, 2, 4);
            packet.ParseBitStream(guid, 7, 6, 2, 0, 5, 4, 3, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_RUN_MODE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetRunMode434(Packet packet)
        {
            var guid = packet.StartBitStream(5, 6, 3, 7, 2, 0, 4, 1);
            packet.ParseBitStream(guid, 7, 0, 4, 6, 5, 1, 2, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_WALK_MODE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetWalkMode434(Packet packet)
        {
            var guid = packet.StartBitStream(7, 6, 5, 1, 3, 4, 2, 0);
            packet.ParseBitStream(guid, 4, 2, 1, 6, 5, 0, 7, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_HOVER, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineSetHover434(Packet packet)
        {
            var guid = packet.StartBitStream(3, 7, 0, 1, 4, 6, 2, 5);
            packet.ParseBitStream(guid, 2, 4, 3, 1, 7, 0, 5, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_UNSET_HOVER, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineUnsetHover434(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 4, 0, 3, 1, 5, 2);
            packet.ParseBitStream(guid, 4, 5, 3, 0, 2, 7, 6, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_WATER_WALK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveWaterWalk434(Packet packet)
        {
            var guid = packet.StartBitStream(6, 1, 4, 2, 3, 7, 5, 0);
            packet.ParseBitStream(guid, 0, 6, 3, 7, 4, 2, 5, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_START_SWIM, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveStartSwim434(Packet packet)
        {
            var guid = packet.StartBitStream(1, 6, 0, 7, 3, 5, 2, 4);
            packet.ParseBitStream(guid, 3, 7, 2, 5, 6, 4, 1, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_STOP_SWIM, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveStopSwim434(Packet packet)
        {
            var guid = packet.StartBitStream(4, 1, 5, 3, 0, 7, 2, 6);
            packet.ParseBitStream(guid, 6, 0, 7, 2, 3, 1, 5, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_FLYING, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveSetFlying434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 4, 1, 6, 7, 2, 3, 5);
            packet.ParseBitStream(guid, 7, 0, 5, 6, 4, 1, 3, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_UNSET_FLYING, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveUnsetFlying434(Packet packet)
        {
            var guid = packet.StartBitStream(5, 0, 4, 7, 2, 3, 1, 6);
            packet.ParseBitStream(guid, 7, 2, 3, 4, 5, 1, 6, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_RUN_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetRunSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(6, 1, 5, 2, 7, 0, 3, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadInt32("Unk Int32");
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_ROOT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveRoot434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 7, 6, 0, 5, 4, 1, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Unk Int32");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_UNROOT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveUnroot434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 1, 3, 7, 5, 2, 4, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadInt32("Unk Int32");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_RUN_SPEED_CHANGE_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveForceRunSpeedChangeAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            packet.ReadInt32("Unk Int32 1"); // ##
            pos.X = packet.ReadSingle();
            packet.ReadSingle("Speed");
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[0] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            var hasTrans = packet.ReadBit("Has transport");
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasMovementFlags2 = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[5] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[3] = packet.ReadBit();

            if (hasTrans)
            {
                hasTransTime3 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 6, 4, 1, 3, 5, 2, 7, 0);

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 1);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 5);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.X = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 7);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 2);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 4);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_SET_COLLISION_HEIGHT_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetCollisionHeightAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            packet.ReadSingle("Collision height");
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.Y = packet.ReadSingle();
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            packet.ReadBits("Unk bits", 2); // ##
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[5] = packet.ReadBit();
            packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[3] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[7] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[0] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[4] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 0, 3, 1, 5, 7, 6, 2, 4);

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 0);
                tpos.O = packet.ReadSingle();
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 1);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.Z = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                }
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_FLIGHT_SPEED_CHANGE_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveForceFlightSpeedChangeAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            packet.ReadInt32("Unk Int32 1"); // ##
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            packet.ReadSingle("Speed");
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[3] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[0] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasPitch = !packet.ReadBit("Has pitch");
            packet.ReadBit("Has Spline");
            var hasO = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 5, 6, 1, 7, 3, 0, 2, 4);

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadSByte("Transport Seat");

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 5);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 2);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 4);
                tpos.X = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                tpos.Y = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_SET_CAN_FLY_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetCanFlyAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[3] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasMovementFlags2 = !packet.ReadBit();
            packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            var hasPitch = !packet.ReadBit("Has pitch");

            if (hasTrans)
            {
                transportGuid[3] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 1, 0, 2, 3, 7, 6, 4, 5);

            if (hasTrans)
            {
                var tpos = new Vector4();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 6);
                packet.ReadUInt32("Transport Time");

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 7);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                tpos.O = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 4);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_SWIM_SPEED_CHANGE_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveForceSwimSpeedChangeAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            packet.ReadSingle("Speed");
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[4] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasTrans = packet.ReadBit("Has transport");
            var hasMovementFlags = !packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            packet.ReadBit("Has Spline");
            guid[0] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[1] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 2, 0, 6, 5, 1, 3, 4, 7);

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 0);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadSByte("Transport Seat");
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                tpos.Z = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 4);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_WALK_SPEED_CHANGE_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveForceWalkSpeedChangeAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            packet.ReadSingle("Speed");
            pos.X = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            guid[0] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasO = !packet.ReadBit();
            packet.ReadBit("Has Spline");
            packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasTrans = packet.ReadBit("Has transport");
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");

            if (hasTrans)
            {
                hasTransTime2 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 5, 6, 7, 2, 1, 3, 4, 0);

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadUInt32("Fall time");
            }

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 5);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadUInt32("Transport Time");

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 3);
                tpos.O = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 6);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_RUN_BACK_SPEED_CHANGE_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveForceRunBackSpeedChangeAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            packet.ReadSingle("Speed");
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasTrans = packet.ReadBit("Has transport");
            guid[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[6] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[3] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 0, 7, 2, 4, 3, 6, 5, 1);

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 5);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 2);
                tpos.Y = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.X = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadUInt32("Transport Time");

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 4);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_RUN_BACK_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveUpdateRunBackSpeed434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasFallDirection = false;
            var pos = new Vector4();

            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasTime = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            packet.ReadBit("Has spline data");
            guid[5] = packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            var hasO = !packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            var hasTransport = packet.ReadBit();

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            if (hasTransport)
            {
                transportGuid[5] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
            }

            guid[7] = packet.ReadBit();

            if (hasTransport)
            {
                var tpos = new Vector4();

                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 6);

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 3);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.Z = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 1);
                tpos.O = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            packet.ReadXORByte(guid, 4);

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical speed");
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.ReadXORByte(guid, 1);

            if (hasO)
                pos.O = packet.ReadSingle();

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.ReadXORByte(guid, 7);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            pos.Z = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_WALK_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveUpdateWalkSpeed434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasFallDirection = false;
            var pos = new Vector4();

            var hasPitch = !packet.ReadBit();
            var hasO = !packet.ReadBit();
            packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasTransport = packet.ReadBit();

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            if (hasTransport)
            {
                transportGuid[6] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
            }

            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            var hasTime = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[6] = packet.ReadBit();

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement flags", 30);

            packet.ReadBit("Has spline data");
            guid[4] = packet.ReadBit();

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasTransport)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 2);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadUInt32("Transport Time");

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.Z = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 5);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasFallData)
            {
                packet.ReadSingle("Vertical speed");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
            }

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            if (hasO)
                pos.O = packet.ReadSingle();

            pos.Y = packet.ReadSingle();
            packet.ReadXORByte(guid, 0);
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadSingle("Speed");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_FORCE_MOVE_ROOT_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleForceMoveRootAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.X = packet.ReadSingle();
            guid[3] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasTrans = packet.ReadBit("Has transport");
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[2] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasFallData = packet.ReadBit("Has fall data");
            packet.ReadBit("Has Spline");
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            packet.ReadBit();
            guid[5] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[6] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 5, 3, 1, 7, 4, 0, 6, 2);

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 4);
                tpos.O = packet.ReadSingle();
                tpos.Z = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 3);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                tpos.Y = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadUInt32("Fall time");
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_FORCE_MOVE_UNROOT_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleForceMoveUnrootAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            packet.ReadInt32("Unk Int32 1"); // ##
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            guid[7] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            packet.ReadBit("Has Spline");
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasMovementFlags = !packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            var hasMovementFlags2 = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[6] = packet.ReadBit();

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[0] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 7, 1, 0, 6, 2, 4, 5, 3);

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 6);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 3);
                tpos.Y = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 5);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadUInt32("Transport Time");

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 4);
                tpos.O = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 7);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_FALL_RESET, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveFallReset434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            guid[1] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasMovementFlags = !packet.ReadBit();
            packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[3] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[2] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");

            if (hasTrans)
            {
                transportGuid[0] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 4, 0, 1, 7, 5, 2, 3, 6);

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 5);
                tpos.Y = packet.ReadSingle();
                tpos.Z = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 6);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 0);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 1);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadUInt32("Fall time");
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_FEATHER_FALL_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveFeatherFallAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[1] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[2] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[5] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasO = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[7] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[6] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 6, 1, 7, 0, 5, 4, 3, 2);

            if (hasTrans)
            {
                var tpos = new Vector4();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.O = packet.ReadSingle();
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 2);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 6);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadUInt32("Transport Time");
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 3);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasO)
                pos.O = packet.ReadSingle();

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_GRAVITY_DISABLE_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveGravityDisableAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.X = packet.ReadSingle();
            guid[3] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasO = !packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasMovementFlags2 = !packet.ReadBit();
            packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[1] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[2] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 0, 2, 1, 3, 5, 7, 4, 6);

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 4);
                tpos.Y = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 0);
                tpos.O = packet.ReadSingle();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_GRAVITY_ENABLE_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveGravityEnableAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.Y = packet.ReadSingle();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasMovementFlags2 = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[3] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[1] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[0] = packet.ReadBit();

            if (hasTrans)
            {
                hasTransTime3 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 5, 4, 1, 7, 0, 2, 3, 6);

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 1);
                tpos.X = packet.ReadSingle();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 4);
                tpos.Z = packet.ReadSingle();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 5);
                packet.ReadUInt32("Transport Time");
                tpos.Y = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasTime)
                packet.ReadUInt32("Timestamp");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_HOVER_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveHoverAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            packet.ReadInt32("Unk Int32 1"); // ##
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            guid[4] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[2] = packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            packet.ReadBit("Has Spline");
            var hasMovementFlags = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasMovementFlags2 = !packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            var hasO = !packet.ReadBit();
            guid[3] = packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 1, 4, 7, 2, 5, 6, 3, 0);

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasO)
                pos.O = packet.ReadSingle();

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 1);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadSByte("Transport Seat");
                tpos.X = packet.ReadSingle();
                tpos.Z = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 0);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 6);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_KNOCK_BACK_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveKnockBackAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.X = packet.ReadSingle();
            guid[6] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[3] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[7] = packet.ReadBit();
            packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[0] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[5] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasTrans)
            {
                transportGuid[4] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ParseBitStream(guid, 4, 5, 1, 6, 0, 3, 2, 7);

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 7);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 1);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 2);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 5);
                tpos.X = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_NOT_ACTIVE_MOVER, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveNotActiveMover434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[6] = packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            guid[3] = packet.ReadBit();
            packet.ReadBit();
            var hasTime = !packet.ReadBit("Has timestamp");
            guid[0] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Has Spline");
            guid[2] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            var hasMovementFlags = !packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 1, 0, 4, 2, 7, 5, 6, 3);

            if (hasFallData)
            {
                packet.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadUInt32("Fall time");
            }

            if (hasTrans)
            {
                var tpos = new Vector4();

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 1);

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 5);
                tpos.Y = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadUInt32("Transport Time");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasO)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_WATER_WALK_ACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveWaterWalkAck434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.X = packet.ReadSingle();
            var hasTime = !packet.ReadBit("Has timestamp");
            var hasPitch = !packet.ReadBit("Has pitch");
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasTrans = packet.ReadBit("Has transport");
            guid[6] = packet.ReadBit();
            var hasFallData = packet.ReadBit("Has fall data");
            guid[4] = packet.ReadBit();
            packet.ReadBit();
            var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
            packet.ReadBit("Has Spline");

            if (hasTrans)
            {
                transportGuid[0] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.ParseBitStream(guid, 2, 7, 3, 5, 6, 0, 4, 1);

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 0);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                tpos.Y = packet.ReadSingle();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadSByte("Transport Seat");
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 4);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadUInt32("Fall time");
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_MOVE_GRAVITY_DISABLE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveGravityDisable434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 1, 5, 7, 6, 4, 3, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadInt32("Unk Int32"); // ##
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_GRAVITY_ENABLE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveGravityEnable434(Packet packet)
        {
            var guid = packet.StartBitStream(1, 4, 7, 5, 2, 0, 3, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Unk Int32"); // ##
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_NORMAL_FALL, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveNormalFall434(Packet packet)
        {
            packet.ReadInt32("Unk Int32"); // ##
            var guid = packet.StartBitStream(3, 0, 1, 5, 7, 4, 6, 2);
            packet.ParseBitStream(guid, 2, 7, 1, 4, 5, 0, 3, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_ACTIVE_MOVER, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetActiveMover430(Packet packet)
        {
            var guid = packet.StartBitStream(6, 2, 7, 0, 3, 5, 4, 1);
            packet.ParseBitStream(guid, 3, 5, 6, 7, 2, 0, 1, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_ACTIVE_MOVER, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetActiveMover434(Packet packet)
        {
            var guid = packet.StartBitStream(5, 7, 3, 6, 0, 4, 1, 2);
            packet.ParseBitStream(guid, 6, 2, 3, 0, 5, 7, 1, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_COMPOUND_STATE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetCompoundState434(Packet packet)
        {
            var guid = packet.StartBitStream(5, 0, 4, 1, 7, 6, 2, 3);
            var count = packet.ReadBits("Count", 23);
            var unk1 = new byte[count];
            var unk2 = new byte[count];
            var hasPosition = new byte[count];
            var unk4 = new byte[count];

            for (int i = 0; i < count; ++i)
            {
                unk1[i] = packet.ReadBit("Unk bit 1", i); // 36
                unk2[i] = packet.ReadBit("Unk bit 2", i); // 8
                hasPosition[i] = packet.ReadBit("Has position", i); // 16
                unk4[i] = packet.ReadBit("Unk bit 4", i); // 44

                if (unk4[i] != 0)
                    packet.ReadBits("Unk bits", 2, i);
            }

            for (int i = 0; i < count; ++i)
            {

                if (unk4[i] != 0)
                    packet.ReadSingle("Unk Float 1", i);

                if (hasPosition[i] != 0)
                    packet.ReadVector4("Position", i);

                if (unk1[i] != 0)
                    packet.ReadInt32("Unk Int32 1", i);

                packet.ReadInt32("Unk Int32 2", i);

                if (unk2[i] != 0)
                    packet.ReadSingle("Unk Float 2", i);

                packet.ReadInt16("Unk Int16", i);
            }

            packet.ParseBitStream(guid, 2, 1, 4, 5, 6, 7, 0, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetFlightSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 5, 1, 6, 3, 2, 7, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadSingle("Speed");
            packet.ReadInt32("Unk Int32"); // ##
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_BACK_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetFlightBackSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(1, 2, 6, 4, 7, 3, 0, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Unk Int32"); // ##
            packet.ReadXORByte(guid, 6);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_RUN_BACK_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetRunBackSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 6, 2, 1, 3, 5, 4, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Unk Int32"); // ##
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_SWIM_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetSwimSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(5, 4, 7, 3, 2, 0, 1, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadInt32("Unk Int32"); // ##
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_SWIM_BACK_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetSwimBackSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(4, 2, 3, 6, 5, 1, 0, 7);
            packet.ReadInt32("Unk Int32"); // ##
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_WALK_SPEED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetWalkSpeed434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 4, 5, 2, 3, 1, 6, 7);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 2);
            packet.ReadInt32("Unk Int32"); // ##
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_TURN_RATE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetTurnRate434(Packet packet)
        {
            var guid = packet.StartBitStream(7, 2, 1, 0, 4, 5, 6, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadSingle("Rate");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadInt32("Unk Int32"); // ##
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_PITCH_RATE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetPitchRate434(Packet packet)
        {
            var guid = packet.StartBitStream(1, 2, 6, 7, 0, 3, 5, 4);
            packet.ReadSingle("Rate");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadInt32("Unk Int32"); // ##
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_FEATHER_FALL, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveSetFeatherFall434(Packet packet)
        {
            var guid = packet.StartBitStream(3, 2, 7, 5, 4, 6, 1, 0);
            packet.ParseBitStream(guid, 1, 4, 7, 6, 2, 0, 5, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_LAND_WALK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveSetLandWalk434(Packet packet)
        {
            var guid = packet.StartBitStream(5, 0, 4, 6, 7, 2, 3, 1);
            packet.ParseBitStream(guid, 5, 7, 3, 4, 1, 2, 0, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_NORMAL_FALL, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSplineMoveSetNormalFall434(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 1, 0, 7, 6, 2, 4);
            packet.ParseBitStream(guid, 7, 6, 2, 0, 5, 4, 3, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_KNOCK_BACK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveUpdateKnockBack434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasFallDirection = false;
            var pos = new Vector4();

            packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasTime = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Has spline data");
            var hasTransport = packet.ReadBit();

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            if (hasTransport)
            {
                transportGuid[7] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
            }

            guid[5] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            guid[6] = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement flags", 30);

            var hasFallData = packet.ReadBit();

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            var hasO = !packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12);

            if (hasO)
                pos.O = packet.ReadSingle();

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical speed");
            }

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.ReadXORByte(guid, 3);

            if (hasTransport)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 5);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 7);
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 6);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                tpos.Y = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 0);
                tpos.O = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 4);

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");

            pos.Z = packet.ReadSingle();

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);

            pos.Y = packet.ReadSingle();
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOUNTSPECIAL_ANIM)]
        public static void HandleMovementNull(Packet packet)
        {
        }
    }
}
