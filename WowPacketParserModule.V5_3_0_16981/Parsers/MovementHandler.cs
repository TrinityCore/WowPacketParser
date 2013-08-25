using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_BINDPOINTUPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            packet.ReadSingle("Position Z");
            packet.ReadSingle("Position Y");
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id");
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            packet.ReadSingle("Position X");
        }

        [Parser(Opcode.SMSG_LOGIN_SETTIMESPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.ReadPackedTime("Game Time");
            packet.ReadUInt32("bit5");
            packet.ReadUInt32("bit7");
            packet.ReadUInt32("bit6");
            packet.ReadSingle("Game Speed");
        }

        public static void ReadClientMovementBlock(ref Packet packet)
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
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasExtraMovementFlags)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13);

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
                packet.WriteLine("Transport Position {0}", transPos);
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
            packet.WriteLine("Position: {0}", pos);
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
    }
}
