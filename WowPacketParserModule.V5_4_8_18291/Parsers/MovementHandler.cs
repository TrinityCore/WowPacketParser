using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using CoreOpcode = WowPacketParser.Enums.Version.Opcodes;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class MovementHandler
    {
        public static PlayerMovementInfo info = new PlayerMovementInfo();

        public static void ReadPlayerMovementInfo(ref Packet packet, params MovementStatusElements [] movementStatusElemente)
        {
            var guid = new byte[8];
            var transportGUID = new byte[8];

            var pos = new Vector4();
            var transportPos = new Vector4();

            bool hasMovementFlags = false;
            bool hasMovementFlags2 = false;
            bool hasTimestamp = false;
            bool hasOrientation = false;
            bool hasTransportData = false;
            bool hasTransportTime2 = false;
            bool hasTransportTime3 = false;
            bool hasPitch = false;
            bool hasFallData = false;
            bool hasFallDirection = false;
            bool hasSplineElevation = false;
            bool hasUnkTime = false;

            uint count = 0;

            foreach (var movementInfo in movementStatusElemente)
            {
                switch (movementInfo)
                {
                    case MovementStatusElements.MSEHasGuidByte0:
                        guid[0] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasGuidByte1:
                        guid[1] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasGuidByte2:
                        guid[2] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasGuidByte3:
                        guid[3] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasGuidByte4:
                        guid[4] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasGuidByte5:
                        guid[5] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasGuidByte6:
                        guid[6] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasGuidByte7:
                        guid[7] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTransportGuidByte0:
                        if (hasTransportData)
                            transportGUID[0] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTransportGuidByte1:
                        if (hasTransportData)
                            transportGUID[1] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTransportGuidByte2:
                        if (hasTransportData)
                            transportGUID[2] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTransportGuidByte3:
                        if (hasTransportData)
                            transportGUID[3] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTransportGuidByte4:
                        if (hasTransportData)
                            transportGUID[4] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTransportGuidByte5:
                        if (hasTransportData)
                            transportGUID[5] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTransportGuidByte6:
                        if (hasTransportData)
                            transportGUID[6] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTransportGuidByte7:
                        if (hasTransportData)
                            transportGUID[7] = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEGuidByte0:
                        packet.ReadXORByte(guid, 0);
                        break;
                    case MovementStatusElements.MSEGuidByte1:
                        packet.ReadXORByte(guid, 1);
                        break;
                    case MovementStatusElements.MSEGuidByte2:
                        packet.ReadXORByte(guid, 2);
                        break;
                    case MovementStatusElements.MSEGuidByte3:
                        packet.ReadXORByte(guid, 3);
                        break;
                    case MovementStatusElements.MSEGuidByte4:
                        packet.ReadXORByte(guid, 4);
                        break;
                    case MovementStatusElements.MSEGuidByte5:
                        packet.ReadXORByte(guid, 5);
                        break;
                    case MovementStatusElements.MSEGuidByte6:
                        packet.ReadXORByte(guid, 6);
                        break;
                    case MovementStatusElements.MSEGuidByte7:
                        packet.ReadXORByte(guid, 7);
                        break;
                    case MovementStatusElements.MSETransportGuidByte0:
                        if (hasTransportData)
                            packet.ReadXORByte(transportGUID, 0);
                        break;
                    case MovementStatusElements.MSETransportGuidByte1:
                        if (hasTransportData)
                            packet.ReadXORByte(transportGUID, 1);
                        break;
                    case MovementStatusElements.MSETransportGuidByte2:
                        if (hasTransportData)
                            packet.ReadXORByte(transportGUID, 2);
                        break;
                    case MovementStatusElements.MSETransportGuidByte3:
                        if (hasTransportData)
                            packet.ReadXORByte(transportGUID, 3);
                        break;
                    case MovementStatusElements.MSETransportGuidByte4:
                        if (hasTransportData)
                            packet.ReadXORByte(transportGUID, 4);
                        break;
                    case MovementStatusElements.MSETransportGuidByte5:
                        if (hasTransportData)
                            packet.ReadXORByte(transportGUID, 5);
                        break;
                    case MovementStatusElements.MSETransportGuidByte6:
                        if (hasTransportData)
                            packet.ReadXORByte(transportGUID, 6);
                        break;
                    case MovementStatusElements.MSETransportGuidByte7:
                        if (hasTransportData)
                            packet.ReadXORByte(transportGUID, 7);
                        break;
                    case MovementStatusElements.MSEHasMovementFlags:
                        hasMovementFlags = !packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasMovementFlags2:
                        hasMovementFlags2 = !packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTimestamp:
                        hasTimestamp = !packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasOrientation:
                        hasOrientation = !packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTransportData:
                        hasTransportData = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTransportTime2:
                        if (hasTransportData)
                            hasTransportTime2 = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasTransportTime3:
                        if (hasTransportData)
                            hasTransportTime3 = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasPitch:
                        hasPitch = !packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasFallData:
                        hasFallData = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasFallDirection:
                        if (hasFallData)
                            hasFallDirection = packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasSplineElevation:
                        hasSplineElevation = !packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEHasSpline:
                        packet.ReadBit("hasSpline");
                        break;
                    case MovementStatusElements.MSECounterCount:
                        count = packet.ReadBits(22);
                        break;
                    case MovementStatusElements.MSECount:
                        packet.ReadInt32("Counter");
                        break;
                    case MovementStatusElements.MSECounter:
                        for (var i = 0; i < count; i++)
                            packet.ReadInt32("Unk Int", i);
                        break;
                    case MovementStatusElements.MSEMovementFlags:
                        if (hasMovementFlags)
                            packet.ReadEnum<MovementFlag>("Movement Flags", 30);
                        break;
                    case MovementStatusElements.MSEMovementFlags2:
                        if (hasMovementFlags2)
                            packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13);
                        break;
                    case MovementStatusElements.MSETimestamp:
                        if (hasTimestamp)
                            packet.ReadInt32("Timestamp");
                        break;
                    case MovementStatusElements.MSEPositionX:
                        pos.X = packet.ReadSingle();
                        break;
                    case MovementStatusElements.MSEPositionY:
                        pos.Y = packet.ReadSingle();
                        break;
                    case MovementStatusElements.MSEPositionZ:
                        pos.Z = packet.ReadSingle();
                        break;
                    case MovementStatusElements.MSEOrientation:
                        if (hasOrientation)
                            pos.O = packet.ReadSingle();
                        break;
                    case MovementStatusElements.MSETransportPositionX:
                        if (hasTransportData)
                            transportPos.X = packet.ReadSingle();
                        break;
                    case MovementStatusElements.MSETransportPositionY:
                        if (hasTransportData)
                            transportPos.Y = packet.ReadSingle();
                        break;
                    case MovementStatusElements.MSETransportPositionZ:
                        if (hasTransportData)
                            transportPos.Z = packet.ReadSingle();
                        break;
                    case MovementStatusElements.MSETransportOrientation:
                        if (hasTransportData)
                            transportPos.O = packet.ReadSingle();
                        break;
                    case MovementStatusElements.MSETransportSeat:
                        if (hasTransportData)
                            packet.ReadByte("Seat");
                        break;
                    case MovementStatusElements.MSETransportTime:
                        if (hasTransportData)
                            packet.ReadInt32("Transport Time");
                        break;
                    case MovementStatusElements.MSETransportTime2:
                        if (hasTransportData && hasTransportTime2)
                            packet.ReadInt32("Transport Time 2");
                        break;
                    case MovementStatusElements.MSETransportTime3:
                        if (hasTransportData && hasTransportTime3)
                            packet.ReadInt32("Transport Time 3");
                        break;
                    case MovementStatusElements.MSEPitch:
                        if (hasPitch)
                            packet.ReadSingle("Pitch");
                        break;
                    case MovementStatusElements.MSEFallTime:
                        if (hasFallData)
                            packet.ReadInt32("Fall time");
                        break;
                    case MovementStatusElements.MSEFallVerticalSpeed:
                        if (hasFallData)
                            packet.ReadSingle("Vertical Speed");
                        break;
                    case MovementStatusElements.MSEFallCosAngle:
                        if (hasFallData && hasFallDirection)
                            packet.ReadSingle("Fall Angle");
                        break;
                    case MovementStatusElements.MSEFallSinAngle:
                        if (hasFallData && hasFallDirection)
                            packet.ReadSingle("Fall Sin");
                        break;
                    case MovementStatusElements.MSEFallHorizontalSpeed:
                        if (hasFallData && hasFallDirection)
                            packet.ReadSingle("Horizontal Speed");
                        break;
                    case MovementStatusElements.MSESplineElevation:
                        if (hasSplineElevation)
                            packet.ReadSingle("Spline elevation");
                        break;
                    case MovementStatusElements.MSEHasUnkTime:
                        hasUnkTime = !packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEUnkTime:
                        if (hasUnkTime)
                            packet.ReadInt32("Unk Time");
                        break;
                    case MovementStatusElements.MSEZeroBit:
                    case MovementStatusElements.MSEOneBit:
                        packet.ReadBit();
                        break;
                    default:
                        break;
                }
            }

            if (hasTransportData)
            {
                packet.WriteGuid("Transport Guid", transportGUID);
                packet.WriteLine("Transport Position {0}", transportPos);
            }

            if (pos.X != 0 && pos.Y != 0 && pos.Z != 0)
                packet.WriteLine("Position: {0}", pos);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_FALL_LAND)]
        public static void HandleMoveFallLand(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementFallLand);
        }

        [Parser(Opcode.MSG_MOVE_HEARTBEAT)]
        public static void HandleMoveHeartbeat(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementHeartBeat);
        }

        [Parser(Opcode.MSG_MOVE_JUMP)]
        public static void HandleMoveJump434(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementJump);
        }

        [Parser(Opcode.MSG_MOVE_SET_FACING)]
        public static void HandleMoveSetFacing(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementSetFacing);
        }

        [Parser(Opcode.MSG_MOVE_SET_PITCH)]
        public static void HandleMoveSetPitch(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementSetPitch);
        }

        [Parser(Opcode.MSG_MOVE_START_ASCEND)]
        public static void HandleMoveStartAscend434(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStartAscend);
        }

        [Parser(Opcode.MSG_MOVE_START_BACKWARD)]
        public static void HandleMoveStartBackward(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStartBackward);
        }

        [Parser(Opcode.MSG_MOVE_START_DESCEND)]
        public static void HandleMoveStartDescend(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStartDescend);
        }

        [Parser(Opcode.MSG_MOVE_START_FORWARD)]
        public static void HandleMoveStartForward(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStartForward);
        }

        [Parser(Opcode.MSG_MOVE_START_PITCH_DOWN)]
        public static void HandleMoveStartPitchDown(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStartPitchDown);
        }

        [Parser(Opcode.MSG_MOVE_START_PITCH_UP)]
        public static void HandleMoveStartPitchUp(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStartPitchUp);
        }

        [Parser(Opcode.MSG_MOVE_START_STRAFE_LEFT)]
        public static void HandleMoveStartStrafeLeft(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStartStrafeLeft);
        }

        [Parser(Opcode.MSG_MOVE_START_STRAFE_RIGHT)]
        public static void HandleMoveStartStrafeRight(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStartStrafeRight);
        }

        [Parser(Opcode.MSG_MOVE_START_SWIM)]
        public static void HandleMoveStartSwim(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStartSwim);
        }

        [Parser(Opcode.MSG_MOVE_START_TURN_LEFT)]
        public static void HandleMoveStartTurnLeft(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStartTurnLeft);
        }

        [Parser(Opcode.MSG_MOVE_START_TURN_RIGHT)]
        public static void HandleMoveStartTurnRight(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStartTurnRight);
        }

        [Parser(Opcode.MSG_MOVE_STOP)]
        public static void HandleMoveStop(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStop);
        }

        [Parser(Opcode.MSG_MOVE_STOP_ASCEND)]
        public static void HandleMoveStopAscend(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStopAscend);
        }

        [Parser(Opcode.MSG_MOVE_STOP_PITCH)]
        public static void HandleMoveStopPitch(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStopPitch);
        }

        [Parser(Opcode.MSG_MOVE_STOP_STRAFE)]
        public static void HandleMoveStopStrafe(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStopStrafe);
        }

        [Parser(Opcode.MSG_MOVE_STOP_SWIM)]
        public static void HandleMoveStopSwim(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStopSwim);
        }

        [Parser(Opcode.MSG_MOVE_STOP_TURN)]
        public static void HandleMoveStopTurn(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementStopTurn);
        }

        [Parser(Opcode.SMSG_MOVE_ROOT)]
        public static void HandleMoveRoot434(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MoveRoot);
        }

        [Parser(Opcode.SMSG_MOVE_UNROOT)]
        public static void HandleMoveUnroot434(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MoveUnroot);
        }

        [Parser(Opcode.SMSG_SET_PHASE_SHIFT)]
        public static void HandlePhaseShift(Packet packet)
        {
            CoreParsers.MovementHandler.ActivePhases.Clear();

            var guid = packet.StartBitStream(0, 3, 1, 4, 6, 2, 7, 5);
            packet.ParseBitStream(guid, 4, 3, 2);

            var count = packet.ReadUInt32() / 2;
            packet.WriteLine("Inactive Terrain swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Inactive Terrain swap", i);

            packet.ParseBitStream(guid, 0, 6);

            count = packet.ReadUInt32() / 2;
            packet.WriteLine("Active Terrain swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int16>(StoreNameType.Map, "Active Terrain swap", i);

            packet.ParseBitStream(guid, 1, 7);

            count = packet.ReadUInt32() / 2;
            packet.WriteLine("WorldMapArea swap count: {0}", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("WorldMapArea swap", i);

            count = packet.ReadUInt32() / 2;
            packet.WriteLine("Phases count: {0}", count);
            for (var i = 0; i < count; ++i)
                CoreParsers.MovementHandler.ActivePhases.Add(packet.ReadUInt16("Phase id", i)); // Phase.dbc

            packet.ParseBitStream(guid, 5);
            packet.WriteGuid("GUID", guid);

            packet.ReadUInt32("UInt32 1");
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            var customLoadScreenSpell = packet.ReadBit();
            var hasTransport = packet.ReadBit();

            if (hasTransport)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Transport Map ID");
                packet.ReadInt32("Transport Entry");
            }

            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

            if (customLoadScreenSpell)
                packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.O = packet.ReadSingle();

            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            var pos = new Vector4();

            pos.X = packet.ReadSingle();
            pos.O = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            pos.Z = packet.ReadSingle();

            packet.WriteLine("Position: {0}", pos);
            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_PLAYER_MOVE)]
        public static void HandlePlayerMove(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.PlayerMove);
        }

        [Parser(Opcode.SMSG_MONSTER_MOVE)]
        public static void HandleMonsterMove(Packet packet)
        {
            var pos = new Vector3();

            var guid2 = new byte[8];
            var factingTargetGUID = new byte[8];
            var ownerGUID = new byte[8];

            pos.Z = packet.ReadSingle();        // +6
            pos.X = packet.ReadSingle();        // +4
            packet.ReadInt32("Int10");          // +10
            pos.Y = packet.ReadSingle();        // +5
            packet.ReadSingle("Float12");       // +12
            packet.ReadSingle("Float13");       // +13
            packet.ReadSingle("Float11");       // +11

            var bit21 = !packet.ReadBit();      // +21
            ownerGUID[0] = packet.ReadBit();    // +32 - 0

            var splineType = (int)packet.ReadBits(3);   // +68
            if (splineType == 3)
                packet.StartBitStream(factingTargetGUID, 6, 4, 3, 0, 5, 7, 1, 2);

            var bit19 = !packet.ReadBit();       // +19
            var bit69 = !packet.ReadBit();       // +69
            var bit120 = !packet.ReadBit();      // +120

            var splineCount = (int)packet.ReadBits(20); // +92

            var bit16 = !packet.ReadBit();      // +16

            ownerGUID[3] = packet.ReadBit();    // +35 - 3
            var bit108 = !packet.ReadBit();     // +108
            var bit22 = !packet.ReadBit();      // +22
            var bit109 = !packet.ReadBit();     // +109
            var bit20 = !packet.ReadBit();      // +20
            ownerGUID[7] = packet.ReadBit();    // +39 - 7
            ownerGUID[4] = packet.ReadBit();    // +36 - 4
            var bit18 = !packet.ReadBit();      // +18
            ownerGUID[5] = packet.ReadBit();    // +37 - 5

            var bits124 = (int)packet.ReadBits(22); // +124

            ownerGUID[6] = packet.ReadBit();    // +38 - 6
            packet.ReadBit();                   // fake bit

            packet.StartBitStream(guid2, 7, 1, 3, 0, 6, 4, 5, 2);

            var bit176 = packet.ReadBit();      // +176
            var bits84 = 0u;
            if (bit176)
            {
                packet.ReadBits("bits74", 2);
                bits84 = packet.ReadBits(22);
            }

            packet.ReadBit("bit56");            // +56
            ownerGUID[2] = packet.ReadBit();    // +38 - 2
            ownerGUID[1] = packet.ReadBit();    // +33 - 1

            var waypoints = new Vector3[bits124];
            for (var i = 0; i < bits124; ++i)
            {
                var vec = packet.ReadPackedVector3();
                waypoints[i].X = vec.X;
                waypoints[i].Y = vec.Y;
                waypoints[i].Z = vec.Z;
            }

            packet.ReadXORByte(ownerGUID, 1);   // +33 - 1

            packet.ParseBitStream(guid2, 6, 4, 1, 7, 0, 3, 5, 2);

            Vector3 endpos = new Vector3();
            for (var i = 0; i < splineCount; ++i)
            {
                var spot = new Vector3
                {
                    Y = packet.ReadSingle(),
                    X = packet.ReadSingle(),
                    Z = packet.ReadSingle(),
                };
                // client always taking first point
                if (i == 0)
                {
                    endpos = spot;
                }

                packet.WriteLine("[{0}] Spline Waypoint: {1}", i, spot);
            }

            if (bit18)
                packet.ReadInt32("Int18");   // +18

            if (splineType == 3)
            {
                packet.ParseBitStream(factingTargetGUID, 5, 7, 0, 4, 3, 2, 6, 1);
                packet.WriteGuid("Facting Target GUID", factingTargetGUID);
            }

            packet.ReadXORByte(ownerGUID, 5);   // +37 - 5

            if (bit21)
                packet.ReadSingle("Float21");   // +21

            if (bit176)
            {
                for (var i = 0; i < bits84; ++i)
                {
                    packet.ReadInt16("short36+4", i);
                    packet.ReadInt16("short36+0", i);
                }

                packet.ReadSingle("Float42");   // +42
                packet.ReadInt16("Int82");      // +82
                packet.ReadInt16("Int86");      // +86
                packet.ReadSingle("Float40");   // +40
            }

            if (bit19)
                packet.ReadInt32("Int19");      // +19

            if (splineType == 4)
                packet.ReadSingle("Facing Angle");  // +45

            packet.ReadXORByte(ownerGUID, 3);   // +35 - 3

            if (bit16)
                packet.ReadInt32("Int16");      // +16

            if (bit69)
                packet.ReadByte("Byte69");      // +69


            packet.ReadXORByte(ownerGUID, 6);   // +38 - 6

            if (bit109)
                packet.ReadByte("Byte109");     // +109

            if (splineType == 2)
            {
                packet.ReadSingle("Float48");   // +48
                packet.ReadSingle("Float49");   // +49
                packet.ReadSingle("Float50");   // +50
            }

            packet.ReadXORByte(ownerGUID, 0);   // +32 - 0

            if (bit120)
                packet.ReadByte("Byte120");     // +120

            if (bit108)
                packet.ReadByte("Byte108");     // +108

            packet.ReadXORByte(ownerGUID, 7);   // +39 - 7
            packet.ReadXORByte(ownerGUID, 2);   // +34 - 2

            if (bit22)
                packet.ReadInt32("Int22");      // +22

            packet.ReadXORByte(ownerGUID, 4);   // +36 - 4

            if (bit20)
                packet.ReadInt32("Int20");      // +20

            // Calculate mid pos
            var mid = new Vector3();
            mid.X = (pos.X + endpos.X) * 0.5f;
            mid.Y = (pos.Y + endpos.Y) * 0.5f;
            mid.Z = (pos.Z + endpos.Z) * 0.5f;
            for (var i = 0; i < bits124; ++i)
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

        [Parser(Opcode.SMSG_LOGIN_SETTIMESPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.ReadInt32("Unk Int32");
            packet.ReadPackedTime("Game Time");
            packet.ReadInt32("Unk Int32");
            packet.ReadInt32("Unk Int32");
            packet.ReadSingle("Game Speed");
        }
    }
}
