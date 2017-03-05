using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_GROUP_ASSIGNMENT)]
        public static void HandleGroupAssignment(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Byte12");
            packet.Translator.ReadByte("Byte10");
            guid[0] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var bit11 = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_ASSISTANT_LEADER)]
        public static void HandleGroupAssistantLeader(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadBool("Promote");
            guid[1] = packet.Translator.ReadBit();
            var bit11 = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GROUP_RAID_CONVERT)]
        public static void HandleGroupRaidConvert(Packet packet)
        {
            packet.Translator.ReadBit("Convert");
        }

        [Parser(Opcode.CMSG_GROUP_REQUEST_JOIN_UPDATES)]
        public static void HandleGroupRequestJoinUpdates(Packet packet)
        {
            packet.Translator.ReadByte("Byte10");
        }

        [Parser(Opcode.CMSG_GROUP_SET_LEADER)]
        public static void HandleGroupSetLeader(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Byte18");

            guid[1] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GROUP_SWAP_SUB_GROUP)]
        public static void HandleGroupSwapSubGroup(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadByte("Byte20");

            guid1[4] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 4);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_GROUP_UNINVITE_GUID)]
        public static void HandleGroupUninviteGUID(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Byte10");
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var reasonLen = packet.Translator.ReadBits(8);
            guid[3] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadWoWString("Reason", reasonLen);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GROUP_SET_ROLES)]
        public static void HandleGroupSetRoles(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Byte1C");
            packet.Translator.ReadInt32("Int18");

            packet.Translator.StartBitStream(guid, 5, 3, 1, 0, 2, 6, 7, 4);
            packet.Translator.ParseBitStream(guid, 4, 6, 1, 3, 0, 7, 5, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GROUP_DISBAND)]
        public static void HandleGroupDisband(Packet packet)
        {
            packet.Translator.ReadByte("Byte10");
        }

        [Parser(Opcode.CMSG_GROUP_INVITE_RESPONSE)]
        public static void HandleGroupInviteResponse(Packet packet)
        {
            packet.Translator.ReadByte("Byte11");
            var bit18 = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit10");
            if (bit18)
                packet.Translator.ReadInt32("Int14");
        }

        [Parser(Opcode.CMSG_GROUP_INVITE)]
        public static void HandleGroupInvite(Packet packet)
        {
            var crossRealmGuid = new byte[8];

            packet.Translator.ReadInt32("Int114");
            packet.Translator.ReadInt32("Int128");
            packet.Translator.ReadByte("Byte118");
            crossRealmGuid[5] = packet.Translator.ReadBit();
            var bits12C = packet.Translator.ReadBits(9);
            crossRealmGuid[2] = packet.Translator.ReadBit();
            crossRealmGuid[1] = packet.Translator.ReadBit();
            crossRealmGuid[7] = packet.Translator.ReadBit();
            crossRealmGuid[4] = packet.Translator.ReadBit();
            crossRealmGuid[3] = packet.Translator.ReadBit();
            var bits10 = packet.Translator.ReadBits(9);
            crossRealmGuid[0] = packet.Translator.ReadBit();
            crossRealmGuid[6] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(crossRealmGuid, 0);
            packet.Translator.ReadXORByte(crossRealmGuid, 4);
            packet.Translator.ReadWoWString("string12C", bits10);
            packet.Translator.ReadXORByte(crossRealmGuid, 5);
            packet.Translator.ReadXORByte(crossRealmGuid, 6);
            packet.Translator.ReadWoWString("string10", bits12C);
            packet.Translator.ReadXORByte(crossRealmGuid, 1);
            packet.Translator.ReadXORByte(crossRealmGuid, 7);
            packet.Translator.ReadXORByte(crossRealmGuid, 3);
            packet.Translator.ReadXORByte(crossRealmGuid, 2);

            packet.Translator.WriteGuid("crossRealmGuid", crossRealmGuid);
        }

        [Parser(Opcode.SMSG_GROUP_DECLINE)]
        public static void HandleGroupDecline(Packet packet)
        {
            packet.Translator.ReadCString();
        }

        [Parser(Opcode.SMSG_GROUP_INVITE)]
        public static void HandleSmsgGroupInvite(Packet packet)
        {
            var invitedGuid = new byte[8];

            var bit160 = packet.Translator.ReadBit();
            var bit150 = packet.Translator.ReadBit();
            invitedGuid[2] = packet.Translator.ReadBit();
            var bit148 = packet.Translator.ReadBit();
            invitedGuid[4] = packet.Translator.ReadBit();
            invitedGuid[0] = packet.Translator.ReadBit();
            invitedGuid[7] = packet.Translator.ReadBit();
            invitedGuid[5] = packet.Translator.ReadBit();
            var realmLen = (int)packet.Translator.ReadBits(9);
            invitedGuid[3] = packet.Translator.ReadBit();
            invitedGuid[1] = packet.Translator.ReadBit();
            var bit149 = packet.Translator.ReadBit();
            var bits164 = (int)packet.Translator.ReadBits(22);
            var inviterName = (int)packet.Translator.ReadBits(6);
            invitedGuid[6] = packet.Translator.ReadBit();

            packet.Translator.ResetBitReader();

            packet.Translator.ReadInt32("Int144");
            packet.Translator.ReadXORByte(invitedGuid, 5);
            packet.Translator.ReadXORByte(invitedGuid, 3);
            packet.Translator.ReadXORByte(invitedGuid, 1);
            packet.Translator.ReadWoWString("realmName", realmLen);
            packet.Translator.ReadXORByte(invitedGuid, 2);
            packet.Translator.ReadInt64("Int158");
            packet.Translator.ReadInt32("Int14C");
            packet.Translator.ReadXORByte(invitedGuid, 7);
            packet.Translator.ReadInt32("Int180");
            packet.Translator.ReadXORByte(invitedGuid, 0);
            packet.Translator.ReadXORByte(invitedGuid, 4);
            packet.Translator.ReadWoWString("inviterName", inviterName);
            packet.Translator.ReadXORByte(invitedGuid, 6);

            for (var i = 0; i < bits164; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            packet.Translator.WriteGuid("invitedGuid", invitedGuid);
        }

        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            var groupGUID = new byte[8];
            var looterGUID = new byte[8];
            var leaderGUID = new byte[8];

            var bit7C = false;
            var bit87 = false;

            packet.Translator.ReadInt32("groupPosition");
            packet.Translator.ReadByte("groupType");
            packet.Translator.ReadByte("groupSlot");
            packet.Translator.ReadByte("groupRole");
            packet.Translator.ReadInt32("groupCounter");

            var hasInstanceDifficulty = packet.Translator.ReadBit();
            groupGUID[5] = packet.Translator.ReadBit(); //40-47
            var memberCounter = (int)packet.Translator.ReadBits(21);

            groupGUID[7] = packet.Translator.ReadBit();
            leaderGUID[1] = packet.Translator.ReadBit();

            var memberName = new uint[memberCounter];
            var memberGUID = new byte[memberCounter][];
            for (var i = 0; i < memberCounter; ++i)
            {
                memberGUID[i] = new byte[8];

                memberGUID[i][1] = packet.Translator.ReadBit();
                memberGUID[i][2] = packet.Translator.ReadBit();
                memberGUID[i][4] = packet.Translator.ReadBit();
                memberGUID[i][6] = packet.Translator.ReadBit();
                memberGUID[i][0] = packet.Translator.ReadBit();
                memberGUID[i][3] = packet.Translator.ReadBit();
                memberName[i] = packet.Translator.ReadBits(6);
                memberGUID[i][5] = packet.Translator.ReadBit();
                memberGUID[i][7] = packet.Translator.ReadBit();
            }

            var bit88 = packet.Translator.ReadBit();
            leaderGUID[5] = packet.Translator.ReadBit();
            groupGUID[0] = packet.Translator.ReadBit();
            groupGUID[4] = packet.Translator.ReadBit();
            leaderGUID[2] = packet.Translator.ReadBit();
            groupGUID[3] = packet.Translator.ReadBit();
            leaderGUID[0] = packet.Translator.ReadBit();
            leaderGUID[4] = packet.Translator.ReadBit();
            groupGUID[1] = packet.Translator.ReadBit();
            groupGUID[2] = packet.Translator.ReadBit();
            groupGUID[6] = packet.Translator.ReadBit();
            leaderGUID[3] = packet.Translator.ReadBit();
            leaderGUID[6] = packet.Translator.ReadBit();
            leaderGUID[7] = packet.Translator.ReadBit();

            if (bit88)
            {
                bit87 = packet.Translator.ReadBit();
                bit7C = packet.Translator.ReadBit();
            }

            var hasLootMode = packet.Translator.ReadBit();

            if (hasLootMode)
            {
                looterGUID[3] = packet.Translator.ReadBit();
                looterGUID[7] = packet.Translator.ReadBit();
                looterGUID[4] = packet.Translator.ReadBit();
                looterGUID[5] = packet.Translator.ReadBit();
                looterGUID[6] = packet.Translator.ReadBit();
                looterGUID[0] = packet.Translator.ReadBit();
                looterGUID[1] = packet.Translator.ReadBit();
                looterGUID[2] = packet.Translator.ReadBit();
            }

            packet.Translator.ResetBitReader();

            if (hasLootMode)
            {
                packet.Translator.ReadXORByte(looterGUID, 3);
                packet.Translator.ReadXORByte(looterGUID, 5);
                packet.Translator.ReadByte("lootMethod");
                packet.Translator.ReadXORByte(looterGUID, 0);
                packet.Translator.ReadXORByte(looterGUID, 1);
                packet.Translator.ReadXORByte(looterGUID, 2);
                packet.Translator.ReadXORByte(looterGUID, 4);
                packet.Translator.ReadXORByte(looterGUID, 6);
                packet.Translator.ReadByte("lootThreshold");
                packet.Translator.ReadXORByte(looterGUID, 7);
                packet.Translator.WriteGuid("looterGUID", looterGUID);
            }

            packet.Translator.ReadXORByte(leaderGUID, 1);

            if (bit88)
            {
                packet.Translator.ReadByte("Byte85");
                packet.Translator.ReadByte("Byte7D");
                packet.Translator.ReadByte("Byte70");
                packet.Translator.ReadInt32("Int74");
                packet.Translator.ReadByte("Byte86");
                packet.Translator.ReadInt32("Int78");
                packet.Translator.ReadSingle("Float80");
                packet.Translator.ReadByte("Byte84");
            }

            packet.Translator.ReadXORByte(groupGUID, 3);
            for (var i = 0; i < memberCounter; ++i)
            {
                packet.Translator.ReadXORByte(memberGUID[i], 6);
                packet.Translator.ReadXORByte(memberGUID[i], 3);
                packet.Translator.ReadByte("Flags", i);
                packet.Translator.ReadXORByte(memberGUID[i], 2);
                packet.Translator.ReadXORByte(memberGUID[i], 1);
                packet.Translator.ReadByte("onlineState", i);
                packet.Translator.ReadXORByte(memberGUID[i], 4);
                packet.Translator.ReadXORByte(memberGUID[i], 5);
                packet.Translator.ReadByte("LFGrole", i);
                packet.Translator.ReadWoWString("MemberName", memberName[i], i);
                packet.Translator.ReadByte("groupID", i);
                packet.Translator.ReadXORByte(memberGUID[i], 7);
                packet.Translator.ReadXORByte(memberGUID[i], 0);
                packet.Translator.WriteGuid("memberGUID", memberGUID[i], i);
            }

            if (hasInstanceDifficulty)
            {
                packet.Translator.ReadInt32("raidDifficulty");
                packet.Translator.ReadInt32("dungeonDifficulty");
            }

            packet.Translator.ReadXORByte(leaderGUID, 5);
            packet.Translator.ReadXORByte(groupGUID, 2);
            packet.Translator.ReadXORByte(leaderGUID, 0);
            packet.Translator.ReadXORByte(groupGUID, 6);
            packet.Translator.ReadXORByte(groupGUID, 5);
            packet.Translator.ReadXORByte(groupGUID, 4);
            packet.Translator.ReadXORByte(leaderGUID, 2);
            packet.Translator.ReadXORByte(leaderGUID, 3);
            packet.Translator.ReadXORByte(leaderGUID, 6);
            packet.Translator.ReadXORByte(groupGUID, 0);
            packet.Translator.ReadXORByte(leaderGUID, 7);
            packet.Translator.ReadXORByte(groupGUID, 7);
            packet.Translator.ReadXORByte(leaderGUID, 4);
            packet.Translator.ReadXORByte(groupGUID, 1);

            packet.Translator.WriteGuid("groupGUID", groupGUID);
            packet.Translator.WriteGuid("leaderGUID", leaderGUID);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Online?");

            packet.Translator.StartBitStream(guid, 5, 3, 4, 1, 6, 0, 2, 7);
            packet.Translator.ParseBitStream(guid, 0, 3, 1, 2, 7, 5, 4, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
