using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class WardenHandler
    {
        [Parser(Opcode.CMSG_WARDEN_DATA)]
        [Parser(Opcode.SMSG_WARDEN_DATA)]
        public static void HandleWardenData(Packet packet)
        {
            var opcode = packet.ReadEnum<WardenServerOpcode>("Warden Opcode", TypeCode.Int32);

            packet.ReadToEnd(); // Hack
        }
    }
}
