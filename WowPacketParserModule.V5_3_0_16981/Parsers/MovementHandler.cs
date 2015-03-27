using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_BIND_POINT_UPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            packet.ReadSingle("Position Z");
            packet.ReadSingle("Position Y");
            packet.ReadInt32<MapId>("Map Id");
            packet.ReadInt32<ZoneId>("Zone Id");
            packet.ReadSingle("Position X");
        }

        [Parser(Opcode.SMSG_LOGIN_SET_TIME_SPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.ReadPackedTime("Game Time");
            packet.ReadUInt32("bit5");
            packet.ReadUInt32("bit7");
            packet.ReadUInt32("bit6");
            packet.ReadSingle("Game Speed");
        }

        public static void ReadClientMovementBlock(Packet packet)
        {
            var guid = new byte[8];
            var transportGUID = new byte[8];
            var pos = new Vector4();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();

            guid[3] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            var counter2 = packet.ReadBits(22);
            guid[2] = packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            packet.ReadBit("bit9C");
            packet.ReadBit("bit85");
            guid[7] = packet.ReadBit();
            var isAlive = !packet.ReadBit();
            var hasFallData = packet.ReadBit();
            var hasExtraMovementFlags = !packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            var hastimestamp = !packet.ReadBit();
            packet.ReadBit("HasSpline");
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            bool hasTransportTime2 = false;
            bool hasTransportTime3 = false;
            bool hasFallDirection = false;
            if (hasTransportData)
            {
                packet.StartBitStream(transportGUID, 6, 3, 5);
                hasTransportTime2 = packet.ReadBit();
                packet.StartBitStream(transportGUID, 4, 7, 0, 1);
                hasTransportTime3 = packet.ReadBit();
                transportGUID[2] = packet.ReadBit();
            }

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            packet.ResetBitReader();

            packet.ReadXORByte(guid, 0);
            for (var i = 0; i < counter2; ++i)
                packet.ReadInt32("Int8C", i);

            packet.ReadXORBytes(guid, 4, 1, 5, 6, 2, 3, 7);
            if (hasSplineElevation)
                packet.ReadSingle("Spline Elevation");

            if (hasTransportData)
            {
                var transPos = new Vector4();
                transPos.Y = packet.ReadSingle();
                packet.ReadXORBytes(transportGUID, 1, 4, 7);
                packet.ReadByte("Seat");
                packet.ReadInt32("Transport Time");
                transPos.X = packet.ReadSingle();
                packet.ReadXORBytes(transportGUID, 0, 6, 2);
                transPos.O = packet.ReadSingle();
                packet.ReadXORBytes(transportGUID, 3);
                if (hasTransportTime3)
                    packet.ReadInt32("Transport Time 3");

                packet.ReadXORBytes(transportGUID, 5);
                if (hasTransportTime2)
                    packet.ReadInt32("Transport Time 2");

                transPos.Z = packet.ReadSingle();
                packet.WriteGuid("Transport Guid", transportGUID);
                packet.AddValue("Transport Position", transPos);
            }

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            if (hasFallData)
            {
                packet.ReadSingle("Velocity Speed");
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }
                packet.ReadInt32("Fall Time");
            }

            if (hastimestamp)
                packet.ReadInt32("timestamp");

            if (hasPitch)
                packet.ReadSingle("Pitch");

            if (isAlive)
                packet.ReadInt32("time(isAlive)");

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            packet.ReadUInt32("MapId");

            packet.ReadSingle("PositionX");
            packet.ReadSingle("PositionY");
            packet.ReadSingle("PositionZ");

            packet.ReadSingle("Orientation");
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER)]
        public static void HandleSetActiveMover(Packet packet)
        {
            packet.ReadBit("unk");

            var guid = packet.StartBitStream(6, 2, 3, 0, 4, 1, 7, 5);
            packet.ParseBitStream(guid, 5, 1, 0, 2, 6, 3, 7, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleMonsterMove(Packet packet)
        {
            var ownerGUID = new byte[8];
            var guid2 = new byte[8];
            var factingTargetGUID = new byte[8];
            var pos = new Vector3();

            packet.ReadSingle("Float34");
            packet.ReadInt32("Move Ticks");
            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            packet.ReadSingle("Float30");
            packet.ReadSingle("Float2C");
            pos.Y = packet.ReadSingle();

            packet.ReadBit("bit38");
            packet.ReadBit(); // fake bit
            var bit6C = !packet.ReadBit();
            ownerGUID[4] = packet.ReadBit();
            var hasTime = !packet.ReadBit();
            ownerGUID[1] = packet.ReadBit();
            var bit6D = !packet.ReadBit();
            ownerGUID[2] = packet.ReadBit();
            var bit4C = !packet.ReadBit();

            packet.StartBitStream(guid2, 6, 1, 3, 5, 2, 7, 4, 0);
            var bit8C = packet.ReadBit();
            ownerGUID[3] = packet.ReadBit();
            var hasFlags = !packet.ReadBit();
            var waypointCount = packet.ReadBits(22);
            packet.StartBitStream(ownerGUID, 5, 0);
            var hasAnimationTime = !packet.ReadBit();
            var hasParabolicTime = !packet.ReadBit();
            ownerGUID[6] = packet.ReadBit();

            var bits90 = 0u;
            if (bit8C)
            {
                bits90 = packet.ReadBits(22);
                packet.ReadBits("bitsA0", 2);
            }

            var hasAnimationState = !packet.ReadBit();
            var splineType = packet.ReadBits(3);
            var splineCount = packet.ReadBits(20);

            if (splineType == 3)
                packet.StartBitStream(factingTargetGUID, 5, 3, 6, 2, 1, 4, 7, 0);

            var hasParabolicSpeed = !packet.ReadBit();
            ownerGUID[7] = packet.ReadBit();
            var bit78 = packet.ReadBit();

            packet.ResetBitReader();
            packet.ReadXORByte(ownerGUID, 2);
            if (splineType == 3)
            {
                packet.ParseBitStream(factingTargetGUID, 1, 0, 6, 5, 3, 4, 7, 2);
                packet.WriteGuid("Facting Target GUID", factingTargetGUID);
            }

            if (bit6D)
                packet.ReadByte("byte6D");

            if (hasParabolicTime)
                packet.ReadInt32("Async-time in ms");

            if (hasAnimationState)
                packet.ReadByteE<MovementAnimationState>("Animation State");

            if (hasTime)
                packet.ReadInt32("Move Time in ms");

            packet.ReadXORBytes(ownerGUID, 7, 1);

            if (hasParabolicSpeed)
                packet.ReadSingle("Vertical Speed");

            packet.ReadXORByte(ownerGUID, 0);
            if (bit8C)
            {
                for (var i = 0; i < bits90; ++i)
                {
                    packet.ReadInt16("short94+2", i);
                    packet.ReadInt16("short94+0", i);
                }

                packet.ReadInt16("shortA8");
                packet.ReadInt16("shortB0");
                packet.ReadSingle("floatAC");
                packet.ReadSingle("floatA4");
            }

            if (hasFlags)
                packet.ReadInt32E<SplineFlag434>("Spline Flags");

            if (splineType == 2)
                packet.ReadVector3("Facing Spot");

            if (bit6C)
                packet.ReadByte("byte6C");

            packet.ReadXORBytes(guid2, 4, 1, 0, 7, 5, 6, 3, 2);

            Vector3 endpos = new Vector3();
            for (var i = 0; i < splineCount; ++i)
            {
                // client always taking first point
                if (i == 0)
                    endpos = packet.ReadVector3("Spline Waypoint", i);
                else
                    packet.ReadVector3("Spline Waypoint", i);
            }

            packet.ReadXORBytes(ownerGUID, 6, 3, 5);
            if (bit4C)
                packet.ReadInt32("int4C");

            if (splineType == 4)
                packet.ReadSingle("Facing Angle");

            if (!bit78)
                packet.ReadByte("byte78");

            packet.ReadXORByte(ownerGUID, 4);
            if (hasAnimationTime)
                packet.ReadInt32("Asynctime in ms"); // Async-time in ms

            // Calculate mid pos
            var mid = new Vector3();
            mid.X = (pos.X + endpos.X) * 0.5f;
            mid.Y = (pos.Y + endpos.Y) * 0.5f;
            mid.Z = (pos.Z + endpos.Z) * 0.5f;
            for (var i = 0; i < waypointCount; ++i)
            {
                // sub_7D11B4(result& wp, int32(from packet), mid)
                var vec = packet.ReadPackedVector3();
                vec.X = mid.X - vec.X;
                vec.Y = mid.Y - vec.Y;
                vec.Z = mid.Z - vec.Z;
                packet.AddValue("Waypoint", vec, i);
            }

            packet.WriteGuid("Owner GUID", ownerGUID);
            packet.WriteGuid("GUID2", guid2);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE)]
        public static void HandlePlayerMove(Packet packet)
        {
            var guid = new byte[8];
            var transportGUID = new byte[8];
            var pos = new Vector4();
            bool hasTransportTime2 = false;
            bool hasTransportTime3 = false;
            bool hasFallDirection = false;


            guid[0] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            var hasExtraMovementFlags = !packet.ReadBit();
            guid[5] = packet.ReadBit();

            if (hasExtraMovementFlags)
                packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            var hasMovementFlags = !packet.ReadBit();
            packet.ReadBit("unk_bit");
            var counter2 = packet.ReadBits(22);
            var hasPitch = !packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            packet.ReadBit("unkbit");
            packet.ReadBit("HasSpline");
            guid[7] = packet.ReadBit();
            var hasTransportData = packet.ReadBit();

            if (hasTransportData)
            {
                transportGUID[6] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                packet.StartBitStream(transportGUID, 0, 3);
                hasTransportTime3 = packet.ReadBit();
                packet.StartBitStream(transportGUID, 7, 4, 1, 5, 2);
            }
            var hastimestamp = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasFallData = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

            var isAlive = !packet.ReadBit();
            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();

            if (hasTransportData)
            {
                var transPos = new Vector4();

                packet.ReadXORBytes(transportGUID, 4, 3, 1);
                if (hasTransportTime3)
                    packet.ReadInt32("Transport Time 3");
                transPos.X = packet.ReadSingle();
                packet.ReadXORBytes(transportGUID, 0);
                transPos.Z = packet.ReadSingle();
                transPos.Y = packet.ReadSingle();
                transPos.O = packet.ReadSingle();
                packet.ReadXORBytes(transportGUID, 2, 6);
                if (hasTransportTime2)
                    packet.ReadInt32("Transport Time 2");
                packet.ReadInt32("Transport Time");
                packet.ReadXORBytes(transportGUID, 7);
                packet.ReadByte("Seat");
                packet.ReadXORBytes(transportGUID, 5);

                packet.WriteGuid("Transport Guid", transportGUID);
                packet.AddValue("Transport Position", transPos);
            }
            packet.ReadXORByte(guid, 6);

            if (isAlive)
                packet.ReadInt32("time(isAlive)");

            if (hasFallData)
            {
                packet.ReadSingle("Velocity Speed");
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Cos");
                    packet.ReadSingle("Fall Sin");
                }
                packet.ReadInt32("Fall Time");
            }

            pos.X = packet.ReadSingle();

            for (var i = 0; i < counter2; ++i)
                packet.ReadInt32("Int8C", i);

            packet.ReadXORByte(guid, 1);
            pos.Y = packet.ReadSingle();
            packet.ReadXORBytes(guid, 2, 7, 5);
            pos.Z = packet.ReadSingle();

            if (hasPitch)
                packet.ReadSingle("Pitch");

            packet.ReadXORBytes(guid, 0, 4);

            if (hasSplineElevation)
                packet.ReadSingle("Spline Elevation");

            if (hastimestamp)
                packet.ReadInt32("timestamp");

            packet.ReadXORByte(guid, 3);

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            CoreParsers.MovementHandler.ActivePhases.Clear();

            var guid = packet.StartBitStream(2, 6, 3, 1, 5, 7, 0, 4);
            packet.ReadXORBytes(guid, 5, 3, 0);

            var count = packet.ReadUInt32() / 2;
            packet.AddValue("WorldMapArea swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("WorldMapArea swap", i);

            packet.ReadUInt32("UInt32 1");
            packet.ReadXORByte(guid, 4);

            count = packet.ReadUInt32() / 2;
            packet.AddValue("Inactive Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadInt16<MapId>("Inactive Terrain swap", i);

            count = packet.ReadUInt32() / 2;
            packet.AddValue("Phases count", count);
            for (var i = 0; i < count; ++i)
                CoreParsers.MovementHandler.ActivePhases.Add(packet.ReadUInt16("Phase id", i)); // Phase.dbc

            packet.ReadXORByte(guid, 7);
            count = packet.ReadUInt32() / 2;
            packet.AddValue("Active Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadInt16<MapId>("Active Terrain swap", i);

            packet.ReadXORBytes(guid, 6, 2, 1);
            packet.WriteGuid("GUID", guid);
        }
    }
}
