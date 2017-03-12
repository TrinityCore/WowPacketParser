using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleClientUpdateAccountData(Packet packet)
        {
            var decompCount = packet.ReadInt32();
            packet.ReadTime("Login Time");
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.ReadWoWString(decompCount);
            pkt.ClosePacket();

            packet.ReadBitsE<AccountDataType>("Data Type", 3);
            packet.AddValue("Account Data", data);
        }
    }
}