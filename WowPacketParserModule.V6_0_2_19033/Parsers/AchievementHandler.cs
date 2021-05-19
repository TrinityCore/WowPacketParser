using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.DBC;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            int criteriaId = packet.ReadInt32<CriteriaId>("CriteriaID");
            ulong quantity = (ulong)packet.ReadInt64("Quantity");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadInt32("Flags");
            packet.ReadPackedTime("CurrentTime");
            packet.ReadTime("ElapsedTime");
            packet.ReadTime("CreationTime");

            if (Settings.UseDBC)
                if (DBC.Criteria.ContainsKey(criteriaId))
                    if (DBC.Criteria[criteriaId].Type == 46)
                        CoreParsers.AchievementHandler.FactionReputationStore[DBC.Criteria[criteriaId].Asset] = quantity;
        }

        [Parser(Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE)]
        public static void HandleCriteriaUpdateAccount(Packet packet)
        {
            ReadCriteriaProgress(packet, "Progress");
        }

        public static void ReadCriteriaProgress(Packet packet, params object[] idx)
        {
            int criteriaId = packet.ReadInt32<CriteriaId>("Id", idx);
            ulong quantity = packet.ReadUInt64("Quantity", idx);
            packet.ReadPackedGuid128("Player", idx);
            packet.ReadPackedTime("Date", idx);
            packet.ReadTime("TimeFromStart", idx);
            packet.ReadTime("TimeFromCreate", idx);

            if (Settings.UseDBC)
                if (DBC.Criteria.ContainsKey(criteriaId))
                    if (DBC.Criteria[criteriaId].Type == 46)
                        CoreParsers.AchievementHandler.FactionReputationStore[DBC.Criteria[criteriaId].Asset] = quantity;

            packet.ResetBitReader();
            packet.ReadBits("Flags", 4, idx); // some flag... & 1 -> delete
        }

        public static void ReadEarnedAchievement(Packet packet, params object[] idx)
        {
            packet.ReadInt32<AchievementId>("Id", idx);
            packet.ReadPackedTime("Date", idx);
            packet.ReadPackedGuid128("Owner", idx);
            packet.ReadInt32("VirtualRealmAddress", idx);
            packet.ReadInt32("NativeRealmAddress", idx);
        }

        [Parser(Opcode.SMSG_ALL_ACCOUNT_CRITERIA)]
        public static void HandleAllAchievementCriteriaDataAccount(Packet packet)
        {
            var count = packet.ReadUInt32("ProgressCount");

            for (var i = 0; i < count; ++i)
                ReadCriteriaProgress(packet, "Progress", i);
        }

        public static void ReadAllAchievements(Packet packet, params object[] idx)
        {
            var earnedCount = packet.ReadUInt32("EarnedCount", idx);
            var progressCount = packet.ReadUInt32("ProgressCount", idx);

            for (var i = 0; i < earnedCount; ++i)
                ReadEarnedAchievement(packet, "Earned", i);

            for (var i = 0; i < progressCount; ++i)
                ReadCriteriaProgress(packet, "Progress", i);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementDataPlayer(Packet packet)
        {
            ReadAllAchievements(packet, "Data");
        }

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED)]
        public static void HandleAchievementEarned(Packet packet)
        {
            packet.ReadPackedGuid128("Sender");
            packet.ReadPackedGuid128("Earner");
            packet.ReadUInt32<AchievementId>("AchievementID");
            packet.ReadPackedTime("Time");
            packet.ReadUInt32("EarnerNativeRealm");
            packet.ReadUInt32("EarnerVirtualRealm");
            packet.ReadBit("Initial");
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS)]
        public static void HandleRespondInspectAchievements(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            ReadAllAchievements(packet, "Data");
        }

        [Parser(Opcode.SMSG_GUILD_CRITERIA_DELETED)]
        public static void HandleGuildCriteriaDeleted(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadInt32("CriteriaID");
        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_DELETED)]
        public static void HandleGuildAchievementDeleted(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadUInt32("AchievementID");
            packet.ReadPackedTime("TimeDeleted");
        }

        [Parser(Opcode.CMSG_GUILD_GET_ACHIEVEMENT_MEMBERS)]
        public static void HandleGuildGetAchievementMembers(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadInt32("AchievementID");
        }

        public static void ReadGuildAchievementMember(Packet packet, params object[] index)
        {
            packet.ReadPackedGuid128("MemberGUID", index);
        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_MEMBERS)]
        public static void HandleGuildAchievementMembers(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadInt32("AchievementID");
            var memberCount = packet.ReadUInt32("MemberCount");

            for (int i = 0; i < memberCount; i++)
                ReadGuildAchievementMember(packet, i);
        }

        [Parser(Opcode.CMSG_SET_ACHIEVEMENTS_HIDDEN)]
        public static void HandleSetAchievementsHidden(Packet packet)
        {
            packet.ReadBit("Hidden");
        }
    }
}
