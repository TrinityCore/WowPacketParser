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

            guid1[1] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();

            var memberCount = packet.Translator.ReadBits("Member Count", 21);

            var bitsED = new uint[memberCount];
            var guid4 = new byte[memberCount][];
            for (var i = 0; i < memberCount; i++)
            {
                guid4[i] = new byte[8];
                bitsED[i] = packet.Translator.ReadBits(6);
                packet.Translator.StartBitStream(guid4[i], 3, 0, 4, 7, 6, 1, 5, 2);
            }

            guid1[2] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();

            var bit2C = packet.Translator.ReadBit();
            var bit80 = packet.Translator.ReadBit();

            if (bit80)
                packet.Translator.StartBitStream(guid3, 2, 0, 3, 7, 4, 1, 6, 5);

            packet.Translator.StartBitStream(guid2, 2, 1, 6);
            guid1[7] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 4, 3);
            guid2[3] = packet.Translator.ReadBit();

            if (bit2C)
            {
                packet.Translator.ReadBit("bit2B");
                packet.Translator.ReadBit("bit20");
            }

            guid1[6] = packet.Translator.ReadBit();
            var bit38 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 5, 0);
            guid2[5] = packet.Translator.ReadBit();

            if (bit2C)
            {
                packet.Translator.ReadInt32("Int18");
                packet.Translator.ReadByte("Byte2A");
                packet.Translator.ReadSingle("Float24");
                packet.Translator.ReadByte("Byte28");
                packet.Translator.ReadByteE<InstanceStatus>("Group Type Status");
                packet.Translator.ReadByte("Byte21");
                packet.Translator.ReadByte("Byte29");
                packet.Translator.ReadInt32("Int1C");
            }

            for (var i = 0; i < memberCount; i++)
            {
                packet.Translator.ReadXORByte(guid4[i], 5);
                packet.Translator.ReadByteE<GroupUpdateFlag>("Update Flags", i);
                packet.Translator.ReadByteE<LfgRoleFlag>("Role", i);
                packet.Translator.ReadXORByte(guid4[i], 3);
                packet.Translator.ReadXORByte(guid4[i], 6);
                packet.Translator.ReadByte("Sub Group", i);
                packet.Translator.ReadWoWString("Name", bitsED[i], i);
                packet.Translator.ReadXORByte(guid4[i], 0);
                packet.Translator.ReadXORByte(guid4[i], 2);
                packet.Translator.ReadByteE<GroupMemberStatusFlag>("Status", i);
                packet.Translator.ReadXORByte(guid4[i], 7);
                packet.Translator.ReadXORByte(guid4[i], 1);
                packet.Translator.ReadXORByte(guid4[i], 4);

                packet.Translator.WriteGuid("Member GUID", guid4[i], i);
            }

            packet.Translator.ReadInt32("Counter");

            if (bit80)
            {
                packet.Translator.ReadXORByte(guid3, 0);
                packet.Translator.ReadXORByte(guid3, 1);
                packet.Translator.ReadXORByte(guid3, 3);
                packet.Translator.ReadXORByte(guid3, 7);
                packet.Translator.ReadXORByte(guid3, 6);
                packet.Translator.ReadXORByte(guid3, 2);
                packet.Translator.ReadByte("Byte78");
                packet.Translator.ReadXORByte(guid3, 5);
                packet.Translator.ReadByteE<ItemQuality>("Loot Threshold");
                packet.Translator.ReadXORByte(guid3, 4);
                packet.Translator.WriteGuid("Looter GUID", guid3);
            }

            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadByteE<MapDifficulty>("Dungeon Difficulty");
            packet.Translator.ReadXORByte(guid1, 7);

            if (bit38)
            {
                packet.Translator.ReadInt32("Int34");
                packet.Translator.ReadInt32("Int30");
            }

            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 3);

            packet.Translator.ReadByteE<LootMethod>("Loot Method");

            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 6);

            packet.Translator.ReadByteE<GroupTypeFlag>("Group Type");
            packet.Translator.ReadInt32("Int48");

            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 5);

            packet.Translator.WriteGuid("Leader GUID", guid1);
            packet.Translator.WriteGuid("Group GUID", guid2);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Online?");
            packet.Translator.StartBitStream(guid, 7, 1, 4, 3, 6, 2, 5, 0);
            packet.Translator.ParseBitStream(guid, 7, 0, 4, 2, 1, 6, 5, 3);

            packet.Translator.WriteGuid("GUID", guid);
        }
    }
}
