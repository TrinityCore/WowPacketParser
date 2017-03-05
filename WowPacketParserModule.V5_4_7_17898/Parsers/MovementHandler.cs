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

            pos.O = packet.Translator.ReadSingle();
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.Translator.ReadInt32<MapId>("Map");
            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();

            packet.AddValue("Position", pos);
            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            var pos = new Vector4();

            packet.Translator.ReadInt32<MapId>("Map");
            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.O = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();

            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_BIND_POINT_UPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            var pos = new Vector3();

            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.Translator.ReadInt32<MapId>("Map");

            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();

            packet.Translator.ReadInt32<AreaId>("Area Id");

            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_LOGIN_SET_TIME_SPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.Translator.ReadPackedTime("Game Time");
            packet.Translator.ReadSingle("Game Speed");
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadInt32("Unk Int32");

            packet.Translator.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("Map ID");

            var customLoadScreenSpell = packet.Translator.ReadBit();
            var hasTransport = packet.Translator.ReadBit();

            if (hasTransport)
            {
                packet.Translator.ReadInt32<MapId>("Transport Map ID");
                packet.Translator.ReadInt32("Transport Entry");
            }

            if (customLoadScreenSpell)
                packet.Translator.ReadUInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleMonsterMove(Packet packet)
        {
            var pos = new Vector3();

            var ownerGUID = new byte[8];
            var guid2 = new byte[8];
            var factingTargetGUID = new byte[8];

            packet.Translator.ReadSingle("Float30");
            packet.Translator.ReadInt32("Int28");
            packet.Translator.ReadSingle("Float34");
            packet.Translator.ReadSingle("Float2C");
            pos.X = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            ownerGUID[3] = packet.Translator.ReadBit();
            var bit40 = !packet.Translator.ReadBit();
            ownerGUID[6] = packet.Translator.ReadBit();
            var bit45 = !packet.Translator.ReadBit();
            var bit6D = !packet.Translator.ReadBit();
            var splineType = packet.Translator.ReadBits(3);
            var bit78 = !packet.Translator.ReadBit();
            ownerGUID[2] = packet.Translator.ReadBit();
            ownerGUID[7] = packet.Translator.ReadBit();
            ownerGUID[5] = packet.Translator.ReadBit();

            if (splineType == 3)
                packet.Translator.StartBitStream(factingTargetGUID, 6, 7, 0, 5, 2, 3, 4, 1);

            var bit58 = !packet.Translator.ReadBit();
            ownerGUID[4] = packet.Translator.ReadBit();
            var waypointCount = packet.Translator.ReadBits(22);
            var bit4C = !packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit
            ownerGUID[0] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            var bit6C = !packet.Translator.ReadBit();
            var bit54 = !packet.Translator.ReadBit();
            var bit48 = !packet.Translator.ReadBit();
            var splineCount = (int)packet.Translator.ReadBits(20);
            ownerGUID[1] = packet.Translator.ReadBit();
            var bitB0 = packet.Translator.ReadBit();

            var bits8C = 0u;
            if (bitB0)
            {
                bits8C = packet.Translator.ReadBits(22);
                packet.Translator.ReadBits("bits9C", 2);
            }

            var bit38 = packet.Translator.ReadBit();
            var bit50 = !packet.Translator.ReadBit();
            if (splineType == 3)
            {
                packet.Translator.ParseBitStream(factingTargetGUID, 5, 3, 6, 1, 4, 2, 0, 7);
                packet.Translator.WriteGuid("Facting Target GUID", factingTargetGUID);
            }

            packet.Translator.ReadXORByte(ownerGUID, 3);

            packet.Translator.ParseBitStream(guid2, 7, 3, 2, 0, 6, 4, 5, 1);


            if (bitB0)
            {
                packet.Translator.ReadSingle("FloatA0");

                for (var i = 0; i < bits8C; ++i)
                {
                    packet.Translator.ReadInt16("short74+2", i);
                    packet.Translator.ReadInt16("short74+0", i);
                }

                packet.Translator.ReadSingle("FloatA8");
                packet.Translator.ReadInt16("IntA4");
                packet.Translator.ReadInt16("IntAC");
            }

            if (bit6D)
                packet.Translator.ReadByte("Byte6D");

            if (splineType == 4)
                packet.Translator.ReadSingle("Facing Angle");

            if (bit40)
                packet.Translator.ReadInt32("Int40");

            packet.Translator.ReadXORByte(ownerGUID, 7);
            if (bit78)
                packet.Translator.ReadByte("Byte78");
            if (bit4C)
                packet.Translator.ReadInt32("Int4C");
            if (bit45)
                packet.Translator.ReadByte("Byte45");

            var waypoints = new Vector3[waypointCount];
            for (var i = 0; i < waypointCount; ++i)
            {
                var vec = packet.Translator.ReadPackedVector3();
                waypoints[i].X = vec.X;
                waypoints[i].Y = vec.Y;
                waypoints[i].Z = vec.Z;
            }

            packet.Translator.ReadXORByte(ownerGUID, 5);
            packet.Translator.ReadXORByte(ownerGUID, 1);
            packet.Translator.ReadXORByte(ownerGUID, 2);

            if (bit48)
                packet.Translator.ReadInt32("Int48");

            Vector3 endpos = new Vector3();
            for (var i = 0; i < splineCount; ++i)
            {
                var spot = new Vector3
                {
                    X = packet.Translator.ReadSingle(),
                    Y = packet.Translator.ReadSingle(),
                    Z = packet.Translator.ReadSingle()
                };
                // client always taking first point
                if (i == 0)
                {
                    endpos = spot;
                }

                packet.AddValue("Spline Waypoint", spot, i);
            }

            packet.Translator.ReadXORByte(ownerGUID, 6);

            if (bit50)
                packet.Translator.ReadInt32("Int50");

            if (splineType == 2)
            {
                packet.Translator.ReadSingle("FloatC0");
                packet.Translator.ReadSingle("FloatC4");
                packet.Translator.ReadSingle("FloatC8");
            }

            if (bit54)
                packet.Translator.ReadSingle("Float54");

            if (bit6C)
                packet.Translator.ReadByte("Byte6C");

            packet.Translator.ReadXORByte(ownerGUID, 0);

            if (bit58)
                packet.Translator.ReadInt32("Int58");

            packet.Translator.ReadXORByte(ownerGUID, 4);

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

            packet.Translator.WriteGuid("Owner GUID", ownerGUID);
            packet.Translator.WriteGuid("Guid2", guid2);
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

            var hasMovementFlags = !packet.Translator.ReadBit();
            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            var hasSplineElevation = !packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            var hasExtraMovementFlags = !packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();

            if (hasExtraMovementFlags)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            guid[7] = packet.Translator.ReadBit();
            var counter = (int)packet.Translator.ReadBits(22);
            guid[2] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var hasTrans = packet.Translator.ReadBit();
            if (hasTrans)
            {
                transportGUID[6] = packet.Translator.ReadBit();
                transportGUID[3] = packet.Translator.ReadBit();
                transportGUID[0] = packet.Translator.ReadBit();
                transportGUID[5] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGUID[7] = packet.Translator.ReadBit();
                transportGUID[2] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGUID[1] = packet.Translator.ReadBit();
                transportGUID[4] = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            var bit94 = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasTimeStamp = !packet.Translator.ReadBit();
            if (hasTrans)
            {
                var transPos = new Vector4();

                if (hasTransportTime2)
                    packet.Translator.ReadInt32("Transport Time 2");

                transPos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGUID, 6);
                transPos.O = packet.Translator.ReadSingle();
                transPos.Z = packet.Translator.ReadSingle();

                if (hasTransportTime3)
                    packet.Translator.ReadInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGUID, 0);
                packet.Translator.ReadInt32("Transport Time");
                packet.Translator.ReadXORByte(transportGUID, 7);
                packet.Translator.ReadXORByte(transportGUID, 2);
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORByte(transportGUID, 1);
                packet.Translator.ReadXORByte(transportGUID, 3);
                packet.Translator.ReadXORByte(transportGUID, 5);
                transPos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGUID, 4);

                packet.Translator.WriteGuid("Transport Guid", transportGUID);
                packet.AddValue("Transport Position", transPos);
            }

            packet.Translator.ReadXORByte(guid, 3);
            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");
            if (bitA8)
                packet.Translator.ReadInt32("IntA8");
            packet.Translator.ReadXORByte(guid, 6);
            pos.X = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 4);

            for (var i = 0; i < counter; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Horizontal Speed");
                }

                packet.Translator.ReadSingle("Velocity Speed");
                packet.Translator.ReadInt32("Fall Time");
            }

            pos.Y = packet.Translator.ReadSingle();

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline Elevation");
            if (hasTimeStamp)
                packet.Translator.ReadInt32("Timestamp");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            pos.Z = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 7);
            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            packet.Translator.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            CoreParsers.MovementHandler.ActivePhases.Clear();

            packet.Translator.ReadUInt32("UInt32 1");

            var count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("Inactive Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt16<MapId>("Inactive Terrain swap", i);

            count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("WorldMapArea swap count", count);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadUInt16("WorldMapArea swap", i);

            count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("Phases count", count);
            for (var i = 0; i < count; ++i)
                CoreParsers.MovementHandler.ActivePhases.Add(packet.Translator.ReadUInt16("Phase id", i)); // Phase.dbc

            count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("Active Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt16<MapId>("Active Terrain swap", i);

            var guid = packet.Translator.StartBitStream(4, 6, 1, 7, 2, 0, 5, 3);
            packet.Translator.ParseBitStream(guid, 0, 4, 7, 6, 3, 5, 1, 2);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 0, 4, 3, 5, 6, 2, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadSingle("Duration modifier");
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.WriteGuid("GUID", guid);
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

            pos.Y = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            guid[4] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var hasExtraMovementFlags = !packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var bits98 = packet.Translator.ReadBits(22);
            guid[5] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            if (hasTransportData)
            {
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasExtraMovementFlags)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.Translator.ReadXORByte(guid, 2);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 4);

            if (hasTransportData)
            {
                var transPos = new Vector4();

                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadXORByte(transportGuid, 2);

                if (hasTransportTime2)
                    packet.Translator.ReadInt32("Transport Time 2");

                transPos.O = packet.Translator.ReadSingle();
                transPos.Z = packet.Translator.ReadSingle();

                if (hasTransportTime3)
                    packet.Translator.ReadInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadInt32("Transport Time");
                transPos.X = packet.Translator.ReadSingle();
                transPos.Y = packet.Translator.ReadSingle();

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", transPos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Horizontal Speed");
                }

                packet.Translator.ReadSingle("Vertical Speed");
                packet.Translator.ReadUInt32("Fall time");
            }

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            packet.AddValue("Position", pos);
            packet.Translator.WriteGuid("Guid", guid);
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

            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            var hasTimestamp = !packet.Translator.ReadBit();
            var hasExtraMovementFlags = !packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            guid[4] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();

            if (hasExtraMovementFlags)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasTransportData)
            {
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);

            if (hasTransportData)
            {
                var transPos = new Vector4();

                packet.Translator.ReadXORByte(transportGuid, 2);
                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadInt32("Transport Time");
                packet.Translator.ReadXORByte(transportGuid, 7);
                transPos.Z = packet.Translator.ReadSingle();
                transPos.Y = packet.Translator.ReadSingle();
                transPos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORByte(transportGuid, 1);
                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");
                packet.Translator.ReadXORByte(transportGuid, 4);
                transPos.O = packet.Translator.ReadSingle();

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", transPos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Horizontal Speed");
                }

                packet.Translator.ReadSingle("Vertical Speed");
                packet.Translator.ReadUInt32("Fall time");
            }

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            var hasOrientation = !packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var hasMovementFlag = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            guid[1] = packet.Translator.ReadBit();
            if (hasTransportData)
            {
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlag)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.Translator.ReadXORByte(transportGuid, 5);
                tpos.Z = packet.Translator.ReadSingle();
                if (hasTransportTime3)
                    packet.Translator.ReadInt32("Transport Time 3");
                tpos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 7);
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadXORByte(transportGuid, 4);
                if (hasTransportTime2)
                    packet.Translator.ReadInt32("Transport Time 2");
                packet.Translator.ReadInt32("Transport Time");
                packet.Translator.ReadXORByte(transportGuid, 0);
                tpos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadXORByte(transportGuid, 1);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Fall Sin");
                }

                packet.Translator.ReadUInt32("Fall time");
                packet.Translator.ReadSingle("Vertical Speed");
            }

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();
            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");
            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");
            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            var hasMovementFlag = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            if (hasTransportData)
            {
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlag)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.Translator.ReadXORByte(transportGuid, 7);
                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadSByte("Seat");
                packet.Translator.ReadXORByte(transportGuid, 4);
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadXORByte(transportGuid, 1);
                tpos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 0);
                tpos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 6);
                tpos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadInt32("Transport Time");

                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (hasFallData)
            {
                packet.Translator.ReadInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Fall Sin");
                }

                packet.Translator.ReadSingle("Vertical Speed");
            }

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();
            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");
            if (bitA8)
                packet.Translator.ReadInt32("IntA8");
            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.Y = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            guid[6] = packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var bit95 = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var hasTransportDat = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();
            if (hasTransportDat)
            {
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
            }

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);

            if (hasTransportDat)
            {
                var tpos = new Vector4();

                tpos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 0);
                tpos.Y = packet.Translator.ReadSingle();

                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                packet.Translator.ReadXORByte(transportGuid, 7);

                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 3);
                tpos.Z = packet.Translator.ReadSingle();
                tpos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadSByte("Transport Seat");
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadUInt32("Transport Time");

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                }

                packet.Translator.ReadInt32("Fall time");
                packet.Translator.ReadSingle("Vertical Speed");
            }

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            var bitA8 = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var hasMovementFlag = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var bits98 = packet.Translator.ReadBits(22);
            var bit94 = packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();

            if (hasTransportData)
            {
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlag)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (hasTransportData)
            {
                var tpos = new Vector4();
                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");
                packet.Translator.ReadXORByte(transportGuid, 6);
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadUInt32("Transport Time");
                tpos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 3);
                tpos.O = packet.Translator.ReadSingle();
                tpos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 0);
                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadXORByte(transportGuid, 5);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Horizontal Speed");
                }

                packet.Translator.ReadInt32("Fall time");
                packet.Translator.ReadSingle("Vertical Speed");
            }

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");
            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");
            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.X = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            guid[3] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var hasMovementFlags = !packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            if (hasTransportData)
            {
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();
            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);
            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            packet.Translator.ReadXORByte(guid, 1);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                tpos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadXORByte(transportGuid, 1);
                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");
                tpos.Z = packet.Translator.ReadSingle();
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadXORByte(transportGuid, 2);
                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");
                packet.Translator.ReadUInt32("Transport Time");
                tpos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 0);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Sin");
                }

                packet.Translator.ReadSingle("Vertical Speed");
                packet.Translator.ReadInt32("Fall time");
            }

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.Z = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            var bitA8 = !packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            guid[5] = packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var hHasSplineElevation = !packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasTransportData)
            {
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 0);

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.Translator.ReadXORByte(transportGuid, 7);

                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadUInt32("Transport Time");
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 5);
                tpos.Z = packet.Translator.ReadSingle();
                tpos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 0);
                tpos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 2);

                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.ReadByte("Seat");

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                }

                packet.Translator.ReadSingle("Vertical Speed");
                packet.Translator.ReadInt32("Fall time");
            }

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (hHasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            var hasTransportData = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var hasTimestamp = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasTransportData)
            {
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.Translator.ReadXORByte(transportGuid, 1);
                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");
                packet.Translator.ReadByte("Seat");
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 4);
                tpos.Z = packet.Translator.ReadSingle();
                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadUInt32("Transport Time");
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 3);
                tpos.X = packet.Translator.ReadSingle();
                tpos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadXORByte(transportGuid, 5);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                }

                packet.Translator.ReadSingle("Vertical Speed");
                packet.Translator.ReadInt32("Fall time");
            }

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.X = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            guid[2] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            guid[3] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            if (hasTransportData)
            {
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
            }

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.Translator.ReadUInt32("Transport Time");
                packet.Translator.ReadXORByte(transportGuid, 2);
                tpos.X = packet.Translator.ReadSingle();

                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadXORByte(transportGuid, 4);
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORByte(transportGuid, 6);
                tpos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 1);

                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                tpos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadXORByte(transportGuid, 0);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Horizontal Speed");
                }

                packet.Translator.ReadSingle("Vertical Speed");
                packet.Translator.ReadInt32("Fall time");
            }

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            guid[3] = packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var bit94 = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            if (hasTransportData)
            {
                transportGuid[5] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
            }

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                packet.Translator.ReadXORByte(transportGuid, 7);
                tpos.Z = packet.Translator.ReadSingle();
                tpos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 2);
                tpos.X = packet.Translator.ReadSingle();

                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                packet.Translator.ReadUInt32("Transport Time");

                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 5);
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 6);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Horizontal Speed");
                }

                packet.Translator.ReadSingle("Vertical Speed");
                packet.Translator.ReadInt32("Fall time");
            }

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.Z = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            var bit94 = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var hasTransportDat = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            var hasMovementFlag = !packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();

            if (hasMovementFlag)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasTransportDat)
            {
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                hasTransTime2 = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                hasTransTime3 = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Cos");
                }

                packet.Translator.ReadSingle("Vertical Speed");
                packet.Translator.ReadInt32("Fall time");
            }

            if (hasTransportDat)
            {
                var tpos = new Vector4();

                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 5);
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 2);
                tpos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadInt32("Transport Time");
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadXORByte(transportGuid, 1);

                if (hasTransTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 7);
                tpos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORByte(transportGuid, 3);
                tpos.O = packet.Translator.ReadSingle();

                if (hasTransTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (hasTimestamp)
                packet.Translator.ReadInt32("Timestamp");

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            guid[7] = packet.Translator.ReadBit();
            var hasMovementFlag = !packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            guid[0] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasTransportData)
            {
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
            }

            if (hasMovementFlag)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            if (hasFallData)
            {
                packet.Translator.ReadInt32("Fall time");

                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                }

                packet.Translator.ReadSingle("Vertical Speed");
            }

            if (hasTransportData)
            {
                var tpos = new Vector4();

                tpos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadXORByte(transportGuid, 4);
                tpos.X = packet.Translator.ReadSingle();

                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 3);

                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                tpos.O = packet.Translator.ReadSingle();
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadUInt32("Transport Time");
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadXORByte(transportGuid, 2);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            var hasPitch = !packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasMovementFlag = !packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var hasSplineElevation = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();

            if (hasTransportData)
            {
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlag)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 5);

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (hasTransportData)
            {
                var tpos = new Vector4();

                tpos.O = packet.Translator.ReadSingle();
                tpos.Z = packet.Translator.ReadSingle();

                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadInt32("Transport Time");
                packet.Translator.ReadXORByte(transportGuid, 6);
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 5);
                tpos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadXORByte(transportGuid, 7);

                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadXORByte(transportGuid, 1);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Sin");
                }

                packet.Translator.ReadInt32("Fall time");
                packet.Translator.ReadSingle("Vertical Speed");
            }

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.Y = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            var hasPitch = !packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var bitAC = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasTransportData)
            {
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
            }

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            if (hasTransportData)
            {
                var tpos = new Vector4();

                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 5);

                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                tpos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadSByte("Seat");
                tpos.O = packet.Translator.ReadSingle();
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadXORByte(transportGuid, 2);
                tpos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadUInt32("Transport Time");

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Horizontal Speed");
                }

                packet.Translator.ReadInt32("Fall time");
                packet.Translator.ReadSingle("Vertical Speed");
            }

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            var hasTimestamp = !packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasTransportData)
            {
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
            }

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 3);
            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Horizontal Speed");
                }

                packet.Translator.ReadInt32("Fall time");
                packet.Translator.ReadSingle("Vertical Speed");
            }

            if (hasTransportData)
            {
                var transPos = new Vector4();

                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");
                packet.Translator.ReadXORByte(transportGuid, 5);
                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadInt32("Transport Time");
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadXORByte(transportGuid, 1);
                transPos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 6);
                transPos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORByte(transportGuid, 0);
                transPos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 4);
                transPos.X = packet.Translator.ReadSingle();

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", transPos);
            }

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            packet.Translator.WriteGuid("Guid", guid);

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

            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();

            guid[4] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var hasUnkTime = !packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var hasMovementFlagExtra = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var hasMovementFlag = !packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var hasTimestamp = !packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var count = packet.Translator.ReadBits(22);
            var hasPitch = !packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            if (hasTransportData)
            {
                transportGuid[7] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
            }

            if (hasMovementFlagExtra)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlag)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 2);

            if (hasUnkTime)
                packet.Translator.ReadInt32("unkTime");

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                }

                packet.Translator.ReadInt32("Fall time");
                packet.Translator.ReadSingle("Vertical Speed");
            }

            if (hasTransportData)
            {
                var tpos = new Vector4();

                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadUInt32("Transport Time");
                packet.Translator.ReadXORByte(transportGuid, 1);
                tpos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 2);

                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                tpos.O = packet.Translator.ReadSingle();
                tpos.Y = packet.Translator.ReadSingle();
                tpos.X = packet.Translator.ReadSingle();

                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadByte("Transport Seat");
                packet.Translator.ReadXORByte(transportGuid, 5);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", tpos);
            }

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (hasTimestamp)
                packet.Translator.ReadUInt32("Timestamp");

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");

            packet.AddValue("Position", pos);
            packet.Translator.WriteGuid("Guid", guid);
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

            pos.X = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            guid[1] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var bit95 = packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var hasTime = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            var hasTrans = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasTrans)
            {
                transportGuid[3] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
            }

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);

            for (var i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);

            if (hasTrans)
            {
                var transPos = new Vector4();

                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadInt32("Transport Time");
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadXORByte(transportGuid, 1);

                if (hasTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.ReadByte("Transport Seat");
                transPos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 6);

                if (hasTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                packet.Translator.ReadXORByte(transportGuid, 3);
                transPos.O =packet.Translator.ReadSingle();
                transPos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(transportGuid, 4);
                transPos.Z = packet.Translator.ReadSingle();

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position", transPos);
            }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                }

                packet.Translator.ReadSingle("Vertical Speed");
                packet.Translator.ReadInt32("Fall time");
            }

            if (hasTime)
                packet.Translator.ReadUInt32("Timestamp");
            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();
            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");
            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline elevation");
            if (bitA8)
                packet.Translator.ReadInt32("IntA8");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_SPEED)]
        public static void HandleMoveSetFlightSpeed(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(2, 1, 6, 5, 7, 3, 0, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_SWIM_SPEED)]
        public static void HandleMoveSetSwimSpeed(Packet packet)
        {
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadInt32("Unk Int32");
            var guid = packet.Translator.StartBitStream(4, 7, 6, 3, 5, 2, 0, 1);
            packet.Translator.ParseBitStream(guid, 1, 6, 5, 2, 0, 3, 4, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_WALK_SPEED)]
        public static void HandleMoveSetWalkSpeed(Packet packet)
        {
            var guid = new byte[8];

            guid[1] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_CAN_FLY)]
        public static void HandleSetCanFly(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 2, 3, 0, 5, 1, 7, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Movement Counter");
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_FLY)]
        public static void HandleUnsetCanFly(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 3, 2, 4, 7, 1, 0, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Movement Counter");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER)]
        public static void HandleSetActiveMover(Packet packet)
        {
            packet.Translator.ReadBit("unk");

            var guid = packet.Translator.StartBitStream(1, 3, 2, 6, 7, 5, 4, 0);
            packet.Translator.ParseBitStream(guid, 5, 1, 7, 2, 6, 3, 4, 0);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_ROOT)]
        public static void HandleSplineMoveRoot(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 3, 5, 6, 2, 1, 0, 7);
            packet.Translator.ParseBitStream(guid, 2, 0, 3, 7, 4, 1, 5, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_UNROOT)]
        public static void HandleSplineMoveUnroot(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 4, 6, 2, 0, 1, 3, 5);
            packet.Translator.ParseBitStream(guid, 4, 2, 6, 0, 5, 3, 7, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_RUN_SPEED)]
        public static void HandleSplineSetRunSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 7, 3, 5, 4, 1, 6, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_SPEED)]
        public static void HandleSplineSetFlightSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 7, 0, 5, 6, 3, 4, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_SWIM_SPEED)]
        public static void HandleSplineSetSwimSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 4, 5, 0, 3, 2, 7, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_WALK_BACK_SPEED)]
        public static void HandleSplineSetWalkSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 7, 5, 4, 3, 0, 1, 2);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SPLINE_SET_RUN_BACK_SPEED)]
        public static void HandleSplineSetRunBackSpeed(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 0, 7, 2, 6, 1, 4, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_ROOT)]
        public static void HandleMoveRoot(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 3, 6, 0, 1, 4, 2, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED)]
        public static void HandleMoveTimeSkipped(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Time");

            packet.Translator.StartBitStream(guid, 5, 7, 6, 2, 0, 1, 4, 3);
            packet.Translator.ParseBitStream(guid, 4, 6, 5, 0, 3, 2, 7, 1);

            packet.Translator.WriteGuid("Guid", guid);
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

            guid[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var bit98 = !packet.Translator.ReadBit();
            var hasTime = !packet.Translator.ReadBit();
            var bitB0 = !packet.Translator.ReadBit();
            var bitsA0 = (int)packet.Translator.ReadBits(22);
            var hasMovementFlagsExtra = !packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var hasO = !packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var bit9D = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var bit9C = packet.Translator.ReadBit();

            if (hasMovementFlagsExtra)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            guid[2] = packet.Translator.ReadBit();
            var bitB4 = packet.Translator.ReadBit();
            var bit78 = !packet.Translator.ReadBit();

            var bit94 = packet.Translator.ReadBit();
            if (bit94)
                hasFallDirection = packet.Translator.ReadBit();

            guid[6] = packet.Translator.ReadBit();

            var hasTransport = packet.Translator.ReadBit();
            if (hasTransport)
            {
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                bit6C = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                bit64 = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
            }

            var hasMovementFlags = !packet.Translator.ReadBit();

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement flags", 30);

            if (bit78)
                packet.Translator.ReadSingle("Float78");

            if (bit94)
            {
                packet.Translator.ReadSingle("Vertical speed");
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Horizontal speed");
                    packet.Translator.ReadSingle("Fall Sin");
                }

                packet.Translator.ReadUInt32("Fall time");
            }

            packet.Translator.ReadXORByte(guid, 0);

            if (hasTransport)
            {
                if (bit6C)
                    packet.Translator.ReadInt32("Int68");
                packet.Translator.ReadInt32("Int5C");
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadByte("Transport Seat");
                packet.Translator.ReadSingle("Float4C");
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadSingle("Float54");
                packet.Translator.ReadSingle("Float48");
                if (bit64)
                    packet.Translator.ReadInt32("Int60");
                packet.Translator.ReadSingle("Float50");

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
            }

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 6);

            for (int i = 0; i < bitsA0; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            if (hasO)
                pos.O = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            pos.Z = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 3);
            if (hasTime)
                packet.Translator.ReadInt32("Timestamp");
            pos.X = packet.Translator.ReadSingle();
            if (bitB0)
                packet.Translator.ReadInt32("IntB0");
            if (bit98)
                packet.Translator.ReadSingle("Float98");
            pos.Y = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.WriteGuid("Guid", guid);
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

            var bit94 = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            var hasFallData = packet.Translator.ReadBit();
            var bit70 = !packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var hasO = !packet.Translator.ReadBit();
            var hasMovementFlagsExtra = !packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();

            var hasTransport = packet.Translator.ReadBit();
            if (hasTransport)
            {
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                bit5C = packet.Translator.ReadBit();
                bit64 = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
            }

            var hasTime = !packet.Translator.ReadBit();

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlagsExtra)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            guid[7] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement flags", 30);

            guid[0] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var bit90 = !packet.Translator.ReadBit();
            if (hasTransport)
            {
                if (bit64)
                    packet.Translator.ReadInt32("Int60");
                packet.Translator.ReadSingle("Float48");
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadInt32("Int54");
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadByte("Transport Seat");
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 3);
                if (bit5C)
                    packet.Translator.ReadInt32("Int58");
                packet.Translator.ReadSingle("Float40");
                packet.Translator.ReadSingle("Float4C");
                packet.Translator.ReadSingle("Float44");
                packet.Translator.ReadXORByte(transportGuid, 6);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
            }

            packet.Translator.ReadSingle("FloatB0");
            if (hasFallData)
            {
                packet.Translator.ReadSingle("Vertical speed");
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Horizontal speed");
                }

                packet.Translator.ReadInt32("Fall time");
            }

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            if (hasTime)
                packet.Translator.ReadInt32("Timestamp");
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadSingle("FloatB4");
            if (hasO)
                pos.O = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 0);
            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            if (bit90)
                packet.Translator.ReadSingle("Float90");
            pos.X = packet.Translator.ReadSingle();
            if (bit70)
                packet.Translator.ReadSingle("Float70");

            for (int i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.WriteGuid("Guid", guid);
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

            guid[3] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var hasTime = !packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var hasTransport = packet.Translator.ReadBit();
            if (hasTransport)
            {
                bit64 = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                bit5C = packet.Translator.ReadBit();
            }

            guid[1] = packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            var hasO = !packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            var bit94 = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement flags", 30);

            var bit90 = !packet.Translator.ReadBit();
            var hasMovementFlagsExtra = !packet.Translator.ReadBit();
            var bit70 = !packet.Translator.ReadBit();
            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();
            var bits98 = (int)packet.Translator.ReadBits(22);
            guid[7] = packet.Translator.ReadBit();

            if (hasMovementFlagsExtra)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal speed");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                }

                packet.Translator.ReadUInt32("Fall time");
                packet.Translator.ReadSingle("Vertical speed");
            }

            if (bitA8)
                packet.Translator.ReadInt32("IntA8");
            if (hasTransport)
            {
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 3);
                if (bit5C)
                    packet.Translator.ReadInt32("Int58");
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadSingle("Float4C");
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadSingle("Float44");
                packet.Translator.ReadSingle("Float48");
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadInt32("Int54");
                packet.Translator.ReadSingle("Float40");
                if (bit64)
                    packet.Translator.ReadInt32("Int60");
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadByte("Byte50");
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.WriteGuid("Guid7", transportGuid);
            }

            if (hasO)
                pos.O = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadSingle("Speed");

            for (int i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            pos.Y = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 3);
            pos.Z = packet.Translator.ReadSingle();

            if (hasTime)
                packet.Translator.ReadInt32("Timestamp");

            packet.Translator.ReadXORByte(guid, 5);
            if (bit70)
                packet.Translator.ReadSingle("Float70");
            packet.Translator.ReadXORByte(guid, 1);
            if (bit90)
                packet.Translator.ReadSingle("Float90");

            packet.Translator.WriteGuid("Guid", guid);
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

            pos.Y = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            packet.Translator.ReadSingle("Speed");
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var bit70 = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var bits98 = packet.Translator.ReadBits(22);
            var bit94 = packet.Translator.ReadBit();
            var bit95 = packet.Translator.ReadBit();
            var hasTransport = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            if (hasTransport)
            {
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                bit64 = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                bit5C = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
            }

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var bitA8 = !packet.Translator.ReadBit();
            var hasO = !packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            var bitAC = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasMovementFlagsExtra = !packet.Translator.ReadBit();
            var hasTime = !packet.Translator.ReadBit();

            if (hasMovementFlagsExtra)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement flags", 30);

            var bit90 = !packet.Translator.ReadBit();
            if (hasTransport)
            {
                packet.Translator.ReadXORByte(transportGuid, 4);
                if (bit64)
                    packet.Translator.ReadInt32("Int60");
                packet.Translator.ReadSingle("Float44");
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadSingle("Float40");
                if (bit5C)
                    packet.Translator.ReadInt32("Int58");
                packet.Translator.ReadSingle("Float48");
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadSingle("Float4C");
                packet.Translator.ReadByte("Transport Seat");
                packet.Translator.ReadInt32("Int54");
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.WriteGuid("Guid7", transportGuid);
            }

            if (hasO)
                pos.O = packet.Translator.ReadSingle();

            if (bit90)
                packet.Translator.ReadSingle("Float90");

            packet.Translator.ReadXORByte(guid, 7);
            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Horizontal speed");
                    packet.Translator.ReadSingle("Fall Sin");
                }

                packet.Translator.ReadSingle("Vertical speed");
                packet.Translator.ReadUInt32("Fall time");
            }

            for (int i = 0; i < bits98; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            packet.Translator.ReadXORByte(guid, 2);
            if (bit70)
                packet.Translator.ReadSingle("Float70");
            if (bitA8)
                packet.Translator.ReadInt32("IntA8");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            if (hasTime)
                packet.Translator.ReadInt32("Timestamp");

            packet.Translator.WriteGuid("Guid", guid);
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

            var hasTransport = packet.Translator.ReadBit();
            if (hasTransport)
            {
                bit6C = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                bit64 = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
            }

            guid[3] = packet.Translator.ReadBit();
            var hasMovementFlagsExtra = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var bit9C = packet.Translator.ReadBit();
            var hasO = !packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            var hasTime = !packet.Translator.ReadBit();
            var bitsA0 = (int)packet.Translator.ReadBits(22);
            guid[4] = packet.Translator.ReadBit();
            var bitB0 = !packet.Translator.ReadBit();

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement flags", 30);

            var bit9D = packet.Translator.ReadBit();
            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();

            if (hasMovementFlagsExtra)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            var bit78 = !packet.Translator.ReadBit();
            var bitB4 = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var bit98 = !packet.Translator.ReadBit();
            if (hasTransport)
            {
                packet.Translator.ReadXORByte(transportGuid, 2);
                if (bit6C)
                    packet.Translator.ReadInt32("Int68");
                packet.Translator.ReadInt32("Int5C");
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadSingle("Float50");
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadSingle("Float4C");
                packet.Translator.ReadSingle("Float48");
                packet.Translator.ReadByte("Byte58");
                packet.Translator.ReadXORByte(transportGuid, 1);
                packet.Translator.ReadXORByte(transportGuid, 6);
                if (bit64)
                    packet.Translator.ReadInt32("Int60");
                packet.Translator.ReadSingle("Float54");
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.WriteGuid("Guid8", transportGuid);
            }

            packet.Translator.ReadXORByte(guid, 0);
            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal speed");
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Fall Sin");
                }

                packet.Translator.ReadUInt32("Fall time");
                packet.Translator.ReadSingle("Vertical speed");
            }

            packet.Translator.ReadXORByte(guid, 6);
            pos.X = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 4);
            pos.Y = packet.Translator.ReadSingle();

            for (int i = 0; i < bitsA0; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 7);
            if (bit78)
                packet.Translator.ReadSingle("Float78");
            if (hasTime)
                packet.Translator.ReadInt32("Timestamp");
            packet.Translator.ReadXORByte(guid, 2);
            pos.Z = packet.Translator.ReadSingle();
            if (hasO)
                pos.O = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 5);
            if (bit98)
                packet.Translator.ReadSingle("Float98");
            if (bitB0)
                packet.Translator.ReadInt32("IntB0");
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("Guid", guid);
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

            guid[3] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var bit98 = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var bitsA0 = (int)packet.Translator.ReadBits(22);
            var bit9C = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var hasTime = !packet.Translator.ReadBit();
            var bitB4 = packet.Translator.ReadBit();
            var bit78 = !packet.Translator.ReadBit();
            var bit9D = packet.Translator.ReadBit();
            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();
            var hasO = !packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            var hasMovementFlagsExtra = !packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var hasTransport = packet.Translator.ReadBit();
            if (hasTransport)
            {
                transportGuid[1] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                bit6C = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                bit64 = packet.Translator.ReadBit();
            }

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement flags", 30);

            var bitB0 = !packet.Translator.ReadBit();

            if (hasMovementFlagsExtra)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            guid[1] = packet.Translator.ReadBit();
            if (hasTransport)
            {
                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadSingle("Float48");
                if (bit6C)
                    packet.Translator.ReadInt32("Int68");
                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 6);
                packet.Translator.ReadSingle("Float50");
                packet.Translator.ReadSingle("Float4C");
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 5);
                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 3);
                packet.Translator.ReadByte("Byte58");
                packet.Translator.ReadSingle("Float54");
                packet.Translator.ReadXORByte(transportGuid, 1);
                if (bit64)
                    packet.Translator.ReadInt32("Int60");
                packet.Translator.ReadInt32("Int5C");
                packet.Translator.WriteGuid("Guid8", transportGuid);
            }

            packet.Translator.ReadSingle("Speed");
            if (hasO)
                pos.O = packet.Translator.ReadSingle();
            if (hasTime)
                packet.Translator.ReadUInt32("Timestamp");
            packet.Translator.ReadXORByte(guid, 5);
            pos.X = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 2);
            if (bit98)
                packet.Translator.ReadSingle("Float98");
            packet.Translator.ReadXORByte(guid, 0);
            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Horizontal speed");
                    packet.Translator.ReadSingle("Fall Sin");
                }

                packet.Translator.ReadSingle("Vertical speed");
                packet.Translator.ReadInt32("Fall time");
            }

            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            if (bit78)
                packet.Translator.ReadSingle("Float78");

            for (int i = 0; i < bitsA0; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            pos.Z = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 3);
            if (bitB0)
                packet.Translator.ReadInt32("IntB0");
            packet.Translator.ReadXORByte(guid, 4);
            pos.Y = packet.Translator.ReadSingle();

            packet.Translator.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_SET_PLAY_HOVER_ANIM)]
        public static void HandlePlayHoverAnim(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Unk");
            guid[0] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            packet.Translator.ParseBitStream(guid, 4, 3, 2, 7, 1, 6, 5, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_TELEPORT_ACK)]
        public static void HandleMoveTeleportAck434(Packet packet)
        {
            packet.Translator.ReadInt32("Time");
            packet.Translator.ReadInt32("Flags");
            var guid = packet.Translator.StartBitStream(0, 7, 6, 2, 5, 1, 4, 3);
            packet.Translator.ParseBitStream(guid, 5, 4, 0, 1, 3, 7, 6, 2);
            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
