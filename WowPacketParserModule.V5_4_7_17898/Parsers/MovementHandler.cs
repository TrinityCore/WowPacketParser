using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            var pos = new Vector4();

            pos.O = packet.ReadSingle();
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();

            packet.AddValue("Position", pos);
            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            var pos = new Vector4();

            packet.ReadInt32<MapId>("Map");
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.O = packet.ReadSingle();
            pos.X = packet.ReadSingle();

            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_BIND_POINT_UPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            var pos = new Vector3();

            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();

            packet.ReadInt32<AreaId>("Area Id");

            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_LOGIN_SET_TIME_SPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.ReadPackedTime("Game Time");
            packet.ReadSingle("Game Speed");
            packet.ReadInt32("Unk Int32");
            packet.ReadInt32("Unk Int32");

            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
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

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleMonsterMove(Packet packet)
        {
            var pos = new Vector3();

            var ownerGUID = new byte[8];
            var guid2 = new byte[8];
            var factingTargetGUID = new byte[8];

            packet.ReadSingle("Float30");
            packet.ReadInt32("Int28");
            packet.ReadSingle("Float34");
            packet.ReadSingle("Float2C");
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            ownerGUID[3] = packet.ReadBit();
            var bit40 = !packet.ReadBit();
            ownerGUID[6] = packet.ReadBit();
            var bit45 = !packet.ReadBit();
            var bit6D = !packet.ReadBit();
            var splineType = packet.ReadBits(3);
            var bit78 = !packet.ReadBit();
            ownerGUID[2] = packet.ReadBit();
            ownerGUID[7] = packet.ReadBit();
            ownerGUID[5] = packet.ReadBit();

            if (splineType == 3)
                packet.StartBitStream(factingTargetGUID, 6, 7, 0, 5, 2, 3, 4, 1);

            var bit58 = !packet.ReadBit();
            ownerGUID[4] = packet.ReadBit();
            var waypointCount = packet.ReadBits(22);
            var bit4C = !packet.ReadBit();
            packet.ReadBit(); // fake bit
            ownerGUID[0] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            var bit6C = !packet.ReadBit();
            var bit54 = !packet.ReadBit();
            var bit48 = !packet.ReadBit();
            var splineCount = (int)packet.ReadBits(20);
            ownerGUID[1] = packet.ReadBit();
            var bitB0 = packet.ReadBit();

            var bits8C = 0u;
            if (bitB0)
            {
                bits8C = packet.ReadBits(22);
                packet.ReadBits("bits9C", 2);
            }

            var bit38 = packet.ReadBit();
            var bit50 = !packet.ReadBit();
            if (splineType == 3)
            {
                packet.ParseBitStream(factingTargetGUID, 5, 3, 6, 1, 4, 2, 0, 7);
                packet.WriteGuid("Facting Target GUID", factingTargetGUID);
            }

            packet.ReadXORByte(ownerGUID, 3);

            packet.ParseBitStream(guid2, 7, 3, 2, 0, 6, 4, 5, 1);


            if (bitB0)
            {
                packet.ReadSingle("FloatA0");

                for (var i = 0; i < bits8C; ++i)
                {
                    packet.ReadInt16("short74+2", i);
                    packet.ReadInt16("short74+0", i);
                }

                packet.ReadSingle("FloatA8");
                packet.ReadInt16("IntA4");
                packet.ReadInt16("IntAC");
            }

            if (bit6D)
                packet.ReadByte("Byte6D");

            if (splineType == 4)
                packet.ReadSingle("Facing Angle");

            if (bit40)
                packet.ReadInt32("Int40");

            packet.ReadXORByte(ownerGUID, 7);
            if (bit78)
                packet.ReadByte("Byte78");
            if (bit4C)
                packet.ReadInt32("Int4C");
            if (bit45)
                packet.ReadByte("Byte45");

            var waypoints = new Vector3[waypointCount];
            for (var i = 0; i < waypointCount; ++i)
            {
                var vec = packet.ReadPackedVector3();
                waypoints[i].X = vec.X;
                waypoints[i].Y = vec.Y;
                waypoints[i].Z = vec.Z;
            }

            packet.ReadXORByte(ownerGUID, 5);
            packet.ReadXORByte(ownerGUID, 1);
            packet.ReadXORByte(ownerGUID, 2);

            if (bit48)
                packet.ReadInt32("Int48");

            Vector3 endpos = new Vector3();
            for (var i = 0; i < splineCount; ++i)
            {
                var spot = new Vector3
                {
                    X = packet.ReadSingle(),
                    Y = packet.ReadSingle(),
                    Z = packet.ReadSingle()
                };
                // client always taking first point
                if (i == 0)
                {
                    endpos = spot;
                }

                packet.AddValue("Spline Waypoint", spot, i);
            }

            packet.ReadXORByte(ownerGUID, 6);

            if (bit50)
                packet.ReadInt32("Int50");

            if (splineType == 2)
            {
                packet.ReadSingle("FloatC0");
                packet.ReadSingle("FloatC4");
                packet.ReadSingle("FloatC8");
            }

            if (bit54)
                packet.ReadSingle("Float54");

            if (bit6C)
                packet.ReadByte("Byte6C");

            packet.ReadXORByte(ownerGUID, 0);

            if (bit58)
                packet.ReadInt32("Int58");

            packet.ReadXORByte(ownerGUID, 4);

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
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE)]
        public static void HandlePlayerMove(Packet packet)
        {
            var pos = new Vector4();
            var guid = new byte[8];
            var transportGUID = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            var hasMovementFlags = !packet.ReadBit();
            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            var hasSplineElevation = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var bit95 = packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            var hasExtraMovementFlags = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();

            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            guid[7] = packet.ReadBit();
            var counter = (int)packet.ReadBits(22);
            guid[2] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var hasTrans = packet.ReadBit();
            if (hasTrans)
            {
                transportGUID[6] = packet.ReadBit();
                transportGUID[3] = packet.ReadBit();
                transportGUID[0] = packet.ReadBit();
                transportGUID[5] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGUID[7] = packet.ReadBit();
                transportGUID[2] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGUID[1] = packet.ReadBit();
                transportGUID[4] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            var bit94 = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasTimeStamp = !packet.ReadBit();
            if (hasTrans)
            {
                var transPos = new Vector4();

                if (hasTransportTime2)
                    packet.ReadInt32("Transport Time 2");

                transPos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGUID, 6);
                transPos.O = packet.ReadSingle();
                transPos.Z = packet.ReadSingle();

                if (hasTransportTime3)
                    packet.ReadInt32("Transport Time 3");

                packet.ReadXORByte(transportGUID, 0);
                packet.ReadInt32("Transport Time");
                packet.ReadXORByte(transportGUID, 7);
                packet.ReadXORByte(transportGUID, 2);
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGUID, 1);
                packet.ReadXORByte(transportGUID, 3);
                packet.ReadXORByte(transportGUID, 5);
                transPos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGUID, 4);

                packet.WriteGuid("Transport Guid", transportGUID);
                packet.AddValue("Transport Position", transPos);
            }

            packet.ReadXORByte(guid, 3);
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (bitA8)
                packet.ReadInt32("IntA8");
            packet.ReadXORByte(guid, 6);
            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 4);

            for (var i = 0; i < counter; ++i)
                packet.ReadInt32("IntEA", i);

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadSingle("Velocity Speed");
                packet.ReadInt32("Fall Time");
            }

            pos.Y = packet.ReadSingle();

            if (hasSplineElevation)
                packet.ReadSingle("Spline Elevation");
            if (hasTimeStamp)
                packet.ReadInt32("Timestamp");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            pos.Z = packet.ReadSingle();
            packet.ReadXORByte(guid, 7);
            if (hasOrientation)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            CoreParsers.MovementHandler.ActivePhases.Clear();

            packet.ReadUInt32("UInt32 1");

            var count = packet.ReadUInt32() / 2;
            packet.AddValue("Inactive Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadInt16<MapId>("Inactive Terrain swap", i);

            count = packet.ReadUInt32() / 2;
            packet.AddValue("WorldMapArea swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("WorldMapArea swap", i);

            count = packet.ReadUInt32() / 2;
            packet.AddValue("Phases count", count);
            for (var i = 0; i < count; ++i)
                CoreParsers.MovementHandler.ActivePhases.Add(packet.ReadUInt16("Phase id", i)); // Phase.dbc

            count = packet.ReadUInt32() / 2;
            packet.AddValue("Active Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadInt16<MapId>("Active Terrain swap", i);

            var guid = packet.StartBitStream(4, 6, 1, 7, 2, 0, 5, 3);
            packet.ParseBitStream(guid, 0, 4, 7, 6, 3, 5, 1, 2);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 0, 4, 3, 5, 6, 2, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadSingle("Duration modifier");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.MSG_MOVE_SET_FACING)]
        public static void HandleMoveSetFacing(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[4] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            var bit95 = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasExtraMovementFlags = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bit94 = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var bits98 = packet.ReadBits(22);
            guid[5] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            if (hasTransportData)
            {
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.ReadXORByte(guid, 2);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);

            if (hasTransportData)
            {
                var transPos = new Vector4();

                packet.ReadXORByte(transportGuid, 4);
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 2);

                if (hasTransportTime2)
                    packet.ReadInt32("Transport Time 2");

                transPos.O = packet.ReadSingle();
                transPos.Z = packet.ReadSingle();

                if (hasTransportTime3)
                    packet.ReadInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 3);
                packet.ReadInt32("Transport Time");
                transPos.X = packet.ReadSingle();
                transPos.Y = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", transPos);
            }

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

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            packet.AddValue("Position", pos);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_SET_PITCH)]
        public static void HandleMoveSetPitch434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var hasTimestamp = !packet.ReadBit();
            var hasExtraMovementFlags = !packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            guid[4] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var bit95 = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            var bit94 = packet.ReadBit();

            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasTransportData)
            {
                hasTransportTime3 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);

            if (hasTransportData)
            {
                var transPos = new Vector4();

                packet.ReadXORByte(transportGuid, 2);
                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 7);
                transPos.Z = packet.ReadSingle();
                transPos.Y = packet.ReadSingle();
                transPos.X = packet.ReadSingle();
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGuid, 1);
                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");
                packet.ReadXORByte(transportGuid, 4);
                transPos.O = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", transPos);
            }

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

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_FORWARD)]
        public static void HandleMoveStartForward(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            var hasOrientation = !packet.ReadBit();
            var bit94 = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasMovementFlag = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bit95 = packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            guid[1] = packet.ReadBit();
            if (hasTransportData)
            {
                transportGuid[7] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlag)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 5);
                tpos.Z = packet.ReadSingle();
                if (hasTransportTime3)
                    packet.ReadInt32("Transport Time 3");
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 4);
                if (hasTransportTime2)
                    packet.ReadInt32("Transport Time 2");
                packet.ReadInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 0);
                tpos.O = packet.ReadSingle();
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 1);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasOrientation)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");
            if (bitA8)
                packet.ReadInt32("IntA8");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_BACKWARD)]
        public static void HandleMoveStartBackward434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            var bits98 = (int)packet.ReadBits(22);
            var hasMovementFlags2 = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            var bit95 = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var bit94 = packet.ReadBit();
            var hasMovementFlag = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            if (hasTransportData)
            {
                transportGuid[3] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlag)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 7);
                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadSByte("Seat");
                packet.ReadXORByte(transportGuid, 4);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 1);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 6);
                tpos.X = packet.ReadSingle();
                packet.ReadInt32("Transport Time");

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasFallData)
            {
                packet.ReadInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasOrientation)
                pos.O = packet.ReadSingle();
            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");
            if (bitA8)
                packet.ReadInt32("IntA8");
            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);

        }

        [Parser(Opcode.MSG_MOVE_START_TURN_LEFT)]
        public static void HandleMoveStartTurnLeft(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[6] = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var bit95 = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var hasTransportDat = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            var bit94 = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            if (hasFallData)
                hasFallDirection = packet.ReadBit();
            if (hasTransportDat)
            {
                hasTransportTime2 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);

            if (hasTransportDat)
            {
                var tpos = new Vector4();

                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                tpos.Y = packet.ReadSingle();

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 7);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 3);
                tpos.Z = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadSByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadUInt32("Transport Time");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_TURN_RIGHT)]
        public static void HandleMoveStartTurnRight434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var bitA8 = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var hasMovementFlag = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bits98 = packet.ReadBits(22);
            var bit94 = packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            var bit95 = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();

            if (hasTransportData)
            {
                transportGuid[5] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlag)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasTransportData)
            {
                var tpos = new Vector4();
                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");
                packet.ReadXORByte(transportGuid, 6);
                tpos.Y = packet.ReadSingle();
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadUInt32("Transport Time");
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 3);
                tpos.O = packet.ReadSingle();
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 0);
                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 5);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (bitA8)
                packet.ReadInt32("IntA8");
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }


        [Parser(Opcode.MSG_MOVE_STOP_TURN)]
        public static void HandleMoveStopTurn434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[3] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            var bit95 = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var hasMovementFlags = !packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bit94 = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            if (hasTransportData)
            {
                transportGuid[5] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();
            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);
            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            packet.ReadXORByte(guid, 1);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 1);
                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");
                tpos.Z = packet.ReadSingle();
                tpos.Y = packet.ReadSingle();
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 2);
                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");
                packet.ReadUInt32("Transport Time");
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 0);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasSplineElevation)
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
                packet.ReadInt32("Fall time");
            }

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_STRAFE_LEFT)]
        public static void HandleMoveStartStrafeLeft434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            var bitA8 = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            var bitAC = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            guid[5] = packet.ReadBit();
            var bit94 = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var bit95 = packet.ReadBit();
            var hHasSplineElevation = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            guid[3] = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasTransportData)
            {
                transportGuid[2] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 0);

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 7);

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadUInt32("Transport Time");
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 5);
                tpos.Z = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 0);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadByte("Seat");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadInt32("Fall time");
            }

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hHasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_STRAFE_RIGHT)]
        public static void HandleMoveStartStrafeRight434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var hasTransportData = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var hasTimestamp = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var bit94 = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            var bit95 = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[1] = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasTransportData)
            {
                hasTransportTime3 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 1);
                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");
                packet.ReadByte("Seat");
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                tpos.Z = packet.ReadSingle();
                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 3);
                tpos.X = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 5);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadInt32("Fall time");
            }

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP_STRAFE)]
        public static void HandleMoveStopStrafe434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var bit95 = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var bit94 = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            guid[3] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            if (hasTransportData)
            {
                hasTransportTime2 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 2);
                tpos.X = packet.ReadSingle();

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 4);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGuid, 6);
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 0);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadInt32("Fall time");
            }

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (bitA8)
                packet.ReadInt32("IntA8");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_SWIM)]
        public static void HandleMoveStartSwim434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            guid[3] = packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var bit95 = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var bit94 = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid[1] = packet.ReadBit();

            if (hasTransportData)
            {
                transportGuid[5] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 7);
                tpos.Z = packet.ReadSingle();
                tpos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                tpos.X = packet.ReadSingle();

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadUInt32("Transport Time");

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 3);
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 5);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 6);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadInt32("Fall time");
            }

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);

        }

        [Parser(Opcode.MSG_MOVE_STOP_SWIM)]
        public static void HandleMoveStopSwim434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;

            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            var bit94 = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var hasTransportDat = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var bit95 = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var hasMovementFlags2 = !packet.ReadBit();
            var hasMovementFlag = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();

            if (hasMovementFlag)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasTransportDat)
            {
                transportGuid[0] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransTime2 = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                hasTransTime3 = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadInt32("Fall time");
            }

            if (hasTransportDat)
            {
                var tpos = new Vector4();

                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 5);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);
                tpos.Z = packet.ReadSingle();
                packet.ReadInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 1);

                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 7);
                tpos.Z = packet.ReadSingle();
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGuid, 3);
                tpos.O = packet.ReadSingle();

                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hasTimestamp)
                packet.ReadInt32("Timestamp");

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_ASCEND)]
        public static void HandleMoveStartAscend434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            guid[7] = packet.ReadBit();
            var hasMovementFlag = !packet.ReadBit();
            var bit94 = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var bit95 = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            guid[0] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasTransportData)
            {
                hasTransportTime3 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasMovementFlag)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            if (hasFallData)
            {
                packet.ReadInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical Speed");
            }

            if (hasTransportData)
            {
                var tpos = new Vector4();

                tpos.Z = packet.ReadSingle();
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 4);
                tpos.X = packet.ReadSingle();

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 3);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                tpos.O = packet.ReadSingle();
                tpos.Y = packet.ReadSingle();
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 2);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP_ASCEND)]
        public static void HandleMoveStopAscend434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var hasPitch = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bit94 = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var bit95 = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasMovementFlag = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var hasSplineElevation = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();

            if (hasTransportData)
            {
                transportGuid[2] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlag)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 5);

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasTransportData)
            {
                var tpos = new Vector4();

                tpos.O = packet.ReadSingle();
                tpos.Z = packet.ReadSingle();

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadByte("Seat");
                packet.ReadInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 6);
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 5);
                tpos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 7);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 1);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (bitA8)
                packet.ReadInt32("IntA8");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.MSG_MOVE_JUMP)]
        public static void HandleMoveJump(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            var hasPitch = !packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var bitAC = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bit95 = packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            var bit94 = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasFallData = packet.ReadBit();

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasTransportData)
            {
                transportGuid[5] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 5);

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.X = packet.ReadSingle();
                packet.ReadSByte("Seat");
                tpos.O = packet.ReadSingle();
                tpos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 2);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadUInt32("Transport Time");

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (bitA8)
                packet.ReadInt32("IntA8");

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);

        }

        [Parser(Opcode.MSG_MOVE_FALL_LAND)]
        public static void HandleMoveFallLand434(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            var hasTimestamp = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var bit95 = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var bitAC = packet.ReadBit();
            var bit94 = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasTransportData)
            {
                transportGuid[2] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 3);
            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal Speed");
                }

                packet.ReadInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasTransportData)
            {
                var transPos = new Vector4();

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");
                packet.ReadXORByte(transportGuid, 5);
                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 1);
                transPos.O = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 6);
                transPos.Y = packet.ReadSingle();
                packet.ReadByte("Seat");
                packet.ReadXORByte(transportGuid, 0);
                transPos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                transPos.X = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", transPos);
            }

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (bitA8)
                packet.ReadInt32("IntA8");

            packet.WriteGuid("Guid", guid);

        }

        [Parser(Opcode.MSG_MOVE_HEARTBEAT)]
        public static void HandleMoveHeartbeat(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();

            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasUnkTime = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var hasMovementFlagExtra = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var hasMovementFlag = !packet.ReadBit();
            var bit94 = packet.ReadBit();
            var bit95 = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasTimestamp = !packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var count = packet.ReadBits(22);
            var hasPitch = !packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            if (hasTransportData)
            {
                transportGuid[7] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
            }

            if (hasMovementFlagExtra)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlag)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);

            for (var i = 0; i < count; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 2);

            if (hasUnkTime)
                packet.ReadInt32("unkTime");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadInt32("Fall time");
                packet.ReadSingle("Vertical Speed");
            }

            if (hasTransportData)
            {
                var tpos = new Vector4();

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadXORByte(transportGuid, 3);
                packet.ReadUInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 1);
                tpos.Z = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 2);

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                tpos.O = packet.ReadSingle();
                tpos.Y = packet.ReadSingle();
                tpos.X = packet.ReadSingle();

                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 5);

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.AddValue("Position", pos);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_STOP)]
        public static void HandleMoveStop(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFallDirection = false;

            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            guid[1] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var hasMovementFlags2 = !packet.ReadBit();
            var bitAC = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var bit95 = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasTime = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var bit94 = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var hasTrans = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid[7] = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasTrans)
            {
                transportGuid[3] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
            }

            if (hasMovementFlags2)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);

            if (hasTrans)
            {
                var transPos = new Vector4();

                packet.ReadXORByte(transportGuid, 2);
                packet.ReadInt32("Transport Time");
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 1);

                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3");

                packet.ReadByte("Transport Seat");
                transPos.Y = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 6);

                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2");

                packet.ReadXORByte(transportGuid, 3);
                transPos.O =packet.ReadSingle();
                transPos.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 4);
                transPos.Z = packet.ReadSingle();

                packet.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", transPos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadSingle("Vertical Speed");
                packet.ReadInt32("Fall time");
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp");
            if (hasOrientation)
                pos.O = packet.ReadSingle();
            if (hasPitch)
                packet.ReadSingle("Pitch");
            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");
            if (bitA8)
                packet.ReadInt32("IntA8");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_SPEED)]
        public static void HandleMoveSetFlightSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(2, 1, 6, 5, 7, 3, 0, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Unk Int32");
            packet.ReadXORByte(guid, 5);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_SWIM_SPEED)]
        public static void HandleMoveSetSwimSpeed(Packet packet)
        {
            packet.ReadSingle("Speed");
            packet.ReadInt32("Unk Int32");
            var guid = packet.StartBitStream(4, 7, 6, 3, 5, 2, 0, 1);
            packet.ParseBitStream(guid, 1, 6, 5, 2, 0, 3, 4, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_WALK_SPEED)]
        public static void HandleMoveSetWalkSpeed(Packet packet)
        {
            var guid = new byte[8];

            guid[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Unk Int32");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_CAN_FLY)]
        public static void HandleSetCanFly(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 2, 3, 0, 5, 1, 7, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Movement Counter");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_FLY)]
        public static void HandleUnsetCanFly(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 3, 2, 4, 7, 1, 0, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Movement Counter");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER)]
        public static void HandleSetActiveMover(Packet packet)
        {
            packet.ReadBit("unk");

            var guid = packet.StartBitStream(1, 3, 2, 6, 7, 5, 4, 0);
            packet.ParseBitStream(guid, 5, 1, 7, 2, 6, 3, 4, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_ROOT)]
        public static void HandleSplineMoveRoot(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 3, 5, 6, 2, 1, 0, 7);
            packet.ParseBitStream(guid, 2, 0, 3, 7, 4, 1, 5, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_UNROOT)]
        public static void HandleSplineMoveUnroot(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 4, 6, 2, 0, 1, 3, 5);
            packet.ParseBitStream(guid, 4, 2, 6, 0, 5, 3, 7, 1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_RUN_SPEED)]
        public static void HandleSplineSetRunSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 7, 3, 5, 4, 1, 6, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_SPEED)]
        public static void HandleSplineSetFlightSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 7, 0, 5, 6, 3, 4, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_SWIM_SPEED)]
        public static void HandleSplineSetSwimSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 4, 5, 0, 3, 2, 7, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_WALK_BACK_SPEED)]
        public static void HandleSplineSetWalkSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 7, 5, 4, 3, 0, 1, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_RUN_BACK_SPEED)]
        public static void HandleSplineSetRunBackSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 0, 7, 2, 6, 1, 4, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_ROOT)]
        public static void HandleMoveRoot(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 3, 6, 0, 1, 4, 2, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Unk Int32");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED)]
        public static void HandleMoveTimeSkipped(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Time");

            packet.StartBitStream(guid, 5, 7, 6, 2, 0, 1, 4, 3);
            packet.ParseBitStream(guid, 4, 6, 5, 0, 3, 2, 7, 1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_RUN_SPEED)]
        public static void HandleMoveUpdateRunSpeed(Packet packet)
        {
            var pos = new Vector4();

            var guid = new byte[8];
            var transportGuid = new byte[8];

            var hasFallDirection = false;
            var bit6C = false;
            var bit64 = false;

            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var bit98 = !packet.ReadBit();
            var hasTime = !packet.ReadBit();
            var bitB0 = !packet.ReadBit();
            var bitsA0 = (int)packet.ReadBits(22);
            var hasMovementFlagsExtra = !packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[3] = packet.ReadBit();
            var bit9D = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bit9C = packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            guid[2] = packet.ReadBit();
            var bitB4 = packet.ReadBit();
            var bit78 = !packet.ReadBit();

            var bit94 = packet.ReadBit();
            if (bit94)
                hasFallDirection = packet.ReadBit();

            guid[6] = packet.ReadBit();

            var hasTransport = packet.ReadBit();
            if (hasTransport)
            {
                transportGuid[1] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                bit6C = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                bit64 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
            }

            var hasMovementFlags = !packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            if (bit78)
                packet.ReadSingle("Float78");

            if (bit94)
            {
                packet.ReadSingle("Vertical speed");
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
            }

            packet.ReadXORByte(guid, 0);

            if (hasTransport)
            {
                if (bit6C)
                    packet.ReadInt32("Int68");
                packet.ReadInt32("Int5C");
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadByte("Transport Seat");
                packet.ReadSingle("Float4C");
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadSingle("Float54");
                packet.ReadSingle("Float48");
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadSingle("Float50");

                packet.WriteGuid("Transport Guid", transportGuid);
            }

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);

            for (int i = 0; i < bitsA0; ++i)
                packet.ReadInt32("IntEA", i);

            if (hasO)
                pos.O = packet.ReadSingle();
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            pos.Z = packet.ReadSingle();
            packet.ReadXORByte(guid, 3);
            if (hasTime)
                packet.ReadInt32("Timestamp");
            pos.X = packet.ReadSingle();
            if (bitB0)
                packet.ReadInt32("IntB0");
            if (bit98)
                packet.ReadSingle("Float98");
            pos.Y = packet.ReadSingle();
            packet.ReadXORByte(guid, 2);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_COLLISION_HEIGHT)]
        public static void HandleMoveUpdateCollisionHeight(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var pos = new Vector4();

            var bit5C = false;
            var bit64 = false;
            var hasFallDirection = false;

            var bit94 = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            var hasFallData = packet.ReadBit();
            var bit70 = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();

            var hasTransport = packet.ReadBit();
            if (hasTransport)
            {
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                bit5C = packet.ReadBit();
                bit64 = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
            }

            var hasTime = !packet.ReadBit();

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            guid[7] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var bit95 = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            guid[0] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var bit90 = !packet.ReadBit();
            if (hasTransport)
            {
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadSingle("Float48");
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadInt32("Int54");
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadByte("Transport Seat");
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 3);
                if (bit5C)
                    packet.ReadInt32("Int58");
                packet.ReadSingle("Float40");
                packet.ReadSingle("Float4C");
                packet.ReadSingle("Float44");
                packet.ReadXORByte(transportGuid, 6);

                packet.WriteGuid("Transport Guid", transportGuid);
            }

            packet.ReadSingle("FloatB0");
            if (hasFallData)
            {
                packet.ReadSingle("Vertical speed");
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal speed");
                }

                packet.ReadInt32("Fall time");
            }

            if (bitA8)
                packet.ReadInt32("IntA8");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            if (hasTime)
                packet.ReadInt32("Timestamp");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadSingle("FloatB4");
            if (hasO)
                pos.O = packet.ReadSingle();
            packet.ReadXORByte(guid, 0);
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            if (bit90)
                packet.ReadSingle("Float90");
            pos.X = packet.ReadSingle();
            if (bit70)
                packet.ReadSingle("Float70");

            for (int i = 0; i < bits98; ++i)
                packet.ReadInt32("IntEA", i);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_RUN_BACK_SPEED)]
        public static void HandleMoveUpdateRunBackSpeed(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var pos = new Vector4();

            var bit5C = false;
            var bit64 = false;
            var hasFallDirection = false;

            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasTime = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasTransport = packet.ReadBit();
            if (hasTransport)
            {
                bit64 = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                bit5C = packet.ReadBit();
            }

            guid[1] = packet.ReadBit();
            var bitAC = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var bit95 = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var bit94 = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            guid[0] = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            var bit90 = !packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            var bit70 = !packet.ReadBit();
            if (hasFallData)
                hasFallDirection = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);
            guid[7] = packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical speed");
            }

            if (bitA8)
                packet.ReadInt32("IntA8");
            if (hasTransport)
            {
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 3);
                if (bit5C)
                    packet.ReadInt32("Int58");
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadSingle("Float4C");
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadSingle("Float44");
                packet.ReadSingle("Float48");
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadInt32("Int54");
                packet.ReadSingle("Float40");
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadByte("Byte50");
                packet.ReadXORByte(transportGuid, 2);
                packet.WriteGuid("Guid7", transportGuid);
            }

            if (hasO)
                pos.O = packet.ReadSingle();
            packet.ReadXORByte(guid, 7);
            packet.ReadSingle("Speed");

            for (int i = 0; i < bits98; ++i)
                packet.ReadInt32("IntEA", i);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 3);
            pos.Z = packet.ReadSingle();

            if (hasTime)
                packet.ReadInt32("Timestamp");

            packet.ReadXORByte(guid, 5);
            if (bit70)
                packet.ReadSingle("Float70");
            packet.ReadXORByte(guid, 1);
            if (bit90)
                packet.ReadSingle("Float90");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_SWIM_SPEED)]
        public static void HandleMoveUpdateSwimSpeed(Packet packet)
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
            packet.ReadSingle("Speed");
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bit70 = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var bits98 = packet.ReadBits(22);
            var bit94 = packet.ReadBit();
            var bit95 = packet.ReadBit();
            var hasTransport = packet.ReadBit();
            guid[4] = packet.ReadBit();
            if (hasTransport)
            {
                transportGuid[0] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                bit64 = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                bit5C = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var bitA8 = !packet.ReadBit();
            var hasO = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var bitAC = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            var hasTime = !packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            var bit90 = !packet.ReadBit();
            if (hasTransport)
            {
                packet.ReadXORByte(transportGuid, 4);
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadSingle("Float44");
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadSingle("Float40");
                if (bit5C)
                    packet.ReadInt32("Int58");
                packet.ReadSingle("Float48");
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadSingle("Float4C");
                packet.ReadByte("Transport Seat");
                packet.ReadInt32("Int54");
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 6);
                packet.WriteGuid("Guid7", transportGuid);
            }

            if (hasO)
                pos.O = packet.ReadSingle();

            if (bit90)
                packet.ReadSingle("Float90");

            packet.ReadXORByte(guid, 7);
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

            for (int i = 0; i < bits98; ++i)
                packet.ReadInt32("IntEA", i);

            packet.ReadXORByte(guid, 2);
            if (bit70)
                packet.ReadSingle("Float70");
            if (bitA8)
                packet.ReadInt32("IntA8");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            if (hasTime)
                packet.ReadInt32("Timestamp");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_FLIGHT_SPEED)]
        public static void HandleMoveUpdateFlightSpeed(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var pos = new Vector4();

            var bit64 = false;
            var bit6C = false;
            var hasFallDirection = false;

            var hasTransport = packet.ReadBit();
            if (hasTransport)
            {
                bit6C = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                bit64 = packet.ReadBit();
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
            }

            guid[3] = packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bit9C = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasTime = !packet.ReadBit();
            var bitsA0 = (int)packet.ReadBits(22);
            guid[4] = packet.ReadBit();
            var bitB0 = !packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            var bit9D = packet.ReadBit();
            if (hasFallData)
                hasFallDirection = packet.ReadBit();
            guid[7] = packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            var bit78 = !packet.ReadBit();
            var bitB4 = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bit98 = !packet.ReadBit();
            if (hasTransport)
            {
                packet.ReadXORByte(transportGuid, 2);
                if (bit6C)
                    packet.ReadInt32("Int68");
                packet.ReadInt32("Int5C");
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadSingle("Float50");
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadSingle("Float4C");
                packet.ReadSingle("Float48");
                packet.ReadByte("Byte58");
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 6);
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadSingle("Float54");
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadXORByte(transportGuid, 4);
                packet.WriteGuid("Guid8", transportGuid);
            }

            packet.ReadXORByte(guid, 0);
            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadUInt32("Fall time");
                packet.ReadSingle("Vertical speed");
            }

            packet.ReadXORByte(guid, 6);
            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 4);
            pos.Y = packet.ReadSingle();

            for (int i = 0; i < bitsA0; ++i)
                packet.ReadInt32("IntEA", i);

            packet.ReadXORByte(guid, 1);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 7);
            if (bit78)
                packet.ReadSingle("Float78");
            if (hasTime)
                packet.ReadInt32("Timestamp");
            packet.ReadXORByte(guid, 2);
            pos.Z = packet.ReadSingle();
            if (hasO)
                pos.O = packet.ReadSingle();
            packet.ReadXORByte(guid, 5);
            if (bit98)
                packet.ReadSingle("Float98");
            if (bitB0)
                packet.ReadInt32("IntB0");
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_WALK_SPEED)]
        public static void HandleMoveUpdateWalkSpeed434(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var pos = new Vector4();

            var bit64 = false;
            var bit6C = false;
            var hasFallDirection = false;

            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var bit98 = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bitsA0 = (int)packet.ReadBits(22);
            var bit9C = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasFallData = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var hasTime = !packet.ReadBit();
            var bitB4 = packet.ReadBit();
            var bit78 = !packet.ReadBit();
            var bit9D = packet.ReadBit();
            if (hasFallData)
                hasFallDirection = packet.ReadBit();
            var hasO = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hasMovementFlagsExtra = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var hasTransport = packet.ReadBit();
            if (hasTransport)
            {
                transportGuid[1] = packet.ReadBit();
                transportGuid[0] = packet.ReadBit();
                transportGuid[4] = packet.ReadBit();
                transportGuid[3] = packet.ReadBit();
                transportGuid[7] = packet.ReadBit();
                transportGuid[5] = packet.ReadBit();
                transportGuid[2] = packet.ReadBit();
                bit6C = packet.ReadBit();
                transportGuid[6] = packet.ReadBit();
                bit64 = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement flags", 30);

            var bitB0 = !packet.ReadBit();

            if (hasMovementFlagsExtra)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            guid[1] = packet.ReadBit();
            if (hasTransport)
            {
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadSingle("Float48");
                if (bit6C)
                    packet.ReadInt32("Int68");
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadSingle("Float50");
                packet.ReadSingle("Float4C");
                packet.ReadXORByte(transportGuid, 7);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 4);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadByte("Byte58");
                packet.ReadSingle("Float54");
                packet.ReadXORByte(transportGuid, 1);
                if (bit64)
                    packet.ReadInt32("Int60");
                packet.ReadInt32("Int5C");
                packet.WriteGuid("Guid8", transportGuid);
            }

            packet.ReadSingle("Speed");
            if (hasO)
                pos.O = packet.ReadSingle();
            if (hasTime)
                packet.ReadUInt32("Timestamp");
            packet.ReadXORByte(guid, 5);
            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 2);
            if (bit98)
                packet.ReadSingle("Float98");
            packet.ReadXORByte(guid, 0);
            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Horizontal speed");
                    packet.ReadSingle("Fall Sin");
                }

                packet.ReadSingle("Vertical speed");
                packet.ReadInt32("Fall time");
            }

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            if (bit78)
                packet.ReadSingle("Float78");

            for (int i = 0; i < bitsA0; ++i)
                packet.ReadInt32("IntEA", i);

            pos.Z = packet.ReadSingle();
            packet.ReadXORByte(guid, 3);
            if (bitB0)
                packet.ReadInt32("IntB0");
            packet.ReadXORByte(guid, 4);
            pos.Y = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_SET_PLAY_HOVER_ANIM)]
        public static void HandlePlayHoverAnim(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.ReadBit();
            packet.ReadBit("Unk");
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ParseBitStream(guid, 4, 3, 2, 7, 1, 6, 5, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_TELEPORT_ACK)]
        public static void HandleMoveTeleportAck434(Packet packet)
        {
            packet.ReadInt32("Time");
            packet.ReadInt32("Flags");
            var guid = packet.StartBitStream(0, 7, 6, 2, 5, 1, 4, 3);
            packet.ParseBitStream(guid, 5, 4, 0, 1, 3, 7, 6, 2);
            packet.WriteGuid("Guid", guid);
        }
    }
}
