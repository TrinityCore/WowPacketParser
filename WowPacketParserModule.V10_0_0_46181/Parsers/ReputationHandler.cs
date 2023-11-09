using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;


namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class ReputationHandler
    {
        public static int GetFactionCount()
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
                return 1000;
            else
                return 443;
        }

        [Parser(Opcode.SMSG_FACTION_BONUS_INFO)]
        public static void HandleFactionBonusInfo(Packet packet)
        {
            for (var i = 0; i < GetFactionCount(); i++)
                packet.ReadBit("FactionHasBonus", i);
        }

        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            for (var i = 0; i < GetFactionCount(); i++)
            {
                packet.ReadUInt16E<FactionFlag>("FactionFlags", i);
                packet.ReadInt32E<ReputationRank>("FactionStandings", i);
            }

            for (var i = 0; i < GetFactionCount(); i++)
                packet.ReadBit("FactionHasBonus", i);
        }

        [Parser(Opcode.SMSG_SET_FACTION_STANDING)]
        public static void HandleSetFactionStanding(Packet packet)
        {
            packet.ReadSingle("BonusFromAchievementSystem");

            var count = packet.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                packet.ReadInt32("Index");
                packet.ReadInt32("Standing");
            }

            packet.ResetBitReader();
            packet.ReadBit("ShowVisual");
        }
    }
}
