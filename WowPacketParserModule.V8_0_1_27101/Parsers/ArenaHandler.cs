using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ArenaHandler
    {
        [Parser(Opcode.SMSG_PVP_SEASON, ClientVersionBuild.V8_1_0_28724)]
        public static void HandlePvPSeason(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
                packet.ReadInt32("MythicPlusSeasonID");
            packet.ReadInt32("CurrentSeason");
            packet.ReadInt32("PreviousSeason");
            packet.ReadInt32("PvPTier");
        }
    }
}
