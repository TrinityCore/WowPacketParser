using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
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
    }
}
