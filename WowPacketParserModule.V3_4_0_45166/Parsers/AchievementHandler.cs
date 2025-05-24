using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class AchievementHandler
    {
        public static void ReadEarnedAchievement(Packet packet, params object[] idx)
        {
            packet.ReadInt32<AchievementId>("Id", idx);
            packet.ReadPackedTime("Date", idx);
            packet.ReadPackedGuid128("Owner", idx);
            packet.ReadInt32("VirtualRealmAddress", idx);
            packet.ReadInt32("NativeRealmAddress", idx);
        }

        public static void ReadCriteriaProgress(Packet packet, params object[] indexes)
        {
            var criteriaId = packet.ReadInt32<CriteriaId>("CriteriaID", indexes);
            var quantity = packet.ReadUInt64("Quantity", indexes);
            packet.ReadPackedGuid128("PlayerGUID", indexes);
            packet.ReadUInt32("Unused343", indexes);
            packet.ReadUInt32("Flags", indexes);
            packet.ReadPackedTime("CurrentTime", indexes);
            packet.ReadTime64("ElapsedTime", indexes);
            packet.ReadTime64("CreationTime", indexes);

            packet.ResetBitReader();
            var hasDynamicID = packet.ReadBit("HasDynamicID", indexes);
            if (hasDynamicID)
                packet.ReadUInt64("DynamicID", indexes);

            if (Settings.UseDBC)
                if (DBC.Criteria.ContainsKey(criteriaId))
                    if (DBC.Criteria[criteriaId].Type == 46)
                        CoreParsers.AchievementHandler.FactionReputationStore[DBC.Criteria[criteriaId].Asset] = quantity;
        }

        public static void ReadAllAchievements(Packet packet, params object[] idx)
        {
            var earnedCount = packet.ReadUInt32("EarnedCount", idx);
            var progressCount = packet.ReadUInt32("ProgressCount", idx);

            for (var i = 0; i < earnedCount; ++i)
                ReadEarnedAchievement(packet, idx, "Earned", i);

            for (var i = 0; i < progressCount; ++i)
                ReadCriteriaProgress(packet, idx, "Progress", i);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V3_4_3_51505)]
        public static void HandleAllAchievementDataPlayer(Packet packet)
        {
            ReadAllAchievements(packet, "Data");
        }

        [Parser(Opcode.SMSG_CRITERIA_UPDATE, ClientVersionBuild.V3_4_3_51505)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            int criteriaId = packet.ReadInt32<CriteriaId>("CriteriaID");
            ulong quantity = (ulong)packet.ReadInt64("Quantity");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadInt32("Unused1015");
            packet.ReadInt32("Flags");
            packet.ReadPackedTime("CurrentTime");
            packet.ReadTime64("ElapsedTime");
            packet.ReadTime64("CreationTime");

            var hasDynamicID = packet.ReadBit("HasDynamicID");

            if (hasDynamicID)
                packet.ReadUInt64("DynamicID");

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

        [Parser(Opcode.SMSG_ACHIEVEMENT_DELETED)]
        public static void HandleAchievementDeleted(Packet packet)
        {
            packet.ReadUInt32("AchievementID");
            packet.ReadUInt32("Immunities"); // Garbage
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

        [Parser(Opcode.SMSG_ALL_ACCOUNT_CRITERIA, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAllAchievementCriteriaDataAccount(Packet packet)
        {
            var count = packet.ReadUInt32("ProgressCount");

            for (var i = 0; i < count; ++i)
                ReadCriteriaProgress(packet, "Progress", i);
        }

        [Parser(Opcode.SMSG_BROADCAST_ACHIEVEMENT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBroadcastAchievement(Packet packet)
        {
            var nameLength = packet.ReadBits(7);
            packet.ReadBit("GuildAchievement");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadUInt32("AchievementID");
            packet.ReadWoWString("Name", nameLength);
        }

        [Parser(Opcode.SMSG_CRITERIA_DELETED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleDeleted(Packet packet)
        {
            packet.ReadInt32("CriteriaID");
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRespondInspectAchievements(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            ReadAllAchievements(packet, "Data");
        }

        [Parser(Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleQueryInspectAchievements(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
        }
    }
}
