using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_GUILD_PARTY_STATE, ClientVersionBuild.V11_1_7_61491)]
        public static void HandleGuildPartyStateResponse(Packet packet)
        {
            packet.ReadUInt32("Current guild members");
            packet.ReadUInt32("Needed guild members");
            packet.ReadSingle("Guild XP multiplier");
            packet.ReadBit("Is guild group");
        }
    }
}
