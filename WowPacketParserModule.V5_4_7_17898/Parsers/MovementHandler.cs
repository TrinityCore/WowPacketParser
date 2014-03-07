using System;
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
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();

            packet.WriteLine("Position: {0}", pos);
            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            var pos = new Vector4();

            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.O = packet.ReadSingle();
            pos.X = packet.ReadSingle();

            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_BINDPOINTUPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            var pos = new Vector3();

            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();

            packet.ReadEntryWithName<Int32>(StoreNameType.Area, "Area Id");

            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_LOGIN_SETTIMESPEED)]
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
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

            var customLoadScreenSpell = packet.ReadBit();
            var hasTransport = packet.ReadBit();

            if (hasTransport)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Transport Map ID");
                packet.ReadInt32("Transport Entry");
            }

            if (customLoadScreenSpell)
                packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_MONSTER_MOVE)]
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
                    Z = packet.ReadSingle(),
                };
                // client always taking first point
                if (i == 0)
                {
                    endpos = spot;
                }

                packet.WriteLine("[{0}] Spline Waypoint: {1}", i, spot);
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
                    Z = mid.Z - waypoints[i].Z,
                };
                packet.WriteLine("[{0}] Waypoint: {1}", i, vec);
            }

            packet.WriteGuid("Owner GUID", ownerGUID);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_PLAYER_MOVE)]
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
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

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
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13);

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
                packet.WriteLine("Transport Position {0}", transPos);
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
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_SET_PHASE_SHIFT)]
        public static void HandlePhaseShift(Packet packet)
        {
            packet.ReadUInt32("UInt32 1");

            var count = packet.ReadUInt32() / 2;
            packet.WriteLine("WorldMapArea swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("WorldMapArea swap", i);

            count = packet.ReadUInt32() / 2;
            packet.WriteLine("Phases count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("Phase id", i); // Phase.dbc

            count = packet.ReadUInt32() / 2;
            packet.WriteLine("Inactive Terrain swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Inactive Terrain swap", i);


            count = packet.ReadUInt32() / 2;
            packet.WriteLine("Active Terrain swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Active Terrain swap", i);


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
            var hasRotation = !packet.ReadBit();
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
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

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
                packet.WriteLine("Transport Position {0}", transPos);
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
            if (hasRotation)
                pos.O = packet.ReadSingle();

            packet.WriteLine("Position: {0}", pos);
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
            var hasTrans = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var count = packet.ReadBits(22);
            var hasPitch = !packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            if (hasTrans)
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
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13);
            
            if (hasMovementFlag)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

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

            if (hasTrans)
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
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasTimestamp)
                packet.ReadUInt32("Timestamp");

            if (hasSplineElevation)
                packet.ReadSingle("Spline elevation");

            packet.WriteLine("Position: {0}", pos);
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
    }
}
