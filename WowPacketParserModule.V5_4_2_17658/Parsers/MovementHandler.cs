using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_MOVE_UPDATE)]
        public static void HandlePlayerMove(Packet packet)
        {
            var pos = new Vector4();
            var guid = new byte[8];
            var transportGUID = new byte[8];

            guid[7] = packet.ReadBit();

            var hasExtraMovementFlags = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var bit95 = packet.ReadBit();
            var isAlive = !packet.ReadBit();

            guid[6] = packet.ReadBit();

            var bit94 = packet.ReadBit();

            guid[0] = packet.ReadBit();

            var hasTransportData = packet.ReadBit();

            bool hasTransportTime2 = false;
            bool hasTransportTime3 = false;

            if (hasTransportData)
            {
                transportGUID[4] = packet.ReadBit();
                transportGUID[1] = packet.ReadBit();
                transportGUID[6] = packet.ReadBit();
                transportGUID[0] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGUID[2] = packet.ReadBit();
                transportGUID[7] = packet.ReadBit();
                transportGUID[3] = packet.ReadBit();
                transportGUID[5] = packet.ReadBit();
            }

            guid[4] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasTimeStamp = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);

            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            var bitAC = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            var hasFallData = packet.ReadBit();

            var hasFallDirection = false;
            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadInt32("Fall Time");
                packet.ReadSingle("Velocity Speed");
            }

            if (hasTransportData)
            {
                var transPos = new Vector4();

                packet.ReadXORByte(transportGUID, 1);

                transPos.Y = packet.ReadSingle();

                if (hasTransportTime2)
                    packet.ReadInt32("Transport Time 2");

                packet.ReadXORByte(transportGUID, 5);

                transPos.X = packet.ReadSingle();
                packet.ReadByte("Seat");
                packet.ReadInt32("Transport Time");

                packet.ReadXORByte(transportGUID, 3);
                packet.ReadXORByte(transportGUID, 6);

                transPos.O = packet.ReadSingle();
                transPos.Z = packet.ReadSingle();

                if (hasTransportTime3)
                    packet.ReadInt32("Transport Time 3");

                packet.ReadXORByte(transportGUID, 7);
                packet.ReadXORByte(transportGUID, 4);
                packet.ReadXORByte(transportGUID, 2);
                packet.ReadXORByte(transportGUID, 0);

                packet.WriteGuid("Transport Guid", transportGUID);
                packet.AddValue("Transport Position", transPos);
            }

            packet.ReadXORByte(guid, 3);

            pos.Y = packet.ReadSingle();

            if (hasPitch)
                packet.ReadSingle("Pitch");

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntEA", i);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);

            if (hasTimeStamp)
                packet.ReadInt32("Timestamp");

            pos.Z = packet.ReadSingle();

            if (hasSplineElevation)
                packet.ReadSingle("Spline Elevation");

            packet.ReadXORByte(guid, 1);

            if (isAlive)
                packet.ReadInt32("time(isAlive)");

            pos.X = packet.ReadSingle();

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleMonsterMove(Packet packet)
        {
            var pos = new Vector3();

            var guid2 = new byte[8];
            var factingTargetGUID = new byte[8];
            var ownerGUID = new byte[8];

            ownerGUID[4] = packet.ReadBit();

            var bit40 = !packet.ReadBit();
            var hasTime = !packet.ReadBit();

            ownerGUID[3] = packet.ReadBit();

            var bit30 = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            var splineCount = (int)packet.ReadBits(20);
            var bit98 = packet.ReadBit();
            var splineType = (int)packet.ReadBits(3);

            if (splineType == 3)
                packet.StartBitStream(factingTargetGUID, 0, 7, 3, 4, 5, 6, 1, 2);

            var bit55 = !packet.ReadBit();
            var bit60 = !packet.ReadBit();
            var bit54 = !packet.ReadBit();
            var bit34 = !packet.ReadBit();

            ownerGUID[6] = packet.ReadBit();
            ownerGUID[1] = packet.ReadBit();
            ownerGUID[0] = packet.ReadBit();

            packet.StartBitStream(guid2, 1, 3, 4, 5, 6, 0, 7, 2);

            ownerGUID[5] = packet.ReadBit();
            var bit20 = packet.ReadBit();
            ownerGUID[7] = packet.ReadBit();
            var bit2D = !packet.ReadBit();

            var bits84 = 0u;
            if (bit98)
            {
                packet.ReadBits("bits74", 2);
                bits84 = packet.ReadBits(22);
            }

            var hasFlags = !packet.ReadBit();
            ownerGUID[2] = packet.ReadBit();
            var bits64 = (int)packet.ReadBits(22);
            var bit0 = !packet.ReadBit();
            packet.ReadSingle("Float14");

            if (splineType == 3)
            {
                packet.ParseBitStream(factingTargetGUID, 1, 6, 4, 3, 5, 0, 2, 7);
                packet.WriteGuid("Facting Target GUID", factingTargetGUID);
            }

            packet.ReadXORByte(ownerGUID, 5);
            if (bit54)
                packet.ReadByte("Byte54");
            packet.ReadXORByte(ownerGUID, 4);
            if (bit98)
            {
                packet.ReadSingle("Float88");
                for (var i = 0; i < bits84; ++i)
                {
                    packet.ReadInt16("short74+2", i);
                    packet.ReadInt16("short74+0", i);
                }

                packet.ReadInt16("Int8C");
                packet.ReadInt16("Int94");
                packet.ReadSingle("Float90");
            }

            if (hasFlags)
                packet.ReadInt32E<SplineFlag434>("Spline Flags");

            if (bit40)
                packet.ReadInt32("Int40");

            if (bit2D)
                packet.ReadByte("Byte2D");

            Vector3 endpos = new Vector3();
            for (var i = 0; i < splineCount; ++i)
            {
                var spot = new Vector3
                {
                    Y = packet.ReadSingle(),
                    X = packet.ReadSingle(),
                    Z = packet.ReadSingle()
                };
                // client always taking first point
                if (i == 0)
                {
                    endpos = spot;
                }

                packet.AddValue("Spline Waypoint", spot, i);
            }

            packet.ParseBitStream(guid2, 6, 7, 2, 5, 3, 4, 1, 0);

            if (bit55)
                packet.ReadByte("Byte55");

            packet.ReadInt32("Move Ticks");

            packet.ReadXORByte(ownerGUID, 0);

            if (splineType == 4)
                packet.ReadSingle("Facing Angle");

            pos.Y = packet.ReadSingle();

            if (bit0)
                packet.ReadSingle("Float3C");

            packet.ReadXORByte(ownerGUID, 7);
            packet.ReadXORByte(ownerGUID, 1);

            var waypoints = new Vector3[bits64];
            for (var i = 0; i < bits64; ++i)
            {
                var vec = packet.ReadPackedVector3();
                waypoints[i].X = vec.X;
                waypoints[i].Y = vec.Y;
                waypoints[i].Z = vec.Z;
            }

            if (splineType == 2)
            {
                packet.ReadSingle("FloatA8");
                packet.ReadSingle("FloatAC");
                packet.ReadSingle("FloatB0");
            }

            if (hasTime)
                packet.ReadInt32("Move Time in ms");

            if (bit30)
                packet.ReadInt32("Int30");

            packet.ReadXORByte(ownerGUID, 6);
            packet.ReadSingle("Float1C");
            packet.ReadXORByte(ownerGUID, 3);
            pos.X = packet.ReadSingle();
            packet.ReadXORByte(ownerGUID, 2);

            if (bit34)
                packet.ReadInt32("Int34");

            if (bit60)
                packet.ReadByte("Byte60");

            packet.ReadSingle("Float18");
            pos.Z = packet.ReadSingle();

            // Calculate mid pos
            var mid = new Vector3();
            mid.X = (pos.X + endpos.X) * 0.5f;
            mid.Y = (pos.Y + endpos.Y) * 0.5f;
            mid.Z = (pos.Z + endpos.Z) * 0.5f;
            for (var i = 0; i < bits64; ++i)
            {
                var vec = new Vector3
                {
                    X = mid.X - waypoints[i].X,
                    Y = mid.Y - waypoints[i].Y,
                    Z = mid.Z - waypoints[i].Z
                };
                packet.AddValue("Waypoint", vec, i);
            }

            packet.WriteGuid("Owner GUID", ownerGUID);
            packet.WriteGuid("GUID2", guid2);
            packet.AddValue("Position", pos);
        }


        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            var pos = new Vector4();

            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            pos.X = packet.ReadSingle();
            pos.O = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();

            packet.AddValue("Position", pos);
            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            packet.ReadInt32<MapId>("Map");
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.O = packet.ReadSingle();

            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_BIND_POINT_UPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            var pos = new Vector3();

            packet.ReadInt32<AreaId>("Area Id");
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();

            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");

            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_LOGIN_SET_TIME_SPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.ReadInt32("Unk Int32");
            packet.ReadInt32("Unk Int32");
            packet.ReadPackedTime("Game Time");
            packet.ReadSingle("Game Speed");

            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            var customLoadScreenSpell = packet.ReadBit();
            var hasTransport = packet.ReadBit();

            packet.ReadInt32<MapId>("Map ID");

            if (hasTransport)
            {
                packet.ReadInt32<MapId>("Transport Map ID");
                packet.ReadInt32("Transport Entry");
            }

            if (customLoadScreenSpell)
                packet.ReadUInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_MOVE_TELEPORT)]
        public static void HandleMoveTeleport(Packet packet)
        {
            var pos = new Vector4();

            var transGuid = new byte[8];
            var guid = new byte[8];

            var onTransport = packet.ReadBit();

            guid[3] = packet.ReadBit();

            if (onTransport)
                packet.StartBitStream(transGuid, 1, 3, 6, 4, 5, 7, 0, 2);

            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            var bit17 = packet.ReadBit();
            if (bit17)
            {
                packet.ReadBit("Unk bit 50");
                packet.ReadBit("Unk bit 51");
            }

            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            if (onTransport)
            {
                packet.ParseBitStream(transGuid, 2, 3, 5, 0, 4, 6, 1, 7);
                packet.WriteGuid("Transport Guid", transGuid);
            }

            if (bit17)
                packet.ReadByte("Byte14");

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            pos.O = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            packet.ReadXORByte(guid, 3);
            pos.Y = packet.ReadSingle();
            packet.ReadUInt32("Time");
            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);

            packet.AddValue("Destination", pos);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_RUN_SPEED)]
        public static void HandleMoveSetRunSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(3, 4, 5, 7, 1, 2, 6, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadInt32("Unk Int32");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_WALK_SPEED)]
        public static void HandleMoveSetWalkSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(2, 6, 1, 0, 3, 5, 4, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadInt32("Unk Int32");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_SWIM_SPEED)]
        public static void HandleMoveSetSwimSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(5, 1, 4, 3, 0, 7, 2, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadInt32("Unk Int32");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_SPEED)]
        public static void HandleMoveSetFlightSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 3, 0, 6, 7, 4, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadInt32("Unk Int32");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_CAN_FLY)]
        public static void HandleSetCanFly(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 4, 7, 0, 6, 3, 5, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadInt32("Movement Counter");
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_FLY)]
        public static void HandleUnsetCanFly(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 6, 1, 7, 4, 5, 3, 0);
            packet.ParseBitStream(guid, 2, 4, 5, 1, 7, 6, 3, 0);
            packet.ReadInt32("Movement Counter");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_MOVE_SET_FACING)]
        public static void HandleMoveSetFacing(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];

            var pos = new Vector4();

            var bit5C = false;
            var bit64 = false;
            var hasFallDirection = false;

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();

            var hasSplineElevation  = !packet.ReadBit();

            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            var hasRotation = packet.ReadBit();
            var hasTime = !packet.ReadBit();
            var bit20 = !packet.ReadBit();
            var hasTrans = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var hasFallData = packet.ReadBit();

            guid[4] = packet.ReadBit();

            var bitAC = packet.ReadBit();

            guid[3] = packet.ReadBit();

            var hasPitch = !packet.ReadBit();

            guid[1] = packet.ReadBit();

            var bitA8 = !packet.ReadBit();
            var bit94 = packet.ReadBit();

            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            var hasMovementFlags = !packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();

            if (hasTrans)
            {
                transportGuid[3] = packet.ReadBit();
                bit64 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                bit5C = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
            }

            if (hasMovementFlagsExtra)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.ParseBitStream(guid, 3, 6, 4, 7, 0, 2, 5, 1);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            if (hasTrans)
            {
                packet.ReadSingle("Float48");

                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 7);

                if (bit5C)
                    packet.ReadInt32("Int58");

                packet.ReadXORByte(transportGuid, 4);

                packet.ReadInt32("Int54");
                packet.ReadByte("Byte50");

                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 5);

                if (bit64)
                    packet.ReadInt32("Int60");

                packet.ReadSingle("Float4C");
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadSingle("Float44");
                packet.ReadSingle("Float40");

                packet.WriteGuid("Transport Guid", transportGuid);
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

            if (hasRotation)
                pos.O = packet.ReadSingle();

            if (hasTime)
                packet.ReadUInt32("Timestamp");

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (bit20)
                packet.ReadInt32("Int20");

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 0, 7, 2, 3, 6, 5, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadSingle("Duration modifier");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_MOVE_HEARTBEAT)]
        public static void HandleMoveHeartbeat(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];

            var pos = new Vector4();

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();

            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var bit95 = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasTrans = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            var bit94 = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var hasMovementFlagExtra = !packet.ReadBit();
            var bit90 = !packet.ReadBit();
            var hasMovementFlag = !packet.ReadBit();

            if (hasMovementFlagExtra)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasTrans)
            {
                transportGuid[3] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlag)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 0);

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.Y = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                packet.ReadByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 7);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 2);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 3);
                packet.ReadUInt32("Transport Time");

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 5);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 6);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasFallData)
            {
                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }
            }

            if (bit90)
                packet.ReadSingle("Float90");

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid2", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            CoreParsers.MovementHandler.ActivePhases.Clear();

            var guid = packet.StartBitStream(7, 5, 0, 4, 3, 1, 6, 2);
            packet.ParseBitStream(guid, 4, 5, 2);

            packet.ReadUInt32("UInt32 1");

            packet.ParseBitStream(guid, 1);

            var count = packet.ReadUInt32() / 2;
            packet.AddValue("Inactive Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadInt16<MapId>("Inactive Terrain swap", i);

            count = packet.ReadUInt32() / 2;
            packet.AddValue("Active Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadInt16<MapId>("Active Terrain swap", i);

            packet.ParseBitStream(guid, 3);


            count = packet.ReadUInt32() / 2;
            packet.AddValue("Phases count", count);
            for (var i = 0; i < count; ++i)
                CoreParsers.MovementHandler.ActivePhases.Add(packet.ReadUInt16("Phase id", i)); // Phase.dbc

            packet.ParseBitStream(guid, 7, 0);

            count = packet.ReadUInt32() / 2;
            packet.AddValue("WorldMapArea swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("WorldMapArea swap", i);

            packet.ParseBitStream(guid, 6);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_CONTROL_UPDATE)]
        public static void HandleClientControlUpdate(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 2, 4, 6, 5, 3, 1, 7);
            packet.ReadBit("AllowMove");
            packet.ParseBitStream(guid, 5, 2, 6, 4, 7, 0, 3, 1);

            packet.WriteGuid("Guid", guid);
        }
    }
}
