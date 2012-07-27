using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ReputationHandler
    {
        [Parser(Opcode.CMSG_RESET_FACTION_CHEAT)]
        public static void HandleResetFactionCheat(Packet packet)
        {
            packet.ReadUInt32("Unk");
        }

        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; i++)
            {
                packet.ReadEnum<FactionFlag>("Faction Flags", TypeCode.Byte, i);
                packet.ReadEnum<ReputationRank>("Faction Standing", TypeCode.UInt32, i);
            }
        }

        [Parser(Opcode.SMSG_SET_FACTION_VISIBLE)]
        [Parser(Opcode.CMSG_SET_WATCHED_FACTION)]
        public static void HandleSetFactionMisc(Packet packet)
        {
            packet.ReadUInt32("Faction Id");
        }

        [Parser(Opcode.SMSG_SET_FORCED_REACTIONS)]
        public static void HandleForcedReactions(Packet packet)
        {
            var counter = packet.ReadInt32("Factions");
            for (var i = 0; i < counter; i++)
            {
                packet.ReadUInt32("Faction Id");
                packet.ReadUInt32("Reputation Rank");
            }
        }

        [Parser(Opcode.SMSG_SET_FACTION_STANDING)]
        public static void HandleSetFactionStanding(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_4_0_8089))
                packet.ReadSingle("Reputation loss");

            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                packet.ReadBoolean("Play Visual");

            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Faction List Id");
                packet.ReadInt32("Standing");
            }
        }

        [Parser(Opcode.CMSG_SET_FACTION_INACTIVE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSetFactionInactive(Packet packet)
        {
            packet.ReadUInt32("Faction Id");
            packet.ReadBoolean("Inactive");
        }
    }
}
