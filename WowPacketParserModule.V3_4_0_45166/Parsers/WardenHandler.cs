using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class WardenHandler
    {
        [Parser(Opcode.CMSG_WARDEN3_DATA, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleWardenData(Packet packet)
        {
            packet.ReadInt32("Unk");
            var len = packet.ReadInt32();

            packet.ReadBytes("WardenData", len);
        }

        [Parser(Opcode.SMSG_WARDEN3_DATA, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleWarden3Data(Packet packet)
        {
            var len = packet.ReadInt32();

            packet.ReadBytes("WardenData", len);
        }

        [Parser(Opcode.SMSG_WARDEN3_ENABLED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleWarden3Enabled(Packet packet)
        {
            packet.ReadInt32("Unk");
        }
    }
}
