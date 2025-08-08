using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.SMSG_CACHE_INFO)]
        public static void HandleCacheInfo(Packet packet)
        {
            var cacheInfoCount = packet.ReadUInt32("CacheInfoCount");

            packet.ResetBitReader();

            var signatureLen = packet.ReadBits(6);

            for (var i = 0; i < cacheInfoCount; ++i)
            {
                packet.ResetBitReader();

                var variableNameLen = packet.ReadBits(6);
                var valueLen = packet.ReadBits(6);

                packet.WriteLine($"[{i.ToString()}] VariableName: \"{packet.ReadWoWString((int)variableNameLen)}\" Value: \"{packet.ReadWoWString((int)valueLen)}\"");
            }

            packet.ReadWoWString("Signature", signatureLen);
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleServerUpdateAccountData(Packet packet)
        {
            packet.ReadTime64("Time");
            var decompCount = packet.ReadInt32();
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32E<AccountDataType>("DataType");
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.ReadWoWString(decompCount);

            packet.AddValue("Account Data", data);
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA_COMPLETE)]
        public static void HandleUpdateAccountDataComplete(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            packet.ReadInt32E<AccountDataType>("DataType");
            packet.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime64("ServerTime");

            for (var i = 0; i < 17; ++i)
                packet.ReadTime64($"[{(AccountDataType)i}] Time", i);
        }

        [Parser(Opcode.SMSG_LOGOUT_CANCEL_ACK)]
        public static void HandleAccountNull(Packet packet)
        {
        }
    }
}
