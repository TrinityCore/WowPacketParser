using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_SET_PHASE_SHIFT)]
        public static void HandlePhaseShift(Packet packet)
        {
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
                packet.ReadUInt16("Phase id", i); // Phase.dbc

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
            var bit28 = !packet.ReadBit();      // +28

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
    }
}
