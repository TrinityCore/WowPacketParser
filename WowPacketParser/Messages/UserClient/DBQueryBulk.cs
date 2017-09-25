using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct DBQueryBulk
    {
        public uint TableHash;
        public List<Submessages.DBQuery> Queries;

        [Parser(Opcode.CMSG_DB_QUERY_BULK, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");
            var count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 5, 7, 6, 1, 4, 3, 2, 1);
            }

            packet.ResetBitReader();
            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORBytes(guids[i], 3, 7, 1, 6, 4, 0, 5);
                packet.ReadInt32("Unk int", i);
                packet.ReadXORByte(guids[i], 2);
                packet.WriteGuid("Guid", guids[i], i);
            }
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_1_17538)]
        public static void HandleDBQueryBulk540(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");
            var count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 3, 4, 7, 2, 5, 1, 6, 0);
            }

            packet.ResetBitReader();
            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORBytes(guids[i], 6, 1, 2);
                packet.ReadInt32("Entry", i);
                packet.ReadXORBytes(guids[i], 4, 5, 7, 0, 3);
                packet.WriteGuid("Guid", guids[i], i);
            }
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK, ClientVersionBuild.V5_4_1_17538, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleDBQueryBulk541(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");
            var count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 1, 7, 2, 5, 0, 6, 3, 4);
            }

            packet.ResetBitReader();
            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORBytes(guids[i], 4, 7, 6, 0, 2, 3);
                packet.ReadInt32("Entry", i);
                packet.ReadXORBytes(guids[i], 5, 1);
                packet.WriteGuid("Guid", guids[i], i);
            }
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleDBQueryBulk542(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");
            uint count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 3, 7, 5, 6, 2, 0, 4, 1);
            }

            packet.ResetBitReader();
            for (int i = 0; i < count; ++i)
            {
                packet.ReadXORBytes(guids[i], 5, 1, 4, 6, 7, 2, 0, 3);
                packet.ReadInt32("Entry", i);
                packet.WriteGuid("Guid", guids[i], i);
            }
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleDBQueryBulk547(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");
            var count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 2, 4, 3, 6, 7, 1, 5, 0);
            }

            packet.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 5);
                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 3);

                packet.ReadInt32("Entry", i);

                packet.ReadXORByte(guids[i], 7);
                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 2);
                packet.ReadXORByte(guids[i], 1);
                packet.ReadXORByte(guids[i], 6);

                packet.WriteGuid("Guid", guids[i], i);
            }
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleDBQueryBulk548(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");
            var count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 6, 3, 0, 1, 4, 5, 7, 2);
            }

            packet.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 1);

                packet.ReadInt32("Entry", i);

                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 5);
                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 7);
                packet.ReadXORByte(guids[i], 2);
                packet.ReadXORByte(guids[i], 3);

                packet.WriteGuid("Guid", guids[i], i);
            }
        }


        [Parser(Opcode.CMSG_DB_QUERY_BULK, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleDbQueryBulk602(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");

            var count = ClientVersion.AddedInVersion(ClientVersionBuild.V6_0_3_19103) ? packet.ReadBits("Count", 13) : packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadInt32("Entry", i);
            }
        }

    }
}
