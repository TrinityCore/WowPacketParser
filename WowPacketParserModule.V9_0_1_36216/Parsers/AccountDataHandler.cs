using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.V9_1_5_40772)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime64("ServerTime");

            var count = ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423) ? 13 : 12;

            for (var i = 0; i < count; ++i)
                packet.ReadTime64($"[{(AccountDataType)i}] Time", i);
        }

        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA, ClientVersionBuild.V9_1_5_40772)]
        public static void HandleClientUpdateAccountData(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime64("Time");

            var decompCount = packet.ReadInt32();
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_6_53840))
                packet.ReadInt32E<AccountDataType>("DataType");
            else
                packet.ReadBitsE<AccountDataType>("Data Type", 4);
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);

            var data = pkt.ReadWoWString(decompCount);

            packet.AddValue("CompressedData", data);
        }

        [Parser(Opcode.CMSG_REQUEST_ACCOUNT_DATA, ClientVersionBuild.V9_1_5_40772)]
        public static void HandleRequestAccountData(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_6_53840))
                packet.ReadInt32E<AccountDataType>("DataType");
            else
                packet.ReadBitsE<AccountDataType>("Data Type", 4);
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA, ClientVersionBuild.V9_1_5_40772)]
        public static void HandleServerUpdateAccountData(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime64("Time");

            var decompCount = packet.ReadInt32();
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_6_53840))
                packet.ReadInt32E<AccountDataType>("DataType");
            else
                packet.ReadBitsE<AccountDataType>("Data Type", 4);
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.ReadWoWString(decompCount);

            packet.AddValue("Account Data", data);
        }
    }
}
