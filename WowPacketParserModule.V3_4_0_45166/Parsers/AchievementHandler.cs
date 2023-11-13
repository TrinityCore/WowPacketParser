using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class AchievementHandler
    {
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
                V6_0_2_19033.Parsers.AchievementHandler.ReadEarnedAchievement(packet, idx, "Earned", i);

            for (var i = 0; i < progressCount; ++i)
                ReadCriteriaProgress(packet, idx, "Progress", i);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V3_4_3_51505)]
        public static void HandleAllAchievementDataPlayer(Packet packet)
        {
            ReadAllAchievements(packet, "Data");
        }
    }
}
