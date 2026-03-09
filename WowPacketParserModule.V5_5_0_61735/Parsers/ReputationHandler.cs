using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class ReputationHandler
    {
        public static void ReadFactionData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("FactionID", idx);
            packet.ReadUInt16E<FactionFlag>("Flags", idx);
            packet.ReadInt32E<ReputationRank>("Standing", idx);
        }

        public static void ReadFactionBonusData(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();

            packet.ReadInt32("FactionID", idx);
            packet.ReadBit("FactionHasBonus", idx);
        }

        public static void ReadFactionStandingData(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Index", indexes);
            packet.ReadInt32("Standing", indexes);
            packet.ReadInt32("FactionID", indexes);
        }

        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            var factionCount = packet.ReadUInt32();
            var bonusCount = packet.ReadUInt32();

            for (var i = 0u; i < factionCount; ++i)
                ReadFactionData(packet, "Factions", i);

            for (var i = 0u; i < bonusCount; ++i)
                ReadFactionBonusData(packet, "Bonus", i);
        }

        [Parser(Opcode.SMSG_FACTION_BONUS_INFO)]
        public static void HandleFactionBonusInfo(Packet packet)
        {
            uint factionCount = packet.ReadUInt32();

            for (var i = 0; i < factionCount; i++)
            {
                packet.ReadInt32("FactionID", i);
                packet.ReadBit("FactionHasBonus", i);
            }
        }

        [Parser(Opcode.SMSG_SET_FACTION_VISIBLE)]
        [Parser(Opcode.SMSG_SET_FACTION_NOT_VISIBLE)]
        public static void HandleSetFactionMisc(Packet packet)
        {
            packet.ReadUInt32("FactionIndex");
        }

        [Parser(Opcode.SMSG_SET_FACTION_STANDING)]
        public static void HandleSetFactionStanding(Packet packet)
        {
            packet.ReadSingle("BonusFromAchievementSystem");

            var count = packet.ReadInt32();
            for (int i = 0; i < count; i++)
                ReadFactionStandingData(packet, i);

            packet.ReadBit("ShowVisual");
        }

        [Parser(Opcode.CMSG_SET_FACTION_AT_WAR)]
        [Parser(Opcode.CMSG_SET_FACTION_NOT_AT_WAR)]
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

        [Parser(Opcode.CMSG_SET_WATCHED_FACTION)]
        public static void HandleSetWatchedFaction(Packet packet)
        {
            packet.ReadInt32("FactionIndex");
        }
    }
}
