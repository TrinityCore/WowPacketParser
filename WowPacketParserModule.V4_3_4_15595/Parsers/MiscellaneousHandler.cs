using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using MovementFlag = WowPacketParser.Enums.v4.MovementFlag;
using MovementFlag2 = WowPacketParser.Enums.v4.MovementFlag2;

namespace WowPacketParserModule.V4_3_4_15595.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_NOTIFICATION)]
        public static void HandleNotification(Packet packet)
        {
            var length = packet.ReadBits(13);
            packet.ReadWoWString("Message", length);
        }

        [Parser(Opcode.CMSG_REQUEST_HONOR_STATS)]
        public static void HandleRequestHonorStats(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 7, 3, 2, 4, 0, 6);

            packet.ParseBitStream(guid, 4, 7, 0, 5, 1, 6, 2, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UPDATE_MISSILE_TRAJECTORY)]
        public static void HandleUpdateMissileTrajectory(Packet packet)
        {
            var position1 = new Vector3();
            var position2 = new Vector3();

            position1.Z = packet.ReadSingle();
            position1.X = packet.ReadSingle();
            packet.ReadSingle("Pitch");
            position2.X = packet.ReadSingle();
            position1.Y = packet.ReadSingle();
            packet.ReadSingle("Speed");
            position2.Y = packet.ReadSingle();
            packet.ReadInt16("Opcode");
            packet.ReadInt32<SpellId>("Spell ID");
            position2.Z = packet.ReadSingle();

            packet.AddValue("Target position", position1);
            packet.AddValue("Source position", position2);

            var guid = packet.StartBitStream(5, 6, 0, 7, 1, 3, 2, 4);

            var hasFallData = false;
            var hasFallVelocity = false;
            var hasFacing = false;
            var hasTransportData = false;
            var hasTransportTime2 = false;
            var hasVehicleId = false;
            var hasSplineElevation = false;
            var hasPitch = false;
            var hasMoveTime = false;

            var transportGuid = new byte[8];
            var moverGuid = new byte[8];

            var hasMovementData = packet.ReadBit();
            if (hasMovementData)
            {
                moverGuid[2] = packet.ReadBit();
                packet.ReadBit("Height change failed");
                var hasMoveFlags = !packet.ReadBit();
                packet.ReadBit("Has spline");
                moverGuid[4] = packet.ReadBit();

                if (hasMoveFlags)
                    packet.ReadBitsE<MovementFlag>("Movement Flags", 30, "Movement");

                moverGuid[3] = packet.ReadBit();
                hasPitch = !packet.ReadBit();
                var hasMovementFlagsExtra = !packet.ReadBit();
                hasTransportData = packet.ReadBit();
                moverGuid[7] = packet.ReadBit();

                hasVehicleId = false;
                if (hasTransportData)
                {
                    transportGuid[0] = packet.ReadBit();
                    transportGuid[7] = packet.ReadBit();
                    transportGuid[2] = packet.ReadBit();
                    transportGuid[4] = packet.ReadBit();
                    hasVehicleId = packet.ReadBit();
                    transportGuid[5] = packet.ReadBit();
                    transportGuid[3] = packet.ReadBit();
                    transportGuid[6] = packet.ReadBit();
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[1] = packet.ReadBit();
                }

                moverGuid[6] = packet.ReadBit();
                hasFacing = !packet.ReadBit();
                hasMoveTime = !packet.ReadBit();
                moverGuid[5] = packet.ReadBit();
                hasSplineElevation = !packet.ReadBit(); // StepUpStartElevation
                moverGuid[1] = packet.ReadBit();
                hasFallData = packet.ReadBit();
                moverGuid[0] = packet.ReadBit();

                if (hasFallData)
                    hasFallVelocity = packet.ReadBit();

                if (hasMovementFlagsExtra)
                    packet.ReadBitsE<MovementFlag2>("Movement Flags Extra", 12, "Movement");
            }

            packet.ResetBitReader();
            packet.ParseBitStream(guid, 2, 1, 3, 7, 6, 4, 0, 5);
            packet.WriteGuid("Caster GUID", guid);

            if (hasMovementData)
            {
                if (hasFallData)
                {
                    if (hasFallVelocity)
                    {
                        packet.ReadVector2("Fall Direction", "Movement", "Fall", "Velocity");
                        packet.ReadSingle("Fall Speed", "Movement", "Fall", "Velocity");
                    }

                    packet.ReadSingle("Jump speed", "Movement", "Jump");
                    packet.ReadUInt32("Fall time", "Movement", "Jump");
                }

                if (hasFacing)
                    packet.ReadSingle("Facing", "Movement");

                if (hasTransportData)
                {
                    var transportPos = new Vector3();
                    transportPos.Z = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 3);
                    packet.ReadXORByte(transportGuid, 7);
                    transportPos.X = packet.ReadSingle();
                    packet.ReadSingle("Orientation", "Movement", "Transport");
                    if (hasTransportTime2)
                        packet.ReadInt32("Previous move time", "Movement", "Transport");
                    packet.ReadXORByte(transportGuid, 2);
                    packet.ReadUInt32("Move time", "Movement", "Transport");
                    packet.ReadXORByte(transportGuid, 0);
                    transportPos.Y = packet.ReadSingle();
                    if (hasVehicleId)
                        packet.ReadInt32("Vehicle ID", "Movement", "Transport");
                    packet.ReadXORByte(transportGuid, 4);
                    packet.ReadXORByte(transportGuid, 6);
                    packet.ReadXORByte(transportGuid, 1);
                    packet.ReadInt32("Vehicle Seat Index", "Movement", "Transport");
                    packet.ReadXORByte(transportGuid, 5);

                    packet.AddValue("Transport position", transportPos, "Movement", "Transport");
                }

                if (hasSplineElevation)
                    packet.ReadSingle("Spline elevation", "Movement");

                packet.ReadXORByte(moverGuid, 4);
                if (hasPitch)
                    packet.ReadSingle("Pitch", "Movement");
                packet.ReadXORByte(moverGuid, 7);
                packet.ReadXORByte(moverGuid, 2);
                if (hasMoveTime)
                    packet.ReadInt32("Move time", "Transport");

                var position = new Vector3();
                position.X = packet.ReadSingle();

                packet.ReadXORByte(moverGuid, 5);
                packet.ReadXORByte(moverGuid, 3);
                packet.ReadXORByte(moverGuid, 1);
                packet.ReadXORByte(moverGuid, 0);
                position.Y = packet.ReadSingle();
                position.Z = packet.ReadSingle();
                packet.ReadXORByte(moverGuid, 6);

                packet.AddValue("Position", position, "Movement");
                packet.WriteGuid("Mover GUID", moverGuid, "Movement");

                if (hasTransportData)
                    packet.WriteGuid("Transport GUID", transportGuid, "Movement");
            }
        }
    }
}
