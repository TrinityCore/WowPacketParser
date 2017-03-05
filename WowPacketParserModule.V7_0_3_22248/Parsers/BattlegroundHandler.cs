using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA)]
        public static void HandleBattlemasterJoinArena(Packet packet)
        {
            packet.Translator.ReadByte("TeamSizeIndex");
            packet.Translator.ReadByteE<LfgRoleFlag>("Roles");
        }

        public static void ReadPlayerData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("PlayerGUID", idx);
            packet.Translator.ReadUInt32("Kills", idx);
            packet.Translator.ReadUInt32("DamageDone", idx);
            packet.Translator.ReadUInt32("HealingDone", idx);
            var statsCount = packet.Translator.ReadUInt32("StatsCount", idx);
            packet.Translator.ReadInt32("PrimaryTalentTree", idx);
            packet.Translator.ReadInt32("PrimaryTalentTreeNameIndex", idx);
            packet.Translator.ReadInt32E<Race>("Race", idx);
            packet.Translator.ReadUInt32("Prestige", idx);

            for (int i = 0; i < statsCount; i++)
                packet.Translator.ReadUInt32("Stats", i, idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Faction", idx);
            packet.Translator.ReadBit("IsInWorld", idx);

            var hasHonor = packet.Translator.ReadBit("HasHonor", idx);
            var hasPreMatchRating = packet.Translator.ReadBit("HasPreMatchRating", idx);
            var hasRatingChange = packet.Translator.ReadBit("HasRatingChange", idx);
            var hasPreMatchMMR = packet.Translator.ReadBit("HasPreMatchMMR", idx);
            var hasMmrChange = packet.Translator.ReadBit("HasMmrChange", idx);

            packet.Translator.ResetBitReader();

            if (hasHonor)
                V6_0_2_19033.Parsers.BattlegroundHandler.ReadHonorData(packet, "Honor");

            if (hasPreMatchRating)
                packet.Translator.ReadUInt32("PreMatchRating", idx);

            if (hasRatingChange)
                packet.Translator.ReadUInt32("RatingChange", idx);

            if (hasPreMatchMMR)
                packet.Translator.ReadUInt32("PreMatchMMR", idx);

            if (hasMmrChange)
                packet.Translator.ReadUInt32("MmrChange", idx);
        }

        [Parser(Opcode.SMSG_PVP_LOG_DATA)]
        public static void HandlePvPLogData(Packet packet)
        {
            var hasRatings = packet.Translator.ReadBit("HasRatings");
            var hasWinner = packet.Translator.ReadBit("HasWinner");

            var playersCount = packet.Translator.ReadUInt32("PlayersCount");

            for (int i = 0; i < 2; i++)
                packet.Translator.ReadByte("PlayerCount", i);

            if (hasRatings)
                V6_0_2_19033.Parsers.BattlegroundHandler.ReadRatingData(packet, "Ratings");

            if (hasWinner)
                packet.Translator.ReadByte("Winner");

            for (int i = 0; i < playersCount; i++)
                ReadPlayerData(packet, "Players", i);
        }
    }
}
