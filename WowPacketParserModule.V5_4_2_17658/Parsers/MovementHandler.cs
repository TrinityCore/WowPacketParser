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

            guid[7] = packet.Translator.ReadBit();

            var hasExtraMovementFlags = !packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var isAlive = !packet.Translator.ReadBit();

            guid[6] = packet.Translator.ReadBit();

            var bit94 = packet.Translator.ReadBit();

            guid[0] = packet.Translator.ReadBit();

            var hasTransportData = packet.Translator.ReadBit();

            bool hasTransportTime2 = false;
            bool hasTransportTime3 = false;

            if (hasTransportData)
            {
                transportGUID[4] = packet.Translator.ReadBit();
                transportGUID[1] = packet.Translator.ReadBit();
                transportGUID[6] = packet.Translator.ReadBit();
                transportGUID[0] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGUID[2] = packet.Translator.ReadBit();
                transportGUID[7] = packet.Translator.ReadBit();
                transportGUID[3] = packet.Translator.ReadBit();
                transportGUID[5] = packet.Translator.ReadBit();
            }

            guid[4] = packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasTimeStamp = !packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);

            if (hasExtraMovementFlags)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            var bitAC = packet.Translator.ReadBit();

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            var hasFallData = packet.Translator.ReadBit();

            var hasFallDirection = false;
            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                }

                packet.Translator.ReadInt32("Fall Time");
                packet.Translator.ReadSingle("Velocity Speed");
            }

            if (hasTransportData)
            {
                var transPos = new Vector4();

                packet.Translator.ReadXORByte(transportGUID, 1);

                transPos.Y = packet.Translator.ReadSingle();

                if (hasTransportTime2)
                    packet.Translator.ReadInt32("Transport Time 2");

                packet.Translator.ReadXORByte(transportGUID, 5);

                transPos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadInt32("Transport Time");

                packet.Translator.ReadXORByte(transportGUID, 3);
                packet.Translator.ReadXORByte(transportGUID, 6);

                transPos.O = packet.Translator.ReadSingle();
                transPos.Z = packet.Translator.ReadSingle();

                if (hasTransportTime3)
                    packet.Translator.ReadInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGUID, 7);
                packet.Translator.ReadXORByte(transportGUID, 4);
                packet.Translator.ReadXORByte(transportGUID, 2);
                packet.Translator.ReadXORByte(transportGUID, 0);

                packet.Translator.WriteGuid("Transport Guid", transportGUID);
                packet.AddValue("Transport Position", transPos);
            }

            packet.Translator.ReadXORByte(guid, 3);

            pos.Y = packet.Translator.ReadSingle();

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 6);

            if (hasTimeStamp)
                packet.Translator.ReadInt32("Timestamp");

            pos.Z = packet.Translator.ReadSingle();

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline Elevation");

            packet.Translator.ReadXORByte(guid, 1);

            if (isAlive)
                packet.Translator.ReadInt32("time(isAlive)");

            pos.X = packet.Translator.ReadSingle();

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);

            packet.Translator.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleMonsterMove(Packet packet)
        {
            var pos = new Vector3();

            var guid2 = new byte[8];
            var factingTargetGUID = new byte[8];
            var ownerGUID = new byte[8];

            ownerGUID[4] = packet.Translator.ReadBit();

            var bit40 = !packet.Translator.ReadBit();
            var hasTime = !packet.Translator.ReadBit();

            ownerGUID[3] = packet.Translator.ReadBit();

            var bit30 = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            var splineCount = (int)packet.Translator.ReadBits(20);
            var bit98 = packet.Translator.ReadBit();
            var splineType = (int)packet.Translator.ReadBits(3);

            if (splineType == 3)
                packet.Translator.StartBitStream(factingTargetGUID, 0, 7, 3, 4, 5, 6, 1, 2);

            var bit55 = !packet.Translator.ReadBit();
            var bit60 = !packet.Translator.ReadBit();
            var bit54 = !packet.Translator.ReadBit();
            var bit34 = !packet.Translator.ReadBit();

            ownerGUID[6] = packet.Translator.ReadBit();
            ownerGUID[1] = packet.Translator.ReadBit();
            ownerGUID[0] = packet.Translator.ReadBit();

            packet.Translator.StartBitStream(guid2, 1, 3, 4, 5, 6, 0, 7, 2);

            ownerGUID[5] = packet.Translator.ReadBit();
            var bit20 = packet.Translator.ReadBit();
            ownerGUID[7] = packet.Translator.ReadBit();
            var bit2D = !packet.Translator.ReadBit();

            var bits84 = 0u;
            if (bit98)
            {
                packet.Translator.ReadBits("bits74", 2);
                bits84 = packet.Translator.ReadBits(22);
            }

            var hasFlags = !packet.Translator.ReadBit();
            ownerGUID[2] = packet.Translator.ReadBit();
            var bits64 = (int)packet.Translator.ReadBits(22);
            var bit0 = !packet.Translator.ReadBit();
            packet.Translator.ReadSingle("Float14");

            if (splineType == 3)
            {
                packet.Translator.ParseBitStream(factingTargetGUID, 1, 6, 4, 3, 5, 0, 2, 7);
                packet.Translator.WriteGuid("Facting Target GUID", factingTargetGUID);
            }

            packet.Translator.ReadXORByte(ownerGUID, 5);
            if (bit54)
                packet.Translator.ReadByte("Byte54");
            packet.Translator.ReadXORByte(ownerGUID, 4);
            if (bit98)
            {
                packet.Translator.ReadSingle("Float88");
                for (var i = 0; i < bits84; ++i)
                {
                    packet.Translator.ReadInt16("short74+2", i);
                    packet.Translator.ReadInt16("short74+0", i);
                }

                packet.Translator.ReadInt16("Int8C");
                packet.Translator.ReadInt16("Int94");
                packet.Translator.ReadSingle("Float90");
            }

            if (hasFlags)
                packet.Translator.ReadInt32E<SplineFlag434>("Spline Flags");

            if (bit40)
                packet.Translator.ReadInt32("Int40");

            if (bit2D)
                packet.Translator.ReadByte("Byte2D");

            Vector3 endpos = new Vector3();
            for (var i = 0; i < splineCount; ++i)
            {
                var spot = new Vector3
                {
                    Y = packet.Translator.ReadSingle(),
                    X = packet.Translator.ReadSingle(),
                    Z = packet.Translator.ReadSingle()
                };
                // client always taking first point
                if (i == 0)
                {
                    endpos = spot;
                }

                packet.AddValue("Spline Waypoint", spot, i);
            }

            packet.Translator.ParseBitStream(guid2, 6, 7, 2, 5, 3, 4, 1, 0);

            if (bit55)
                packet.Translator.ReadByte("Byte55");

            packet.Translator.ReadInt32("Move Ticks");

            packet.Translator.ReadXORByte(ownerGUID, 0);

            if (splineType == 4)
                packet.Translator.ReadSingle("Facing Angle");

            pos.Y = packet.Translator.ReadSingle();

            if (bit0)
                packet.Translator.ReadSingle("Float3C");

            packet.Translator.ReadXORByte(ownerGUID, 7);
            packet.Translator.ReadXORByte(ownerGUID, 1);

            var waypoints = new Vector3[bits64];
            for (var i = 0; i < bits64; ++i)
            {
                var vec = packet.Translator.ReadPackedVector3();
                waypoints[i].X = vec.X;
                waypoints[i].Y = vec.Y;
                waypoints[i].Z = vec.Z;
            }

            if (splineType == 2)
            {
                packet.Translator.ReadSingle("FloatA8");
                packet.Translator.ReadSingle("FloatAC");
                packet.Translator.ReadSingle("FloatB0");
            }

            if (hasTime)
                packet.Translator.ReadInt32("Move Time in ms");

            if (bit30)
                packet.Translator.ReadInt32("Int30");

            packet.Translator.ReadXORByte(ownerGUID, 6);
            packet.Translator.ReadSingle("Float1C");
            packet.Translator.ReadXORByte(ownerGUID, 3);
            pos.X = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(ownerGUID, 2);

            if (bit34)
                packet.Translator.ReadInt32("Int34");

            if (bit60)
                packet.Translator.ReadByte("Byte60");

            packet.Translator.ReadSingle("Float18");
            pos.Z = packet.Translator.ReadSingle();

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

            packet.Translator.WriteGuid("Owner GUID", ownerGUID);
            packet.Translator.WriteGuid("GUID2", guid2);
            packet.AddValue("Position", pos);
        }


        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            var pos = new Vector4();

            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.Translator.ReadInt32<MapId>("Map");
            pos.X = packet.Translator.ReadSingle();
            pos.O = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();

            packet.AddValue("Position", pos);
            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            var pos = new Vector4();

            pos.Y = packet.Translator.ReadSingle();
            packet.Translator.ReadInt32<MapId>("Map");
            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.O = packet.Translator.ReadSingle();

            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_BIND_POINT_UPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            var pos = new Vector3();

            packet.Translator.ReadInt32<AreaId>("Area Id");
            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();

            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.Translator.ReadInt32<MapId>("Map");

            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_LOGIN_SET_TIME_SPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadPackedTime("Game Time");
            packet.Translator.ReadSingle("Game Speed");

            packet.Translator.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            var customLoadScreenSpell = packet.Translator.ReadBit();
            var hasTransport = packet.Translator.ReadBit();

            packet.Translator.ReadInt32<MapId>("Map ID");

            if (hasTransport)
            {
                packet.Translator.ReadInt32<MapId>("Transport Map ID");
                packet.Translator.ReadInt32("Transport Entry");
            }

            if (customLoadScreenSpell)
                packet.Translator.ReadUInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_MOVE_TELEPORT)]
        public static void HandleMoveTeleport(Packet packet)
        {
            var pos = new Vector4();

            var transGuid = new byte[8];
            var guid = new byte[8];

            var onTransport = packet.Translator.ReadBit();

            guid[3] = packet.Translator.ReadBit();

            if (onTransport)
                packet.Translator.StartBitStream(transGuid, 1, 3, 6, 4, 5, 7, 0, 2);

            guid[4] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();

            var bit17 = packet.Translator.ReadBit();
            if (bit17)
            {
                packet.Translator.ReadBit("Unk bit 50");
                packet.Translator.ReadBit("Unk bit 51");
            }

            guid[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            if (onTransport)
            {
                packet.Translator.ParseBitStream(transGuid, 2, 3, 5, 0, 4, 6, 1, 7);
                packet.Translator.WriteGuid("Transport Guid", transGuid);
            }

            if (bit17)
                packet.Translator.ReadByte("Byte14");

            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            pos.O = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 3);
            pos.Y = packet.Translator.ReadSingle();
            packet.Translator.ReadUInt32("Time");
            pos.X = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);

            packet.AddValue("Destination", pos);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_RUN_SPEED)]
        public static void HandleMoveSetRunSpeed(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(3, 4, 5, 7, 1, 2, 6, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_WALK_SPEED)]
        public static void HandleMoveSetWalkSpeed(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(2, 6, 1, 0, 3, 5, 4, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadInt32("Unk Int32");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_SWIM_SPEED)]
        public static void HandleMoveSetSwimSpeed(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(5, 1, 4, 3, 0, 7, 2, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_SPEED)]
        public static void HandleMoveSetFlightSpeed(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(1, 5, 3, 0, 6, 7, 4, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_CAN_FLY)]
        public static void HandleSetCanFly(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 4, 7, 0, 6, 3, 5, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadInt32("Movement Counter");
            packet.Translator.ReadXORByte(guid, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_FLY)]
        public static void HandleUnsetCanFly(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 6, 1, 7, 4, 5, 3, 0);
            packet.Translator.ParseBitStream(guid, 2, 4, 5, 1, 7, 6, 3, 0);
            packet.Translator.ReadInt32("Movement Counter");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.Y = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();

            var hasSplineElevation  = !packet.Translator.ReadBit();

            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();

            var hasRotation = packet.Translator.ReadBit();
            var hasTime = !packet.Translator.ReadBit();
            var bit20 = !packet.Translator.ReadBit();
            var hasTrans = packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var hasFallData = packet.Translator.ReadBit();

            guid[4] = packet.Translator.ReadBit();

            var bitAC = packet.Translator.ReadBit();

            guid[3] = packet.Translator.ReadBit();

            var hasPitch = !packet.Translator.ReadBit();

            guid[1] = packet.Translator.ReadBit();

            var bitA8 = !packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();

            guid[7] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            var hasMovementFlags = !packet.Translator.ReadBit();
            var hasMovementFlagsExtra = !packet.Translator.ReadBit();

            if (hasTrans)
            {
                transportGuid[3] = packet.Translator.ReadBit();
                bit64 = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                bit5C = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
            }

            if (hasMovementFlagsExtra)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.Translator.ParseBitStream(guid, 3, 6, 4, 7, 0, 2, 5, 1);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            if (hasTrans)
            {
                packet.Translator.ReadSingle("Float48");

                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadXORByte(transportGuid, 7);

                if (bit5C)
                    packet.Translator.ReadInt32("Int58");

                packet.Translator.ReadXORByte(transportGuid, 4);

                packet.Translator.ReadInt32("Int54");
                packet.Translator.ReadByte("Byte50");

                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 5);

                if (bit64)
                    packet.Translator.ReadInt32("Int60");

                packet.Translator.ReadSingle("Float4C");
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadSingle("Float44");
                packet.Translator.ReadSingle("Float40");

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
            }

            if (hasFallData)
            {
                packet.Translator.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Horizontal Speed");
                }

                packet.Translator.ReadUInt32("Fall time");
            }

            if (hasRotation)
                pos.O = packet.Translator.ReadSingle();

            if (hasTime)
                packet.Translator.ReadUInt32("Timestamp");

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (bit20)
                packet.Translator.ReadInt32("Int20");

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            packet.Translator.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 0, 7, 2, 3, 6, 5, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadSingle("Duration modifier");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("GUID", guid);
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

            pos.X = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();

            guid[2] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var bit95 = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasTrans = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var hasMovementFlagExtra = !packet.Translator.ReadBit();
            var bit90 = !packet.Translator.ReadBit();
            var hasMovementFlag = !packet.Translator.ReadBit();

            if (hasMovementFlagExtra)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasTrans)
            {
                transportGuid[3] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlag)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 0);

            if (hasTrans)
            {
                var tpos = new Vector4();

                tpos.Y = packet.Translator.ReadSingle();
                tpos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("Transport Seat");
                packet.Translator.ReadXORByte(transportGuid, 7);
                tpos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 2);

                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadUInt32("Transport Time");

                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                packet.Translator.ReadXORByte(transportGuid, 5);
                tpos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 6);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasFallData)
            {
                packet.Translator.ReadUInt32("Fall time");
                packet.Translator.ReadSingle("Vertical Speed");

                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Sin");
                }
            }

            if (bit90)
                packet.Translator.ReadSingle("Float90");

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            packet.Translator.WriteGuid("Guid2", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            CoreParsers.MovementHandler.ActivePhases.Clear();

            var guid = packet.Translator.StartBitStream(7, 5, 0, 4, 3, 1, 6, 2);
            packet.Translator.ParseBitStream(guid, 4, 5, 2);

            packet.Translator.ReadUInt32("UInt32 1");

            packet.Translator.ParseBitStream(guid, 1);

            var count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("Inactive Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt16<MapId>("Inactive Terrain swap", i);

            count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("Active Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt16<MapId>("Active Terrain swap", i);

            packet.Translator.ParseBitStream(guid, 3);


            count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("Phases count", count);
            for (var i = 0; i < count; ++i)
                CoreParsers.MovementHandler.ActivePhases.Add(packet.Translator.ReadUInt16("Phase id", i)); // Phase.dbc

            packet.Translator.ParseBitStream(guid, 7, 0);

            count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("WorldMapArea swap count", count);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadUInt16("WorldMapArea swap", i);

            packet.Translator.ParseBitStream(guid, 6);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_CONTROL_UPDATE)]
        public static void HandleClientControlUpdate(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 2, 4, 6, 5, 3, 1, 7);
            packet.Translator.ReadBit("AllowMove");
            packet.Translator.ParseBitStream(guid, 5, 2, 6, 4, 7, 0, 3, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
