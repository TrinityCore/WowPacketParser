using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];

            guid1[1] = packet.ReadBit();
            guid2[7] = packet.ReadBit();

            var memberCount = packet.ReadBits("Member Count", 21);

            var bitsED = new uint[memberCount];
            var guid4 = new byte[memberCount][];
            for (var i = 0; i < memberCount; i++)
            {
                guid4[i] = new byte[8];
                bitsED[i] = packet.ReadBits(6);
                packet.StartBitStream(guid4[i], 3, 0, 4, 7, 6, 1, 5, 2);
            }

            guid1[2] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            var bit2C = packet.ReadBit();
            var bit80 = packet.ReadBit();

            if (bit80)
                packet.StartBitStream(guid3, 2, 0, 3, 7, 4, 1, 6, 5);

            packet.StartBitStream(guid2, 2, 1, 6);
            guid1[7] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            packet.StartBitStream(guid1, 4, 3);
            guid2[3] = packet.ReadBit();

            if (bit2C)
            {
                packet.ReadBit("bit2B");
                packet.ReadBit("bit20");
            }

            guid1[6] = packet.ReadBit();
            var bit38 = packet.ReadBit();
            packet.StartBitStream(guid1, 5, 0);
            guid2[5] = packet.ReadBit();

            if (bit2C)
            {
                packet.ReadInt32("Int18");
                packet.ReadByte("Byte2A");
                packet.ReadSingle("Float24");
                packet.ReadByte("Byte28");
                packet.ReadByteE<InstanceStatus>("Group Type Status");
                packet.ReadByte("Byte21");
                packet.ReadByte("Byte29");
                packet.ReadInt32("Int1C");
            }

            for (var i = 0; i < memberCount; i++)
            {
                packet.ReadXORByte(guid4[i], 5);
                packet.ReadByteE<GroupUpdateFlag>("Update Flags", i);
                packet.ReadByteE<LfgRoleFlag>("Role", i);
                packet.ReadXORByte(guid4[i], 3);
                packet.ReadXORByte(guid4[i], 6);
                packet.ReadByte("Sub Group", i);
                packet.ReadWoWString("Name", bitsED[i], i);
                packet.ReadXORByte(guid4[i], 0);
                packet.ReadXORByte(guid4[i], 2);
                packet.ReadByteE<GroupMemberStatusFlag>("Status", i);
                packet.ReadXORByte(guid4[i], 7);
                packet.ReadXORByte(guid4[i], 1);
                packet.ReadXORByte(guid4[i], 4);

                packet.WriteGuid("Member GUID", guid4[i], i);
            }

            packet.ReadInt32("Counter");

            if (bit80)
            {
                packet.ReadXORByte(guid3, 0);
                packet.ReadXORByte(guid3, 1);
                packet.ReadXORByte(guid3, 3);
                packet.ReadXORByte(guid3, 7);
                packet.ReadXORByte(guid3, 6);
                packet.ReadXORByte(guid3, 2);
                packet.ReadByte("Byte78");
                packet.ReadXORByte(guid3, 5);
                packet.ReadByteE<ItemQuality>("Loot Threshold");
                packet.ReadXORByte(guid3, 4);
                packet.WriteGuid("Looter GUID", guid3);
            }

            packet.ReadXORByte(guid2, 3);
            packet.ReadByteE<MapDifficulty>("Dungeon Difficulty");
            packet.ReadXORByte(guid1, 7);

            if (bit38)
            {
                packet.ReadInt32("Int34");
                packet.ReadInt32("Int30");
            }

            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 3);

            packet.ReadByteE<LootMethod>("Loot Method");

            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 6);

            packet.ReadByteE<GroupTypeFlag>("Group Type");
            packet.ReadInt32("Int48");

            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 5);

            packet.WriteGuid("Leader GUID", guid1);
            packet.WriteGuid("Group GUID", guid2);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Online?");
            packet.StartBitStream(guid, 7, 1, 4, 3, 6, 2, 5, 0);
            packet.ParseBitStream(guid, 7, 0, 4, 2, 1, 6, 5, 3);

            packet.WriteGuid("GUID", guid);
        }
    }
}
