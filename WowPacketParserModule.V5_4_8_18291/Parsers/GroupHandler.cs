using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_GROUP_INVITE)]
        public static void HandleGroupInvite(Packet packet)
        {
            var crossRealmGuid = new byte[8];

            packet.ReadInt32("Int114");
            packet.ReadByte("Byte118");
            packet.ReadInt32("Int128");
            crossRealmGuid[7] = packet.ReadBit();
            var realmNameLen = packet.ReadBits(9);
            crossRealmGuid[3] = packet.ReadBit();
            var nameLen = packet.ReadBits(9);
            crossRealmGuid[2] = packet.ReadBit();
            crossRealmGuid[5] = packet.ReadBit();
            crossRealmGuid[4] = packet.ReadBit();
            crossRealmGuid[0] = packet.ReadBit();
            crossRealmGuid[1] = packet.ReadBit();
            crossRealmGuid[6] = packet.ReadBit();

            packet.ReadXORByte(crossRealmGuid, 7);
            packet.ReadXORByte(crossRealmGuid, 6);
            packet.ReadXORByte(crossRealmGuid, 0);
            packet.ReadXORByte(crossRealmGuid, 4);
            packet.ReadWoWString("Name", nameLen);
            packet.ReadXORByte(crossRealmGuid, 1);
            packet.ReadXORByte(crossRealmGuid, 2);
            packet.ReadXORByte(crossRealmGuid, 3);
            packet.ReadWoWString("Realm Name", realmNameLen);
            packet.ReadXORByte(crossRealmGuid, 5);

            packet.WriteGuid("crossRealmGuid", crossRealmGuid);
        }

        [Parser(Opcode.SMSG_GROUP_INVITE)]
        public static void HandleSmsgGroupInvite(Packet packet)
        {
            var inviterGuid = new byte[8];

            var realmName1 = (int)packet.ReadBits(8);
            var realmName2 = (int)packet.ReadBits(8);
            inviterGuid[2] = packet.ReadBit();
            var bit160 = packet.ReadBit("bit160");
            var inviterName = (int)packet.ReadBits(6);
            inviterGuid[7] = packet.ReadBit();
            inviterGuid[5] = packet.ReadBit();
            var bit150 = packet.ReadBit("bit150");
            var bit148 = packet.ReadBit("bit148");
            inviterGuid[1] = packet.ReadBit();
            var bit149 = packet.ReadBit("bit149");
            var bit74 = packet.ReadBit("bit74");
            var bits164 = (int)packet.ReadBits(22);
            inviterGuid[3] = packet.ReadBit();
            inviterGuid[0] = packet.ReadBit();
            inviterGuid[4] = packet.ReadBit();
            inviterGuid[6] = packet.ReadBit();
            packet.ResetBitReader();

            packet.ReadXORByte(inviterGuid, 6);
            packet.ReadWoWString("realmName1", realmName1);
            packet.ReadXORByte(inviterGuid, 7);
            packet.ReadXORByte(inviterGuid, 2);
            packet.ReadXORByte(inviterGuid, 0);
            packet.ReadInt64("LowGuid?");
            packet.ReadInt32("RealmId?");
            packet.ReadInt32("Int14C");
            packet.ReadXORByte(inviterGuid, 1);
            packet.ReadXORByte(inviterGuid, 5);
            packet.ReadWoWString("realmName2", realmName2);
            packet.ReadXORByte(inviterGuid, 4);
            packet.ReadInt32("Int180");
            packet.ReadWoWString("inviterName", inviterName);
            packet.ReadXORByte(inviterGuid, 3);
            packet.ReadInt32("unk");
            for (var i = 0; i < bits164; ++i)
                packet.ReadInt32("IntEA", i);

            packet.WriteGuid("inviterGuid", inviterGuid);
        }

        [Parser(Opcode.CMSG_GROUP_INVITE_RESPONSE)]
        public static void HandleGroupInviteResponse(Packet packet)
        {
            packet.ReadByte("Byte11");
            var bit18 = packet.ReadBit();
            packet.ReadBit("Accept");
            if (bit18)
                packet.ReadInt32("Int14");
        }

        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {
            var leaderGUID = new byte[8];
            var looterGUID = new byte[8];
            var groupGUID = new byte[8];

            var bit7C = false;
            var bit87 = false;

            groupGUID[0] = packet.ReadBit();
            leaderGUID[7] = packet.ReadBit();
            leaderGUID[1] = packet.ReadBit();
            var hasInstanceDifficulty = packet.ReadBit();
            groupGUID[7] = packet.ReadBit();
            leaderGUID[6] = packet.ReadBit();
            leaderGUID[5] = packet.ReadBit();
            var memberCounter = (int)packet.ReadBits(21);

            var memberName = new uint[memberCounter];
            var memberGUID = new byte[memberCounter][];
            for (var i = 0; i < memberCounter; ++i)
            {
                memberGUID[i] = new byte[8];

                memberGUID[i][1] = packet.ReadBit();
                memberGUID[i][2] = packet.ReadBit();
                memberGUID[i][5] = packet.ReadBit();
                memberGUID[i][6] = packet.ReadBit();
                memberName[i] = packet.ReadBits(6);
                memberGUID[i][7] = packet.ReadBit();
                memberGUID[i][3] = packet.ReadBit();
                memberGUID[i][0] = packet.ReadBit();
                memberGUID[i][4] = packet.ReadBit();
            }

            leaderGUID[3] = packet.ReadBit();
            leaderGUID[0] = packet.ReadBit();
            var hasLootMode = packet.ReadBit();
            groupGUID[5] = packet.ReadBit();

            if (hasLootMode)
            {
                looterGUID[6] = packet.ReadBit();
                looterGUID[4] = packet.ReadBit();
                looterGUID[5] = packet.ReadBit();
                looterGUID[2] = packet.ReadBit();
                looterGUID[1] = packet.ReadBit();
                looterGUID[0] = packet.ReadBit();
                looterGUID[7] = packet.ReadBit();
                looterGUID[3] = packet.ReadBit();
            }
            groupGUID[2] = packet.ReadBit();
            groupGUID[4] = packet.ReadBit();
            groupGUID[1] = packet.ReadBit();

            var bit88 = packet.ReadBit();
            leaderGUID[2] = packet.ReadBit();
            groupGUID[6] = packet.ReadBit();
            if (bit88)
            {
                bit87 = packet.ReadBit();
                bit7C = packet.ReadBit();
            }

            leaderGUID[4] = packet.ReadBit();
            groupGUID[3] = packet.ReadBit();

            packet.ResetBitReader();

            packet.ReadXORByte(leaderGUID, 0);
            if (hasInstanceDifficulty)
            {
                packet.ReadInt32("raidDifficulty");
                packet.ReadInt32("dungeonDifficulty");
            }

            for (var i = 0; i < memberCounter; ++i)
            {
                packet.ReadXORByte(memberGUID[i], 6);
                packet.ReadXORByte(memberGUID[i], 3);
                packet.ReadByte("LFGrole", i);
                packet.ReadByte("onlineState", i);
                packet.ReadXORByte(memberGUID[i], 7);
                packet.ReadXORByte(memberGUID[i], 4);
                packet.ReadXORByte(memberGUID[i], 1);
                packet.ReadWoWString("MemberName", memberName[i], i);
                packet.ReadXORByte(memberGUID[i], 5);
                packet.ReadXORByte(memberGUID[i], 2);
                packet.ReadByte("groupID", i);
                packet.ReadXORByte(memberGUID[i], 0);
                packet.ReadByte("Flags", i);
                packet.WriteGuid("memberGUID", memberGUID[i], i);
            }
            packet.ReadXORByte(groupGUID, 1);

            if (bit88)
            {
                packet.ReadSingle("Float80");
                packet.ReadByte("Byte85");
                packet.ReadByte("Byte7D");
                packet.ReadInt32("Int74");
                packet.ReadByte("Byte84");
                packet.ReadByte("Byte70");
                packet.ReadByte("Byte86");
                packet.ReadInt32("Int78");
            }

            packet.ReadXORByte(leaderGUID, 4);
            packet.ReadXORByte(leaderGUID, 2);

            if (hasLootMode)
            {
                packet.ReadByte("lootMethod");
                packet.ReadXORByte(looterGUID, 0);
                packet.ReadXORByte(looterGUID, 5);
                packet.ReadXORByte(looterGUID, 4);
                packet.ReadXORByte(looterGUID, 3);
                packet.ReadXORByte(looterGUID, 2);
                packet.ReadByte("lootThreshold");
                packet.ReadXORByte(looterGUID, 7);
                packet.ReadXORByte(looterGUID, 1);
                packet.ReadXORByte(looterGUID, 6);
                packet.WriteGuid("looterGUID", looterGUID);
            }

            packet.ReadXORByte(groupGUID, 6);
            packet.ReadXORByte(groupGUID, 4);

            packet.ReadByte("groupType");
            packet.ReadByte("groupSlot"); // ??
            packet.ReadInt32("groupPosition");

            packet.ReadXORByte(groupGUID, 7);
            packet.ReadXORByte(leaderGUID, 3);
            packet.ReadXORByte(leaderGUID, 1);

            packet.ReadInt32("groupCounter");

            packet.ReadXORByte(groupGUID, 0);
            packet.ReadXORByte(groupGUID, 2);
            packet.ReadXORByte(groupGUID, 5);
            packet.ReadXORByte(groupGUID, 3);
            packet.ReadXORByte(leaderGUID, 7);

            packet.ReadByte("groupRole");

            packet.ReadXORByte(leaderGUID, 5);
            packet.ReadXORByte(leaderGUID, 6);

            packet.WriteGuid("leaderGUID", leaderGUID);
            packet.WriteGuid("groupGUID", groupGUID);
        }
    }
}
