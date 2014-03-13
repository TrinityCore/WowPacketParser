using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_GROUP_ASSIGNMENT)]
        public static void HandleGroupAssignment(Packet packet)
        {
            var guid3 = new byte[8];

            packet.ReadByte("Byte12");
            packet.ReadByte("Byte10");
            guid3[0] = packet.ReadBit();
            guid3[5] = packet.ReadBit();
            guid3[6] = packet.ReadBit();
            guid3[7] = packet.ReadBit();
            guid3[3] = packet.ReadBit();
            guid3[1] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            var bit11 = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            packet.ReadXORByte(guid3, 4);
            packet.ReadXORByte(guid3, 3);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(guid3, 2);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid3, 0);

            packet.WriteGuid("Guid3", guid3);

        }
        [Parser(Opcode.CMSG_GROUP_ASSISTANT_LEADER)]
        public static void HandleGroupAssistantLeader(Packet packet)
        {
            var guid3 = new byte[8];

            packet.ReadByte("Byte10");
            guid3[1] = packet.ReadBit();
            var bit11 = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            guid3[7] = packet.ReadBit();
            guid3[5] = packet.ReadBit();
            guid3[3] = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            guid3[6] = packet.ReadBit();

            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid3, 4);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid3, 0);
            packet.ReadXORByte(guid3, 2);
            packet.ReadXORByte(guid3, 3);
            packet.ReadXORByte(guid3, 5);

            packet.WriteGuid("Guid3", guid3);
        }

        [Parser(Opcode.CMSG_GROUP_RAID_CONVERT)]
        public static void HandleGroupRaidConvert(Packet packet)
        {
            packet.ReadBit("Convert");
        }

        [Parser(Opcode.CMSG_GROUP_REQUEST_JOIN_UPDATES)]
        public static void HandleGroupRequestJoinUpdates(Packet packet)
        {
            packet.ReadByte("Byte10");
        }

        [Parser(Opcode.CMSG_GROUP_SET_LEADER)]
        public static void HandleGroupSetLeader(Packet packet)
        {
            var guid2 = new byte[8];

            packet.ReadByte("Byte18");

            guid2[1] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[7] = packet.ReadBit();

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 3);

            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_GROUP_SWAP_SUB_GROUP)]
        public static void HandleGroupSwapSubGroup(Packet packet)
        {
            var guid2 = new byte[8];
            var guid3 = new byte[8];

            packet.ReadByte("Byte20");

            guid2[4] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid3[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid3[5] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            guid3[6] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid3[3] = packet.ReadBit();
            guid3[1] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            guid2[1] = packet.ReadBit();

            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid3, 4);
            packet.ReadXORByte(guid3, 0);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid3, 3);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid3, 2);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 4);

            packet.WriteGuid("Guid2", guid2);
            packet.WriteGuid("Guid3", guid3);

        }

        [Parser(Opcode.CMSG_GROUP_UNINVITE_GUID)]
        public static void HandleGroupUninviteGUID(Packet packet)
        {
            var guid3 = new byte[8];

            var bits1A = 0;

            packet.ReadByte("Byte10");
            guid3[0] = packet.ReadBit();
            guid3[7] = packet.ReadBit();
            guid3[5] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            bits1A = (int)packet.ReadBits(8);
            guid3[3] = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            guid3[6] = packet.ReadBit();
            guid3[1] = packet.ReadBit();

            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid3, 0);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid3, 2);
            packet.ReadXORByte(guid3, 4);
            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(guid3, 3);

            packet.WriteGuid("Guid3", guid3);
        }
        
        [Parser(Opcode.CMSG_GROUP_SET_ROLES)]
        public static void HandleGroupSetRoles(Packet packet)
        {
            var guid2 = new byte[8];

            packet.ReadByte("Byte1C");
            packet.ReadInt32("Int18");

            guid2[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[4] = packet.ReadBit();

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 2);

            packet.WriteGuid("Guid2", guid2);

        }
        
        [Parser(Opcode.CMSG_GROUP_DISBAND)]
        public static void HandleGroupDisband(Packet packet)
        {
            packet.ReadByte("Byte10");
        }
        
        [Parser(Opcode.CMSG_GROUP_INVITE_RESPONSE)]
        public static void HandleGroupInviteResponse(Packet packet)
        {
            packet.ReadByte("Byte11");
            var bit18 = packet.ReadBit();
            var bit10 = packet.ReadBit();
            if (bit18)
                packet.ReadInt32("Int14");
        }

        [Parser(Opcode.CMSG_GROUP_INVITE)]
        public static void HandleGroupInvite(Packet packet)
        {
            var crossRealmGuid = new byte[8];

            var bits10 = 0;
            var bits12C = 0;

            packet.ReadInt32("Int114");
            packet.ReadInt32("Int128");
            packet.ReadByte("Byte118");
            crossRealmGuid[5] = packet.ReadBit();
            bits12C = (int)packet.ReadBits(8);
            crossRealmGuid[2] = packet.ReadBit();
            crossRealmGuid[1] = packet.ReadBit();
            crossRealmGuid[7] = packet.ReadBit();
            crossRealmGuid[4] = packet.ReadBit();
            crossRealmGuid[3] = packet.ReadBit();
            bits10 = (int)packet.ReadBits(8);
            crossRealmGuid[0] = packet.ReadBit();
            crossRealmGuid[6] = packet.ReadBit();
            packet.ReadXORByte(crossRealmGuid, 0);
            packet.ReadXORByte(crossRealmGuid, 4);
            packet.ReadXORByte(crossRealmGuid, 5);
            packet.ReadXORByte(crossRealmGuid, 6);
            packet.ReadXORByte(crossRealmGuid, 1);
            packet.ReadXORByte(crossRealmGuid, 7);
            packet.ReadXORByte(crossRealmGuid, 3);
            packet.ReadXORByte(crossRealmGuid, 2);

            packet.WriteGuid("crossRealmGuid", crossRealmGuid);
        }

        [Parser(Opcode.SMSG_GROUP_DECLINE)]
        public static void HandleGroupDecline(Packet packet)
        {
            packet.ReadCString();
        }

        [Parser(Opcode.SMSG_GROUP_INVITE)]
        public static void HandleSmsgGroupInvite(Packet packet)
        {
            var invitedGuid = new byte[8];

            var inviterName = 0;
            var realmLen = 0;
            var bits164 = 0;

            var bit160 = packet.ReadBit();
            var bit150 = packet.ReadBit();
            invitedGuid[2] = packet.ReadBit();
            var bit148 = packet.ReadBit();
            invitedGuid[4] = packet.ReadBit();
            invitedGuid[0] = packet.ReadBit();
            invitedGuid[7] = packet.ReadBit();
            invitedGuid[5] = packet.ReadBit();
            realmLen = (int)packet.ReadBits(9);
            invitedGuid[3] = packet.ReadBit();
            invitedGuid[1] = packet.ReadBit();
            var bit149 = packet.ReadBit();
            bits164 = (int)packet.ReadBits(22);
            inviterName = (int)packet.ReadBits(6);
            invitedGuid[6] = packet.ReadBit();

            packet.ResetBitReader();

            packet.ReadInt32("Int144");
            packet.ReadXORByte(invitedGuid, 5);
            packet.ReadXORByte(invitedGuid, 3);
            packet.ReadXORByte(invitedGuid, 1);
            packet.ReadWoWString("realmName", realmLen);
            packet.ReadXORByte(invitedGuid, 2);
            packet.ReadInt64("Int158");
            packet.ReadInt32("Int14C");
            packet.ReadXORByte(invitedGuid, 7);
            packet.ReadInt32("Int180");
            packet.ReadXORByte(invitedGuid, 0);
            packet.ReadXORByte(invitedGuid, 4);
            packet.ReadWoWString("inviterName", inviterName);
            packet.ReadXORByte(invitedGuid, 6);

            for (var i = 0; i < bits164; ++i)
            {
                packet.ReadInt32("IntEA", i);
            }

            packet.WriteGuid("invitedGuid", invitedGuid);
        }

        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            var groupGUID = new byte[8];
            var looterGUID = new byte[8];
            var leaderGUID = new byte[8];

            var bit7C = false;
            var bit87 = false;

            var memberCounter = 0;
            
            packet.ReadInt32("groupPosition");
            packet.ReadByte("groupType");
            packet.ReadByte("groupSlot");
            packet.ReadByte("groupRole");
            packet.ReadInt32("groupCounter");

            var hasInstanceDifficulty = packet.ReadBit();
            groupGUID[5] = packet.ReadBit(); //40-47
            memberCounter = (int)packet.ReadBits(21);

            groupGUID[7] = packet.ReadBit();
            leaderGUID[1] = packet.ReadBit();

            var memberName = new uint[memberCounter];
            var memberGUID = new byte[memberCounter][];
            for (var i = 0; i < memberCounter; ++i)
            {
                memberGUID[i] = new byte[8];

                memberGUID[i][1] = packet.ReadBit(); 
                memberGUID[i][2] = packet.ReadBit();
                memberGUID[i][4] = packet.ReadBit();
                memberGUID[i][6] = packet.ReadBit();
                memberGUID[i][0] = packet.ReadBit();
                memberGUID[i][3] = packet.ReadBit();
                memberName[i] = packet.ReadBits(6);
                memberGUID[i][5] = packet.ReadBit();
                memberGUID[i][7] = packet.ReadBit();
            }

            var bit88 = packet.ReadBit();
            leaderGUID[5] = packet.ReadBit();
            groupGUID[0] = packet.ReadBit();
            groupGUID[4] = packet.ReadBit();
            leaderGUID[2] = packet.ReadBit();
            groupGUID[3] = packet.ReadBit();
            leaderGUID[0] = packet.ReadBit();
            leaderGUID[4] = packet.ReadBit();
            groupGUID[1] = packet.ReadBit();
            groupGUID[2] = packet.ReadBit();
            groupGUID[6] = packet.ReadBit();
            leaderGUID[3] = packet.ReadBit();
            leaderGUID[6] = packet.ReadBit();
            leaderGUID[7] = packet.ReadBit();

            if (bit88)
            {
                bit87 = packet.ReadBit();
                bit7C = packet.ReadBit();
            }

            var hasLootMode = packet.ReadBit();

            if (hasLootMode)
            {
                looterGUID[3] = packet.ReadBit();
                looterGUID[7] = packet.ReadBit();
                looterGUID[4] = packet.ReadBit();
                looterGUID[5] = packet.ReadBit();
                looterGUID[6] = packet.ReadBit();
                looterGUID[0] = packet.ReadBit();
                looterGUID[1] = packet.ReadBit();
                looterGUID[2] = packet.ReadBit();
            }

            packet.ResetBitReader();

            if (hasLootMode)
            {
                packet.ReadXORByte(looterGUID, 3);
                packet.ReadXORByte(looterGUID, 5);
                packet.ReadByte("lootMethod");
                packet.ReadXORByte(looterGUID, 0);
                packet.ReadXORByte(looterGUID, 1);
                packet.ReadXORByte(looterGUID, 2);
                packet.ReadXORByte(looterGUID, 4);
                packet.ReadXORByte(looterGUID, 6);
                packet.ReadByte("lootThreshold");
                packet.ReadXORByte(looterGUID, 7);
                packet.WriteGuid("looterGUID", looterGUID);
            }

            packet.ReadXORByte(leaderGUID, 1);

            if (bit88)
            {
                packet.ReadByte("Byte85");
                packet.ReadByte("Byte7D");
                packet.ReadByte("Byte70");
                packet.ReadInt32("Int74");
                packet.ReadByte("Byte86");
                packet.ReadInt32("Int78");
                packet.ReadSingle("Float80");
                packet.ReadByte("Byte84");
            }

            packet.ReadXORByte(groupGUID, 3);
            for (var i = 0; i < memberCounter; ++i)
            {
                packet.ReadXORByte(memberGUID[i], 6);
                packet.ReadXORByte(memberGUID[i], 3);
                packet.ReadByte("Flags");
                packet.ReadXORByte(memberGUID[i], 2);
                packet.ReadXORByte(memberGUID[i], 1);
                packet.ReadByte("onlineState");
                packet.ReadXORByte(memberGUID[i], 4);
                packet.ReadXORByte(memberGUID[i], 5);
                packet.ReadByte("LFGrole");
                packet.ReadWoWString("MemberName", memberName[i], i);
                packet.ReadByte("groupID");
                packet.ReadXORByte(memberGUID[i], 7);
                packet.ReadXORByte(memberGUID[i], 0);
                packet.WriteGuid("memberGUID", memberGUID[i], i);
            }

            if (hasInstanceDifficulty)
            {
                packet.ReadInt32("raidDifficulty");
                packet.ReadInt32("dungeonDifficulty");
            }

            packet.ReadXORByte(leaderGUID, 5);
            packet.ReadXORByte(groupGUID, 2);
            packet.ReadXORByte(leaderGUID, 0);
            packet.ReadXORByte(groupGUID, 6);
            packet.ReadXORByte(groupGUID, 5);
            packet.ReadXORByte(groupGUID, 4);
            packet.ReadXORByte(leaderGUID, 2);
            packet.ReadXORByte(leaderGUID, 3);
            packet.ReadXORByte(leaderGUID, 6);
            packet.ReadXORByte(groupGUID, 0);
            packet.ReadXORByte(leaderGUID, 7);
            packet.ReadXORByte(groupGUID, 7);
            packet.ReadXORByte(leaderGUID, 4);
            packet.ReadXORByte(groupGUID, 1);

            packet.WriteGuid("groupGUID", groupGUID);
            packet.WriteGuid("leaderGUID", leaderGUID);

        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Online?");

            packet.StartBitStream(guid, 5, 3, 4, 1, 6, 0, 2, 7);
            packet.ParseBitStream(guid, 0, 3, 1, 2, 7, 5, 4, 6);

            packet.WriteGuid("Guid", guid);
        }
    }
}
