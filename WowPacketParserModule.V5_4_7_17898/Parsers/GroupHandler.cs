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
            var guid24 = new byte[8];

            var bits10 = 0;
            var bits12C = 0;

            packet.ReadInt32("Int114");
            packet.ReadInt32("Int128");
            packet.ReadByte("Byte118");
            guid24[5] = packet.ReadBit();
            bits12C = (int)packet.ReadBits(8);
            guid24[2] = packet.ReadBit();
            guid24[1] = packet.ReadBit();
            guid24[7] = packet.ReadBit();
            guid24[4] = packet.ReadBit();
            guid24[3] = packet.ReadBit();
            bits10 = (int)packet.ReadBits(8);
            guid24[0] = packet.ReadBit();
            guid24[6] = packet.ReadBit();
            packet.ReadXORByte(guid24, 0);
            packet.ReadXORByte(guid24, 4);
            packet.ReadXORByte(guid24, 5);
            packet.ReadXORByte(guid24, 6);
            packet.ReadXORByte(guid24, 1);
            packet.ReadXORByte(guid24, 7);
            packet.ReadXORByte(guid24, 3);
            packet.ReadXORByte(guid24, 2);

            packet.WriteGuid("Guid24", guid24);
        }
        [Parser(Opcode.SMSG_GROUP_INVITE)]
        public static void HandleSmsgGroupInvite(Packet packet)
        {
            var guid2F = new byte[8];

            var bits0 = 0;
            var bits41 = 0;
            var bits164 = 0;

            var bit160 = packet.ReadBit();
            var bit150 = packet.ReadBit();
            guid2F[2] = packet.ReadBit();
            var bit148 = packet.ReadBit();
            guid2F[4] = packet.ReadBit();
            guid2F[0] = packet.ReadBit();
            guid2F[7] = packet.ReadBit();
            guid2F[5] = packet.ReadBit();
            bits41 = (int)packet.ReadBits(9);
            guid2F[3] = packet.ReadBit();
            guid2F[1] = packet.ReadBit();
            var bit149 = packet.ReadBit();
            bits164 = (int)packet.ReadBits(22);
            bits0 = (int)packet.ReadBits(6);
            guid2F[6] = packet.ReadBit();
            packet.ReadInt32("Int144");
            packet.ReadXORByte(guid2F, 5);
            packet.ReadXORByte(guid2F, 3);
            packet.ReadXORByte(guid2F, 1);
            packet.ReadWoWString("String41", bits41);
            packet.ReadXORByte(guid2F, 2);
            packet.ReadInt64("Int158");
            packet.ReadInt32("Int14C");
            packet.ReadXORByte(guid2F, 7);
            packet.ReadInt32("Int180");
            packet.ReadXORByte(guid2F, 0);
            packet.ReadXORByte(guid2F, 4);
            packet.ReadWoWString("String10", bits0);
            packet.ReadXORByte(guid2F, 6);
            for (var i = 0; i < bits164; ++i)
            {
                packet.ReadInt32("IntEA");
            }

            packet.WriteGuid("Guid2F", guid2F);
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
