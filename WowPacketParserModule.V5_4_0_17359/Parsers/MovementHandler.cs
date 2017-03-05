using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class MovementHandler
    {

        [Parser(Opcode.SMSG_LOGIN_SET_TIME_SPEED)]
        public static void HandleUnknown431(Packet packet)
        {
            packet.ReadSingle("Game Speed");
            packet.ReadPackedTime("Game Time");
            packet.ReadInt32("Unk 1");
            packet.ReadInt32("Unk 2");
            packet.ReadPackedTime("Game Time?");
        }

        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            packet.ReadInt32<MapId>("Map");
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.O = packet.ReadSingle();

            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_BIND_POINT_UPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            var pos = new Vector3();

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            packet.ReadInt32<AreaId>("Area Id");
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");

            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleMonsterMove(Packet packet)
        {
            var ownerGUID = new byte[8];
            var guid2 = new byte[8];
            var factingTargetGUID = new byte[8];
            var pos = new Vector3();

            var bitB0 = packet.ReadBit();
            ownerGUID[5] = packet.ReadBit();
            var hasAnimationTime = !packet.ReadBit();
            var hasTime = !packet.ReadBit();
            packet.StartBitStream(ownerGUID, 4, 3);
            var bit4C = !packet.ReadBit();
            var bit78 = packet.ReadBit();
            ownerGUID[2] = packet.ReadBit();
            var hasFlags = !packet.ReadBit();
            ownerGUID[0] = packet.ReadBit();

            var bits8C = 0u;
            if (bitB0)
            {
                packet.ReadBits("bits9C", 2);
                bits8C = packet.ReadBits(22);
            }

            var waypointCount = packet.ReadBits(22);
            var bit6D = !packet.ReadBit();
            ownerGUID[7] = packet.ReadBit();
            packet.ReadBit(); // fake bit
            packet.StartBitStream(guid2, 5, 3, 4, 6, 2, 1, 7, 0);
            var hasAnimationState = !packet.ReadBit();
            var hasParabolicTime = !packet.ReadBit();
            var hasParabolicSpeed = !packet.ReadBit();
            ownerGUID[6] = packet.ReadBit();
            var splineCount = packet.ReadBits(20);
            ownerGUID[1] = packet.ReadBit();
            var bit6C = !packet.ReadBit();
            var splineType = packet.ReadBits(3);

            if (splineType == 3)
                packet.StartBitStream(factingTargetGUID, 4, 6, 5, 1, 0, 7, 3, 2);

            packet.ReadBit("bit38");
            packet.ResetBitReader();

            if (bitB0)
            {
                packet.ReadSingle("floatA8");
                packet.ReadSingle("floatA0");
                packet.ReadInt16("shortAC");
                for (var i = 0; i < bits8C; ++i)
                {
                    packet.ReadInt16("short94+2", i);
                    packet.ReadInt16("short94+0", i);
                }
                packet.ReadInt16("shortA4");
            }

            packet.ReadXORBytes(guid2, 0, 1, 2, 7, 3, 4, 6, 5);
            if (splineType == 3)
            {
                packet.ParseBitStream(factingTargetGUID, 2, 1, 7, 0, 5, 3, 4, 6);
                packet.WriteGuid("Facting Target GUID", factingTargetGUID);
            }

            pos.Y = packet.ReadSingle();
            packet.ReadXORByte(ownerGUID, 7);
            if (hasAnimationTime)
                packet.ReadInt32("Asynctime in ms"); // Async-time in ms

            var waypoints = new Vector3[waypointCount];
            for (var i = 0; i < waypointCount; ++i)
            {
                var vec = packet.ReadPackedVector3();
                waypoints[i].X = vec.X;
                waypoints[i].Y = vec.Y;
                waypoints[i].Z = vec.Z;
            }

            packet.ReadSingle("Float2C");
            pos.Z = packet.ReadSingle();
            packet.ReadSingle("Float30");

            if (splineType == 2)
            {
                var spot = new Vector3
                {
                    Y = packet.ReadSingle(),
                    Z = packet.ReadSingle(),
                    X = packet.ReadSingle()
                };
                packet.AddValue("Facing Spot", spot);
            }

            Vector3 endpos = new Vector3();
            for (var i = 0; i < splineCount; ++i)
            {
                var spot = new Vector3
                {
                    Y = packet.ReadSingle(),
                    Z = packet.ReadSingle(),
                    X = packet.ReadSingle()
                };
                // client always taking first point
                if (i == 0)
                {
                    endpos = spot;
                }

                packet.AddValue("Spline Waypoint", spot, i);
            }

            packet.ReadXORByte(ownerGUID, 5);
            packet.ReadInt32("Move Ticks");

            if (hasFlags)
                packet.ReadInt32E<SplineFlag434>("Spline Flags");

            if (hasAnimationState)
                packet.ReadByteE<MovementAnimationState>("Animation State");

            packet.ReadXORByte(ownerGUID, 0);
            if (bit6D)
                packet.ReadByte("byte6D");

            if (hasParabolicTime)
                packet.ReadInt32("Async-time in ms");

            pos.X = packet.ReadSingle();
            if (hasTime)
                packet.ReadInt32("Move Time in ms");

            packet.ReadXORByte(ownerGUID, 4);
            if (hasParabolicSpeed)
                packet.ReadSingle("Vertical Speed");

            if (!bit78)
                packet.ReadByte("byte78");

            if (splineType == 4)
                packet.ReadSingle("Facing Angle");

            if (bit6C)
                packet.ReadByte("byte6C");

            packet.ReadSingle("Float34");
            if (bit4C)
                packet.ReadInt32("int4C");

            packet.ReadXORBytes(ownerGUID, 6, 2, 3, 1);

            // Calculate mid pos
            var mid = new Vector3();
            mid.X = (pos.X + endpos.X) * 0.5f;
            mid.Y = (pos.Y + endpos.Y) * 0.5f;
            mid.Z = (pos.Z + endpos.Z) * 0.5f;
            for (var i = 0; i < waypointCount; ++i)
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

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            CoreParsers.MovementHandler.ActivePhases.Clear();
            packet.ReadUInt32("UInt32 1");
            //packet.ReadUInt32("UInt32 2");

            var count = packet.ReadUInt32() / 2;
            packet.AddValue("WorldMapArea swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("WorldMapArea swap", i);

            count = packet.ReadUInt32() / 2;
            packet.AddValue("Phases count", count);
            for (var i = 0; i < count; ++i)
                CoreParsers.MovementHandler.ActivePhases.Add(packet.ReadUInt16("Phase id", i)); // Phase.dbc

            count = packet.ReadUInt32() / 2;
            packet.AddValue("Inactive Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadInt16<MapId>("Inactive Terrain swap", i);


            count = packet.ReadUInt32() / 2;
            packet.AddValue("Active Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadInt16<MapId>("Active Terrain swap", i);

            var guid = packet.StartBitStream(0, 2, 1, 5, 3, 7, 4, 6);
            packet.ParseBitStream(guid, 0, 5, 4, 7, 6, 2, 1, 3);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE)]
        public static void HandlePlayerMove(Packet packet)
        {
            var guid = new byte[8];
            var transportGUID = new byte[8];
            var pos = new Vector4();

            var bit95 = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var counter2 = (int)packet.ReadBits(22);
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasTimeStamp = !packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            var hasTransportTime2 = false;
            var hasTransportTime3 = false;

            if (hasTransportData)
            {
                packet.StartBitStream(transportGUID, 1, 2, 3, 4, 5);
                hasTransportTime3 = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                packet.StartBitStream(transportGUID, 0, 7, 6);
            }

            var hasMovementFlags = !packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            var hasOrientation = !packet.ReadBit();
            var isAlive = !packet.ReadBit();
            var hasFallDirection = false;
            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            var hasExtraMovementFlags = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var bit94 = packet.ReadBit();

            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            var hasSplineElevation = !packet.ReadBit();

            pos.X = packet.ReadSingle();

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadInt32("Fall Time");
                packet.ReadSingle("Velocity Speed");
            }

            packet.ReadXORByte(guid, 3);

            if (hasTransportData)
            {
                var transPos = new Vector4();

                packet.ReadXORByte(transportGUID, 2);
                packet.ReadXORByte(transportGUID, 0);
                packet.ReadXORByte(transportGUID, 5);
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGUID, 4);
                packet.ReadXORByte(transportGUID, 3);
                if (hasTransportTime2)
                    packet.ReadInt32("Transport Time 2");

                packet.ReadXORByte(transportGUID, 6);
                packet.ReadXORByte(transportGUID, 7);
                transPos.X = packet.ReadSingle();
                if (hasTransportTime3)
                    packet.ReadInt32("Transport Time 3");

                packet.ReadInt32("Transport Time");
                transPos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGUID, 1);
                transPos.Y = packet.ReadSingle();
                transPos.O = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGUID);
                packet.AddValue("Transport Position", transPos);
            }

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);

            for (var i = 0; i < counter2; ++i)
                packet.ReadInt32("Int9C", i);

            packet.ReadXORByte(guid, 1);

            if (hasPitch)
                packet.ReadSingle("Pitch");

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();

            packet.ReadXORByte(guid, 4);

            if (isAlive)
                packet.ReadInt32("time(isAlive)");

            if (hasSplineElevation)
                packet.ReadSingle("Spline Elevation");

            if (hasTimeStamp)
                packet.ReadInt32("Timestamp");

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED)]
        public static void HandleMoveTimeSkipped434(Packet packet)
        {
            packet.ReadUInt32("Time");
            var guid = packet.StartBitStream(3, 0, 5, 1, 7, 6, 4, 2);
            packet.ParseBitStream(guid, 1, 6, 0, 5, 3, 4, 2, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_RUN_SPEED)]
        public static void HandleMoveUpdateRunSpeed(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var pos = new Vector4();

            var hasTransportTime3 = false;
            var hasTransportTime2 = false;

            guid[7] = packet.ReadBit();

            var hasPitch = !packet.ReadBit();

            guid[0] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            packet.StartBitStream(guid, 1, 4, 5);

            var hasMovementFlags = !packet.ReadBit();

            var hasFallDirection = false;
            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            guid[2] = packet.ReadBit();

            packet.ReadBit("Has spline data");

            var hasMovementFlagsExtra = !packet.ReadBit();
            var hasTransport = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var hasTime = !packet.ReadBit();

            if (hasTransport)
            {
                hasTransportTime3 = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                packet.StartBitStream(transportGuid, 4, 5, 3, 2, 7, 0, 1, 6);
            }

            var bit95 = packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            var hasO = !packet.ReadBit();

            guid[3] = packet.ReadBit();

            var counter = packet.ReadBits(22);
            var bit94 = packet.ReadBit();

            guid[6] = packet.ReadBit();
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);

            if (hasTransport)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 7);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.O = packet.ReadSingle();

                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 0);

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadSByte("Transport Seat");
                tpos.Z = packet.ReadSingle();
                tpos.Y = packet.ReadSingle();

                packet.ReadXORByte(transportGuid, 2);

                tpos.X = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");

                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 3);

                packet.WriteGuid("Transport GUID", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

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

            pos.Y = packet.ReadSingle();
            packet.ReadXORByte(guid, 7);

            if (hasTime)
                packet.ReadInt32("Timestamp");

            pos.Z = packet.ReadSingle();
            packet.ReadXORByte(guid, 3);

            for (var i = 0; i < counter; i++)
                packet.ReadInt32("IntEA");

            packet.ReadXORByte(guid, 0);

            if (hasO)
                pos.O = packet.ReadSingle();

            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 1);

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 5);

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 6, 1, 4, 5, 2, 3, 7);
            packet.ParseBitStream(guid, 7, 3, 5, 4, 2, 6, 1, 0);
            packet.ReadSingle("Duration modifier");

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_SWIM_SPEED)]
        public static void HandleUnknown5730(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 3, 4, 2, 5, 7, 0, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 7, 1, 6, 3, 0, 2, 5);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_WALK_BACK_SPEED)]
        public static void HandleSplineSetWalkSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 0, 6, 5, 7, 4, 3, 2);
            packet.ParseBitStream(guid, 7, 5, 1, 6, 3, 2, 4, 0);

            packet.ReadSingle("Speed");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending434(Packet packet)
        {
            packet.ReadInt32<MapId>("Map ID");

            var customLoadScreenSpell = packet.ReadBit();
            var hasTransport = packet.ReadBit();
            if (hasTransport)
            {
                packet.ReadInt32<MapId>("Transport Map ID");
                packet.ReadInt32("Transport Entry");
            }

            if (customLoadScreenSpell)
                packet.ReadUInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_SET_PLAY_HOVER_ANIM)]
        public static void HandlePlayHoverAnim(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 1, 6, 0, 4);
            packet.ReadBit("unk");
            packet.StartBitStream(guid, 2, 5, 7);
            packet.ParseBitStream(guid, 3, 2, 1, 4, 6, 7, 0, 5);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_DISABLE_GRAVITY)]
        public static void HandleSplineMoveGravityDisable(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 5, 3, 6, 1, 4, 2, 7);
            packet.ParseBitStream(guid, 6, 3, 1, 5, 4, 7, 2, 0);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_ROOT)]
        public static void HandleSplineMoveRoot(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 2, 5, 1, 0, 7, 6, 3);
            packet.ParseBitStream(guid, 7, 5, 3, 0, 6, 1, 4, 2);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_1815)]
        public static void HandleUnknown1815(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var bit5C = false;
            var bit64 = false;
            var bit88 = false;

            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();

            packet.StartBitStream(guid1, 1, 3);
            var bit18 = !packet.ReadBit();
            guid1[0] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid1[4] = packet.ReadBit();
            var bit70 = !packet.ReadBit();
            guid1[2] = packet.ReadBit();
            var bit95 = packet.ReadBit();
            var bit94 = packet.ReadBit();
            var bits98 = packet.ReadBits(22);
            var bit8C = packet.ReadBit();
            packet.StartBitStream(guid1, 5, 7);
            var bit90 = !packet.ReadBit();
            var bit68 = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var hasTime = !packet.ReadBit();
            var bit1C = !packet.ReadBit();
            guid1[6] = packet.ReadBit();

            if (bit68)
            {
                guid2[1] = packet.ReadBit();
                guid2[2] = packet.ReadBit();
                guid2[6] = packet.ReadBit();
                bit64 = packet.ReadBit();
                guid2[3] = packet.ReadBit();
                bit5C = packet.ReadBit();
                guid2[0] = packet.ReadBit();
                guid2[7] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                guid2[5] = packet.ReadBit();
            }

            var bits18 = 0u;
            if (bit18)
                bits18 = packet.ReadBits(30);

            var bits1C = 0u;
            if (bit1C)
                bits1C = packet.ReadBits(13);

            if (bit8C)
                bit88 = packet.ReadBit();

            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 5);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED");

            packet.ReadXORByte(guid1, 6);

            if (bit68)
            {
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(guid2, 2);
                packet.ReadSingle("Float44");
                packet.ReadXORByte(guid2, 5);
                packet.ReadInt32("Int54");
                packet.ReadSingle("Float40");
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid2, 3);
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadXORByte(guid2, 4);
                packet.ReadSingle("Float48");
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid2, 7);
                packet.ReadSingle("Float4C");
                if (bit5C)
                    packet.ReadInt32("Int58");
                packet.ReadByte("Byte50");
                packet.WriteGuid("Guid7", guid2);
            }

            if (bit90)
                packet.ReadSingle("Float90");

            if (bit8C)
            {
                packet.ReadInt32("Int74");
                if (bit88)
                {
                    packet.ReadSingle("Float84");
                    packet.ReadSingle("Float80");
                    packet.ReadSingle("Float7C");
                }

                packet.ReadSingle("Float78");
            }

            if (bit70)
                packet.ReadSingle("Float70");
            if (hasTime)
                packet.ReadInt32("Time");
            if (bitA8)
                packet.ReadInt32("IntA8");
            if (hasOrientation)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid1);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.CMSG_UNKNOWN_2874)]
        public static void HandleUnknown2874(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var bit5C = false;
            var bit64 = false;
            var bit88 = false;

            var pos = new Vector4();

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();

            var bit18 = !packet.ReadBit();
            guid1[1] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            var bit94 = packet.ReadBit();
            packet.StartBitStream(guid1, 4, 3);
            var bit1C = !packet.ReadBit();
            var bit70 = !packet.ReadBit();
            var bit68 = packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            var bits98 = packet.ReadBits(22);
            guid1[5] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var bit95 = packet.ReadBit();
            packet.StartBitStream(guid1, 6, 2);
            var bit90 = !packet.ReadBit();
            var hasTime = !packet.ReadBit();
            guid1[0] = packet.ReadBit();
            var bit8C = packet.ReadBit();

            var bits18 = 0u;
            if (bit18)
                bits18 = packet.ReadBits(30);

            var bits1C = 0u;
            if (bit1C)
                bits1C = packet.ReadBits(13);

            if (bit68)
            {
                guid2[3] = packet.ReadBit();
                bit5C = packet.ReadBit();
                guid2[1] = packet.ReadBit();
                guid2[0] = packet.ReadBit();
                bit64 = packet.ReadBit();
                guid2[7] = packet.ReadBit();
                guid2[5] = packet.ReadBit();
                guid2[2] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                guid2[6] = packet.ReadBit();
            }

            if (bit8C)
                bit88 = packet.ReadBit();
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 6);
            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED");

            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 5);
            if (bit68)
            {
                packet.ReadSingle("Float48");
                packet.ReadXORByte(guid2, 2);
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(guid2, 1);
                if (bit5C)
                    packet.ReadInt32("Int58");
                packet.ReadXORByte(guid2, 5);
                packet.ReadByte("Byte50");
                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(guid2, 0);
                packet.ReadSingle("Float44");
                packet.ReadSingle("Float40");
                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid2, 7);
                packet.ReadSingle("Float4C");
                packet.ReadInt32("Int54");
                packet.WriteGuid("Guid7", guid2);
            }

            if (bitA8)
                packet.ReadInt32("IntA8");
            if (bit8C)
            {
                if (bit88)
                {
                    packet.ReadSingle("Float80");
                    packet.ReadSingle("Float7C");
                    packet.ReadSingle("Float84");
                }

                packet.ReadSingle("Float78");
                packet.ReadInt32("Int74");
            }

            if (bit90)
                packet.ReadSingle("Float90");
            if (hasTime)
                packet.ReadInt32("Time");
            if (bit70)
                packet.ReadSingle("Float70");
            if (hasOrientation)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid1);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_FLIGHT_SPEED)]
        public static void HandleMoveUpdateFlightSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 1, 5, 6, 4, 3, 0, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 6, 2, 3, 1, 7, 4, 5);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6754)]
        public static void HandleUnknown6754(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var bit64 = false;
            var bit6C = false;
            var bit90 = false;

            var pos = new Vector4();

            pos.O = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            packet.ReadSingle("Float10");
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();

            var hasMovementFlags = !packet.ReadBit();
            guid1[1] = packet.ReadBit();
            var bit70 = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            var bit94 = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            var bitB4 = packet.ReadBit();
            if (bit70)
            {
                bit6C = packet.ReadBit();
                packet.StartBitStream(guid2, 2, 7);
                bit64 = packet.ReadBit();
                packet.StartBitStream(guid2, 5, 4, 6, 0, 1, 3);
            }

            var bit98 = !packet.ReadBit();
            var bit9C = packet.ReadBit();
            packet.StartBitStream(guid1, 3, 5, 4);
            var bit78 = !packet.ReadBit();
            var bit38 = !packet.ReadBit();
            if (bit94)
                bit90 = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            var bitsA0 = packet.ReadBits(22);
            var bit9D = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            var bitB0 = !packet.ReadBit();
            guid1[2] = packet.ReadBit();
            var hasExtraMovementFlags = !packet.ReadBit();
            var bit28 = !packet.ReadBit();

            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (bit70)
            {
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid2, 6);
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid2, 4);
                packet.ReadInt32("Int5C");
                packet.ReadSingle("Float54");
                packet.ReadXORByte(guid2, 3);
                packet.ReadSingle("Float50");
                packet.ReadByte("Byte58");
                packet.ReadXORByte(guid2, 7);
                packet.ReadXORByte(guid2, 2);
                if (bit6C)
                    packet.ReadInt32("Int68");
                packet.ReadSingle("Float4C");
                packet.ReadXORByte(guid2, 5);
                packet.ReadSingle("Float48");
                packet.WriteGuid("Guid8", guid2);
            }

            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 6);
            if (bit38)
                packet.ReadSingle("Float38");
            packet.ReadXORByte(guid1, 0);
            if (bit78)
                packet.ReadSingle("Float78");
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 5);
            if (bit98)
                packet.ReadSingle("Float98");
            if (bit94)
            {
                packet.ReadSingle("Float80");
                packet.ReadInt32("Int7C");
                if (bit90)
                {
                    packet.ReadSingle("Float88");
                    packet.ReadSingle("Float84");
                    packet.ReadSingle("Float8C");
                }

            }

            packet.ReadXORByte(guid1, 3);
            if (bit28)
                packet.ReadInt32("Int28");
            packet.ReadXORByte(guid1, 2);

            for (var i = 0; i < bitsA0; ++i)
                packet.ReadInt32("IntEA", i);

            if (bitB0)
                packet.ReadInt32("IntB0");
            packet.ReadXORByte(guid1, 7);

            packet.WriteGuid("Guid3", guid1);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.MSG_UNKNOWN_5750)]
        public static void HandleUnknown5750(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var guid = new byte[8];

                packet.StartBitStream(guid, 1, 7, 4, 5, 3, 6, 2);
                packet.ReadBit("bit16");
                guid[0] = packet.ReadBit();
                packet.ParseBitStream(guid, 0, 2, 3, 1, 4, 6, 5, 7);

                packet.WriteGuid("Guid", guid);
            }
            else
            {
                var guid1 = new byte[8];
                var guid2 = new byte[8];

                var bit64 = false;
                var bit6C = false;
                var bit90 = false;

                var bit98 = !packet.ReadBit();
                packet.StartBitStream(guid1, 2, 6, 0, 1, 5);
                var bit28 = !packet.ReadBit();
                var bit9C = packet.ReadBit();
                var bitsA0 = packet.ReadBits(22);
                guid1[4] = packet.ReadBit();

                var bit70 = packet.ReadBit();
                if (bit70)
                {
                    packet.StartBitStream(guid2, 5, 6);
                    bit6C = packet.ReadBit();
                    packet.StartBitStream(guid2, 7, 2, 0, 4, 3);
                    bit64 = packet.ReadBit();
                    guid2[1] = packet.ReadBit();
                }

                guid1[3] = packet.ReadBit();
                var bit38 = !packet.ReadBit();
                var hasExtraMovementFlags = !packet.ReadBit();
                var bitB0 = !packet.ReadBit();
                var hasMovementFlags = !packet.ReadBit();
                guid1[7] = packet.ReadBit();
                var bit94 = packet.ReadBit();
                var bitB4 = packet.ReadBit();

                if (hasMovementFlags)
                    packet.ReadBitsE<MovementFlag>("Movement flags", 30);

                if (hasExtraMovementFlags)
                    packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

                if (bit94)
                    bit90 = packet.ReadBit();

                var bit78 = !packet.ReadBit();
                var bit9D = packet.ReadBit();

                if (bit70)
                {
                    packet.ReadXORByte(guid2, 1);
                    packet.ReadXORByte(guid2, 5);
                    packet.ReadSingle("Float54");
                    packet.ReadSingle("Float48");
                    packet.ReadXORByte(guid2, 2);
                    packet.ReadXORByte(guid2, 3);
                    if (bit64)
                        packet.ReadInt32("Int60");
                    packet.ReadXORByte(guid2, 6);
                    packet.ReadXORByte(guid2, 0);
                    packet.ReadByte("Byte58");
                    packet.ReadSingle("Float50");
                    packet.ReadSingle("Float4C");
                    packet.ReadXORByte(guid2, 7);
                    packet.ReadXORByte(guid2, 4);
                    packet.ReadInt32("Int5C");
                    if (bit6C)
                        packet.ReadInt32("Int68");
                    packet.WriteGuid("Guid2", guid2);
                }

                if (bit38)
                    packet.ReadSingle("Float38");
                packet.ReadXORByte(guid1, 7);
                packet.ReadSingle("Float34");
                packet.ReadXORByte(guid1, 2);
                if (bit94)
                {
                    if (bit90)
                    {
                        packet.ReadSingle("Float84");
                        packet.ReadSingle("Float88");
                        packet.ReadSingle("Float8C");
                    }

                    packet.ReadSingle("Float80");
                    packet.ReadInt32("Int7C");
                }

                packet.ReadXORByte(guid1, 6);
                if (bitB0)
                    packet.ReadInt32("IntB0");
                if (bit28)
                    packet.ReadInt32("Int28");
                packet.ReadSingle("Float30");
                packet.ReadXORByte(guid1, 1);
                if (bit98)
                    packet.ReadSingle("Float98");
                packet.ReadXORByte(guid1, 4);
                if (bit78)
                    packet.ReadSingle("Float78");
                packet.ReadXORByte(guid1, 5);

                for (var i = 0; i < bitsA0; ++i)
                    packet.ReadInt32("IntEA", i);

                packet.ReadSingle("Float10");
                packet.ReadXORByte(guid1, 0);
                packet.ReadSingle("Float2C");
                packet.ReadXORByte(guid1, 3);

                packet.WriteGuid("Guid1", guid1);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_5722)]
        public static void HandleUnknown5722(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var bit5C = false;
            var bit64 = false;
            var bit88 = false;

            var hasExtraMovementFlags = !packet.ReadBit();

            var hasMovementFlags = !packet.ReadBit();
            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            packet.StartBitStream(guid1, 2, 4, 5);
            var bit95 = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            var bit70 = !packet.ReadBit();
            guid1[6] = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            var bit68 = packet.ReadBit();
            if (bit68)
            {
                packet.StartBitStream(guid2, 0, 1);
                bit5C = packet.ReadBit();
                packet.StartBitStream(guid2, 4, 2, 6, 5);
                bit64 = packet.ReadBit();
                packet.StartBitStream(guid2, 3, 7);
            }

            guid1[7] = packet.ReadBit();
            var bit8C = packet.ReadBit();
            var bit94 = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            if (bit8C)
                bit88 = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            var bit90 = !packet.ReadBit();
            var bit20 = !packet.ReadBit();
            var bit30 = !packet.ReadBit();
            if (bit68)
            {
                packet.ReadXORByte(guid2, 7);
                packet.ReadXORByte(guid2, 2);
                packet.ReadByte("Byte50");
                if (bit5C)
                    packet.ReadInt32("Int58");
                packet.ReadXORByte(guid2, 1);
                packet.ReadSingle("Float48");
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(guid2, 5);
                packet.ReadSingle("Float40");
                packet.ReadSingle("Float44");
                packet.ReadSingle("Float4C");
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadXORByte(guid2, 4);
                packet.ReadInt32("Int54");
                packet.ReadXORByte(guid2, 3);
                packet.WriteGuid("Guid2", guid2);
            }

            if (bit70)
                packet.ReadSingle("Float70");
            if (bit20)
                packet.ReadInt32("Int20");
            if (bit8C)
            {
                packet.ReadInt32("Int74");
                packet.ReadSingle("Float78");
                if (bit88)
                {
                    packet.ReadSingle("Float7C");
                    packet.ReadSingle("Float80");
                    packet.ReadSingle("Float84");
                }
            }

            if (bitA8)
                packet.ReadInt32("IntA8");
            packet.ReadSingle("FloatB0");
            packet.ReadSingle("Float24");
            if (bit30)
                packet.ReadSingle("Float30");
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 2);
            packet.ReadSingle("Float28");
            packet.ReadXORByte(guid1, 4);
            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntEA", i);

            if (bit90)
                packet.ReadSingle("Float90");
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 1);
            packet.ReadSingle("Float2C");
            packet.ReadXORByte(guid1, 5);

            packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6742)]
        public static void HandleUnknown6742(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var bit5C = false;
            var bit64 = false;
            var bit88 = false;

            var bit68 = packet.ReadBit();
            if (bit68)
            {
                packet.StartBitStream(guid2, 5, 7, 3, 6);
                bit64 = packet.ReadBit();
                packet.StartBitStream(guid2, 2, 0);
                bit5C = packet.ReadBit();
                packet.StartBitStream(guid2, 4, 1);
            }

            guid1[6] = packet.ReadBit();
            var hasExtraMovementFlags = !packet.ReadBit();
            guid1[3] = packet.ReadBit();
            var bit20 = !packet.ReadBit();
            var bit30 = !packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var bit90 = !packet.ReadBit();
            guid1[2] = packet.ReadBit();
            var bit8C = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            var bit94 = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            guid1[7] = packet.ReadBit();
            var bit70 = !packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            if (bit8C)
                bit88 = packet.ReadBit();

            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            packet.StartBitStream(guid1, 0, 1);
            var bit95 = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            packet.ReadXORByte(guid1, 5);
            if (bit68)
            {
                packet.ReadXORByte(guid2, 4);
                packet.ReadInt32("Int54");
                packet.ReadXORByte(guid2, 5);
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadSingle("Float48");
                packet.ReadXORByte(guid2, 2);
                packet.ReadXORByte(guid2, 0);
                packet.ReadSingle("Float4C");
                packet.ReadSingle("Float40");
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid2, 6);
                if (bit5C)
                    packet.ReadInt32("Int58");
                packet.ReadByte("Byte50");
                packet.ReadXORByte(guid2, 7);
                packet.ReadSingle("Float44");
                packet.ReadXORByte(guid2, 3);
                packet.WriteGuid("Guid2", guid2);
            }

            if (bit8C)
            {
                packet.ReadSingle("Float78");
                if (bit88)
                {
                    packet.ReadSingle("Float84");
                    packet.ReadSingle("Float80");
                    packet.ReadSingle("Float7C");
                }

                packet.ReadInt32("Int74");
            }

            if (bit20)
                packet.ReadInt32("Int20");

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntEA", i);

            packet.ReadSingle("Float28");
            if (bit30)
                packet.ReadSingle("Float30");
            packet.ReadXORByte(guid1, 2);
            if (bit90)
                packet.ReadSingle("Float90");
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadSingle("FloatB0");
            packet.ReadSingle("Float2C");
            if (bitA8)
                packet.ReadInt32("IntA8");
            packet.ReadXORByte(guid1, 7);
            if (bit70)
                packet.ReadSingle("Float70");
            packet.ReadSingle("Float24");
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 0);

            packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5942)]
        public static void HandleUnknown5942(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var bit5C = false;
            var bit64 = false;
            var bit88 = false;

            var bit20 = !packet.ReadBit();
            packet.StartBitStream(guid1, 2, 5);
            var bit90 = !packet.ReadBit();
            packet.StartBitStream(guid1, 3, 1);
            var bitA8 = !packet.ReadBit();
            guid1[6] = packet.ReadBit();
            var bit95 = packet.ReadBit();
            var bit68 = packet.ReadBit();
            guid1[7] = packet.ReadBit();

            if (bit68)
            {
                packet.StartBitStream(guid2, 5, 7, 1, 6);
                bit5C = packet.ReadBit();
                packet.StartBitStream(guid2, 0, 2, 3);
                bit64 = packet.ReadBit();
                guid2[4] = packet.ReadBit();
            }

            var bitAC = packet.ReadBit();
            var bit94 = packet.ReadBit();
            var bit30 = !packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var bit8C = packet.ReadBit();
            if (bit8C)
                bit88 = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            var bit70 = !packet.ReadBit();

            var hasExtraMovementFlags = !packet.ReadBit();
            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            guid1[4] = packet.ReadBit();

            var hasMovementFlags = !packet.ReadBit();
            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            if (bit68)
            {
                if (bit5C)
                    packet.ReadInt32("Int58");
                packet.ReadXORByte(guid2, 7);
                packet.ReadSingle("Float44");
                packet.ReadXORByte(guid2, 1);
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadSingle("Float4C");
                packet.ReadXORByte(guid2, 2);
                packet.ReadXORByte(guid2, 5);
                packet.ReadXORByte(guid2, 0);
                packet.ReadSingle("Float48");
                packet.ReadInt32("Int54");
                packet.ReadXORByte(guid2, 3);
                packet.ReadSingle("Float40");
                packet.ReadXORByte(guid2, 6);
                packet.ReadByte("Byte50");
                packet.ReadXORByte(guid2, 4);
                packet.WriteGuid("Guid2", guid2);
            }

            if (bit8C)
            {
                if (bit88)
                {
                    packet.ReadSingle("Float80");
                    packet.ReadSingle("Float84");
                    packet.ReadSingle("Float7C");
                }

                packet.ReadSingle("Float78");
                packet.ReadInt32("Int74");
            }

            packet.ReadSingle("Float2C");
            packet.ReadXORByte(guid1, 1);
            if (bit90)
                packet.ReadSingle("Float90");
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 4);
            if (bit20)
                packet.ReadInt32("Int20");
            packet.ReadSingle("Float24");
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 6);
            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntEA", i);

            packet.ReadXORByte(guid1, 0);
            if (bit70)
                packet.ReadSingle("Float70");
            if (bitA8)
                packet.ReadInt32("IntA8");
            packet.ReadXORByte(guid1, 5);
            packet.ReadSingle("FloatB0");
            if (bit30)
                packet.ReadSingle("Float30");
            packet.ReadSingle("Float28");
            packet.ReadXORByte(guid1, 3);

            packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4890)]
        public static void HandleUnknown4890(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var bit94 = false;
            var bit9C = false;
            var bitC0 = false;

            var bitFC = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var bitEC = packet.ReadBit();
            var bitF4 = packet.ReadBit();
            var bit2C = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            var bit58 = !packet.ReadBit();
            var bitA0 = packet.ReadBit();
            if (bitA0)
            {
                bit94 = packet.ReadBit();
                packet.StartBitStream(guid2, 3, 4, 2);
                bit9C = packet.ReadBit();
                packet.StartBitStream(guid2, 5, 1, 6, 7, 0);
            }

            var hasExtraMovementFlags = !packet.ReadBit();
            guid1[3] = packet.ReadBit();
            var bit68 = !packet.ReadBit();
            var bitE4 = packet.ReadBit();
            var bit14 = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            var bitCD = packet.ReadBit();

            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            guid1[2] = packet.ReadBit();
            var bitC8 = !packet.ReadBit();
            var bit44 = packet.ReadBit();
            var bitCC = packet.ReadBit();
            var bit34 = packet.ReadBit();
            var bit3C = packet.ReadBit();
            var bitC4 = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var bitsD0 = (int)packet.ReadBits(22);
            guid1[6] = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            var bit104 = packet.ReadBit();
            if (bitC4)
                bitC0 = packet.ReadBit();

            var bits18 = (int)packet.ReadBits(19);
            for (var i = 0; i < bits18; ++i)
                packet.ReadBits("bitsED", 2, i);

            var bitE0 = !packet.ReadBit();
            if (bitA0)
            {
                packet.ReadXORByte(guid2, 2);
                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(guid2, 1);
                packet.ReadByte("Byte88");
                packet.ReadSingle("Float7C");
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(guid2, 7);
                packet.ReadSingle("Float80");
                packet.ReadSingle("Float78");
                if (bit94)
                    packet.ReadInt32("Int90");
                packet.ReadSingle("Float84");
                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid2, 0);
                packet.ReadInt32("Int8C");
                if (bit9C)
                    packet.ReadInt32("Int98");
                packet.ReadXORByte(guid2, 5);
                packet.WriteGuid("GuidE", guid2);
            }

            if (bit14)
                packet.ReadSingle("Float10");
            if (bitC4)
            {
                if (bitC0)
                {
                    packet.ReadSingle("FloatB4");
                    packet.ReadSingle("FloatBC");
                    packet.ReadSingle("FloatB8");
                }

                packet.ReadSingle("FloatB0");
                packet.ReadInt32("IntAC");
            }

            for (var i = 0; i < bits18; ++i)
            {
                packet.ReadSingle("FloatED", i);
                packet.ReadSingle("FloatED", i);
                packet.ReadSingle("FloatED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("Int1C", i);
                packet.ReadSingle("FloatED", i);
            }

            if (bit68)
                packet.ReadSingle("Float68");
            packet.ReadXORByte(guid1, 5);
            if (bit34)
                packet.ReadSingle("Float30");

            for (var i = 0; i < bitsD0; ++i)
                packet.ReadInt32("IntEA", i);

            packet.ReadSingle("Float5C");
            packet.ReadXORByte(guid1, 1);
            if (bitEC)
                packet.ReadSingle("FloatE8");
            if (bitF4)
                packet.ReadSingle("FloatF0");
            if (bit2C)
                packet.ReadSingle("Float28");
            packet.ReadXORByte(guid1, 3);
            if (bit3C)
                packet.ReadSingle("Float38");
            packet.ReadXORByte(guid1, 7);
            packet.ReadSingle("Float60");
            if (bitE0)
                packet.ReadInt32("IntE0");
            if (bit58)
                packet.ReadInt32("Int58");
            packet.ReadSingle("Float64");
            if (bitC8)
                packet.ReadSingle("FloatC8");
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 2);
            if (bitFC)
                packet.ReadSingle("FloatF8");
            if (bitA8)
                packet.ReadSingle("FloatA8");
            packet.ReadXORByte(guid1, 4);
            if (bit104)
                packet.ReadSingle("Float100");
            if (bit44)
                packet.ReadSingle("Float40");

            packet.WriteGuid("Guid9", guid1);
        }

        [Parser(Opcode.MSG_UNKNOWN_6127)]
        public static void HandleUnknown6127(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var guid = new byte[8];

                packet.StartBitStream(guid, 5, 3, 0, 6, 1, 7, 4, 2);
                packet.ParseBitStream(guid, 6, 7, 3, 0, 5, 4, 2, 1);

                packet.WriteGuid("Guid", guid);
            }
            else
            {
                var guid1 = new byte[8];
                var guid2 = new byte[8];

                var bit5C = false;
                var bit64 = false;
                var bit88 = false;

                var bitA8 = !packet.ReadBit();
                packet.StartBitStream(guid1, 1, 5);
                var bits98 = packet.ReadBits(22);
                packet.StartBitStream(guid1, 2, 4);
                var bit90 = !packet.ReadBit();
                var bit95 = packet.ReadBit();
                var bit70 = !packet.ReadBit();
                guid1[0] = packet.ReadBit();
                var bit30 = !packet.ReadBit();
                var bit20 = !packet.ReadBit();
                var bit94 = packet.ReadBit();
                packet.StartBitStream(guid1, 7, 6);
                var bitAC = packet.ReadBit();
                guid1[3] = packet.ReadBit();

                var bit68 = packet.ReadBit();
                if (bit68)
                {
                    packet.StartBitStream(guid2, 4, 2, 7, 6, 3);
                    bit64 = packet.ReadBit();
                    guid2[1] = packet.ReadBit();
                    bit5C = packet.ReadBit();
                    packet.StartBitStream(guid2, 0, 5);
                }

                var bit8C = packet.ReadBit();

                var hasExtraMovementFlags = !packet.ReadBit();
                if (hasExtraMovementFlags)
                    packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

                if (bit8C)
                    bit88 = packet.ReadBit();

                var hasMovementFlags = !packet.ReadBit();
                if (hasMovementFlags)
                    packet.ReadBitsE<MovementFlag>("Movement flags", 30);

                if (bit8C)
                {
                    packet.ReadInt32("Int74");

                    if (bit88)
                    {
                        packet.ReadSingle("Float80");
                        packet.ReadSingle("Float84");
                        packet.ReadSingle("Float7C");
                    }

                    packet.ReadSingle("Float78");
                }

                if (bit68)
                {
                    if (bit64)
                        packet.ReadInt32("Int60");

                    packet.ReadXORByte(guid2, 4);
                    packet.ReadSingle("Float4C");
                    packet.ReadSingle("Float44");
                    packet.ReadXORByte(guid2, 7);
                    packet.ReadXORByte(guid2, 3);
                    packet.ReadXORByte(guid2, 5);
                    packet.ReadSingle("Float40");
                    packet.ReadXORByte(guid2, 2);
                    packet.ReadSingle("Float48");
                    packet.ReadByte("Byte50");
                    packet.ReadXORByte(guid2, 6);
                    packet.ReadInt32("Int54");
                    if (bit5C)
                        packet.ReadInt32("Int58");
                    packet.ReadXORByte(guid2, 1);
                    packet.ReadXORByte(guid2, 0);
                    packet.WriteGuid("Guid2", guid2);
                }

                packet.ReadSingle("Float24");
                if (bit20)
                    packet.ReadInt32("Int20");
                if (bit70)
                    packet.ReadSingle("Float70");
                packet.ReadXORByte(guid1, 2);
                if (bit90)
                    packet.ReadSingle("Float90");
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid1, 0);
                packet.ReadXORByte(guid1, 1);
                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid1, 5);
                if (bitA8)
                    packet.ReadInt32("IntA8");
                packet.ReadSingle("Float28");
                if (bit30)
                    packet.ReadSingle("Float30");
                packet.ReadXORByte(guid1, 7);
                packet.ReadXORByte(guid1, 4);
                for (var i = 0; i < bits98; ++i)
                    packet.ReadInt32("IntEA", i);

                packet.ReadSingle("Float2C");

                packet.WriteGuid("Guid1", guid1);
            }
        }
    }
}
