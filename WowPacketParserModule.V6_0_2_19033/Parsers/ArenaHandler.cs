using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ArenaHandler
    {
        [Parser(Opcode.SMSG_PVP_SEASON)]
        public static void HandlePvPSeason(Packet packet)
        {
            packet.ReadUInt32("CurrentSeason");
            packet.ReadUInt32("PreviousSeason");
        }

        public static void ReadClientOpponentSpecData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SpecializationID", idx);
            packet.ReadInt32("Unk", idx);
            packet.ReadPackedGuid128("Guid", idx);
        }

        [Parser(Opcode.SMSG_ARENA_PREP_OPPONENT_SPECIALIZATIONS)]
        public static void HandleArenaPrepOpponentSpecializations(Packet packet)
        {
            var count = packet.ReadInt32("OpponentDataCount");
            for (var i = 0; i < count; ++i)
                ReadClientOpponentSpecData(packet, "OpponentData", i);
        }
    }
}
