using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            var criteriaId = packet.ReadInt32<CriteriaId>("CriteriaID");
            var quantity = packet.ReadUInt64("Quantity");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadInt32("Flags");
            packet.ReadPackedTime("CurrentTime");
            packet.ReadTime("ElapsedTime");
            packet.ReadTime("CreationTime");

            var hasDynamicID = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_5_31921))
                hasDynamicID = packet.ReadBit();

            if (hasDynamicID)
                packet.ReadUInt64("DynamicID");

            CoreParsers.AchievementHandler.TryUpdateFactionStandingFromCriteria(criteriaId, quantity);
        }
    }
}
