using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_RATED_SOLO_SHUFFLE)]
        public static void HandleBattlemasterJoinRatedSoloShuffle(Packet packet)
        {
            packet.ReadByteE<LfgRoleFlag>("RolesMask");
        }

        public static void ReadRatedMatchDeserterPenalty(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PersonalRatingChange");
            packet.ReadInt32<SpellId>("QueuePenaltySpellID");
            packet.ReadInt32("QueuePenaltyDuration");
        }

        [Parser(Opcode.SMSG_PVP_MATCH_INITIALIZE)]
        public static void HandlePvpMatchInitialize(Packet packet)
        {
            packet.ReadUInt32<MapId>("MapID");
            packet.ReadByteE<MatchState>("State");
            packet.ReadInt64("StartTime");
            packet.ReadInt64("Duration");
            packet.ReadByte("ArenaFaction");
            packet.ReadUInt32("BattlemasterListID");

            packet.ResetBitReader();
            packet.ReadBit("Registered");
            packet.ReadBit("AffectsRating");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_5_47777))
            {
                var hasDeserterPenalty = packet.ReadBit("HasRatedMatchDeserterPenalty");
                if (hasDeserterPenalty)
                    ReadRatedMatchDeserterPenalty(packet, "RatedMatchDeserterPenalty");
            }
        }
        
        public static void ReadBrawlInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PvpBrawlId");
            packet.ReadInt32("Time");
            packet.ResetBitReader();
            packet.ReadBit("Started");
        }
        
        public static void ReadSpecialEventInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PvpBrawlID");
            packet.ReadInt32<AchievementId>("AchievementId");
            packet.ResetBitReader();
            packet.ReadBit("CanQueue");
        }

        [Parser(Opcode.SMSG_REQUEST_SCHEDULED_PVP_INFO_RESPONSE)]
        public static void HandleRequestScheduledPvpInfoResponse(Packet packet)
        {
            var hasBrawlInfo = packet.ReadBit("HasBrawlInfo");
            var hasSpecialEventInfo = packet.ReadBit("HasSpecialEventInfo");

            if (hasBrawlInfo)
                ReadBrawlInfo(packet, "BrawlInfo");

            if (hasSpecialEventInfo)
                ReadSpecialEventInfo(packet, "SpecialEventInfo");
        }
    }
}
