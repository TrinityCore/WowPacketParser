using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ReputationHandler
    {
        [Parser(Opcode.CMSG_REQUEST_FORCED_REACTIONS)]
        public static void HandleReputationZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_RESET_FACTION_CHEAT)]
        public static void HandleResetFactionCheat(Packet packet)
        {
            packet.Translator.ReadUInt32("FactionId");
            packet.Translator.ReadInt32("Level");
        }

        [Parser(Opcode.SMSG_FACTION_BONUS_INFO)]
        public static void HandleFactionBonusInfo(Packet packet)
        {
            for (var i = 0; i < 0x100; i++)
                packet.Translator.ReadBit("FactionHasBonus", i);
        }

        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            for (var i = 0; i < 0x100; i++)
            {
                packet.Translator.ReadByteE<FactionFlag>("FactionFlags", i);
                packet.Translator.ReadUInt32E<ReputationRank>("FactionStandings", i);
            }

            for (var i = 0; i < 0x100; i++)
                packet.Translator.ReadBit("FactionHasBonus", i);
        }

        [Parser(Opcode.SMSG_SET_FORCED_REACTIONS)]
        public static void HandleForcedReactions(Packet packet)
        {
            var counter = packet.Translator.ReadBits("ForcedReactionCount", 6);

            for (var i = 0; i < counter; i++)
            {
                packet.Translator.ReadUInt32("Faction");
                packet.Translator.ReadUInt32("Reaction");
            }
        }

        [Parser(Opcode.SMSG_SET_FACTION_STANDING)]
        public static void HandleSetFactionStanding(Packet packet)
        {
            packet.Translator.ReadSingle("BonusFromAchievementSystem");
            packet.Translator.ReadSingle("ReferAFriendBonus");

            var count = packet.Translator.ReadInt32("");
            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadInt32("Index");
                packet.Translator.ReadInt32("Standing");
            }

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("ShowVisual");
        }

        [Parser(Opcode.CMSG_SET_FACTION_AT_WAR)]
        public static void HandleSetFactionAtWar(Packet packet)
        {
            packet.Translator.ReadByte("FactionIndex");
        }

        [Parser(Opcode.CMSG_SET_FACTION_NOT_AT_WAR)]
        public static void HandleSetFactionNotAtWar(Packet packet)
        {
            packet.Translator.ReadByte("FactionIndex");
        }
    }
}
