using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class AccountDataHandler
    {
        public static void ReadAccountCharacterList(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("WowAccountGUID", idx);
            packet.ReadPackedGuid128("CharacterGUID", idx);
            packet.ReadUInt32("VirtualRealmAddress", idx);
            packet.ReadByteE<Race>("Race" ,idx);
            packet.ReadByteE<Class>("Class", idx);
            packet.ReadByteE<Gender>("Gender", idx);
            packet.ReadByte("Level", idx);
            packet.ReadTime64("LastLogin", idx);
            packet.ReadUInt32("Unk440", idx);

            packet.ResetBitReader();

            uint characterNameLength = packet.ReadBits(6);
            uint realmNameLength = packet.ReadBits(9);

            packet.ReadWoWString("CharacterName", characterNameLength, idx);
            packet.ReadWoWString("RealmName", realmNameLength, idx);
        }

        [Parser(Opcode.SMSG_GET_ACCOUNT_CHARACTER_LIST_RESULT)]
        public static void HandleGetAccountCharacterListResult(Packet packet)
        {
            packet.ReadUInt32("Token");
            uint count = packet.ReadUInt32("CharacterCount");

            packet.ResetBitReader();

            packet.ReadBit("UnkBit");

            for (var i = 0; i < count; ++i)
            {
                ReadAccountCharacterList(packet, i);
            }
        }

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

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime64("ServerTime");

            for (var i = 0; i < 15; ++i)
                packet.ReadTime64($"[{(AccountDataType)i}] Time", i);
        }

        [Parser(Opcode.CMSG_REQUEST_ACCOUNT_DATA)]
        public static void HandleRequestAccountData(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32E<AccountDataType>("DataType");
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

        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleClientUpdateAccountData(Packet packet)
        {
            packet.ReadTime64("Time");

            var decompCount = packet.ReadInt32();
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32E<AccountDataType>("DataType");
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.ReadWoWString(decompCount);

            packet.AddValue("CompressedData", data);
        }

        [Parser(Opcode.CMSG_ACCOUNT_NOTIFICATION_ACKNOWLEDGED)]
        public static void HandleAccountNotificationAckknowledged(Packet packet)
        {
            packet.ReadUInt64("InstanceID");
            packet.ReadUInt32("OpenSeconds");
            packet.ReadUInt32("ReadSeconds");
        }

        [Parser(Opcode.SMSG_LOGOUT_CANCEL_ACK)]
        public static void HandleAccountNull(Packet packet)
        {
        }
    }
}
