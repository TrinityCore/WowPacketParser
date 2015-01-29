using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            var leaderGUID = new byte[8];
            var groupGUID = new byte[8];
            var looterGUID = new byte[8];

            var bit60 = packet.ReadBit();

            leaderGUID[4] = packet.ReadBit();
            leaderGUID[7] = packet.ReadBit();
            leaderGUID[3] = packet.ReadBit();
            leaderGUID[2] = packet.ReadBit();
            groupGUID[6] = packet.ReadBit();
            groupGUID[4] = packet.ReadBit();

            var bit48 = packet.ReadBit();
            if (bit48)
            {
                packet.ReadBit("bit3C");
                packet.ReadBit("bit47");
            }

            groupGUID[1] = packet.ReadBit();
            groupGUID[2] = packet.ReadBit();
            leaderGUID[5] = packet.ReadBit();

            var memberCount = packet.ReadBits("Member Count", 21);

            groupGUID[0] = packet.ReadBit();

            var bitsED = new uint[memberCount];
            var memberGUID = new byte[memberCount][];

            for (var i = 0; i < memberCount; i++)
            {
                memberGUID[i] = new byte[8];
                memberGUID[i][0] = packet.ReadBit();
                memberGUID[i][3] = packet.ReadBit();
                memberGUID[i][7] = packet.ReadBit();
                bitsED[i] = packet.ReadBits(6);
                memberGUID[i][2] = packet.ReadBit();
                memberGUID[i][4] = packet.ReadBit();
                memberGUID[i][5] = packet.ReadBit();
                memberGUID[i][6] = packet.ReadBit();
                memberGUID[i][1] = packet.ReadBit();
            }

            groupGUID[7] = packet.ReadBit();
            groupGUID[3] = packet.ReadBit();
            leaderGUID[0] = packet.ReadBit();

            var bit78 = packet.ReadBit();
            if (bit78)
                packet.StartBitStream(looterGUID, 6, 4, 3, 1, 7, 2, 0, 5);

            groupGUID[5] = packet.ReadBit();
            leaderGUID[6] = packet.ReadBit();
            leaderGUID[1] = packet.ReadBit();

            packet.ReadXORByte(groupGUID, 2);

            if (bit48)
            {
                packet.ReadByte("Byte3D");
                packet.ReadByte("Byte30");
                packet.ReadInt32("Int34");
                packet.ReadByte("Byte44");
                packet.ReadByte("Byte46");
                packet.ReadByte("Byte45");
                packet.ReadInt32("Int38");
                packet.ReadSingle("Float40");
            }

            packet.ReadXORByte(groupGUID, 7);

            if (bit78)
            {
                packet.ReadXORByte(looterGUID, 3);
                packet.ReadXORByte(looterGUID, 0);
                packet.ReadByte("Byte71");
                packet.ReadXORByte(looterGUID, 6);
                packet.ReadXORByte(looterGUID, 1);
                packet.ReadXORByte(looterGUID, 5);
                packet.ReadXORByte(looterGUID, 4);
                packet.ReadByte("Byte70");
                packet.ReadXORByte(looterGUID, 7);
                packet.ReadXORByte(looterGUID, 2);

                packet.WriteGuid("Looter GUID", looterGUID);
            }

            for (var i = 0; i < memberCount; i++)
            {
                packet.ReadXORByte(memberGUID[i], 4);
                packet.ReadByte("ByteED", i);
                packet.ReadXORByte(memberGUID[i], 1);
                packet.ReadXORByte(memberGUID[i], 3);
                packet.ReadByte("ByteED", i);
                packet.ReadXORByte(memberGUID[i], 6);
                packet.ReadByte("Sub Group", i);
                packet.ReadXORByte(memberGUID[i], 7);
                packet.ReadWoWString("Name", bitsED[i], i);
                packet.ReadByteE<LfgRoleFlag>("Role", i);
                packet.ReadXORByte(memberGUID[i], 2);
                packet.ReadXORByte(memberGUID[i], 5);
                packet.ReadXORByte(memberGUID[i], 0);

                packet.WriteGuid("Member GUID", memberGUID[i], i);
            }

            packet.ReadInt32("Int4C");
            packet.ReadXORByte(groupGUID, 3);
            packet.ReadByte("Byte51");
            packet.ReadXORByte(groupGUID, 5);
            packet.ReadByte("Byte50");
            packet.ReadXORByte(leaderGUID, 2);
            packet.ReadXORByte(leaderGUID, 0);
            packet.ReadXORByte(leaderGUID, 4);
            packet.ReadXORByte(leaderGUID, 6);
            packet.ReadXORByte(leaderGUID, 1);
            if (bit60)
            {
                packet.ReadInt32("Int58");
                packet.ReadInt32("Int5C");
            }

            packet.ReadXORByte(groupGUID, 0);
            packet.ReadXORByte(groupGUID, 6);
            packet.ReadXORByte(leaderGUID, 5);
            packet.ReadInt32("Int54");
            packet.ReadXORByte(groupGUID, 4);
            packet.ReadXORByte(leaderGUID, 7);
            packet.ReadXORByte(leaderGUID, 3);
            packet.ReadXORByte(groupGUID, 1);
            packet.ReadByteE<MapDifficulty>("Dungeon Difficulty");

            packet.WriteGuid("Leader GUID", leaderGUID);
            packet.WriteGuid("Group GUID", groupGUID);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Online?");
            packet.StartBitStream(guid, 7, 1, 4, 3, 6, 5, 0, 2);
            packet.ParseBitStream(guid, 3, 4, 7, 6, 0, 1, 5, 2);

            packet.WriteGuid("GUID", guid);
        }
    }
}
