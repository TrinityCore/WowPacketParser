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

            var bit60 = packet.Translator.ReadBit();

            leaderGUID[4] = packet.Translator.ReadBit();
            leaderGUID[7] = packet.Translator.ReadBit();
            leaderGUID[3] = packet.Translator.ReadBit();
            leaderGUID[2] = packet.Translator.ReadBit();
            groupGUID[6] = packet.Translator.ReadBit();
            groupGUID[4] = packet.Translator.ReadBit();

            var bit48 = packet.Translator.ReadBit();
            if (bit48)
            {
                packet.Translator.ReadBit("bit3C");
                packet.Translator.ReadBit("bit47");
            }

            groupGUID[1] = packet.Translator.ReadBit();
            groupGUID[2] = packet.Translator.ReadBit();
            leaderGUID[5] = packet.Translator.ReadBit();

            var memberCount = packet.Translator.ReadBits("Member Count", 21);

            groupGUID[0] = packet.Translator.ReadBit();

            var bitsED = new uint[memberCount];
            var memberGUID = new byte[memberCount][];

            for (var i = 0; i < memberCount; i++)
            {
                memberGUID[i] = new byte[8];
                memberGUID[i][0] = packet.Translator.ReadBit();
                memberGUID[i][3] = packet.Translator.ReadBit();
                memberGUID[i][7] = packet.Translator.ReadBit();
                bitsED[i] = packet.Translator.ReadBits(6);
                memberGUID[i][2] = packet.Translator.ReadBit();
                memberGUID[i][4] = packet.Translator.ReadBit();
                memberGUID[i][5] = packet.Translator.ReadBit();
                memberGUID[i][6] = packet.Translator.ReadBit();
                memberGUID[i][1] = packet.Translator.ReadBit();
            }

            groupGUID[7] = packet.Translator.ReadBit();
            groupGUID[3] = packet.Translator.ReadBit();
            leaderGUID[0] = packet.Translator.ReadBit();

            var bit78 = packet.Translator.ReadBit();
            if (bit78)
                packet.Translator.StartBitStream(looterGUID, 6, 4, 3, 1, 7, 2, 0, 5);

            groupGUID[5] = packet.Translator.ReadBit();
            leaderGUID[6] = packet.Translator.ReadBit();
            leaderGUID[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(groupGUID, 2);

            if (bit48)
            {
                packet.Translator.ReadByte("Byte3D");
                packet.Translator.ReadByte("Byte30");
                packet.Translator.ReadInt32("Int34");
                packet.Translator.ReadByte("Byte44");
                packet.Translator.ReadByte("Byte46");
                packet.Translator.ReadByte("Byte45");
                packet.Translator.ReadInt32("Int38");
                packet.Translator.ReadSingle("Float40");
            }

            packet.Translator.ReadXORByte(groupGUID, 7);

            if (bit78)
            {
                packet.Translator.ReadXORByte(looterGUID, 3);
                packet.Translator.ReadXORByte(looterGUID, 0);
                packet.Translator.ReadByte("Byte71");
                packet.Translator.ReadXORByte(looterGUID, 6);
                packet.Translator.ReadXORByte(looterGUID, 1);
                packet.Translator.ReadXORByte(looterGUID, 5);
                packet.Translator.ReadXORByte(looterGUID, 4);
                packet.Translator.ReadByte("Byte70");
                packet.Translator.ReadXORByte(looterGUID, 7);
                packet.Translator.ReadXORByte(looterGUID, 2);

                packet.Translator.WriteGuid("Looter GUID", looterGUID);
            }

            for (var i = 0; i < memberCount; i++)
            {
                packet.Translator.ReadXORByte(memberGUID[i], 4);
                packet.Translator.ReadByte("ByteED", i);
                packet.Translator.ReadXORByte(memberGUID[i], 1);
                packet.Translator.ReadXORByte(memberGUID[i], 3);
                packet.Translator.ReadByte("ByteED", i);
                packet.Translator.ReadXORByte(memberGUID[i], 6);
                packet.Translator.ReadByte("Sub Group", i);
                packet.Translator.ReadXORByte(memberGUID[i], 7);
                packet.Translator.ReadWoWString("Name", bitsED[i], i);
                packet.Translator.ReadByteE<LfgRoleFlag>("Role", i);
                packet.Translator.ReadXORByte(memberGUID[i], 2);
                packet.Translator.ReadXORByte(memberGUID[i], 5);
                packet.Translator.ReadXORByte(memberGUID[i], 0);

                packet.Translator.WriteGuid("Member GUID", memberGUID[i], i);
            }

            packet.Translator.ReadInt32("Int4C");
            packet.Translator.ReadXORByte(groupGUID, 3);
            packet.Translator.ReadByte("Byte51");
            packet.Translator.ReadXORByte(groupGUID, 5);
            packet.Translator.ReadByte("Byte50");
            packet.Translator.ReadXORByte(leaderGUID, 2);
            packet.Translator.ReadXORByte(leaderGUID, 0);
            packet.Translator.ReadXORByte(leaderGUID, 4);
            packet.Translator.ReadXORByte(leaderGUID, 6);
            packet.Translator.ReadXORByte(leaderGUID, 1);
            if (bit60)
            {
                packet.Translator.ReadInt32("Int58");
                packet.Translator.ReadInt32("Int5C");
            }

            packet.Translator.ReadXORByte(groupGUID, 0);
            packet.Translator.ReadXORByte(groupGUID, 6);
            packet.Translator.ReadXORByte(leaderGUID, 5);
            packet.Translator.ReadInt32("Int54");
            packet.Translator.ReadXORByte(groupGUID, 4);
            packet.Translator.ReadXORByte(leaderGUID, 7);
            packet.Translator.ReadXORByte(leaderGUID, 3);
            packet.Translator.ReadXORByte(groupGUID, 1);
            packet.Translator.ReadByteE<MapDifficulty>("Dungeon Difficulty");

            packet.Translator.WriteGuid("Leader GUID", leaderGUID);
            packet.Translator.WriteGuid("Group GUID", groupGUID);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Online?");
            packet.Translator.StartBitStream(guid, 7, 1, 4, 3, 6, 5, 0, 2);
            packet.Translator.ParseBitStream(guid, 3, 4, 7, 6, 0, 1, 5, 2);

            packet.Translator.WriteGuid("GUID", guid);
        }
    }
}
