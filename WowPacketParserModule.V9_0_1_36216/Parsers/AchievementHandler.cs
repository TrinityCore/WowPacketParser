using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.DBC;
using System.Collections.Generic;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class AchievementHandler
    {
        public static Dictionary<int, ulong> FactionReputationStore { get; } = new Dictionary<int, ulong>();

        public static void ReadCriteriaProgress(Packet packet, params object[] indexes)
        {
            var criteriaId = packet.ReadInt32<CriteriaId>("CriteriaID", indexes);
            var quantity = packet.ReadUInt64("Quantity", indexes);
            packet.ReadPackedGuid128("PlayerGUID", indexes);
            packet.ReadPackedTime("CurrentTime", indexes);
            packet.ReadTime("ElapsedTime", indexes);
            packet.ReadTime("CreationTime", indexes);

            var hasRafAcceptanceID = packet.ReadBit("HasRafAcceptanceID", indexes);
            if (hasRafAcceptanceID)
                packet.ReadUInt64("RafAcceptanceID", indexes);

            if (Settings.UseDBC)
                if (DBC.Criteria.ContainsKey(criteriaId))
                    if (DBC.Criteria[criteriaId].Type == 46)
                        FactionReputationStore[DBC.Criteria[criteriaId].Asset] = quantity;
        }
    }
}
