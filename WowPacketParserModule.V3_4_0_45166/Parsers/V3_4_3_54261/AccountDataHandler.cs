using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers.V3_4_3_54261
{
    public static class AccountDataHandler
    {
        [BuildMatch(ClientVersionBuild.V3_4_3_54261)]
        [Parser(Opcode.CMSG_REQUEST_ACCOUNT_DATA, true)]
        public static void HandleRequestAccountData(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadBitsE<AccountDataType>("Data Type", 4);
        }
    }
}
