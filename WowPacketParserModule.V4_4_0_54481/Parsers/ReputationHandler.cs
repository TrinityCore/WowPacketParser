using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class ReputationHandler
    {
        public const int FactionCount = 1000;

        [Parser(Opcode.SMSG_FACTION_BONUS_INFO)]
        public static void HandleFactionBonusInfo(Packet packet)
        {
            for (var i = 0; i < FactionCount; i++)
                packet.ReadBit("FactionHasBonus", i);
        }

        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            for (var i = 0; i < FactionCount; i++)
            {
                packet.ReadUInt16E<ReputationFlags>("ReputationFlags", i);
                packet.ReadUInt32E<ReputationRank>("FactionStandings", i);
            }

            for (var i = 0; i < FactionCount; i++)
                packet.ReadBit("FactionHasBonus", i);
        }

        [Parser(Opcode.SMSG_SET_FORCED_REACTIONS)]
        public static void HandleForcedReactions(Packet packet)
        {
            var counter = packet.ReadUInt32("ForcedReactionCount");

            for (var i = 0; i < counter; i++)
            {
                packet.ReadUInt32("Faction");
                packet.ReadUInt32("Reaction");
            }
        }

        [Parser(Opcode.SMSG_SET_FACTION_VISIBLE)]
        [Parser(Opcode.SMSG_SET_FACTION_NOT_VISIBLE)]
        public static void HandleSetFactionMisc(Packet packet)
        {
            packet.ReadUInt32("FactionIndex");
        }

        public static void ReadFactionStandingData(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Index", indexes);
            packet.ReadInt32("Standing", indexes);
            packet.ReadInt32("FactionID", indexes);
        }

        [Parser(Opcode.SMSG_SET_FACTION_STANDING)]
        public static void HandleSetFactionStanding(Packet packet)
        {
            packet.ReadSingle("BonusFromAchievementSystem");

            var count = packet.ReadInt32();
            for (int i = 0; i < count; i++)
                ReadFactionStandingData(packet, i);

            packet.ResetBitReader();
            packet.ReadBit("ShowVisual");
        }

        [Parser(Opcode.CMSG_SET_FACTION_AT_WAR)]
        public static void HandleSetFactionAtWar(Packet packet)
        {
            packet.ReadUInt16("FactionIndex");
        }

        [Parser(Opcode.CMSG_SET_FACTION_INACTIVE)]
        public static void HandleSetFactionInactive(Packet packet)
        {
            packet.ReadInt32("Index");
            packet.ReadBool("State");
        }

        [Parser(Opcode.CMSG_SET_FACTION_NOT_AT_WAR)]
        public static void HandleSetFactionNotAtWar(Packet packet)
        {
            packet.ReadUInt16("FactionIndex");
        }

        [Parser(Opcode.CMSG_SET_WATCHED_FACTION)]
        public static void HandleSetWatchedFaction(Packet packet)
        {
            packet.ReadInt32("FactionIndex");
        }

        [Parser(Opcode.CMSG_REQUEST_FORCED_REACTIONS)]
        public static void HandleReputationZero(Packet packet)
        {
        }
    }
}
