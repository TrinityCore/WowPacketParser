using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class ArenaHandler
    {
        public static void ReadClientOpponentSpecData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SpecializationID", idx);
            packet.ReadByte("Unk", idx);
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
