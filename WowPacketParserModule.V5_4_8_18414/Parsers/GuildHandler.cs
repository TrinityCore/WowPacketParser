using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_GUILD_ASSIGN_MEMBER_RANK)]
        public static void HandleGuildAssignMemberRank(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GUILD_DISBAND)]
        [Parser(Opcode.CMSG_GUILD_INFO_TEXT)]
        [Parser(Opcode.CMSG_GUILD_NEWS_UPDATE_STICKY)]
        [Parser(Opcode.CMSG_GUILD_PROMOTE)]
        [Parser(Opcode.CMSG_GUILD_QUERY)]
        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS)]
        [Parser(Opcode.CMSG_GUILD_QUERY_RANKS)]
        [Parser(Opcode.CMSG_GUILD_REMOVE)]
        [Parser(Opcode.CMSG_GUILD_REQUEST_CHALLENGE_UPDATE)]
        [Parser(Opcode.CMSG_GUILD_ROSTER)]
        [Parser(Opcode.CMSG_GUILD_SET_GUILD_MASTER)]
        [Parser(Opcode.SMSG_GUILD_COMMAND_RESULT)]
        [Parser(Opcode.SMSG_GUILD_QUERY_RESPONSE)]
        public static void HandleGuildQuery(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GUILD_DEMOTE)]
        public static void HandleGuildDemote(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadToEnd();
            }
            else
            {
                packet.WriteLine("              : SMSG_???");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_GUILD_MOTD)]
        public static void HandleGuildMotd(Packet packet)
        {
            var len = packet.ReadBits("Len", 10);
            packet.ReadWoWString("Motd", len);
        }

        [Parser(Opcode.CMSG_OFFER_PETITION)]
        public static void HandlePetitionOffer(Packet packet)
        {
            packet.ReadUInt32("Unk UInt32");
            var guid = new byte[8];
            var targetGuid = new byte[8];
            targetGuid[4] = packet.ReadBit();
            targetGuid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            targetGuid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            targetGuid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            targetGuid[3] = packet.ReadBit();
            targetGuid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            targetGuid[5] = packet.ReadBit();
            targetGuid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            packet.ReadXORByte(targetGuid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(targetGuid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(targetGuid, 0);
            packet.ReadXORByte(targetGuid, 2);
            packet.ReadXORByte(targetGuid, 5);
            packet.ReadXORByte(targetGuid, 3);
            packet.ReadXORByte(targetGuid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(targetGuid, 1);
            packet.ReadXORByte(guid, 6);
            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("targetGuid", targetGuid);
        }

        [Parser(Opcode.CMSG_PETITION_BUY)]
        public static void HandlePetitionBuy(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_PETITION_DECLINE)]
        public static void HandlePetitionDecline(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_PETITION_QUERY)]
        public static void HandlePetitionQuery(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_PETITION_RENAME)]
        public static void HandlePetitionRename(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_PETITION_SHOW_SIGNATURES)]
        public static void HandlePetitionShowSignatures(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_PETITION_SHOWLIST)]
        public static void HandlePetitionShowlist(Packet packet)
        {
            var guid = packet.StartBitStream(1, 7, 2, 5, 4, 0, 3, 6);
            packet.ParseBitStream(guid, 6, 3, 2, 4, 1, 7, 5, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PETITION_SIGN)]
        public static void HandlePetitionSign(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_QUERY_GUILD_REWARDS)]
        public static void HandleQueryGuildRewards(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_QUERY_GUILD_XP)]
        public static void HandleQueryGuildXp(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_TURN_IN_PETITION)]
        public static void HandlePetitionTurnIn(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
