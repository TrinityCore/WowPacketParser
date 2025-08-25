using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class GuildHandler
    {
        public static void ReadPetitionSignature(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("Signer", indexes);
            packet.ReadInt32("Choice", indexes);
        }

        [Parser(Opcode.SMSG_PETITION_ALREADY_SIGNED)]
        public static void HandlePetitionAlreadySigned(Packet packet)
        {
            packet.ReadPackedGuid128("SignerGUID");
        }

        [Parser(Opcode.SMSG_OFFER_PETITION_ERROR)]
        public static void HandlePetitionError(Packet packet)
        {
            packet.ReadPackedGuid128("PetitionGUID");
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_LIST)]
        public static void HandlePetitionShowList(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            var counter = packet.ReadUInt32("Counter");
            for (var i = 0; i < counter; i++)
            {
                packet.ReadUInt32("Index");
                packet.ReadUInt32("CharterCost");
                packet.ReadUInt32("CharterEntry");
                packet.ReadUInt32("Unk440");
                packet.ReadUInt32("RequiredSigns");
            }
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_SIGNATURES)]
        public static void HandlePetitionShowSignatures(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
            packet.ReadPackedGuid128("Owner");
            packet.ReadPackedGuid128("OwnerWoWAccount");
            packet.ReadInt32("PetitionID");

            var signaturesCount = packet.ReadInt32("SignaturesCount");
            for (int i = 0; i < signaturesCount; i++)
                ReadPetitionSignature(packet, i, "PetitionSignature");
        }

        [Parser(Opcode.SMSG_PETITION_SIGN_RESULTS)]
        public static void HandlePetitionSignResults(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
            packet.ReadPackedGuid128("Player");
            packet.ReadBits("Error", 4);
        }

        [Parser(Opcode.SMSG_TURN_IN_PETITION_RESULT)]
        public static void HandlePetitionTurnInResults(Packet packet)
        {
            packet.ReadBitsE<PetitionResultType>("Result", 4);
        }

        [Parser(Opcode.CMSG_GUILD_PROMOTE_MEMBER)]
        public static void HandleGuildPromoteMember(Packet packet)
        {
            packet.ReadPackedGuid128("Promotee");
        }

        [Parser(Opcode.CMSG_GUILD_DEMOTE_MEMBER)]
        public static void HandleGuildDemoteMember(Packet packet)
        {
            packet.ReadPackedGuid128("Demotee");
        }

        [Parser(Opcode.CMSG_GUILD_ASSIGN_MEMBER_RANK)]
        public static void HandleGuildAssignMemberRank(Packet packet)
        {
            packet.ReadPackedGuid128("Member");
            packet.ReadInt32("RankOrder");
        }

        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER)]
        public static void HandleGuildOfficerRemoveMember(Packet packet)
        {
            packet.ReadPackedGuid128("Removee");
        }

        [Parser(Opcode.CMSG_GUILD_ADD_RANK)]
        public static void HandleGuildAddRank(Packet packet)
        {
            var nameLength = packet.ReadBits(7);
            packet.ReadInt32("RankOrder");
            packet.ReadWoWString("Name", nameLength);
        }

        [Parser(Opcode.CMSG_GUILD_DELETE_RANK)]
        public static void HandleGuildDeleteRank(Packet packet)
        {
            packet.ReadInt32("RankOrder");
        }

        [Parser(Opcode.CMSG_GUILD_SHIFT_RANK)]
        public static void HandleGuildShiftRank(Packet packet)
        {
            packet.ReadInt32("RankOrder");
            packet.ReadBit("ShiftUp");
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS)]
        public static void HandlelGuildSetRankPermissions(Packet packet)
        {
            packet.ReadByte("RankID");
            packet.ReadInt32("RankOrder");
            packet.ReadUInt32E<GuildRankRightsFlag>("Flags");
            packet.ReadUInt32("WithdrawGoldLimit");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadUInt32E<GuildBankRightsFlag>("TabFlags", i);
                packet.ReadUInt32("TabWithdrawItemLimit", i);
            }

            packet.ResetBitReader();
            var rankNameLen = packet.ReadBits(7);

            packet.ReadWoWString("RankName", rankNameLen);

            packet.ReadUInt32E<GuildRankRightsFlag>("OldFlags");
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBER_RECIPES)]
        public static void HandleGuildQueryMemberRecipes(Packet packet)
        {
            packet.ReadPackedGuid128("GuildMember");
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadUInt32("SkillLineID");
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_RECIPES)]
        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS)]
        [Parser(Opcode.CMSG_GUILD_GET_RANKS)]
        public static void HandleGuildGuildGUid(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
        }

        [Parser(Opcode.CMSG_GUILD_NEWS_UPDATE_STICKY)]
        public static void HandleGuildNewsUpdateSticky(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadInt32("NewsID");
            packet.ReadBit("Sticky");
        }

        [Parser(Opcode.CMSG_GUILD_SET_ACHIEVEMENT_TRACKING)]
        public static void HandleGuildSetAchievementTracking(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadInt32<AchievementId>("AchievementIDs", i);
        }

        [Parser(Opcode.CMSG_GUILD_SET_FOCUSED_ACHIEVEMENT)]
        public static void HandleGuildSetFocusedAchievement(Packet packet)
        {
            packet.ReadInt32("AchievementID");
        }

        [Parser(Opcode.CMSG_GUILD_CHALLENGE_UPDATE_REQUEST)]
        [Parser(Opcode.CMSG_GUILD_DECLINE_INVITATION)]
        [Parser(Opcode.CMSG_GUILD_DELETE)]
        [Parser(Opcode.CMSG_GUILD_EVENT_LOG_QUERY)]
        [Parser(Opcode.CMSG_GUILD_GET_ROSTER)]
        [Parser(Opcode.CMSG_GUILD_LEAVE)]
        [Parser(Opcode.CMSG_GUILD_PERMISSIONS_QUERY)]
        [Parser(Opcode.CMSG_GUILD_REPLACE_GUILD_MASTER)]
        public static void HandleGuildZero(Packet packet)
        {
        }
    }
}
