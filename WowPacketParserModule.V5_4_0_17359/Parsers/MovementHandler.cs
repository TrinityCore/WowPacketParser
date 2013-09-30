using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            var pos = new Vector4();

            pos.Z = packet.ReadSingle();
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.O = packet.ReadSingle();

            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_BINDPOINTUPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            var pos = new Vector3();

            pos.Y = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            packet.ReadEntryWithName<Int32>(StoreNameType.Area, "Area Id");
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");

            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_MONSTER_MOVE)]
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
                    X = packet.ReadSingle(),
                };
                packet.WriteLine("Facing Spot {0}", spot);
            }

            Vector3 endpos = new Vector3();
            for (var i = 0; i < splineCount; ++i)
            {
                var spot = new Vector3
                {
                    Y = packet.ReadSingle(),
                    Z = packet.ReadSingle(),
                    X = packet.ReadSingle(),
                };
                // client always taking first point
                if (i == 0)
                {
                    endpos = spot;
                }

                packet.WriteLine("[{0}] Spline Waypoint: {1}", i, spot);
            }

            packet.ReadXORByte(ownerGUID, 5);
            packet.ReadInt32("Move Ticks");

            if (hasFlags)
                packet.ReadEnum<SplineFlag434>("Spline Flags", TypeCode.Int32);

            if (hasAnimationState)
                packet.ReadEnum<MovementAnimationState>("Animation State", TypeCode.Byte);

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
                    Z = mid.Z - waypoints[i].Z,
                };
                packet.WriteLine("[{0}] Waypoint: {1}", i, vec);
            }

            packet.WriteGuid("Owner GUID", ownerGUID);
            packet.WriteGuid("GUID2", guid2);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_SET_PHASE_SHIFT)]
        public static void HandlePhaseShift(Packet packet)
        {
            packet.ReadUInt32("UInt32 1");
            //packet.ReadUInt32("UInt32 2");

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

            var guid = packet.StartBitStream(0, 2, 1, 5, 3, 7, 4, 6);
            packet.ParseBitStream(guid, 0, 5, 4, 7, 6, 2, 1, 3);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_PLAYER_MOVE)]
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
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);
 
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
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13);
 
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
                packet.WriteLine("Transport Position {0}", transPos);
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
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED)]
        public static void HandleMoveTimeSkipped434(Packet packet)
        {
            packet.ReadUInt32("Time");
            var guid = packet.StartBitStream(3, 0, 5, 1, 7, 6, 4, 2);
            packet.ParseBitStream(guid, 1, 6, 0, 5, 3, 4, 2, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_UPDATE_RUN_SPEED)]
        public static void HandleMoveUpdateRunSpeed434(Packet packet)
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
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13);

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement flags", 30);

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
                packet.WriteLine("Transport Position: {0}", tpos);
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
            packet.WriteLine("Position: {0}", pos);
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

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_WALK_SPEED)]
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

        [Parser(Opcode.SMSG_SPLINE_MOVE_GRAVITY_DISABLE)]
        public static void HandleSplineMoveGravityDisable(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 5, 3, 6, 1, 4, 2, 7);
            packet.ParseBitStream(guid, 6, 3, 1, 5, 4, 7, 2, 0);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_ROOT)]
        public static void HandleSplineMoveRoot(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 2, 5, 1, 0, 7, 6, 3);
            packet.ParseBitStream(guid, 7, 5, 3, 0, 6, 1, 4, 2);

            packet.WriteGuid("GUID", guid);
        }
    }
}
