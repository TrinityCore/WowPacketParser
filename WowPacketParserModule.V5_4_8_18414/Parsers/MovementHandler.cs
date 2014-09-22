using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParserModule.V5_4_8_18414.Misc;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.WowGuid;
using UpdateFields = WowPacketParser.Enums.Version.UpdateFields;
using MovementFlag = WowPacketParserModule.V5_4_8_18414.Enums.MovementFlag;
using MovementFlagExtra = WowPacketParserModule.V5_4_8_18414.Enums.MovementFlagExtra;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class MovementHandler
    {
        public static PlayerMovementInfo info = new PlayerMovementInfo();

        public static void ReadPlayerMovementInfo(ref Packet packet, params MovementStatusElements[] movementStatusElemente)
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
                        hasMovementFlags = !packet.ReadBit("!hasMovementFlags");
                        break;
                    case MovementStatusElements.MSEHasMovementFlags2:
                        hasMovementFlags2 = !packet.ReadBit("!hasMovementFlags2");
                        break;
                    case MovementStatusElements.MSEHasTimestamp:
                        hasTimestamp = !packet.ReadBit("!hasTimestamp");
                        break;
                    case MovementStatusElements.MSEHasOrientation:
                        hasOrientation = !packet.ReadBit("!hasOrientation");
                        break;
                    case MovementStatusElements.MSEHasTransportData:
                        hasTransportData = packet.ReadBit("hasTransportData");
                        break;
                    case MovementStatusElements.MSEHasTransportTime2:
                        if (hasTransportData)
                            hasTransportTime2 = packet.ReadBit("hasTransportTime2");
                        break;
                    case MovementStatusElements.MSEHasTransportTime3:
                        if (hasTransportData)
                            hasTransportTime3 = packet.ReadBit("hasTransportTime3");
                        break;
                    case MovementStatusElements.MSEHasPitch:
                        hasPitch = !packet.ReadBit("!hasPitch");
                        break;
                    case MovementStatusElements.MSEHasFallData:
                        hasFallData = packet.ReadBit("hasFallData");
                        break;
                    case MovementStatusElements.MSEHasFallDirection:
                        if (hasFallData)
                            hasFallDirection = packet.ReadBit("hasFallDirection");
                        break;
                    case MovementStatusElements.MSEHasSplineElevation:
                        hasSplineElevation = !packet.ReadBit("!hasSplineElevation");
                        break;
                    case MovementStatusElements.MSEHasSpline:
                        packet.ReadBit("hasSpline");
                        break;
                    case MovementStatusElements.MSECounterCount:
                        count = packet.ReadBits("counter", 22);
                        break;
                    case MovementStatusElements.MSECount:
                        packet.ReadInt32("MCounter");
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
                        hasUnkTime = !packet.ReadBit("!hasUnkTime");
                        break;
                    case MovementStatusElements.MSEUnkTime:
                        if (hasUnkTime)
                            packet.ReadInt32("Unk Time");
                        break;
                    case MovementStatusElements.MSEZeroBit:
                    case MovementStatusElements.MSEOneBit:
                        packet.ReadBit();
                        break;
                    case MovementStatusElements.MSEbit148:
                        packet.ReadBit("bit148");
                        break;
                    case MovementStatusElements.MSEbit149:
                        packet.ReadBit("bit149");
                        break;
                    case MovementStatusElements.MSEbit172:
                        packet.ReadBit("bit172");
                        break;
                    case MovementStatusElements.MSEExtra2Bits:
                        packet.ReadBits("2bits", 2);
                        break;
                    case MovementStatusElements.MSEExtraInt32:
                        packet.ReadInt32("Int32");
                        break;
                    case MovementStatusElements.MSEExtraFloat:
                        packet.ReadSingle("Single");
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

            if (pos.X != 0 || pos.Y != 0 || pos.Z != 0)
                packet.WriteLine("Position: {0}", pos);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_FORCE_MOVE_ROOT_ACK)]
        public static void HandleForceMoveRootAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementForceMoveRootAck);
        }

        [Parser(Opcode.CMSG_FORCE_MOVE_UNROOT_ACK)]
        public static void HandleForceMoveUnRootAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementForceMoveUnrootAck);
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED)]
        public static void HandleMoveTimeSkipped(Packet packet)
        {
            packet.ReadInt32("Time"); // 24
            var guid = packet.StartBitStream(5, 0, 7, 4, 1, 2, 6, 3);
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 7, 2, 0, 6, 1, 5, 3, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER)]
        public static void HandleSetAsctiveMover(Packet packet)
        {
            packet.ReadBit("unk");
            var guid = packet.StartBitStream(3, 0, 2, 1, 5, 4, 7, 6);
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 3, 4, 5, 2, 7, 0, 1, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_MOVE_FALL_LAND)]
        public static void HandleMoveFallLand(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle(); // 40
            pos.Z = packet.ReadSingle(); // 44
            pos.X = packet.ReadSingle(); // 36
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            packet.ReadBit("bit 172"); // 172
            packet.ReadBit("bit 148"); // 148
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            guid[7] = packet.ReadBit(); // 23
            packet.ReadBit("bit 149"); // 149
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            guid[5] = packet.ReadBit(); // 21
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            guid[2] = packet.ReadBit(); // 18
            guid[3] = packet.ReadBit(); // 19
            guid[0] = packet.ReadBit(); // 16
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[1] = packet.ReadBit(); // 17
            var hasTrans = packet.ReadBit("Has transport"); // 104
            guid[6] = packet.ReadBit(); // 22
            guid[4] = packet.ReadBit(); // 20
            if (hasTrans)
            {
                transportGuid[0] = packet.ReadBit(); // 56
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[6] = packet.ReadBit(); // 62
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
            }

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasFallData)
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 4, 3, 7, 0, 2, 5, 1, 6);

            if (Count > 0)
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin"); // 128
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 124
                }
                packet.ReadUInt32("Fall time"); // 116
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasTrans)
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 4); // 60
                tpos.Y = packet.ReadSingle(); // 68
                tpos.O = packet.ReadSingle(); // 76
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadXORByte(transportGuid, 6); // 62
                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 5); // 61
                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadUInt32("Transport Time"); // 84
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSpline)
                packet.ReadInt32("Spline"); // 168

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_FLIGHT_SPEED_CHANGE_ACK)]
        public static void HandleMoveForceFlightSpeedChangeAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementForceFlightSpeedChangeAck);
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_RUN_BACK_SPEED_CHANGE_ACK)]
        public static void HandleMoveForceRunBackSpeedChangeAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementForceRunBackSpeedChangeAck);
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_RUN_SPEED_CHANGE_ACK)]
        public static void HandleMoveForceRunSpeedChangeAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementForceRunSpeedChangeAck);
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_SWIM_SPEED_CHANGE_ACK)]
        public static void HandleMoveForceSwimSpeedChangeAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementForceSwimSpeedChangeAck);
        }

        [Parser(Opcode.CMSG_MOVE_FORCE_WALK_SPEED_CHANGE_ACK)]
        public static void HandleMoveForceWalkSpeedChangeAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementForceWalkSpeedChangeAck);
        }

        [Parser(Opcode.CMSG_MOVE_GRAVITY_DISABLE_ACK)]
        public static void HandleMoveGravityDisableAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementGravityDisableAck);
        }

        [Parser(Opcode.CMSG_MOVE_GRAVITY_ENABLE_ACK)]
        public static void HandleMovementGravityEnableAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementGravityEnableAck);
        }

        [Parser(Opcode.CMSG_UNK_00D9)]
        public static void HandleCUnk00D9(Packet packet)
        {
            // ������� ��� ������� ����� ����������� (� ����� �� ������ �����)
            ReadPlayerMovementInfo(ref packet, info.CUnk00D9);
        }

        [Parser(Opcode.CMSG_UNK_01F1)]
        public static void HandleCUnk01F1(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.CUnk01F1); // 679E4F
        }

        [Parser(Opcode.CMSG_MOVE_FEATHER_FALL_ACK)]
        public static void HandleMoveFeatherFallAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementFeatherFallAck);
        }

        [Parser(Opcode.CMSG_UNK_08D3)]
        public static void HandleCUnk08D3(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.CUnk08D3);
        }

        [Parser(Opcode.CMSG_UNK_09FA)]
        public static void HandleCUnk09FA(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.CUnk09FA);
        }

        [Parser(Opcode.CMSG_UNK_09FB)]
        public static void HandleCUnk09FB(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.CUnk09FB);
        }

        [Parser(Opcode.CMSG_MOVE_SET_CAN_FLY_ACK)]
        public static void HandleMoveSetCanFlyAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementSetCanFlyAck);
        }

        [Parser(Opcode.CMSG_MOVE_SET_CAN_TRANSITION_BETWEEN_SWIM_AND_FLY_ACK)]
        public static void HandleMoveSetCanTransitionBetweenSwimAndFlyAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementSetCanTransitionBetweenSwimAndFlyAck);
        }

        [Parser(Opcode.CMSG_MOVE_SPLINE_DONE)]
        public static void HandleMovesplineDone(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementSplineDone);
        }

        [Parser(Opcode.CMSG_UNK_185B)]
        public static void HandleCUnk185B(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.CUnk185B);
        }

        [Parser(Opcode.SMSG_PLAYER_MOVE)]
        public static void HandlePlayerMove(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.PlayerMove);
        }

        [Parser(Opcode.SMSG_MOVE_FEATHER_FALL)]
        public static void HandleMoveFeatherFall(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementFeatherFall);
        }

        [Parser(Opcode.SMSG_MOVE_GRAVITY_DISABLE)]
        public static void HandleMoveGravityDisable(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementGravityDisable);
        }

        [Parser(Opcode.SMSG_MOVE_GRAVITY_ENABLE)]
        public static void HandleMoveGravityEnable(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementGravityEnable);
        }

        [Parser(Opcode.SMSG_MOVE_LAND_WALK)]
        public static void HandleMoveLandWalk(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementLandWalk);
        }

        [Parser(Opcode.SMSG_MOVE_NORMAL_FALL)]
        public static void HandleMoveNormalFall(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementNormalFall);
        }

        [Parser(Opcode.SMSG_MOVE_ROOT)]
        public static void HandleMoveRoot(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementRoot);
        }

        [Parser(Opcode.SMSG_MOVE_SET_CAN_FLY)]
        public static void HandleSetCanFly(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementSetCanFly);
        }

        [Parser(Opcode.SMSG_MOVE_SET_CAN_TRANSITION_BETWEEN_SWIM_AND_FLY)]
        public static void HandleMoveSetCanTransitionBetweenSwimAndFly(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementSetCanTransitionBetweenSwimAndFly);
        }

        [Parser(Opcode.SMSG_MOVE_UNROOT)]
        public static void HandleMoveUnroot(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementUnroot);
        }

        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_FLY)]
        public static void HandleUnSetCanFly(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementUnSetCanFly);
        }

        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_TRANSITION_BETWEEN_SWIM_AND_FLY)]
        public static void HandleMoveUnSetCanTransitionBetweenSwimAndFly(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementUnSetCanTransitionBetweenSwimAndFly);
        }

        [Parser(Opcode.SMSG_UNK_0047)]
        public static void HandleUnk0047(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.Unk0047);
        }

        [Parser(Opcode.SMSG_UNK_00E1)]
        public static void HandleUnk00E1(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.Unk00E1);
        }

        [Parser(Opcode.SMSG_UNK_01E2)]
        public static void HandleUnk01E2(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.Unk01E2);
        }

        [Parser(Opcode.SMSG_UNK_023B)]
        public static void HandleUnk023B(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.Unk023B);
        }

        [Parser(Opcode.SMSG_UNK_0251)]
        public static void HandleUnk0251(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.SUnk0251);
        }

        [Parser(Opcode.SMSG_UNK_0861)]
        public static void HandleUnk0861(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.SUnk0861);
        }

        [Parser(Opcode.SMSG_UNK_08A3)]
        public static void HandleUnk08A3(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.Unk08A3);
        }

        [Parser(Opcode.SMSG_UNK_158E)]
        public static void HandleSUnk158E(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.Unk158E);
        }

        [Parser(Opcode.SMSG_UNK_1812)]
        public static void HandleSUnk1812(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.Unk1812);
        }

        [Parser(Opcode.MSG_MOVE_HEARTBEAT)]
        public static void HandleMoveHeartbeat(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle(); // 40
            pos.X = packet.ReadSingle(); // 36
            pos.Z = packet.ReadSingle(); // 44
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            packet.ReadBit("bit 148"); // 148
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[3] = packet.ReadBit(); // 19
            guid[6] = packet.ReadBit(); // 22
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            packet.ReadBit("bit 149"); // 149
            packet.ReadBit("bit 172"); // 172
            guid[7] = packet.ReadBit(); // 23
            guid[2] = packet.ReadBit(); // 18
            guid[4] = packet.ReadBit(); // 20
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            var hasTrans = packet.ReadBit("Has transport"); // 104
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            guid[5] = packet.ReadBit(); // 21
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            guid[1] = packet.ReadBit(); // 17
            guid[0] = packet.ReadBit(); // 16

            if (hasTrans) // 104
            {
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[7] = packet.ReadBit(); // 63
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[4] = packet.ReadBit(); // 60
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
            }

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 2, 3, 6, 1, 4, 7);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 5, 0);

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Horizontal Speed"); // 132
                }
                packet.ReadSingle("Vertical Speed"); // 120
                packet.ReadUInt32("Fall time"); // 116
            }

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 0); // 56
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 7); // 63
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadXORByte(transportGuid, 4); // 60
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 5); // 61
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadUInt32("Transport Time"); // 84
                tpos.O = packet.ReadSingle(); // 76
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_JUMP)]
        public static void HandleMoveJump(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle(); // 40
            pos.X = packet.ReadSingle(); // 36
            pos.Z = packet.ReadSingle(); // 44
            guid[1] = packet.ReadBit(); // 17
            guid[7] = packet.ReadBit(); // 23
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            guid[5] = packet.ReadBit(); // 21
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[6] = packet.ReadBit(); // 22
            guid[4] = packet.ReadBit(); // 20
            packet.ReadBit("bit 149"); // 149
            var hasTrans = packet.ReadBit("Has transport"); // 104
            packet.ReadBit("bit 148"); // 148
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[3] = packet.ReadBit(); // 19
            packet.ReadBit("bit 172"); // 172
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            guid[2] = packet.ReadBit(); // 18
            guid[0] = packet.ReadBit(); // 16

            if (hasTrans) // 104
            {
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[4] = packet.ReadBit(); // 60
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[7] = packet.ReadBit(); // 63
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
            }

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 7, 1, 0);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 2, 6, 3, 4, 5);

            if (hasFallData) // 140
            {
                packet.ReadSingle("Vertical Speed"); // 120
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Horizontal Speed"); // 132
                }
                packet.ReadUInt32("Fall time"); // 116
            }

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 0); // 56
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 2); // 58
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadUInt32("Transport Time"); // 84
                tpos.X = packet.ReadSingle(); // 64
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 3); // 59
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                tpos.O = packet.ReadSingle(); // 76
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_SET_FACING)]
        public static void HandleMoveSetFacing(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle(); // 40
            pos.X = packet.ReadSingle(); // 36
            pos.Z = packet.ReadSingle(); // 44
            guid[5] = packet.ReadBit(); // 21
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            guid[3] = packet.ReadBit(); // 19
            guid[2] = packet.ReadBit(); // 18
            var Count = packet.ReadBits("Counter", 22); // 152
            packet.ReadBit("bit 172"); // 172
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            guid[0] = packet.ReadBit(); // 16
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            packet.ReadBit("bit 148"); // 148
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[4] = packet.ReadBit(); // 20
            packet.ReadBit("bit 149"); // 149
            guid[1] = packet.ReadBit(); // 17
            guid[6] = packet.ReadBit(); // 22
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            var hasTrans = packet.ReadBit("Has transport"); // 104
            guid[7] = packet.ReadBit(); // 23

            if (hasTrans) // 104
            {
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[0] = packet.ReadBit(); // 56
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[6] = packet.ReadBit(); // 62
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[4] = packet.ReadBit(); // 60
            }

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            packet.ResetBitReader();
            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 0, 6, 3, 1, 2, 7, 4, 5);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadXORByte(transportGuid, 2); // 58
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadXORByte(transportGuid, 7); // 63
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.ReadUInt32("Transport Time"); // 84
                tpos.X = packet.ReadSingle(); // 64
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadSByte("Transport Seat"); // 80
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData) // 140
            {
                packet.ReadUInt32("Fall time"); // 116
                packet.ReadSingle("Vertical Speed"); // 120
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Fall Sin"); // 124
                }
            }

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_SET_PITCH)]
        public static void HandleMoveSetPitch(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle(); // 44
            pos.X = packet.ReadSingle(); // 36
            pos.Y = packet.ReadSingle(); // 40
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            packet.ReadBit("bit 149"); // 149
            guid[4] = packet.ReadBit(); // 20
            var hasTrans = packet.ReadBit("Has transport"); // 104
            guid[7] = packet.ReadBit(); // 23
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            guid[0] = packet.ReadBit(); // 16
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            guid[5] = packet.ReadBit(); // 21
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            packet.ReadBit("bit 172"); // 172
            guid[2] = packet.ReadBit(); // 18
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            guid[6] = packet.ReadBit(); // 22
            guid[1] = packet.ReadBit(); // 17
            packet.ReadBit("bit 148"); // 148
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            guid[3] = packet.ReadBit(); // 19

            if (hasTrans) // 104
            {
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[6] = packet.ReadBit(); // 62
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[2] = packet.ReadBit(); // 58
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[0] = packet.ReadBit(); // 56
            }

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 2, 4, 5, 6, 0);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 3, 7, 1);

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadXORByte(transportGuid, 2); // 58
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadXORByte(transportGuid, 5); // 61
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadUInt32("Transport Time"); // 84
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadSByte("Transport Seat"); // 80
                tpos.O = packet.ReadSingle(); // 76
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 7); // 63
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Fall Sin"); // 124
                }
                packet.ReadUInt32("Fall time"); // 116
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_SET_RUN_MODE)]
        public static void HandleMoveSetRunMode(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle(); // 44
            pos.Y = packet.ReadSingle(); // 40
            pos.X = packet.ReadSingle(); // 36
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[1] = packet.ReadBit(); // 17
            guid[4] = packet.ReadBit(); // 20
            guid[0] = packet.ReadBit(); // 16
            guid[3] = packet.ReadBit(); // 19
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[6] = packet.ReadBit(); // 22
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            guid[2] = packet.ReadBit(); // 18
            guid[5] = packet.ReadBit(); // 21
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            guid[7] = packet.ReadBit(); // 23
            packet.ReadBit("bit 148"); // 148
            packet.ReadBit("bit 172"); // 172
            packet.ReadBit("bit 149"); // 149
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            var hasTrans = packet.ReadBit("Has transport"); // 104

            if (hasTrans) // 104
            {
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[5] = packet.ReadBit(); // 61
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[3] = packet.ReadBit(); // 59
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[0] = packet.ReadBit(); // 56
            }

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 5, 6, 3, 7, 1, 0);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 4, 2);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 3); // 59
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 7); // 63
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadSByte("Transport Seat"); // 80
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadUInt32("Transport Time"); // 84
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 5); // 61
                tpos.Y = packet.ReadSingle(); // 68
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 128
                }
                packet.ReadUInt32("Fall time"); // 116
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_SET_WALK_MODE)]
        public static void HandleMoveSetWalkMode(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle(); // 40
            pos.X = packet.ReadSingle(); // 36
            pos.Z = packet.ReadSingle(); // 44
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            packet.ReadBit("bit 172"); // 172
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[2] = packet.ReadBit(); // 18
            guid[4] = packet.ReadBit(); // 20
            guid[5] = packet.ReadBit(); // 21
            guid[1] = packet.ReadBit(); // 17
            guid[0] = packet.ReadBit(); // 16
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            packet.ReadBit("bit 148"); // 148
            guid[7] = packet.ReadBit(); // 23
            guid[3] = packet.ReadBit(); // 19
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[6] = packet.ReadBit(); // 22
            packet.ReadBit("bit 149"); // 149
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            var hasTrans = packet.ReadBit("Has transport"); // 104

            if (hasTrans) // 104
            {
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[0] = packet.ReadBit(); // 56
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[3] = packet.ReadBit(); // 59
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[2] = packet.ReadBit(); // 58
            }

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 4, 3, 7, 2, 1, 0, 6);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 5);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadXORByte(transportGuid, 6); // 62
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 7); // 63
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.ReadUInt32("Transport Time"); // 84
                packet.ReadXORByte(transportGuid, 2); // 58
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadXORByte(transportGuid, 3); // 59
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Fall Cos"); // 128
                }
                packet.ReadSingle("Vertical Speed"); // 120
                packet.ReadUInt32("Fall time"); // 116
            }

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_ASCEND)]
        public static void HandleMoveStartAscend(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle(); // 40
            pos.X = packet.ReadSingle(); // 36
            pos.Z = packet.ReadSingle(); // 44
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[3] = packet.ReadBit(); // 19
            var hasTrans = packet.ReadBit("Has transport"); // 104
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            packet.ReadBit("bit 172"); // 172
            guid[0] = packet.ReadBit(); // 16
            guid[4] = packet.ReadBit(); // 20
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            guid[7] = packet.ReadBit(); // 23
            packet.ReadBit("bit 149"); // 149
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            guid[5] = packet.ReadBit(); // 21
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            packet.ReadBit("bit 148"); // 148
            guid[6] = packet.ReadBit(); // 22
            guid[2] = packet.ReadBit(); // 18
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            var Count = packet.ReadBits("Counter", 22); // 152
            guid[1] = packet.ReadBit(); // 17
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            var hasFallData = packet.ReadBit("Has fall data"); // 140

            if (hasTrans) // 104
            {
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[5] = packet.ReadBit(); // 61
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[1] = packet.ReadBit(); // 57
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[7] = packet.ReadBit(); // 63
            }

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 2, 5);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 1, 0, 4, 7, 6, 3);

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadUInt32("Transport Time"); // 84
                tpos.Y = packet.ReadSingle(); // 68
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadXORByte(transportGuid, 6); // 62
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadXORByte(transportGuid, 2); // 58
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 7); // 63
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasFallData) // 140
            {
                packet.ReadSingle("Vertical Speed"); // 120
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Horizontal Speed"); // 132
                }
                packet.ReadUInt32("Fall time"); // 116
            }

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_BACKWARD)]
        public static void HandleMoveStartBackWard(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle(); // 40
            pos.Z = packet.ReadSingle(); // 44
            pos.X = packet.ReadSingle(); // 36
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[7] = packet.ReadBit(); // 23
            guid[2] = packet.ReadBit(); // 18
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            packet.ReadBit("bit 172"); // 172
            guid[5] = packet.ReadBit(); // 21
            guid[3] = packet.ReadBit(); // 19
            guid[6] = packet.ReadBit(); // 22
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            guid[4] = packet.ReadBit(); // 20
            var hasTrans = packet.ReadBit("Has transport"); // 104
            guid[0] = packet.ReadBit(); // 16
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            packet.ReadBit("bit 148"); // 148
            guid[1] = packet.ReadBit(); // 17
            packet.ReadBit("bit 149"); // 149

            if (hasTrans) // 104
            {
                transportGuid[1] = packet.ReadBit(); // 57
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[7] = packet.ReadBit(); // 63
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[4] = packet.ReadBit(); // 60
            }

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            packet.ResetBitReader();
            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 1, 3, 5, 2, 0, 4, 7, 6);

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadUInt32("Transport Time"); // 84
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadSByte("Transport Seat"); // 80
                tpos.O = packet.ReadSingle(); // 76
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadXORByte(transportGuid, 0); // 56
                tpos.Y = packet.ReadSingle(); // 68
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadXORByte(transportGuid, 7); // 63
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasFallData) // 140
            {
                packet.ReadUInt32("Fall time"); // 116
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Horizontal Speed"); // 132
                }
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_DESCEND)]
        public static void HandleMoveStartDescend(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle(); // 36
            pos.Y = packet.ReadSingle(); // 40
            pos.Z = packet.ReadSingle(); // 44
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[7] = packet.ReadBit(); // 23
            guid[0] = packet.ReadBit(); // 16
            guid[4] = packet.ReadBit(); // 20
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            guid[6] = packet.ReadBit(); // 22
            guid[2] = packet.ReadBit(); // 18
            packet.ReadBit("bit 148"); // 148
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasTrans = packet.ReadBit("Has transport"); // 104
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[1] = packet.ReadBit(); // 17
            packet.ReadBit("bit 149"); // 149
            packet.ReadBit("bit 172"); // 172
            guid[3] = packet.ReadBit(); // 19
            guid[5] = packet.ReadBit(); // 21
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32

            if (hasTrans) // 104
            {
                transportGuid[0] = packet.ReadBit(); // 56
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[7] = packet.ReadBit(); // 63
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[2] = packet.ReadBit(); // 58
            }

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 4, 7, 1, 3);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 2, 6, 0, 5);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.ReadXORByte(transportGuid, 1); // 57
                tpos.Y = packet.ReadSingle(); // 68
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadUInt32("Transport Time"); // 84
                packet.ReadXORByte(transportGuid, 4); // 60
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                tpos.O = packet.ReadSingle(); // 76
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData) // 140
            {
                packet.ReadUInt32("Fall time"); // 116
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 128
                }
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_FORWARD)]
        public static void HandleMoveStartForward(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle(); // 44
            pos.X = packet.ReadSingle(); // 36
            pos.Y = packet.ReadSingle(); // 40
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            packet.ReadBit("bit 149"); // 149
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            packet.ReadBit("bit 148"); // 148
            guid[0] = packet.ReadBit(); // 16
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            var Count = packet.ReadBits("Counter", 22); // 152
            guid[4] = packet.ReadBit(); // 20
            guid[1] = packet.ReadBit(); // 17
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            guid[7] = packet.ReadBit(); // 23
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var hasTrans = packet.ReadBit("Has transport"); // 104
            guid[5] = packet.ReadBit(); // 21
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[3] = packet.ReadBit(); // 19
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            guid[2] = packet.ReadBit(); // 18
            guid[6] = packet.ReadBit(); // 22
            packet.ReadBit("bit 172"); // 172

            if (hasTrans) // 104
            {
                transportGuid[1] = packet.ReadBit(); // 57
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[6] = packet.ReadBit(); // 62
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
            }

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 1, 6, 7);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 5, 0, 3, 2, 4);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 6); // 62
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 4); // 60
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 7); // 63
                tpos.O = packet.ReadSingle(); // 76
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.ReadXORByte(transportGuid, 2); // 58
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadXORByte(transportGuid, 0); // 56
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadUInt32("Transport Time"); // 84
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Horizontal Speed"); // 132
                }
                packet.ReadUInt32("Fall time"); // 116
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_PITCH_DOWN)]
        public static void HandleMoveStartPitchDown(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle(); // 44
            pos.Y = packet.ReadSingle(); // 40
            pos.X = packet.ReadSingle(); // 36
            guid[2] = packet.ReadBit(); // 18
            guid[7] = packet.ReadBit(); // 23
            guid[3] = packet.ReadBit(); // 19
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[5] = packet.ReadBit(); // 21
            packet.ReadBit("bit 172"); // 172
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            packet.ReadBit("bit 148"); // 148
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[4] = packet.ReadBit(); // 20
            guid[1] = packet.ReadBit(); // 17
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            packet.ReadBit("bit 149"); // 149
            var hasTrans = packet.ReadBit("Has transport"); // 104
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var Count = packet.ReadBits("Counter", 22); // 152
            guid[6] = packet.ReadBit(); // 22
            guid[0] = packet.ReadBit(); // 16
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112

            if (hasTrans) // 104
            {
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[4] = packet.ReadBit(); // 60
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[0] = packet.ReadBit(); // 56
            }

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 6, 3, 5, 0, 4);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 7, 2, 1);

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Fall Cos"); // 128
                }
                packet.ReadUInt32("Fall time"); // 116
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 7); // 63
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadUInt32("Transport Time"); // 84
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 5); // 61
                tpos.X = packet.ReadSingle(); // 64
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 3); // 59
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_PITCH_UP)]
        public static void HandleMoveStartPitchUp(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle(); // 40
            pos.Z = packet.ReadSingle(); // 44
            pos.X = packet.ReadSingle(); // 36
            guid[0] = packet.ReadBit(); // 16
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            guid[3] = packet.ReadBit(); // 19
            packet.ReadBit("bit 148"); // 148
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[5] = packet.ReadBit(); // 21
            packet.ReadBit("bit 149"); // 149
            guid[2] = packet.ReadBit(); // 18
            guid[7] = packet.ReadBit(); // 23
            guid[1] = packet.ReadBit(); // 17
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasTrans = packet.ReadBit("Has transport"); // 104
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[6] = packet.ReadBit(); // 22
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            guid[4] = packet.ReadBit(); // 20
            packet.ReadBit("bit 172"); // 172
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144

            if (hasTrans) // 104
            {
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[1] = packet.ReadBit(); // 57
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[0] = packet.ReadBit(); // 56
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[3] = packet.ReadBit(); // 59
            }

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 6);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 0, 5, 7, 1, 3, 4, 2);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 3); // 59
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadXORByte(transportGuid, 0); // 56
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 5); // 61
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 1); // 57
                tpos.X = packet.ReadSingle(); // 64
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadUInt32("Transport Time"); // 84
                tpos.Z = packet.ReadSingle(); // 72
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasFallData) // 140
            {
                packet.ReadSingle("Vertical Speed"); // 120
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Fall Sin"); // 124
                }
                packet.ReadUInt32("Fall time"); // 116
            }

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_STRAFE_LEFT)]
        public static void HandleMoveStartStrafeLeft(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle(); // 40
            pos.Z = packet.ReadSingle(); // 44
            pos.X = packet.ReadSingle(); // 36
            guid[0] = packet.ReadBit(); // 16
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            guid[3] = packet.ReadBit(); // 19
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            packet.ReadBit("bit 148"); // 148
            guid[2] = packet.ReadBit(); // 18
            packet.ReadBit("bit 149"); // 149
            var hasTrans = packet.ReadBit("Has transport"); // 104
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            guid[5] = packet.ReadBit(); // 21
            var Count = packet.ReadBits("Counter", 22); // 152
            packet.ReadBit("bit 172"); // 172
            guid[4] = packet.ReadBit(); // 20
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            guid[7] = packet.ReadBit(); // 23
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[1] = packet.ReadBit(); // 17
            guid[6] = packet.ReadBit(); // 22
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24

            if (hasTrans) // 104
            {
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[5] = packet.ReadBit(); // 61
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[4] = packet.ReadBit(); // 60
            }

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 0, 2);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 3, 5, 1, 7, 4, 6);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 2); // 58
                tpos.Z = packet.ReadSingle(); // 72
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 3); // 59
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadXORByte(transportGuid, 5); // 61
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 1); // 57
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadUInt32("Transport Time"); // 84
                packet.ReadSByte("Transport Seat"); // 80
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasFallData) // 140
            {
                packet.ReadUInt32("Fall time"); // 116
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 128
                }
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_STRAFE_RIGHT)]
        public static void HandleMoveStartStrafeRight(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle(); // 40
            pos.X = packet.ReadSingle(); // 36
            pos.Z = packet.ReadSingle(); // 44
            guid[0] = packet.ReadBit(); // 16
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            var Count = packet.ReadBits("Counter", 22); // 152
            guid[7] = packet.ReadBit(); // 23
            guid[6] = packet.ReadBit(); // 22
            guid[4] = packet.ReadBit(); // 20
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[5] = packet.ReadBit(); // 21
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            guid[3] = packet.ReadBit(); // 19
            packet.ReadBit("bit 149"); // 149
            var hasTrans = packet.ReadBit("Has transport"); // 104
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[1] = packet.ReadBit(); // 17
            packet.ReadBit("bit 172"); // 172
            guid[2] = packet.ReadBit(); // 18
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            packet.ReadBit("bit 148"); // 148
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasTrans) // 104
            {
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[4] = packet.ReadBit(); // 60
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[7] = packet.ReadBit(); // 63
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
            }

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 6, 7, 0, 4, 1);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 2, 3, 5);

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 3); // 59
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 7); // 63
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadUInt32("Transport Time"); // 84
                tpos.O = packet.ReadSingle(); // 76
                tpos.Y = packet.ReadSingle(); // 68
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 4); // 60
                tpos.X = packet.ReadSingle(); // 64
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasFallData) // 140
            {
                packet.ReadSingle("Vertical Speed"); // 120
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 128
                }
                packet.ReadUInt32("Fall time"); // 116
            }

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_SWIM)]
        public static void HandleMoveStartSwim(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle(); // 36
            pos.Y = packet.ReadSingle(); // 40
            pos.Z = packet.ReadSingle(); // 44
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            var hasTrans = packet.ReadBit("Has transport"); // 104
            guid[2] = packet.ReadBit(); // 18
            packet.ReadBit("bit 172"); // 172
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            guid[3] = packet.ReadBit(); // 19
            packet.ReadBit("bit 149"); // 149
            guid[6] = packet.ReadBit(); // 22
            guid[1] = packet.ReadBit(); // 17
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            packet.ReadBit("bit 148"); // 148
            guid[7] = packet.ReadBit(); // 23
            guid[0] = packet.ReadBit(); // 16
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[5] = packet.ReadBit(); // 21
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[4] = packet.ReadBit(); // 20

            if (hasTrans) // 104
            {
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[4] = packet.ReadBit(); // 60
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[3] = packet.ReadBit(); // 59
            }

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            packet.ResetBitReader();

            packet.ParseBitStream(guid, 5, 0);

            for (var cnt = 0; cnt < Count; cnt++) // 152
                packet.ReadInt32("Dword 156", cnt); // 156

            packet.ParseBitStream(guid, 7, 3, 4, 1, 6, 2);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 3); // 59
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadXORByte(transportGuid, 0); // 56
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadSByte("Transport Seat"); // 80
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 2); // 58
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadUInt32("Transport Time"); // 84
                tpos.O = packet.ReadSingle(); // 76
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Horizontal Speed"); // 132
                }
                packet.ReadUInt32("Fall time"); // 116
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_TURN_LEFT)]
        public static void HandleMoveStartTurnLeft(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle(); // 44
            pos.X = packet.ReadSingle(); // 36
            pos.Y = packet.ReadSingle(); // 40
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[4] = packet.ReadBit(); // 20
            guid[5] = packet.ReadBit(); // 21
            packet.ReadBit("bit 148"); // 148
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            packet.ReadBit("bit 172"); // 172
            packet.ReadBit("bit 149"); // 149
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[3] = packet.ReadBit(); // 19
            guid[1] = packet.ReadBit(); // 17
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[0] = packet.ReadBit(); // 16
            guid[2] = packet.ReadBit(); // 18
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasTrans = packet.ReadBit("Has transport"); // 104
            guid[7] = packet.ReadBit(); // 23
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            guid[6] = packet.ReadBit(); // 22

            if (hasTrans) // 104
            {
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[7] = packet.ReadBit(); // 63
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[1] = packet.ReadBit(); // 57
            }

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 7, 3, 6, 4, 1);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 5, 0, 2);

            if (hasFallData) // 140
            {
                packet.ReadUInt32("Fall time"); // 116
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Fall Cos"); // 128
                }
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 3); // 59
                tpos.X = packet.ReadSingle(); // 64
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadXORByte(transportGuid, 5); // 61
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 0); // 56
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadUInt32("Transport Time"); // 84
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_START_TURN_RIGHT)]
        public static void HandleMoveStartTurnRight(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle(); // 36
            pos.Z = packet.ReadSingle(); // 44
            pos.Y = packet.ReadSingle(); // 40
            packet.ReadBit("bit 148"); // 148
            packet.ReadBit("bit 172"); // 172
            guid[1] = packet.ReadBit(); // 17
            guid[0] = packet.ReadBit(); // 16
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[2] = packet.ReadBit(); // 18
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            guid[4] = packet.ReadBit(); // 20
            guid[6] = packet.ReadBit(); // 22
            guid[5] = packet.ReadBit(); // 21
            guid[3] = packet.ReadBit(); // 19
            packet.ReadBit("bit 149"); // 149
            var hasTrans = packet.ReadBit("Has transport"); // 104
            guid[7] = packet.ReadBit(); // 23

            if (hasTrans) // 104
            {
                transportGuid[2] = packet.ReadBit(); // 58
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[4] = packet.ReadBit(); // 60
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[1] = packet.ReadBit(); // 57
            }

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            packet.ResetBitReader();

            packet.ParseBitStream(guid, 5, 1, 3, 0, 4, 2, 6);

            for (var cnt = 0; cnt < Count; cnt++) // 152
                packet.ReadInt32("Dword 156", cnt); // 156

            packet.ParseBitStream(guid, 7);

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Sin"); // 124
                }
                packet.ReadSingle("Vertical Speed"); // 120
                packet.ReadUInt32("Fall time"); // 116
            }

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadXORByte(transportGuid, 3); // 59
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadXORByte(transportGuid, 1); // 57
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 0); // 56
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadUInt32("Transport Time"); // 84
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 6); // 62
                tpos.O = packet.ReadSingle(); // 76
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP)]
        public static void HandleMoveStop(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle(); // 36
            pos.Y = packet.ReadSingle(); // 40
            pos.Z = packet.ReadSingle(); // 44
            guid[5] = packet.ReadBit(); // 21
            guid[2] = packet.ReadBit(); // 18
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            guid[0] = packet.ReadBit(); // 16
            packet.ReadBit("bit 172"); // 172
            packet.ReadBit("bit 148"); // 148
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[1] = packet.ReadBit(); // 17
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            guid[3] = packet.ReadBit(); // 19
            guid[4] = packet.ReadBit(); // 20
            var hasTrans = packet.ReadBit("Has transport"); // 104
            packet.ReadBit("bit 149"); // 149
            guid[6] = packet.ReadBit(); // 22
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            guid[7] = packet.ReadBit(); // 23

            if (hasTrans) // 104
            {
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[3] = packet.ReadBit(); // 59
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[6] = packet.ReadBit(); // 62
            }

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            packet.ResetBitReader();

            packet.ParseBitStream(guid, 0, 3);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 6, 1, 4, 2, 5, 7);

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasFallData) // 140
            {
                packet.ReadSingle("Vertical Speed"); // 120
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Fall Sin"); // 124
                }
                packet.ReadUInt32("Fall time"); // 116
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadUInt32("Transport Time"); // 84
                packet.ReadXORByte(transportGuid, 3); // 59
                tpos.O = packet.ReadSingle(); // 76
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 4); // 60
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadSByte("Transport Seat"); // 80
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 5); // 61
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP_ASCEND)]
        public static void HandleMoveStopAscend(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle(); // 44
            pos.X = packet.ReadSingle(); // 36
            pos.Y = packet.ReadSingle(); // 40
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[0] = packet.ReadBit(); // 16
            guid[3] = packet.ReadBit(); // 19
            guid[7] = packet.ReadBit(); // 23
            guid[2] = packet.ReadBit(); // 18
            guid[6] = packet.ReadBit(); // 22
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            var hasTrans = packet.ReadBit("Has transport"); // 104
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            packet.ReadBit("bit 148"); // 148
            packet.ReadBit("bit 172"); // 172
            guid[4] = packet.ReadBit(); // 20
            packet.ReadBit("bit 149"); // 149
            guid[5] = packet.ReadBit(); // 21
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[1] = packet.ReadBit(); // 17
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144

            if (hasTrans) // 104
            {
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[1] = packet.ReadBit(); // 57
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[7] = packet.ReadBit(); // 63
            }

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            packet.ResetBitReader();

            packet.ParseBitStream(guid, 0);

            for (var cnt = 0; cnt < Count; cnt++) // 152
                packet.ReadInt32("Dword 156", cnt); // 156

            packet.ParseBitStream(guid, 4, 5, 1, 7, 6, 3, 2);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 5); // 61
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 3); // 59
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                tpos.X = packet.ReadSingle(); // 64
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadXORByte(transportGuid, 2); // 58
                tpos.Z = packet.ReadSingle(); // 72
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadUInt32("Transport Time"); // 84
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 128
                }
                packet.ReadUInt32("Fall time"); // 116
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP_PITCH)]
        public static void HandleMoveStopPitch(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Y = packet.ReadSingle(); // 40
            pos.X = packet.ReadSingle(); // 36
            pos.Z = packet.ReadSingle(); // 44
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            guid[0] = packet.ReadBit(); // 16
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            packet.ReadBit("bit 148"); // 148
            guid[2] = packet.ReadBit(); // 18
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            guid[3] = packet.ReadBit(); // 19
            guid[7] = packet.ReadBit(); // 23
            guid[5] = packet.ReadBit(); // 21
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            packet.ReadBit("bit 172"); // 172
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            var hasTrans = packet.ReadBit("Has transport"); // 104
            guid[6] = packet.ReadBit(); // 22
            guid[4] = packet.ReadBit(); // 20
            packet.ReadBit("bit 149"); // 149
            guid[1] = packet.ReadBit(); // 17

            if (hasTrans) // 104
            {
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[5] = packet.ReadBit(); // 61
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[1] = packet.ReadBit(); // 57
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[2] = packet.ReadBit(); // 58
            }

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            packet.ResetBitReader();

            packet.ParseBitStream(guid, 0, 6, 7, 1);

            for (var cnt = 0; cnt < Count; cnt++) // 152
                packet.ReadInt32("Dword 156", cnt); // 156

            packet.ParseBitStream(guid, 5, 3, 4, 2);

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Sin"); // 124
                }
                packet.ReadUInt32("Fall time"); // 116
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 1); // 57
                tpos.X = packet.ReadSingle(); // 64
                tpos.Z = packet.ReadSingle(); // 72
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.ReadXORByte(transportGuid, 0); // 56
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadSByte("Transport Seat"); // 80
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadUInt32("Transport Time"); // 84
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 7); // 63
                tpos.O = packet.ReadSingle(); // 76
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP_STRAFE)]
        public static void HandleMoveStopStrafe(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.ReadSingle(); // 44
            pos.X = packet.ReadSingle(); // 36
            pos.Y = packet.ReadSingle(); // 40
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[6] = packet.ReadBit(); // 22
            var hasTrans = packet.ReadBit("Has transport"); // 104
            packet.ReadBit("bit 172"); // 172
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            guid[4] = packet.ReadBit(); // 20
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            guid[5] = packet.ReadBit(); // 21
            guid[3] = packet.ReadBit(); // 19
            guid[2] = packet.ReadBit(); // 18
            var Count = packet.ReadBits("Counter", 22); // 152
            packet.ReadBit("bit 149"); // 149
            guid[7] = packet.ReadBit(); // 23
            guid[0] = packet.ReadBit(); // 16
            packet.ReadBit("bit 148"); // 148
            guid[1] = packet.ReadBit(); // 17

            if (hasTrans) // 104
            {
                transportGuid[7] = packet.ReadBit(); // 63
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[6] = packet.ReadBit(); // 62
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[0] = packet.ReadBit(); // 56
            }

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            packet.ResetBitReader();

            packet.ParseBitStream(guid, 5, 3);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 2, 0, 1, 6, 4, 7);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadXORByte(transportGuid, 0); // 56
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadUInt32("Transport Time"); // 84
                tpos.Y = packet.ReadSingle(); // 68
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 4); // 60
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadSByte("Transport Seat"); // 80
                tpos.X = packet.ReadSingle(); // 64
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadXORByte(transportGuid, 5); // 61
                tpos.O = packet.ReadSingle(); // 76
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Horizontal Speed"); // 132
                }
                packet.ReadUInt32("Fall time"); // 116
                packet.ReadSingle("Vertical Speed"); // 120
            }

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP_TURN)]
        public static void HandleMoveStopTurn(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle(); // 36
            pos.Z = packet.ReadSingle(); // 44
            pos.Y = packet.ReadSingle(); // 40
            var hasTrans = packet.ReadBit("Has transport"); // 104
            var Count = packet.ReadBits("Counter", 22); // 152
            packet.ReadBit("bit 149"); // 149
            guid[4] = packet.ReadBit(); // 20
            guid[5] = packet.ReadBit(); // 21
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[3] = packet.ReadBit(); // 19
            packet.ReadBit("bit 172"); // 172
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            guid[0] = packet.ReadBit(); // 16
            guid[1] = packet.ReadBit(); // 17
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            guid[6] = packet.ReadBit(); // 22
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[2] = packet.ReadBit(); // 18
            packet.ReadBit("bit 148"); // 148
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[7] = packet.ReadBit(); // 23
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32

            if (hasMovementFlags2)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            if (hasTrans)
            {
                transportGuid[1] = packet.ReadBit(); // 57
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[0] = packet.ReadBit(); // 56
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[4] = packet.ReadBit(); // 60
            }

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasFallData)
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 2, 3, 6);

            if (Count > 0)
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 0, 5, 4, 7, 1);

            if (hasTrans)
            {
                var tpos = new Vector4();
                packet.ReadUInt32("Transport Time"); // 84
                if (hasTransTime3)
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.ReadSByte("Transport Seat"); // 80
                tpos.Y = packet.ReadSingle(); // 68
                tpos.X = packet.ReadSingle(); // 64
                if (hasTransTime2)
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 3); // 59
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadXORByte(transportGuid, 0); // 56
                tpos.Z = packet.ReadSingle(); // 72
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Fall Sin"); // 128
                    packet.ReadSingle("Fall Cos"); // 124
                    packet.ReadSingle("Horizontal Speed"); // 132
                }
                packet.ReadSingle("Vertical Speed"); // 120
                packet.ReadUInt32("Fall time"); // 116
            }

            if (hasSpline)
                packet.ReadInt32("Spline"); // 168

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.MSG_MOVE_STOP_SWIM)]
        public static void HandleMoveStopSwim(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.X = packet.ReadSingle(); // 36
            pos.Z = packet.ReadSingle(); // 44
            pos.Y = packet.ReadSingle(); // 40
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            guid[6] = packet.ReadBit(); // 22
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            packet.ReadBit("bit 172"); // 172
            packet.ReadBit("bit 149"); // 149
            packet.ReadBit("bit 148"); // 148
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            guid[4] = packet.ReadBit(); // 20
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            var hasTrans = packet.ReadBit("Has transport"); // 104
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            guid[1] = packet.ReadBit(); // 17
            var Count = packet.ReadBits("Counter", 22); // 152
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[7] = packet.ReadBit(); // 23
            guid[0] = packet.ReadBit(); // 16
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            guid[3] = packet.ReadBit(); // 19
            guid[5] = packet.ReadBit(); // 21
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            guid[2] = packet.ReadBit(); // 18

            if (hasTrans) // 104
            {
                transportGuid[1] = packet.ReadBit(); // 57
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[2] = packet.ReadBit(); // 58
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[7] = packet.ReadBit(); // 63
                transportGuid[3] = packet.ReadBit(); // 59
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[0] = packet.ReadBit(); // 56
            }

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            packet.ResetBitReader();

            packet.ParseBitStream(guid, 7, 6, 1, 5, 4, 3);

            for (var cnt = 0; cnt < Count; cnt++) // 152
                packet.ReadInt32("Dword 156", cnt); // 156

            packet.ParseBitStream(guid, 0, 2);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                packet.ReadUInt32("Transport Time"); // 84
                packet.ReadXORByte(transportGuid, 1); // 57
                packet.ReadXORByte(transportGuid, 2); // 58
                tpos.Z = packet.ReadSingle(); // 72
                tpos.X = packet.ReadSingle(); // 64
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadSByte("Transport Seat"); // 80
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 5); // 61
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Fall Sin"); // 124
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 128
                }
                packet.ReadSingle("Vertical Speed"); // 120
                packet.ReadUInt32("Fall time"); // 116
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_TELEPORT_ACK)]
        public static void HandleMoveTeleportAck(Packet packet)
        {
            packet.ReadInt32("Time");
            packet.ReadInt32("Flags");

            var guid = packet.StartBitStream(0, 7, 3, 5, 4, 6, 1, 2);
            packet.ParseBitStream(guid, 4, 1, 6, 7, 0, 2, 5, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_FLIGHT_SPLINE_SYNC)]
        public static void HandleFlightSplineSync(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 4, 2, 7, 1, 3, 0, 5);
            packet.ParseBitStream(guid, 2, 7, 5, 1, 4, 6, 0, 3);
            packet.ReadSingle("Duration modifier");

            packet.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_RUN_SPEED)]
        public static void HandleSplineSetRunSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(3, 0, 1, 4, 7, 5, 6, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadSingle("Speed");
            packet.ReadXORBytes(guid, 1, 5, 3, 7, 6, 2, 0);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_UNK_09DB)]
        public static void HandleUnk09DB(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.Unk09DB);
        }

        [Parser(Opcode.CMSG_UNK_00F2)]
        public static void HandleUnk00F2(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            packet.ReadInt32("MCounter"); // 176*4
            pos.X = packet.ReadSingle(); // 36
            pos.Y = packet.ReadSingle(); // 40
            pos.Z = packet.ReadSingle(); // 44
            guid[5] = packet.ReadBit(); // 21
            var hasSpline = !packet.ReadBit("has no Spline"); // 168
            var hasTime = !packet.ReadBit("Has no timestamp"); // 32
            var hasMovementFlags2 = !packet.ReadBit("has no MovementFlags2"); // 28
            packet.ReadBit("bit 172"); // 172
            var hasTrans = packet.ReadBit("Has transport"); // 104
            var hasSplineElev = !packet.ReadBit("Has no Spline Elevation"); // 144
            guid[3] = packet.ReadBit(); // 19
            var hasPitch = !packet.ReadBit("Has no pitch"); // 112
            var Count = packet.ReadBits("Counter", 22); // 152
            guid[1] = packet.ReadBit(); // 17
            var hasMovementFlags = !packet.ReadBit("has no MovementFlags"); // 24
            guid[7] = packet.ReadBit(); // 23
            guid[6] = packet.ReadBit(); // 22
            guid[4] = packet.ReadBit(); // 20
            guid[0] = packet.ReadBit(); // 16
            var hasO = !packet.ReadBit("Has no Orient"); // 48
            guid[2] = packet.ReadBit(); // 18
            packet.ReadBit("bit 149"); // 149
            var hasFallData = packet.ReadBit("Has fall data"); // 140
            packet.ReadBit("bit 148"); // 148

            if (hasMovementFlags) // 24
                packet.ReadEnum<MovementFlag>("Movement Flags", 30); // 24

            if (hasTrans) // 104
            {
                transportGuid[2] = packet.ReadBit(); // 58
                hasTransTime3 = packet.ReadBit("hasTransTime3"); // 100
                transportGuid[3] = packet.ReadBit(); // 59
                transportGuid[1] = packet.ReadBit(); // 57
                transportGuid[4] = packet.ReadBit(); // 60
                transportGuid[6] = packet.ReadBit(); // 62
                transportGuid[0] = packet.ReadBit(); // 56
                hasTransTime2 = packet.ReadBit("hasTransTime2"); // 92
                transportGuid[5] = packet.ReadBit(); // 61
                transportGuid[7] = packet.ReadBit(); // 63
            }

            if (hasFallData) // 140
                hasFallDirection = packet.ReadBit("Has Fall Direction"); // 136

            if (hasMovementFlags2) // 28
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13); // 28

            packet.ResetBitReader();

            packet.ParseBitStream(guid, 4, 1, 0, 2, 5, 3);

            if (Count > 0) // 152
                for (var cnt = 0; cnt < Count; cnt++)
                {
                    packet.ReadInt32("Dword 156", cnt); // 156
                }

            packet.ParseBitStream(guid, 7, 6);

            if (hasTrans) // 104
            {
                var tpos = new Vector4();
                if (hasTransTime3) // 100
                    packet.ReadUInt32("Transport Time 3"); // 96
                if (hasTransTime2) // 92
                    packet.ReadUInt32("Transport Time 2"); // 88
                packet.ReadUInt32("Transport Time"); // 84
                tpos.X = packet.ReadSingle(); // 64
                tpos.Y = packet.ReadSingle(); // 68
                packet.ReadXORByte(transportGuid, 7); // 63
                packet.ReadSByte("Transport Seat"); // 80
                packet.ReadXORByte(transportGuid, 1); // 57
                tpos.Z = packet.ReadSingle(); // 72
                tpos.O = packet.ReadSingle(); // 76
                packet.ReadXORByte(transportGuid, 6); // 62
                packet.ReadXORByte(transportGuid, 2); // 58
                packet.ReadXORByte(transportGuid, 3); // 59
                packet.ReadXORByte(transportGuid, 0); // 56
                packet.ReadXORByte(transportGuid, 4); // 60
                packet.ReadXORByte(transportGuid, 5); // 61
                packet.WriteGuid("Transport Guid", transportGuid);
                packet.WriteLine("Transport Position: {0}", tpos);
            }

            if (hasFallData) // 140
            {
                if (hasFallDirection) // 136
                {
                    packet.ReadSingle("Horizontal Speed"); // 132
                    packet.ReadSingle("Fall Cos"); // 128
                    packet.ReadSingle("Fall Sin"); // 124
                }
                packet.ReadSingle("Vertical Speed"); // 120
                packet.ReadUInt32("Fall time"); // 116
            }

            if (hasSplineElev)
                packet.ReadSingle("Spline elevation"); // 144

            if (hasPitch)
                packet.ReadSingle("Pitch"); // 112

            if (hasTime)
                packet.ReadUInt32("Timestamp"); // 32

            if (hasO)
                pos.O = packet.ReadSingle(); // 48

            if (hasSpline) // 168
                packet.ReadInt32("Spline"); // 168

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.CMSG_MOVE_WATER_WALK_ACK)]
        public static void HandleWaterWalkAck(Packet packet)
        {
            ReadPlayerMovementInfo(ref packet, info.MovementWaterWalkAck);
        }

        [Parser(Opcode.SMSG_BINDPOINTUPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            packet.ReadVector3("Position");
            packet.ReadEntry<Int32>(StoreNameType.Zone, "Zone Id");
            packet.ReadEntry<Int32>(StoreNameType.Map, "Map Id");
        }

        [Parser(Opcode.SMSG_MOVE_SET_ACTIVE_MOVER)]
        public static void HandleMoveSetActiveMover(Packet packet)
        {
            var guid = packet.StartBitStream(5, 1, 4, 2, 3, 7, 0, 6);
            packet.ParseBitStream(guid, 4, 6, 2, 0, 3, 7, 5, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_SPEED)] // sub_C8A820
        public static void HandleMoveSetFlightSpeed(Packet packet)
        {
            packet.ReadSingle("Speed");
            packet.ReadInt32("MCounter");
            var guid = packet.StartBitStream(6, 5, 0, 4, 1, 7, 3, 2);
            packet.ParseBitStream(guid, 0, 7, 4, 5, 6, 2, 3, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_RUN_SPEED)]
        public static void HandleMoveSetRunSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(1, 7, 4, 2, 5, 3, 6, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadInt32("MCounter");
            packet.ParseBitStream(guid, 7, 3, 0);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 2, 4, 6, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_RUN_BACK_SPEED)] // sub_C8977A
        public static void HandleMoveSetRunBackSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(7, 1, 0, 2, 4, 3, 6, 5);
            packet.ReadInt32("MCounter");
            packet.ParseBitStream(guid, 0, 3, 7, 5, 2, 4, 1);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 6);
            packet.WriteGuid("Guid", guid);
        }


        [Parser(Opcode.SMSG_MOVE_SET_SWIM_SPEED)]
        public static void HandleMoveSetswimSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(5, 0, 6, 3, 7, 2, 4, 1);
            packet.ReadInt32("MCounter"); // 28
            packet.ParseBitStream(guid, 1, 3);
            packet.ReadSingle("Speed"); // 24
            packet.ParseBitStream(guid, 6, 7, 0, 5, 2, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_WALK_SPEED)] // sub_C8F849
        public static void HandleMoveSetWalkSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 3, 1, 2, 0, 4, 5);
            packet.ParseBitStream(guid, 5, 6);
            packet.ReadInt32("MCounter");
            packet.ReadXORByte(guid, 4);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 2, 3, 0, 1, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_TELEPORT)] // C90474
        public static void HandleMoveTeleport(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];
            var pos = new Vector4();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var unk56 = packet.ReadBit("unk56");
            guid[4] = packet.ReadBit();

            if (unk56)
                guid2 = packet.StartBitStream(1, 3, 6, 4, 5, 2, 0, 7);

            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var unk27 = packet.ReadBit("unk27");

            if (unk27)
            {
                packet.ReadBit("unk25");
                packet.ReadBit("unk26");

                packet.ReadByte("unk24");
            }

            if (unk56)
            {
                packet.ParseBitStream(guid2, 4, 3, 7, 1, 6, 0, 2, 5);
                packet.WriteGuid("Guid2", guid2);
            }

            packet.ParseBitStream(guid, 4, 7);
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            packet.ParseBitStream(guid, 2, 3, 5);
            pos.X = packet.ReadSingle();
            packet.ReadInt32("Count");
            packet.ParseBitStream(guid, 0, 6, 1);
            pos.O = packet.ReadSingle();
            packet.WriteLine("Pos: {0}", pos);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_WATER_WALK)]
        public static void HandleSMoveWaterWalk(Packet packet)
        {
            var guid = packet.StartBitStream(2, 0, 4, 5, 3, 7, 1, 6);
            packet.ParseBitStream(guid, 4, 7, 0, 1, 6, 2, 3, 5);
            packet.ReadInt32("MCounter");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MONSTER_MOVE)]
        public static void HandleMonsterMove(Packet packet)
        {
            var guid3 = new byte[8];
            var target = new byte[8];
            var pos = new Vector3();

            pos.Z = packet.ReadSingle(); // 24
            pos.X = packet.ReadSingle(); // 16
            packet.ReadInt32("Spline ID");
            pos.Y = packet.ReadSingle(); //20
            packet.WriteLine("Pos: {0}", pos);
            var transportPos = new Vector3
            {
                Y = packet.ReadSingle(), // 48
                Z = packet.ReadSingle(), // 52
                X = packet.ReadSingle(), // 44
            };
            packet.WriteLine("transportPos: {0}", transportPos);
            var hasAngle = !packet.ReadBit("!hasAngle");
            guid3[0] = packet.ReadBit();
            var splineType = packet.ReadEnum<SplineType>("Spline Type", 3);
            if (splineType == SplineType.FacingTarget)
            {
                target = packet.StartBitStream(6, 4, 3, 0, 5, 7, 1, 2); // 184
            }
            var unk76 = !packet.ReadBit("!unk76");
            var unk69 = !packet.ReadBit("!unk69");
            var unk120 = -packet.ReadBit("unk120");

            var uncompressedSplineCount = packet.ReadBits("uncompressedSplineCount", 20);
            var hasSplineFlags = !packet.ReadBit("!hasSplineFlags");
            guid3[3] = packet.ReadBit();
            var unk108 = !packet.ReadBit("!unk108");
            var unk88 = !packet.ReadBit("!unk88");
            var unk109 = !packet.ReadBit("!unk109");
            var hasDutation = !packet.ReadBit("!hasduration");
            guid3[7] = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            var unk72 = !packet.ReadBit("!unk72");
            guid3[5] = packet.ReadBit();
            var compressedSplineCount = packet.ReadBits("compressedSplineCount", 22);
            guid3[6] = packet.ReadBit();
            var unk112 = packet.ReadBit("!unk112") ? 0u : 1u;
            var guid2 = packet.StartBitStream(7, 1, 3, 0, 6, 4, 5, 2); // 112
            var unk176 = packet.ReadBit("unk176");
            var unk78 = 0u;
            var count140 = 0u;
            if (unk176)
            {
                unk78 = packet.ReadBits("unk78*2", 2);
                count140 = packet.ReadBits("count140", 22);
            }
            var unk56 = packet.ReadBit("unk56");
            guid3[2] = packet.ReadBit();
            guid3[1] = packet.ReadBit();
            for (var i = 0; i < compressedSplineCount; i++)
            {
                var vec = packet.ReadPackedVector3();
                vec.X += pos.X;
                vec.Y += pos.Y;
                vec.Z += pos.Z;
                packet.WriteLine("[{0}] Waypoint: {1}", i, vec); // not completed
            }
            packet.ParseBitStream(guid3, 1);
            packet.ParseBitStream(guid2, 6, 4, 1, 7, 0, 3, 5, 2);
            packet.WriteGuid("Guid2", guid2);

            for (var i = 0; i < uncompressedSplineCount; i++)
            {
                var point = new Vector3
                {
                    Y = packet.ReadSingle(), // 100
                    X = packet.ReadSingle(), // 96
                    Z = packet.ReadSingle(), // 104
                };
                packet.WriteLine("[{0}] Point: {1}", i, point);
            }

            if (unk72)
                packet.ReadInt32("unk72");

            if (splineType == SplineType.FacingTarget)
            {
                packet.ParseBitStream(target, 5, 7, 0, 4, 3, 2, 6, 1);
                packet.WriteGuid("Target", target);
            }

            packet.ParseBitStream(guid3, 5);

            if (hasAngle)
                packet.ReadSingle("Angle");

            if (unk176)
            {
                for (var i = 0; i < count140; i++)
                {
                    packet.ReadInt16("unk146", i);
                    packet.ReadInt16("unk144", i);
                }
                packet.ReadSingle("unka8h*4");
                packet.ReadInt16("unk82*2");
                packet.ReadInt16("unk86*2");
                packet.ReadSingle("unka0h*4");
            }

            if (unk76)
                packet.ReadInt32("unk76");


            if (splineType == SplineType.FacingAngle)
                packet.ReadSingle("Facing Angle");

            packet.ParseBitStream(guid3, 3);

            if (hasSplineFlags)
                packet.ReadEnum<SplineFlag>("Spline Flags", TypeCode.UInt32);

            if (unk69)
                packet.ReadByte("unk69");

            packet.ParseBitStream(guid3, 6);

            if (unk109)
                packet.ReadByte("unk109");

            if (splineType == SplineType.FacingSpot)
            {
                var facingSpot = new Vector3
                {
                    X = packet.ReadSingle(),
                    Y = packet.ReadSingle(),
                    Z = packet.ReadSingle(),
                };
                packet.WriteLine("Facing spot: {0}", facingSpot);
            }

            packet.ParseBitStream(guid3, 0);

            if (unk120 != -1)
                packet.ReadByte("unk120");

            if (unk108)
                packet.ReadByte("unk108");

            packet.ParseBitStream(guid3, 7, 2);

            if (unk88)
                packet.ReadInt32("unk88");

            packet.ParseBitStream(guid3, 4);

            if (hasDutation)
                packet.ReadInt32("Spline Duration");

            packet.WriteGuid("Unit", guid3);

            var guidUnit = new Guid(BitConverter.ToUInt64(guid3, 0));

            if (Storage.Objects != null && Storage.Objects.ContainsKey(guidUnit))
            {
                var obj = Storage.Objects[guidUnit].Item1;
                UpdateField uf;
                if (obj.UpdateFields != null && obj.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_FLAGS), out uf))
                    if ((uf.UInt32Value & (uint)UnitFlags.IsInCombat) == 0) // movement could be because of aggro so ignore that
                        obj.Movement.HasWpsOrRandMov = true;
            }
        }

        [Parser(Opcode.SMSG_SET_PHASE_SHIFT)]
        public static void HandlePhaseShift(Packet packet)
        {
            CoreParsers.MovementHandler.ActivePhases.Clear();

            var guid = packet.StartBitStream(0, 3, 1, 4, 6, 2, 7, 5);
            packet.ParseBitStream(guid, 4, 3, 2);

            var count = packet.ReadUInt32() / 2;
            packet.AddValue("Phases count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadInt16("Phase id", i); // if + Phase.dbc, if - duno atm

            packet.ParseBitStream(guid, 0, 6);

            count = packet.ReadUInt32() / 2;
            packet.AddValue("Inactive Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadEntry<Int16>(StoreNameType.Map, "Inactive Terrain swap", i); // Map.dbc, all possible terrainswaps

            packet.ParseBitStream(guid, 1, 7);

            count = packet.ReadUInt32() / 2;
            packet.AddValue("WorldMapArea swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadUInt16("WorldMapArea swap", i); // WorldMapArea.dbc

            count = packet.ReadUInt32() / 2;
            packet.AddValue("Active Terrain swap count", count);
            for (var i = 0; i < count; ++i)
                packet.ReadEntry<Int16>(StoreNameType.Map, "Active Terrain swap", i); // Map.dbc, all active terrainswaps

            packet.ParseBitStream(guid, 5);
            packet.WriteGuid("GUID", guid);

            packet.ReadUInt32("Flags");
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_FLIGHT_SPEED)]
        public static void HandleSplineMoveSetFlightSpeed(Packet packet)
        {
            packet.ReadSingle("Speed"); // 24
            var guid = packet.StartBitStream(1, 4, 7, 3, 2, 6, 5, 0);
            packet.ParseBitStream(guid, 5, 1, 0, 6, 2, 4, 7, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_RUN_BACK_SPEED)]
        public static void HandleUnk1F9F(Packet packet)
        {
            var guid = packet.StartBitStream(7, 4, 0, 3, 2, 5, 6, 1);
            packet.ParseBitStream(guid, 6, 4, 1, 5, 2, 3, 7);
            packet.ReadSingle("Speed"); // 24
            packet.ParseBitStream(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_SWIM_SPEED)]
        public static void HandleSplinemoveSetSwimSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(5, 6, 7, 3, 4, 2, 1, 0);
            packet.ParseBitStream(guid, 4, 1, 6, 7, 3);
            packet.ReadSingle("Speed"); // 24
            packet.ParseBitStream(guid, 5, 0, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_WALK_SPEED)]
        public static void HandleSplineMoveSetWalkSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(4, 1, 7, 6, 3, 2, 5, 0);
            packet.ParseBitStream(guid, 2, 3, 1, 0, 6, 5, 4, 7);
            packet.WriteGuid("Guid", guid);
            packet.ReadSingle("Speed");
        }
    }
}
