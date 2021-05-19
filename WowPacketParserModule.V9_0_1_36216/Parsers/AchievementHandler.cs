using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.DBC;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
            {
                packet.ReadTime64("ElapsedTime");
                packet.ReadTime64("CreationTime");
            }
            else
            {
                packet.ReadTime("ElapsedTime");
                packet.ReadTime("CreationTime");
            }

            var hasRafAcceptanceID = packet.ReadBit("HasRafAcceptanceID");

            if (hasRafAcceptanceID)
                packet.ReadUInt64("RafAcceptanceID");

            if (Settings.UseDBC)
                if (DBC.Criteria.ContainsKey(criteriaId))
                    if (DBC.Criteria[criteriaId].Type == 46)
                        CoreParsers.AchievementHandler.FactionReputationStore[DBC.Criteria[criteriaId].Asset] = quantity;
        }

        public static void ReadCriteriaProgress(Packet packet, params object[] indexes)
        {
            var criteriaId = packet.ReadInt32<CriteriaId>("CriteriaID", indexes);
            var quantity = packet.ReadUInt64("Quantity", indexes);
            packet.ReadPackedGuid128("PlayerGUID", indexes);
            packet.ReadPackedTime("CurrentTime", indexes);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
            {
                packet.ReadTime64("ElapsedTime", indexes);
                packet.ReadTime64("CreationTime", indexes);
            }
            else
            {
                packet.ReadTime("ElapsedTime", indexes);
                packet.ReadTime("CreationTime", indexes);
            }

            packet.ResetBitReader();
            var hasRafAcceptanceID = packet.ReadBit("HasRafAcceptanceID", indexes);
            if (hasRafAcceptanceID)
                packet.ReadUInt64("RafAcceptanceID", indexes);

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
                V6_0_2_19033.Parsers.AchievementHandler.ReadEarnedAchievement(packet, "Earned", i);

            for (var i = 0; i < progressCount; ++i)
                ReadCriteriaProgress(packet, "Progress", i);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementDataPlayer(Packet packet)
        {
            ReadAllAchievements(packet, "Data");
        }
    }
}
