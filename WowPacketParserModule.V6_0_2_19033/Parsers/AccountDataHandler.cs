using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AccountDataHandler
    {

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleServerUpdateAccountData(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime("Time");

            var decompCount = packet.ReadInt32();
            packet.ResetBitReader();
            packet.ReadBitsE<AccountDataType>("Data Type", 3);
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.ReadWoWString(decompCount);

            packet.AddValue("Account Data", data);
        }
    }
}
