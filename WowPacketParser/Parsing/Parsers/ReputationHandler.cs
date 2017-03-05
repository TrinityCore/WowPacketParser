using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ReputationHandler
    {
        [Parser(Opcode.CMSG_RESET_FACTION_CHEAT)]
        public static void HandleResetFactionCheat(Packet packet)
        {
            packet.Translator.ReadUInt32("Faction Id");
        }

        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            var count = packet.Translator.ReadInt32("Count");
            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadByteE<FactionFlag>("Faction Flags", i);
                packet.Translator.ReadUInt32E<ReputationRank>("Faction Standing", i);
            }
        }

        [Parser(Opcode.SMSG_SET_FACTION_VISIBLE)]
        [Parser(Opcode.CMSG_SET_WATCHED_FACTION)]
        [Parser(Opcode.SMSG_SET_FACTION_NOT_VISIBLE)]
        public static void HandleSetFactionMisc(Packet packet)
        {
            packet.Translator.ReadUInt32("FactionIndex");
        }

        [Parser(Opcode.SMSG_SET_FORCED_REACTIONS)]
        public static void HandleForcedReactions(Packet packet)
        {
            var counter = packet.Translator.ReadInt32("Factions");
            for (var i = 0; i < counter; i++)
            {
                packet.Translator.ReadUInt32("Faction Id");
                packet.Translator.ReadUInt32("Reputation Rank");
            }
        }

        [Parser(Opcode.SMSG_SET_FACTION_STANDING)]
        public static void HandleSetFactionStanding(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_4_0_8089))
                packet.Translator.ReadSingle("Reputation loss");

            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                packet.Translator.ReadBool("Play Visual");

            var count = packet.Translator.ReadInt32("Count");
            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadInt32("Faction List Id");
                packet.Translator.ReadInt32("Standing");
            }
        }

        [Parser(Opcode.CMSG_SET_FACTION_INACTIVE)]
        public static void HandleSetFactionInactive(Packet packet)
        {
            packet.Translator.ReadUInt32("Index");
            packet.Translator.ReadBool("State");
        }

        [Parser(Opcode.CMSG_SET_FACTION_AT_WAR)]
        public static void HandleSetFactionAtWar(Packet packet)
        {
            packet.Translator.ReadUInt32("Faction Id");
            packet.Translator.ReadBool("At War");
        }
    }
}
