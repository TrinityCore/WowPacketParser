using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class WardenHandler
    {
        [Parser(Opcode.CMSG_WARDEN_DATA)]
        [Parser(Opcode.SMSG_WARDEN_DATA)]
        public static void HandleWardenData(Packet packet)
        {
            var len = packet.ReadInt32();

            packet.ReadBytes(len);
        }
    }
}
