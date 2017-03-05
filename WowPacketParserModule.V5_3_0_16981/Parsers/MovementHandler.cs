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
            packet.Translator.ReadSingle("Position Z");
            packet.Translator.ReadSingle("Position Y");
            packet.Translator.ReadInt32<MapId>("Map Id");
            packet.Translator.ReadInt32<ZoneId>("Zone Id");
            packet.Translator.ReadSingle("Position X");
        }

        [Parser(Opcode.SMSG_LOGIN_SET_TIME_SPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.Translator.ReadPackedTime("Game Time");
            packet.Translator.ReadUInt32("bit5");
            packet.Translator.ReadUInt32("bit7");
            packet.Translator.ReadUInt32("bit6");
            packet.Translator.ReadSingle("Game Speed");
        }

        public static void ReadClientMovementBlock(Packet packet)
        {
            var guid = new byte[8];
            var transportGUID = new byte[8];
            var pos = new Vector4();
            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();

            guid[3] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var counter2 = packet.Translator.ReadBits(22);
            guid[2] = packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit9C");
            packet.Translator.ReadBit("bit85");
            guid[7] = packet.Translator.ReadBit();
            var isAlive = !packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();
            var hasExtraMovementFlags = !packet.Translator.ReadBit();
            var hasMovementFlags = !packet.Translator.ReadBit();
            var hastimestamp = !packet.Translator.ReadBit();
            packet.Translator.ReadBit("HasSpline");
            guid[6] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            bool hasTransportTime2 = false;
            bool hasTransportTime3 = false;
            bool hasFallDirection = false;
            if (hasTransportData)
            {
                packet.Translator.StartBitStream(transportGUID, 6, 3, 5);
                hasTransportTime2 = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(transportGUID, 4, 7, 0, 1);
                hasTransportTime3 = packet.Translator.ReadBit();
                transportGUID[2] = packet.Translator.ReadBit();
            }

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasExtraMovementFlags)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadXORByte(guid, 0);
            for (var i = 0; i < counter2; ++i)
                packet.Translator.ReadInt32("Int8C", i);

            packet.Translator.ReadXORBytes(guid, 4, 1, 5, 6, 2, 3, 7);
            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline Elevation");

            if (hasTransportData)
            {
                var transPos = new Vector4();
                transPos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(transportGUID, 1, 4, 7);
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadInt32("Transport Time");
                transPos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(transportGUID, 0, 6, 2);
                transPos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(transportGUID, 3);
                if (hasTransportTime3)
                    packet.Translator.ReadInt32("Transport Time 3");

                packet.Translator.ReadXORBytes(transportGUID, 5);
                if (hasTransportTime2)
                    packet.Translator.ReadInt32("Transport Time 2");

                transPos.Z = packet.Translator.ReadSingle();
                packet.Translator.WriteGuid("Transport Guid", transportGUID);
                packet.AddValue("Transport Position", transPos);
            }

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            if (hasFallData)
            {
                packet.Translator.ReadSingle("Velocity Speed");
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Fall Cos");
                }
                packet.Translator.ReadInt32("Fall Time");
            }

            if (hastimestamp)
                packet.Translator.ReadInt32("timestamp");

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            if (isAlive)
                packet.Translator.ReadInt32("time(isAlive)");

            packet.Translator.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            packet.Translator.ReadUInt32("MapId");

            packet.Translator.ReadSingle("PositionX");
            packet.Translator.ReadSingle("PositionY");
            packet.Translator.ReadSingle("PositionZ");

            packet.Translator.ReadSingle("Orientation");
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER)]
        public static void HandleSetActiveMover(Packet packet)
        {
            packet.Translator.ReadBit("unk");

            var guid = packet.Translator.StartBitStream(6, 2, 3, 0, 4, 1, 7, 5);
            packet.Translator.ParseBitStream(guid, 5, 1, 0, 2, 6, 3, 7, 4);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleMonsterMove(Packet packet)
        {
            var ownerGUID = new byte[8];
            var guid2 = new byte[8];
            var factingTargetGUID = new byte[8];
            var pos = new Vector3();

            packet.Translator.ReadSingle("Float34");
            packet.Translator.ReadInt32("Move Ticks");
            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            packet.Translator.ReadSingle("Float30");
            packet.Translator.ReadSingle("Float2C");
            pos.Y = packet.Translator.ReadSingle();

            packet.Translator.ReadBit("bit38");
            packet.Translator.ReadBit(); // fake bit
            var bit6C = !packet.Translator.ReadBit();
            ownerGUID[4] = packet.Translator.ReadBit();
            var hasTime = !packet.Translator.ReadBit();
            ownerGUID[1] = packet.Translator.ReadBit();
            var bit6D = !packet.Translator.ReadBit();
            ownerGUID[2] = packet.Translator.ReadBit();
            var bit4C = !packet.Translator.ReadBit();

            packet.Translator.StartBitStream(guid2, 6, 1, 3, 5, 2, 7, 4, 0);
            var bit8C = packet.Translator.ReadBit();
            ownerGUID[3] = packet.Translator.ReadBit();
            var hasFlags = !packet.Translator.ReadBit();
            var waypointCount = packet.Translator.ReadBits(22);
            packet.Translator.StartBitStream(ownerGUID, 5, 0);
            var hasAnimationTime = !packet.Translator.ReadBit();
            var hasParabolicTime = !packet.Translator.ReadBit();
            ownerGUID[6] = packet.Translator.ReadBit();

            var bits90 = 0u;
            if (bit8C)
            {
                bits90 = packet.Translator.ReadBits(22);
                packet.Translator.ReadBits("bitsA0", 2);
            }

            var hasAnimationState = !packet.Translator.ReadBit();
            var splineType = packet.Translator.ReadBits(3);
            var splineCount = packet.Translator.ReadBits(20);

            if (splineType == 3)
                packet.Translator.StartBitStream(factingTargetGUID, 5, 3, 6, 2, 1, 4, 7, 0);

            var hasParabolicSpeed = !packet.Translator.ReadBit();
            ownerGUID[7] = packet.Translator.ReadBit();
            var bit78 = packet.Translator.ReadBit();

            packet.Translator.ResetBitReader();
            packet.Translator.ReadXORByte(ownerGUID, 2);
            if (splineType == 3)
            {
                packet.Translator.ParseBitStream(factingTargetGUID, 1, 0, 6, 5, 3, 4, 7, 2);
                packet.Translator.WriteGuid("Facting Target GUID", factingTargetGUID);
            }

            if (bit6D)
                packet.Translator.ReadByte("byte6D");

            if (hasParabolicTime)
                packet.Translator.ReadInt32("Async-time in ms");

            if (hasAnimationState)
                packet.Translator.ReadByteE<MovementAnimationState>("Animation State");

            if (hasTime)
                packet.Translator.ReadInt32("Move Time in ms");

            packet.Translator.ReadXORBytes(ownerGUID, 7, 1);

            if (hasParabolicSpeed)
                packet.Translator.ReadSingle("Vertical Speed");

            packet.Translator.ReadXORByte(ownerGUID, 0);
            if (bit8C)
            {
                for (var i = 0; i < bits90; ++i)
                {
                    packet.Translator.ReadInt16("short94+2", i);
                    packet.Translator.ReadInt16("short94+0", i);
                }

                packet.Translator.ReadInt16("shortA8");
                packet.Translator.ReadInt16("shortB0");
                packet.Translator.ReadSingle("floatAC");
                packet.Translator.ReadSingle("floatA4");
            }

            if (hasFlags)
                packet.Translator.ReadInt32E<SplineFlag434>("Spline Flags");

            if (splineType == 2)
                packet.Translator.ReadVector3("Facing Spot");

            if (bit6C)
                packet.Translator.ReadByte("byte6C");

            packet.Translator.ReadXORBytes(guid2, 4, 1, 0, 7, 5, 6, 3, 2);

            Vector3 endpos = new Vector3();
            for (var i = 0; i < splineCount; ++i)
            {
                // client always taking first point
                if (i == 0)
                    endpos = packet.Translator.ReadVector3("Spline Waypoint", i);
                else
                    packet.Translator.ReadVector3("Spline Waypoint", i);
            }

            packet.Translator.ReadXORBytes(ownerGUID, 6, 3, 5);
            if (bit4C)
                packet.Translator.ReadInt32("int4C");

            if (splineType == 4)
                packet.Translator.ReadSingle("Facing Angle");

            if (!bit78)
                packet.Translator.ReadByte("byte78");

            packet.Translator.ReadXORByte(ownerGUID, 4);
            if (hasAnimationTime)
                packet.Translator.ReadInt32("Asynctime in ms"); // Async-time in ms

            // Calculate mid pos
            var mid = new Vector3();
            mid.X = (pos.X + endpos.X) * 0.5f;
            mid.Y = (pos.Y + endpos.Y) * 0.5f;
            mid.Z = (pos.Z + endpos.Z) * 0.5f;
            for (var i = 0; i < waypointCount; ++i)
            {
                // sub_7D11B4(result& wp, int32(from packet), mid)
                var vec = packet.Translator.ReadPackedVector3();
                vec.X = mid.X - vec.X;
                vec.Y = mid.Y - vec.Y;
                vec.Z = mid.Z - vec.Z;
                packet.AddValue("Waypoint", vec, i);
            }

            packet.Translator.WriteGuid("Owner GUID", ownerGUID);
            packet.Translator.WriteGuid("GUID2", guid2);
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


            guid[0] = packet.Translator.ReadBit();
            var hasOrientation = !packet.Translator.ReadBit();
            var hasExtraMovementFlags = !packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            if (hasExtraMovementFlags)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

            var hasMovementFlags = !packet.Translator.ReadBit();
            packet.Translator.ReadBit("unk_bit");
            var counter2 = packet.Translator.ReadBits(22);
            var hasPitch = !packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            packet.Translator.ReadBit("unkbit");
            packet.Translator.ReadBit("HasSpline");
            guid[7] = packet.Translator.ReadBit();
            var hasTransportData = packet.Translator.ReadBit();

            if (hasTransportData)
            {
                transportGUID[6] = packet.Translator.ReadBit();
                hasTransportTime2 = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(transportGUID, 0, 3);
                hasTransportTime3 = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(transportGUID, 7, 4, 1, 5, 2);
            }
            var hastimestamp = !packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit();

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            var isAlive = !packet.Translator.ReadBit();
            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            guid[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();

            if (hasTransportData)
            {
                var transPos = new Vector4();

                packet.Translator.ReadXORBytes(transportGUID, 4, 3, 1);
                if (hasTransportTime3)
                    packet.Translator.ReadInt32("Transport Time 3");
                transPos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(transportGUID, 0);
                transPos.Z = packet.Translator.ReadSingle();
                transPos.Y = packet.Translator.ReadSingle();
                transPos.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(transportGUID, 2, 6);
                if (hasTransportTime2)
                    packet.Translator.ReadInt32("Transport Time 2");
                packet.Translator.ReadInt32("Transport Time");
                packet.Translator.ReadXORBytes(transportGUID, 7);
                packet.Translator.ReadByte("Seat");
                packet.Translator.ReadXORBytes(transportGUID, 5);

                packet.Translator.WriteGuid("Transport Guid", transportGUID);
                packet.AddValue("Transport Position", transPos);
            }
            packet.Translator.ReadXORByte(guid, 6);

            if (isAlive)
                packet.Translator.ReadInt32("time(isAlive)");

            if (hasFallData)
            {
                packet.Translator.ReadSingle("Velocity Speed");
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Cos");
                    packet.Translator.ReadSingle("Fall Sin");
                }
                packet.Translator.ReadInt32("Fall Time");
            }

            pos.X = packet.Translator.ReadSingle();

            for (var i = 0; i < counter2; ++i)
                packet.Translator.ReadInt32("Int8C", i);

            packet.Translator.ReadXORByte(guid, 1);
            pos.Y = packet.Translator.ReadSingle();
            packet.Translator.ReadXORBytes(guid, 2, 7, 5);
            pos.Z = packet.Translator.ReadSingle();

            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            packet.Translator.ReadXORBytes(guid, 0, 4);

            if (hasSplineElevation)
                packet.Translator.ReadSingle("Spline Elevation");

            if (hastimestamp)
                packet.Translator.ReadInt32("timestamp");

            packet.Translator.ReadXORByte(guid, 3);

            if (hasOrientation)
                pos.O = packet.Translator.ReadSingle();

            packet.Translator.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            CoreParsers.MovementHandler.ActivePhases.Clear();

            var guid = packet.Translator.StartBitStream(2, 6, 3, 1, 5, 7, 0, 4);
            packet.Translator.ReadXORBytes(guid, 5, 3, 0);

            var count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("WorldMapArea swap count", count);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadUInt16("WorldMapArea swap", i);

            packet.Translator.ReadUInt32("UInt32 1");
            packet.Translator.ReadXORByte(guid, 4);

            count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("Inactive Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt16<MapId>("Inactive Terrain swap", i);

            count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("Phases count", count);
            for (var i = 0; i < count; ++i)
                CoreParsers.MovementHandler.ActivePhases.Add(packet.Translator.ReadUInt16("Phase id", i)); // Phase.dbc

            packet.Translator.ReadXORByte(guid, 7);
            count = packet.Translator.ReadUInt32() / 2;
            packet.AddValue("Active Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt16<MapId>("Active Terrain swap", i);

            packet.Translator.ReadXORBytes(guid, 6, 2, 1);
            packet.Translator.WriteGuid("GUID", guid);
        }
    }
}
