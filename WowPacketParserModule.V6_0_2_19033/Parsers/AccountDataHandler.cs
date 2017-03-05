using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime("Server Time");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }
        }

        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleClientUpdateAccountData(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime("Time");

            var decompCount = packet.ReadInt32();
            packet.ResetBitReader();
            packet.ReadBitsE<AccountDataType>("Data Type", 3);
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);

            var data = pkt.ReadWoWString(decompCount);

            packet.AddValue("CompressedData", data);
        }

        [Parser(Opcode.CMSG_REQUEST_ACCOUNT_DATA)]
        public static void HandleRequestAccountData(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadBitsE<AccountDataType>("Data Type", 3);
        }

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
