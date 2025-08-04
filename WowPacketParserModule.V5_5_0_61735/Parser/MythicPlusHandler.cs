using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class MythicPlusHandler
    {
        [Parser(Opcode.SMSG_MYTHIC_PLUS_SEASON_DATA)]
        public static void HandleMythicPlusSeasonData(Packet packet)
        {
            packet.ReadBit("IsMythicPlusActive");
        }

        [Parser(Opcode.SMSG_MYTHIC_PLUS_CURRENT_AFFIXES)]
        public static void HandleMythicPlusCurrentAffixes(Packet packet)
        {
            var count = packet.ReadUInt32();
            for (int i = 0; i < count; i++)
            {
                packet.ReadInt32("KeystoneAffixID", i);
                packet.ReadInt32("RequiredSeason", i);
            }
        }

        [Parser(Opcode.SMSG_MYTHIC_PLUS_NEW_WEEK_RECORD)]
        public static void HandleMythicPlusNewWeekRecord(Packet packet)
        {
            packet.ReadInt32("MapChallengeModeID");
            packet.ReadInt32("CompletionTime"); // in ms
            packet.ReadUInt32("KeystoneLevel");
        }
    }
}
