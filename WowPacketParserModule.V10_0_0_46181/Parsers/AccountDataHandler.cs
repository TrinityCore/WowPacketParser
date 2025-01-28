using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime64("ServerTime");

            var count = ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_6_53840) ? 16 : 15;

            for (var i = 0; i < count; ++i)
                packet.ReadTime64($"[{(AccountDataType)i}] Time", i);
        }
    }
}
