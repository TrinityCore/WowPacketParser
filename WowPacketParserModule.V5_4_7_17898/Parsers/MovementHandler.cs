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
    }
}
