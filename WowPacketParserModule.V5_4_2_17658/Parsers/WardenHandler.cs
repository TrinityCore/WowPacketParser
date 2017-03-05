using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.V5_4_0_17359.Parsers
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
