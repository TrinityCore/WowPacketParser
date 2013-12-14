using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class MovementHandler
    {

        [Parser(Opcode.SMSG_PLAYER_MOVE)]
        public static void HandlePlayerMove(Packet packet)
        {
            var pos = new Vector4();
            var guid = new byte[8];
            var transportGUID = new byte[8];

            guid[7] = packet.ReadBit();

            var hasExtraMovementFlags = !packet.ReadBit();
            var hasPitch = !packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var bit95 = packet.ReadBit();
            var isAlive = !packet.ReadBit();

            guid[6] = packet.ReadBit();

            var bit94 = packet.ReadBit();

            guid[0] = packet.ReadBit();

            var hasTransportData = packet.ReadBit();

            bool hasTransportTime2 = false;
            bool hasTransportTime3 = false;

            if (hasTransportData)
            {
                transportGUID[4] = packet.ReadBit();
                transportGUID[1] = packet.ReadBit();
                transportGUID[6] = packet.ReadBit();
                transportGUID[0] = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                hasTransportTime3 = packet.ReadBit();
                transportGUID[2] = packet.ReadBit();
                transportGUID[7] = packet.ReadBit();
                transportGUID[3] = packet.ReadBit();
                transportGUID[5] = packet.ReadBit();
            }

            guid[4] = packet.ReadBit();
            var hasMovementFlags = !packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasOrientation = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasTimeStamp = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bits98 = (int)packet.ReadBits(22);

            if (hasExtraMovementFlags)
                packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13);

            var bitAC = packet.ReadBit();

            if (hasMovementFlags)
                packet.ReadEnum<MovementFlag>("Movement Flags", 30);

            var hasFallData = packet.ReadBit();
            
            var hasFallDirection = false;
            if (hasFallData)
                hasFallDirection = packet.ReadBit();

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.ReadSingle("Horizontal Speed");
                    packet.ReadSingle("Fall Sin");
                    packet.ReadSingle("Fall Cos");
                }

                packet.ReadInt32("Fall Time");
                packet.ReadSingle("Velocity Speed");
            }

            if (hasTransportData)
            {
                var transPos = new Vector4();

                packet.ReadXORByte(transportGUID, 1);

                transPos.Y = packet.ReadSingle();

                if (hasTransportTime2)
                    packet.ReadInt32("Transport Time 2");

                packet.ReadXORByte(transportGUID, 5);

                transPos.X = packet.ReadSingle();
                packet.ReadByte("Seat");
                packet.ReadInt32("Transport Time");

                packet.ReadXORByte(transportGUID, 3);
                packet.ReadXORByte(transportGUID, 6);

                transPos.O = packet.ReadSingle();
                transPos.Z = packet.ReadSingle();

                if (hasTransportTime3)
                    packet.ReadInt32("Transport Time 3");

                packet.ReadXORByte(transportGUID, 7);
                packet.ReadXORByte(transportGUID, 4);
                packet.ReadXORByte(transportGUID, 2);
                packet.ReadXORByte(transportGUID, 0);
                
                packet.WriteGuid("Transport Guid", transportGUID);
                packet.WriteLine("Transport Position {0}", transPos);
            }

            packet.ReadXORByte(guid, 3);

            pos.Y = packet.ReadSingle();

            if (hasPitch)
                packet.ReadSingle("Pitch");

            for (var i = 0; i < bits98; ++i)
                packet.ReadInt32("IntEA", i);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);

            if (hasTimeStamp)
                packet.ReadInt32("Timestamp");

            pos.Z = packet.ReadSingle();

            if (hasSplineElevation)
                packet.ReadSingle("Spline Elevation");

            packet.ReadXORByte(guid, 1);

            if (isAlive)
                packet.ReadInt32("time(isAlive)");

            pos.X = packet.ReadSingle();

            if (hasOrientation)
                pos.O = packet.ReadSingle();

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Position: {0}", pos);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            var pos = new Vector4();

            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            pos.X = packet.ReadSingle();
            pos.O = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();

            packet.WriteLine("Position: {0}", pos);
            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }
    }
}
